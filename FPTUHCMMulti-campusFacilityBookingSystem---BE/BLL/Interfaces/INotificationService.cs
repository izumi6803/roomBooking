using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface INotificationService
    {
        Task<ApiResponse<NotificationResponseDto>> GetByIdAsync(string id);
        Task<ApiResponseWithPagination<List<NotificationResponseDto>>> GetFilteredAsync(NotificationFilterDto filter);
        Task<ApiResponse<List<NotificationResponseDto>>> GetUnreadByUserIdAsync(string userId);
        Task<ApiResponse<int>> GetUnreadCountByUserIdAsync(string userId);
        Task<ApiResponse> MarkAsReadAsync(string notificationId);
        Task<ApiResponse> MarkAllAsReadByUserIdAsync(string userId);
        Task<ApiResponse> DeleteAsync(string notificationId);
        
        // Internal methods for creating notifications
        Task CreateBookingPendingApprovalNotificationAsync(string bookingId);
        Task CreateBookingApprovedNotificationAsync(string bookingId);
        Task CreateBookingRejectedNotificationAsync(string bookingId, string? reason);
        Task CreateBookingCancelledByUserNotificationAsync(string bookingId); // Thông báo cho admin khi user hủy booking
        Task CreateFeedbackReceivedNotificationAsync(string feedbackId);
        Task CreateBookingReminderNotificationsAsync(); // For check-in/check-out reminders
        Task ProcessNoShowBookingsAsync(); // Check and update No_Show bookings
        Task CreateBookingCancelledNotificationAsync(string bookingId, string reason); // Create notification when booking is cancelled
    }
}

