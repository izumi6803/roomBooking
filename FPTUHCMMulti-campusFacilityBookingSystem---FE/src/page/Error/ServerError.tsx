import { Link } from 'react-router-dom'
import { ServerCrash, Home, RefreshCw } from 'lucide-react'

const ServerError = () => {
  return (
    <div className="flex min-h-[60vh] flex-col items-center justify-center px-4 py-16">
      <div className="text-center">
        <div className="mb-6 inline-flex h-20 w-20 items-center justify-center rounded-full bg-red-100">
          <ServerCrash className="h-10 w-10 text-red-600" />
        </div>
        <h1 className="mb-3 text-5xl font-bold text-gray-900">500</h1>
        <h2 className="mb-2 text-2xl font-semibold text-gray-800">Lỗi máy chủ nội bộ</h2>
        <p className="mb-8 max-w-md mx-auto text-gray-600">
          Đã xảy ra lỗi ở phía chúng tôi. Chúng tôi đang làm việc để khắc phục sự cố. Vui lòng thử lại sau.
        </p>
        <div className="flex flex-wrap items-center justify-center gap-4">
          <button
            onClick={() => window.location.reload()}
            className="inline-flex items-center gap-2 rounded-lg bg-orange-500 px-6 py-3 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors"
          >
            <RefreshCw className="h-4 w-4" />
            Tải lại trang
          </button>
          <Link
            to="/"
            className="inline-flex items-center gap-2 rounded-lg border border-gray-300 px-6 py-3 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors"
          >
            <Home className="h-4 w-4" />
            Về trang chủ
          </Link>
        </div>
      </div>
    </div>
  )
}

export default ServerError

