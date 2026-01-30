import { XCircle } from 'lucide-react'

interface RejectionReasonSectionProps {
  rejectionReason: string
}

/**
 * RejectionReasonSection Component - Hiển thị lý do từ chối booking
 * 
 * @description Component này chỉ hiển thị khi booking bị từ chối (status = "Rejected").
 * Hiển thị lý do mà admin đã nhập khi từ chối booking.
 * 
 * @param rejectionReason - Lý do từ chối từ admin (bắt buộc)
 * 
 * @example
 * <RejectionReasonSection rejectionReason="Không đủ điều kiện sử dụng" />
 */
export const RejectionReasonSection = ({
  rejectionReason,
}: RejectionReasonSectionProps) => {
  return (
    <div className="rounded-lg border border-red-200 bg-red-50 p-3">
      {/* Header với icon */}
      <div className="mb-1 flex items-center gap-2">
        <XCircle className="h-4 w-4 text-red-600" />
        <p className="text-xs font-semibold text-red-700">Lý do từ chối</p>
      </div>
      
      {/* Nội dung lý do */}
      <p className="text-xs text-red-600">{rejectionReason}</p>
    </div>
  )
}
