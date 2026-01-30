using DAL.Models.Enums;

namespace Applications.DTOs.Request
{
    public class NotificationFilterDto
    {
        public string? UserId { get; set; }
        public NotificationType? Type { get; set; }
        public NotificationStatus? Status { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}

