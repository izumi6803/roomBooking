/**
 * CampusForm Component - Form tạo/chỉnh sửa Campus
 * 
 * Component này hiển thị modal form để:
 * - Tạo campus mới
 * - Chỉnh sửa campus hiện có
 * 
 * Tính năng:
 * - Validation email format
 * - Validation phone number format
 * - Auto-format phone number input (chỉ cho phép số và ký tự đặc biệt)
 * - Status selection (Active/Inactive)
 * - Loading state khi submit
 * 
 * Fields:
 * - name: Tên campus (required)
 * - address: Địa chỉ (required)
 * - phoneNumber: Số điện thoại (required)
 * - email: Email (required, validated)
 * - status: Trạng thái (Active/Inactive, required)
 */

// Import icons
import { Building2, MapPin, Phone, Mail, X, Loader2 } from 'lucide-react'
// Import types
import type { Campus, CampusRequest } from '../api/campusApi'

/**
 * Interface định nghĩa props của CampusForm
 * 
 * @property {Campus | null} campus - Campus object nếu đang chỉnh sửa, null nếu tạo mới
 * @property {() => void} onClose - Callback khi đóng form
 * @property {(campusData: CampusRequest) => Promise<void>} onSave - Callback khi submit form (async)
 * @property {boolean} loading - Trạng thái loading (optional, default: false)
 */
interface CampusFormProps {
  campus: Campus | null
  onClose: () => void
  onSave: (campusData: CampusRequest) => Promise<void>
  loading?: boolean
}

/**
 * CampusForm Component Function
 * 
 * Modal form component để tạo hoặc chỉnh sửa campus
 * 
 * @param {CampusFormProps} props - Props của component
 * @returns {JSX.Element} - JSX element chứa form modal
 */
const CampusForm = ({ campus, onClose, onSave, loading = false }: CampusFormProps) => {
  // Kiểm tra xem đang edit hay create
  // isEdit = true nếu campus không null (đang chỉnh sửa)
  // isEdit = false nếu campus null (đang tạo mới)
  const isEdit = campus !== null

  /**
   * Function: Handle khi submit form
   * 
   * Xử lý submit form:
   * 1. Prevent default form submission
   * 2. Lấy data từ form
   * 3. Validate email và phone number
   * 4. Gọi onSave callback
   * 
   * @param {React.FormEvent<HTMLFormElement>} e - Form submit event
   */
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    
    // Lấy data từ form bằng FormData API
    const formData = new FormData(e.currentTarget)

    // Tạo CampusRequest object từ form data
    const campusData: CampusRequest = {
      name: formData.get('name')?.toString() || '', // Lấy name, fallback về empty string
      address: formData.get('address')?.toString() || '',
      phoneNumber: formData.get('phoneNumber')?.toString() || '',
      email: formData.get('email')?.toString() || '',
      status: (formData.get('status')?.toString() || 'Active') as 'Active' | 'Inactive', // Default: 'Active'
    }

    // Validate email format
    // Regex pattern: phải có @ và domain với ít nhất 1 dấu chấm
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    if (!emailRegex.test(campusData.email)) {
      alert('Email không hợp lệ. Vui lòng nhập email đúng định dạng.')
      return // Dừng lại, không submit
    }

    // Validate phone number (basic validation)
    // Chỉ cho phép số và các ký tự: +, -, khoảng trắng, (, )
    const phoneRegex = /^[0-9+\-\s()]+$/
    if (!phoneRegex.test(campusData.phoneNumber)) {
      alert('Số điện thoại không hợp lệ. Vui lòng chỉ nhập số và các ký tự +, -, (, ).')
      return // Dừng lại
    }

    try {
      // Gọi onSave callback (async)
      await onSave(campusData)
      // Nếu thành công, parent component sẽ đóng form
    } catch (error) {
      // Error đã được xử lý trong parent component (CampusManagement)
      // Chỉ log để debug
      console.error('Error saving campus:', error)
    }
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
      <div className="w-full max-w-2xl rounded-lg border border-gray-200 bg-white shadow-xl">
        <div className="flex items-center justify-between border-b border-gray-200 p-6">
          <h2 className="text-xl font-semibold text-gray-900">
            {isEdit ? 'Chỉnh sửa campus' : 'Thêm campus mới'}
          </h2>
          <button
            onClick={onClose}
            disabled={loading}
            className="rounded-lg p-1 text-gray-400 hover:bg-gray-100 hover:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <X className="h-5 w-5" />
          </button>
        </div>

        <form onSubmit={handleSubmit} className="p-6">
          <div className="space-y-4">
            <div>
              <label className="mb-2 block text-sm font-semibold text-gray-700">
                <Building2 className="mr-1 inline h-4 w-4" />
                Tên campus <span className="text-red-500">*</span>
              </label>
              <input
                type="text"
                name="name"
                defaultValue={campus?.name || ''}
                required
                disabled={loading}
                placeholder="VD: Campus Hồ Chí Minh"
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
              />
            </div>

            <div>
              <label className="mb-2 block text-sm font-semibold text-gray-700">
                <MapPin className="mr-1 inline h-4 w-4" />
                Địa chỉ <span className="text-red-500">*</span>
              </label>
              <textarea
                name="address"
                defaultValue={campus?.address || ''}
                required
                disabled={loading}
                rows={3}
                placeholder="VD: 123 Đường ABC, Quận XYZ, TP. Hồ Chí Minh"
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
              />
            </div>

            <div className="grid gap-4 sm:grid-cols-2">
              <div>
                <label className="mb-2 block text-sm font-semibold text-gray-700">
                  <Phone className="mr-1 inline h-4 w-4" />
                  Số điện thoại <span className="text-red-500">*</span>
                </label>
                <input
                  type="tel"
                  name="phoneNumber"
                  defaultValue={campus?.phoneNumber || ''}
                  required
                  disabled={loading}
                  placeholder="VD: 028 7300 1866"
                  onInput={(e) => {
                    const target = e.target as HTMLInputElement;
                    target.value = target.value.replace(/[^0-9+\-\s()]/g, '');
                  }}
                  className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                />
              </div>

              <div>
                <label className="mb-2 block text-sm font-semibold text-gray-700">
                  <Mail className="mr-1 inline h-4 w-4" />
                  Email <span className="text-red-500">*</span>
                </label>
                <input
                  type="email"
                  name="email"
                  defaultValue={campus?.email || ''}
                  required
                  disabled={loading}
                  placeholder="VD: contact@fpt.edu.vn"
                  className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                />
              </div>
            </div>

            <div>
              <label className="mb-2 block text-sm font-semibold text-gray-700">
                Trạng thái <span className="text-red-500">*</span>
              </label>
              <select
                name="status"
                defaultValue={campus?.status || 'Active'}
                required
                disabled={loading}
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
              >
                <option value="Active">Active</option>
                <option value="Inactive">Inactive</option>
              </select>
              <p className="mt-1 text-xs text-gray-500">
                Active: Campus đang hoạt động | Inactive: Campus đã bị vô hiệu hóa
              </p>
            </div>
          </div>

          <div className="mt-6 flex justify-end gap-3">
            <button
              type="button"
              onClick={onClose}
              disabled={loading}
              className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            >
              Hủy
            </button>
            <button
              type="submit"
              disabled={loading}
              className="flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {loading && <Loader2 className="h-4 w-4 animate-spin" />}
              {isEdit ? 'Cập nhật' : 'Tạo mới'}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}

export default CampusForm

