import { apiClient, handleApiError } from '../../../../services/apiClient';
import type { PaginatedResponse, ActionResponse, DeleteResponse } from '../../../../types/api';

// Interface cho Campus từ API response
export interface Campus {
  campusId: string;
  name: string;
  address: string;
  phoneNumber: string;
  email: string;
  status: 'Active' | 'Inactive';
  createdAt: string; // ISO 8601 format
  updatedAt: string; // ISO 8601 format
}

// Interface cho Create/Update Campus Request
export interface CampusRequest {
  name: string;
  address: string;
  phoneNumber: string;
  email: string;
  status: 'Active' | 'Inactive';
}

// Interface cho API Response với pagination
export interface CampusesResponse extends PaginatedResponse<Campus> {
  code: number;
  message: string;
}

// Interface cho Create/Update Campus Response
export interface CampusActionResponse extends ActionResponse<Campus> {
  code?: number;
  message?: string;
}

// Interface cho Delete Response
export type DeleteCampusResponse = DeleteResponse;

// Query parameters cho getCampuses
export interface GetCampusesParams {
  page?: number;
  limit?: number;
}

/**
 * Lấy danh sách campuses với pagination (Admin view)
 * 
 * Giải thích câu 12: "thông qua backend, front-end chỉ gọi api"
 * 
 * FRONTEND CHỈ GỌI API - KHÔNG CÓ BUSINESS LOGIC:
 * 
 * Function này CHỈ làm nhiệm vụ:
 * 1. Format query parameters (page, limit)
 * 2. Gửi HTTP GET request đến backend
 * 3. Nhận response từ backend
 * 4. Trả về data (pass-through)
 * 
 * KHÔNG làm:
 * - ❌ Query database
 * - ❌ Apply pagination logic (skip, take)
 * - ❌ Filter, sort data
 * - ❌ Check permissions (backend làm)
 * - ❌ Format response
 * 
 * BACKEND XỬ LÝ TẤT CẢ LOGIC:
 * 
 * Khi backend nhận GET /api/campuses/paged?Page=1&Limit=10:
 * 1. Verify authentication:
 *    - Extract token từ Authorization header
 *    - Verify JWT token signature và expiration
 *    - Extract userId và roleId từ token
 * 
 * 2. Check permissions:
 *    - Verify user có quyền truy cập endpoint này không
 *    - Role-based access control (RBAC)
 * 
 * 3. Query database:
 *    - SELECT * FROM Campuses 
 *    - WHERE Status = 'Active' (hoặc filter khác)
 *    - ORDER BY CreatedAt DESC
 *    - LIMIT 10 OFFSET 0 (pagination)
 *    - COUNT(*) để tính total items
 * 
 * 4. Format response:
 *    - Map database entities sang DTOs
 *    - Format dates, status, etc.
 *    - Tạo pagination object: { page, limit, total }
 *    - Wrap trong ApiResponse: { success, data, pagination }
 * 
 * 5. Trả về HTTP response:
 *    - Status 200 với JSON body
 *    - Hoặc status 401/403/500 với error message
 * 
 * FLOW HOẠT ĐỘNG:
 * 1. Frontend: Gọi getCampuses({ page: 1, limit: 10 })
 * 2. Frontend: Format query params: { Page: 1, Limit: 10 }
 * 3. Frontend: Gửi GET /api/campuses/paged?Page=1&Limit=10 với Authorization header
 * 4. Backend: Nhận request, verify token, check permissions
 * 5. Backend: Query database với pagination
 * 6. Backend: Format response: { success: true, data: [...], pagination: {...} }
 * 7. Frontend: Nhận response, hiển thị data lên UI
 * 
 * @param params - Query parameters: page, limit
 * @returns Promise với danh sách campuses và pagination info
 * @description
 * - GET /api/campuses/paged
 * - Có pagination (page, limit)
 * - Tất cả user đã đăng nhập có thể truy cập (backend verify)
 */
export const getCampuses = async (params?: GetCampusesParams): Promise<CampusesResponse> => {
  try {
    // FRONTEND: Chỉ format query parameters
    // Không có logic phức tạp, chỉ chuẩn bị data để gửi
    const queryParams: Record<string, number> = {};
    
    if (params?.page !== undefined) {
      queryParams.Page = params.page; // Backend expect "Page" (capital P)
    }
    
    if (params?.limit !== undefined) {
      queryParams.Limit = params.limit; // Backend expect "Limit" (capital L)
    }

    // FRONTEND: Gửi HTTP GET request đến backend
    // apiClient tự động thêm Authorization header (từ interceptor)
    // Backend sẽ xử lý tất cả logic: verify token, query DB, pagination, format response
    const response = await apiClient.get<CampusesResponse>('/api/campuses/paged', {
      params: queryParams,
    });

    // FRONTEND: Chỉ trả về data từ response
    // Không xử lý, không transform, chỉ pass through
    return response.data;
  } catch (error) {
    // FRONTEND: Chỉ xử lý error để hiển thị message cho user
    // Logic xử lý error thực sự đã được backend làm (trả về error response)
    throw handleApiError(error, 'Lỗi khi lấy danh sách campuses');
  }
};

/**
 * Tạo campus mới
 * @param campusData - Thông tin campus cần tạo
 * @returns Promise với campus đã được tạo
 * @description
 * - POST /api/campuses
 * - Chỉ Facility_Admin (RL0003) có thể tạo
 */
export const createCampus = async (campusData: CampusRequest): Promise<CampusActionResponse> => {
  try {
    const response = await apiClient.post<CampusActionResponse>('/api/campuses', campusData);
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi tạo campus');
  }
};

/**
 * Cập nhật thông tin campus
 * @param campusId - ID của campus cần cập nhật
 * @param campusData - Thông tin campus cần cập nhật
 * @param status - Trạng thái campus (optional query parameter)
 * @returns Promise với campus đã được cập nhật
 * @description
 * - PUT /api/campuses/{id}
 * - Chỉ Facility_Admin (RL0003) có thể cập nhật
 * - Có thể cập nhật status qua query parameter
 */
export const updateCampus = async (
  campusId: string,
  campusData: CampusRequest,
  status?: 'Active' | 'Inactive'
): Promise<CampusActionResponse> => {
  try {
    const queryParams: Record<string, string> = {};
    if (status) {
      queryParams.status = status;
    }

    const response = await apiClient.put<CampusActionResponse>(
      `/api/campuses/${campusId}`,
      campusData,
      { params: queryParams }
    );

    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi cập nhật campus');
  }
};

/**
 * Vô hiệu hóa campus (soft delete - set status = Inactive)
 * @param campusId - ID của campus cần vô hiệu hóa
 * @returns Promise với kết quả
 * @description
 * - DELETE /api/campuses/{id}
 * - Chỉ Facility_Admin (RL0003) có thể vô hiệu hóa
 * - Soft delete: chỉ set status = Inactive, không xóa dữ liệu
 */
export const deleteCampus = async (campusId: string): Promise<DeleteCampusResponse> => {
  try {
    const response = await apiClient.delete<DeleteCampusResponse>(`/api/campuses/${campusId}`);
    return response.data;
  } catch (error) {
    throw handleApiError(error, 'Lỗi khi vô hiệu hóa campus');
  }
};

