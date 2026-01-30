import { useState } from 'react'
import { Clock, Image, LogIn, LogOut, XCircle } from 'lucide-react'
import { parseDateString, formatDate, formatTime } from '../utils/dateUtils'
import type { AdminBooking } from '../api/adminBookingApi'

interface CheckInOutImagesSectionProps {
  booking: AdminBooking
}

/**
 * Component hiển thị ảnh check-in/check-out với tab switching
 * 
 * @description Component này:
 *   - Hiển thị tab để chuyển đổi giữa ảnh check-in và check-out
 *   - Hiển thị thời gian và ghi chú cho mỗi loại
 *   - Có modal để xem ảnh full size khi click
 *   - Auto-select tab có ảnh nếu tab hiện tại không có
 */
export const CheckInOutImagesSection = ({ booking }: CheckInOutImagesSectionProps) => {
  // State để quản lý ảnh đang được chọn để xem full size
  const [selectedImage, setSelectedImage] = useState<string | null>(null)
  // State để quản lý tab đang active (check-in hoặc check-out)
  const [activeTab, setActiveTab] = useState<'check-in' | 'check-out'>('check-in')

  // Kiểm tra xem có ảnh check-in không
  const hasCheckInImages = booking.checkInImages && booking.checkInImages.length > 0
  // Kiểm tra xem có ảnh check-out không
  const hasCheckOutImages = booking.checkOutImages && booking.checkOutImages.length > 0

  // Auto-select tab: nếu tab hiện tại không có ảnh, chuyển sang tab kia
  const currentTab =
    activeTab === 'check-in' && !hasCheckInImages
      ? 'check-out'
      : activeTab === 'check-out' && !hasCheckOutImages
        ? 'check-in'
        : activeTab

  // Nếu không có ảnh nào → không render gì cả
  if (!hasCheckInImages && !hasCheckOutImages) {
    return null
  }

  return (
    <>
      <div className="rounded-lg border border-blue-200 bg-blue-50 p-3">
        {/* Header với tabs */}
        <div className="mb-3 flex items-center justify-between">
          <div className="flex items-center gap-2">
            <Image className="h-4 w-4 text-blue-600" />
            <p className="text-xs font-semibold text-blue-700">Ảnh xác nhận</p>
          </div>

          {/* Tab buttons */}
          <div className="flex gap-1">
            {/* Tab Check-in */}
            {hasCheckInImages && (
              <button
                onClick={() => setActiveTab('check-in')}
                className={`flex items-center gap-1 px-2 py-1 rounded text-xs font-medium transition-colors ${
                  currentTab === 'check-in'
                    ? 'bg-blue-600 text-white'
                    : 'bg-white text-blue-600 hover:bg-blue-100'
                }`}
              >
                <LogIn className="h-3 w-3" />
                Check-in ({booking.checkInImages?.length})
              </button>
            )}

            {/* Tab Check-out */}
            {hasCheckOutImages && (
              <button
                onClick={() => setActiveTab('check-out')}
                className={`flex items-center gap-1 px-2 py-1 rounded text-xs font-medium transition-colors ${
                  currentTab === 'check-out'
                    ? 'bg-purple-600 text-white'
                    : 'bg-white text-purple-600 hover:bg-purple-100'
                }`}
              >
                <LogOut className="h-3 w-3" />
                Check-out ({booking.checkOutImages?.length})
              </button>
            )}
          </div>
        </div>

        {/* Content Check-in */}
        {currentTab === 'check-in' && hasCheckInImages && (
          <ImageTabContent
            type="check-in"
            images={booking.checkInImages || []}
            timestamp={booking.checkInTime}
            note={booking.checkInNote}
            onImageClick={setSelectedImage}
          />
        )}

        {/* Content Check-out */}
        {currentTab === 'check-out' && hasCheckOutImages && (
          <ImageTabContent
            type="check-out"
            images={booking.checkOutImages || []}
            timestamp={booking.checkOutTime}
            note={booking.checkOutNote}
            onImageClick={setSelectedImage}
          />
        )}
      </div>

      {/* Modal xem ảnh full size */}
      {selectedImage && (
        <ImageModal imageUrl={selectedImage} onClose={() => setSelectedImage(null)} />
      )}
    </>
  )
}

/**
 * Component hiển thị nội dung của một tab (check-in hoặc check-out)
 */
interface ImageTabContentProps {
  type: 'check-in' | 'check-out'
  images: string[]
  timestamp?: string | null
  note?: string | null
  onImageClick: (imageUrl: string) => void
}

const ImageTabContent = ({
  type,
  images,
  timestamp,
  note,
  onImageClick,
}: ImageTabContentProps) => {
  // Màu sắc và label tùy theo type
  const colors = {
    'check-in': {
      text: 'text-blue-600',
      note: 'text-blue-700',
      border: 'border-blue-200',
      hoverBorder: 'hover:border-blue-400',
    },
    'check-out': {
      text: 'text-purple-600',
      note: 'text-purple-700',
      border: 'border-purple-200',
      hoverBorder: 'hover:border-purple-400',
    },
  }

  const label = type === 'check-in' ? 'Check-in' : 'Check-out'
  const colorSet = colors[type]

  // Parse thời gian từ backend
  const date = parseDateString(timestamp)

  return (
    <div>
      {/* Hiển thị thời gian check-in/check-out */}
      {timestamp && (
        <div className={`flex items-center gap-2 text-xs ${colorSet.text} mb-2`}>
          <Clock className="h-3 w-3" />
          <span>
            {label} lúc {formatTime(date)} ngày {formatDate(date)}
          </span>
        </div>
      )}

      {/* Hiển thị ghi chú (nếu có) */}
      {note && (
        <p className={`text-xs ${colorSet.note} italic mb-2`}>Ghi chú: "{note}"</p>
      )}

      {/* Grid ảnh: responsive 3-4-5 columns */}
      <div className="grid grid-cols-3 sm:grid-cols-4 md:grid-cols-5 gap-2">
        {images.map((img, idx) => (
          <div
            key={idx}
            className="relative cursor-pointer group"
            onClick={() => onImageClick(img)}
          >
            <img
              src={img}
              alt={`${label} ${idx + 1}`}
              className={`w-full h-16 object-cover rounded-lg border ${colorSet.border} ${colorSet.hoverBorder} transition-colors`}
            />
            {/* Hover overlay */}
            <div className="absolute inset-0 bg-black/0 group-hover:bg-black/20 rounded-lg transition-colors flex items-center justify-center">
              <span className="text-white text-xs opacity-0 group-hover:opacity-100">
                Xem
              </span>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

/**
 * Modal hiển thị ảnh full size
 */
interface ImageModalProps {
  imageUrl: string
  onClose: () => void
}

const ImageModal = ({ imageUrl, onClose }: ImageModalProps) => {
  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/80 p-4"
      onClick={onClose}
    >
      <div className="relative max-w-4xl max-h-[90vh]">
        {/* Nút đóng */}
        <button
          onClick={onClose}
          className="absolute -top-10 right-0 text-white hover:text-gray-300 transition-colors"
          aria-label="Đóng"
        >
          <XCircle className="h-8 w-8" />
        </button>
        {/* Ảnh full size */}
        <img
          src={imageUrl}
          alt="Preview"
          className="max-w-full max-h-[85vh] object-contain rounded-lg"
        />
      </div>
    </div>
  )
}
