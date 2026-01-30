using DAL.Models.Enums;

namespace Applications.DTOs.Request
{
    public class UpdateCampusDto
    {
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public CampusStatus? Status { get; set; }
    }
}