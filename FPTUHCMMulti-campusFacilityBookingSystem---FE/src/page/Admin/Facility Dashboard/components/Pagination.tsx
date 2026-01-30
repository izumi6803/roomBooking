import { ChevronLeft, ChevronRight } from 'lucide-react'

interface PaginationProps {
  currentPage: number
  totalPages: number
  onPageChange: (page: number) => void
  totalItems: number
  itemsPerPage: number
}

const Pagination = ({ currentPage, totalPages, onPageChange, totalItems, itemsPerPage }: PaginationProps) => {
  // Tính toán range của items hiện tại
  const startItem = totalItems === 0 ? 0 : (currentPage - 1) * itemsPerPage + 1
  const endItem = Math.min(currentPage * itemsPerPage, totalItems)

  // Tạo danh sách các trang để hiển thị
  const getPageNumbers = () => {
    const pages: (number | string)[] = []
    const maxVisiblePages = 5

    if (totalPages <= maxVisiblePages) {
      // Nếu tổng số trang <= 5, hiển thị tất cả
      for (let i = 1; i <= totalPages; i++) {
        pages.push(i)
      }
    } else {
      // Luôn hiển thị trang đầu
      pages.push(1)

      // Tính toán range các trang ở giữa
      let start = Math.max(2, currentPage - 1)
      let end = Math.min(totalPages - 1, currentPage + 1)

      // Điều chỉnh nếu đang ở đầu hoặc cuối
      if (currentPage <= 3) {
        end = 4
      } else if (currentPage >= totalPages - 2) {
        start = totalPages - 3
      }

      // Thêm ellipsis nếu cần
      if (start > 2) {
        pages.push('...')
      }

      // Thêm các trang ở giữa
      for (let i = start; i <= end; i++) {
        pages.push(i)
      }

      // Thêm ellipsis nếu cần
      if (end < totalPages - 1) {
        pages.push('...')
      }

      // Luôn hiển thị trang cuối
      pages.push(totalPages)
    }

    return pages
  }

  if (totalPages <= 1) {
    return null // Không hiển thị pagination nếu chỉ có 1 trang hoặc không có dữ liệu
  }

  return (
    <div className="flex flex-col items-center justify-between gap-4 border-t border-gray-200 bg-white px-4 py-4 sm:flex-row sm:px-6">
      {/* Thông tin số items */}
      <div className="text-sm text-gray-700">
        Hiển thị <span className="font-semibold">{startItem}</span> đến{' '}
        <span className="font-semibold">{endItem}</span> trong tổng số{' '}
        <span className="font-semibold">{totalItems}</span> kết quả
      </div>

      {/* Navigation buttons */}
      <div className="flex items-center gap-2">
        {/* Previous button */}
        <button
          onClick={() => onPageChange(currentPage - 1)}
          disabled={currentPage === 1}
          className="flex items-center gap-1 rounded-lg border border-gray-300 bg-white px-3 py-2 text-sm font-semibold text-gray-700 transition-colors hover:bg-gray-50 disabled:cursor-not-allowed disabled:opacity-50 disabled:hover:bg-white"
        >
          <ChevronLeft className="h-4 w-4" />
          Trước
        </button>

        {/* Page numbers */}
        <div className="flex items-center gap-1">
          {getPageNumbers().map((page, index) => {
            if (page === '...') {
              return (
                <span key={`ellipsis-${index}`} className="px-2 text-gray-500">
                  ...
                </span>
              )
            }

            const pageNum = page as number
            return (
              <button
                key={pageNum}
                onClick={() => onPageChange(pageNum)}
                className={`min-w-[2.5rem] rounded-lg border px-3 py-2 text-sm font-semibold transition-colors ${
                  currentPage === pageNum
                    ? 'border-orange-500 bg-orange-50 text-orange-700'
                    : 'border-gray-300 bg-white text-gray-700 hover:bg-gray-50'
                }`}
              >
                {pageNum}
              </button>
            )
          })}
        </div>

        {/* Next button */}
        <button
          onClick={() => onPageChange(currentPage + 1)}
          disabled={currentPage === totalPages}
          className="flex items-center gap-1 rounded-lg border border-gray-300 bg-white px-3 py-2 text-sm font-semibold text-gray-700 transition-colors hover:bg-gray-50 disabled:cursor-not-allowed disabled:opacity-50 disabled:hover:bg-white"
        >
          Sau
          <ChevronRight className="h-4 w-4" />
        </button>
      </div>
    </div>
  )
}

export default Pagination

