namespace Applications.DTOs.Request
{
    public class UsageReportRequestDto
    {
        public string? CampusId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}

