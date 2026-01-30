import axios from 'axios';
import { getToken } from '../../../../utils/auth';

const baseURL = import.meta.env.VITE_BASE_URL || 'http://localhost:5252';

// Tạo axios instance với baseURL
const adminBookingApiClient = axios.create({
  baseURL: baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Thêm request interceptor để tự động thêm token vào header
adminBookingApiClient.interceptors.request.use(
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

// Interface cho Feedback trong Booking
export interface BookingFeedback {
  feedbackId: string;
  rating: number;
  comments: string | null;
  reportIssue: boolean;
  issueDescription: string | null;
  createdAt: string;
}

// Interface cho Booking từ API response
export interface AdminBooking {
  bookingId: string;
  userId: string;
  userName: string;
  userEmail?: string | null;
  userPhoneNumber?: string | null;
  userStudentId?: string | null;
  facilityId: string;
  facilityName: string;
  startTime: string; // dd/MM/yyyy HH:mm:ss format
  endTime: string; // dd/MM/yyyy HH:mm:ss format
  purpose: string;
  category: string;
  estimatedAttendees: number;
  specialRequirements: string;
  status: 'Draft' | 'Pending_Approval' | 'Approved' | 'Rejected' | 'Cancelled' | 'Completed' | 'No_Show';
  approvedBy: string | null;
  approvedByUserName?: string | null;
  approvedAt: string | null;
  rejectionReason: string | null;
  checkInTime: string | null;
  checkInNote?: string | null;
  checkInImages?: string[] | null;
  checkOutTime: string | null;
  checkOutNote?: string | null;
  checkOutImages?: string[] | null;
  isUsed: boolean;
  createdAt: string;
  updatedAt: string;
  feedback: BookingFeedback | null; // Feedback từ user (nếu có)
}

// Interface cho Pagination
export interface Pagination {
  page: number;
  limit: number;
  total: number;
}

// Interface cho API Response
export interface AdminBookingsResponse {
  success: boolean;
  error: {
    code: number;
    message: string;
  } | null;
  code: number;
  message: string;
  data: AdminBooking[];
  pagination?: Pagination;
}

// Interface cho Approve/Reject Response
export interface BookingActionResponse {
  success: boolean;
  error: {
    code: number;
    message: string;
  } | null;
  data: AdminBooking | null;
}

// Interface cho Reject Request Body
export interface RejectBookingRequest {
  reason?: string;
}

// Query parameters cho getBookings
export interface GetBookingsParams {
  status?: 'Draft' | 'Pending_Approval' | 'Approved' | 'Rejected' | 'Cancelled' | 'Completed' | 'No_Show' | 'PENDING';
  page?: number;
  limit?: number;
}

/**
 * Lấy danh sách bookings với filtering và pagination (Admin view)
 * @param params - Query parameters: status, page, limit
 * @returns Promise với danh sách bookings và pagination info
 * @description
 * - GET /api/admin/bookings
 * - Có thể filter theo status
 * - Có pagination (page, limit)
 * - "PENDING" sẽ tự động convert thành "Pending_Approval"
 */
export const getAdminBookings = async (
  params?: GetBookingsParams
): Promise<AdminBookingsResponse> => {
  try {
    // Convert "PENDING" thành "Pending_Approval" nếu có
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

    const response = await adminBookingApiClient.get<AdminBookingsResponse>('/api/admin/bookings', {
      params: queryParams,
    });

    return response.data;
  } catch (error: unknown) {
    // Xử lý lỗi từ axios
    if (error && typeof error === 'object' && 'response' in error) {
      const axiosError = error as { response?: { data?: AdminBookingsResponse; statusText?: string } };
      // Lỗi từ server (400, 401, 500, etc.)
      if (axiosError.response) {
        const result = axiosError.response.data;
        if (result) {
          throw new Error(result.error?.message || result.message || 'Lỗi khi lấy danh sách bookings');
        }
        throw new Error(axiosError.response.statusText || 'Lỗi khi lấy danh sách bookings');
      }
    }
    
    // Lỗi network
    if (error && typeof error === 'object' && 'request' in error) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    // Lỗi khác
    console.error('Get admin bookings API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

/**
 * Duyệt booking (Approve)
 * @param bookingId - ID của booking cần duyệt
 * @returns Promise với booking đã được duyệt
 * @description
 * - PATCH /api/admin/bookings/{bookingId}/approve
 * - Booking phải ở trạng thái Pending_Approval
 * - Facility không được trùng lịch với booking đã approved
 * - Ghi nhận approver ID (người duyệt)
 */
export const approveBooking = async (bookingId: string): Promise<BookingActionResponse> => {
  try {
    const response = await adminBookingApiClient.patch<BookingActionResponse>(
      `/api/admin/bookings/${bookingId}/approve`
    );

    return response.data;
  } catch (error: unknown) {
    // Xử lý lỗi từ axios
    if (error && typeof error === 'object' && 'response' in error) {
      const axiosError = error as { response?: { data?: BookingActionResponse; statusText?: string } };
      if (axiosError.response) {
        const result = axiosError.response.data;
        if (result?.error) {
          throw new Error(result.error.message || 'Lỗi khi duyệt booking');
        }
        throw new Error(axiosError.response.statusText || 'Lỗi khi duyệt booking');
      }
    }
    
    if (error && typeof error === 'object' && 'request' in error) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    console.error('Approve booking API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

/**
 * Từ chối booking (Reject)
 * @param bookingId - ID của booking cần từ chối
 * @param reason - Lý do từ chối (optional)
 * @returns Promise với booking đã bị từ chối
 * @description
 * - PATCH /api/admin/bookings/{bookingId}/reject
 * - Booking phải ở trạng thái Pending_Approval
 * - Có thể gửi lý do (reason) trong body
 * - Lý do sẽ được lưu vào RejectionReason field
 */
export const rejectBooking = async (
  bookingId: string,
  reason?: string
): Promise<BookingActionResponse> => {
  try {
    const requestBody: RejectBookingRequest = {};
    if (reason && reason.trim()) {
      requestBody.reason = reason.trim();
    }

    const response = await adminBookingApiClient.patch<BookingActionResponse>(
      `/api/admin/bookings/${bookingId}/reject`,
      requestBody
    );

    return response.data;
  } catch (error: unknown) {
    // Xử lý lỗi từ axios
    if (error && typeof error === 'object' && 'response' in error) {
      const axiosError = error as { response?: { data?: BookingActionResponse; statusText?: string } };
      if (axiosError.response) {
        const result = axiosError.response.data;
        if (result?.error) {
          throw new Error(result.error.message || 'Lỗi khi từ chối booking');
        }
        throw new Error(axiosError.response.statusText || 'Lỗi khi từ chối booking');
      }
    }
    
    if (error && typeof error === 'object' && 'request' in error) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    console.error('Reject booking API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

