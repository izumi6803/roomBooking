using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class UpdateBookingFeedbackDto
    {
        [Range(1, 5, ErrorMessage = "Rating phải từ 1 (Very Poor) đến 5 (Excellent)")]
        public int? Rating { get; set; }

        [MaxLength(2000, ErrorMessage = "Comments không được vượt quá 2000 ký tự")]
        public string? Comments { get; set; }

        public bool? ReportIssue { get; set; }

        [MaxLength(1000, ErrorMessage = "Issue description không được vượt quá 1000 ký tự")]
        public string? IssueDescription { get; set; }
    }
}

