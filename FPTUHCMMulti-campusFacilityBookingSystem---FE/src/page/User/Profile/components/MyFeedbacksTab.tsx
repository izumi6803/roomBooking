/**
 * MyFeedbacksTab Component - Tab hiển thị lịch sử feedbacks của user
 * 
 * Component này hiển thị danh sách tất cả feedbacks mà user đã gửi:
 * - Rating (sao) với visual stars
 * - Comments/Nhận xét
 * - Issue reports (nếu có)
 * - Trạng thái xử lý (Đã xử lý / Đang xử lý)
 * - Thông tin booking liên quan
 * 
 * Tính năng:
 * - Load feedbacks từ API khi component mount
 * - Hiển thị loading state
 * - Hiển thị error state
 * - Empty state khi chưa có feedback
 * - Format date/time theo locale Việt Nam
 * - Visual star rating
 */

// Import React hooks
import { useState, useEffect } from 'react';
// Import API function và type
import { getMyFeedbacks, type MyFeedback } from '../api/myFeedbacksApi';
// Import icons
import { Loader2, MessageSquare, Star, AlertTriangle, CheckCircle } from 'lucide-react';

/**
 * MyFeedbacksTab Component Function
 * 
 * Component tab để hiển thị lịch sử feedbacks
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa danh sách feedbacks
 */
const MyFeedbacksTab = () => {
  // State lưu danh sách feedbacks
  // MyFeedback[]: Array các feedback objects
  const [feedbacks, setFeedbacks] = useState<MyFeedback[]>([]);
  
  // State quản lý trạng thái loading
  const [isLoading, setIsLoading] = useState(true);
  
  // State lưu thông báo lỗi
  const [error, setError] = useState<string | null>(null);

  /**
   * useEffect: Fetch feedbacks khi component mount
   * 
   * Side effect này chạy một lần khi component mount
   * Dependency array rỗng []: Chỉ chạy một lần
   */
  useEffect(() => {
    fetchFeedbacks();
  }, []); // Empty dependency array: Chỉ chạy một lần khi mount

  /**
   * Function: Fetch feedbacks từ API
   * 
   * Gọi API để lấy danh sách feedbacks của user hiện tại
   */
  const fetchFeedbacks = async () => {
    setIsLoading(true);
    setError(null);

    try {
      // Gọi API
      const response = await getMyFeedbacks();

      if (response.success && response.data) {
        // Thành công: Lưu feedbacks vào state
        setFeedbacks(response.data);
      } else {
        // Thất bại: Hiển thị error
        setError(response.error?.message || 'Không thể tải danh sách feedbacks');
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
   * Function: Format date/time theo locale Việt Nam
   * 
   * Chuyển đổi ISO date string thành format dễ đọc
   * Format: dd/mm/yyyy, hh:mm
   * 
   * @param {string} dateString - ISO date string (ví dụ: "2024-01-15T10:30:00Z")
   * @returns {string} - Formatted date string (ví dụ: "15/01/2024, 10:30")
   */
  const formatDateTime = (dateString: string) => {
    // Tạo Date object từ string
    const date = new Date(dateString);
    
    // Format theo locale Việt Nam
    // toLocaleString: Convert date thành string theo locale
    // 'vi-VN': Locale Việt Nam
    return date.toLocaleString('vi-VN', {
      day: '2-digit',      // Ngày: 2 chữ số (01-31)
      month: '2-digit',    // Tháng: 2 chữ số (01-12)
      year: 'numeric',     // Năm: 4 chữ số (2024)
      hour: '2-digit',     // Giờ: 2 chữ số (00-23)
      minute: '2-digit',   // Phút: 2 chữ số (00-59)
    });
  };

  /**
   * Function: Render star rating
   * 
   * Hiển thị rating dưới dạng stars (sao)
   * - Stars được fill (vàng) nếu <= rating
   * - Stars không fill (xám) nếu > rating
   * 
   * @param {number} rating - Rating từ 1-5
   * @returns {JSX.Element} - JSX element chứa stars
   */
  const renderStars = (rating: number) => {
    return (
      <div className="flex items-center gap-1">
        {/* Map qua array [1, 2, 3, 4, 5] để render 5 stars */}
        {[1, 2, 3, 4, 5].map((star) => (
          <Star
            key={star} // Key prop bắt buộc
            // Dynamic className dựa trên rating
            className={`w-4 h-4 ${
              // Nếu star <= rating: Fill vàng
              star <= rating 
                ? 'fill-yellow-400 text-yellow-400' // fill: Fill màu vàng, text: Màu viền vàng
                : 'text-gray-300' // Không fill, chỉ viền xám
            }`}
          />
        ))}
        {/* Hiển thị rating dạng số (rating/5) */}
        <span className="ml-2 text-sm text-gray-600">({rating}/5)</span>
      </div>
    );
  };

  /**
   * Render UI
   * 
   * JSX structure:
   * - Loading state
   * - Error state
   * - Empty state
   * - Feedbacks list với cards
   */
  return (
    <div className="space-y-4">
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

      {/* Empty State: Chưa có feedback nào */}
      {!isLoading && !error && feedbacks.length === 0 && (
        <div className="bg-white rounded-lg border border-gray-200 p-12 text-center">
          <MessageSquare className="w-12 h-12 text-gray-400 mx-auto mb-4" />
          <p className="text-gray-600">Bạn chưa có feedback nào</p>
        </div>
      )}

      {/* Feedbacks List */}
      {!isLoading && !error && feedbacks.length > 0 && (
        <div className="space-y-4">
          {/* Map qua array feedbacks để render từng feedback card */}
          {feedbacks.map((feedback) => (
            <div
              key={feedback.feedbackId} // Key prop
              className="bg-white rounded-lg border border-gray-200 p-6 hover:shadow-md transition-shadow"
            >
              <div className="space-y-4">
                {/* Header: Facility name và status badges */}
                <div className="flex items-start justify-between gap-4">
                  <div className="flex-1">
                    {/* Facility name */}
                    <h3 className="text-lg font-semibold text-gray-800 mb-2">
                      {feedback.facilityName}
                    </h3>
                    <div className="flex items-center gap-4 text-sm text-gray-600">
                      {/* Booking ID */}
                      <span>Booking ID: {feedback.bookingId}</span>
                      
                      {/* Status badge: Đã xử lý */}
                      {feedback.isResolved && (
                        <span className="inline-flex items-center gap-1 px-2 py-1 rounded-full text-xs font-medium bg-green-100 text-green-700">
                          <CheckCircle className="w-3 h-3" />
                          Đã xử lý
                        </span>
                      )}
                      
                      {/* Status badge: Đang xử lý (nếu có report issue và chưa resolved) */}
                      {!feedback.isResolved && feedback.reportIssue && (
                        <span className="inline-flex items-center gap-1 px-2 py-1 rounded-full text-xs font-medium bg-yellow-100 text-yellow-700">
                          <AlertTriangle className="w-3 h-3" />
                          Đang xử lý
                        </span>
                      )}
                    </div>
                  </div>
                </div>

                {/* Rating Section */}
                <div className="flex items-center gap-2">
                  <span className="text-sm font-medium text-gray-700">Đánh giá:</span>
                  {renderStars(feedback.rating)}
                </div>

                {/* Comments Section - chỉ hiển thị nếu có comments */}
                {feedback.comments && (
                  <div>
                    <p className="text-sm font-medium text-gray-700 mb-1">Nhận xét:</p>
                    <p className="text-sm text-gray-600 bg-gray-50 p-3 rounded-lg">
                      {feedback.comments}
                    </p>
                  </div>
                )}

                {/* Issue Report Section - chỉ hiển thị nếu có report issue */}
                {feedback.reportIssue && (
                  <div className="p-4 bg-yellow-50 border border-yellow-200 rounded-lg">
                    <div className="flex items-center gap-2 mb-2">
                      <AlertTriangle className="w-4 h-4 text-yellow-600" />
                      <p className="text-sm font-medium text-yellow-800">Báo cáo vấn đề</p>
                    </div>
                    {/* Issue description */}
                    {feedback.issueDescription && (
                      <p className="text-sm text-yellow-700">{feedback.issueDescription}</p>
                    )}
                    {/* Resolved timestamp */}
                    {feedback.resolvedAt && (
                      <p className="text-xs text-yellow-600 mt-2">
                        Đã xử lý lúc: {formatDateTime(feedback.resolvedAt)}
                      </p>
                    )}
                  </div>
                )}

                {/* Timestamp: Thời gian tạo feedback */}
                <div className="text-xs text-gray-500 pt-2 border-t border-gray-100">
                  Tạo lúc: {formatDateTime(feedback.createdAt)}
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default MyFeedbacksTab;
