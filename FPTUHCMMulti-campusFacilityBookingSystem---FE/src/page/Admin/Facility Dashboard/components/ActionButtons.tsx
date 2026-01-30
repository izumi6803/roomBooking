import { CheckCircle2, XCircle } from 'lucide-react'

interface ActionButtonsProps {
  /** Booking ID để pass vào callback */
  bookingId: string
  /** Callback khi click nút Duyệt */
  onApprove: (bookingId: string) => void
  /** Callback khi click nút Từ chối */
  onReject: (bookingId: string) => void
}

/**
 * ActionButtons Component - Nút hành động cho admin (Duyệt/Từ chối)
 * 
 * @description Component này hiển thị 2 nút:
 *   - Nút "Duyệt": Màu xanh lá, khi click sẽ approve booking
 *   - Nút "Từ chối": Màu đỏ, khi click sẽ reject booking
 * 
 * Layout: Vertical stack (flex-col), width cố định trên desktop (lg:w-48)
 * 
 * @param bookingId - ID của booking cần approve/reject
 * @param onApprove - Callback function được gọi khi click "Duyệt"
 * @param onReject - Callback function được gọi khi click "Từ chối"
 * 
 * @example
 * <ActionButtons
 *   bookingId="B00001"
 *   onApprove={(id) => handleApprove(id)}
 *   onReject={(id) => handleReject(id)}
 * />
 */
export const ActionButtons = ({ bookingId, onApprove, onReject }: ActionButtonsProps) => {
  return (
    <div className="flex flex-col gap-2 lg:w-48">
      {/* Nút Duyệt - Màu xanh lá */}
      <button
        onClick={() => onApprove(bookingId)}
        className="flex items-center justify-center gap-2 rounded-lg bg-green-600 px-4 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-green-700 transition-colors"
        aria-label="Duyệt booking"
      >
        <CheckCircle2 className="h-4 w-4" />
        Duyệt
      </button>

      {/* Nút Từ chối - Màu đỏ outline */}
      <button
        onClick={() => onReject(bookingId)}
        className="flex items-center justify-center gap-2 rounded-lg border border-red-300 bg-red-50 px-4 py-2.5 text-sm font-semibold text-red-600 hover:bg-red-100 transition-colors"
        aria-label="Từ chối booking"
      >
        <XCircle className="h-4 w-4" />
        Từ chối
      </button>
    </div>
  )
}
