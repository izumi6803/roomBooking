namespace Applications.DTOs.Response
{
    public class BookingReportResponseDto
    {
        public PeriodInfo Period { get; set; } = new();
        public OverallStatistics Overall { get; set; } = new();
        public List<DailyStatistics> DailyStats { get; set; } = new();
        public List<FacilityStatistics> FacilityStats { get; set; } = new();
        public List<CampusStatistics> CampusStats { get; set; } = new();
    }

    public class PeriodInfo
    {
        public string PeriodType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
    }

    public class OverallStatistics
    {
        public int TotalBookings { get; set; }
        public int ApprovedBookings { get; set; }
        public int RejectedBookings { get; set; }
        public int CancelledBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int PendingBookings { get; set; }
        public double ApprovalRate { get; set; } // %
        public double CancellationRate { get; set; } // %
        public double CompletionRate { get; set; } // %
        public double UtilizationRate { get; set; } // % (completed / total available slots)
    }

    public class DailyStatistics
    {
        public DateTime Date { get; set; }
        public int TotalBookings { get; set; }
        public int CompletedBookings { get; set; }
        public double UtilizationRate { get; set; } // %
    }

    public class FacilityStatistics
    {
        public string FacilityId { get; set; } = string.Empty;
        public string FacilityName { get; set; } = string.Empty;
        public string CampusName { get; set; } = string.Empty;
        public int TotalBookings { get; set; }
        public int CompletedBookings { get; set; }
        public double UtilizationRate { get; set; } // %
        public double AverageRating { get; set; } // From feedbacks
    }

    public class CampusStatistics
    {
        public string CampusId { get; set; } = string.Empty;
        public string CampusName { get; set; } = string.Empty;
        public int TotalBookings { get; set; }
        public int CompletedBookings { get; set; }
        public double UtilizationRate { get; set; } // %
        public int TotalFacilities { get; set; }
    }
}

