using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using DAL.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý cơ sở vật chất (Facilities)
    /// </summary>
    [ApiController]
    [Route("api/facilities")]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        /// <summary>
        /// Lấy danh sách facilities với filtering và pagination
        /// </summary>
        /// <param name="filter">Bộ lọc (name, status, typeId, campusId, page, limit)</param>
        /// <returns>Danh sách facilities</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Tìm kiếm và lọc facilities
        /// 
        /// **Filters:**
        /// - name: Tìm theo tên (contains)
        /// - status: Available | Under_Maintenance
        /// - typeId: Lọc theo loại (FT0001, FT0002, ...)
        /// - campusId: Lọc theo campus (C0001, C0002)
        /// - page: Trang (default: 1)
        /// - limit: Số items/trang (default: 10)
        /// </remarks>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseWithPagination<List<FacilityResponseDto>>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] FacilityFilterDto filter)
        {
            try
            {
                var result = await _facilityService.GetAllAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy chi tiết facility (type, capacity, equipment, schedule, campus info)
        /// </summary>
        /// <param name="id">Facility ID</param>
        /// <returns>Thông tin chi tiết facility</returns>
        /// <response code="200">Trả về thông tin thành công</response>
        /// <response code="404">Không tìm thấy facility</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Xem chi tiết cơ sở vật chất trước khi đặt
        /// 
        /// **Thông tin trả về:**
        /// - Thông tin cơ bản (tên, capacity, phòng, tầng)
        /// - Loại facility và campus
        /// - Trang thiết bị (amenities)
        /// - Lịch booking sắp tới
        /// - Hình ảnh
        /// </remarks>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<FacilityResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _facilityService.GetFacilityDetailAsync(id);
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
        /// Tạo cơ sở vật chất mới
        /// </summary>
        /// <param name="dto">Thông tin facility</param>
        /// <returns>Facility đã tạo</returns>
        /// <response code="200">Tạo thành công</response>
        /// <response code="400">Dữ liệu không hợp lệ</response>
        /// <response code="403">Không có quyền</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Tạo cơ sở vật chất mới trong hệ thống
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<FacilityResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Create([FromBody] CreateFacilityDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ."));
            }

            try
            {
                var result = await _facilityService.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin facility
        /// </summary>
        /// <param name="id">Facility ID</param>
        /// <param name="dto">Thông tin cập nhật</param>
        /// <param name="status">Trạng thái facility (dropdown)</param>
        /// <returns>Facility đã cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="404">Không tìm thấy facility</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Cập nhật thông tin hoặc thay đổi trạng thái facility
        /// 
        /// **Status Options:**
        /// - Available: Facility sẵn sàng để đặt
        /// - Under_Maintenance: Facility đang bảo trì, không cho đặt
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<FacilityResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateFacilityDto dto, [FromQuery] FacilityStatus? status)
        {
            try
            {
                if (status.HasValue)
                    dto.Status = status.Value;
                    
                var result = await _facilityService.UpdateAsync(id, dto);
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
        /// Vô hiệu hóa facility (soft delete - set status = Under_Maintenance)
        /// </summary>
        /// <param name="id">Facility ID</param>
        /// <returns>Kết quả xóa</returns>
        /// <response code="200">Vô hiệu hóa thành công</response>
        /// <response code="404">Không tìm thấy facility</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Đánh dấu facility không khả dụng (soft delete)
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _facilityService.DeleteAsync(id);
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
    }
}
