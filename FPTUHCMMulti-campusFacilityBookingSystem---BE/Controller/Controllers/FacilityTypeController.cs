using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý loại cơ sở vật chất (Facility Types)
    /// </summary>
    [ApiController]
    [Route("api/facility-types")]
    public class FacilityTypeController : ControllerBase
    {
        private readonly IFacilityTypeService _facilityTypeService;

        public FacilityTypeController(IFacilityTypeService facilityTypeService)
        {
            _facilityTypeService = facilityTypeService;
        }

        /// <summary>
        /// Lấy danh sách tất cả loại cơ sở vật chất
        /// </summary>
        /// <param name="request">Pagination parameters</param>
        /// <returns>Danh sách facility types</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **5 Loại cơ sở:**
        /// - FT0001: Classroom
        /// - FT0002: Meeting Room
        /// - FT0003: Computer Lab
        /// - FT0004: Sports Court
        /// - FT0005: Auditorium
        /// </remarks>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseWithPagination<List<FacilityTypeResponseDto>>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] PagedRequestDto request)
        {
            try
            {
                var result = await _facilityTypeService.GetAllAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy chi tiết loại cơ sở vật chất
        /// </summary>
        /// <param name="id">Facility Type ID</param>
        /// <returns>Thông tin chi tiết</returns>
        /// <response code="200">Trả về thông tin thành công</response>
        /// <response code="404">Không tìm thấy loại cơ sở</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **Mục đích:** Xem thông tin loại cơ sở (capacity mặc định, amenities, etc.)
        /// </remarks>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<FacilityTypeResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _facilityTypeService.GetByIdAsync(id);
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
        /// Tạo loại cơ sở vật chất mới
        /// </summary>
        /// <param name="dto">Thông tin loại cơ sở</param>
        /// <returns>Facility type đã tạo</returns>
        /// <response code="200">Tạo thành công</response>
        /// <response code="403">Không có quyền</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<FacilityTypeResponseDto>), 200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Create([FromBody] CreateFacilityTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ."));
            }

            try
            {
                var result = await _facilityTypeService.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin loại cơ sở vật chất
        /// </summary>
        /// <param name="id">Facility Type ID</param>
        /// <param name="dto">Thông tin cập nhật</param>
        /// <returns>Facility type đã cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="404">Không tìm thấy loại cơ sở</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<FacilityTypeResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateFacilityTypeDto dto)
        {
            try
            {
                var result = await _facilityTypeService.UpdateAsync(id, dto);
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
        /// Vô hiệu hóa loại cơ sở vật chất (soft delete)
        /// </summary>
        /// <param name="id">Facility Type ID</param>
        /// <returns>Kết quả vô hiệu hóa</returns>
        /// <response code="200">Vô hiệu hóa thành công</response>
        /// <response code="404">Không tìm thấy loại cơ sở</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Mục đích:** Vô hiệu hóa loại cơ sở vật chất (set status = Inactive)
        /// 
        /// **Lưu ý:** 
        /// - Khi vô hiệu hóa facility type, tất cả facilities đang sử dụng type này sẽ bị xóa (soft delete - set status = Under_Maintenance)
        /// - Facility type sẽ bị vô hiệu hóa (soft delete - set status = Inactive), không bị xóa vĩnh viễn
        /// - Chỉ hiển thị các facility type có status = Active trong danh sách
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _facilityTypeService.DeleteAsync(id);
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
