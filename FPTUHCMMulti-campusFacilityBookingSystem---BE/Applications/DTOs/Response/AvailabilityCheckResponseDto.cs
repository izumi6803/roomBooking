namespace Applications.DTOs.Response
{
    public class AvailabilityCheckResponseDto
    {
        public bool IsAvailable { get; set; }
        public string? ConflictReason { get; set; }
        public string? FacilityStatus { get; set; }
        public List<AlternativeFacilityDto>? AlternativeFacilities { get; set; }
    }

    public class AlternativeFacilityDto
    {
        public string FacilityId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CampusName { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string? RoomNumber { get; set; }
        public string? FloorNumber { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? NextAvailableTime { get; set; }
    }
}

