using DAL.Models.Enums;

namespace DAL.Models
{
    public class Notification
    {
        public string NotificationId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty; // User nhận thông báo
        public NotificationType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationStatus Status { get; set; } = NotificationStatus.Unread;
        
        // Related entities (optional)
        public string? BookingId { get; set; } // Nếu thông báo liên quan đến booking
        public string? FeedbackId { get; set; } // Nếu thông báo liên quan đến feedback
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; } // Thời gian đọc thông báo

        // Navigation properties
        public User User { get; set; } = null!;
        public Booking? Booking { get; set; }
        public BookingFeedback? Feedback { get; set; }
    }
}

