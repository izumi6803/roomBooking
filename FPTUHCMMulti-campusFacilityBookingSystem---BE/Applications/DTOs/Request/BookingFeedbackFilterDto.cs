namespace Applications.DTOs.Request
{
    public class BookingFeedbackFilterDto
    {
        public string? UserId { get; set; }
        public string? BookingId { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
        public bool? ReportIssue { get; set; }
        public bool? IsResolved { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}

