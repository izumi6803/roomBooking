import { getStatusColor, getStatusLabel, getCategoryColor } from '../utils/constants'

interface BookingHeaderProps {
  /** Tên facility */
  facilityName: string
  /** Loại facility (optional) */
  category?: string | null
  /** Trạng thái booking */
  status: string
  /** Mục đích sử dụng */
  purpose: string
}

/**
 * BookingHeader Component - Header section của booking card
 * 
 * @description Component này hiển thị:
 *   - Tên facility (heading lớn)
 *   - Category badge (nếu có) - màu sắc tùy loại
 *   - Status badge - màu sắc và label tùy trạng thái
 *   - Purpose - mục đích sử dụng
 * 
 * Layout: Responsive flexbox với wrap, hỗ trợ mobile và desktop
 * 
 * @param facilityName - Tên facility (bắt buộc)
 * @param category - Loại facility (optional: Academic, Teaching, Sports, ...)
 * @param status - Trạng thái booking (Pending_Approval, Approved, Rejected, ...)
 * @param purpose - Mục đích sử dụng facility
 * 
 * @example
 * <BookingHeader
 *   facilityName="Phòng Lab 101"
 *   category="Academic"
 *   status="Pending_Approval"
 *   purpose="Họp nhóm dự án"
 * />
 */
export const BookingHeader = ({
  facilityName,
  category,
  status,
  purpose,
}: BookingHeaderProps) => {
  return (
    <div className="flex items-start justify-between">
      <div>
        {/* Facility name + Badges */}
        <div className="flex items-center gap-2 flex-wrap">
          {/* Tên facility */}
          <h3 className="text-lg font-semibold text-gray-900">{facilityName}</h3>

          {/* Category badge - chỉ hiển thị nếu có */}
          {category && (
            <span
              className={`inline-flex rounded-full px-2 py-1 text-xs font-semibold ${getCategoryColor(category)}`}
            >
              {category}
            </span>
          )}

          {/* Status badge - luôn hiển thị */}
          <span
            className={`inline-flex rounded-full px-2 py-1 text-xs font-semibold ${getStatusColor(status)}`}
          >
            {getStatusLabel(status)}
          </span>
        </div>

        {/* Purpose - mục đích sử dụng */}
        <p className="mt-1 text-sm text-gray-600">{purpose}</p>
      </div>
    </div>
  )
}
