using DAL.Models.Enums;

namespace DAL.Models
{
    public class Facility
    {
        public string FacilityId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public string? RoomNumber { get; set; }
        public string? FloorNumber { get; set; }
        public string CampusId { get; set; } = string.Empty;
        public string TypeId { get; set; } = string.Empty;
        public FacilityStatus Status { get; set; }
        public string? Amenities { get; set; } // JSON string
        public string? FacilityManagerId { get; set; }
        public int MaxConcurrentBookings { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Campus Campus { get; set; } = null!;
        public FacilityType FacilityType { get; set; } = null!;
        public User? FacilityManager { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
