/**
 * CampusManagement Component - Quản lý Campus
 * 
 * Component này cho phép admin quản lý tất cả các campus trong hệ thống:
 * - Xem danh sách campuses với pagination
 * - Tạo campus mới
 * - Chỉnh sửa campus
 * - Vô hiệu hóa campus (soft delete)
 * - Filter và search
 * 
 * Tính năng:
 * - Pagination: Phân trang danh sách campuses
 * - CRUD operations: Create, Read, Update, Delete (soft delete)
 * - Form modal: Sử dụng CampusForm component
 * - Delete confirmation: Modal xác nhận trước khi vô hiệu hóa
 * - Status badges: Hiển thị trạng thái Active/Inactive
 * - Date formatting: Format date từ nhiều định dạng khác nhau
 */

// Import React hooks
import { useState, useEffect, useCallback } from 'react'
// Import icons từ lucide-react
import { Plus, Edit2, Trash2, Loader2, AlertCircle, Building2 } from 'lucide-react'
// Import types và API functions
import type { Campus, CampusRequest, GetCampusesParams } from './api/campusApi'
import { getCampuses, createCampus, updateCampus, deleteCampus } from './api/campusApi'
// Import toast hook
import { useToast } from '../../../components/toast'
// Import components
import CampusForm from './components/CampusForm'
import Pagination from '../Facility Dashboard/components/Pagination'

/**
 * CampusManagement Component Function
 * 
 * Component chính để quản lý campuses
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa UI quản lý campuses
 */
const CampusManagement = () => {
  // Lấy các function từ toast hook
  const { showSuccess, showError } = useToast()
  
  // State cho campuses và pagination
  const [campuses, setCampuses] = useState<Campus[]>([]) // Danh sách campuses
  const [loading, setLoading] = useState(true) // Trạng thái loading
  const [error, setError] = useState<string | null>(null) // Thông báo lỗi

  // State cho pagination
  const [currentPage, setCurrentPage] = useState(1) // Trang hiện tại
  const [itemsPerPage] = useState(10) // Số items trên mỗi trang (constant)
  const [totalItems, setTotalItems] = useState(0) // Tổng số items

  // State cho form và actions
  const [showForm, setShowForm] = useState(false) // Hiển thị form modal
  const [editingCampus, setEditingCampus] = useState<Campus | null>(null) // Campus đang chỉnh sửa (null nếu tạo mới)
  const [formLoading, setFormLoading] = useState(false) // Trạng thái loading khi submit form
  const [deleteConfirm, setDeleteConfirm] = useState<string | null>(null) // Campus ID cần xác nhận xóa
  const [deleteLoading, setDeleteLoading] = useState(false) // Trạng thái loading khi xóa

  /**
   * Function: Fetch campuses từ API
   * 
   * Giải thích câu 5: "tránh rerender cho hàm"
   * 
   * TẠI SAO DÙNG useCallback?
   * 
   * 1. Vấn đề không dùng useCallback:
   *    - Mỗi lần component re-render, function fetchCampuses sẽ được tạo mới
   *    - useEffect([fetchCampuses]) sẽ thấy fetchCampuses là function mới → chạy lại
   *    - → Infinite loop hoặc re-render không cần thiết
   * 
   * 2. Giải pháp với useCallback:
   *    - useCallback memoize (cache) function, chỉ tạo mới khi dependencies thay đổi
   *    - Dependencies: [currentPage, itemsPerPage]
   *    - → Function chỉ được tạo mới khi currentPage hoặc itemsPerPage thay đổi
   *    - → useEffect chỉ chạy lại khi thực sự cần (khi dependencies thay đổi)
   * 
   * 3. Lợi ích:
   *    - Tránh infinite loop
   *    - Tối ưu performance (không tạo function mới mỗi lần render)
   *    - Predictable behavior (dễ debug hơn)
   * 
   * 4. Khi nào nên dùng useCallback:
   *    - Function được dùng trong dependency array của useEffect/useMemo
   *    - Function được pass xuống child component (tránh child re-render)
   *    - Function có logic phức tạp (tốn thời gian tạo)
   * 
   * Gọi API để lấy danh sách campuses với pagination
   * 
   * @returns {Promise<void>} - Promise không trả về giá trị
   */
  const fetchCampuses = useCallback(async () => {
    setLoading(true)
    setError(null)

    try {
      // Tạo params object
      const params: GetCampusesParams = {
        page: currentPage,
        limit: itemsPerPage,
      }

      // Gọi API
      const response = await getCampuses(params)

      if (response.success && response.data) {
        // Thành công: Lưu campuses vào state
        setCampuses(response.data)
        
        // Cập nhật total items từ pagination
        if (response.pagination) {
          setTotalItems(response.pagination.total)
        } else {
          // Fallback: Nếu không có pagination info, dùng length của data
          setTotalItems(response.data.length)
        }
      } else {
        // Thất bại: Hiển thị error và clear data
        setError(response.error?.message || response.message || 'Không thể tải danh sách campuses')
        setCampuses([])
      }
    } catch (err) {
      // Xử lý exception
      console.error('Error fetching campuses:', err)
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi tải danh sách campuses'
      setError(errorMessage)
      setCampuses([])
    } finally {
      setLoading(false)
    }
  }, [currentPage, itemsPerPage]) // Dependencies: Chạy lại khi currentPage hoặc itemsPerPage thay đổi

  /**
   * useEffect: Fetch campuses khi component mount hoặc khi dependencies thay đổi
   * 
   * Side effect này chạy khi:
   * - Component mount lần đầu
   * - fetchCampuses function thay đổi (khi currentPage hoặc itemsPerPage thay đổi)
   */
  useEffect(() => {
    fetchCampuses()
  }, [fetchCampuses]) // Dependency: fetchCampuses function

  /**
   * Function: Handle khi user chuyển trang
   * 
   * @param {number} page - Số trang mới
   */
  const handlePageChange = (page: number) => {
    setCurrentPage(page)
    // Scroll lên đầu trang với smooth animation
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  /**
   * Function: Handle save (create or update)
   * 
   * Xử lý cả create và update:
   * - Nếu editingCampus tồn tại: Update campus
   * - Nếu editingCampus null: Create campus mới
   * 
   * @param {CampusRequest} campusData - Dữ liệu campus cần lưu
   */
  const handleSave = async (campusData: CampusRequest) => {
    setFormLoading(true)

    try {
      if (editingCampus) {
        // Update campus: Gọi API update với campusId, data, và status
        await updateCampus(editingCampus.campusId, campusData, campusData.status)
      } else {
        // Create new campus: Gọi API create
        await createCampus(campusData)
      }

      // Thành công: Đóng form, reset state, hiển thị success message, và reload danh sách
      setShowForm(false)
      setEditingCampus(null)
      showSuccess(editingCampus ? 'Cập nhật campus thành công!' : 'Tạo campus mới thành công!')
      await fetchCampuses() // Reload danh sách
    } catch (err) {
      // Thất bại: Hiển thị error
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi lưu campus'
      showError(errorMessage)
      throw err // Re-throw để form có thể xử lý (disable loading state)
    } finally {
      setFormLoading(false)
    }
  }

  /**
   * Function: Handle edit
   * 
   * Mở form modal với campus được chọn để chỉnh sửa
   * 
   * @param {Campus} campus - Campus cần chỉnh sửa
   */
  const handleEdit = (campus: Campus) => {
    setEditingCampus(campus) // Lưu campus đang chỉnh sửa
    setShowForm(true) // Hiển thị form modal
  }

  /**
   * Function: Handle delete (soft delete - vô hiệu hóa)
   * 
   * Vô hiệu hóa campus (không xóa vĩnh viễn)
   * Status sẽ được đổi thành 'Inactive'
   * 
   * @param {string} campusId - ID của campus cần vô hiệu hóa
   */
  const handleDelete = async (campusId: string) => {
    setDeleteLoading(true)

    try {
      // Gọi API delete (soft delete)
      await deleteCampus(campusId)
      
      // Thành công: Đóng modal, hiển thị success, và reload danh sách
      setDeleteConfirm(null)
      showSuccess('Vô hiệu hóa campus thành công!')
      await fetchCampuses()
    } catch (err) {
      // Thất bại: Hiển thị error
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi vô hiệu hóa campus'
      showError(errorMessage)
    } finally {
      setDeleteLoading(false)
    }
  }

  /**
   * Function: Format date an toàn với nhiều định dạng
   * 
   * Hỗ trợ nhiều định dạng date từ backend:
   * - ISO 8601 (standard)
   * - dd/MM/yyyy HH:mm:ss (từ backend)
   * 
   * @param {string | null | undefined} dateString - Date string cần format
   * @returns {string} - Formatted date string hoặc '-' nếu không hợp lệ
   */
  const formatDate = (dateString: string | null | undefined) => {
    if (!dateString) return '-'

    let date = new Date(dateString)

    // Nếu Date mặc định không parse được, thử parse theo định dạng dd/MM/yyyy HH:mm:ss
    if (isNaN(date.getTime())) {
      // Regex để match format dd/MM/yyyy HH:mm:ss
      const match = dateString.match(
        /^(\d{1,2})\/(\d{1,2})\/(\d{4})\s+(\d{1,2}):(\d{2}):(\d{2})$/
      )

      if (match) {
        // Destructure match groups
        const [, day, month, year, hour, minute, second] = match
        // Tạo Date object (month là 0-indexed nên trừ 1)
        date = new Date(
          Number(year),
          Number(month) - 1,
          Number(day),
          Number(hour),
          Number(minute),
          Number(second)
        )
      } else {
        // Nếu vẫn không parse được thì trả về nguyên chuỗi để tránh crash UI
        return dateString
      }
    }

    // Format date theo locale Việt Nam
    return new Intl.DateTimeFormat('vi-VN', {
      year: 'numeric',
      month: 'short', // Tháng dạng chữ (ví dụ: "thg 1")
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    }).format(date)
  }

  /**
   * Function: Get status badge color
   * 
   * Trả về className cho status badge dựa trên status
   * 
   * @param {string} status - Status của campus ('Active' hoặc 'Inactive')
   * @returns {string} - Tailwind CSS classes cho badge
   */
  const getStatusColor = (status: string) => {
    return status === 'Active'
      ? 'bg-green-100 text-green-700' // Màu xanh cho Active
      : 'bg-gray-100 text-gray-700' // Màu xám cho Inactive
  }

  // Tính toán total pages
  const totalPages = Math.ceil(totalItems / itemsPerPage)

  /**
   * Render UI
   * 
   * JSX structure:
   * - Header với nút "Thêm Campus"
   * - Loading state
   * - Error state
   * - Empty state
   * - Campuses table với pagination
   * - Delete confirmation modal
   * - Campus form modal
   */
  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <div className="mx-auto max-w-7xl px-4">
        {/* Header Section */}
        <div className="mb-6 flex items-center justify-between">
          <div>
            <h1 className="mb-2 text-3xl font-bold text-gray-900">Quản lý Campus</h1>
            <p className="text-gray-600">Quản lý tất cả các campus trong hệ thống</p>
          </div>
          {/* Nút "Thêm Campus" */}
          <button
            onClick={() => {
              setEditingCampus(null) // Reset editing campus
              setShowForm(true) // Hiển thị form modal
            }}
            className="flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors"
          >
            <Plus className="h-5 w-5" />
            Thêm Campus
          </button>
        </div>

        {/* Loading State */}
        {loading && (
          <div className="flex flex-col items-center justify-center rounded-lg border border-gray-200 bg-white p-12">
            <Loader2 className="h-8 w-8 animate-spin text-orange-500" />
            <p className="mt-4 text-sm text-gray-600">Đang tải danh sách campuses...</p>
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
            {/* Nút "Thử lại" */}
            <button
              onClick={fetchCampuses}
              className="mt-4 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white hover:bg-red-700 transition-colors"
            >
              Thử lại
            </button>
          </div>
        )}

        {/* Empty State */}
        {!loading && !error && campuses.length === 0 && (
          <div className="rounded-lg border border-gray-200 bg-white p-12 text-center shadow-sm">
            <Building2 className="mx-auto h-12 w-12 text-gray-400" />
            <h3 className="mt-4 text-lg font-semibold text-gray-900">Chưa có campus nào</h3>
            <p className="mt-2 text-sm text-gray-600">
              Bắt đầu bằng cách thêm campus đầu tiên vào hệ thống.
            </p>
            {/* Nút "Thêm Campus" trong empty state */}
            <button
              onClick={() => {
                setEditingCampus(null)
                setShowForm(true)
              }}
              className="mt-4 flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors mx-auto"
            >
              <Plus className="h-5 w-5" />
              Thêm Campus
            </button>
          </div>
        )}

        {/* Campuses Table */}
        {!loading && !error && campuses.length > 0 && (
          <>
            <div className="overflow-hidden rounded-lg border border-gray-200 bg-white shadow-sm">
              <div className="overflow-x-auto">
                <table className="min-w-full divide-y divide-gray-200">
                  <thead className="bg-gray-50">
                    <tr>
                      <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                        Tên Campus
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                        Địa chỉ
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                        Liên hệ
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                        Trạng thái
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                        Ngày tạo
                      </th>
                      <th className="px-6 py-3 text-right text-xs font-semibold uppercase tracking-wider text-gray-700">
                        Thao tác
                      </th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-gray-200 bg-white">
                    {/* Map qua danh sách campuses để render từng row */}
                    {campuses.map((campus) => (
                      <tr key={campus.campusId} className="hover:bg-gray-50">
                        {/* Tên Campus */}
                        <td className="whitespace-nowrap px-6 py-4">
                          <div className="flex items-center">
                            <Building2 className="mr-2 h-5 w-5 text-orange-500" />
                            <div>
                              <div className="text-sm font-semibold text-gray-900">{campus.name}</div>
                              <div className="text-xs text-gray-500">ID: {campus.campusId}</div>
                            </div>
                          </div>
                        </td>
                        {/* Địa chỉ */}
                        <td className="px-6 py-4">
                          <div className="max-w-xs text-sm text-gray-900">{campus.address}</div>
                        </td>
                        {/* Liên hệ */}
                        <td className="whitespace-nowrap px-6 py-4">
                          <div className="text-sm text-gray-900">{campus.phoneNumber}</div>
                          <div className="text-xs text-gray-500">{campus.email}</div>
                        </td>
                        {/* Trạng thái */}
                        <td className="whitespace-nowrap px-6 py-4">
                          <span
                            className={`inline-flex rounded-full px-2 py-1 text-xs font-semibold ${getStatusColor(
                              campus.status
                            )}`}
                          >
                            {campus.status === 'Active' ? 'Đang hoạt động' : 'Đã vô hiệu hóa'}
                          </span>
                        </td>
                        {/* Ngày tạo */}
                        <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                          {formatDate(campus.createdAt)}
                        </td>
                        {/* Thao tác */}
                        <td className="whitespace-nowrap px-6 py-4 text-right text-sm font-medium">
                          <div className="flex items-center justify-end gap-2">
                            {/* Nút Edit */}
                            <button
                              onClick={() => handleEdit(campus)}
                              className="rounded-lg bg-blue-50 p-2 text-blue-600 hover:bg-blue-100 transition-colors"
                              title="Chỉnh sửa"
                            >
                              <Edit2 className="h-4 w-4" />
                            </button>
                            {/* Nút Delete */}
                            <button
                              onClick={() => setDeleteConfirm(campus.campusId)}
                              disabled={campus.status === 'Inactive'} // Disable nếu đã inactive
                              className="rounded-lg bg-red-50 p-2 text-red-600 hover:bg-red-100 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                              title={campus.status === 'Inactive' ? 'Đã vô hiệu hóa' : 'Vô hiệu hóa'}
                            >
                              <Trash2 className="h-4 w-4" />
                            </button>
                          </div>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
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

        {/* Delete Confirmation Modal */}
        {deleteConfirm && (
          <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
            <div className="w-full max-w-md rounded-lg border border-gray-200 bg-white p-6 shadow-xl">
              <div className="mb-4 flex items-center gap-3">
                <div className="flex h-10 w-10 items-center justify-center rounded-full bg-red-100">
                  <Trash2 className="h-5 w-5 text-red-600" />
                </div>
                <div>
                  <h3 className="text-lg font-semibold text-gray-900">Vô hiệu hóa Campus</h3>
                  <p className="text-sm text-gray-600">Bạn có chắc chắn muốn vô hiệu hóa campus này?</p>
                </div>
              </div>
              {/* Warning message */}
              <p className="mb-6 rounded-lg bg-gray-50 p-3 text-sm text-gray-700">
                <strong>Lưu ý:</strong> Đây là thao tác soft delete. Campus sẽ được đánh dấu là "Inactive" nhưng
                dữ liệu vẫn được giữ lại trong hệ thống.
              </p>
              <div className="flex justify-end gap-3">
                {/* Nút Hủy */}
                <button
                  onClick={() => setDeleteConfirm(null)}
                  disabled={deleteLoading}
                  className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  Hủy
                </button>
                {/* Nút Xác nhận */}
                <button
                  onClick={() => handleDelete(deleteConfirm)}
                  disabled={deleteLoading}
                  className="flex items-center gap-2 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-red-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  {deleteLoading && <Loader2 className="h-4 w-4 animate-spin" />}
                  Xác nhận vô hiệu hóa
                </button>
              </div>
            </div>
          </div>
        )}

        {/* Campus Form Modal */}
        {showForm && (
          <CampusForm
            campus={editingCampus} // null nếu tạo mới, Campus object nếu chỉnh sửa
            onClose={() => {
              setShowForm(false)
              setEditingCampus(null)
            }}
            onSave={handleSave}
            loading={formLoading}
          />
        )}
      </div>
    </div>
  )
}

export default CampusManagement
