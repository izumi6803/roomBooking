import { CheckCircle2, Clock } from 'lucide-react'
import { formatDate, formatTime } from '../utils/dateUtils'

interface BookingMetaInfoProps {
  /** Thời gian tạo booking request */
  createdAt: Date | null
  /** Tên admin đã duyệt (nếu có) */
  approvedBy?: string | null
  /** Tên hiển thị của admin đã duyệt (nếu có) */
  approvedByUserName?: string | null
  /** Thời gian admin duyệt (nếu có) */
  approvedAt: Date | null
}

/**
 * BookingMetaInfo Component - Hiển thị thông tin meta của booking
 * 
 * @description Component này hiển thị 2 thông tin:
 *   1. Thời gian tạo booking request (luôn hiển thị nếu có)
 *   2. Thông tin duyệt (chỉ hiển thị nếu đã được duyệt)
 * 
 * @param createdAt - Thời gian tạo booking request
 * @param approvedBy - ID của admin đã duyệt (fallback nếu không có approvedByUserName)
 * @param approvedByUserName - Tên hiển thị của admin đã duyệt (ưu tiên)
 * @param approvedAt - Thời gian duyệt
 * 
 * @example
 * <BookingMetaInfo
 *   createdAt={new Date("2025-12-16T08:00:00")}
 *   approvedBy="ADMIN001"
 *   approvedByUserName="Nguyễn Văn A"
 *   approvedAt={new Date("2025-12-16T09:00:00")}
 * />
 */
export const BookingMetaInfo = ({
  createdAt,
  approvedBy,
  approvedByUserName,
  approvedAt,
}: BookingMetaInfoProps) => {
  return (
    <div className="space-y-2">
      {/* Thông tin duyệt (nếu đã được duyệt) */}
      {approvedBy && approvedAt && (
        <div className="flex items-center gap-2 text-xs text-gray-500">
          <CheckCircle2 className="h-3 w-3 text-green-600" />
          <span>
            Đã duyệt bởi <span className="font-medium">{approvedByUserName || approvedBy}</span> vào{' '}
            {formatDate(approvedAt)} lúc {formatTime(approvedAt)}
          </span>
        </div>
      )}

      {/* Thời gian tạo booking request */}
      {createdAt && (
        <div className="flex items-center gap-2 text-xs text-gray-500">
          <Clock className="h-3 w-3" />
          <span>
            Yêu cầu vào {formatDate(createdAt)} lúc {formatTime(createdAt)}
          </span>
        </div>
      )}
    </div>
  )
}
