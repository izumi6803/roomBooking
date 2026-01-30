import { apiClient, handleApiError, createFormDataConfig } from '../../../../services/apiClient';
import type { ApiResponse, ActionResponse } from '../../../../types/api';

// Interface cho User Profile từ API response
export interface UserProfile {
  userId: string;
  email: string;
  fullName: string;
  phoneNumber: string | null;
  studentId: string | null;
  userName: string;
  roleId: string;
  roleName: string;
  status: 'Active' | 'Inactive';
  isVerify: string;
  avatarUrl: string | null;
  lastLogin: string | null; // ISO 8601 format
  createdAt: string; // ISO 8601 format
  updatedAt: string; // ISO 8601 format
}

// Interface cho API Response
export type ProfileResponse = ActionResponse<UserProfile>;

// Interface cho Update Profile Request
export interface UpdateProfileRequest {
  phoneNumber?: string;
  avatarUrl?: string;
  studentId?: string;
}

// Interface cho Update Profile với Avatar Upload
export interface UpdateProfileWithAvatarRequest {
  phoneNumber?: string;
  studentId?: string;
  avatar?: File;
}

// Interface cho Change Password Request
export interface ChangePasswordRequest {
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
}

// Interface cho Change Password Response
export interface ChangePasswordResponse extends ApiResponse {
  message?: string;
}

/**
 * Lấy thông tin profile của user hiện tại
 * @returns Promise với user profile
 * @description
 * - GET /api/users/profile
 * - Tất cả user đã đăng nhập có thể truy cập
 * - User ID lấy từ JWT token
 */
export const getProfile = async (): Promise<ProfileResponse> => {
  try {
    const response = await apiClient.get<ProfileResponse>('/api/users/profile');
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy thông tin profile');
  }
};

/**
 * Cập nhật thông tin profile
 * @param profileData - Thông tin cần cập nhật (phoneNumber, avatarUrl, studentId)
 * @returns Promise với profile đã được cập nhật
 * @description
 * - PUT /api/users/profile
 * - Tất cả user đã đăng nhập có thể truy cập
 * - User chỉ có thể cập nhật profile của chính mình
 */
export const updateProfile = async (profileData: UpdateProfileRequest): Promise<ProfileResponse> => {
  try {
    const response = await apiClient.put<ProfileResponse>('/api/users/profile', profileData);
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi cập nhật profile');
  }
};

/**
 * Cập nhật thông tin profile với upload avatar trực tiếp
 * @param profileData - Thông tin cần cập nhật (phoneNumber, studentId, avatar file)
 * @returns Promise với profile đã được cập nhật
 * @description
 * - PUT /api/users/profile/upload (multipart/form-data)
 * - Upload avatar trực tiếp lên Cloudinary
 */
export const updateProfileWithAvatar = async (profileData: UpdateProfileWithAvatarRequest): Promise<ProfileResponse> => {
  try {
    const formData = new FormData();
    
    if (profileData.phoneNumber) {
      formData.append('phoneNumber', profileData.phoneNumber);
    }
    if (profileData.studentId) {
      formData.append('studentId', profileData.studentId);
    }
    if (profileData.avatar) {
      formData.append('avatar', profileData.avatar);
    }

    const response = await apiClient.put<ProfileResponse>(
      '/api/users/profile/upload',
      formData,
      createFormDataConfig()
    );

    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi cập nhật profile');
  }
};

/**
 * Đổi mật khẩu
 * @param passwordData - Mật khẩu cũ và mới
 * @returns Promise với kết quả
 * @description
 * - PUT /api/users/change-password
 * - Tất cả user đã đăng nhập có thể đổi mật khẩu
 * - Chỉ áp dụng cho user đăng nhập bằng email/password (không áp dụng cho Google login)
 */
export const changePassword = async (passwordData: ChangePasswordRequest): Promise<ChangePasswordResponse> => {
  try {
    const response = await apiClient.put<ChangePasswordResponse>('/api/users/change-password', {
      oldPassword: passwordData.oldPassword,
      newPassword: passwordData.newPassword,
      confirmPassword: passwordData.confirmPassword,
    });

    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi đổi mật khẩu');
  }
};

