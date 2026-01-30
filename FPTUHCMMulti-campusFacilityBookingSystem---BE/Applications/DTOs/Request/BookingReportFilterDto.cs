namespace Applications.DTOs.Request
{
    public class BookingReportFilterDto
    {
        public string? PeriodType { get; set; } // "day", "week", "month", "year", "custom"
        public int? Days { get; set; } // 1, 3, 7, 14, 30
        public int? Month { get; set; } // 1-12
        public int? Year { get; set; } // 2024, 2025...
        public DateTime? StartDate { get; set; } // For custom period
        public DateTime? EndDate { get; set; } // For custom period
        public string? CampusId { get; set; }
        public string? FacilityId { get; set; }
    }
}

