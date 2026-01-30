using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResponseWithPagination<List<RoleResponseDto>>> GetAllAsync(PagedRequestDto request);
        Task<ApiResponse<RoleResponseDto>> GetByIdAsync(string id);
        Task<ApiResponse<RoleResponseDto>> CreateAsync(CreateRoleDto dto);
        Task<ApiResponse<RoleResponseDto>> UpdateAsync(string id, UpdateRoleDto dto);
        Task<ApiResponse> DeleteAsync(string id);
    }
}


