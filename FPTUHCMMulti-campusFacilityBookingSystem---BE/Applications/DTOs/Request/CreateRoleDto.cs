using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Tên vai trò là bắt buộc")]
        [MaxLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string RoleName { get; set; } = string.Empty;
    }
}

