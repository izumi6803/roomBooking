using Applications.DTOs.Request;
using Applications.DTOs.Response;
using Applications.Helpers;
using BLL.Interfaces;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories;

namespace BLL.Classes
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<BookingReportResponseDto>> GetBookingReportAsync(BookingReportFilterDto filter)
        {
            var (startDate, endDate) = CalculatePeriod(filter);
            
            // Đảm bảo endDate bao gồm hết ngày cuối (23:59:59.999)
            var endDateInclusive = endDate.Date.AddDays(1).AddTicks(-1);
            
            var periodInfo = new PeriodInfo
            {
                PeriodType = filter.PeriodType ?? "custom",
                StartDate = startDate,
                EndDate = endDate,
                TotalDays = (int)(endDate - startDate).TotalDays + 1
            };

            // Get all bookings in period
            var allBookings = await _unitOfWork.BookingRepo.GetAllAsync();
            var bookingsInPeriod = allBookings
                .Where(b => b.StartTime >= startDate && b.StartTime <= endDateInclusive)
                .ToList();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.CampusId))
            {
                var facilities = await _unitOfWork.FacilityRepo.GetAllAsync();
                var facilityIds = facilities
                    .Where(f => f.CampusId == filter.CampusId)
                    .Select(f => f.FacilityId)
                    .ToList();
                bookingsInPeriod = bookingsInPeriod
                    .Where(b => facilityIds.Contains(b.FacilityId))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(filter.FacilityId))
            {
                bookingsInPeriod = bookingsInPeriod
                    .Where(b => b.FacilityId == filter.FacilityId)
                    .ToList();
            }

            // Overall statistics
            var overall = new OverallStatistics
            {
                TotalBookings = bookingsInPeriod.Count,
                ApprovedBookings = bookingsInPeriod.Count(b => b.Status == BookingStatus.Approved),
                RejectedBookings = bookingsInPeriod.Count(b => b.Status == BookingStatus.Rejected),
                CancelledBookings = bookingsInPeriod.Count(b => b.Status == BookingStatus.Cancelled),
                CompletedBookings = bookingsInPeriod.Count(b => b.Status == BookingStatus.Completed),
                PendingBookings = bookingsInPeriod.Count(b => b.Status == BookingStatus.Pending_Approval)
            };

            if (overall.TotalBookings > 0)
            {
                overall.ApprovalRate = (double)overall.ApprovedBookings / overall.TotalBookings * 100;
                overall.CancellationRate = (double)overall.CancelledBookings / overall.TotalBookings * 100;
                overall.CompletionRate = (double)overall.CompletedBookings / overall.TotalBookings * 100;
            }

            // Calculate utilization rate based on actual booking hours
            var allFacilities = await _unitOfWork.FacilityRepo.GetAllAsync();
            var availableFacilities = allFacilities.Where(f => f.Status == FacilityStatus.Available).ToList();
            var completedBookings = bookingsInPeriod.Where(b => b.Status == BookingStatus.Completed).ToList();
            
            // Calculate total used hours from completed bookings
            var totalUsedHours = completedBookings.Sum(b => (b.EndTime - b.StartTime).TotalHours);
            
            // Calculate total available hours (facilities × days × operating hours per day)
            // Assuming 12 operating hours per day (7:00 - 19:00)
            const int OPERATING_HOURS_PER_DAY = 12;
            var totalDays = CalculateDaysInPeriod(startDate, endDate);
            var totalAvailableHours = availableFacilities.Count * totalDays * OPERATING_HOURS_PER_DAY;
            
            if (totalAvailableHours > 0)
            {
                overall.UtilizationRate = Math.Round(totalUsedHours / totalAvailableHours * 100, 2);
            }

            // Daily statistics
            const int DAILY_OPERATING_HOURS = 12;
            var dailyStats = new List<DailyStatistics>();
            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                var dayBookings = bookingsInPeriod
                    .Where(b => b.StartTime.Date == date)
                    .ToList();

                var dayCompletedBookings = dayBookings.Where(b => b.Status == BookingStatus.Completed).ToList();
                var dayUsedHours = dayCompletedBookings.Sum(b => (b.EndTime - b.StartTime).TotalHours);
                var dayAvailableHours = availableFacilities.Count * DAILY_OPERATING_HOURS;
                var dayUtilization = dayAvailableHours > 0 
                    ? Math.Round(dayUsedHours / dayAvailableHours * 100, 2) 
                    : 0;

                dailyStats.Add(new DailyStatistics
                {
                    Date = date,
                    TotalBookings = dayBookings.Count,
                    CompletedBookings = dayCompletedBookings.Count,
                    UtilizationRate = dayUtilization
                });
            }

            // Facility statistics
            const int FACILITY_OPERATING_HOURS = 12;
            var facilityStats = new List<FacilityStatistics>();
            var facilityGroups = bookingsInPeriod.GroupBy(b => b.FacilityId);
            
            foreach (var group in facilityGroups)
            {
                var facility = allFacilities.FirstOrDefault(f => f.FacilityId == group.Key);
                if (facility == null) continue;

                var facilityBookings = group.ToList();
                var facilityCompletedBookings = facilityBookings.Where(b => b.Status == BookingStatus.Completed).ToList();
                var facilityUsedHours = facilityCompletedBookings.Sum(b => (b.EndTime - b.StartTime).TotalHours);
                var facilityAvailableHours = totalDays * FACILITY_OPERATING_HOURS;
                var utilization = facilityAvailableHours > 0 
                    ? Math.Round(facilityUsedHours / facilityAvailableHours * 100, 2) 
                    : 0;

                // Get average rating from feedbacks using the correct method
                var avgRating = await _unitOfWork.BookingFeedbackRepo.GetAverageFacilityRatingAsync(facility.FacilityId);

                facilityStats.Add(new FacilityStatistics
                {
                    FacilityId = facility.FacilityId,
                    FacilityName = facility.Name,
                    CampusName = facility.Campus?.Name ?? "",
                    TotalBookings = facilityBookings.Count,
                    CompletedBookings = facilityCompletedBookings.Count,
                    UtilizationRate = utilization,
                    AverageRating = avgRating
                });
            }

            // Campus statistics
            const int CAMPUS_OPERATING_HOURS = 12;
            var campusStats = new List<CampusStatistics>();
            var campuses = await _unitOfWork.CampusRepo.GetAllAsync();
            
            foreach (var campus in campuses)
            {
                var campusFacilities = availableFacilities.Where(f => f.CampusId == campus.CampusId).ToList();
                var campusFacilityIds = campusFacilities.Select(f => f.FacilityId).ToList();
                var campusBookings = bookingsInPeriod
                    .Where(b => campusFacilityIds.Contains(b.FacilityId))
                    .ToList();

                var campusCompletedBookings = campusBookings.Where(b => b.Status == BookingStatus.Completed).ToList();
                var campusUsedHours = campusCompletedBookings.Sum(b => (b.EndTime - b.StartTime).TotalHours);
                var campusAvailableHours = campusFacilities.Count * totalDays * CAMPUS_OPERATING_HOURS;
                var utilization = campusAvailableHours > 0
                    ? Math.Round(campusUsedHours / campusAvailableHours * 100, 2)
                    : 0;

                campusStats.Add(new CampusStatistics
                {
                    CampusId = campus.CampusId,
                    CampusName = campus.Name,
                    TotalBookings = campusBookings.Count,
                    CompletedBookings = campusCompletedBookings.Count,
                    UtilizationRate = utilization,
                    TotalFacilities = campusFacilities.Count
                });
            }

            var report = new BookingReportResponseDto
            {
                Period = periodInfo,
                Overall = overall,
                DailyStats = dailyStats,
                FacilityStats = facilityStats.OrderByDescending(f => f.UtilizationRate).ToList(),
                CampusStats = campusStats.OrderByDescending(c => c.UtilizationRate).ToList()
            };

            return ApiResponse<BookingReportResponseDto>.Ok(report);
        }

        private (DateTime startDate, DateTime endDate) CalculatePeriod(BookingReportFilterDto filter)
        {
            var now = DateTimeHelper.VietnamNow;

            if (filter.PeriodType == "day" && filter.Days.HasValue)
            {
                var days = filter.Days.Value;
                return (now.Date.AddDays(-days + 1), now.Date);
            }

            if (filter.PeriodType == "week")
            {
                return (now.Date.AddDays(-6), now.Date);
            }

            if (filter.PeriodType == "month" && filter.Month.HasValue && filter.Year.HasValue)
            {
                var start = new DateTime(filter.Year.Value, filter.Month.Value, 1);
                var end = start.AddMonths(1).AddDays(-1);
                return (start, end);
            }

            if (filter.PeriodType == "year" && filter.Year.HasValue)
            {
                var start = new DateTime(filter.Year.Value, 1, 1);
                var end = new DateTime(filter.Year.Value, 12, 31);
                return (start, end);
            }

            // Custom period
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                return (filter.StartDate.Value.Date, filter.EndDate.Value.Date);
            }

            // Default: last 30 days
            return (now.Date.AddDays(-29), now.Date);
        }

        private int CalculateDaysInPeriod(DateTime startDate, DateTime endDate)
        {
            return (int)(endDate - startDate).TotalDays + 1;
        }
    }
}

