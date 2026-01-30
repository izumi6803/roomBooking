using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class CreateBookingDto
    {
        [Required(ErrorMessage = "ID người dùng là bắt buộc")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID cơ sở vật chất là bắt buộc")]
        public string FacilityId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thời gian bắt đầu là bắt buộc")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc là bắt buộc")]
        public DateTime EndTime { get; set; }

        public string? Purpose { get; set; }
        public string? Category { get; set; }
        public int? EstimatedAttendees { get; set; }
        public string? SpecialRequirements { get; set; }
    }
}

