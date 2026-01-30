using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email hoặc Tên đăng nhập là bắt buộc")]
        public string EmailOrUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; } = string.Empty;
    }
}


