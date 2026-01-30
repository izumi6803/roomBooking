using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class CheckAvailabilityDto
    {
        [Required(ErrorMessage = "Facility ID là bắt buộc")]
        public string FacilityId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thời gian bắt đầu là bắt buộc")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc là bắt buộc")]
        public DateTime EndTime { get; set; }

        public string? ExcludeBookingId { get; set; } // Để check khi update booking
    }
}

