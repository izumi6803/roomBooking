using DAL.Models.Enums;

namespace Applications.DTOs.Request
{
    public class BookingFilterDto : PagedRequestDto
    {
        public string? UserId { get; set; }
        public string? FacilityId { get; set; }
        public BookingStatus? Status { get; set; }
    }
}

