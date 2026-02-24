// API Configuration
// Use environment variable for production, fallback to localhost for development
// Remove trailing slash if present to avoid double slashes
const baseURL = (import.meta.env.VITE_BASE_URL || 'http://localhost:5252').replace(/\/$/, '');
export const API_BASE_URL = baseURL;

// API Endpoints
export const API_ENDPOINTS = {
  // Auth
  AUTH: {
    LOGIN: '/api/auth/login',
    LOGOUT: '/api/auth/logout',
    GOOGLE_LOGIN: '/api/auth/login/google',
    VERIFY_EMAIL: '/api/auth/verify-email',
    FORGOT_PASSWORD: '/api/auth/forgot-password',
    RESET_PASSWORD: '/api/auth/reset-password',
  },
  
  // Campus
  CAMPUS: {
    GET_ALL: '/api/campuses',
    GET_BY_ID: (id: string) => `/api/campuses/${id}`,
    GET_FACILITIES: (campusId: string) => `/api/campuses/${campusId}/facilities`,
  },
  
  // Facility
  FACILITY: {
    GET_ALL: '/api/facilities',
    GET_BY_ID: (id: string) => `/api/facilities/${id}`,
  },
  
  // Booking
  BOOKING: {
    GET_ALL: '/api/bookings',
    GET_MY_BOOKINGS: '/api/bookings/me',
    GET_BY_ID: (id: string) => `/api/bookings/${id}`,
    CREATE: '/api/bookings',
    UPDATE: (id: string) => `/api/bookings/${id}`,
    SUBMIT: (id: string) => `/api/bookings/${id}/submit`,
    CANCEL: (id: string) => `/api/bookings/${id}`,
    CHECK_IN: (id: string) => `/api/bookings/${id}/check-in`,
    CHECK_IN_WITH_IMAGES: (id: string) => `/api/bookings/${id}/check-in-with-images`,
    CHECK_OUT: (id: string) => `/api/bookings/${id}/check-out`,
    CHECK_OUT_WITH_IMAGES: (id: string) => `/api/bookings/${id}/check-out-with-images`,
  },
  
  // Feedback
  FEEDBACK: {
    GET_ALL: '/api/feedbacks',
    GET_BY_ID: (id: string) => `/api/feedbacks/${id}`,
    CREATE: '/api/feedbacks',
    GET_FACILITY_RATING: (facilityId: string) => `/api/feedbacks/facility/${facilityId}/rating`,
  },
  
  // User
  USER: {
    GET_ALL: '/api/users',
    GET_BY_ID: (id: string) => `/api/users/${id}`,
    GET_PROFILE: '/api/users/profile',
  },
  
  // System Settings
  SYSTEM_SETTINGS: {
    GET: '/api/system-settings',
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




