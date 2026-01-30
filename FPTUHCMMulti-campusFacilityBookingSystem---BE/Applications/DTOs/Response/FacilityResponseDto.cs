namespace Applications.DTOs.Response
{
    public class FacilityResponseDto
    {
        public string FacilityId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public string? RoomNumber { get; set; }
        public string? FloorNumber { get; set; }
        public string CampusId { get; set; } = string.Empty;
        public string CampusName { get; set; } = string.Empty;
        public string TypeId { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Amenities { get; set; }
        public string? FacilityManagerId { get; set; }
        public string? FacilityManagerName { get; set; }
        public int MaxConcurrentBookings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
