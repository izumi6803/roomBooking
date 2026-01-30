import { apiClient, handleApiError } from '../../../../services/apiClient';
import type { ApiResponse } from '../../../../types/api';

// Interface cho Daily Statistics
export interface DailyStat {
  date: string; // ISO 8601 format
  totalBookings: number;
  completedBookings: number;
  utilizationRate: number;
}

// Interface cho Facility Statistics
export interface FacilityStat {
  facilityId: string;
  facilityName: string;
  campusName: string;
  totalBookings: number;
  completedBookings: number;
  utilizationRate: number;
  averageRating: number;
}

// Interface cho Campus Statistics
export interface CampusStat {
  campusId: string;
  campusName: string;
  totalBookings: number;
  completedBookings: number;
  utilizationRate: number;
  totalFacilities: number;
}

// Interface cho Overall Statistics
export interface OverallStatistics {
  totalBookings: number;
  approvedBookings: number;
  rejectedBookings: number;
  cancelledBookings: number;
  completedBookings: number;
  pendingBookings: number;
  approvalRate: number;
  cancellationRate: number;
  completionRate: number;
  utilizationRate: number;
}

// Interface cho Period Info
export interface PeriodInfo {
  periodType: string;
  startDate: string;
  endDate: string;
  totalDays: number;
}

// Interface cho Report Data
export interface ReportData {
  // Period info
  period?: PeriodInfo;
  // Overall statistics (nested in 'overall' object)
  overall?: OverallStatistics;
  // Daily statistics
  dailyStats?: DailyStat[];
  // Facility statistics
  facilityStats?: FacilityStat[];
  // Campus statistics
  campusStats?: CampusStat[];
}

// Interface cho Report Response
export interface ReportResponse extends ApiResponse<ReportData> {
  code?: number;
  message?: string;
}

// Query parameters cho getReport
export interface GetReportParams {
  periodType: 'day' | 'week' | 'month' | 'year' | 'custom';
  days?: number; // For periodType='day': 1, 3, 7, 14, or 30
  month?: number; // For periodType='month': 1-12
  year?: number; // For periodType='year' or 'month'
  startDate?: string; // For periodType='custom': ISO 8601 format
  endDate?: string; // For periodType='custom': ISO 8601 format
  campusId?: string; // Optional filter
  facilityId?: string; // Optional filter
}

/**
 * Lấy báo cáo thống kê bookings với filter thời gian và phân tích chi tiết
 * @param params - Query parameters cho filter
 * @returns Promise với dữ liệu báo cáo
 * @description
 * - GET /api/reports/bookings
 * - Chỉ Facility_Admin (RL0003) có thể truy cập
 * - Hỗ trợ nhiều loại periodType: day, week, month, year, custom
 */
export const getReport = async (params: GetReportParams): Promise<ReportResponse> => {
  try {
    const queryParams: Record<string, string | number> = {
      periodType: params.periodType,
    };

    // Thêm các tham số tùy theo periodType
    if (params.periodType === 'day' && params.days) {
      queryParams.days = params.days;
    } else if (params.periodType === 'month') {
      if (params.month) queryParams.month = params.month;
      if (params.year) queryParams.year = params.year;
    } else if (params.periodType === 'year' && params.year) {
      queryParams.year = params.year;
    } else if (params.periodType === 'custom') {
      if (params.startDate) queryParams.startDate = params.startDate;
      if (params.endDate) queryParams.endDate = params.endDate;
    }

    // Thêm các filter tùy chọn
    if (params.campusId) {
      queryParams.campusId = params.campusId;
    }
    if (params.facilityId) {
      queryParams.facilityId = params.facilityId;
    }

    const response = await apiClient.get<ReportResponse>('/api/reports/bookings', {
      params: queryParams,
    });

    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy báo cáo');
  }
};
