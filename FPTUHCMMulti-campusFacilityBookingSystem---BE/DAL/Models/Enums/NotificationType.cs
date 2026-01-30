namespace DAL.Models.Enums
{
    public enum NotificationType
    {
        Booking_Pending_Approval,  // Thông báo cho Facility Admin khi có booking chờ duyệt
        Booking_Approved,          // Thông báo cho User khi booking được duyệt
        Booking_Rejected,          // Thông báo cho User khi booking bị từ chối
        Booking_Reminder_CheckIn,  // Thông báo cho User khi gần tới giờ check in
        Booking_Reminder_CheckOut, // Thông báo cho User khi gần tới giờ check out
        Feedback_Received,        // Thông báo cho Facility Admin khi có feedback mới
        Booking_No_Show,          // Thông báo cho User khi booking bị chuyển thành No_Show
        Booking_Cancelled         // Thông báo cho User khi booking bị hủy
    }
}

