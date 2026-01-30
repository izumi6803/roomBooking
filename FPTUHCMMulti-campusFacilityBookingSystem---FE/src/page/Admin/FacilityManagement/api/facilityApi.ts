import axios from 'axios';
import { getToken } from '../../../../utils/auth';

const baseURL = import.meta.env.VITE_BASE_URL || 'http://localhost:5252';

// Tạo axios instance với baseURL
const facilityApiClient = axios.create({
  baseURL: baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Thêm request interceptor để tự động thêm token vào header
facilityApiClient.interceptors.request.use(
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

// Interface cho Facility từ API response
export interface Facility {
  facilityId: string;
  name: string;
  description: string;
  capacity: number;
  roomNumber: string;
  floorNumber: string;
  campusId: string;
  campusName: string;
  typeId: string;
  typeName: string;
  status: 'Available' | 'Under_Maintenance';
  amenities: string;
  facilityManagerId: string | null;
  facilityManagerName: string | null;
  maxConcurrentBookings: number;
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
export interface FacilitiesResponse {
  message: string;
  data: Facility[];
  pagination?: Pagination;
}

// Interface cho Create/Update Facility Request
export interface FacilityRequest {
  name: string;
  description: string;
  capacity: number;
  roomNumber: string;
  floorNumber: string;
  campusId: string;
  typeId: string;
  status: 'Available' | 'Under_Maintenance';
  amenities: string;
  facilityManagerId?: string;
  maxConcurrentBookings: number;
}

// Interface cho Create/Update Facility Response
export interface FacilityActionResponse {
  success: boolean;
  error: {
    code: number;
    message: string;
  } | null;
  data?: Facility;
}

// Interface cho Delete Response
export interface DeleteFacilityResponse {
  success: boolean;
  error: {
    code: number;
    message: string;
  } | null;
}

// Query parameters cho getFacilities
export interface GetFacilitiesParams {
  name?: string;
  status?: 'Available' | 'Under_Maintenance';
  typeId?: string;
  campusId?: string;
  page?: number;
  limit?: number;
}

/**
 * Lấy danh sách facilities với filtering và pagination
 * @param params - Query parameters: name, status, typeId, campusId, page, limit
 * @returns Promise với danh sách facilities và pagination info
 * @description
 * - GET /api/facilities
 * - Công khai - Không cần đăng nhập
 */
export const getFacilities = async (params?: GetFacilitiesParams): Promise<FacilitiesResponse> => {
  try {
    const queryParams: Record<string, string | number> = {};
    
    if (params?.name) {
      queryParams.name = params.name;
    }
    
    if (params?.status) {
      queryParams.status = params.status;
    }
    
    if (params?.typeId) {
      queryParams.typeId = params.typeId;
    }
    
    if (params?.campusId) {
      queryParams.campusId = params.campusId;
    }
    
    if (params?.page !== undefined) {
      queryParams.page = params.page;
    }
    
    if (params?.limit !== undefined) {
      queryParams.limit = params.limit;
    }

    const response = await facilityApiClient.get<FacilitiesResponse>('/api/facilities', {
      params: queryParams,
    });

    return response.data;
  } catch (error: unknown) {
    const axiosError = error as { response?: { data?: { message?: string }; statusText?: string }; request?: unknown };
    if (axiosError.response) {
      const result = axiosError.response.data;
      if (result?.message) {
        throw new Error(result.message);
      }
      throw new Error(axiosError.response.statusText || 'Lỗi khi lấy danh sách facilities');
    }
    
    if (axiosError.request) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    console.error('Get facilities API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

/**
 * Tạo facility mới
 * @param facilityData - Thông tin facility cần tạo
 * @returns Promise với facility đã được tạo
 * @description
 * - POST /api/facilities
 * - Chỉ Facility_Admin (RL0003) có thể tạo
 */
export const createFacility = async (facilityData: FacilityRequest): Promise<FacilityActionResponse> => {
  try {
    const response = await facilityApiClient.post<FacilityActionResponse>('/api/facilities', facilityData);

    return response.data;
  } catch (error: unknown) {
    const axiosError = error as { response?: { data?: FacilityActionResponse; statusText?: string }; request?: unknown };
    if (axiosError.response) {
      const result = axiosError.response.data;
      if (result?.error) {
        throw new Error(result.error.message || 'Lỗi khi tạo facility');
      }
      throw new Error(axiosError.response.statusText || 'Lỗi khi tạo facility');
    }
    
    if (axiosError.request) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    console.error('Create facility API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

/**
 * Cập nhật thông tin facility
 * @param facilityId - ID của facility cần cập nhật
 * @param facilityData - Thông tin facility cần cập nhật
 * @param status - Trạng thái facility (optional query parameter)
 * @returns Promise với facility đã được cập nhật
 * @description
 * - PUT /api/facilities/{id}
 * - Chỉ Facility_Admin (RL0003) có thể cập nhật
 * - Có thể cập nhật status qua query parameter
 */
export const updateFacility = async (
  facilityId: string,
  facilityData: FacilityRequest,
  status?: 'Available' | 'Under_Maintenance'
): Promise<FacilityActionResponse> => {
  try {
    const queryParams: Record<string, string> = {};
    if (status) {
      queryParams.status = status;
    }

    const response = await facilityApiClient.put<FacilityActionResponse>(
      `/api/facilities/${facilityId}`,
      facilityData,
      { params: queryParams }
    );

    return response.data;
  } catch (error: unknown) {
    const axiosError = error as { response?: { data?: FacilityActionResponse; statusText?: string }; request?: unknown };
    if (axiosError.response) {
      const result = axiosError.response.data;
      if (result?.error) {
        throw new Error(result.error.message || 'Lỗi khi cập nhật facility');
      }
      throw new Error(axiosError.response.statusText || 'Lỗi khi cập nhật facility');
    }
    
    if (axiosError.request) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    console.error('Update facility API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

/**
 * Vô hiệu hóa facility (soft delete - set status = Under_Maintenance)
 * @param facilityId - ID của facility cần vô hiệu hóa
 * @returns Promise với kết quả
 * @description
 * - DELETE /api/facilities/{id}
 * - Chỉ Facility_Admin (RL0003) có thể vô hiệu hóa
 * - Soft delete: chỉ set status = Under_Maintenance, không xóa dữ liệu
 */
export const deleteFacility = async (facilityId: string): Promise<DeleteFacilityResponse> => {
  try {
    const response = await facilityApiClient.delete<DeleteFacilityResponse>(`/api/facilities/${facilityId}`);

    return response.data;
  } catch (error: unknown) {
    const axiosError = error as { response?: { data?: DeleteFacilityResponse; statusText?: string }; request?: unknown };
    if (axiosError.response) {
      const result = axiosError.response.data;
      if (result?.error) {
        throw new Error(result.error.message || 'Lỗi khi vô hiệu hóa facility');
      }
      throw new Error(axiosError.response.statusText || 'Lỗi khi vô hiệu hóa facility');
    }
    
    if (axiosError.request) {
      throw new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
    
    console.error('Delete facility API error:', error);
    throw new Error('Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
  }
};

// Interface cho Feedback
export interface FacilityFeedback {
  feedbackId: string;
  bookingId: string;
  userId: string;
  userName: string;
  rating: number;
  comments: string;
  reportIssue: boolean;
  isResolved: boolean;
  createdAt: string;
}

export interface FacilityFeedbackResponse {
  success: boolean;
  data?: FacilityFeedback[];
  error?: {
    code: number;
    message: string;
  };
}

/**
 * Lấy danh sách feedbacks của một facility
 * @param facilityId - ID của facility
 * @returns Promise với danh sách feedbacks
 */
export const getFacilityFeedbacks = async (facilityId: string): Promise<FacilityFeedbackResponse> => {
  try {
    // Get all bookings for this facility first
    const bookingsResponse = await facilityApiClient.get('/api/bookings', {
      params: {
        facilityId,
        limit: 1000, // Get all bookings
      },
    });

    const bookings = bookingsResponse.data?.data || [];
    const bookingIds = bookings.map((b: { bookingId: string }) => b.bookingId);

    if (bookingIds.length === 0) {
      return { success: true, data: [] };
    }

    // Get all feedbacks for these bookings
    const feedbacks: FacilityFeedback[] = [];
    for (const bookingId of bookingIds) {
      try {
        const feedbackResponse = await facilityApiClient.get(`/api/feedbacks/booking/${bookingId}`);
        if (feedbackResponse.data?.success && feedbackResponse.data?.data) {
          const feedbackData = Array.isArray(feedbackResponse.data.data)
            ? feedbackResponse.data.data
            : [feedbackResponse.data.data];
          feedbacks.push(...feedbackData);
        }
      } catch {
        // Skip if no feedback for this booking
        continue;
      }
    }

    return { success: true, data: feedbacks };
  } catch (error: unknown) {
    console.error('Get facility feedbacks error:', error);
    const axiosError = error as { response?: { status?: number; data?: { error?: { message?: string } } } };
    return {
      success: false,
      error: {
        code: axiosError.response?.status || 500,
        message: axiosError.response?.data?.error?.message || 'Lỗi khi lấy danh sách feedbacks',
      },
    };
  }
};

/**
 * Lấy rating trung bình của facility
 * @param facilityId - ID của facility
 * @returns Promise với rating trung bình
 */
export const getFacilityRating = async (facilityId: string): Promise<{ success: boolean; data?: number; error?: { code: number; message: string } }> => {
  try {
    const response = await facilityApiClient.get(`/api/feedbacks/facility/${facilityId}/rating`);
    return {
      success: true,
      data: response.data?.data || 0,
    };
  } catch (error: unknown) {
    const axiosError = error as { response?: { status?: number; data?: { error?: { message?: string } } } };
    return {
      success: false,
      error: {
        code: axiosError.response?.status || 500,
        message: axiosError.response?.data?.error?.message || 'Lỗi khi lấy rating',
      },
    };
  }
};

