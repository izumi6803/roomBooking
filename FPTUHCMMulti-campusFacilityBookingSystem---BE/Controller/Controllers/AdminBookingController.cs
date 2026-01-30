using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using DAL.Models.Enums;
    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý bookings (Admin/Lecturer)
    /// </summary>
    [ApiController]
    [Route("api/admin/bookings")]
    [Authorize]
    public class AdminBookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public AdminBookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Lấy danh sách bookings với filtering (Admin view)
        /// </summary>
        /// <param name="status">Lọc theo trạng thái booking</param>
        /// <param name="page">Trang (default: 1)</param>
        /// <param name="limit">Số items/trang (default: 10)</param>
        /// <returns>Danh sách bookings</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập (Lecturer, Admin)
        /// 
        /// **Mục đích:** Xem tất cả bookings trong hệ thống để approve/reject
        /// 
        /// **Status values:**
        /// - Pending_Approval
        /// - Approved
        /// - Rejected
        /// - Cancelled
        /// - Completed
        /// - No_Show
        /// 
        /// **Lưu ý:** Có thể dùng "PENDING" (sẽ tự động convert thành "Pending_Approval")
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithPagination<List<BookingResponseDto>>), 200)]
        public async Task<IActionResult> GetBookings([FromQuery] BookingStatus? status, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            try
            {
                var filter = new BookingFilterDto
                {
                    Status = status,
                    Page = page,
                    Limit = limit
                };

                var result = await _bookingService.GetAllAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Duyệt booking (Approve)
        /// </summary>
        /// <param name="bookingId">Booking ID</param>
        /// <returns>Kết quả duyệt</returns>
        /// <response code="200">Duyệt thành công</response>
        /// <response code="400">Booking không ở trạng thái Pending_Approval</response>
        /// <response code="401">Chưa đăng nhập</response>
        /// <response code="404">Không tìm thấy booking</response>
        /// <response code="409">Facility đã được đặt trong khoảng thời gian này</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập (Lecturer, Admin)
        /// 
        /// **Mục đích:** Duyệt booking Pending_Approval → Approved
        /// 
        /// **Kiểm tra:**
        /// - Booking phải ở trạng thái Pending_Approval
        /// - Facility không bị trùng lịch với booking đã approved
        /// - Ghi nhận approver ID (người duyệt)
        /// 
        /// **Lưu ý:**
        /// - MSSV và Email của user sẽ được hiển thị trong response để admin xem
        /// - Không cần duyệt MSSV/Email, chỉ cần duyệt thông tin booking
        /// - MSSV được user tự cập nhật trong profile (chỉ Student role)
        /// </remarks>
        [HttpPatch("{bookingId}/approve")]
        [ProducesResponseType(typeof(ApiResponse<BookingResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 409)]
        public async Task<IActionResult> ApproveBooking(string bookingId)
        {
            // lấy approverid từ jwt token (người duyệt booking)
            var approverId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                            User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(approverId))
            {
                return Unauthorized(ApiResponse.Fail(401, "Không tìm thấy user id trong token."));
            }

            try
            {
                // gọi service để duyệt booking
                // service sẽ kiểm tra conflict, cập nhật status, và tạo notification cho user
                // mssv và email sẽ được tự động include trong response (qua automapper)
                var result = await _bookingService.ApproveBookingAsync(bookingId, approverId);
                
                if (!result.Success)
                {
                    if (result.Error?.Code == 404)
                        return NotFound(result);
                    if (result.Error?.Code == 400)
                        return BadRequest(result);
                    if (result.Error?.Code == 409)
                        return Conflict(result);
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Từ chối booking (Reject)
        /// </summary>
        /// <param name="bookingId">Booking ID</param>
        /// <param name="dto">Lý do từ chối (optional)</param>
        /// <returns>Kết quả từ chối</returns>
        /// <response code="200">Từ chối thành công</response>
        /// <response code="400">Booking không ở trạng thái Pending_Approval</response>
        /// <response code="401">Chưa đăng nhập</response>
        /// <response code="404">Không tìm thấy booking</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập (Lecturer, Admin)
        /// 
        /// **Mục đích:** Từ chối booking Pending_Approval → Rejected
        /// 
        /// **Lý do từ chối:**
        /// - Có thể gửi lý do (reason) trong body
        /// - Lý do sẽ được lưu vào `RejectionReason` field
        /// </remarks>
        [HttpPatch("{bookingId}/reject")]
        [ProducesResponseType(typeof(ApiResponse<BookingResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> RejectBooking(string bookingId, [FromBody] RejectBookingDto? dto = null)
        {
            var approverId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                            User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(approverId))
            {
                return Unauthorized(ApiResponse.Fail(401, "Không tìm thấy user id trong token."));
            }

            try
            {
                var reason = dto?.Reason;
                var result = await _bookingService.RejectBookingAsync(bookingId, approverId, reason);
                
                if (!result.Success)
                {
                    if (result.Error?.Code == 404)
                        return NotFound(result);
                    if (result.Error?.Code == 400)
                        return BadRequest(result);
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }
    }
}
