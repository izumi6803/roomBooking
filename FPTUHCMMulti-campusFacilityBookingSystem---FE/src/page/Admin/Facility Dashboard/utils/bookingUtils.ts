/**
 * Utility functions cho booking data processing
 * 
 * @description File này chứa các helper functions để xử lý dữ liệu booking từ backend,
 *              đặc biệt là các field có thể có format khác nhau (JSON hoặc plain text)
 */

/**
 * Parse chuỗi yêu cầu đặc biệt từ backend thành object
 * 
 * @description Backend có thể lưu specialRequirements dưới 2 dạng:
 *   1. JSON string: "{\"projector\": true, \"whiteboard\": true}"
 *   2. Plain text: "Cần máy chiếu và bảng trắng"
 * 
 * Function này tự động detect và parse:
 *   - Nếu là JSON hợp lệ → parse và trả về object
 *   - Nếu không phải JSON → wrap thành object {"note": "..."}
 *   - Nếu empty/null → trả về null
 * 
 * @param specialRequirements - Chuỗi từ backend (có thể null/undefined/empty)
 * @returns Object với các key-value, hoặc null nếu không có
 * 
 * @example
 * parseSpecialRequirements('{"projector": true}')  // → {projector: true}
 * parseSpecialRequirements('Cần máy chiếu')        // → {note: "Cần máy chiếu"}
 * parseSpecialRequirements(null)                   // → null
 * parseSpecialRequirements('')                     // → null
 */
export const parseSpecialRequirements = (
  specialRequirements?: string | null
): Record<string, boolean | string | number | string[]> | null => {
  if (!specialRequirements || specialRequirements.trim() === '') {
    return null
  }

  try {
    // Thử parse JSON
    return JSON.parse(specialRequirements)
  } catch {
    // Nếu không phải JSON hợp lệ, wrap thành object với key "note"
    return { note: specialRequirements }
  }
}
