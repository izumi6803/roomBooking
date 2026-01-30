using DAL.Models.Enums;

namespace DAL.Models
{
    public class Campus
    {
        public string CampusId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public CampusStatus Status { get; set; } = CampusStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Facility> Facilities { get; set; } = new List<Facility>();
    }
}
