using DAL.Models.Enums;

namespace DAL.Models
{
    public class Booking
    {
        public string BookingId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FacilityId { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Purpose { get; set; }
        public string? Category { get; set; }
        public int? EstimatedAttendees { get; set; }
        public string? SpecialRequirements { get; set; } // JSON string
        public BookingStatus Status { get; set; } = BookingStatus.Pending_Approval;
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime? CheckInTime { get; set; }
        public string? CheckInNote { get; set; } // Ghi chú khi check-in (ví dụ: số lượng bàn, ghế)
        public string? CheckInImages { get; set; } // JSON array of image URLs
        public DateTime? CheckOutTime { get; set; }
        public string? CheckOutNote { get; set; } // Ghi chú khi check-out
        public string? CheckOutImages { get; set; } // JSON array of image URLs
        public bool IsUsed { get; set; } = false;
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; } = null!;
        public Facility Facility { get; set; } = null!;
        public User? Approver { get; set; }
        public BookingFeedback? Feedback { get; set; } // 1 Booking chỉ có 1 Feedback
    }
}
