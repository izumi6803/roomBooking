import axios from 'axios';
import { publicApiClient, apiClient, handleApiError } from '../../../services/apiClient';

// Interface cho request body
export interface LoginRequest {
  emailOrUsername: string;
  password: string;
}

// Interface cho response từ API
export interface LoginResponse {
  success: boolean;
  error: {
    code: number;
    message: string;
  } | null;
  data: {
    token: string;
    userId: string;
    email: string;
    fullName: string;
    roleId: string;
    isVerified: boolean;
  } | null;
}

// Interface cho user info được lưu trong localStorage
export interface AuthUser {
  token: string;
  userId: string;
  email: string;
  fullName: string;
  roleId: string;
  isVerified: boolean;
}

// Interface cho logout response
export interface LogoutResponse {
  success: boolean;
  error: {
    code: number;
    message: string;
  } | null;
}

/**
 * Gọi API đăng nhập bằng email/password
 * 
 * Giải thích câu 10: "người dùng nhập username, password rồi nhấn login hoặc nhấp vào login google, 
 * token được lưu lại ở sessionStorage và được sử dụng để authentication"
 * 
 * FLOW ĐĂNG NHẬP BẰNG EMAIL/PASSWORD:
 * 
 * 1. User nhập email/username và password → nhấn nút "Đăng nhập"
 * 2. Function này được gọi với emailOrUsername và password
 * 3. Gửi POST request đến /api/auth/login với credentials
 * 4. Server verify credentials và trả về:
 *    - success: true/false
 *    - data: { token, userId, email, fullName, roleId, isVerified }
 *    - error: null hoặc error object
 * 
 * 5. Nếu thành công (success = true và có data):
 *    - Extract token từ result.data.token
 *    - Tạo AuthUser object chứa thông tin user
 *    - Lưu vào sessionStorage:
 *      * 'auth_token': JWT token để authenticate các request sau
 *      * 'auth_user': JSON string của AuthUser object (chứa userId, email, fullName, roleId)
 *      * Xóa 'is_google_login' flag (vì đăng nhập bằng email/password)
 * 
 * 6. Token trong sessionStorage sẽ được sử dụng bởi:
 *    - apiClient interceptor: Tự động thêm vào Authorization header mỗi request
 *    - getToken() function: Lấy token để verify authentication
 *    - getCurrentUser() function: Lấy thông tin user hiện tại
 * 
 * TẠI SAO DÙNG sessionStorage THAY VÌ localStorage?
 * - sessionStorage: Chỉ tồn tại trong tab hiện tại, tự động xóa khi đóng tab
 * - localStorage: Tồn tại vĩnh viễn, phải xóa thủ công
 * - Security: sessionStorage an toàn hơn (tự động clear khi đóng tab)
 * - Multi-tab: Mỗi tab có session riêng (có thể đăng nhập khác account ở tab khác)
 * 
 * @param emailOrUsername - Email hoặc username của người dùng
 * @param password - Mật khẩu
 * @returns Promise với kết quả đăng nhập và AuthUser object nếu thành công
 */
export const loginAPI = async (
  emailOrUsername: string,
  password: string
): Promise<{ success: boolean; message: string; data?: AuthUser }> => {
  try {
    // Gửi POST request đến /api/auth/login
    // Sử dụng publicApiClient vì đây là endpoint không cần authentication
    const response = await publicApiClient.post<LoginResponse>('/api/auth/login', {
      emailOrUsername: emailOrUsername.trim(), // Trim whitespace
      password: password,
    } as LoginRequest);

    const result = response.data;

    // Xử lý response thành công (HTTP 200 và success = true)
    if (result.success && result.data) {
      // Tạo AuthUser object từ response data
      const authUser: AuthUser = {
        token: result.data.token,           // JWT token từ server
        userId: result.data.userId,         // User ID
        email: result.data.email,           // Email
        fullName: result.data.fullName,     // Tên đầy đủ
        roleId: result.data.roleId,         // Role ID (RL0001, RL0002, RL0003)
        isVerified: result.data.isVerified, // Email đã verify chưa
      };

      // LƯU TOKEN VÀ USER INFO VÀO sessionStorage
      // Đây là bước quan trọng để lưu authentication state
      sessionStorage.setItem('auth_token', authUser.token);  // Lưu JWT token
      sessionStorage.setItem('auth_user', JSON.stringify(authUser)); // Lưu user info dạng JSON string
      
      // Xóa flag Google login nếu có (user đăng nhập bằng email/password, không phải Google)
      sessionStorage.removeItem('is_google_login');

      return {
        success: true,
        message: 'Đăng nhập thành công!',
        data: authUser,
      };
    }

    // Xử lý lỗi từ API (khi success = false nhưng status 200)
    if (result.error) {
      return {
        success: false,
        message: result.error.message || 'Đăng nhập thất bại',
      };
    }

    // Trường hợp không có error object nhưng success = false
    return {
      success: false,
      message: 'Đăng nhập thất bại. Vui lòng thử lại.',
    };
  } catch (error) {
    const errorMessage = handleApiError(error, 'Đăng nhập thất bại');
    return {
      success: false,
      message: errorMessage.message,
    };
  }
};

/**
 * Gọi API đăng xuất
 * @returns Promise với kết quả đăng xuất
 * @description 
 * - Gửi POST request đến /api/auth/logout với Authorization header chứa JWT token
 * - Sau khi nhận 200 OK, xóa token và user info khỏi localStorage
 * - API yêu cầu authentication (token phải có trong header)
 */
export const logoutAPI = async (): Promise<{ success: boolean; message: string }> => {
  const clearAuthData = () => {
    const wasGoogleLogin = sessionStorage.getItem('is_google_login') === 'true';
    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('auth_user');
    sessionStorage.removeItem('is_google_login');
    
    // Revoke Google session nếu user đăng nhập bằng Google
    if (wasGoogleLogin && typeof window.google !== 'undefined' && window.google.accounts) {
      try {
        window.google.accounts.id.disableAutoSelect();
      } catch (error) {
        console.warn('Error revoking Google session:', error);
      }
    }
  };

  try {
    const response = await apiClient.post<LogoutResponse>('/api/auth/logout');
    const result = response.data;

    // Xử lý response thành công (200)
    if (result.success) {
      clearAuthData();
      return {
        success: true,
        message: 'Đăng xuất thành công!',
      };
    }

    // Xử lý trường hợp success = false
    if (result.error) {
      return {
        success: false,
        message: result.error.message || 'Đăng xuất thất bại',
      };
    }

    return {
      success: false,
      message: 'Đăng xuất thất bại. Vui lòng thử lại.',
    };
  } catch (error) {
    // Xử lý lỗi từ axios
    if (axios.isAxiosError(error)) {
      // Nếu là lỗi 401 (Unauthorized), có thể token đã hết hạn
      // Vẫn xóa token và user info để đảm bảo client side clean
      if (error.response?.status === 401) {
        clearAuthData();
        return {
          success: false,
          message: 'Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.',
        };
      }
    }

    // Ngay cả khi không kết nối được, vẫn xóa token ở client side
    clearAuthData();
    const errorMessage = handleApiError(error, 'Đăng xuất thất bại');
    return {
      success: false,
      message: errorMessage.message || 'Đã xóa thông tin đăng nhập ở client.',
    };
  }
};
