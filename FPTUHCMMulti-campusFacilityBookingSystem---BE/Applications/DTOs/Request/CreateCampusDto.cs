using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class CreateCampusDto
    {
        [Required(ErrorMessage = "Tên campus là bắt buộc.")]
        [MaxLength(255, ErrorMessage = "Tên campus không được vượt quá 255 ký tự.")]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public CampusStatus Status { get; set; } = CampusStatus.Active;
    }
}
