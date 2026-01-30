using Applications.DTOs.Request;
using Applications.DTOs.Response;

namespace BLL.Interfaces
{
    public interface IFacilityService
    {
        Task<ApiResponseWithPagination<List<FacilityResponseDto>>> GetAllAsync(FacilityFilterDto filter);
        Task<ApiResponse<List<FacilityAvailabilityDto>>> GetFacilitiesWithAvailabilityAsync(string campusId, DateTime from, DateTime to);
        Task<ApiResponse<FacilityResponseDto>> GetByIdAsync(string id);
        Task<ApiResponse<FacilityResponseDto>> GetFacilityDetailAsync(string id);
        Task<ApiResponse<FacilityResponseDto>> CreateAsync(CreateFacilityDto dto);
        Task<ApiResponse<FacilityResponseDto>> UpdateAsync(string id, UpdateFacilityDto dto);
        Task<ApiResponse> DeleteAsync(string id);
    }
}


