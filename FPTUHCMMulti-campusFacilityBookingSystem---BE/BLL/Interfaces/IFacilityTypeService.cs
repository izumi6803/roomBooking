using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface IFacilityTypeService
    {
        Task<ApiResponseWithPagination<List<FacilityTypeResponseDto>>> GetAllAsync(PagedRequestDto request);
        Task<ApiResponse<FacilityTypeResponseDto>> GetByIdAsync(string id);
        Task<ApiResponse<FacilityTypeResponseDto>> CreateAsync(CreateFacilityTypeDto dto);
        Task<ApiResponse<FacilityTypeResponseDto>> UpdateAsync(string id, UpdateFacilityTypeDto dto);
        Task<ApiResponse> DeleteAsync(string id);
    }
}


