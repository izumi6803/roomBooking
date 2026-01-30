import { apiClient, handleApiError } from '../../../../services/apiClient';
import type { ApiResponse } from '../../../../types/api';

// Interface cho Feedback từ API response
export interface MyFeedback {
  feedbackId: string;
  bookingId: string;
  userId: string;
  userName: string;
  facilityName: string;
  rating: number;
  comments: string;
  reportIssue: boolean;
  issueDescription: string | null;
  createdAt: string; // ISO 8601 format
  resolvedAt: string | null; // ISO 8601 format
  isResolved: boolean;
}

// Interface cho API Response
export type MyFeedbacksResponse = ApiResponse<MyFeedback[]>;

/**
 * Lấy feedbacks của riêng mình
 * @returns Promise với danh sách feedbacks
 * @description
 * - GET /api/feedbacks/my-feedbacks
 * - Tất cả user đã đăng nhập có thể truy cập
 * - User xem lại tất cả feedback mình đã tạo
 */
export const getMyFeedbacks = async (): Promise<MyFeedbacksResponse> => {
  try {
    const response = await apiClient.get<MyFeedbacksResponse>('/api/feedbacks/my-feedbacks');
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy danh sách feedbacks');
  }
};

