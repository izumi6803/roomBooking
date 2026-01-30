import { Calendar, Clock, MapPin, Users } from 'lucide-react'
import { formatDate, formatTime } from '../utils/dateUtils'

interface BookingDetailsSectionProps {
  startTime: Date | null
  endTime: Date | null
  facilityId: string
  estimatedAttendees?: number | null
}

/**
 * BookingDetailsSection Component - Hiển thị thông tin chi tiết booking
 * 
 * @description Component này hiển thị 4 thông tin chính:
 *   1. Ngày: Ngày diễn ra booking (format: "15 thg 12, 2025")
 *   2. Giờ: Khoảng thời gian (ví dụ: "09:00 - 10:00")
 *   3. Facility ID: Mã cơ sở vật chất
 *   4. Số người: Số lượng người tham gia dự kiến
 * 
 * Layout: Responsive grid - 1 cột (mobile) → 2 cột (tablet) → 4 cột (desktop)
 * 
 * @param startTime - Date object: Thời gian bắt đầu (có thể null)
 * @param endTime - Date object: Thời gian kết thúc (có thể null)
 * @param facilityId - Mã facility (bắt buộc)
 * @param estimatedAttendees - Số người tham gia (optional, hiển thị "N/A" nếu null)
 * 
 * @example
 * <BookingDetailsSection
 *   startTime={new Date("2025-12-16T09:00:00")}
 *   endTime={new Date("2025-12-16T10:00:00")}
 *   facilityId="F00001"
 *   estimatedAttendees={10}
 * />
 */
export const BookingDetailsSection = ({
  startTime,
  endTime,
  facilityId,
  estimatedAttendees,
}: BookingDetailsSectionProps) => {
  return (
    <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
      <div className="flex items-start gap-2">
        <Calendar className="mt-0.5 h-4 w-4 text-gray-400" />
        <div>
          <p className="text-xs text-gray-500">Ngày</p>
          <p className="text-sm font-medium text-gray-900">{formatDate(startTime)}</p>
        </div>
      </div>
      <div className="flex items-start gap-2">
        <Clock className="mt-0.5 h-4 w-4 text-gray-400" />
        <div>
          <p className="text-xs text-gray-500">Giờ</p>
          <p className="text-sm font-medium text-gray-900">
            {formatTime(startTime)} - {formatTime(endTime)}
          </p>
        </div>
      </div>
      <div className="flex items-start gap-2">
        <MapPin className="mt-0.5 h-4 w-4 text-gray-400" />
        <div>
          <p className="text-xs text-gray-500">Facility ID</p>
          <p className="text-sm font-medium text-gray-900">{facilityId}</p>
        </div>
      </div>
      <div className="flex items-start gap-2">
        <Users className="mt-0.5 h-4 w-4 text-gray-400" />
        <div>
          <p className="text-xs text-gray-500">Số người tham gia</p>
          <p className="text-sm font-medium text-gray-900">{estimatedAttendees || 'N/A'}</p>
        </div>
      </div>
    </div>
  )
}
