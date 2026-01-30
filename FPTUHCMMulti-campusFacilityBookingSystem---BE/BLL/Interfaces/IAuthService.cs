using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> LoginWithGoogleAsync(GoogleLoginDto dto);
        Task<bool> VerifyEmailAsync(string email, string code);
        Task<bool> ResendVerificationEmailAsync(string email);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string code, string newPassword);
    }
}

