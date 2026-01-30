namespace Applications.DTOs.Response
{
    public class BookingFeedbackResponseDto
    {
        public string FeedbackId { get; set; } = string.Empty;
        public string BookingId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FacilityName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public bool ReportIssue { get; set; }
        public string? IssueDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public bool IsResolved => ResolvedAt.HasValue;
    }
}

