using Applications.DTOs.Request;
using Applications.DTOs.Response;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    /// <summary>
    /// API quản lý vai trò (Roles)
    /// </summary>
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Lấy danh sách tất cả roles
        /// </summary>
        /// <param name="request">Pagination parameters</param>
        /// <returns>Danh sách roles (3 roles cố định)</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// 
        /// **3 Roles hệ thống:**
        /// - RL0001: Student
        /// - RL0002: Lecturer
        /// - RL0003: Facility_Admin
        /// </remarks>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseWithPagination<List<RoleResponseDto>>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] PagedRequestDto request)
        {
            try
            {
                var result = await _roleService.GetAllAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Lấy chi tiết role
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <returns>Thông tin role</returns>
        /// <response code="200">Trả về thông tin thành công</response>
        /// <response code="404">Không tìm thấy role</response>
        /// <remarks>
        /// **Roles:** Công khai - Không cần đăng nhập
        /// </remarks>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _roleService.GetByIdAsync(id);
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
        /// Tạo role mới
        /// </summary>
        /// <param name="dto">Thông tin role</param>
        /// <returns>Role đã tạo</returns>
        /// <response code="200">Tạo thành công</response>
        /// <response code="403">Không có quyền</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Lưu ý:** Hệ thống chỉ có 3 roles cố định, không nên tạo thêm
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), 200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Create([FromBody] CreateRoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(400, "Dữ liệu không hợp lệ."));
            }

            try
            {
                var result = await _roleService.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail(500, ex.Message));
            }
        }

        /// <summary>
        /// Cập nhật thông tin role
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <param name="dto">Thông tin cập nhật</param>
        /// <returns>Role đã cập nhật</returns>
        /// <response code="200">Cập nhật thành công</response>
        /// <response code="404">Không tìm thấy role</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateRoleDto dto)
        {
            try
            {
                var result = await _roleService.UpdateAsync(id, dto);
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
        /// Xóa role (không được phép)
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <returns>Error 403</returns>
        /// <response code="403">Không thể xóa roles hệ thống</response>
        /// <remarks>
        /// **Roles:** Chỉ Facility_Admin (RL0003)
        /// 
        /// **Lưu ý:** Roles hệ thống không thể xóa
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "RL0003")]
        [ProducesResponseType(typeof(ApiResponse), 403)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _roleService.DeleteAsync(id);
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
