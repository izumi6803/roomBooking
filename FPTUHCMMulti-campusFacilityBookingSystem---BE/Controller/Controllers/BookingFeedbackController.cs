using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý Feedback cho Booking
    /// </summary>
    [ApiController]
    [Route("api/feedbacks")]
    [Authorize]
    public class BookingFeedbackController : ControllerBase
    {
        private readonly IBookingFeedbackService _feedbackService;

        public BookingFeedbackController(IBookingFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// Lấy tất cả feedbacks với phân trang và filter
        /// </summary>
        /// <param name="filter">Bộ lọc feedback</param>
        /// <returns>Danh sách feedbacks</returns>
        /// <response code="200">Trả về danh sách feedbacks</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Lấy danh sách feedback với filter nâng cao
        /// 
        /// **Filters:**
        /// - userId: Filter theo user tạo feedback
        /// - bookingId: Filter theo booking
        /// - minRating / maxRating: Filter theo rating (1-5)
        /// - reportIssue: Chỉ lấy feedback có report issue
        /// - isResolved: Chỉ lấy issue đã/chưa resolved
        /// - page: Trang hiện tại (default: 1)
        /// - limit: Số item per page (default: 10)
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithPagination<List<BookingFeedbackResponseDto>>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] BookingFeedbackFilterDto filter)
        {
            try
            {
                var result = await _feedbackService.GetFilteredAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy feedback theo ID
        /// </summary>
        /// <param name="id">Feedback ID</param>
        /// <returns>Chi tiết feedback</returns>
        /// <response code="200">Trả về feedback</response>
        /// <response code="404">Không tìm thấy feedback</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Lấy chi tiết một feedback
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<BookingFeedbackResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _feedbackService.GetByIdAsync(id);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy feedbacks của một booking
        /// </summary>
        /// <param name="bookingId">Booking ID</param>
        /// <returns>Danh sách feedbacks của booking</returns>
        /// <response code="200">Trả về danh sách feedbacks</response>
        /// <response code="404">Không tìm thấy booking</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Lấy tất cả feedback của một booking
        /// </remarks>
        [HttpGet("booking/{bookingId}")]
        [ProducesResponseType(typeof(ApiResponse<List<BookingFeedbackResponseDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetByBookingId(string bookingId)
        {
            try
            {
                var result = await _feedbackService.GetByBookingIdAsync(bookingId);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy feedbacks của một user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Danh sách feedbacks của user</returns>
        /// <response code="200">Trả về danh sách feedbacks</response>
        /// <response code="404">Không tìm thấy user</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Lấy tất cả feedback mà user đã tạo
        /// </remarks>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(ApiResponse<List<BookingFeedbackResponseDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var result = await _feedbackService.GetByUserIdAsync(userId);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy feedbacks của riêng mình
        /// </summary>
        /// <returns>Danh sách feedbacks của user hiện tại</returns>
        /// <response code="200">Trả về danh sách feedbacks</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** User xem lại tất cả feedback mình đã tạo
        /// </remarks>
        [HttpGet("my-feedbacks")]
        [ProducesResponseType(typeof(ApiResponse<List<BookingFeedbackResponseDto>>), 200)]
        public async Task<IActionResult> GetMyFeedbacks()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(ApiResponse.Fail(401, "Không xác định được user"));
                }

                var result = await _feedbackService.GetByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy rating trung bình của một facility
        /// </summary>
        /// <param name="facilityId">Facility ID</param>
        /// <returns>Rating trung bình (0.0 - 5.0)</returns>
        /// <response code="200">Trả về rating trung bình</response>
        /// <response code="404">Không tìm thấy facility</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Xem rating trung bình của facility để đánh giá chất lượng
        /// </remarks>
        [HttpGet("facility/{facilityId}/rating")]
        [ProducesResponseType(typeof(ApiResponse<double>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetFacilityRating(string facilityId)
        {
            try
            {
                var result = await _feedbackService.GetAverageFacilityRatingAsync(facilityId);
                if (!result.Success)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy danh sách issues chưa resolve
        /// </summary>
        /// <returns>Danh sách feedbacks có report issue chưa resolve</returns>
        /// <response code="200">Trả về danh sách issues</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Facility Admin xem các issue cần xử lý
        /// </remarks>
        [HttpGet("unresolved-issues")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<List<BookingFeedbackResponseDto>>), 200)]
        public async Task<IActionResult> GetUnresolvedIssues()
        {
            try
            {
                var result = await _feedbackService.GetUnresolvedIssuesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Tạo feedback mới cho booking
        /// </summary>
        /// <param name="dto">Thông tin feedback</param>
        /// <returns>Feedback đã tạo</returns>
        /// <response code="201">Tạo feedback thành công</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc booking chưa completed</response>
        /// <response code="403">Không có quyền tạo feedback cho booking này</response>
        /// <response code="404">Không tìm thấy booking</response>
        /// <response code="409">Đã tạo feedback cho booking này rồi</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** User tạo feedback sau khi hoàn thành booking
        /// 
        /// **Business Rules:**
        /// - Chỉ tạo feedback cho booking của mình
        /// - Booking phải ở trạng thái Completed
        /// - Mỗi user chỉ tạo 1 feedback cho 1 booking
        /// - Nếu reportIssue = true, phải có issueDescription
        /// 
        /// **Rating Scale:**
        /// - 5 = Excellent
        /// - 4 = Good
        /// - 3 = Average
        /// - 2 = Poor
        /// - 1 = Very Poor
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<BookingFeedbackResponseDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 403)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [ProducesResponseType(typeof(ApiResponse), 409)]
        public async Task<IActionResult> Create([FromBody] CreateBookingFeedbackDto dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(ApiResponse.Fail(401, "Không xác định được user"));
                }

                var result = await _feedbackService.CreateAsync(userId, dto);
                if (!result.Success)
                {
                    if (result.Error?.Code == 403)
                        return Forbid();
                    if (result.Error?.Code == 409)
                        return Conflict(result);
                    if (result.Error?.Code == 404)
                        return NotFound(result);
                    return BadRequest(result);
                }

                return CreatedAtAction(nameof(GetById), new { id = result.Data?.FeedbackId }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật feedback
        /// </summary>
        /// <param name="id">Feedback ID</param>
        /// <param name="dto">Thông tin cập nhật</param>
        /// <returns>Feedback đã cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="403">Không có quyền cập nhật feedback này</response>
        /// <response code="404">Không tìm thấy feedback</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** User chỉnh sửa feedback của mình
        /// 
        /// **Business Rules:**
        /// - Chỉ owner mới được cập nhật feedback
        /// - Nếu reportIssue = true, phải có issueDescription
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<BookingFeedbackResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 403)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateBookingFeedbackDto dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(ApiResponse.Fail(401, "Không xác định được user"));
                }

                var result = await _feedbackService.UpdateAsync(id, userId, dto);
                if (!result.Success)
                {
                    if (result.Error?.Code == 403)
                        return Forbid();
                    if (result.Error?.Code == 404)
                        return NotFound(result);
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
        /// Resolve issue trong feedback
        /// </summary>
        /// <param name="id">Feedback ID</param>
        /// <returns>Feedback đã resolve</returns>
        /// <response code="200">Resolve thành công</response>
        /// <response code="400">Feedback không có issue hoặc đã resolve rồi</response>
        /// <response code="404">Không tìm thấy feedback</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Facility Admin đánh dấu đã xử lý issue
        /// 
        /// **Business Rules:**
        /// - Feedback phải có reportIssue = true
        /// - Chưa được resolve trước đó
        /// </remarks>
        [HttpPatch("{id}/resolve")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<BookingFeedbackResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> ResolveIssue(string id)
        {
            try
            {
                var adminUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(adminUserId))
                {
                    return Unauthorized(ApiResponse.Fail(401, "Không xác định được user"));
                }

                var result = await _feedbackService.ResolveIssueAsync(id, adminUserId);
                if (!result.Success)
                {
                    if (result.Error?.Code == 404)
                        return NotFound(result);
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
        /// Xóa feedback
        /// </summary>
        /// <param name="id">Feedback ID</param>
        /// <returns>Kết quả xóa</returns>
        /// <response code="200">Xóa thành công</response>
        /// <response code="403">Không có quyền xóa feedback này</response>
        /// <response code="404">Không tìm thấy feedback</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** User xóa feedback của mình
        /// 
        /// **Business Rules:**
        /// - Chỉ owner mới được xóa feedback
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 403)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(ApiResponse.Fail(401, "Không xác định được user"));
                }

                var result = await _feedbackService.DeleteAsync(id, userId);
                if (!result.Success)
                {
                    if (result.Error?.Code == 403)
                        return Forbid();
                    if (result.Error?.Code == 404)
                        return NotFound(result);
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

