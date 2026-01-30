using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface ICampusService
    {
        Task<ApiResponseWithPagination<List<CampusResponseDto>>> GetAllAsync(PagedRequestDto request);
        Task<ApiResponse<List<CampusResponseDto>>> GetAllCampusesAsync();
        Task<ApiResponse<CampusResponseDto>> GetByIdAsync(string id);
        Task<ApiResponse<CampusResponseDto>> CreateAsync(CreateCampusDto dto);
        Task<ApiResponse<CampusResponseDto>> UpdateAsync(string id, UpdateCampusDto dto);
        Task<ApiResponse> DeleteAsync(string id);
    }
}


