import { CheckCircle2, XCircle, AlertCircle, Loader2 } from 'lucide-react'
import type { AdminBooking } from '../api/adminBookingApi'

interface ActionModalProps {
  show: boolean
  booking: AdminBooking | null
  actionType: 'approve' | 'reject' | null
  rejectionReason: string
  onRejectionReasonChange: (reason: string) => void
  onConfirm: () => void
  onCancel: () => void
  loading?: boolean
}

// Helper function to parse date string from backend
// Backend returns format: "dd/MM/yyyy HH:mm:ss" (e.g., "10/12/2025 09:10:11")
const parseDateString = (dateString: string | null | undefined): Date | null => {
  if (!dateString) return null;
  
  try {
    // Try parsing "dd/MM/yyyy HH:mm:ss" format
    const ddMMyyyyMatch = dateString.match(/^(\d{2})\/(\d{2})\/(\d{4})\s+(\d{2}):(\d{2}):(\d{2})$/);
    if (ddMMyyyyMatch) {
      const [, day, month, year, hours, minutes, seconds] = ddMMyyyyMatch;
      return new Date(
        parseInt(year),
        parseInt(month) - 1, // Month is 0-indexed
        parseInt(day),
        parseInt(hours),
        parseInt(minutes),
        parseInt(seconds)
      );
    }
    
    // Fallback: try standard Date parsing (for ISO 8601 format)
    const date = new Date(dateString);
    if (!isNaN(date.getTime())) {
      return date;
    }
    
    return null;
  } catch {
    return null;
  }
};

const ActionModal = ({
  show,
  booking,
  actionType,
  rejectionReason,
  onRejectionReasonChange,
  onConfirm,
  onCancel,
  loading = false
}: ActionModalProps) => {
  if (!show || !booking || !actionType) return null

  // Parse date strings từ API (supports "dd/MM/yyyy HH:mm:ss" format)
  const startTime = parseDateString(booking.startTime);
  const endTime = parseDateString(booking.endTime);

  const formatDate = (date: Date | null) => {
    if (!date) return 'N/A';
    try {
      return new Intl.DateTimeFormat('vi-VN', {
        year: 'numeric',
        month: 'short',
        day: 'numeric'
      }).format(date);
    } catch {
      return 'N/A';
    }
  }

  const formatTime = (date: Date | null) => {
    if (!date) return 'N/A';
    try {
      return new Intl.DateTimeFormat('vi-VN', {
        hour: '2-digit',
        minute: '2-digit',
        hour12: false
      }).format(date);
    } catch {
      return 'N/A';
    }
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
      <div className="w-full max-w-md rounded-lg border border-gray-200 bg-white p-6 shadow-xl">
        {actionType === 'approve' ? (
          <>
            <div className="mb-4 flex items-center gap-3">
              <div className="flex h-10 w-10 items-center justify-center rounded-full bg-green-100">
                <CheckCircle2 className="h-5 w-5 text-green-600" />
              </div>
              <div>
                <h3 className="text-lg font-semibold text-gray-900">Duyệt đặt phòng</h3>
                <p className="text-sm text-gray-600">Xác nhận duyệt yêu cầu đặt phòng này</p>
              </div>
            </div>
            <div className="mb-6 rounded-lg bg-gray-50 p-4">
              <p className="text-sm text-gray-700">
                <span className="font-semibold">Cơ sở vật chất:</span> {booking.facilityName}
              </p>
              <p className="mt-1 text-sm text-gray-700">
                <span className="font-semibold">Ngày:</span> {formatDate(startTime)} từ{' '}
                {formatTime(startTime)} đến {formatTime(endTime)}
              </p>
              <p className="mt-1 text-sm text-gray-700">
                <span className="font-semibold">Người yêu cầu:</span> {booking.userName}
              </p>
              <p className="mt-1 text-sm text-gray-700">
                <span className="font-semibold">Mục đích:</span> {booking.purpose}
              </p>
            </div>
            <div className="flex justify-end gap-3">
              <button
                onClick={onCancel}
                disabled={loading}
                className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
              >
                Hủy
              </button>
              <button
                onClick={onConfirm}
                disabled={loading}
                className="flex items-center gap-2 rounded-lg bg-green-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-green-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {loading && <Loader2 className="h-4 w-4 animate-spin" />}
                Xác nhận duyệt
              </button>
            </div>
          </>
        ) : (
          <>
            <div className="mb-4 flex items-center gap-3">
              <div className="flex h-10 w-10 items-center justify-center rounded-full bg-red-100">
                <XCircle className="h-5 w-5 text-red-600" />
              </div>
              <div>
                <h3 className="text-lg font-semibold text-gray-900">Từ chối đặt phòng</h3>
                <p className="text-sm text-gray-600">Cung cấp lý do từ chối</p>
              </div>
            </div>
            <div className="mb-4 rounded-lg bg-gray-50 p-4">
              <p className="text-sm text-gray-700">
                <span className="font-semibold">Cơ sở vật chất:</span> {booking.facilityName}
              </p>
              <p className="mt-1 text-sm text-gray-700">
                <span className="font-semibold">Ngày:</span> {formatDate(startTime)} từ{' '}
                {formatTime(startTime)} đến {formatTime(endTime)}
              </p>
              <p className="mt-1 text-sm text-gray-700">
                <span className="font-semibold">Người yêu cầu:</span> {booking.userName}
              </p>
              <p className="mt-1 text-sm text-gray-700">
                <span className="font-semibold">Mục đích:</span> {booking.purpose}
              </p>
            </div>
            <div className="mb-4">
              <label className="mb-2 block text-sm font-semibold text-gray-700">
                Lý do từ chối <span className="text-red-500">*</span>
              </label>
              <textarea
                value={rejectionReason}
                onChange={(e) => onRejectionReasonChange(e.target.value)}
                placeholder="Nhập lý do từ chối..."
                rows={4}
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                required
              />
              {!rejectionReason && (
                <p className="mt-1 flex items-center gap-1 text-xs text-red-600">
                  <AlertCircle className="h-3 w-3" />
                  Lý do từ chối là bắt buộc
                </p>
              )}
            </div>
            <div className="flex justify-end gap-3">
              <button
                onClick={onCancel}
                disabled={loading}
                className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
              >
                Hủy
              </button>
              <button
                onClick={onConfirm}
                disabled={!rejectionReason.trim() || loading}
                className="flex items-center gap-2 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-red-700 disabled:bg-gray-300 disabled:cursor-not-allowed transition-colors"
              >
                {loading && <Loader2 className="h-4 w-4 animate-spin" />}
                Xác nhận từ chối
              </button>
            </div>
          </>
        )}
      </div>
    </div>
  )
}

export default ActionModal

