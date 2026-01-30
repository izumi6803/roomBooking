using Applications.DTOs.Request;
using Applications.DTOs.Response;
using Applications.Helpers;
using BLL.Interfaces;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace BLL.Classes
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private const string DefaultGooglePasswordHash = "Li9EQojtoWXDlQqOM55n0hFRn76PmCiheTGGM7zblAv5cFMyICasjdabsjdnouiwh1uidasoiu";

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _unitOfWork.UserRepo.GetByEmailOrUsernameAsync(dto.EmailOrUsername);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Email/tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // Check if user is trying to login with Google account
            if (user.Password == DefaultGooglePasswordHash)
            {
                throw new UnauthorizedAccessException("Tài khoản này sử dụng đăng nhập Google. Vui lòng đăng nhập bằng Google.");
            }

            // Check if user has password (email/username login)
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new UnauthorizedAccessException("Email/tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // Ensure user is using the correct login method
            // If user has email (not null), they should login with email only
            // If user has username (not null), they should login with username only
            bool isValidLoginMethod = false;
            if (!string.IsNullOrEmpty(user.Email) && user.Email == dto.EmailOrUsername)
            {
                isValidLoginMethod = true;
            }
            else if (!string.IsNullOrEmpty(user.UserName) && user.UserName == dto.EmailOrUsername)
            {
                isValidLoginMethod = true;
            }

            if (!isValidLoginMethod)
            {
                throw new UnauthorizedAccessException("Email/tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // Verify password
            bool isPasswordValid = false;
            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            }
            catch
            {
                isPasswordValid = false;
            }

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Email/tên đăng nhập hoặc mật khẩu không đúng.");
            }

            if (user.Status != UserStatus.Active)
            {
                throw new UnauthorizedAccessException("Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ hỗ trợ.");
            }

            // Update last login
            user.LastLogin = DateTimeHelper.VietnamNow;
            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Token = token,
                UserId = user.UserId,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                RoleId = user.RoleId,
                IsVerified = user.IsVerify == VerificationStatus.Verified
            };
        }

        public async Task<AuthResponseDto> LoginWithGoogleAsync(GoogleLoginDto dto)
        {
            // Validate Google ID token
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                dto.IdToken,
                new ValidationSettings
                {
                    Audience = new[] { _configuration["Google:ClientId"] },
                    IssuedAtClockTolerance = TimeSpan.FromMinutes(5)
                });

            var email = payload.Email;

            // Validate email domain
            if (!IsValidFptEmail(email))
            {
                throw new UnauthorizedAccessException("Chỉ cho phép địa chỉ email @fpt.edu.vn hoặc @fe.edu.vn.");
            }

            var user = await _unitOfWork.UserRepo.GetByEmailAsync(email);

            // If user doesn't exist, create new user
            if (user == null)
            {
                user = await CreateGoogleUserAsync(payload);
            }
            else
            {
                // Check if user is trying to login with Google but account was created with email/username
                if (user.Password != DefaultGooglePasswordHash && !string.IsNullOrEmpty(user.Password))
                {
                    throw new UnauthorizedAccessException("Tài khoản này sử dụng đăng nhập bằng email/tên đăng nhập. Vui lòng đăng nhập bằng thông tin đăng nhập của bạn.");
                }
            }

            if (user.Status != UserStatus.Active)
            {
                throw new UnauthorizedAccessException("Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ hỗ trợ.");
            }

            // Update last login
            user.LastLogin = DateTimeHelper.VietnamNow;
            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Token = token,
                UserId = user.UserId,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                RoleId = user.RoleId,
                IsVerified = user.IsVerify == VerificationStatus.Verified
            };
        }

        public async Task<bool> VerifyEmailAsync(string email, string code)
        {
            var user = await _unitOfWork.UserRepo.GetByEmailAsync(email);

            if (user == null ||
                string.IsNullOrEmpty(user.EmailVerificationCode) ||
                user.EmailVerificationCode != code ||
                user.EmailVerificationCodeExpiry < DateTime.UtcNow)
            {
                return false;
            }

            user.IsVerify = VerificationStatus.Verified;
            user.EmailVerificationCode = null;
            user.EmailVerificationCodeExpiry = null;
            user.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ResendVerificationEmailAsync(string email)
        {
            var user = await _unitOfWork.UserRepo.GetByEmailAsync(email);

            if (user == null)
            {
                throw new ArgumentException("Không tìm thấy người dùng với email này.");
            }

            // Check if user is already verified
            if (user.IsVerify == VerificationStatus.Verified)
            {
                throw new InvalidOperationException("Email đã được xác thực.");
            }

            // Generate new verification code (6 digits)
            user.EmailVerificationCode = GenerateVerificationCode();
            user.EmailVerificationCodeExpiry = DateTimeHelper.VietnamNow.AddHours(24);
            user.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Send verification email
            await _emailService.SendVerificationCodeAsync(user.Email!, user.EmailVerificationCode);

            return true;
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _unitOfWork.UserRepo.GetByEmailAsync(email);

            if (user == null)
            {
                throw new ArgumentException("Không tìm thấy người dùng với email này.");
            }

            // Check if user uses Google login
            if (user.Password == DefaultGooglePasswordHash)
            {
                throw new InvalidOperationException("Tài khoản này sử dụng đăng nhập Google. Không thể đặt lại mật khẩu.");
            }

            // Generate password reset code (6 digits)
            user.PasswordResetCode = GenerateVerificationCode();
            user.PasswordResetCodeExpiry = DateTimeHelper.VietnamNow.AddHours(1); // 1 hour expiry
            user.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Send password reset email
            await _emailService.SendPasswordResetCodeAsync(user.Email!, user.PasswordResetCode);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(string email, string code, string newPassword)
        {
            var user = await _unitOfWork.UserRepo.GetByEmailAsync(email);

            if (user == null ||
                string.IsNullOrEmpty(user.PasswordResetCode) ||
                user.PasswordResetCode != code ||
                user.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return false;
            }

            // Validate password strength
            var validationResult = ValidatePasswordStrength(newPassword);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ErrorMessage);
            }

            // Update password
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetCode = null;
            user.PasswordResetCodeExpiry = null;
            user.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private (bool IsValid, string ErrorMessage) ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return (false, "Mật khẩu không được để trống.");
            }

            // Check minimum length
            if (password.Length < 8)
            {
                return (false, "Mật khẩu phải có ít nhất 8 ký tự.");
            }

            // Check for uppercase letter
            if (!password.Any(char.IsUpper))
            {
                return (false, "Mật khẩu phải có ít nhất một chữ cái viết hoa.");
            }

            // Check for lowercase letter
            if (!password.Any(char.IsLower))
            {
                return (false, "Mật khẩu phải có ít nhất một chữ cái thường.");
            }

            // Check for digit
            if (!password.Any(char.IsDigit))
            {
                return (false, "Mật khẩu phải có ít nhất một chữ số.");
            }

            // Check for special character
            var specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            if (!password.Any(c => specialChars.Contains(c)))
            {
                return (false, "Mật khẩu phải có ít nhất một ký tự đặc biệt (!@#$%^&*()_+-=[]{}|;:,.<>?).");
            }

            return (true, string.Empty);
        }

        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private async Task<User> CreateGoogleUserAsync(Payload payload)
        {
            // Generate user ID (format: U0001)
            var userId = await GenerateUserIdAsync();

            // Determine role based on email domain
            var roleId = DetermineRoleByEmailDomain(payload.Email);

            var user = new User
            {
                UserId = userId,
                Email = payload.Email,
                FullName = payload.Name,
                Password = DefaultGooglePasswordHash,
                RoleId = roleId,
                Status = UserStatus.Active,
                IsVerify = VerificationStatus.Unverified,
                AvatarUrl = null, // Leave avatar blank
                EmailVerificationCode = GenerateVerificationCode(),
                EmailVerificationCodeExpiry = DateTimeHelper.VietnamNow.AddHours(24),
                CreatedAt = DateTimeHelper.VietnamNow,
                UpdatedAt = DateTimeHelper.VietnamNow
            };

            await _unitOfWork.UserRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Send verification email
            await _emailService.SendVerificationCodeAsync(user.Email!, user.EmailVerificationCode);

            return user;
        }

        private string DetermineRoleByEmailDomain(string email)
        {
            // @fpt.edu.vn -> Student (RL0001)
            // @fe.edu.vn -> Lecturer (RL0002)
            if (email.EndsWith("@fpt.edu.vn", StringComparison.OrdinalIgnoreCase))
            {
                return "RL0001"; // Student
            }
            else if (email.EndsWith("@fe.edu.vn", StringComparison.OrdinalIgnoreCase))
            {
                return "RL0002"; // Lecturer
            }

            return "RL0001"; // Default to Student
        }

        private bool IsValidFptEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return email.EndsWith("@fpt.edu.vn", StringComparison.OrdinalIgnoreCase) ||
                   email.EndsWith("@fe.edu.vn", StringComparison.OrdinalIgnoreCase);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.RoleId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("IsVerified", (user.IsVerify == VerificationStatus.Verified).ToString()),
                new Claim("AvatarUrl", user.AvatarUrl ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(48),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<string> GenerateUserIdAsync()
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            var maxId = 0;
            
            foreach (var user in users)
            {
                if (user.UserId.StartsWith("U") && user.UserId.Length == 6)
                {
                    if (int.TryParse(user.UserId.Substring(1), out var id))
                    {
                        if (id > maxId)
                            maxId = id;
                    }
                }
            }

            return $"U{(maxId + 1):D5}";
        }
    }
}
