import { apiClient, handleApiError } from '../../../../services/apiClient';
import type { PaginatedResponse, ActionResponse } from '../../../../types/api';

// Interface cho FacilityType từ API response
export interface FacilityType {
  typeId: string;
  name: string;
  description: string;
  defaultAmenities: string;
  defaultCapacity: number;
  typicalDurationHours: number;
  iconUrl: string;
  createdAt: string; // ISO 8601 format
  updatedAt: string; // ISO 8601 format
}

// Interface cho Create/Update FacilityType Request
export interface FacilityTypeRequest {
  name: string;
  description: string;
  defaultAmenities: string;
  defaultCapacity: number;
  typicalDurationHours: number;
  iconUrl: string;
}

// Interface cho API Response với pagination
export interface FacilityTypesResponse extends PaginatedResponse<FacilityType> {
  code?: number;
  message?: string;
}

// Interface cho Create/Update FacilityType Response
export type FacilityTypeActionResponse = ActionResponse<FacilityType>;

// Query parameters cho getFacilityTypes
export interface GetFacilityTypesParams {
  page?: number;
  limit?: number;
}

/**
 * Lấy danh sách tất cả loại cơ sở vật chất
 * @param params - Query parameters: page, limit
 * @returns Promise với danh sách facility types và pagination info
 * @description
 * - GET /api/facility-types
 * - Công khai - Không cần đăng nhập
 */
export const getFacilityTypes = async (params?: GetFacilityTypesParams): Promise<FacilityTypesResponse> => {
  try {
    const queryParams: Record<string, number> = {};
    
    if (params?.page !== undefined) {
      queryParams.Page = params.page;
    }
    
    if (params?.limit !== undefined) {
      queryParams.Limit = params.limit;
    }

    const response = await apiClient.get<FacilityTypesResponse>('/api/facility-types', {
      params: queryParams,
    });

    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy danh sách facility types');
  }
};

/**
 * Lấy chi tiết loại cơ sở vật chất
 * @param typeId - ID của facility type
 * @returns Promise với facility type
 * @description
 * - GET /api/facility-types/{id}
 * - Công khai - Không cần đăng nhập
 */
export const getFacilityTypeById = async (typeId: string): Promise<FacilityTypeActionResponse> => {
  try {
    const response = await apiClient.get<FacilityTypeActionResponse>(`/api/facility-types/${typeId}`);
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi lấy chi tiết facility type');
  }
};

/**
 * Tạo loại cơ sở vật chất mới
 * @param facilityTypeData - Thông tin facility type cần tạo
 * @returns Promise với facility type đã được tạo
 * @description
 * - POST /api/facility-types
 * - Chỉ Facility_Admin (RL0003) có thể tạo
 */
export const createFacilityType = async (facilityTypeData: FacilityTypeRequest): Promise<FacilityTypeActionResponse> => {
  try {
    const response = await apiClient.post<FacilityTypeActionResponse>('/api/facility-types', facilityTypeData);
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi tạo facility type');
  }
};

/**
 * Cập nhật thông tin loại cơ sở vật chất
 * @param typeId - ID của facility type cần cập nhật
 * @param facilityTypeData - Thông tin facility type cần cập nhật
 * @returns Promise với facility type đã được cập nhật
 * @description
 * - PUT /api/facility-types/{id}
 * - Chỉ Facility_Admin (RL0003) có thể cập nhật
 */
export const updateFacilityType = async (
  typeId: string,
  facilityTypeData: FacilityTypeRequest
): Promise<FacilityTypeActionResponse> => {
  try {
    const response = await apiClient.put<FacilityTypeActionResponse>(
      `/api/facility-types/${typeId}`,
      facilityTypeData
    );
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi cập nhật facility type');
  }
};

