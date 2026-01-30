/**
 * CancelBookingModal Component - Modal xác nhận hủy booking
 * 
 * Component này hiển thị modal để user nhập lý do hủy booking:
 * - Hiển thị thông tin booking (facility name, booking date)
 * - Textarea để nhập lý do hủy (bắt buộc, tối thiểu 10 ký tự)
 * - Validation lý do hủy
 * - Character counter (tối đa 500 ký tự)
 * - Warning message về thời gian hủy
 * 
 * Tính năng:
 * - Controlled component (isOpen prop để control visibility)
 * - Validation: Lý do phải có ít nhất 10 ký tự
 * - Disable khi đang loading
 * - Reset form khi đóng
 */

// Import useState hook
import { useState } from 'react';
// Import icons
import { X, AlertTriangle } from 'lucide-react';

/**
 * Interface định nghĩa props của CancelBookingModal
 * 
 * @property {boolean} isOpen - Trạng thái mở/đóng modal
 * @property {() => void} onClose - Callback khi đóng modal
 * @property {(reason: string) => void} onConfirm - Callback khi xác nhận hủy (nhận lý do)
 * @property {string} facilityName - Tên cơ sở vật chất
 * @property {string} bookingDate - Ngày đặt (đã được format)
 * @property {boolean} isLoading - Trạng thái loading (optional, default: false)
 */
interface CancelBookingModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: (reason: string) => void;
  facilityName: string;
  bookingDate: string;
  isLoading?: boolean;
}

/**
 * CancelBookingModal Component Function
 * 
 * Modal component để xác nhận hủy booking
 * 
 * @param {CancelBookingModalProps} props - Props của component
 * @returns {JSX.Element | null} - JSX element chứa modal hoặc null nếu không mở
 */
const CancelBookingModal = ({
  isOpen,
  onClose,
  onConfirm,
  facilityName,
  bookingDate,
  isLoading = false, // Default value: false
}: CancelBookingModalProps) => {
  // State lưu lý do hủy booking
  const [reason, setReason] = useState('');
  
  // State lưu validation error
  const [error, setError] = useState<string | null>(null);

  /**
   * Function: Handle khi submit form
   * 
   * Validate và gọi onConfirm callback với lý do
   * 
   * @param {React.FormEvent} e - Form submit event
   */
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    // Validation: Lý do không được để trống
    if (!reason.trim()) {
      setError('Vui lòng nhập lý do hủy booking');
      return; // Dừng lại
    }

    // Validation: Lý do phải có ít nhất 10 ký tự
    if (reason.trim().length < 10) {
      setError('Lý do hủy phải có ít nhất 10 ký tự');
      return; // Dừng lại
    }

    // Hợp lệ: Gọi callback với lý do đã trim
    onConfirm(reason.trim());
  };

  /**
   * Function: Handle khi đóng modal
   * 
   * Reset form và gọi onClose callback
   * Chỉ đóng được nếu không đang loading
   */
  const handleClose = () => {
    // Chỉ đóng được nếu không đang loading
    if (!isLoading) {
      // Reset form
      setReason('');
      setError(null);
      
      // Gọi callback
      onClose();
    }
  };

  /**
   * Early return: Không render nếu modal không mở
   * 
   * Nếu isOpen = false, return null (không render gì)
   */
  if (!isOpen) return null;

  /**
   * Render Modal
   * 
   * JSX structure:
   * - Backdrop (overlay)
   * - Modal container
   *   - Header với icon và nút đóng
   *   - Content với form
   *     - Booking info
   *     - Textarea cho lý do
   *     - Warning message
   *   - Footer với buttons
   */
  return (
    // Backdrop (overlay)
    // fixed: Vị trí cố định (không scroll)
    // inset-0: Chiếm toàn bộ màn hình (top:0, right:0, bottom:0, left:0)
    // z-50: Z-index cao để hiển thị trên các element khác
    // flex: Sử dụng flexbox để căn giữa
    // items-center justify-center: Căn giữa theo cả 2 trục
    // bg-black bg-opacity-50: Background đen với độ trong suốt 50%
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50">
      {/* Modal Container
          bg-white: Background trắng
          rounded-lg: Bo góc lớn
          shadow-xl: Shadow rất lớn (tạo độ sâu)
          w-full max-w-md: Chiều rộng 100% nhưng tối đa md (448px)
          mx-4: Margin trái-phải 4 units (16px) để không sát mép màn hình */}
      <div className="bg-white rounded-lg shadow-xl w-full max-w-md mx-4">
        {/* Header Section
            flex: Sử dụng flexbox
            items-center: Căn items vào giữa theo cross-axis
            justify-between: Khoảng cách giữa các items là space-between
            p-6: Padding tất cả các phía 6 units (24px)
            border-b: Border bottom
            border-gray-200: Màu border xám nhạt */}
        <div className="flex items-center justify-between p-6 border-b border-gray-200">
          {/* Left side: Icon và title */}
          <div className="flex items-center gap-3">
            {/* Icon container
                p-2: Padding 2 units (8px)
                bg-red-100: Background đỏ nhạt
                rounded-lg: Bo góc lớn */}
            <div className="p-2 bg-red-100 rounded-lg">
              <AlertTriangle className="w-5 h-5 text-red-600" />
            </div>
            {/* Title */}
            <h3 className="text-lg font-semibold text-gray-800">Hủy Booking</h3>
          </div>
          
          {/* Right side: Close button */}
          <button
            onClick={handleClose}
            disabled={isLoading}
            className="p-1 text-gray-400 hover:text-gray-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Content Section
            Form element để submit */}
        <form onSubmit={handleSubmit} className="p-6">
          <div className="space-y-4">
            {/* Booking Info */}
            <div>
              {/* Facility name */}
              <p className="text-sm text-gray-600 mb-2">
                <strong>Cơ sở vật chất:</strong> {facilityName}
              </p>
              {/* Booking date */}
              <p className="text-sm text-gray-600 mb-4">
                <strong>Ngày đặt:</strong> {bookingDate}
              </p>
            </div>

            {/* Reason Textarea */}
            <div>
              {/* Label */}
              <label htmlFor="reason" className="block text-sm font-medium text-gray-700 mb-2">
                Lý do hủy <span className="text-red-500">*</span>
              </label>
              
              {/* Textarea
                  id="reason": Để label có thể focus vào textarea
                  value: Controlled component
                  onChange: Handler khi user nhập
                  disabled: Disable khi đang loading
                  rows={4}: Chiều cao 4 dòng
                  resize-none: Không cho phép resize */}
              <textarea
                id="reason"
                value={reason}
                onChange={(e) => {
                  setReason(e.target.value);
                  setError(null); // Clear error khi user bắt đầu nhập
                }}
                disabled={isLoading}
                rows={4}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500 focus:border-orange-500 disabled:bg-gray-100 disabled:cursor-not-allowed resize-none"
                placeholder="Vui lòng nhập lý do hủy booking (tối thiểu 10 ký tự)..."
              />
              
              {/* Error message */}
              {error && <p className="mt-1 text-sm text-red-600">{error}</p>}
              
              {/* Character counter
                  Hiển thị số ký tự đã nhập / tối đa */}
              <p className="mt-1 text-xs text-gray-500">
                {reason.length}/500 ký tự (tối thiểu 10 ký tự)
              </p>
            </div>

            {/* Warning Message */}
            <div className="bg-yellow-50 border border-yellow-200 rounded-lg p-3">
              <p className="text-sm text-yellow-800">
                <strong>Lưu ý:</strong> Bạn chỉ có thể hủy booking tối đa 1 ngày trước ngày đặt lịch.
              </p>
            </div>
          </div>

          {/* Footer: Action Buttons
              flex: Sử dụng flexbox
              items-center: Căn items vào giữa
              justify-end: Căn về phía phải
              gap-3: Khoảng cách giữa các buttons
              mt-6: Margin top 6 units (24px)
              pt-4: Padding top 4 units (16px)
              border-t: Border top */}
          <div className="flex items-center justify-end gap-3 mt-6 pt-4 border-t border-gray-200">
            {/* Cancel Button */}
            <button
              type="button"
              onClick={handleClose}
              disabled={isLoading}
              className="px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            >
              Hủy
            </button>
            
            {/* Confirm Button
                type="submit": Submit form
                disabled: Disable nếu đang loading HOẶC lý do không hợp lệ */}
            <button
              type="submit"
              disabled={isLoading || !reason.trim() || reason.trim().length < 10}
              className="px-4 py-2 text-sm font-medium text-white bg-red-600 rounded-lg hover:bg-red-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {isLoading ? 'Đang xử lý...' : 'Xác nhận hủy'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CancelBookingModal;
