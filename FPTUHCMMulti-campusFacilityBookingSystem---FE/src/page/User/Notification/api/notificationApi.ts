import { apiClient, handleApiError } from '../../../../services/apiClient';
import type { ApiResponse, PaginatedResponse } from '../../../../types/api';

// Interface cho Notification từ API response
export interface Notification {
  notificationId: string;
  userId: string;
  type: string;
  title: string;
  message: string;
  status: 'Unread' | 'Read';
  bookingId?: string;
  feedbackId?: string;
  createdAt: string;
  readAt?: string;
}

// Interface cho API Response
export type NotificationResponse = PaginatedResponse<Notification>;

// Interface cho Unread Count Response
export type UnreadCountResponse = ApiResponse<number>;

// Interface cho Standard Response
export type StandardResponse = ApiResponse;

// Interface cho Filter Parameters
export interface NotificationFilters {
  userId?: string;
  type?: 'Booking_Pending_Approval' | 'Booking_Approved' | 'Booking_Rejected' | 'Booking_Reminder_Checkin' | 'Booking_Reminder_CheckOut' | 'Feedback_Received' | 'Booking_No_Show';
  status?: 'Unread' | 'Read';
  page?: number;
  limit?: number;
}

/**
 * Lấy danh sách thông báo với filtering và pagination
 */
export const getNotifications = async (filters?: NotificationFilters): Promise<NotificationResponse> => {
  try {
    const params = new URLSearchParams();
    
    if (filters?.userId) params.append('UserId', filters.userId);
    if (filters?.type) params.append('Type', filters.type);
    if (filters?.status) params.append('Status', filters.status);
    if (filters?.page !== undefined) params.append('Page', filters.page.toString());
    if (filters?.limit !== undefined) params.append('Limit', filters.limit.toString());

    const response = await apiClient.get<NotificationResponse>(
      `/api/notifications${params.toString() ? `?${params.toString()}` : ''}`
    );
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy danh sách thông báo');
  }
};

/**
 * Lấy tất cả thông báo chưa đọc của user hiện tại
 */
export const getUnreadNotifications = async (): Promise<NotificationResponse> => {
  try {
    const response = await apiClient.get<NotificationResponse>('/api/notifications/unread');
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy thông báo chưa đọc');
  }
};

/**
 * Lấy số lượng thông báo chưa đọc của user hiện tại
 */
export const getUnreadCount = async (): Promise<UnreadCountResponse> => {
  try {
    const response = await apiClient.get<UnreadCountResponse>('/api/notifications/unread/count');
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy số lượng thông báo chưa đọc');
  }
};

/**
 * Đánh dấu thông báo là đã đọc
 */
export const markNotificationAsRead = async (notificationId: string): Promise<StandardResponse> => {
  try {
    const response = await apiClient.put<StandardResponse>(
      `/api/notifications/${notificationId}/read`
    );
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi đánh dấu thông báo đã đọc');
  }
};

/**
 * Đánh dấu tất cả thông báo của user hiện tại là đã đọc
 */
export const markAllNotificationsAsRead = async (): Promise<StandardResponse> => {
  try {
    const response = await apiClient.put<StandardResponse>('/api/notifications/read-all');
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi đánh dấu tất cả thông báo đã đọc');
  }
};

/**
 * Xóa thông báo
 */
export const deleteNotification = async (notificationId: string): Promise<StandardResponse> => {
  try {
    const response = await apiClient.delete<StandardResponse>(
      `/api/notifications/${notificationId}`
    );
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi xóa thông báo');
  }
};

