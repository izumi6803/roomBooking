/**
 * MyBookingsTab Component - Tab hiển thị lịch sử bookings của user
 * 
 * Component này hiển thị danh sách tất cả bookings của user với các tính năng:
 * - Pagination (phân trang)
 * - Filter theo status (trạng thái)
 * - Hiển thị chi tiết booking (facility, date, time, purpose, etc.)
 * - Status badges với màu sắc và icon
 * - Hủy booking (với validation: phải còn ít nhất 24h trước ngày đặt)
 * - Cancel booking modal
 * 
 * Status types:
 * - Draft: Đang soạn thảo
 * - Pending_Approval: Chờ duyệt
 * - Approved: Đã duyệt
 * - Rejected: Bị từ chối
 * - Cancelled: Đã hủy
 * - Completed: Đã hoàn thành
 * - No_Show: Không đến
 */

// Import React hooks
import { useState, useEffect } from 'react';
// Import API functions và types
import { getMyBookings, cancelBooking, type MyBooking, type GetMyBookingsParams } from '../api/myBookingsApi';
// Import icons
import { Loader2, Calendar, MapPin, Clock, CheckCircle, XCircle, AlertCircle, X } from 'lucide-react';
// Import CancelBookingModal component
import CancelBookingModal from './CancelBookingModal';
// Import toast hook
import { useToast } from '../../../../components/toast';

/**
 * MyBookingsTab Component Function
 * 
 * Component tab để hiển thị lịch sử bookings
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa danh sách bookings với pagination và filter
 */
const MyBookingsTab = () => {
  // Lấy các function từ toast hook
  const { showSuccess, showError } = useToast();
  
  // State lưu danh sách bookings
  const [bookings, setBookings] = useState<MyBooking[]>([]);
  
  // State quản lý trạng thái loading
  const [isLoading, setIsLoading] = useState(true);
  
  // State lưu thông báo lỗi
  const [error, setError] = useState<string | null>(null);
  
  // State quản lý pagination
  const [currentPage, setCurrentPage] = useState(1); // Trang hiện tại
  const [totalPages, setTotalPages] = useState(1); // Tổng số trang
  const [totalItems, setTotalItems] = useState(0); // Tổng số items
  
  // State quản lý filter
  // GetMyBookingsParams['status'] | '': Type là status từ API hoặc empty string
  const [statusFilter, setStatusFilter] = useState<GetMyBookingsParams['status'] | ''>('');
  
  // State quản lý cancel modal
  const [cancelModalOpen, setCancelModalOpen] = useState(false); // Trạng thái mở/đóng modal
  const [selectedBooking, setSelectedBooking] = useState<MyBooking | null>(null); // Booking được chọn để hủy
  const [isCancelling, setIsCancelling] = useState(false); // Trạng thái đang hủy booking
  
  // Constant: Số items trên mỗi trang
  const limit = 10;

  /**
   * Function: Fetch bookings từ API
   * 
   * Gọi API để lấy danh sách bookings với pagination và filter
   */
  const fetchBookings = async () => {
    setIsLoading(true);
    setError(null);

    try {
      // Tạo params object
      const params: GetMyBookingsParams = {
        page: currentPage, // Trang hiện tại
        limit: limit, // Số items trên mỗi trang
      };

      // Thêm status filter nếu có
      if (statusFilter) {
        params.status = statusFilter as GetMyBookingsParams['status'];
      }

      // Gọi API
      const response = await getMyBookings(params);

      if (response.success && response.data) {
        // Thành công: Lưu bookings vào state
        setBookings(response.data);
        
        // Cập nhật pagination info nếu có
        if (response.pagination) {
          setTotalItems(response.pagination.total); // Tổng số items
          // Tính tổng số trang: Math.ceil để làm tròn lên
          // Ví dụ: 25 items / 10 per page = 2.5 -> 3 pages
          setTotalPages(Math.ceil(response.pagination.total / response.pagination.limit));
        }
      } else {
        // Thất bại: Hiển thị error
        setError(response.error?.message || 'Không thể tải danh sách bookings');
      }
    } catch (err: unknown) {
      // Xử lý exception
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi không xác định';
      setError(errorMessage);
    } finally {
      setIsLoading(false);
    }
  };

  /**
   * useEffect: Fetch bookings khi currentPage hoặc statusFilter thay đổi
   * 
   * Side effect này chạy khi:
   * - Component mount lần đầu
   * - currentPage thay đổi (user chuyển trang)
   * - statusFilter thay đổi (user thay đổi filter)
   */
  useEffect(() => {
    fetchBookings();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [currentPage, statusFilter]); // Dependency array: Chạy lại khi currentPage hoặc statusFilter thay đổi

  /**
   * Function: Handle khi user thay đổi status filter
   * 
   * @param {React.ChangeEvent<HTMLSelectElement>} e - Event object từ select
   */
  const handleStatusFilterChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    // Cập nhật status filter
    setStatusFilter(e.target.value as GetMyBookingsParams['status'] | '');
    // Reset về trang 1 khi filter thay đổi
    // Lý do: Khi filter thay đổi, số lượng items có thể thay đổi, nên reset về trang đầu
    setCurrentPage(1);
  };

  /**
   * Function: Render status badge với màu sắc và icon
   * 
   * Tạo badge hiển thị trạng thái booking với:
   * - Label tiếng Việt
   * - Màu sắc phù hợp (xám, vàng, xanh, đỏ, cam, xanh dương)
   * - Icon phù hợp
   * 
   * @param {MyBooking['status']} status - Trạng thái booking
   * @returns {JSX.Element} - JSX element chứa status badge
   */
  const getStatusBadge = (status: MyBooking['status']) => {
    // Record type: Object với keys là status values, values là config object
    const statusConfig: Record<MyBooking['status'], { label: string; className: string; icon: React.ReactNode }> = {
      Draft: {
        label: 'Đang soạn thảo',
        className: 'bg-gray-100 text-gray-700',
        icon: <AlertCircle className="w-3 h-3" />,
      },
      Pending_Approval: {
        label: 'Chờ duyệt',
        className: 'bg-yellow-100 text-yellow-700',
        icon: <Clock className="w-3 h-3" />,
      },
      Approved: {
        label: 'Đã duyệt',
        className: 'bg-green-100 text-green-700',
        icon: <CheckCircle className="w-3 h-3" />,
      },
      Rejected: {
        label: 'Bị từ chối',
        className: 'bg-red-100 text-red-700',
        icon: <XCircle className="w-3 h-3" />,
      },
      Cancelled: {
        label: 'Đã hủy',
        className: 'bg-gray-100 text-gray-700',
        icon: <XCircle className="w-3 h-3" />,
      },
      Completed: {
        label: 'Đã hoàn thành',
        className: 'bg-blue-100 text-blue-700',
        icon: <CheckCircle className="w-3 h-3" />,
      },
      No_Show: {
        label: 'Không đến',
        className: 'bg-orange-100 text-orange-700',
        icon: <AlertCircle className="w-3 h-3" />,
      },
    };

    const config = statusConfig[status] || statusConfig.Draft;

    return (
      <span className={`inline-flex items-center gap-1 px-2 py-1 rounded-full text-xs font-medium ${config.className}`}>
        {config.icon}
        {config.label}
      </span>
    );
  };

  /**
   * Function: Format date/time theo locale Việt Nam
   * 
   * Format: dd/mm/yyyy, hh:mm
   * 
   * @param {string} dateString - ISO date string
   * @returns {string} - Formatted date/time string
   */
  const formatDateTime = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleString('vi-VN', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  };

  /**
   * Function: Format date (chỉ ngày, không có giờ) theo locale Việt Nam
   * 
   * Format: dd/mm/yyyy
   * 
   * @param {string} dateString - ISO date string
   * @returns {string} - Formatted date string
   */
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('vi-VN', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
    });
  };

  /**
   * Function: Kiểm tra xem booking có thể hủy được không
   * 
   * Điều kiện để có thể hủy:
   * 1. Status phải là Draft, Pending_Approval, hoặc Approved
   * 2. Phải còn ít nhất 24 giờ (1 ngày) trước ngày booking
   * 
   * @param {MyBooking} booking - Booking object cần kiểm tra
   * @returns {boolean} - true nếu có thể hủy, false nếu không
   */
  const canCancelBooking = (booking: MyBooking): boolean => {
    // Chỉ cho phép hủy nếu status là Draft, Pending_Approval, hoặc Approved
    // Các status khác (Rejected, Cancelled, Completed, No_Show) không thể hủy
    if (!['Draft', 'Pending_Approval', 'Approved'].includes(booking.status)) {
      return false;
    }

    // Kiểm tra thời gian: phải còn ít nhất 1 ngày (24 giờ) trước ngày booking
    const now = new Date(); // Thời gian hiện tại
    const bookingStartTime = new Date(booking.startTime); // Thời gian bắt đầu booking
    
    // Tính khoảng cách thời gian (milliseconds)
    const timeDiff = bookingStartTime.getTime() - now.getTime();
    
    // Chuyển đổi sang giờ
    // 1000: milliseconds -> seconds
    // 60: seconds -> minutes
    // 60: minutes -> hours
    const hoursDiff = timeDiff / (1000 * 60 * 60);

    // Phải còn ít nhất 24 giờ (1 ngày) trước ngày booking
    return hoursDiff >= 24;
  };

  /**
   * Function: Mở modal hủy booking
   * 
   * Kiểm tra xem booking có thể hủy không trước khi mở modal
   * Nếu không thể hủy, hiển thị error message
   * 
   * @param {MyBooking} booking - Booking cần hủy
   */
  const handleOpenCancelModal = (booking: MyBooking) => {
    // Kiểm tra xem có thể hủy không
    if (!canCancelBooking(booking)) {
      // Tính toán thời gian để hiển thị error message phù hợp
      const bookingDate = new Date(booking.startTime);
      const now = new Date();
      const hoursDiff = (bookingDate.getTime() - now.getTime()) / (1000 * 60 * 60);
      
      // Hiển thị error message tùy theo lý do
      if (hoursDiff < 24) {
        showError('Bạn chỉ có thể hủy booking tối đa 1 ngày trước ngày đặt lịch');
      } else {
        showError('Booking này không thể hủy');
      }
      return; // Dừng lại, không mở modal
    }
    
    // Có thể hủy: Lưu booking được chọn và mở modal
    setSelectedBooking(booking);
    setCancelModalOpen(true);
  };

  /**
   * Function: Đóng modal hủy booking
   * 
   * Chỉ đóng được nếu không đang trong quá trình hủy (isCancelling = false)
   */
  const handleCloseCancelModal = () => {
    // Chỉ đóng được nếu không đang loading
    if (!isCancelling) {
      setCancelModalOpen(false);
      setSelectedBooking(null);
    }
  };

  /**
   * Function: Xác nhận hủy booking
   * 
   * Gọi API để hủy booking với lý do
   * Sau khi thành công, reload danh sách bookings
   * 
   * @param {string} reason - Lý do hủy booking
   */
  const handleConfirmCancel = async (reason: string) => {
    // Kiểm tra có booking được chọn không
    if (!selectedBooking) return;

    setIsCancelling(true);
    try {
      // Gọi API hủy booking
      const response = await cancelBooking(selectedBooking.bookingId, reason);
      
      if (response.success) {
        // Thành công
        showSuccess('Đã hủy booking thành công');
        
        // Đóng modal
        setCancelModalOpen(false);
        setSelectedBooking(null);
        
        // Reload danh sách bookings để cập nhật UI
        await fetchBookings();
      } else {
        // Thất bại
        showError(response.error?.message || 'Không thể hủy booking');
      }
    } catch (err: unknown) {
      // Xử lý exception
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi không xác định';
      showError(errorMessage);
    } finally {
      setIsCancelling(false);
    }
  };

  return (
    <div className="space-y-4">
      {/* Filter Section */}
      <div className="bg-white rounded-lg border border-gray-200 p-4">
        <div className="flex flex-wrap items-center gap-4">
          <div className="flex-1 min-w-[200px]">
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Lọc theo trạng thái
            </label>
            <select
              value={statusFilter}
              onChange={handleStatusFilterChange}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:border-orange-500 focus:ring-1 focus:ring-orange-500"
            >
              <option value="">Tất cả</option>
              <option value="Draft">Đang soạn thảo</option>
              <option value="Pending_Approval">Chờ duyệt</option>
              <option value="Approved">Đã duyệt</option>
              <option value="Rejected">Bị từ chối</option>
              <option value="Cancelled">Đã hủy</option>
              <option value="Completed">Đã hoàn thành</option>
              <option value="No_Show">Không đến</option>
            </select>
          </div>
        </div>
      </div>

      {/* Loading State */}
      {isLoading && (
        <div className="flex items-center justify-center py-12">
          <Loader2 className="w-8 h-8 text-orange-500 animate-spin" />
        </div>
      )}

      {/* Error State */}
      {error && !isLoading && (
        <div className="bg-red-50 border border-red-200 rounded-lg p-4">
          <p className="text-sm text-red-600">{error}</p>
        </div>
      )}

      {/* Bookings List */}
      {!isLoading && !error && bookings.length === 0 && (
        <div className="bg-white rounded-lg border border-gray-200 p-12 text-center">
          <Calendar className="w-12 h-12 text-gray-400 mx-auto mb-4" />
          <p className="text-gray-600">Bạn chưa có booking nào</p>
        </div>
      )}

      {!isLoading && !error && bookings.length > 0 && (
        <>
          <div className="space-y-4">
            {bookings.map((booking) => (
              <div
                key={booking.bookingId}
                className="bg-white rounded-lg border border-gray-200 p-6 hover:shadow-md transition-shadow"
              >
                <div className="flex flex-col md:flex-row md:items-start md:justify-between gap-4">
                  <div className="flex-1 space-y-3">
                    <div className="flex items-start justify-between gap-4">
                      <div>
                        <h3 className="text-lg font-semibold text-gray-800 mb-1">
                          {booking.facilityName}
                        </h3>
                        <p className="text-sm text-gray-600">{booking.purpose}</p>
                      </div>
                      {getStatusBadge(booking.status)}
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-2 gap-3 text-sm">
                      <div className="flex items-center gap-2 text-gray-600">
                        <Calendar className="w-4 h-4 text-orange-500" />
                        <span>
                          <strong>Ngày:</strong> {formatDate(booking.startTime)}
                        </span>
                      </div>
                      <div className="flex items-center gap-2 text-gray-600">
                        <Clock className="w-4 h-4 text-orange-500" />
                        <span>
                          <strong>Thời gian:</strong> {formatDateTime(booking.startTime)} - {formatDateTime(booking.endTime)}
                        </span>
                      </div>
                      {booking.category && (
                        <div className="flex items-center gap-2 text-gray-600">
                          <MapPin className="w-4 h-4 text-orange-500" />
                          <span>
                            <strong>Loại:</strong> {booking.category}
                          </span>
                        </div>
                      )}
                      {booking.estimatedAttendees > 0 && (
                        <div className="flex items-center gap-2 text-gray-600">
                          <span>
                            <strong>Số người tham dự:</strong> {booking.estimatedAttendees}
                          </span>
                        </div>
                      )}
                    </div>

                    {booking.specialRequirements && (
                      <div className="text-sm text-gray-600">
                        <strong>Yêu cầu đặc biệt:</strong> {booking.specialRequirements}
                      </div>
                    )}

                    {booking.rejectionReason && (
                      <div className="p-3 bg-red-50 border border-red-200 rounded-lg">
                        <p className="text-sm text-red-700">
                          <strong>Lý do từ chối:</strong> {booking.rejectionReason}
                        </p>
                      </div>
                    )}

                    <div className="text-xs text-gray-500 pt-2 border-t border-gray-100">
                      Tạo lúc: {formatDateTime(booking.createdAt)}
                    </div>

                    {/* Action Buttons */}
                    {canCancelBooking(booking) && (
                      <div className="pt-2">
                        <button
                          onClick={() => handleOpenCancelModal(booking)}
                          className="flex items-center gap-2 px-4 py-2 text-sm font-medium text-red-600 bg-red-50 border border-red-200 rounded-lg hover:bg-red-100 transition-colors"
                        >
                          <X className="w-4 h-4" />
                          Hủy booking
                        </button>
                      </div>
                    )}
                  </div>
                </div>
              </div>
            ))}
          </div>

          {/* Cancel Booking Modal */}
          {selectedBooking && (
            <CancelBookingModal
              isOpen={cancelModalOpen}
              onClose={handleCloseCancelModal}
              onConfirm={handleConfirmCancel}
              facilityName={selectedBooking.facilityName}
              bookingDate={formatDateTime(selectedBooking.startTime)}
              isLoading={isCancelling}
            />
          )}

          {/* Pagination */}
          {totalPages > 1 && (
            <div className="flex items-center justify-between bg-white rounded-lg border border-gray-200 px-4 py-3">
              <div className="text-sm text-gray-600">
                Hiển thị {(currentPage - 1) * limit + 1} - {Math.min(currentPage * limit, totalItems)} trong tổng số {totalItems} bookings
              </div>
              <div className="flex items-center gap-2">
                <button
                  onClick={() => setCurrentPage((prev) => Math.max(1, prev - 1))}
                  disabled={currentPage === 1}
                  className="px-3 py-1.5 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                >
                  Trước
                </button>
                <span className="px-3 py-1.5 text-sm font-medium text-gray-700">
                  Trang {currentPage} / {totalPages}
                </span>
                <button
                  onClick={() => setCurrentPage((prev) => Math.min(totalPages, prev + 1))}
                  disabled={currentPage === totalPages}
                  className="px-3 py-1.5 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                >
                  Sau
                </button>
              </div>
            </div>
          )}
        </>
      )}
    </div>
  );
};

export default MyBookingsTab;

