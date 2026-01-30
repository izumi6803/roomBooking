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
    public class FacilityTypeService : IFacilityTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacilityTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseWithPagination<List<FacilityTypeResponseDto>>> GetAllAsync(PagedRequestDto request)
        {
            var (items, total) = await _unitOfWork.FacilityTypeRepo.GetPagedAsync(request.Page, request.Limit);
            var responseDtos = _mapper.Map<List<FacilityTypeResponseDto>>(items);

            return ApiResponseWithPagination<List<FacilityTypeResponseDto>>.Ok(
                responseDtos,
                request.Page,
                request.Limit,
                total
            );
        }

        public async Task<ApiResponse<FacilityTypeResponseDto>> GetByIdAsync(string id)
        {
            var facilityType = await _unitOfWork.FacilityTypeRepo.GetByIdAsync(id);
            if (facilityType == null)
            {
                return ApiResponse<FacilityTypeResponseDto>.Fail(404, "Không tìm thấy loại cơ sở vật chất.");
            }

            var responseDto = _mapper.Map<FacilityTypeResponseDto>(facilityType);
            return ApiResponse<FacilityTypeResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<FacilityTypeResponseDto>> CreateAsync(CreateFacilityTypeDto dto)
        {
            var typeId = await GenerateTypeIdAsync();

            var facilityType = _mapper.Map<FacilityType>(dto);
            facilityType.TypeId = typeId;
            facilityType.Status = FacilityTypeStatus.Active;
            facilityType.CreatedAt = DateTimeHelper.VietnamNow;
            facilityType.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.FacilityTypeRepo.CreateAsync(facilityType);

            var responseDto = _mapper.Map<FacilityTypeResponseDto>(facilityType);
            return ApiResponse<FacilityTypeResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<FacilityTypeResponseDto>> UpdateAsync(string id, UpdateFacilityTypeDto dto)
        {
            var facilityType = await _unitOfWork.FacilityTypeRepo.GetByIdAsync(id);
            if (facilityType == null)
            {
                return ApiResponse<FacilityTypeResponseDto>.Fail(404, "Không tìm thấy loại cơ sở vật chất.");
            }

            if (!string.IsNullOrEmpty(dto.Name))
                facilityType.Name = dto.Name;
            if (dto.Description != null)
                facilityType.Description = dto.Description;
            if (dto.DefaultAmenities != null)
                facilityType.DefaultAmenities = dto.DefaultAmenities;
            if (dto.DefaultCapacity.HasValue)
                facilityType.DefaultCapacity = dto.DefaultCapacity;
            if (dto.TypicalDurationHours.HasValue)
                facilityType.TypicalDurationHours = dto.TypicalDurationHours;
            if (dto.IconUrl != null)
                facilityType.IconUrl = dto.IconUrl;

            facilityType.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.FacilityTypeRepo.UpdateAsync(facilityType);

            var responseDto = _mapper.Map<FacilityTypeResponseDto>(facilityType);
            return ApiResponse<FacilityTypeResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            var facilityType = await _unitOfWork.FacilityTypeRepo.GetByIdAsync(id);
            if (facilityType == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy loại cơ sở vật chất.");
            }

            // Lấy danh sách facilities đang sử dụng type này và xóa 
            var facilities = await _unitOfWork.FacilityRepo.GetAllAsync();
            var facilitiesUsingType = facilities.Where(f => f.TypeId == id).ToList();
            
            if (facilitiesUsingType.Any())
            {
                // Xóa tất cả facilities đang sử dụng type này 
                foreach (var facility in facilitiesUsingType)
                {
                    facility.Status = FacilityStatus.Under_Maintenance;
                    facility.UpdatedAt = DateTimeHelper.VietnamNow;
                    await _unitOfWork.FacilityRepo.UpdateAsync(facility);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            // Soft delete: set status = Inactive
            facilityType.Status = FacilityTypeStatus.Inactive;
            facilityType.UpdatedAt = DateTimeHelper.VietnamNow;
            await _unitOfWork.FacilityTypeRepo.UpdateAsync(facilityType);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse.Ok();
        }

        private async Task<string> GenerateTypeIdAsync()
        {
            var types = await _unitOfWork.FacilityTypeRepo.GetAllAsync();
            var maxId = 0;

            foreach (var type in types)
            {
                if (type.TypeId.StartsWith("FT") && type.TypeId.Length == 6)
                {
                    if (int.TryParse(type.TypeId.Substring(2), out var id))
                    {
                        if (id > maxId)
                            maxId = id;
                    }
                }
            }

            return $"FT{(maxId + 1):D4}";
        }
    }
}
