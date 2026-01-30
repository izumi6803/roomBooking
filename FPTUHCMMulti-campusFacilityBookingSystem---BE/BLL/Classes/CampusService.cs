using Applications.DTOs.Request;
using Applications.DTOs.Response;
using Applications.Helpers;
using AutoMapper;
using BLL.Interfaces;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories;

namespace BLL.Classes
{
    public class CampusService : ICampusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CampusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseWithPagination<List<CampusResponseDto>>> GetAllAsync(PagedRequestDto request)
        {
            var (items, total) = await _unitOfWork.CampusRepo.GetPagedAsync(request.Page, request.Limit);

            var responseDtos = _mapper.Map<List<CampusResponseDto>>(items);

            return ApiResponseWithPagination<List<CampusResponseDto>>.Ok(
                responseDtos,
                request.Page,
                request.Limit,
                total
            );
        }

        public async Task<ApiResponse<List<CampusResponseDto>>> GetAllCampusesAsync()
        {
            var campuses = await _unitOfWork.CampusRepo.GetAllCampusesAsync();
            var responseDtos = _mapper.Map<List<CampusResponseDto>>(campuses);
            return ApiResponse<List<CampusResponseDto>>.Ok(responseDtos);
        }

        public async Task<ApiResponse<CampusResponseDto>> GetByIdAsync(string id)
        {
            var campus = await _unitOfWork.CampusRepo.GetByIdAsync(id);
            if (campus == null)
            {
                return ApiResponse<CampusResponseDto>.Fail(404, "Không tìm thấy cơ sở.");
            }

            var responseDto = _mapper.Map<CampusResponseDto>(campus);
            return ApiResponse<CampusResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<CampusResponseDto>> CreateAsync(CreateCampusDto dto)
        {
            var campusId = await GenerateCampusIdAsync();

            var campus = _mapper.Map<Campus>(dto);
            campus.CampusId = campusId;
            campus.CreatedAt = DateTimeHelper.VietnamNow;
            campus.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.CampusRepo.CreateAsync(campus);

            var responseDto = _mapper.Map<CampusResponseDto>(campus);
            return ApiResponse<CampusResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<CampusResponseDto>> UpdateAsync(string id, UpdateCampusDto dto)
        {
            var campus = await _unitOfWork.CampusRepo.GetByIdAsync(id);
            if (campus == null)
            {
                return ApiResponse<CampusResponseDto>.Fail(404, "Không tìm thấy cơ sở.");
            }

            if (!string.IsNullOrEmpty(dto.Name))
                campus.Name = dto.Name;
            if (dto.ImageUrl != null)
                campus.ImageUrl = dto.ImageUrl;
            if (dto.Address != null)
                campus.Address = dto.Address;
            if (dto.PhoneNumber != null)
                campus.PhoneNumber = dto.PhoneNumber;
            if (dto.Email != null)
                campus.Email = dto.Email;
            if (dto.Status.HasValue)
                campus.Status = dto.Status.Value;

            campus.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.CampusRepo.UpdateAsync(campus);

            var responseDto = _mapper.Map<CampusResponseDto>(campus);
            return ApiResponse<CampusResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            var campus = await _unitOfWork.CampusRepo.GetByIdAsync(id);
            if (campus == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy cơ sở.");
            }

            // Soft delete
            campus.Status = CampusStatus.Inactive;
            campus.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.CampusRepo.UpdateAsync(campus);

            return ApiResponse.Ok();
        }

        private async Task<string> GenerateCampusIdAsync()
        {
            var campuses = await _unitOfWork.CampusRepo.GetAllAsync();
            var maxId = 0;

            foreach (var campus in campuses)
            {
                if (campus.CampusId.StartsWith("C") && campus.CampusId.Length == 6)
                {
                    if (int.TryParse(campus.CampusId.Substring(1), out var id))
                    {
                        if (id > maxId)
                            maxId = id;
                    }
                }
            }

            return $"C{(maxId + 1):D5}";
        }
    }
}
