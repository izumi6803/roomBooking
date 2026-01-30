import { apiClient, handleApiError } from '../../../../services/apiClient';
import type { ApiResponse } from '../../../../types/api';

// Interface cho System Settings
export interface SystemSettings {
  minimumBookingHoursBeforeStart: number;
  checkInMinutesBeforeStart: number;
  checkInMinutesAfterStart: number;
  checkoutMinRatio: number;
  checkOutMinutesAfterCheckIn: number;
}

// Interface cho API Response
export interface SystemSettingsResponse extends ApiResponse<SystemSettings> {
  data: SystemSettings;
}

// Interface cho Update Request
export interface UpdateSystemSettingsRequest {
  minimumBookingHoursBeforeStart?: number;
  checkInMinutesBeforeStart?: number;
  checkInMinutesAfterStart?: number;
  checkoutMinRatio?: number;
  checkOutMinutesAfterCheckIn?: number;
}

/**
 * Lấy cấu hình hệ thống hiện tại (Admin only)
 * @returns Promise với system settings
 * @description
 * - GET /api/system-settings
 * - Chỉ Facility_Admin (RL0003) có thể truy cập
 */
export const getSystemSettings = async (): Promise<SystemSettingsResponse> => {
  try {
    const response = await apiClient.get<SystemSettingsResponse>('/api/system-settings');
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy cấu hình hệ thống');
  }
};

/**
 * Cập nhật cấu hình hệ thống (Admin only)
 * @param settings - Các cấu hình cần cập nhật (chỉ cần gửi các field muốn cập nhật)
 * @returns Promise với kết quả cập nhật
 * @description
 * - PUT /api/system-settings
 * - Chỉ Facility_Admin (RL0003) có thể truy cập
 * - Chỉ cần gửi các field muốn cập nhật
 * - Các giá trị phải >= 0
 */
export const updateSystemSettings = async (
  settings: UpdateSystemSettingsRequest
): Promise<SystemSettingsResponse> => {
  try {
    const response = await apiClient.put<SystemSettingsResponse>(
      '/api/system-settings',
      settings
    );
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi cập nhật cấu hình hệ thống');
  }
};
