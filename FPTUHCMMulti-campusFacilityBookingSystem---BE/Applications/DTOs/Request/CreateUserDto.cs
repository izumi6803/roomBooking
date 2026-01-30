using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class CreateUserDto
    {
        public string? Email { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        public string FullName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        /// <summary>
        /// MSSV, format: SE/SS/IB/MC + 6 số, 2 số đầu >= 14 
        /// </summary>
        [RegularExpression(@"^(SE|SS|IB|MC)(1[4-9]|[2-9][0-9])[0-9]{4}$",
            ErrorMessage = "MSSV không hợp lệ. Ví dụ đúng: SE173162. Ví dụ sai: AB000111.")]
        public string? StudentId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        public string RoleId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cơ sở là bắt buộc")]
        public string CampusId { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }
    }
}


