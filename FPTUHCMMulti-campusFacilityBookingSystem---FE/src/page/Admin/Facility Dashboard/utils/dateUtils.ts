/**
 * Utility functions để parse và format ngày giờ từ backend
 * 
 * @description File này chứa các helper functions để xử lý ngày giờ:
 *   - parseDateString: Parse chuỗi từ backend thành Date object
 *   - formatDate: Format Date thành chuỗi ngày tiếng Việt
 *   - formatTime: Format Date thành chuỗi giờ:phút (24h)
 * 
 * ⚠️ LƯU Ý QUAN TRỌNG:
 * Backend sử dụng DateTimeConverter (custom) trả về format "dd/MM/yyyy HH:mm:ss"
 * (ví dụ: "10/12/2025 09:10:11"), KHÔNG phải ISO 8601 chuẩn.
 * 
 * Functions này hỗ trợ cả 2 format để đảm bảo tương thích:
 *   1. Format backend: "dd/MM/yyyy HH:mm:ss" (ưu tiên)
 *   2. ISO 8601: "2025-12-10T09:10:11Z" (fallback)
 */

/**
 * Parse chuỗi ngày từ backend thành JavaScript Date object
 * 
 * @param dateString - Chuỗi ngày từ backend (có thể null/undefined)
 * @returns Date object nếu parse thành công, null nếu thất bại
 * 
 * @example
 * parseDateString("10/12/2025 09:10:11") // → Date object: 10 Dec 2025, 09:10:11
 * parseDateString("2025-12-10T09:10:11") // → Date object (ISO 8601 fallback)
 * parseDateString(null) // → null
 */
export const parseDateString = (dateString: string | null | undefined): Date | null => {
  if (!dateString) return null

  try {
    // Format 1: "dd/MM/yyyy HH:mm:ss" - DateTime đầy đủ từ backend
    const ddMMyyyyMatch = dateString.match(/^(\d{2})\/(\d{2})\/(\d{4})\s+(\d{2}):(\d{2}):(\d{2})$/)
    if (ddMMyyyyMatch) {
      const [, day, month, year, hours, minutes, seconds] = ddMMyyyyMatch
      return new Date(
        parseInt(year),
        parseInt(month) - 1, // JavaScript month bắt đầu từ 0 (0 = January)
        parseInt(day),
        parseInt(hours),
        parseInt(minutes),
        parseInt(seconds)
      )
    }

    // Format 2: Fallback - ISO 8601 hoặc format khác mà JS Date hiểu
    const date = new Date(dateString)
    if (!isNaN(date.getTime())) {
      return date
    }

    return null
  } catch {
    return null
  }
}

/**
 * Format Date object thành chuỗi ngày tiếng Việt
 * 
 * @param date - Date object hoặc null
 * @returns Chuỗi format "15 thg 12, 2025" hoặc "N/A" nếu lỗi
 * 
 * @example
 * formatDate(new Date()) // → "15 thg 12, 2025"
 */
export const formatDate = (date: Date | null): string => {
  if (!date) return 'N/A'
  try {
    return new Intl.DateTimeFormat('vi-VN', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    }).format(date)
  } catch {
    return 'N/A'
  }
}

/**
 * Format Date object thành chuỗi giờ:phút (24h)
 * 
 * @param date - Date object hoặc null
 * @returns Chuỗi format "09:10" hoặc "N/A" nếu lỗi
 * 
 * @example
 * formatTime(new Date()) // → "09:10"
 */
export const formatTime = (date: Date | null): string => {
  if (!date) return 'N/A'
  try {
    return new Intl.DateTimeFormat('vi-VN', {
      hour: '2-digit',
      minute: '2-digit',
      hour12: false, // Sử dụng format 24 giờ
    }).format(date)
  } catch {
    return 'N/A'
  }
}
