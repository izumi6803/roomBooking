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
    public class BookingFeedbackService : IBookingFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public BookingFeedbackService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<ApiResponse<BookingFeedbackResponseDto>> GetByIdAsync(string id)
        {
            var feedback = await _unitOfWork.BookingFeedbackRepo.GetByIdWithDetailsAsync(id);
            if (feedback == null)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(404, "Không tìm thấy feedback");
            }

            var dto = _mapper.Map<BookingFeedbackResponseDto>(feedback);
            return ApiResponse<BookingFeedbackResponseDto>.Ok(dto);
        }

        public async Task<ApiResponse<List<BookingFeedbackResponseDto>>> GetAllAsync()
        {
            var feedbacks = await _unitOfWork.BookingFeedbackRepo.GetAllAsync();
            var dtos = _mapper.Map<List<BookingFeedbackResponseDto>>(feedbacks);
            return ApiResponse<List<BookingFeedbackResponseDto>>.Ok(dtos);
        }

        public async Task<ApiResponse<List<BookingFeedbackResponseDto>>> GetByBookingIdAsync(string bookingId)
        {
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
            {
                return ApiResponse<List<BookingFeedbackResponseDto>>.Fail(404, "Không tìm thấy booking");
            }

            var feedbacks = await _unitOfWork.BookingFeedbackRepo.GetByBookingIdAsync(bookingId);
            var dtos = _mapper.Map<List<BookingFeedbackResponseDto>>(feedbacks);
            return ApiResponse<List<BookingFeedbackResponseDto>>.Ok(dtos);
        }

        public async Task<ApiResponse<List<BookingFeedbackResponseDto>>> GetByUserIdAsync(string userId)
        {
            var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse<List<BookingFeedbackResponseDto>>.Fail(404, "Không tìm thấy user");
            }

            var feedbacks = await _unitOfWork.BookingFeedbackRepo.GetByUserIdAsync(userId);
            var dtos = _mapper.Map<List<BookingFeedbackResponseDto>>(feedbacks);
            return ApiResponse<List<BookingFeedbackResponseDto>>.Ok(dtos);
        }

        public async Task<ApiResponseWithPagination<List<BookingFeedbackResponseDto>>> GetFilteredAsync(BookingFeedbackFilterDto filter)
        {
            var (items, total) = await _unitOfWork.BookingFeedbackRepo.GetFilteredAsync(
                filter.UserId,
                filter.BookingId,
                filter.MinRating,
                filter.MaxRating,
                filter.ReportIssue,
                filter.IsResolved,
                filter.Page,
                filter.Limit
            );

            var dtos = _mapper.Map<List<BookingFeedbackResponseDto>>(items);
            return ApiResponseWithPagination<List<BookingFeedbackResponseDto>>.Ok(dtos, filter.Page, filter.Limit, total);
        }

        public async Task<ApiResponse<double>> GetAverageFacilityRatingAsync(string facilityId)
        {
            var facility = await _unitOfWork.FacilityRepo.GetByIdAsync(facilityId);
            if (facility == null)
            {
                return ApiResponse<double>.Fail(404, "Không tìm thấy facility");
            }

            var averageRating = await _unitOfWork.BookingFeedbackRepo.GetAverageFacilityRatingAsync(facilityId);
            return ApiResponse<double>.Ok(averageRating);
        }

        public async Task<ApiResponse<List<BookingFeedbackResponseDto>>> GetUnresolvedIssuesAsync()
        {
            var feedbacks = await _unitOfWork.BookingFeedbackRepo.GetUnresolvedIssuesAsync();
            var dtos = _mapper.Map<List<BookingFeedbackResponseDto>>(feedbacks);
            return ApiResponse<List<BookingFeedbackResponseDto>>.Ok(dtos);
        }

        public async Task<ApiResponse<BookingFeedbackResponseDto>> CreateAsync(string userId, CreateBookingFeedbackDto dto)
        {
            // Validate booking exists
            var booking = await _unitOfWork.BookingRepo.GetByIdAsync(dto.BookingId);
            if (booking == null)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(404, "Không tìm thấy booking");
            }

            // Validate booking belongs to user (chỉ owner mới tạo feedback)
            if (booking.UserId != userId)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(403, "Chỉ người đặt booking mới có thể tạo feedback");
            }

            // Validate booking is completed
            if (booking.Status != BookingStatus.Completed)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(400, "Chỉ có thể tạo feedback cho booking đã hoàn thành");
            }

            // Check if booking already has feedback (1 booking = 1 feedback)
            var existingFeedback = await _unitOfWork.BookingFeedbackRepo.BookingHasFeedbackAsync(dto.BookingId);
            if (existingFeedback)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(409, "Booking này đã có feedback rồi");
            }

            // Validate issue description if report issue
            if (dto.ReportIssue && string.IsNullOrWhiteSpace(dto.IssueDescription))
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(400, "Vui lòng mô tả vấn đề khi report issue");
            }

            // Generate FeedbackId
            var feedbackId = await GenerateFeedbackIdAsync();

            var feedback = _mapper.Map<BookingFeedback>(dto);
            feedback.FeedbackId = feedbackId;
            feedback.UserId = userId;
            feedback.CreatedAt = DateTime.UtcNow.AddHours(7); // UTC+7

            await _unitOfWork.BookingFeedbackRepo.AddAsync(feedback);
            await _unitOfWork.BookingFeedbackRepo.SaveChangesAsync();

            // Create notification for Facility Admin
            await _notificationService.CreateFeedbackReceivedNotificationAsync(feedbackId);

            var result = await _unitOfWork.BookingFeedbackRepo.GetByIdWithDetailsAsync(feedbackId);
            var responseDto = _mapper.Map<BookingFeedbackResponseDto>(result!);

            return ApiResponse<BookingFeedbackResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<BookingFeedbackResponseDto>> UpdateAsync(string id, string userId, UpdateBookingFeedbackDto dto)
        {
            var feedback = await _unitOfWork.BookingFeedbackRepo.GetByIdAsync(id);
            if (feedback == null)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(404, "Không tìm thấy feedback");
            }

            // Only owner can update their feedback
            if (feedback.UserId != userId)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(403, "Bạn không có quyền cập nhật feedback này");
            }

            // Validate issue description if report issue
            if (dto.ReportIssue == true && string.IsNullOrWhiteSpace(dto.IssueDescription))
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(400, "Vui lòng mô tả vấn đề khi report issue");
            }

            // Update fields
            if (dto.Rating.HasValue)
                feedback.Rating = dto.Rating.Value;
            if (dto.Comments != null)
                feedback.Comments = dto.Comments;
            if (dto.ReportIssue.HasValue)
                feedback.ReportIssue = dto.ReportIssue.Value;
            if (dto.IssueDescription != null)
                feedback.IssueDescription = dto.IssueDescription;

            await _unitOfWork.BookingFeedbackRepo.UpdateAsync(feedback);
            await _unitOfWork.BookingFeedbackRepo.SaveChangesAsync();

            var result = await _unitOfWork.BookingFeedbackRepo.GetByIdWithDetailsAsync(id);
            var responseDto = _mapper.Map<BookingFeedbackResponseDto>(result!);

            return ApiResponse<BookingFeedbackResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse<BookingFeedbackResponseDto>> ResolveIssueAsync(string id, string adminUserId)
        {
            var feedback = await _unitOfWork.BookingFeedbackRepo.GetByIdAsync(id);
            if (feedback == null)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(404, "Không tìm thấy feedback");
            }

            if (!feedback.ReportIssue)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(400, "Feedback này không có issue để resolve");
            }

            if (feedback.ResolvedAt.HasValue)
            {
                return ApiResponse<BookingFeedbackResponseDto>.Fail(400, "Issue này đã được resolve rồi");
            }

            feedback.ResolvedAt = DateTime.UtcNow.AddHours(7); // UTC+7

            await _unitOfWork.BookingFeedbackRepo.UpdateAsync(feedback);
            await _unitOfWork.BookingFeedbackRepo.SaveChangesAsync();

            var result = await _unitOfWork.BookingFeedbackRepo.GetByIdWithDetailsAsync(id);
            var responseDto = _mapper.Map<BookingFeedbackResponseDto>(result!);

            return ApiResponse<BookingFeedbackResponseDto>.Ok(responseDto);
        }

        public async Task<ApiResponse> DeleteAsync(string id, string userId)
        {
            var feedback = await _unitOfWork.BookingFeedbackRepo.GetByIdAsync(id);
            if (feedback == null)
            {
                return ApiResponse.Fail(404, "Không tìm thấy feedback");
            }

            // Only owner can delete their feedback
            if (feedback.UserId != userId)
            {
                return ApiResponse.Fail(403, "Bạn không có quyền xóa feedback này");
            }

            await _unitOfWork.BookingFeedbackRepo.DeleteAsync(feedback);
            await _unitOfWork.BookingFeedbackRepo.SaveChangesAsync();

            return ApiResponse.Ok();
        }

        private async Task<string> GenerateFeedbackIdAsync()
        {
            var allFeedbacks = await _unitOfWork.BookingFeedbackRepo.GetAllAsync();
            var maxId = allFeedbacks
                .Select(f => int.Parse(f.FeedbackId.Substring(2)))
                .DefaultIfEmpty(0)
                .Max();

            return $"BF{(maxId + 1):D4}";
        }
    }
}

