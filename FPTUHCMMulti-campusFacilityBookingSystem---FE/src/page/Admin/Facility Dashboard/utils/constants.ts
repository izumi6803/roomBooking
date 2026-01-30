/**
 * Constants và mappings cho Booking Status và Facility Category
 * 
 * @description File này chứa:
 *   - Type definitions cho BookingStatus và FacilityCategory
 *   - Mapping tables: STATUS_COLORS, STATUS_LABELS, CATEGORY_COLORS
 *   - Helper functions để lấy color/label với fallback
 * 
 * Tất cả constants này được dùng trong BookingCard và các components liên quan
 */

/**
 * Booking Status Types - Tất cả các trạng thái có thể có của một booking
 */
export type BookingStatus =
  | 'Pending_Approval' // Chờ admin duyệt
  | 'Approved'         // Đã được duyệt
  | 'Rejected'         // Bị từ chối
  | 'Completed'        // Đã hoàn thành (sau khi check-out)
  | 'Cancelled'        // Đã hủy bởi user
  | 'Draft'            // Bản nháp (legacy, không dùng nữa)
  | 'No_Show'          // Không đến (sau khi check-in window đã hết)

/**
 * Facility Category Types - Các loại cơ sở vật chất
 */
export type FacilityCategory =
  | 'Academic'         // Học thuật
  | 'Teaching'         // Giảng dạy
  | 'Administrative'   // Hành chính
  | 'Sports'           // Thể thao
  | 'Research'         // Nghiên cứu

/**
 * Map booking status sang Tailwind CSS classes cho badge
 */
export const STATUS_COLORS: Record<BookingStatus, string> = {
  Pending_Approval: 'bg-yellow-100 text-yellow-700',
  Approved: 'bg-green-100 text-green-700',
  Rejected: 'bg-red-100 text-red-700',
  Completed: 'bg-blue-100 text-blue-700',
  Cancelled: 'bg-gray-100 text-gray-700',
  Draft: 'bg-gray-100 text-gray-600',
  No_Show: 'bg-orange-100 text-orange-700',
}

/**
 * Map booking status sang label tiếng Việt
 */
export const STATUS_LABELS: Record<BookingStatus, string> = {
  Pending_Approval: 'Chờ duyệt',
  Approved: 'Đã duyệt',
  Rejected: 'Đã từ chối',
  Completed: 'Hoàn thành',
  Cancelled: 'Đã hủy',
  Draft: 'Bản nháp',
  No_Show: 'Không đến',
}

/**
 * Map facility category sang màu badge
 */
export const CATEGORY_COLORS: Record<FacilityCategory, string> = {
  Academic: 'bg-blue-100 text-blue-700',
  Teaching: 'bg-purple-100 text-purple-700',
  Administrative: 'bg-orange-100 text-orange-700',
  Sports: 'bg-green-100 text-green-700',
  Research: 'bg-indigo-100 text-indigo-700',
}

/**
 * ============================================
 * HELPER FUNCTIONS
 * ============================================
 */

/**
 * Lấy Tailwind CSS classes cho status badge
 * 
 * @param status - Status string từ backend
 * @returns Tailwind classes cho background và text color
 * 
 * @example
 * getStatusColor("Pending_Approval") // → "bg-yellow-100 text-yellow-700"
 * getStatusColor("InvalidStatus")    // → "bg-gray-100 text-gray-700" (fallback)
 */
export const getStatusColor = (status: string): string => {
  return STATUS_COLORS[status as BookingStatus] || 'bg-gray-100 text-gray-700'
}

/**
 * Lấy label tiếng Việt cho status
 * 
 * @param status - Status string từ backend
 * @returns Label tiếng Việt hoặc status gốc nếu không tìm thấy mapping
 * 
 * @example
 * getStatusLabel("Pending_Approval") // → "Chờ duyệt"
 * getStatusLabel("InvalidStatus")    // → "InvalidStatus" (fallback)
 */
export const getStatusLabel = (status: string): string => {
  return STATUS_LABELS[status as BookingStatus] || status
}

/**
 * Lấy Tailwind CSS classes cho category badge
 * 
 * @param category - Category string từ backend (optional)
 * @returns Tailwind classes cho background và text color
 * 
 * @example
 * getCategoryColor("Academic")  // → "bg-blue-100 text-blue-700"
 * getCategoryColor(undefined)   // → "bg-gray-100 text-gray-700" (fallback)
 */
export const getCategoryColor = (category?: string): string => {
  if (!category) return 'bg-gray-100 text-gray-700'
  return CATEGORY_COLORS[category as FacilityCategory] || 'bg-gray-100 text-gray-700'
}
