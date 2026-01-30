using DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class CreateFacilityDto
    {
        [Required(ErrorMessage = "Tên cơ sở là bắt buộc.")]
        [MaxLength(255, ErrorMessage = "Tên cơ sở không được vượt quá 255 ký tự.")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Sức chứa là bắt buộc.")]
        [Range(1, 10000, ErrorMessage = "Sức chứa phải từ 1 đến 10000.")]
        public int Capacity { get; set; }

        [MaxLength(50, ErrorMessage = "Số phòng không được vượt quá 50 ký tự.")]
        public string? RoomNumber { get; set; }

        [MaxLength(10, ErrorMessage = "Số tầng không được vượt quá 10 ký tự.")]
        public string? FloorNumber { get; set; }

        [Required(ErrorMessage = "Campus ID là bắt buộc.")]
        public string CampusId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Loại cơ sở là bắt buộc.")]
        public string TypeId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public FacilityStatus Status { get; set; } = FacilityStatus.Available;
        public string? Amenities { get; set; }
        public string? FacilityManagerId { get; set; }
        public int MaxConcurrentBookings { get; set; } = 1;
    }
}
