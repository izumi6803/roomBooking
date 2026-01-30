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
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly ISystemSettingsService _systemSettingsService;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService, ISystemSettingsService systemSettingsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
            _systemSettingsService = systemSettingsService;
        }

        public async Task<ApiResponseWithPagination<List<BookingResponseDto>>> GetAllAsync(BookingFilterDto filter)
        {
            var (items, total) = await _unitOfWork.BookingRepo.GetPagedAsync(
                filter.UserId,
                filter.FacilityId,
                filter.Status?.ToString(),
                filter.Page,
                filter.Limit
            );

            var responseDtos = _mapper.Map<List<BookingResponseDto>>(items);

            return ApiResponseWithPagination<List<BookingResponseDto>>.Ok(
                responseDtos,
                filter.Page,
                filter.Limit,
                total
            );
        }

        public async Task<ApiResponse<BookingResponseDto>> GetByIdAsync(string id)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(id);
            if (booking == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy lượt đặt.");
            }

            var responseDto = _mapper.Map<BookingResponseDto>(booking);
            return ApiResponse<BookingResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<BookingResponseDto>> CreateAsync(CreateBookingDto dto)
        {
            // validate user tồn tại và kiểm tra thông tin bắt buộc
            // lấy thông tin user từ database để kiểm tra các thông tin cần thiết trước khi cho phép đặt phòng
            var user = await _unitOfWork.UserRepo.GetByIdAsync(dto.UserId);
            if (user == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy user.");
            }

            // kiểm tra email bắt buộc
            // email là thông tin quan trọng để liên hệ với user, bắt buộc phải có trước khi đặt phòng
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return ApiResponse<BookingResponseDto>.Fail(400, "Bạn phải cập nhật email trong hồ sơ cá nhân trước khi đặt phòng.");
            }

            // kiểm tra số điện thoại bắt buộc
            // số điện thoại cần thiết để admin liên hệ khi cần, bắt buộc phải có
            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                return ApiResponse<BookingResponseDto>.Fail(400, "Bạn phải cập nhật số điện thoại trong hồ sơ cá nhân trước khi đặt phòng. Vui lòng vào Hồ sơ để cập nhật.");
            }

            // nếu user có role là student thì phải có mssv
            // sinh viên bắt buộc phải có mssv để admin xác minh danh tính khi duyệt booking
            // mssv sẽ được hiển thị trong response để admin xem (không cần duyệt, chỉ xem)
            if (user.Role != null && user.Role.RoleName.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(user.StudentId))
                {
                    return ApiResponse<BookingResponseDto>.Fail(400, "Sinh viên phải cập nhật MSSV trong hồ sơ cá nhân trước khi đặt phòng. Vui lòng vào Hồ sơ để cập nhật.");
                }
            }

            // validate thời gian booking hợp lý
            // kiểm tra các ràng buộc về thời gian: duration (1-3 giờ), thời gian tối thiểu trước khi bắt đầu (lấy từ system settings)
            // hàm validatebookingtimeasync sẽ lấy giá trị minimumbookinghoursbeforestart từ system settings
            // nếu chưa có setting thì dùng giá trị mặc định là 3 giờ
            var timeValidation = await ValidateBookingTimeAsync(dto.StartTime, dto.EndTime);
            if (!timeValidation.IsValid)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, timeValidation.ErrorMessage);
            }

            // validate facility tồn tại và có sẵn
            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(dto.FacilityId);
            if (facility == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy facility");
            }

            // check trạng thái facility
            if (facility.Status != FacilityStatus.Available)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, $"Facility đang ở trạng thái {facility.Status}. Vui lòng chọn facility khác hoặc đợi facility sẵn sàng.");
            }

            // check conflict 
            var hasConflict = await _unitOfWork.BookingRepo.HasConflictAsync(
                dto.FacilityId,
                dto.StartTime,
                dto.EndTime
            );

            if (hasConflict)
            {
                return ApiResponse<BookingResponseDto>.Fail(409, "Facility đã được đặt trong khoảng thời gian này. Vui lòng chọn thời gian khác hoặc facility khác.");
            }

            // tạo bookingid tự động và map dữ liệu
            // generatebookingidasync tạo id theo format b00001, b00002, ...
            var bookingId = await GenerateBookingIdAsync();
            
            // map từ dto sang entity bằng automapper
            var booking = _mapper.Map<Booking>(dto);
            booking.BookingId = bookingId;
            // booking mặc định là pending_approval (đã bỏ draft status)
            // khi tạo xong booking sẽ tự động ở trạng thái chờ admin duyệt
            booking.Status = BookingStatus.Pending_Approval;
            booking.CreatedAt = DateTimeHelper.VietnamNow;
            booking.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.BookingRepo.CreateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            // tạo thông báo cho admin
            await _notificationService.CreateBookingPendingApprovalNotificationAsync(bookingId);

            var responseDto = _mapper.Map<BookingResponseDto>(booking);
            return ApiResponse<BookingResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<BookingResponseDto>> UpdateAsync(string id, UpdateBookingDto dto)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(id);
            if (booking == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy lượt đặt.");
            }

            // validate facility tồn tại và có sẵn
            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
            if (facility == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy facility.");
            }

            // check trạng thái của facility
            if (facility.Status != FacilityStatus.Available)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, $"Facility đang ở trạng thái {facility.Status}. Không thể cập nhật booking cho facility đang bảo trì.");
            }

            // check nếu giờ bắt đầu hoặc giờ kết thúc thay đổi
            bool timeChanged = (dto.StartTime.HasValue && dto.StartTime.Value != booking.StartTime) ||
                              (dto.EndTime.HasValue && dto.EndTime.Value != booking.EndTime);

            // nếu giờ bắt đầu hoặc giờ kết thúc thay đổi, check conflict
            if (timeChanged)
            {
                var newStartTime = dto.StartTime ?? booking.StartTime;
                var newEndTime = dto.EndTime ?? booking.EndTime;

                // validate thời gian 
                var timeValidation = await ValidateBookingTimeAsync(newStartTime, newEndTime);
                if (!timeValidation.IsValid)
                {
                    return ApiResponse<BookingResponseDto>.Fail(400, timeValidation.ErrorMessage);
                }

                // check conflict 
                var hasConflict = await _unitOfWork.BookingRepo.HasConflictAsync(
                    booking.FacilityId,
                    newStartTime,
                    newEndTime,
                    id 
                );

                if (hasConflict)
                {
                    return ApiResponse<BookingResponseDto>.Fail(409, "Facility đã được đặt trong khoảng thời gian này. Vui lòng chọn thời gian khác hoặc facility khác.");
                }
            }

            // update booking fields
            if (dto.StartTime.HasValue)
                booking.StartTime = dto.StartTime.Value;
            if (dto.EndTime.HasValue)
                booking.EndTime = dto.EndTime.Value;
            if (dto.Purpose != null)
                booking.Purpose = dto.Purpose;
            if (dto.Category != null)
                booking.Category = dto.Category;
            if (dto.EstimatedAttendees.HasValue)
                booking.EstimatedAttendees = dto.EstimatedAttendees;
            if (dto.SpecialRequirements != null)
                booking.SpecialRequirements = dto.SpecialRequirements;

            booking.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.BookingRepo.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            var responseDto = _mapper.Map<BookingResponseDto>(booking);
            return ApiResponse<BookingResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(id);
            if (booking == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy lượt đặt.");
            }

            booking.Status = BookingStatus.Cancelled;
            booking.CancelledAt = DateTimeHelper.VietnamNow;
            booking.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.BookingRepo.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse.Ok();
        }

        public async Task<ApiResponse<BookingResponseDto>> ApproveBookingAsync(string bookingId, string approverId)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy booking.");
            }

            if (booking.Status != BookingStatus.Pending_Approval)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, "Chỉ có thể duyệt booking ở trạng thái Pending_Approval.");
            }

            // check conflict
            var hasConflict = await _unitOfWork.BookingRepo.HasConflictAsync(
                booking.FacilityId,
                booking.StartTime,
                booking.EndTime,
                bookingId
            );

            if (hasConflict)
            {
                return ApiResponse<BookingResponseDto>.Fail(409, "Facility đã được đặt trong khoảng thời gian này.");
            }

            booking.Status = BookingStatus.Approved;
            booking.ApprovedBy = approverId;
            booking.ApprovedAt = DateTimeHelper.VietnamNow;
            booking.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.BookingRepo.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            // tạo thông báo cho user
            await _notificationService.CreateBookingApprovedNotificationAsync(bookingId);

            var responseDto = _mapper.Map<BookingResponseDto>(booking);
            return ApiResponse<BookingResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<BookingResponseDto>> RejectBookingAsync(string bookingId, string approverId, string? reason)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy booking.");
            }

            if (booking.Status != BookingStatus.Pending_Approval)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, "Chỉ có thể từ chối booking ở trạng thái Pending_Approval.");
            }

            booking.Status = BookingStatus.Rejected;
            booking.ApprovedBy = approverId;
            booking.ApprovedAt = DateTimeHelper.VietnamNow;
            booking.RejectionReason = reason;
            booking.UpdatedAt = DateTimeHelper.VietnamNow;

            await _unitOfWork.BookingRepo.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            // tạo thông báo cho user
            await _notificationService.CreateBookingRejectedNotificationAsync(bookingId, reason);

            var responseDto = _mapper.Map<BookingResponseDto>(booking);
            return ApiResponse<BookingResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<BookingResponseDto>> SubmitBookingAsync(string bookingId)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy booking.");
            }

            // booking đã tự động là Pending_Approval khi tạo, nên chỉ cần check thông báo đã được gửi
            if (booking.Status != BookingStatus.Pending_Approval)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, $"Booking không ở trạng thái Pending_Approval. Trạng thái hiện tại: {booking.Status}.");
            }



            var responseDto = _mapper.Map<BookingResponseDto>(booking);
            return ApiResponse<BookingResponseDto>.Ok(responseDto);
        }


        public async Task<ApiResponse> CancelAsync(string bookingId, string userId, string? reason = null)
        {
            // tìm booking cần hủy
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy booking.");
            }

            // kiểm tra quyền - chỉ chủ booking mới có thể hủy
            if (booking.UserId != userId)
            {
                return ApiResponse.Fail(403, "Bạn không có quyền hủy booking này.");
            }

            // kiểm tra status - chỉ có thể hủy booking đang chờ duyệt hoặc đã được duyệt
            // không thể hủy booking đã bị từ chối, đã hủy, hoặc đã hoàn tất
            if (booking.Status != BookingStatus.Pending_Approval && booking.Status != BookingStatus.Approved)
            {
                return ApiResponse.Fail(400, "Chỉ có thể hủy booking ở trạng thái Pending_Approval hoặc Approved.");
            }

            // kiểm tra ràng buộc thời gian - chỉ cho phép hủy trước 2 giờ từ starttime
            // ví dụ: booking bắt đầu lúc 10h thì phải hủy trước 8h
            // mục đích: tránh hủy quá gần giờ bắt đầu, gây lãng phí tài nguyên
            var now = DateTimeHelper.VietnamNow;
            var minCancelTime = booking.StartTime.AddHours(-2);
            if (now > minCancelTime)
            {
                return ApiResponse.Fail(400, $"Chỉ có thể hủy booking trước 2 giờ từ thời gian bắt đầu. Thời gian hủy tối đa: {minCancelTime:dd/MM/yyyy HH:mm:ss}.");
            }

            // cập nhật booking sang trạng thái cancelled
            booking.Status = BookingStatus.Cancelled;
            booking.CancelledAt = now;
            booking.CancellationReason = string.IsNullOrEmpty(reason) ? "Hủy bởi người dùng" : reason;
            booking.UpdatedAt = now;

            await _unitOfWork.BookingRepo.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            // tạo thông báo cho tất cả admin khi user hủy booking
            // admin sẽ nhận notification để biết có slot trống và có thể xử lý
            // notification được gửi cho tất cả admin (roleid = rl0003) có status active
            await _notificationService.CreateBookingCancelledByUserNotificationAsync(bookingId);

            return ApiResponse.Ok();
        }

        private async Task<string> GenerateBookingIdAsync()
        {
            var bookings = await _unitOfWork.BookingRepo.GetAllAsync();
            var maxId = 0;

            foreach (var booking in bookings)
            {
                if (booking.BookingId.StartsWith("B") && booking.BookingId.Length == 6)
                {
                    if (int.TryParse(booking.BookingId.Substring(1), out var id))
                    {
                        if (id > maxId)
                            maxId = id;
                    }
                }
            }

            return $"B{(maxId + 1):D5}";
        }

        public async Task<ApiResponse<AvailabilityCheckResponseDto>> CheckAvailabilityAsync(CheckAvailabilityDto dto)
        {
            var facility = await _unitOfWork.FacilityRepo.GetByIdWithDetailsAsync(dto.FacilityId);
            if (facility == null)
            {
                return ApiResponse<AvailabilityCheckResponseDto>.Fail(404, "Không tìm thấy facility");
            }

            var response = new AvailabilityCheckResponseDto
            {
                FacilityStatus = facility.Status.ToString()
            };

            // check trạng thái facility
            if (facility.Status != FacilityStatus.Available)
            {
                response.IsAvailable = false;
                response.ConflictReason = $"Facility đang ở trạng thái {facility.Status}";
                
                // Get alternative facilities
                var alternatives = await GetAlternativeFacilitiesAsync(
                    dto.FacilityId,
                    dto.StartTime,
                    dto.EndTime,
                    facility.Capacity
                );
                response.AlternativeFacilities = alternatives.Data ?? new List<AlternativeFacilityDto>();
                
                return ApiResponse<AvailabilityCheckResponseDto>.Ok(response);
            }

            // check conflict
            var hasConflict = await _unitOfWork.BookingRepo.HasConflictAsync(
                dto.FacilityId,
                dto.StartTime,
                dto.EndTime,
                dto.ExcludeBookingId
            );

            if (hasConflict)
            {
                response.IsAvailable = false;
                response.ConflictReason = "Facility đã được đặt trong khoảng thời gian này";
                
                // Get alternative facilities
                var alternatives = await GetAlternativeFacilitiesAsync(
                    dto.FacilityId,
                    dto.StartTime,
                    dto.EndTime,
                    facility.Capacity
                );
                response.AlternativeFacilities = alternatives.Data ?? new List<AlternativeFacilityDto>();
                
                return ApiResponse<AvailabilityCheckResponseDto>.Ok(response);
            }

            response.IsAvailable = true;
            return ApiResponse<AvailabilityCheckResponseDto>.Ok(response);
        }

        public async Task<ApiResponse<List<AlternativeFacilityDto>>> GetAlternativeFacilitiesAsync(
            string facilityId,
            DateTime startTime,
            DateTime endTime,
            int capacity)
        {
            var originalFacility = await _unitOfWork.FacilityRepo.GetByIdWithDetailsAsync(facilityId);
            if (originalFacility == null)
            {
                return ApiResponse<List<AlternativeFacilityDto>>.Fail(404, "Không tìm thấy facility");
            }

            // lấy facility trong cùng campus với capacity tương đương
            var facilities = await _unitOfWork.FacilityRepo.GetFilteredAsync(
                null,
                FacilityStatus.Available.ToString(),
                originalFacility.TypeId,
                originalFacility.CampusId,
                1,
                20
            );

            var alternatives = new List<AlternativeFacilityDto>();

            foreach (var facility in facilities.items.Where(f => f.FacilityId != facilityId && f.Capacity >= capacity))
            {
                var hasConflict = await _unitOfWork.BookingRepo.HasConflictAsync(
                    facility.FacilityId,
                    startTime,
                    endTime
                );

                if (!hasConflict)
                {
                    alternatives.Add(new AlternativeFacilityDto
                    {
                        FacilityId = facility.FacilityId,
                        Name = facility.Name,
                        CampusName = facility.Campus.Name,
                        Capacity = facility.Capacity,
                        RoomNumber = facility.RoomNumber,
                        FloorNumber = facility.FloorNumber,
                        IsAvailable = true,
                        NextAvailableTime = null
                    });
                }
                else
                {
                    // tìm thời gian available tiếp theo
                    var nextAvailable = await FindNextAvailableTimeAsync(
                        facility.FacilityId,
                        startTime,
                        endTime
                    );

                    alternatives.Add(new AlternativeFacilityDto
                    {
                        FacilityId = facility.FacilityId,
                        Name = facility.Name,
                        CampusName = facility.Campus.Name,
                        Capacity = facility.Capacity,
                        RoomNumber = facility.RoomNumber,
                        FloorNumber = facility.FloorNumber,
                        IsAvailable = false,
                        NextAvailableTime = nextAvailable
                    });
                }
            }

            return ApiResponse<List<AlternativeFacilityDto>>.Ok(alternatives.OrderBy(a => a.IsAvailable ? 0 : 1).ToList());
        }

        private async Task<DateTime?> FindNextAvailableTimeAsync(string facilityId, DateTime startTime, DateTime endTime)
        {
            var duration = endTime - startTime;
            var checkTime = endTime;

            // Check next 7 days
            for (int i = 0; i < 7; i++)
            {
                var nextStart = checkTime.AddDays(i);
                var nextEnd = nextStart.Add(duration);

                var hasConflict = await _unitOfWork.BookingRepo.HasConflictAsync(
                    facilityId,
                    nextStart,
                    nextEnd
                );

                if (!hasConflict)
                {
                    return nextStart;
                }
            }

            return null;
        }

        public async Task<ApiResponse<BookingResponseDto>> CheckInAsync(string bookingId, string userId, CheckInDto? dto = null)
        {
            // tìm booking cần check-in
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy booking.");
            }

            // validate booking thuộc về user - chỉ chủ booking mới có thể check-in
            if (booking.UserId != userId)
            {
                return ApiResponse<BookingResponseDto>.Fail(403, "Bạn không có quyền check-in booking này.");
            }

            // validate status phải là approved - chỉ booking đã được duyệt mới có thể check-in
            if (booking.Status != BookingStatus.Approved)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, $"Chỉ có thể check-in booking ở trạng thái Approved. Trạng thái hiện tại: {booking.Status}.");
            }

            // validate chưa check-in - mỗi booking chỉ check-in một lần
            if (booking.CheckInTime.HasValue)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, "Booking đã được check-in trước đó.");
            }

            // validate thời gian check-in (lấy từ system settings)
            // ràng buộc: chỉ cho phép check-in trong khoảng thời gian từ x phút trước starttime đến x phút sau starttime
            // ví dụ: booking 9h, before=15, after=15 → cho phép check-in từ 8h45 đến 9h15
            // mặc định: 15 phút trước và 15 phút sau (có thể thay đổi bởi admin qua system settings)
            var now = DateTimeHelper.VietnamNow;
            var checkInMinutesBefore = await _systemSettingsService.GetCheckInMinutesBeforeStartAsync();
            var checkInMinutesAfter = await _systemSettingsService.GetCheckInMinutesAfterStartAsync();
            
            // tính khoảng thời gian cho phép check-in
            var allowedCheckInStart = booking.StartTime.AddMinutes(-checkInMinutesBefore);
            var allowedCheckInEnd = booking.StartTime.AddMinutes(checkInMinutesAfter);
            
            // kiểm tra nếu check-in quá sớm
            if (now < allowedCheckInStart)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, $"Chỉ có thể check-in từ {checkInMinutesBefore} phút trước thời gian bắt đầu ({allowedCheckInStart:dd/MM/yyyy HH:mm:ss}).");
            }

            // kiểm tra nếu check-in quá muộn (sau thời gian cho phép)
            // nếu quá muộn, booking sẽ bị hủy tự động bởi background service
            if (now > allowedCheckInEnd)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, $"Đã quá thời gian check-in. Thời gian check-in cho phép: từ {allowedCheckInStart:dd/MM/yyyy HH:mm:ss} đến {allowedCheckInEnd:dd/MM/yyyy HH:mm:ss}.");
            }

            // set check-in time, note, and images
            booking.CheckInTime = now; // ghi nhận thời gian check-in (vietnam time)
            booking.UpdatedAt = now;
            
            // lưu ghi chú và ảnh nếu có
            if (dto != null)
            {
                booking.CheckInNote = dto.Note; // ghi chú khi check-in (ví dụ: số lượng bàn, ghế)
                // convert image urls list to json string để lưu vào database
                // frontend cần upload ảnh trước (qua cloudinary) và gửi urls trong request
                if (dto.ImageUrls != null && dto.ImageUrls.Any())
                {
                    booking.CheckInImages = System.Text.Json.JsonSerializer.Serialize(dto.ImageUrls);
                }
            }

            await _unitOfWork.BookingRepo.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            var responseDto = _mapper.Map<BookingResponseDto>(booking);
            return ApiResponse<BookingResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<BookingResponseDto>> CheckOutAsync(string bookingId, string userId, CheckOutDto? dto = null)
        {
            // tìm booking cần check-out
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return ApiResponse<BookingResponseDto>.Fail(404, "Không tìm thấy booking.");
            }

            // validate booking thuộc về user - chỉ chủ booking mới có thể check-out
            if (booking.UserId != userId)
            {
                return ApiResponse<BookingResponseDto>.Fail(403, "Bạn không có quyền check-out booking này.");
            }

            // validate đã check-in - phải check-in trước khi check-out
            if (!booking.CheckInTime.HasValue)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, "Phải check-in trước khi check-out.");
            }

            // validate chưa check-out - mỗi booking chỉ check-out một lần
            if (booking.CheckOutTime.HasValue)
            {
                return ApiResponse<BookingResponseDto>.Fail(400, "Booking đã được check-out trước đó.");
            }

            var now = DateTimeHelper.VietnamNow;

            // validate thời gian check-out - chỉ cho phép sau khi đã qua x phút từ lúc check-in
            // ràng buộc: user phải đợi ít nhất x phút sau khi check-in mới được phép check-out
            // mặc định x = 0 (có thể check-out ngay sau khi check-in), admin có thể custom qua system settings
            // ví dụ: nếu set = 30 phút, user check-in lúc 9h thì phải đợi đến 9h30 mới được checkout
            // mục đích: đảm bảo user sử dụng đủ thời gian tối thiểu, tránh lãng phí tài nguyên
            var checkoutMinMinutes = await _systemSettingsService.GetCheckoutMinMinutesAfterCheckInAsync();
            // đảm bảo số phút >= 0
            if (checkoutMinMinutes < 0) checkoutMinMinutes = 0;

            // tính thời gian sớm nhất có thể check-out (check-in time + số phút tối thiểu)
            var minCheckoutTime = booking.CheckInTime.Value.AddMinutes(checkoutMinMinutes);

            // kiểm tra nếu check-out quá sớm
            if (now < minCheckoutTime)
            {
                return ApiResponse<BookingResponseDto>.Fail(
                    400,
                    $"Chỉ có thể check-out sau khi đã check-in ít nhất {checkoutMinMinutes} phút. " +
                    $"Thời gian sớm nhất có thể check-out: {minCheckoutTime:dd/MM/yyyy HH:mm:ss}."
                );
            }

            // set check-out time, note, and images
            booking.CheckOutTime = now; // ghi nhận thời gian check-out (vietnam time)
            booking.UpdatedAt = now;

            // lưu ghi chú và ảnh nếu có
            if (dto != null)
            {
                booking.CheckOutNote = dto.Note; // ghi chú khi check-out (ví dụ: tình trạng phòng sau khi sử dụng)
                // convert image urls list to json string để lưu vào database
                // frontend cần upload ảnh trước (qua cloudinary) và gửi urls trong request
                if (dto.ImageUrls != null && dto.ImageUrls.Any())
                {
                    booking.CheckOutImages = System.Text.Json.JsonSerializer.Serialize(dto.ImageUrls);
                }
            }

            // khi check-out thì booking hoàn tất, tự động set status = completed
            // booking đã được sử dụng xong, không cần thao tác gì thêm
            booking.Status = BookingStatus.Completed;

            await _unitOfWork.BookingRepo.UpdateAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            var responseDto = _mapper.Map<BookingResponseDto>(booking);
            return ApiResponse<BookingResponseDto>.Ok(responseDto);
        }

        /// <summary>
        /// Validate thời gian booking hợp lý
        /// </summary>
        private (bool IsValid, string ErrorMessage) ValidateBookingTime(DateTime startTime, DateTime endTime)
        {
            var now = DateTimeHelper.VietnamNow;

            // 1. EndTime phải sau StartTime
            if (endTime <= startTime)
            {
                return (false, "Thời gian kết thúc phải sau thời gian bắt đầu.");
            }

            // 2. Thời gian tối thiểu: 1 giờ
            var duration = endTime - startTime;
            if (duration.TotalHours < 1)
            {
                return (false, "Thời gian booking phải tối thiểu 1 giờ.");
            }

            // 3. Thời gian tối đa: 3 giờ
            if (duration.TotalHours > 3)
            {
                return (false, "Thời gian booking không được vượt quá 3 giờ.");
            }

            // 4. StartTime phải trước ít nhất X giờ (lấy từ settings, mặc định 3 giờ)
            // Note: Method này là sync, nhưng validation thực tế được làm trong ValidateBookingTimeAsync
            // Giữ lại method này để tương thích, nhưng logic chính sẽ dùng async version
            var minStartTime = now.AddHours(-1);
            if (startTime < minStartTime)
            {
                return (false, "Chỉ được phép đặt phòng trước ít nhất 3 giờ. Ví dụ: muốn đặt phòng lúc 10h thì phải đặt từ trước 7h.");
            }

            // 5. Không được đặt quá xa trong tương lai: 3 tháng
            var maxStartTime = now.AddMonths(3);
            if (startTime > maxStartTime)
            {
                return (false, "Không thể đặt booking quá 3 tháng trong tương lai.");
            }

            // 6. EndTime không được quá xa trong tương lai
            var maxEndTime = now.AddMonths(3).AddDays(1);
            if (endTime > maxEndTime)
            {
                return (false, "Thời gian kết thúc không được vượt quá 3 tháng trong tương lai.");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// validate thời gian booking hợp lý (async version với settings)
        /// hàm này kiểm tra tất cả các ràng buộc về thời gian khi tạo/cập nhật booking
        /// </summary>
        private async Task<(bool IsValid, string ErrorMessage)> ValidateBookingTimeAsync(DateTime startTime, DateTime endTime)
        {
            var now = DateTimeHelper.VietnamNow;

            // ràng buộc 1: endtime phải sau starttime
            // đảm bảo thời gian kết thúc luôn sau thời gian bắt đầu
            if (endTime <= startTime)
            {
                return (false, "Thời gian kết thúc phải sau thời gian bắt đầu.");
            }

            // ràng buộc 2: thời gian tối thiểu: 1 giờ
            // mỗi booking phải có thời lượng tối thiểu 1 giờ
            var duration = endTime - startTime;
            if (duration.TotalHours < 1)
            {
                return (false, "Thời gian booking phải tối thiểu 1 giờ.");
            }

            // ràng buộc 3: thời gian tối đa: 3 giờ
            // mỗi booking không được vượt quá 3 giờ để đảm bảo công bằng cho tất cả user
            if (duration.TotalHours > 3)
            {
                return (false, "Thời gian booking không được vượt quá 3 giờ.");
            }

            // ràng buộc 4: starttime phải trước ít nhất x giờ (lấy từ system settings, mặc định 3 giờ)
            // ví dụ: nếu setting = 3 giờ, booking bắt đầu lúc 10h thì phải đặt trước 7h
            // mục đích: cho admin có thời gian xử lý và duyệt booking
            // giá trị này có thể được admin thay đổi qua system settings api
            var minBookingHours = await _systemSettingsService.GetMinimumBookingHoursBeforeStartAsync();
            var minStartTime = now.AddHours(minBookingHours);
            if (startTime < minStartTime)
            {
                return (false, $"Không thể đặt booking. Thời gian bắt đầu phải trước ít nhất {minBookingHours} giờ từ bây giờ.");
            }

            // ràng buộc 5: không được đặt quá xa trong tương lai: 3 tháng
            // giới hạn booking trong 3 tháng để quản lý dễ dàng hơn
            var maxStartTime = now.AddMonths(3);
            if (startTime > maxStartTime)
            {
                return (false, "Không thể đặt booking quá 3 tháng trong tương lai.");
            }

            // ràng buộc 6: endtime không được quá xa trong tương lai
            // đảm bảo cả thời gian kết thúc cũng không vượt quá 3 tháng
            var maxEndTime = now.AddMonths(3).AddDays(1);
            if (endTime > maxEndTime)
            {
                return (false, "Thời gian kết thúc không được vượt quá 3 tháng trong tương lai.");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// tự động hủy các booking quá thời gian check-in
        /// hàm này được gọi bởi background service mỗi 5 phút để kiểm tra và hủy các booking không check-in đúng giờ
        /// </summary>
        public async Task ProcessLateCheckInBookingsAsync()
        {
            var now = DateTimeHelper.VietnamNow;
            // lấy số phút cho phép check-in sau starttime từ system settings (mặc định 15 phút)
            // ví dụ: nếu setting = 15 phút, booking 9h thì phải check-in trước 9h15
            var checkInMinutesAfter = await _systemSettingsService.GetCheckInMinutesAfterStartAsync();

            // query tất cả bookings và filter các booking cần hủy
            // điều kiện:
            // - status = approved (đã được duyệt)
            // - checkintime = null (chưa check-in)
            // - starttime + checkinminutesafter < now (đã quá thời gian cho phép check-in)
            var bookings = await _unitOfWork.BookingRepo.GetAllAsync();
            var lateCheckInBookings = bookings
                .Where(b => b.Status == BookingStatus.Approved
                    && b.CheckInTime == null
                    && b.StartTime.AddMinutes(checkInMinutesAfter) < now)
                .ToList();

            // với mỗi booking quá thời gian, hủy booking và tạo thông báo cho user
            foreach (var booking in lateCheckInBookings)
            {
                // cập nhật booking sang trạng thái cancelled
                booking.Status = BookingStatus.Cancelled;
                booking.CancelledAt = DateTimeHelper.VietnamNow;
                booking.CancellationReason = "Quá thời gian check-in"; // lý do hủy tự động
                booking.UpdatedAt = DateTimeHelper.VietnamNow;
                await _unitOfWork.BookingRepo.UpdateAsync(booking);

                // tạo thông báo cho user để thông báo booking đã bị hủy
                var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(booking.FacilityId);
                var notificationId = await GenerateNotificationIdAsync();
                var notification = new Notification
                {
                    NotificationId = notificationId,
                    UserId = booking.UserId, // gửi thông báo cho chủ booking
                    Type = NotificationType.Booking_Cancelled,
                    Title = "Booking đã bị hủy do quá thời gian check-in",
                    Message = $"Booking {booking.BookingId} cho facility {facility?.Name ?? "N/A"} đã bị hủy do quá thời gian check-in ({checkInMinutesAfter} phút sau giờ bắt đầu).",
                    Status = NotificationStatus.Unread,
                    BookingId = booking.BookingId,
                    CreatedAt = now
                };
                await _unitOfWork.NotificationRepo.CreateAsync(notification);
            }

            // lưu tất cả thay đổi vào database (transaction)
            // chỉ lưu nếu có booking nào bị hủy
            if (lateCheckInBookings.Any())
            {
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task<string> GenerateNotificationIdAsync()
        {
            var notifications = await _unitOfWork.NotificationRepo.GetAllAsync();
            var maxId = 0;

            foreach (var notification in notifications)
            {
                if (notification.NotificationId.StartsWith("N") && notification.NotificationId.Length == 6)
                {
                    if (int.TryParse(notification.NotificationId.Substring(1), out var id))
                    {
                        if (id > maxId)
                            maxId = id;
                    }
                }
            }

            return $"N{(maxId + 1):D5}";
        }
    }
}
