import axios from 'axios';
import { getToken } from '../../../../utils/auth';

const baseURL = import.meta.env.VITE_BASE_URL || 'http://localhost:5252';

// Tạo axios instance với baseURL
const myBookingsApiClient = axios.create({
  baseURL: baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Thêm request interceptor để tự động thêm token vào header
myBookingsApiClient.interceptors.request.use(
  (config) => {
    const token = getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Interface cho Booking từ API response
export interface MyBooking {
  bookingId: string;
  userId: string;
  userName: string;
  facilityId: string;
  facilityName: string;
  startTime: string; // ISO 8601 format
  endTime: string; // ISO 8601 format
  purpose: string;
  category: string;
  estimatedAttendees: number;
  specialRequirements: string;
  status: 'Draft' | 'Pending_Approval' | 'Approved' | 'Rejected' | 'Cancelled' | 'Completed' | 'No_Show';
  approvedBy: string | null;
  approvedAt: string | null; // ISO 8601 format
  rejectionReason: string | null;
  checkInTime: string | null; // ISO 8601 format
  checkOutTime: string | null; // ISO 8601 format
  isUsed: boolean;
  createdAt: string; // ISO 8601 format
  updatedAt: string; // ISO 8601 format
}

// Interface cho Pagination
export interface Pagination {
  page: number;
  limit: number;
  total: number;
}

// Interface cho API Response với pagination
export interface MyBookingsResponse {
  success: boolean;
  error: {
    code: number;
    message: string;
  } | null;
  code?: number;
  message?: string;
  data: MyBooking[];
  pagination?: Pagination;
}

// Query parameters cho getMyBookings
export interface GetMyBookingsParams {
  status?: 'Draft' | 'Pending_Approval' | 'Approved' | 'Rejected' | 'Cancelled' | 'Completed' | 'No_Show' | 'PENDING';
  page?: number;
  limit?: number;
}

/**
 * Lấy tất cả bookings của user hiện tại
 * @param params - Query parameters: status, page, limit
 * @returns Promise với danh sách bookings và pagination info
 * @description
 * - GET /api/bookings/me
 * - Tất cả user đã đăng nhập có thể truy cập
 * - User xem lại tất cả bookings của mình với filter theo status và phân trang
 */
export const getMyBookings = async (params?: GetMyBookingsParams): Promise<MyBookingsResponse> => {
  try {
    const queryParams: Record<string, string | number> = {};
    
    if (params?.status) {
      // Convert "PENDING" to "Pending_Approval" như API docs
      queryParams.status = params.status === 'PENDING' ? 'Pending_Approval' : params.status;
    }
    
    if (params?.page !== undefined) {
      queryParams.page = params.page;
    }
    
    if (params?.limit !== undefined) {
      queryParams.limit = params.limit;
    }

    const response = await myBookingsApiClient.get<MyBookingsResponse>('/api/bookings/me', {
      params: queryParams,
    });

    return response.data;
  } catch (error: unknown) {
    const axiosError = error as { response?: { data?: MyBookingsResponse; statusText?: string }; request?: unknown };
    if (axiosError.response) {
      const result = axiosError.response.data;
      if (result?.error) {
        throw new Error(result.error.message || 'Lỗi khi lấy danh sách bookings');
      }
      throw new Error(axiosError.response.statusText || 'Lỗi khi lấy danh sách bookings');
    }
    
    if (axiosError.request) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    console.error('Get my bookings API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

// Interface cho Cancel Booking Response
export interface CancelBookingResponse {
  success: boolean;
  error: {
    code: number;
    message: string;
  } | null;
}

/**
 * Hủy booking (set status = Cancelled)
 * @param bookingId - ID của booking cần hủy
 * @param reason - Lý do hủy
 * @returns Promise với response từ API
 * @description
 * - DELETE /api/bookings/{id}?reason={reason}
 * - Tất cả user đã đăng nhập có thể truy cập
 * - User chỉ được phép hủy tối đa 1 ngày trước ngày đặt lịch
 */
export const cancelBooking = async (
  bookingId: string,
  reason: string
): Promise<CancelBookingResponse> => {
  try {
    const response = await myBookingsApiClient.delete<CancelBookingResponse>(
      `/api/bookings/${bookingId}`,
      {
        params: {
          reason: reason,
        },
      }
    );

    return response.data;
  } catch (error: unknown) {
    const axiosError = error as { response?: { data?: CancelBookingResponse; statusText?: string }; request?: unknown };
    if (axiosError.response) {
      const result = axiosError.response.data;
      if (result?.error) {
        throw new Error(result.error.message || 'Lỗi khi hủy booking');
      }
      throw new Error(axiosError.response.statusText || 'Lỗi khi hủy booking');
    }
    
    if (axiosError.request) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    console.error('Cancel booking API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

