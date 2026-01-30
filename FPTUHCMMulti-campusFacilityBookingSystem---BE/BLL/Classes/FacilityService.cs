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
    public class FacilityService : IFacilityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacilityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseWithPagination<List<FacilityResponseDto>>> GetAllAsync(FacilityFilterDto filter)
        {
            var (items, total) = await _unitOfWork.FacilityRepo.GetFilteredAsync(
                filter.Name,
                filter.Status?.ToString(),
                filter.TypeId,
                filter.CampusId,
                filter.Page,
                filter.Limit
            );

            var responseDtos = _mapper.Map<List<FacilityResponseDto>>(items);

            return ApiResponseWithPagination<List<FacilityResponseDto>>.Ok(
                responseDtos,
                filter.Page,
                filter.Limit,
                total
            );
        }

        public async Task<ApiResponse<FacilityResponseDto>> GetByIdAsync(string id)
        {
            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(id);
            if (facility == null)
            {
                return ApiResponse<FacilityResponseDto>.Fail(404, "Không tìm thấy cơ sở vật chất.");
            }

            var responseDto = _mapper.Map<FacilityResponseDto>(facility);
            return ApiResponse<FacilityResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<List<FacilityAvailabilityDto>>> GetFacilitiesWithAvailabilityAsync(string campusId, DateTime from, DateTime to)
        {
            var facilities = await _unitOfWork.FacilityRepo.GetFacilitiesWithAvailabilityAsync(campusId, from, to);

            var responseDtos = facilities.Select(f => new FacilityAvailabilityDto
            {
                FacilityId = f.FacilityId,
                Name = f.Name,
                Description = f.Description,
                Capacity = f.Capacity,
                RoomNumber = f.RoomNumber,
                TypeName = f.FacilityType?.Name ?? string.Empty,
                Status = f.Status.ToString(),
                IsAvailable = !f.Bookings.Any(),
                BookedSlots = f.Bookings.Select(b => new BookingSlot
                {
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    Status = b.Status.ToString()
                }).ToList()
            }).ToList();

            return ApiResponse<List<FacilityAvailabilityDto>>.Ok(responseDtos);
        }

        public async Task<ApiResponse<FacilityResponseDto>> GetByIdWithDetailsAsync(string id)
        {
            var facility = await _unitOfWork.FacilityRepo.GetByIdWithDetailsAsync(id);
            if (facility == null)
            {
                return ApiResponse<FacilityResponseDto>.Fail(404, "Không tìm thấy cơ sở vật chất.");
            }

            var responseDto = _mapper.Map<FacilityResponseDto>(facility);
            return ApiResponse<FacilityResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<FacilityResponseDto>> GetFacilityDetailAsync(string id)
        {
            return await GetByIdWithDetailsAsync(id);
        }

        public async Task<ApiResponse<FacilityResponseDto>> CreateAsync(CreateFacilityDto dto)
        {
            var facilityId = await GenerateNewFacilityIdAsync();

            var facility = _mapper.Map<Facility>(dto);
            facility.FacilityId = facilityId;
            facility.CreatedAt = DateTimeHelper.VietnamNow;
            facility.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.FacilityRepo.CreateAsync(facility);
            await _unitOfWork.SaveChangesAsync();

            var responseDto = _mapper.Map<FacilityResponseDto>(facility);
            return ApiResponse<FacilityResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<FacilityResponseDto>> UpdateAsync(string id, UpdateFacilityDto dto)
        {
            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(id);
            if (facility == null)
            {
                return ApiResponse<FacilityResponseDto>.Fail(404, "Không tìm thấy cơ sở vật chất.");
            }

            if (!string.IsNullOrEmpty(dto.Name))
                facility.Name = dto.Name;
            if (dto.Description != null)
                facility.Description = dto.Description;
            if (dto.Capacity.HasValue)
                facility.Capacity = dto.Capacity.Value;
            if (dto.RoomNumber != null)
                facility.RoomNumber = dto.RoomNumber;
            if (dto.FloorNumber != null)
                facility.FloorNumber = dto.FloorNumber;
            if (dto.Status.HasValue)
                facility.Status = dto.Status.Value;
            if (dto.Amenities != null)
                facility.Amenities = dto.Amenities;
            if (dto.MaxConcurrentBookings.HasValue)
                facility.MaxConcurrentBookings = dto.MaxConcurrentBookings.Value;

            facility.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.FacilityRepo.UpdateAsync(facility);
            await _unitOfWork.SaveChangesAsync();

            var responseDto = _mapper.Map<FacilityResponseDto>(facility);
            return ApiResponse<FacilityResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(id);
            if (facility == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy cơ sở vật chất.");
            }

            facility.Status = FacilityStatus.Under_Maintenance;
            facility.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.FacilityRepo.UpdateAsync(facility);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse.Ok();
        }

        private async Task<string> GenerateNewFacilityIdAsync()
        {
            var facilities = await _unitOfWork.FacilityRepo.GetAllAsync();
            var maxId = 0;

            foreach (var facility in facilities)
            {
                if (facility.FacilityId.StartsWith("F") && facility.FacilityId.Length == 6)
                {
                    if (int.TryParse(facility.FacilityId.Substring(1), out var id))
                    {
                        if (id > maxId)
                            maxId = id;
                    }
                }
            }

            return $"F{(maxId + 1):D5}";
        }
    }
}
