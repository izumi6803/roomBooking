import { AlertTriangle, MessageSquare, Star } from 'lucide-react'
import { formatDate, formatTime, parseDateString } from '../utils/dateUtils'

interface FeedbackSectionProps {
  feedback: {
    rating: number
    comments?: string | null
    reportIssue?: boolean
    issueDescription?: string | null
    createdAt?: string | null
  }
}

/**
 * FeedbackSection Component - Hiển thị đánh giá từ người dùng sau khi sử dụng facility
 * 
 * @description Component này hiển thị:
 *   - Rating: 5 sao với số điểm
 *   - Comments: Nhận xét của người dùng (nếu có)
 *   - Issue report: Báo cáo sự cố (nếu có)
 *   - Created date: Thời gian đánh giá
 * 
 * @param feedback - Object chứa thông tin feedback từ API
 *   - rating: Điểm đánh giá (1-5)
 *   - comments: Nhận xét (optional)
 *   - reportIssue: Có báo cáo sự cố không (optional)
 *   - issueDescription: Mô tả sự cố (optional)
 *   - createdAt: Thời gian tạo (optional)
 * 
 * @example
 * <FeedbackSection feedback={{
 *   rating: 5,
 *   comments: "Rất tốt!",
 *   createdAt: "10/12/2025 14:30:00"
 * }} />
 */
export const FeedbackSection = ({ feedback }: FeedbackSectionProps) => {
  // Nếu không có feedback → không render
  if (!feedback) return null

  // Parse thời gian tạo feedback từ backend
  const feedbackDate = parseDateString(feedback.createdAt || null)

  return (
    <div className="rounded-lg border border-green-200 bg-green-50 p-3">
      <div className="mb-2 flex items-center justify-between">
        <div className="flex items-center gap-2">
          <MessageSquare className="h-4 w-4 text-green-600" />
          <p className="text-xs font-semibold text-green-700">Đánh giá của người dùng</p>
        </div>
        <div className="flex items-center gap-1">
          {[1, 2, 3, 4, 5].map((star) => (
            <Star
              key={star}
              className={`h-4 w-4 ${
                star <= feedback.rating
                  ? 'fill-yellow-400 text-yellow-400'
                  : 'text-gray-300'
              }`}
            />
          ))}
          <span className="ml-1 text-xs font-medium text-gray-600">
            ({feedback.rating}/5)
          </span>
        </div>
      </div>
      {feedback.comments && (
        <p className="text-xs text-green-700 italic mb-2">"{feedback.comments}"</p>
      )}
      {feedback.reportIssue && feedback.issueDescription && (
        <div className="mt-2 rounded-lg border border-orange-200 bg-orange-50 p-2">
          <div className="flex items-center gap-1 mb-1">
            <AlertTriangle className="h-3 w-3 text-orange-600" />
            <p className="text-xs font-semibold text-orange-700">Báo cáo sự cố</p>
          </div>
          <p className="text-xs text-orange-600">{feedback.issueDescription}</p>
        </div>
      )}
      {feedbackDate && (
        <p className="text-xs text-gray-400 mt-2">
          Đánh giá vào {formatDate(feedbackDate)} lúc {formatTime(feedbackDate)}
        </p>
      )}
    </div>
  )
}
