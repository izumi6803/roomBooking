namespace Applications.DTOs.Request
{
    public class UpdateBookingDto
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Purpose { get; set; }
        public string? Category { get; set; }
        public int? EstimatedAttendees { get; set; }
        public string? SpecialRequirements { get; set; }
        public string? Status { get; set; }
    }
}

