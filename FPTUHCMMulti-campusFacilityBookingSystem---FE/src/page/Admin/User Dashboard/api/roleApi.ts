import { apiClient, handleApiError } from '../../../../services/apiClient';
import type { ActionResponse } from '../../../../types/api';

// Interface cho Role từ API response
export interface Role {
  roleId: string;
  roleName: string;
  createdAt: string; // ISO 8601 format
  updatedAt: string; // ISO 8601 format
}

// Interface cho API Response
export type RoleResponse = ActionResponse<Role>;

/**
 * Lấy chi tiết role
 * @param roleId - ID của role (VD: RL0001, RL0002, RL0003)
 * @returns Promise với role detail
 * @description
 * - GET /api/roles/{id}
 * - Công khai - Không cần đăng nhập
 */
export const getRoleById = async (roleId: string): Promise<RoleResponse> => {
  try {
    const response = await apiClient.get<RoleResponse>(`/api/roles/${roleId}`);
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy chi tiết role');
  }
};

