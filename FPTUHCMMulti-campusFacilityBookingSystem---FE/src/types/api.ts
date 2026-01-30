/**
 * Shared API types and interfaces
 * Common response structures used across the application
 * 
 * File này định nghĩa các kiểu dữ liệu (types/interfaces) chung cho toàn bộ API responses
 * trong ứng dụng. Thay vì mỗi API file tự định nghĩa riêng, chúng ta tập trung ở đây
 * để đảm bảo tính nhất quán và dễ bảo trì.
 */

/**
 * Interface định nghĩa cấu trúc lỗi từ API
 * 
 * Khi API trả về lỗi, nó sẽ có dạng:
 * {
 *   code: 400,           // Mã lỗi HTTP (400, 401, 404, 500, etc.)
 *   message: "Email đã tồn tại"  // Thông báo lỗi dễ hiểu cho người dùng
 * }
 */
export interface ApiError {
  code: number;      // Mã lỗi (ví dụ: 400 Bad Request, 401 Unauthorized, 404 Not Found, 500 Internal Server Error)
  message: string;  // Thông báo lỗi mô tả chi tiết vấn đề (ví dụ: "Email đã tồn tại", "Token không hợp lệ")
}

/**
 * Interface định nghĩa cấu trúc response chuẩn từ API
 * 
 * Giải thích câu 8: "tại mỗi cái có response generic type khác nhau"
 * 
 * TẠI SAO CẦN NHIỀU INTERFACE KHÁC NHAU?
 * 
 * 1. ApiResponse<T> - Base interface cho tất cả responses:
 *    - Generic <T> cho phép chỉ định kiểu dữ liệu của field 'data'
 *    - Flexible: Có thể dùng cho bất kỳ loại data nào
 *    - Ví dụ: ApiResponse<User>, ApiResponse<Notification[]>, ApiResponse<string>
 * 
 * 2. PaginatedResponse<T> - Cho responses có phân trang:
 *    - Extends ApiResponse<T[]> → data phải là mảng
 *    - Thêm field pagination?: Pagination
 *    - Type-safe: TypeScript biết chắc data là mảng và có pagination info
 *    - Ví dụ: PaginatedResponse<User> → data: User[], pagination: Pagination
 * 
 * 3. ActionResponse<T> - Cho create/update operations:
 *    - Extends ApiResponse<T>
 *    - Đảm bảo data có kiểu T (object vừa được tạo/cập nhật)
 *    - Type-safe: TypeScript biết chính xác kiểu dữ liệu trong data
 *    - Ví dụ: ActionResponse<User> → data: User (user vừa được tạo)
 * 
 * 4. DeleteResponse - Cho delete operations:
 *    - Extends ApiResponse (không có generic vì delete không trả về data)
 *    - Rõ ràng về mục đích: Chỉ cần biết success/error
 *    - Type-safe: TypeScript biết không có data field
 * 
 * LỢI ÍCH CỦA GENERIC TYPES:
 * - Type Safety: TypeScript biết chính xác kiểu dữ liệu → ít bug hơn
 * - IntelliSense: IDE gợi ý đúng properties của data
 * - Refactoring: Dễ dàng thay đổi kiểu dữ liệu mà không sợ break code
 * - Documentation: Code tự document về kiểu dữ liệu
 * 
 * Đây là cấu trúc response cơ bản nhất mà tất cả API endpoints đều tuân theo.
 * 
 * Generic Type <T>: Cho phép chỉ định kiểu dữ liệu của field 'data'
 * - <T = unknown>: Nếu không chỉ định, mặc định là 'unknown' (kiểu không xác định)
 * 
 * Ví dụ sử dụng:
 * - ApiResponse<User> → data sẽ có kiểu User
 * - ApiResponse<Notification[]> → data sẽ là mảng các Notification
 * - ApiResponse → data sẽ là unknown (khi không chỉ định)
 * 
 * Cấu trúc response thành công:
 * {
 *   success: true,
 *   error: null,
 *   data: { ... },        // Dữ liệu trả về (có thể là object, array, hoặc bất kỳ kiểu nào)
 *   code: 200,            // Optional: Mã HTTP status
 *   message: "Thành công" // Optional: Thông báo
 * }
 * 
 * Cấu trúc response lỗi:
 * {
 *   success: false,
 *   error: {
 *     code: 400,
 *     message: "Email đã tồn tại"
 *   },
 *   data: undefined,      // Không có dữ liệu khi lỗi
 *   code: 400,
 *   message: "Bad Request"
 * }
 */
export interface ApiResponse<T = unknown> {
  success: boolean;           // true nếu request thành công, false nếu có lỗi
  error: ApiError | null;    // Thông tin lỗi (null nếu thành công, ApiError object nếu có lỗi)
  data?: T;                   // Dữ liệu trả về (optional - chỉ có khi success = true)
                              // Dấu ? nghĩa là field này có thể không có (optional)
  code?: number;             // Optional: Mã HTTP status code (200, 400, 401, 404, 500, etc.)
  message?: string;          // Optional: Thông báo tổng quan về kết quả
}

/**
 * Interface định nghĩa thông tin phân trang (pagination)
 * 
 * Khi API trả về danh sách có phân trang, cần thông tin này để:
 * - Biết đang ở trang nào
 * - Biết mỗi trang có bao nhiêu items
 * - Biết tổng số items để tính số trang
 * 
 * Ví dụ:
 * {
 *   page: 1,        // Trang hiện tại (bắt đầu từ 1)
 *   limit: 10,      // Số items mỗi trang
 *   total: 100      // Tổng số items (có thể tính số trang = Math.ceil(total / limit) = 10 trang)
 * }
 */
export interface Pagination {
  page: number;   // Số trang hiện tại (thường bắt đầu từ 1, không phải 0)
  limit: number;  // Số lượng items tối đa trên mỗi trang
  total: number; // Tổng số items trong toàn bộ dataset (không phải số trang)
}

/**
 * Interface định nghĩa API Response có kèm thông tin phân trang
 * 
 * Extends ApiResponse<T[]>: Kế thừa từ ApiResponse nhưng data phải là mảng (T[])
 * 
 * Generic Type <T>: Kiểu dữ liệu của mỗi phần tử trong mảng data
 * 
 * Ví dụ:
 * - PaginatedResponse<User> → data là User[], mỗi phần tử là User
 * - PaginatedResponse<Notification> → data là Notification[], mỗi phần tử là Notification
 * 
 * Cấu trúc response:
 * {
 *   success: true,
 *   error: null,
 *   data: [              // Mảng các items của trang hiện tại
 *     { id: 1, name: "Item 1" },
 *     { id: 2, name: "Item 2" },
 *     ...
 *   ],
 *   pagination: {        // Thông tin phân trang (optional)
 *     page: 1,
 *     limit: 10,
 *     total: 100
 *   }
 * }
 * 
 * Ứng dụng: Dùng cho các API trả về danh sách có phân trang như:
 * - GET /api/users?page=1&limit=10
 * - GET /api/notifications?page=2&limit=20
 */
export interface PaginatedResponse<T> extends ApiResponse<T[]> {
  pagination?: Pagination;  // Optional: Thông tin phân trang (chỉ có khi API hỗ trợ pagination)
}

/**
 * Interface định nghĩa response cho các thao tác tạo mới (create) hoặc cập nhật (update)
 * 
 * Extends ApiResponse<T>: Kế thừa tất cả properties từ ApiResponse
 * 
 * Generic Type <T>: Kiểu dữ liệu của object được tạo/cập nhật
 * 
 * Tại sao cần interface riêng này?
 * - Khi create/update, thường trả về object vừa được tạo/cập nhật
 * - Đảm bảo type safety: TypeScript biết chính xác kiểu dữ liệu trong data
 * 
 * Ví dụ khi tạo User mới:
 * {
 *   success: true,
 *   error: null,
 *   data: {              // User object vừa được tạo
 *     userId: "USR001",
 *     email: "user@example.com",
 *     fullName: "Nguyễn Văn A"
 *   }
 * }
 * 
 * Ví dụ khi cập nhật Profile:
 * {
 *   success: true,
 *   error: null,
 *   data: {              // Profile object đã được cập nhật
 *     userId: "USR001",
 *     phoneNumber: "0123456789",
 *     avatarUrl: "https://..."
 *   }
 * }
 * 
 * Lưu ý: data?: T được khai báo lại ở đây để TypeScript hiểu rõ hơn về kiểu dữ liệu,
 * mặc dù đã có trong ApiResponse<T> (đây là cách TypeScript hoạt động với generic types)
 */
export interface ActionResponse<T = unknown> extends ApiResponse<T> {
  data?: T;  // Dữ liệu của object vừa được tạo/cập nhật (optional - chỉ có khi success = true)
}

/**
 * Interface định nghĩa response cho thao tác xóa (delete)
 * 
 * Extends ApiResponse: Kế thừa từ ApiResponse (không có generic vì delete thường không trả về data)
 * 
 * Tại sao cần interface riêng này?
 * - Khi xóa, thường chỉ cần biết thành công hay thất bại
 * - Không cần trả về data (vì đã xóa rồi)
 * - Đảm bảo type safety và rõ ràng về mục đích sử dụng
 * 
 * Ví dụ response khi xóa thành công:
 * {
 *   success: true,
 *   error: null
 * }
 * 
 * Ví dụ response khi xóa thất bại:
 * {
 *   success: false,
 *   error: {
 *     code: 404,
 *     message: "Không tìm thấy item để xóa"
 *   }
 * }
 * 
 * Lưu ý: success và error được khai báo lại ở đây để làm rõ ý nghĩa,
 * mặc dù đã có trong ApiResponse (đây là cách TypeScript hoạt động với interface extension)
 */
export interface DeleteResponse extends ApiResponse {
  success: boolean;        // true nếu xóa thành công, false nếu thất bại
  error: ApiError | null; // Thông tin lỗi (null nếu thành công, ApiError object nếu có lỗi)
}

