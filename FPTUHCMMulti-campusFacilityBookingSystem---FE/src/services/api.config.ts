// API Configuration
export const API_BASE_URL = 'http://localhost:5252/api';

// API Endpoints
export const API_ENDPOINTS = {
  // Auth
  AUTH: {
    LOGIN: '/auth/login',
    LOGOUT: '/auth/logout',
    GOOGLE_LOGIN: '/auth/login/google',
    VERIFY_EMAIL: '/auth/verify-email',
    FORGOT_PASSWORD: '/auth/forgot-password',
    RESET_PASSWORD: '/auth/reset-password',
  },
  
  // Campus
  CAMPUS: {
    GET_ALL: '/campuses',
    GET_BY_ID: (id: string) => `/campuses/${id}`,
    GET_FACILITIES: (campusId: string) => `/campuses/${campusId}/facilities`,
  },
  
  // Facility
  FACILITY: {
    GET_ALL: '/facilities',
    GET_BY_ID: (id: string) => `/facilities/${id}`,
  },
  
  // Booking
  BOOKING: {
    GET_ALL: '/bookings',
    GET_MY_BOOKINGS: '/bookings/me',
    GET_BY_ID: (id: string) => `/bookings/${id}`,
    CREATE: '/bookings',
    UPDATE: (id: string) => `/bookings/${id}`,
    SUBMIT: (id: string) => `/bookings/${id}/submit`,
    CANCEL: (id: string) => `/bookings/${id}`,
    CHECK_IN: (id: string) => `/bookings/${id}/check-in`,
    CHECK_IN_WITH_IMAGES: (id: string) => `/bookings/${id}/check-in-with-images`,
    CHECK_OUT: (id: string) => `/bookings/${id}/check-out`,
    CHECK_OUT_WITH_IMAGES: (id: string) => `/bookings/${id}/check-out-with-images`,
  },
  
  // Feedback
  FEEDBACK: {
    GET_ALL: '/feedbacks',
    GET_BY_ID: (id: string) => `/feedbacks/${id}`,
    CREATE: '/feedbacks',
    GET_FACILITY_RATING: (facilityId: string) => `/feedbacks/facility/${facilityId}/rating`,
  },
  
  // User
  USER: {
    GET_ALL: '/users',
    GET_BY_ID: (id: string) => `/users/${id}`,
    GET_PROFILE: '/users/profile',
  },
  
  // System Settings
  SYSTEM_SETTINGS: {
    GET: '/system-settings',
  },
};

// Helper function to build full URL
export const buildUrl = (endpoint: string, params?: Record<string, string | number | undefined>) => {
  const url = new URL(`${API_BASE_URL}${endpoint}`);
  
  if (params) {
    Object.entries(params).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        url.searchParams.append(key, String(value));
      }
    });
  }
  
  return url.toString();
};

// API Response types
export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  error?: {
    code: number;
    message: string;
  };
}

export interface ApiResponseWithPagination<T> extends ApiResponse<T> {
  pagination?: {
    page: number;
    limit: number;
    total: number;
  };
}

// Fetch wrapper with error handling
export const apiFetch = async <T>(
  url: string,
  options?: RequestInit
): Promise<ApiResponse<T>> => {
  try {
    const token = sessionStorage.getItem('auth_token');
    
    // Check if body is FormData - if so, don't set Content-Type (browser will set it with boundary)
    const isFormData = options?.body instanceof FormData;
    
    const headers: HeadersInit = isFormData 
      ? { ...options?.headers }
      : {
          'Content-Type': 'application/json',
          ...options?.headers,
        };
    
    if (token) {
      (headers as Record<string, string>)['Authorization'] = `Bearer ${token}`;
    }
    
    const response = await fetch(url, {
      ...options,
      headers,
    });
    
    const contentType = response.headers.get('content-type');
    let data: ApiResponse<T>;

    if (contentType && contentType.includes('application/json')) {
      const text = await response.text();
      if (text) {
        try {
          data = JSON.parse(text);
        } catch (e) {
          return {
            success: false,
            error: {
              code: response.status,
              message: `Server returned invalid JSON. Status: ${response.status} ${response.statusText}`,
            },
          };
        }
      } else {
        data = {
          success: response.ok,
          error: response.ok ? undefined : {
            code: response.status,
            message: response.statusText || 'Request failed',
          },
        };
      }
    } else {
      const text = await response.text();
      data = {
        success: response.ok,
        error: response.ok ? undefined : {
          code: response.status,
          message: text || response.statusText || 'Request failed',
        },
      };
    }
    
    return data;
  } catch (error) {
    console.error('API Error:', error);
    return {
      success: false,
      error: {
        code: 500,
        message: error instanceof Error ? error.message : 'Đã xảy ra lỗi không xác định',
      },
    };
  }
};




