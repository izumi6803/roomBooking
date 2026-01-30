using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class BookingFeedback
    {
        [Key]
        [MaxLength(6)]
        public string FeedbackId { get; set; } = string.Empty;

        [Required]
        public string BookingId { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [Range(1, 5, ErrorMessage = "Rating phải từ 1 đến 5")]
        public int Rating { get; set; }

        public string? Comments { get; set; }

        public bool ReportIssue { get; set; } = false;

        public string? IssueDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ResolvedAt { get; set; }

        // Navigation Properties
        public Booking Booking { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}