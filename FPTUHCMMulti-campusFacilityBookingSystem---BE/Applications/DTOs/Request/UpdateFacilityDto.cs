using DAL.Models.Enums;

namespace Applications.DTOs.Request
{
    public class UpdateFacilityDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public string? RoomNumber { get; set; }
        public string? FloorNumber { get; set; }
        public FacilityStatus? Status { get; set; }
        public string? Amenities { get; set; }
        public string? FacilityManagerId { get; set; }
        public int? MaxConcurrentBookings { get; set; }
    }
}
