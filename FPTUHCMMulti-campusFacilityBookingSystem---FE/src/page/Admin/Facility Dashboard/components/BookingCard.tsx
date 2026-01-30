import { parseDateString } from '../utils/dateUtils'
import { parseSpecialRequirements } from '../utils/bookingUtils'
import type { AdminBooking } from '../api/adminBookingApi'

// Import tất cả sub-components
import { BookingHeader } from './BookingHeader'
import { BookingDetailsSection } from './BookingDetailsSection'
import { UserInfoSection } from './UserInfoSection'
import { SpecialRequirementsSection } from './SpecialRequirementsSection'
import { RejectionReasonSection } from './RejectionReasonSection'
import { CheckInOutImagesSection } from './CheckInOutImagesSection'
import { FeedbackSection } from './FeedbackSection'
import { BookingMetaInfo } from './BookingMetaInfo'
import { ActionButtons } from './ActionButtons'

interface BookingCardProps {
  booking: AdminBooking
  onApprove: (bookingId: string) => void
  onReject: (bookingId: string) => void
}

/**
 * BookingCard Component - Hiển thị thông tin chi tiết một booking trong Admin Dashboard
 * 
 * @description Component này hiển thị đầy đủ thông tin về một booking:
 *   - Header: Tên facility, category badge, status badge, mục đích sử dụng
 *   - Booking details: Ngày, giờ, facility ID, số người tham gia
 *   - User info: Tên, MSSV (nếu có), email, SĐT của người đặt
 *   - Special requirements: Yêu cầu đặc biệt (nếu có)
 *   - Check-in/Check-out images: Ảnh xác nhận (nếu có)
 *   - Feedback: Đánh giá từ người dùng (nếu có)
 *   - Rejection reason: Lý do từ chối (nếu bị reject)
 *   - Action buttons: Nút Duyệt/Từ chối (chỉ hiện khi status = Pending_Approval)
 * 
 * @param booking - Dữ liệu booking từ API (type: AdminBooking)
 * @param onApprove - Callback khi admin click nút "Duyệt"
 * @param onReject - Callback khi admin click nút "Từ chối"
 * 
 * @example
 * <BookingCard
 *   booking={bookingData}
 *   onApprove={(id) => handleApprove(id)}
 *   onReject={(id) => handleReject(id)}
 * />
 */
const BookingCard = ({ booking, onApprove, onReject }: BookingCardProps) => {
  // ============================================
  // STEP 1: PARSE DỮ LIỆU NGÀY GIỜ TỪ API
  // ============================================
  /**
   * Backend trả về chuỗi format "dd/MM/yyyy HH:mm:ss" (không phải ISO 8601).
   * Cần parse thành Date object để format và hiển thị đúng.
   */
  const startTime = parseDateString(booking.startTime) // Giờ bắt đầu booking
  const endTime = parseDateString(booking.endTime) // Giờ kết thúc booking
  const createdAt = parseDateString(booking.createdAt) // Thời gian tạo booking request
  const approvedAt = parseDateString(booking.approvedAt) // Thời gian admin duyệt (nếu có)

  // ============================================
  // STEP 2: PARSE YÊU CẦU ĐẶC BIỆT
  // ============================================
  /**
   * Backend có thể lưu specialRequirements dưới dạng:
   * - JSON string: '{"projector": true}'
   * - Plain text: 'Cần máy chiếu'
   * Function này tự động detect và parse về object hoặc null
   */
  const specialRequirements = parseSpecialRequirements(booking.specialRequirements)

  // ============================================
  // STEP 3: XÁC ĐỊNH CÁC ĐIỀU KIỆN HIỂN THỊ
  // ============================================
  /**
   * Chỉ hiển thị nút Approve/Reject khi booking đang ở trạng thái "Pending_Approval".
   * Các trạng thái khác (Approved, Rejected, Completed, ...) không cần action buttons.
   */
  const canApproveOrReject = booking.status === 'Pending_Approval'

  /**
   * Kiểm tra xem có ảnh check-in/check-out không.
   * Chỉ render CheckInOutImagesSection nếu có ít nhất 1 ảnh.
   */
  const hasCheckInOutImages =
    (booking.checkInImages?.length ?? 0) > 0 ||
    (booking.checkOutImages?.length ?? 0) > 0

  // ============================================
  // RENDER COMPONENT
  // ============================================
  /**
   * Layout structure:
   * - Container: Card với border, shadow, hover effect
   * - Main layout: Flex row trên desktop, column trên mobile
   *   - Left: Tất cả thông tin booking (flex-1)
   *   - Right: Action buttons (chỉ khi canApproveOrReject = true)
   */
  return (
    <div className="rounded-lg border border-gray-200 bg-white p-6 shadow-sm transition-shadow hover:shadow-md">
      <div className="flex flex-col gap-4 lg:flex-row lg:items-start lg:justify-between">
        {/* ============================================
            LEFT SECTION: MAIN CONTENT
            Hiển thị tất cả thông tin booking theo thứ tự logic
            ============================================ */}
        <div className="flex-1 space-y-4">
          {/* Header: Facility name + Category + Status + Purpose */}
          <BookingHeader
            facilityName={booking.facilityName}
            category={booking.category}
            status={booking.status}
            purpose={booking.purpose}
          />

          {/* Booking Details: Ngày, giờ, facility ID, số người */}
          <BookingDetailsSection
            startTime={startTime}
            endTime={endTime}
            facilityId={booking.facilityId}
            estimatedAttendees={booking.estimatedAttendees}
          />

          {/* User Info: Thông tin người đặt phòng */}
          <UserInfoSection
            userName={booking.userName}
            userStudentId={booking.userStudentId}
            userEmail={booking.userEmail}
            userPhoneNumber={booking.userPhoneNumber}
          />

          {/* Special Requirements: Yêu cầu đặc biệt (chỉ hiện nếu có) */}
          {specialRequirements && (
            <SpecialRequirementsSection specialRequirements={specialRequirements} />
          )}

          {/* Rejection Reason: Lý do từ chối (chỉ hiện nếu bị reject) */}
          {booking.rejectionReason && (
            <RejectionReasonSection rejectionReason={booking.rejectionReason} />
          )}

          {/* Check-in/Check-out Images: Ảnh xác nhận (chỉ hiện nếu có) */}
          {hasCheckInOutImages && <CheckInOutImagesSection booking={booking} />}

          {/* Feedback: Đánh giá từ người dùng (chỉ hiện nếu có) */}
          {booking.feedback && <FeedbackSection feedback={booking.feedback} />}

          {/* Meta Info: Thông tin tạo và duyệt */}
          <BookingMetaInfo
            createdAt={createdAt}
            approvedBy={booking.approvedBy}
            approvedByUserName={booking.approvedByUserName}
            approvedAt={approvedAt}
          />
        </div>

        {/* ============================================
            RIGHT SECTION: ACTION BUTTONS
            Chỉ hiển thị khi booking ở trạng thái Pending_Approval
            ============================================ */}
        {canApproveOrReject && (
          <ActionButtons
            bookingId={booking.bookingId}
            onApprove={onApprove}
            onReject={onReject}
          />
        )}
      </div>
    </div>
  )
}

export default BookingCard