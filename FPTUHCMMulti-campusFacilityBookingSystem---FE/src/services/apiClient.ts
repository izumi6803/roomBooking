import axios, { type AxiosInstance, type AxiosRequestConfig, type AxiosError } from 'axios';
import { getToken } from '../utils/auth';

const baseURL = import.meta.env.VITE_BASE_URL || 'http://localhost:5252';

/**
 * Public API client for unauthenticated endpoints (login, register, etc.)
 * Does not include Authorization header
 */
export const publicApiClient: AxiosInstance = axios.create({
  baseURL: baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
});

/**
 * Centralized Axios instance with default configuration and authentication
 * All authenticated API files should use this instance instead of creating their own
 */
export const apiClient: AxiosInstance = axios.create({
  baseURL: baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
});

/**
 * REQUEST INTERCEPTOR - Tự động thêm token vào headers
 * 
 * Giải thích câu 9: "bằng cách lấy token từ response rồi lưu vào headers"
 * 
 * Flow hoạt động:
 * 1. Khi user đăng nhập thành công (loginAPI hoặc loginWithGoogle):
 *    - Server trả về token trong response.data.token
 *    - Token được lưu vào sessionStorage.setItem('auth_token', token)
 * 
 * 2. Khi gọi API cần authentication:
 *    - Interceptor này tự động chạy TRƯỚC mỗi request
 *    - Lấy token từ sessionStorage bằng getToken()
 *    - Nếu có token, tự động thêm vào header: Authorization: Bearer <token>
 *    - Server sẽ verify token này để xác thực user
 * 
 * Lợi ích:
 * - Không cần thêm token thủ công vào mỗi API call
 * - Tự động refresh token nếu cần (có thể mở rộng)
 * - Centralized authentication logic
 * 
 * @param {AxiosRequestConfig} config - Request config object
 * @returns {AxiosRequestConfig} - Config đã được thêm Authorization header
 */
apiClient.interceptors.request.use(
  (config) => {
    // Lấy token từ sessionStorage (đã được lưu khi login)
    const token = getToken();
    
    // Nếu có token, thêm vào Authorization header
    // Format: "Bearer <token>" (chuẩn JWT authentication)
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    
    return config;
  },
  (error) => {
    // Nếu có lỗi trong quá trình setup request, reject promise
    return Promise.reject(error);
  }
);

// Response interceptor: handle common errors globally
apiClient.interceptors.response.use(
  (response) => response,
  (error: AxiosError) => {
    // Handle 401 Unauthorized - token expired or invalid
    if (error.response?.status === 401) {
      // Optionally redirect to login or clear auth
      console.warn('Unauthorized request - token may be expired');
    }
    return Promise.reject(error);
  }
);

/**
 * Standardized error handler for API calls
 * Converts axios errors to user-friendly error messages
 * 
 * Giải thích câu 7: "bằng cách kiểm tra nếu server có trả về hoặc không trả về error"
 * 
 * Function này xử lý 3 trường hợp lỗi:
 * 
 * 1. Server trả về error response (error.response):
 *    - Server đã nhận request và trả về status code lỗi (400, 401, 404, 500, etc.)
 *    - Có thể có error message trong response.data.error.message hoặc response.data.message
 *    - Ưu tiên lấy message từ response, fallback về statusText hoặc defaultMessage
 * 
 * 2. Request đã gửi nhưng không nhận được response (error.request):
 *    - Server không phản hồi (timeout, network error, server down)
 *    - Request đã được gửi đi nhưng không có response
 *    - Trả về message về lỗi kết nối mạng
 * 
 * 3. Unknown error:
 *    - Lỗi không phải từ axios (có thể là lỗi JavaScript khác)
 *    - Log error để debug và trả về defaultMessage
 * 
 * @param {unknown} error - Error object từ catch block (có thể là AxiosError hoặc Error khác)
 * @param {string} defaultMessage - Message mặc định nếu không thể extract error message
 * @returns {Error} - Error object với message đã được format
 * 
 * Ví dụ sử dụng:
 * try {
 *   await apiClient.get('/api/users');
 * } catch (error) {
 *   const errorMessage = handleApiError(error, 'Không thể tải danh sách users');
 *   showError(errorMessage.message);
 * }
 */
export const handleApiError = (error: unknown, defaultMessage: string): Error => {
  // Kiểm tra xem error có phải là AxiosError không
  if (axios.isAxiosError(error)) {
    // TRƯỜNG HỢP 1: Server đã trả về response với status code lỗi
    // Ví dụ: 400 Bad Request, 401 Unauthorized, 404 Not Found, 500 Internal Server Error
    if (error.response) {
      // Lấy response data và extract error message
      // Server có thể trả về error theo format:
      // - { error: { message: "..." } } (chuẩn của dự án)
      // - { message: "..." } (format khác)
      const responseData = error.response.data as { error?: { message?: string }; message?: string };
      
      // Ưu tiên lấy message từ response.data.error.message
      // Nếu không có, lấy từ response.data.message
      // Nếu vẫn không có, lấy từ error.response.statusText (ví dụ: "Bad Request")
      // Cuối cùng fallback về defaultMessage
      const errorMessage = 
        responseData?.error?.message || 
        responseData?.message || 
        error.response.statusText || 
        defaultMessage;
      
      return new Error(errorMessage);
    }
    
    // TRƯỜNG HỢP 2: Request đã được gửi nhưng không nhận được response
    // Ví dụ: Server down, network timeout, CORS error, DNS error
    if (error.request) {
      return new Error('Không thể kết nối đến server. Vui lòng kiểm tra kết nối mạng.');
    }
  }
  
  // TRƯỜNG HỢP 3: Unknown error (không phải AxiosError)
  // Ví dụ: Lỗi JavaScript khác, lỗi trong quá trình xử lý
  console.error('API Error:', error);
  return new Error(defaultMessage || 'Đã xảy ra lỗi không xác định. Vui lòng thử lại.');
};

/**
 * Helper to handle FormData requests (for file uploads)
 * Automatically removes Content-Type header to let browser set it with boundary
 */
export const createFormDataConfig = (config?: AxiosRequestConfig): AxiosRequestConfig => {
  return {
    ...config,
    headers: {
      ...config?.headers,
      // Don't set Content-Type for FormData - browser will set it with boundary
    },
  };
};

