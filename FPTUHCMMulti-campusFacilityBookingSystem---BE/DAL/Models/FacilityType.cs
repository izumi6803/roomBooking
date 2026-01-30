using DAL.Models.Enums;

namespace DAL.Models
{
    public class FacilityType
    {
        public string TypeId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DefaultAmenities { get; set; } // JSON string
        public int? DefaultCapacity { get; set; }
        public int? TypicalDurationHours { get; set; }
        public string? IconUrl { get; set; }
        public FacilityTypeStatus Status { get; set; } = FacilityTypeStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Facility> Facilities { get; set; } = new List<Facility>();
    }
}

