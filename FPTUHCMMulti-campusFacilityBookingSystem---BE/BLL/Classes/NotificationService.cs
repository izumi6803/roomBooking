using Applications.DTOs.Request;
using Applications.DTOs.Response;
using Applications.Helpers;
using AutoMapper;
using BLL.Interfaces;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BLL.Classes
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<NotificationResponseDto>> GetByIdAsync(string id)
        {
            var notification = await _unitOfWork.NotificationRepo.GetByIdAsync(id);
            if (notification == null)
            {
                return ApiResponse<NotificationResponseDto>.Fail(404, "Không tìm thấy thông báo.");
            }

            var dto = _mapper.Map<NotificationResponseDto>(notification);
            return ApiResponse<NotificationResponseDto>.Ok(dto);
        }

        public async Task<ApiResponseWithPagination<List<NotificationResponseDto>>> GetFilteredAsync(NotificationFilterDto filter)
        {
            var (items, total) = await _unitOfWork.NotificationRepo.GetFilteredAsync(
                filter.UserId,
                filter.Type,
                filter.Status,
                filter.Page,
                filter.Limit
            );

            var dtos = _mapper.Map<List<NotificationResponseDto>>(items);
            return ApiResponseWithPagination<List<NotificationResponseDto>>.Ok(
                dtos,
                filter.Page,
                filter.Limit,
                total
            );
        }

        public async Task<ApiResponse<List<NotificationResponseDto>>> GetUnreadByUserIdAsync(string userId)
        {
            var notifications = await _unitOfWork.NotificationRepo.GetUnreadByUserIdAsync(userId);
            var dtos = _mapper.Map<List<NotificationResponseDto>>(notifications);
            return ApiResponse<List<NotificationResponseDto>>.Ok(dtos);
        }

        public async Task<ApiResponse<int>> GetUnreadCountByUserIdAsync(string userId)
        {
            var count = await _unitOfWork.NotificationRepo.GetUnreadCountByUserIdAsync(userId);
            return ApiResponse<int>.Ok(count);
        }

        public async Task<ApiResponse> MarkAsReadAsync(string notificationId)
        {
            var notification = await _unitOfWork.NotificationRepo.GetByIdAsync(notificationId);
            if (notification == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy thông báo.");
            }

            await _unitOfWork.NotificationRepo.MarkAsReadAsync(notificationId);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse.Ok();
        }

        public async Task<ApiResponse> MarkAllAsReadByUserIdAsync(string userId)
        {
            await _unitOfWork.NotificationRepo.MarkAllAsReadByUserIdAsync(userId);
            return ApiResponse.Ok();
        }

        public async Task<ApiResponse> DeleteAsync(string notificationId)
        {
            var notification = await _unitOfWork.NotificationRepo.GetByIdAsync(notificationId);
            if (notification == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy thông báo.");
            }

            await _unitOfWork.NotificationRepo.RemoveAsync(notification);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse.Ok();
        }

        // ==================== Internal Methods for Creating Notifications ====================

        public async Task CreateBookingPendingApprovalNotificationAsync(string bookingId)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null) return;

            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
            if (facility == null || string.IsNullOrEmpty(facility.FacilityManagerId)) return;

            var notificationId = await GenerateNotificationIdAsync();
            var notification = new Notification
            {
                NotificationId = notificationId,
                UserId = facility.FacilityManagerId,
                Type = NotificationType.Booking_Pending_Approval,
                Title = "Có booking mới chờ phê duyệt",
                Message = $"Booking {bookingId} từ {booking.User?.FullName} cho facility {facility.Name} đang chờ phê duyệt.",
                Status = NotificationStatus.Unread,
                BookingId = bookingId,
                CreatedAt = DateTimeHelper.VietnamNow
            };

            await _unitOfWork.NotificationRepo.CreateAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateBookingApprovedNotificationAsync(string bookingId)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null) return;

            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
            if (facility == null) return;

            var notificationId = await GenerateNotificationIdAsync();
            var notification = new Notification
            {
                NotificationId = notificationId,
                UserId = booking.UserId,
                Type = NotificationType.Booking_Approved,
                Title = "Booking đã được phê duyệt",
                Message = $"Booking {bookingId} cho facility {facility.Name} đã được phê duyệt. Thời gian: {booking.StartTime:dd/MM/yyyy HH:mm:ss} - {booking.EndTime:dd/MM/yyyy HH:mm:ss}",
                Status = NotificationStatus.Unread,
                BookingId = bookingId,
                CreatedAt = DateTimeHelper.VietnamNow
            };

            await _unitOfWork.NotificationRepo.CreateAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateBookingRejectedNotificationAsync(string bookingId, string? reason)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null) return;

            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
            if (facility == null) return;

            var notificationId = await GenerateNotificationIdAsync();
            var notification = new Notification
            {
                NotificationId = notificationId,
                UserId = booking.UserId,
                Type = NotificationType.Booking_Rejected,
                Title = "Booking đã bị từ chối",
                Message = $"Booking {bookingId} cho facility {facility.Name} đã bị từ chối.{(string.IsNullOrEmpty(reason) ? "" : $" Lý do: {reason}")}",
                Status = NotificationStatus.Unread,
                BookingId = bookingId,
                CreatedAt = DateTimeHelper.VietnamNow
            };

            await _unitOfWork.NotificationRepo.CreateAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateBookingCancelledByUserNotificationAsync(string bookingId)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null) return;

            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
            if (facility == null) return;

            var user = await _unitOfWork.UserRepo.GetByIdAsync(booking.UserId);
            if (user == null) return;

            // Lấy tất cả admin (RL0003 - Facility_Admin)
            var allUsers = await _unitOfWork.UserRepo.GetAllAsync();
            var admins = allUsers
                .Where(u => u.RoleId == "RL0003" && u.Status == UserStatus.Active)
                .ToList();

            var notifications = new List<Notification>();
            var now = DateTimeHelper.VietnamNow;

            foreach (var admin in admins)
            {
                var notificationId = await GenerateNotificationIdAsync();
                notifications.Add(new Notification
                {
                    NotificationId = notificationId,
                    UserId = admin.UserId,
                    Type = NotificationType.Booking_Cancelled,
                    Title = "User đã hủy booking",
                    Message = $"User {user.FullName ?? user.Email} đã hủy booking {bookingId} cho facility {facility.Name}. Thời gian đã đặt: {booking.StartTime:dd/MM/yyyy HH:mm:ss} - {booking.EndTime:dd/MM/yyyy HH:mm:ss}.{(string.IsNullOrEmpty(booking.CancellationReason) ? "" : $" Lý do: {booking.CancellationReason}")}",
                    Status = NotificationStatus.Unread,
                    BookingId = bookingId,
                    CreatedAt = now
                });
            }

            if (notifications.Any())
            {
                await _unitOfWork.NotificationRepo.AddRangeAsync(notifications);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateFeedbackReceivedNotificationAsync(string feedbackId)
        {
            var feedback = await _unitOfWork.BookingFeedbackRepo.GetByIdWithDetailsAsync(feedbackId);
            if (feedback == null) return;

            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(feedback.BookingId);
            if (booking == null) return;

            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
            if (facility == null || string.IsNullOrEmpty(facility.FacilityManagerId)) return;

            var notificationId = await GenerateNotificationIdAsync();
            var notification = new Notification
            {
                NotificationId = notificationId,
                UserId = facility.FacilityManagerId,
                Type = NotificationType.Feedback_Received,
                Title = "Có feedback mới",
                Message = $"Có feedback mới cho booking {feedback.BookingId} của facility {facility.Name}. Đánh giá: {feedback.Rating}/5",
                Status = NotificationStatus.Unread,
                BookingId = feedback.BookingId,
                FeedbackId = feedbackId,
                CreatedAt = DateTimeHelper.VietnamNow
            };

            await _unitOfWork.NotificationRepo.CreateAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateBookingReminderNotificationsAsync()
        {
            var now = DateTimeHelper.VietnamNow;
            var checkInReminderTime = now.AddMinutes(30); // 30 phút trước check-in
            var checkOutReminderTime = now.AddMinutes(30); // 30 phút trước check-out

            // Get bookings that need check-in reminder (Approved, chưa check-in, start time trong 1 giờ tới)
            var bookingsForCheckIn = await _unitOfWork.BookingRepo.GetAllAsync();
            bookingsForCheckIn = bookingsForCheckIn
                .Where(b => b.Status == BookingStatus.Approved
                    && b.CheckInTime == null
                    && b.StartTime <= checkInReminderTime
                    && b.StartTime > now)
                .ToList();

            // Get bookings that need check-out reminder (Approved, đã check-in, chưa check-out, end time trong 1 giờ tới)
            var bookingsForCheckOut = await _unitOfWork.BookingRepo.GetAllAsync();
            bookingsForCheckOut = bookingsForCheckOut
                .Where(b => b.Status == BookingStatus.Approved
                    && b.CheckInTime != null
                    && b.CheckOutTime == null
                    && b.EndTime <= checkOutReminderTime
                    && b.EndTime > now)
                .ToList();

            var notifications = new List<Notification>();

            foreach (var booking in bookingsForCheckIn)
            {
                // Check if notification already exists
                var existing = await _unitOfWork.NotificationRepo.GetFilteredAsync(
                    booking.UserId,
                    NotificationType.Booking_Reminder_CheckIn,
                    null,
                    1,
                    10
                );

                var hasExisting = existing.items.Any(n => n.BookingId == booking.BookingId && n.CreatedAt.Date == now.Date);
                if (hasExisting) continue;

                var notificationId = await GenerateNotificationIdAsync();
                var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
                notifications.Add(new Notification
                {
                    NotificationId = notificationId,
                    UserId = booking.UserId,
                    Type = NotificationType.Booking_Reminder_CheckIn,
                    Title = "Nhắc nhở check-in",
                    Message = $"Booking {booking.BookingId} cho facility {facility?.Name ?? "N/A"} sẽ bắt đầu lúc {booking.StartTime:dd/MM/yyyy HH:mm:ss}. Vui lòng check-in sớm.",
                    Status = NotificationStatus.Unread,
                    BookingId = booking.BookingId,
                    CreatedAt = now
                });
            }

            foreach (var booking in bookingsForCheckOut)
            {
                // Check if notification already exists
                var existing = await _unitOfWork.NotificationRepo.GetFilteredAsync(
                    booking.UserId,
                    NotificationType.Booking_Reminder_CheckOut,
                    null,
                    1,
                    10
                );

                var hasExisting = existing.items.Any(n => n.BookingId == booking.BookingId && n.CreatedAt.Date == now.Date);
                if (hasExisting) continue;

                var notificationId = await GenerateNotificationIdAsync();
                var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
                notifications.Add(new Notification
                {
                    NotificationId = notificationId,
                    UserId = booking.UserId,
                    Type = NotificationType.Booking_Reminder_CheckOut,
                    Title = "Nhắc nhở check-out",
                    Message = $"Booking {booking.BookingId} cho facility {facility?.Name ?? "N/A"} sẽ kết thúc lúc {booking.EndTime:dd/MM/yyyy HH:mm:ss}. Vui lòng check-out đúng giờ.",
                    Status = NotificationStatus.Unread,
                    BookingId = booking.BookingId,
                    CreatedAt = now
                });
            }

            if (notifications.Any())
            {
                await _unitOfWork.NotificationRepo.AddRangeAsync(notifications);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task ProcessNoShowBookingsAsync()
        {
            var now = DateTimeHelper.VietnamNow;
            var noShowThreshold = now.AddMinutes(-30); // 30 phút sau giờ check-in

            // Get bookings that should be marked as No_Show
            var bookings = await _unitOfWork.BookingRepo.GetAllAsync();
            var noShowBookings = bookings
                .Where(b => b.Status == BookingStatus.Approved
                    && b.CheckInTime == null
                    && b.StartTime <= noShowThreshold
                    && b.StartTime <= now)
                .ToList();

            var notifications = new List<Notification>();

            foreach (var booking in noShowBookings)
            {
                booking.Status = BookingStatus.No_Show;
                booking.UpdatedAt = DateTimeHelper.VietnamNow;
                await _unitOfWork.BookingRepo.UpdateAsync(booking);

                // Create notification for user
                var notificationId = await GenerateNotificationIdAsync();
                var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
                notifications.Add(new Notification
                {
                    NotificationId = notificationId,
                    UserId = booking.UserId,
                    Type = NotificationType.Booking_No_Show,
                    Title = "Booking đã bị đánh dấu No_Show",
                    Message = $"Booking {booking.BookingId} cho facility {facility?.Name ?? "N/A"} đã bị đánh dấu No_Show do không check-in trong vòng 30 phút sau giờ bắt đầu.",
                    Status = NotificationStatus.Unread,
                    BookingId = booking.BookingId,
                    CreatedAt = now
                });
            }

            if (notifications.Any())
            {
                await _unitOfWork.NotificationRepo.AddRangeAsync(notifications);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<string> GenerateNotificationIdAsync()
        {
            var notifications = await _unitOfWork.NotificationRepo.GetAllAsync();
            var maxId = 0;

            foreach (var notification in notifications)
            {
                if (notification.NotificationId.StartsWith("N") && notification.NotificationId.Length == 6)
                {
                    if (int.TryParse(notification.NotificationId.Substring(1), out var id))
                    {
                        if (id > maxId)
                            maxId = id;
                    }
                }
            }

            return $"N{(maxId + 1):D5}";
        }

        public async Task CreateBookingCancelledNotificationAsync(string bookingId, string reason)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null) return;

            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
            if (facility == null) return;

            var notificationId = await GenerateNotificationIdAsync();
            var notification = new Notification
            {
                NotificationId = notificationId,
                UserId = booking.UserId,
                Type = NotificationType.Booking_Rejected, // Use Rejected type for cancellation notification
                Title = "Booking đã bị hủy",
                Message = $"Booking {bookingId} cho facility {facility.Name} đã bị hủy. Lý do: {reason}",
                Status = NotificationStatus.Unread,
                BookingId = bookingId,
                CreatedAt = DateTimeHelper.VietnamNow
            };

            await _unitOfWork.NotificationRepo.CreateAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

