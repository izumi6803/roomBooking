namespace Applications.DTOs.Response
{
    public class UsageReportResponseDto
    {
        public string? CampusId { get; set; }
        public string? CampusName { get; set; }
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }
        public int TotalBookings { get; set; }
        public int ApprovedBookings { get; set; }
        public int RejectedBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int CancelledBookings { get; set; }
        public decimal UtilizationRate { get; set; }
        public decimal AvgRating { get; set; }
        public int UniqueUsers { get; set; }
        public string? PeakHours { get; set; }
        public string? BusiestDay { get; set; }
        public Dictionary<string, int> BookingsByStatus { get; set; } = new();
        public Dictionary<string, int> BookingsByFacility { get; set; } = new();
    }
}

