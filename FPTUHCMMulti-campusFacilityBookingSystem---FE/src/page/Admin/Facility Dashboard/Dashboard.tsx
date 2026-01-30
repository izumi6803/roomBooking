/**
 * Facility Dashboard Component - Dashboard quản lý bookings
 * 
 * Component này cho phép Facility Admin quản lý bookings:
 * - Xem danh sách bookings với pagination
 * - Filter theo status
 * - Approve/Reject bookings
 * - Xem thống kê (stats cards)
 * 
 * Tính năng:
 * - Pagination: Phân trang danh sách bookings
 * - Status filter: Lọc theo trạng thái booking
 * - Approve booking: Duyệt booking
 * - Reject booking: Từ chối booking (có thể nhập lý do)
 * - Stats cards: Hiển thị thống kê tổng quan
 * - Booking cards: Hiển thị chi tiết từng booking
 * - Action modal: Modal để approve/reject với lý do
 * 
 * Status options:
 * - Draft: Bản nháp
 * - Pending_Approval: Chờ duyệt
 * - Approved: Đã duyệt
 * - Rejected: Đã từ chối
 * - Cancelled: Đã hủy
 * - Completed: Hoàn thành
 * - No_Show: Không đến
 */

// Import React hooks
import { useState, useEffect, useCallback } from 'react'
// Import icons
import { CheckCircle2, Loader2, AlertCircle, Filter } from 'lucide-react'
// Import types và API functions
import type { AdminBooking, GetBookingsParams } from './api/adminBookingApi'
import { getAdminBookings, approveBooking, rejectBooking } from './api/adminBookingApi'
// Import toast hook
import { useToast } from '../../../components/toast'
// Import components
import StatsCards from './components/StatsCards'
import BookingCard from './components/BookingCard'
import ActionModal from './components/ActionModal'
import Pagination from './components/Pagination'

/**
 * Status options theo API docs
 * 
 * Các trạng thái booking có thể filter
 */
const STATUS_OPTIONS = [
  { value: '', label: 'Tất cả' },
  { value: 'Draft', label: 'Bản nháp' },
  { value: 'Pending_Approval', label: 'Chờ duyệt' },
  { value: 'Approved', label: 'Đã duyệt' },
  { value: 'Rejected', label: 'Đã từ chối' },
  { value: 'Cancelled', label: 'Đã hủy' },
  { value: 'Completed', label: 'Hoàn thành' },
  { value: 'No_Show', label: 'Không đến' }
] as const

/**
 * Dashboard Component Function
 * 
 * Component dashboard để quản lý bookings
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa UI dashboard
 */
const Dashboard = () => {
  const { showSuccess, showError } = useToast()
  
  // State cho bookings và pagination
  const [bookings, setBookings] = useState<AdminBooking[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  // State cho pagination
  const [currentPage, setCurrentPage] = useState(1)
  const [itemsPerPage] = useState(10)
  const [totalItems, setTotalItems] = useState(0)

  // State cho filter
  const [selectedStatus, setSelectedStatus] = useState<string>('')

  // State cho modal approve/reject
  const [selectedBooking, setSelectedBooking] = useState<AdminBooking | null>(null)
  const [actionType, setActionType] = useState<'approve' | 'reject' | null>(null)
  const [rejectionReason, setRejectionReason] = useState('')
  const [showModal, setShowModal] = useState(false)
  const [actionLoading, setActionLoading] = useState(false)

  // Fetch bookings từ API
  const fetchBookings = useCallback(async () => {
    setLoading(true)
    setError(null)

    try {
      const params: GetBookingsParams = {
        page: currentPage,
        limit: itemsPerPage
      }

      // Thêm status filter nếu có
      if (selectedStatus) {
        params.status = selectedStatus as GetBookingsParams['status']
      }

      const response = await getAdminBookings(params)

      if (response.success && response.data) {
        setBookings(response.data)
        // Cập nhật total items từ pagination
        if (response.pagination) {
          setTotalItems(response.pagination.total)
        } else {
          // Nếu không có pagination info, dùng length của data
          setTotalItems(response.data.length)
        }
      } else {
        setError(response.error?.message || response.message || 'Không thể tải danh sách bookings')
        setBookings([])
      }
    } catch (err: unknown) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi tải danh sách bookings'
      console.error('Error fetching bookings:', err)
      setError(errorMessage)
      setBookings([])
    } finally {
      setLoading(false)
    }
  }, [currentPage, itemsPerPage, selectedStatus])

  // Fetch bookings khi component mount hoặc khi dependencies thay đổi
  useEffect(() => {
    fetchBookings()
  }, [fetchBookings])

  // Tính toán stats từ bookings hiện tại
  const stats = {
    total: bookings.length,
    byCampus: {
      HCM: 0, // Sẽ cần thêm thông tin campus từ API nếu có
      NVH: 0
    },
    byType: {
      'meeting-room': 0, // Sẽ cần thêm thông tin type từ API nếu có
      'lab-room': 0,
      'sports-field': 0
    }
  }

  // Handlers cho approve/reject
  const handleApprove = (bookingId: string) => {
    const booking = bookings.find((b) => b.bookingId === bookingId)
    if (booking) {
      setSelectedBooking(booking)
      setActionType('approve')
      setShowModal(true)
    }
  }

  const handleReject = (bookingId: string) => {
    const booking = bookings.find((b) => b.bookingId === bookingId)
    if (booking) {
      setSelectedBooking(booking)
      setActionType('reject')
      setRejectionReason('')
      setShowModal(true)
    }
  }

  // Confirm action (approve hoặc reject)
  const confirmAction = async () => {
    if (!selectedBooking || !actionType) return

    // Validate rejection reason nếu là reject
    if (actionType === 'reject' && !rejectionReason.trim()) {
      return
    }

    setActionLoading(true)

    try {
      if (actionType === 'approve') {
        await approveBooking(selectedBooking.bookingId)
      } else {
        await rejectBooking(selectedBooking.bookingId, rejectionReason)
      }

      // Đóng modal
      setShowModal(false)
      setSelectedBooking(null)
      setActionType(null)
      setRejectionReason('')

      // Show success toast
      showSuccess(actionType === 'approve' ? 'Đã duyệt booking thành công!' : 'Đã từ chối booking thành công!')

      // Refresh danh sách bookings
      await fetchBookings()
    } catch (err: unknown) {
      console.error('Error performing action:', err)
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi thực hiện thao tác'
      showError(errorMessage)
    } finally {
      setActionLoading(false)
    }
  }

  // Handle page change
  const handlePageChange = (page: number) => {
    setCurrentPage(page)
    // Scroll to top khi chuyển trang
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  // Handle status filter change
  const handleStatusChange = (status: string) => {
    setSelectedStatus(status)
    setCurrentPage(1) // Reset về trang 1 khi filter thay đổi
  }

  // Tính toán total pages
  const totalPages = Math.ceil(totalItems / itemsPerPage)

  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <div className="mx-auto max-w-7xl px-4">
        {/* Header */}
        <div className="mb-6">
          <h1 className="mb-2 text-3xl font-bold text-gray-900">Quản lý yêu cầu đặt cơ sở vật chất</h1>
          <p className="text-gray-600">Xem xét và quản lý tất cả các yêu cầu đặt cơ sở vật chất trong hệ thống</p>
        </div>

        {/* Stats Cards */}
        <StatsCards
          total={stats.total}
          hcm={stats.byCampus.HCM}
          nvh={stats.byCampus.NVH}
          meetingRooms={stats.byType['meeting-room']}
        />

        {/* Filter Section */}
        <div className="mb-6 rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
          <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
            <div className="flex items-center gap-2">
              <Filter className="h-5 w-5 text-gray-500" />
              <label htmlFor="status-filter" className="text-sm font-semibold text-gray-700">
                Lọc theo trạng thái:
              </label>
            </div>
            <select
              id="status-filter"
              value={selectedStatus}
              onChange={(e) => handleStatusChange(e.target.value)}
              className="rounded-lg border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
            >
              {STATUS_OPTIONS.map((option) => (
                <option key={option.value} value={option.value}>
                  {option.label}
                </option>
              ))}
            </select>
          </div>
        </div>

        {/* Loading State */}
        {loading && (
          <div className="flex flex-col items-center justify-center rounded-lg border border-gray-200 bg-white p-12">
            <Loader2 className="h-8 w-8 animate-spin text-orange-500" />
            <p className="mt-4 text-sm text-gray-600">Đang tải danh sách bookings...</p>
          </div>
        )}

        {/* Error State */}
        {!loading && error && (
          <div className="rounded-lg border border-red-200 bg-red-50 p-6">
            <div className="flex items-center gap-3">
              <AlertCircle className="h-5 w-5 text-red-600" />
              <div>
                <h3 className="text-sm font-semibold text-red-800">Lỗi khi tải dữ liệu</h3>
                <p className="mt-1 text-sm text-red-600">{error}</p>
              </div>
            </div>
            <button
              onClick={fetchBookings}
              className="mt-4 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white hover:bg-red-700 transition-colors"
            >
              Thử lại
            </button>
          </div>
        )}

        {/* Empty State */}
        {!loading && !error && bookings.length === 0 && (
          <div className="rounded-lg border border-gray-200 bg-white p-12 text-center shadow-sm">
            <CheckCircle2 className="mx-auto h-12 w-12 text-green-500" />
            <h3 className="mt-4 text-lg font-semibold text-gray-900">
              {selectedStatus ? 'Không có booking nào với trạng thái này' : 'Không có booking nào'}
            </h3>
            <p className="mt-2 text-sm text-gray-600">
              {selectedStatus
                ? 'Thử chọn trạng thái khác hoặc xóa bộ lọc để xem tất cả bookings.'
                : 'Hiện tại không có booking nào trong hệ thống.'}
            </p>
          </div>
        )}

        {/* Bookings List */}
        {!loading && !error && bookings.length > 0 && (
          <>
            <div className="space-y-4">
              {bookings.map((booking) => (
                <BookingCard
                  key={booking.bookingId}
                  booking={booking}
                  onApprove={handleApprove}
                  onReject={handleReject}
                />
              ))}
            </div>

            {/* Pagination */}
            {totalPages > 1 && (
              <div className="mt-6">
                <Pagination
                  currentPage={currentPage}
                  totalPages={totalPages}
                  onPageChange={handlePageChange}
                  totalItems={totalItems}
                  itemsPerPage={itemsPerPage}
                />
              </div>
            )}
          </>
        )}
      </div>

      {/* Action Modal */}
      <ActionModal
        show={showModal}
        booking={selectedBooking}
        actionType={actionType}
        rejectionReason={rejectionReason}
        onRejectionReasonChange={setRejectionReason}
        onConfirm={confirmAction}
        onCancel={() => {
          setShowModal(false)
          setSelectedBooking(null)
          setActionType(null)
          setRejectionReason('')
        }}
        loading={actionLoading}
      />
    </div>
  )
}

export default Dashboard
