import { apiClient, handleApiError } from '../../../../services/apiClient';
import type { PaginatedResponse, ActionResponse, DeleteResponse } from '../../../../types/api';

// Interface cho User từ API response
export interface User {
  userId: string;
  email: string;
  fullName: string;
  phoneNumber: string | null;
  userName: string;
  roleId: string;
  roleName: string;
  status: 'Active' | 'Inactive';
  isVerify: string; // Có thể là boolean hoặc string
  avatarUrl: string | null;
  lastLogin: string | null; // ISO 8601 format
  createdAt: string; // ISO 8601 format
  updatedAt: string; // ISO 8601 format
  campusId?: string; // Có thể có hoặc không
  campusName?: string; // Có thể có hoặc không
}

// Interface cho API Response với pagination
export type UsersResponse = PaginatedResponse<User>;

// Interface cho User Detail Response
export type UserDetailResponse = ActionResponse<User>;

// Interface cho Delete Response
export interface DeleteUserResponse extends DeleteResponse {
  error: {
    code: number;
    message: string;
  } | null;
  message?: string;
}

// Query parameters cho getUsers
export interface GetUsersParams {
  name?: string;
  email?: string;
  roleId?: string;
  campusId?: string;
  status?: 'Active' | 'Inactive';
  page?: number;
  limit?: number;
}

/**
 * Lấy danh sách tất cả users với filtering (Admin only)
 * @param params - Query parameters: name, email, roleId, campusId, status, page, limit
 * @returns Promise với danh sách users và pagination info
 * @description
 * - GET /api/users
 * - Chỉ Facility_Admin (RL0003) có thể truy cập
 */
export const getUsers = async (params?: GetUsersParams): Promise<UsersResponse> => {
  try {
    const queryParams: Record<string, string | number> = {};
    
    if (params?.name) {
      queryParams.name = params.name;
    }
    
    if (params?.email) {
      queryParams.email = params.email;
    }
    
    if (params?.roleId) {
      queryParams.roleId = params.roleId;
    }
    
    if (params?.campusId) {
      queryParams.campusId = params.campusId;
    }
    
    if (params?.status) {
      queryParams.status = params.status;
    }
    
    if (params?.page !== undefined) {
      queryParams.page = params.page;
    }
    
    if (params?.limit !== undefined) {
      queryParams.limit = params.limit;
    }

    const response = await apiClient.get<UsersResponse>('/api/users', {
      params: queryParams,
    });

    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy danh sách users');
  }
};

/**
 * Lấy chi tiết user (Admin only)
 * @param userId - ID của user
 * @returns Promise với user detail
 * @description
 * - GET /api/users/{id}
 * - Chỉ Facility_Admin (RL0003) có thể truy cập
 */
export const getUserById = async (userId: string): Promise<UserDetailResponse> => {
  try {
    const response = await apiClient.get<UserDetailResponse>(`/api/users/${userId}`);
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy chi tiết user');
  }
};

/**
 * Xóa user (Admin only)
 * @param userId - ID của user cần xóa
 * @returns Promise với kết quả
 * @description
 * - DELETE /api/users/{id}
 * - Chỉ Facility_Admin (RL0003) có thể xóa
 */
export const deleteUser = async (userId: string): Promise<DeleteUserResponse> => {
  try {
    const response = await apiClient.delete<DeleteUserResponse>(`/api/users/${userId}`);
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi xóa user');
  }
};

