namespace Applications.DTOs.Request
{
    public class UpdateFacilityTypeDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DefaultAmenities { get; set; }
        public int? DefaultCapacity { get; set; }
        public int? TypicalDurationHours { get; set; }
        public string? IconUrl { get; set; }
    }
}


