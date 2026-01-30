import { Link } from 'react-router-dom'
import { Home, AlertCircle } from 'lucide-react'

const NotFound = () => {
  return (
    <div className="flex min-h-[60vh] flex-col items-center justify-center px-4 py-16">
      <div className="text-center">
        <div className="mb-6 inline-flex h-20 w-20 items-center justify-center rounded-full bg-orange-100">
          <AlertCircle className="h-10 w-10 text-orange-600" />
        </div>
        <h1 className="mb-3 text-5xl font-bold text-gray-900">404</h1>
        <h2 className="mb-2 text-2xl font-semibold text-gray-800">Không tìm thấy trang</h2>
        <p className="mb-8 text-gray-600">
          Trang bạn đang tìm có thể đã bị xóa, đổi tên hoặc tạm thời không khả dụng.
        </p>
        <div className="flex flex-wrap items-center justify-center gap-4">
          <Link
            to="/"
            className="inline-flex items-center gap-2 rounded-lg bg-orange-500 px-6 py-3 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors"
          >
            <Home className="h-4 w-4" />
            Về trang chủ
          </Link>
          <button
            onClick={() => window.history.back()}
            className="rounded-lg border border-gray-300 px-6 py-3 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors"
          >
            Quay lại
          </button>
        </div>
      </div>
    </div>
  )
}

export default NotFound

