namespace Applications.DTOs.Response
{
    public class FacilityTypeResponseDto
    {
        public string TypeId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DefaultAmenities { get; set; }
        public int? DefaultCapacity { get; set; }
        public int? TypicalDurationHours { get; set; }
        public string? IconUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}


