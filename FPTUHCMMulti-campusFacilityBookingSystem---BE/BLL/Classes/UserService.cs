using Applications.DTOs.Request;
using Applications.DTOs.Response;
using Applications.Helpers;
using AutoMapper;
using BLL.Interfaces;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories;

namespace BLL.Classes
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponseDto?> GetProfileAsync(string userId)
        {
            var user = await _unitOfWork.UserRepo.GetByIdWithRoleAsync(userId);
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto?> UpdateProfileAsync(string userId, UpdateUserProfileDto dto)
        {
            var user = await _unitOfWork.UserRepo.GetByIdWithRoleAsync(userId);
            if (user == null)
            {
                return null;
            }

            // validate sđt
            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                var phoneValidation = ValidatePhoneNumber(dto.PhoneNumber);
                if (!phoneValidation.IsValid)
                {
                    throw new ArgumentException(phoneValidation.ErrorMessage);
                }
            }

            // validate mssv (chỉ cho phép student role - rl0001)
            // mssv chỉ được cập nhật bởi user có role student
            // admin khi duyệt booking chỉ xem mssv, không cần duyệt
            if (!string.IsNullOrWhiteSpace(dto.StudentId))
            {
                // kiểm tra user có phải student không
                if (user.RoleId != "RL0001")
                {
                    throw new UnauthorizedAccessException("Chỉ user có role Student mới được cập nhật MSSV.");
                }

                // validate format mssv bằng regex
                // format: 2 chữ cái (se, ss, ib, mc) + 6 số (2 số đầu >= 14)
                // ví dụ đúng: se173456, ss152345
                // ví dụ sai: ab000111, se123456 (khóa 12 không hợp lệ)
                if (!DAL.Models.StudentIdRegex.IsValid(dto.StudentId))
                {
                    throw new ArgumentException("MSSV không hợp lệ. Ví dụ đúng: SE173456. Ví dụ sai: AB000111.");
                }
            }

            // cập nhật thông tin user
            user.PhoneNumber = dto.PhoneNumber;
            user.AvatarUrl = dto.AvatarUrl;
            
            // chỉ cập nhật mssv nếu user là student và đã validate thành công
            // mssv sẽ được hiển thị trong booking response để admin xem khi duyệt
            if (!string.IsNullOrWhiteSpace(dto.StudentId) && user.RoleId == "RL0001")
            {
                user.StudentId = dto.StudentId;
            }
            
            user.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Reload user with Role after update for correct mapping
            var updatedUser = await _unitOfWork.UserRepo.GetByIdWithRoleAsync(userId);
            return _mapper.Map<UserResponseDto>(updatedUser);
        }

        private (bool IsValid, string ErrorMessage) ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return (true, string.Empty); // Cho phép null/empty
            }

            // Chỉ chứa số
            if (!phoneNumber.All(char.IsDigit))
            {
                return (false, "Số điện thoại chỉ được chứa các chữ số.");
            }

            // Phải có đúng 10 số
            if (phoneNumber.Length != 10)
            {
                return (false, "Số điện thoại phải có đúng 10 chữ số.");
            }

            // Bắt đầu bằng 0
            if (!phoneNumber.StartsWith("0"))
            {
                return (false, "Số điện thoại phải bắt đầu bằng số 0.");
            }

            return (true, string.Empty);
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("Không tìm thấy người dùng.");
            }

            const string DefaultGooglePasswordHash = "Li9EQojtoWXDlQqOM55n0hFRn76PmCiheTGGM7zblAv5cFMyICasjdabsjdnouiwh1uidasoiu";
            if (user.Password == DefaultGooglePasswordHash || string.IsNullOrEmpty(user.Password))
            {
                throw new InvalidOperationException("Tài khoản này sử dụng đăng nhập Google. Không thể thay đổi mật khẩu.");
            }

            bool isOldPasswordValid = false;
            try
            {
                isOldPasswordValid = BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.Password);
            }
            catch
            {
                isOldPasswordValid = false;
            }

            if (!isOldPasswordValid)
            {
                throw new UnauthorizedAccessException("Mật khẩu hiện tại không đúng.");
            }

            if (dto.OldPassword == dto.NewPassword)
            {
                throw new ArgumentException("Mật khẩu mới phải khác mật khẩu hiện tại.");
            }

            // Validate password strength
            var validationResult = ValidatePasswordStrength(dto.NewPassword);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ErrorMessage);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
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

        public async Task<ApiResponseWithPagination<List<UserResponseDto>>> GetAllAsync(UserFilterDto filter)
        {
            var (items, total) = await _unitOfWork.UserRepo.GetFilteredAsync(
                filter.Name,
                filter.Email,
                filter.RoleId,
                filter.Status?.ToString(),
                filter.Page,
                filter.Limit
            );

            var responseDtos = _mapper.Map<List<UserResponseDto>>(items);

            return ApiResponseWithPagination<List<UserResponseDto>>.Ok(
                responseDtos,
                filter.Page,
                filter.Limit,
                total
            );
        }

        public async Task<ApiResponse<UserResponseDto>> GetByIdAsync(string id)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<UserResponseDto>.Fail(404, "Không tìm thấy người dùng.");
            }

            var responseDto = _mapper.Map<UserResponseDto>(user);
            return ApiResponse<UserResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse> DeleteAsync(string userId)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy user.");
            }

            // Soft delete: set status to Inactive (user cannot login anymore)
            user.Status = UserStatus.Inactive;
            user.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.UserRepo.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse.Ok();
        }
    }
}
