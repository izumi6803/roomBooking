using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý cấu hình hệ thống (System Settings)
    /// </summary>
    [ApiController]
    [Route("api/system-settings")]
    [Authorize]
    public class SystemSettingsController : ControllerBase
    {
        private readonly ISystemSettingsService _systemSettingsService;

        public SystemSettingsController(ISystemSettingsService systemSettingsService)
        {
            _systemSettingsService = systemSettingsService;
        }

        /// <summary>
        /// Lấy cấu hình hệ thống hiện tại
        /// </summary>
        /// <returns>Cấu hình hệ thống</returns>
        /// <response code="200">Trả về cấu hình thành công</response>
        /// <response code="401">Chưa đăng nhập</response>
        /// <remarks>
        /// **Roles:** Tất cả user đã đăng nhập
        /// 
        /// **Mục đích:** Xem các cấu hình thời gian hiện tại của hệ thống
        /// 
        /// **Các cấu hình:**
        /// - MinimumBookingHoursBeforeStart: Thời gian tối thiểu (giờ) trước khi booking bắt đầu (mặc định: 3)
        /// - CheckInMinutesBeforeStart: Số phút cho phép check-in trước StartTime (mặc định: 15)
        /// - CheckInMinutesAfterStart: Số phút cho phép check-in sau StartTime (mặc định: 15)
        /// - CheckoutMinMinutesAfterCheckIn: Số phút tối thiểu sau khi check-in để được phép check-out (mặc định: 0)
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<SystemSettingsResponseDto>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetSettings()
        {
            try
            {
                var result = await _systemSettingsService.GetSettingsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật cấu hình hệ thống
        /// </summary>
        /// <param name="dto">Thông tin cấu hình cần cập nhật</param>
        /// <returns>Cấu hình đã cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="400">Dữ liệu không hợp lệ</response>
        /// <response code="401">Chưa đăng nhập</response>
        /// <response code="403">Không có quyền (chỉ Facility_Admin)</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Cập nhật các cấu hình thời gian của hệ thống
        /// 
        /// **Các cấu hình có thể cập nhật:**
        /// - MinimumBookingHoursBeforeStart: Thời gian tối thiểu (giờ) trước khi booking bắt đầu (>= 0)
        /// - CheckInMinutesBeforeStart: Số phút cho phép check-in trước StartTime (>= 0)
        /// - CheckInMinutesAfterStart: Số phút cho phép check-in sau StartTime (>= 0)
        /// - CheckoutMinMinutesAfterCheckIn: Số phút tối thiểu sau khi check-in để được phép check-out (>= 0, mặc định: 0)
        /// 
        /// **Lưu ý:**
        /// - Chỉ cần gửi các field muốn cập nhật (các field khác giữ nguyên)
        /// - Nếu không set giá trị, hệ thống sẽ dùng giá trị mặc định
        /// - Các giá trị phải >= 0
        /// </remarks>
        [HttpPut]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<SystemSettingsResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateSettings([FromBody] UpdateSystemSettingsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ."));
            }

            try
            {
                var result = await _systemSettingsService.UpdateSettingsAsync(dto);
                if (!result.Success)
                {
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

