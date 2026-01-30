import type { AuthUser } from './loginAPI';
import { publicApiClient, handleApiError } from '../../../services/apiClient';
import type { ApiResponse } from '../../../types/api';

// Interface cho Google login request
export interface GoogleLoginRequest {
  idToken: string;
}

// Interface cho Google login data
export interface GoogleLoginData {
  token: string;
  userId: string;
  email: string;
  fullName: string;
  roleId: string;
  isVerified: boolean;
}

// Interface cho Google login response
export interface GoogleLoginResponse extends ApiResponse<GoogleLoginData> {
  data: GoogleLoginData;
}

// Interface cho verify email response
export interface VerifyEmailResponse extends ApiResponse {
  success: boolean;
}

// Interface cho resend verification email response
export interface ResendVerificationResponse extends ApiResponse {
  success: boolean;
}

/**
 * Đăng nhập bằng Google OAuth
 * 
 * Giải thích câu 12: "thông qua backend, front-end chỉ gọi api"
 * Giải thích: Google OAuth được tích hợp như thế nào? Token được xử lý ra sao?
 * 
 * FRONTEND CHỈ GỌI API - KHÔNG CÓ BUSINESS LOGIC:
 * 
 * Function này CHỈ làm nhiệm vụ:
 * 1. Format request body với idToken
 * 2. Gửi HTTP POST request đến backend
 * 3. Nhận response từ backend
 * 4. Lưu token vào sessionStorage (nếu thành công)
 * 
 * KHÔNG làm:
 * - ❌ Verify Google token (không có secret key)
 * - ❌ Query database
 * - ❌ Tạo user
 * - ❌ Phân role
 * - ❌ Tạo JWT token
 * 
 * BACKEND XỬ LÝ TẤT CẢ LOGIC:
 * 
 * Khi backend nhận POST /api/auth/login/google với idToken:
 * 1. Verify idToken với Google:
 *    - Sử dụng Google ClientID và secret
 *    - Verify signature, expiration, audience
 *    - Extract user info từ token (email, name, picture)
 * 
 * 2. Check database:
 *    - Query: SELECT * FROM Users WHERE Email = @email
 *    - Nếu chưa tồn tại: Tạo user mới, gửi mã verify email
 *    - Nếu đã tồn tại: Lấy thông tin user
 * 
 * 3. Phân role dựa trên email domain:
 *    - @fpt.edu.vn → Student (RL0001)
 *    - @fe.edu.vn → Lecturer (RL0002)
 *    - Logic này ở backend, frontend không biết
 * 
 * 4. Tạo JWT token riêng của hệ thống:
 *    - Token này KHÁC với Google idToken
 *    - Chứa userId, roleId, và các claims khác
 *    - Có expiration time do hệ thống kiểm soát
 * 
 * 5. Trả về response:
 *    - { success: true, data: { token, userId, email, ... } }
 *    - Token này là JWT token của hệ thống, không phải Google token
 * 
 * FLOW ĐĂNG NHẬP BẰNG GOOGLE OAUTH:
 * 
 * 1. User nhấp vào nút "Đăng nhập bằng Google"
 * 2. Google OAuth popup hiển thị, user chọn account và authorize
 * 3. Google trả về idToken (JWT token từ Google) → Frontend nhận
 * 4. Frontend gọi function này với idToken
 * 5. Frontend gửi POST /api/auth/login/google với idToken
 * 6. Backend verify idToken với Google, tạo/lấy user, và trả về:
 *    - success: true/false
 *    - data: { token, userId, email, fullName, roleId, isVerified }
 *    - error: null hoặc error object
 * 
 * 7. Nếu thành công (success = true và có data):
 *    a. Nếu user CHƯA verify email (isVerified = false):
 *       - KHÔNG lưu token vào sessionStorage
 *       - Trả về needsVerification: true
 *       - Hiển thị form verify email với mã 6 số
 *       - User nhập mã → verifyEmail() → sau đó mới lưu token
 * 
 *    b. Nếu user ĐÃ verify email (isVerified = true):
 *       - Extract token từ result.data.token (JWT token từ backend)
 *       - Tạo AuthUser object
 *       - Lưu vào sessionStorage:
 *         * 'auth_token': JWT token từ server (KHÔNG phải Google token)
 *         * 'auth_user': JSON string của AuthUser
 *         * 'is_google_login': 'true' (flag để biết đăng nhập bằng Google)
 * 
 * 8. Token trong sessionStorage được sử dụng giống như email/password login:
 *    - apiClient interceptor tự động thêm vào Authorization header
 *    - getToken() lấy token để verify
 *    - getCurrentUser() lấy thông tin user
 * 
 * TẠI SAO KHÔNG DÙNG GOOGLE TOKEN TRỰC TIẾP?
 * - Google idToken chỉ dùng để verify identity với Google (một lần)
 * - Hệ thống cần token riêng để:
 *   * Kiểm soát expiration time (có thể set 1 ngày, 1 tuần, etc.)
 *   * Thêm custom claims (role, permissions, campusId)
 *   * Revoke token khi cần (logout, ban user)
 *   * Không phụ thuộc vào Google (nếu Google thay đổi policy)
 * 
 * @param idToken - Google ID Token từ Google OAuth (JWT token từ Google)
 * @returns Promise với kết quả đăng nhập và thông tin user
 * @description
 * - Gửi POST request đến /api/auth/login/google với idToken
 * - Nếu là lần đầu login, hệ thống tự động tạo user mới và gửi mã xác thực qua email
 * - Logic phân role: @fpt.edu.vn → Student, @fe.edu.vn → Lecturer (xử lý ở backend)
 */
export const loginWithGoogle = async (
  idToken: string
): Promise<{ 
  success: boolean; 
  message: string; 
  data?: AuthUser;
  needsVerification?: boolean;
  email?: string;
}> => {
  try {
    // FRONTEND: Chỉ format request và gửi HTTP POST
    // Không có business logic ở đây
    // Backend sẽ verify idToken với Google, tạo/lấy user, tạo JWT token
    const response = await publicApiClient.post<GoogleLoginResponse>('/api/auth/login/google', {
      idToken: idToken, // Google idToken - backend sẽ verify
    } as GoogleLoginRequest);

    const result = response.data;

    // Xử lý response thành công (HTTP 200 và success = true)
    if (result.success && result.data) {
      // Tạo AuthUser object từ response data
      const authUser: AuthUser = {
        token: result.data.token,           // JWT token từ server (KHÔNG phải Google token)
        userId: result.data.userId,         // User ID
        email: result.data.email,          // Email từ Google account
        fullName: result.data.fullName,    // Tên từ Google account
        roleId: result.data.roleId,        // Role ID (phân theo email domain)
        isVerified: result.data.isVerified, // Email đã verify chưa
      };

      // TRƯỜNG HỢP 1: User chưa verify email
      // Không lưu token vào sessionStorage vì chưa được verify
      // Hiển thị form verify email với mã 6 số
      if (!result.data.isVerified) {
        return {
          success: true,
          message: 'Vui lòng xác thực email để hoàn tất đăng nhập. Mã xác thực đã được gửi đến email của bạn.',
          needsVerification: true,  // Flag để UI hiển thị form verify
          email: result.data.email,  // Email để hiển thị trong form verify
        };
      }

      // TRƯỜNG HỢP 2: User đã verify email
      // Lưu token và user info vào sessionStorage (giống email/password login)
      sessionStorage.setItem('auth_token', authUser.token);  // Lưu JWT token từ server
      sessionStorage.setItem('auth_user', JSON.stringify(authUser)); // Lưu user info
      
      // Lưu flag để biết user đăng nhập bằng Google
      // Flag này được dùng khi logout để revoke Google session
      sessionStorage.setItem('is_google_login', 'true');

      return {
        success: true,
        message: 'Đăng nhập thành công!',
        data: authUser,
        needsVerification: false,
      };
    }

    // Xử lý lỗi từ API
    if (result.error) {
      return {
        success: false,
        message: result.error.message || 'Đăng nhập thất bại',
      };
    }

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
 * Xác thực email bằng mã 6 số
 * @param email - Email cần xác thực
 * @param code - Mã xác thực 6 số nhận qua email
 * @returns Promise với kết quả xác thực
 * @description
 * - Gửi POST request đến /api/auth/verify-email với email và code trong query params
 * - Mã có hiệu lực 24 giờ
 */
export const verifyEmail = async (
  email: string,
  code: string
): Promise<{ success: boolean; message: string }> => {
  try {
    const response = await publicApiClient.post<VerifyEmailResponse>(
      `/api/auth/verify-email?email=${encodeURIComponent(email)}&code=${encodeURIComponent(code)}`
    );

    const result = response.data;

    if (result.success) {
      return {
        success: true,
        message: 'Xác thực email thành công!',
      };
    }

    if (result.error) {
      return {
        success: false,
        message: result.error.message || 'Xác thực email thất bại',
      };
    }

    return {
      success: false,
      message: 'Xác thực email thất bại. Vui lòng thử lại.',
    };
  } catch (error) {
    const errorMessage = handleApiError(error, 'Xác thực email thất bại');
    return {
      success: false,
      message: errorMessage.message,
    };
  }
};

/**
 * Gửi lại mã xác thực email
 * @param email - Email cần gửi lại mã
 * @returns Promise với kết quả gửi lại mã
 * @description
 * - Gửi POST request đến /api/auth/resend-verification-email với email trong query param
 * - Sử dụng khi mã cũ đã hết hạn (24h)
 */
export const resendVerificationEmail = async (
  email: string
): Promise<{ success: boolean; message: string }> => {
  try {
    const response = await publicApiClient.post<ResendVerificationResponse>(
      `/api/auth/resend-verification-email?email=${encodeURIComponent(email)}`
    );

    const result = response.data;

    if (result.success) {
      return {
        success: true,
        message: 'Mã xác thực mới đã được gửi đến email của bạn.',
      };
    }

    if (result.error) {
      return {
        success: false,
        message: result.error.message || 'Gửi lại mã thất bại',
      };
    }

    return {
      success: false,
      message: 'Gửi lại mã thất bại. Vui lòng thử lại.',
    };
  } catch (error) {
    const errorMessage = handleApiError(error, 'Gửi lại mã thất bại');
    return {
      success: false,
      message: errorMessage.message,
    };
  }
};

// Interface cho forgot password response
export interface ForgotPasswordResponse extends ApiResponse {
  success: boolean;
}

// Interface cho reset password response
export interface ResetPasswordResponse extends ApiResponse {
  success: boolean;
}

/**
 * Yêu cầu đặt lại mật khẩu - gửi mã 6 số qua email
 * @param email - Email tài khoản cần đặt lại mật khẩu
 * @returns Promise với kết quả gửi mã
 * @description
 * - Gửi POST request đến /api/auth/forgot-password với email trong query param
 * - Mã có hiệu lực 1 giờ
 * - Không áp dụng cho tài khoản đăng nhập Google
 */
export const forgotPassword = async (
  email: string
): Promise<{ success: boolean; message: string }> => {
  try {
    const response = await publicApiClient.post<ForgotPasswordResponse>(
      `/api/auth/forgot-password?email=${encodeURIComponent(email)}`
    );

    const result = response.data;

    if (result.success) {
      return {
        success: true,
        message: 'Mã đặt lại mật khẩu đã được gửi đến email của bạn. Mã có hiệu lực trong 1 giờ.',
      };
    }

    if (result.error) {
      return {
        success: false,
        message: result.error.message || 'Gửi mã thất bại',
      };
    }

    return {
      success: false,
      message: 'Gửi mã thất bại. Vui lòng thử lại.',
    };
  } catch (error) {
    const errorMessage = handleApiError(error, 'Gửi mã thất bại');
    return {
      success: false,
      message: errorMessage.message,
    };
  }
};

/**
 * Đặt lại mật khẩu bằng mã 6 số
 * @param email - Email tài khoản
 * @param code - Mã reset 6 số nhận qua email
 * @param newPassword - Mật khẩu mới
 * @returns Promise với kết quả đặt lại mật khẩu
 * @description
 * - Gửi POST request đến /api/auth/reset-password với email, code và newPassword trong query params
 * - Mã có hiệu lực 1 giờ
 */
export const resetPassword = async (
  email: string,
  code: string,
  newPassword: string
): Promise<{ success: boolean; message: string }> => {
  try {
    const response = await publicApiClient.post<ResetPasswordResponse>(
      `/api/auth/reset-password?email=${encodeURIComponent(email)}&code=${encodeURIComponent(code)}&newPassword=${encodeURIComponent(newPassword)}`
    );

    const result = response.data;

    if (result.success) {
      return {
        success: true,
        message: 'Đặt lại mật khẩu thành công! Bạn có thể đăng nhập với mật khẩu mới.',
      };
    }

    if (result.error) {
      return {
        success: false,
        message: result.error.message || 'Đặt lại mật khẩu thất bại',
      };
    }

    return {
      success: false,
      message: 'Đặt lại mật khẩu thất bại. Vui lòng thử lại.',
    };
  } catch (error) {
    const errorMessage = handleApiError(error, 'Đặt lại mật khẩu thất bại');
    return {
      success: false,
      message: errorMessage.message,
    };
  }
};

