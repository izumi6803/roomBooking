/**
 * UserDashboard Component - Quản lý người dùng
 * 
 * Component này cho phép admin quản lý tất cả người dùng trong hệ thống:
 * - Xem danh sách users với pagination
 * - Filter theo nhiều tiêu chí (tên, email, role, campus, status)
 * - Xóa user (không thể xóa admin)
 * - Hiển thị thống kê (tổng số, students, lecturers, admins, active, inactive)
 * 
 * Tính năng:
 * - Pagination: Phân trang danh sách users
 * - Multiple filters: Tên, email, role, campus, status
 * - Stats cards: Hiển thị thống kê tổng quan
 * - Delete user: Xóa user (hard delete, không thể xóa admin)
 * - Role badges: Hiển thị role với màu sắc khác nhau
 * - Status badges: Hiển thị trạng thái Active/Inactive
 * - Date formatting: Format date từ nhiều định dạng
 */

// Import React hooks
import { useState, useEffect, useCallback, useMemo } from 'react'
// Import icons
import { Search, Trash2, Users, Filter, X, Loader2, AlertCircle } from 'lucide-react'
// Import types và API functions
import type { User, GetUsersParams } from './api/userApi'
import { getUsers, deleteUser } from './api/userApi'
import { getCampuses, type Campus } from '../CampusManagement/api/campusApi'
import { getRoleById, type Role } from './api/roleApi'
// Import toast hook
import { useToast } from '../../../components/toast'
// Import Pagination component
import Pagination from '../Facility Dashboard/components/Pagination'

/**
 * UserDashboard Component Function
 * 
 * Component chính để quản lý users
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa UI quản lý users
 */
const UserDashboard = () => {
  const { showSuccess, showError } = useToast()
  
  // State cho users và pagination
  const [users, setUsers] = useState<User[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  // Pagination
  const [currentPage, setCurrentPage] = useState(1)
  const [itemsPerPage] = useState(10)
  const [totalItems, setTotalItems] = useState(0)

  // Filters
  const [nameFilter, setNameFilter] = useState('')
  const [emailFilter, setEmailFilter] = useState('')
  const [roleIdFilter, setRoleIdFilter] = useState('')
  const [campusIdFilter, setCampusIdFilter] = useState('')
  const [statusFilter, setStatusFilter] = useState<'Active' | 'Inactive' | ''>('')

  // Options cho filters
  const [campuses, setCampuses] = useState<Campus[]>([])
  const [roles, setRoles] = useState<Map<string, Role>>(new Map()) // Map roleId -> Role

  // Delete confirmation
  const [deleteConfirm, setDeleteConfirm] = useState<string | null>(null)
  const [deleteLoading, setDeleteLoading] = useState(false)

  // Load campuses và roles
  useEffect(() => {
    const loadOptions = async () => {
      try {
        // Load campuses
        const campusesRes = await getCampuses({ page: 1, limit: 100 })
        if (campusesRes.success && campusesRes.data) {
          setCampuses(campusesRes.data.filter((c) => c.status === 'Active'))
        }

        // Load common roles (RL0001, RL0002, RL0003)
        const roleIds = ['RL0001', 'RL0002', 'RL0003']
        const rolesMap = new Map<string, Role>()
        
        for (const roleId of roleIds) {
          try {
            const roleRes = await getRoleById(roleId)
            if (roleRes.success && roleRes.data) {
              rolesMap.set(roleId, roleRes.data)
            }
          } catch (err) {
            console.error(`Error loading role ${roleId}:`, err)
          }
        }
        
        setRoles(rolesMap)
      } catch (error) {
        console.error('Error loading options:', error)
      }
    }

    loadOptions()
  }, [])

  // Fetch users
  const fetchUsers = useCallback(async () => {
    setLoading(true)
    setError(null)

    try {
      const params: GetUsersParams = {
        page: currentPage,
        limit: itemsPerPage,
      }

      if (nameFilter.trim()) {
        params.name = nameFilter.trim()
      }

      if (emailFilter.trim()) {
        params.email = emailFilter.trim()
      }

      if (roleIdFilter) {
        params.roleId = roleIdFilter
      }

      if (campusIdFilter) {
        params.campusId = campusIdFilter
      }

      if (statusFilter) {
        params.status = statusFilter
      }

      const response = await getUsers(params)

      if (response.success && response.data) {
        setUsers(response.data)
        if (response.pagination) {
          setTotalItems(response.pagination.total)
        } else {
          setTotalItems(response.data.length)
        }
      } else {
        setError(response.error?.message || 'Không thể tải danh sách users')
        setUsers([])
      }
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi tải danh sách users'
      setError(errorMessage)
      setUsers([])
    } finally {
      setLoading(false)
    }
  }, [currentPage, itemsPerPage, nameFilter, emailFilter, roleIdFilter, campusIdFilter, statusFilter])

  // Fetch users when component mount or dependencies change
  useEffect(() => {
    fetchUsers()
  }, [fetchUsers])

  // Handle page change
  const handlePageChange = (page: number) => {
    setCurrentPage(page)
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  // Handle filter changes - reset to page 1
  const handleFilterChange = () => {
    setCurrentPage(1)
  }

  // Handle delete user
  const handleDelete = async (userId: string) => {
    setDeleteLoading(true)

    try {
      await deleteUser(userId)
      setDeleteConfirm(null)
      showSuccess('Xóa tài khoản thành công!')
      await fetchUsers()
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi xóa user'
      showError(errorMessage)
    } finally {
      setDeleteLoading(false)
    }
  }

  // Get role name by roleId
  const getRoleName = (roleId: string): string => {
    const role = roles.get(roleId)
    return role ? role.roleName : roleId
  }

  // Format date (shorter format)
  const formatDate = (dateString: string | null): string => {
    if (!dateString) return 'Never'
    
    let date: Date
    
    // Check if date is in DD/MM/YYYY HH:mm:ss format (from API)
    if (dateString.includes('/') && dateString.match(/^\d{2}\/\d{2}\/\d{4}/)) {
      // Parse DD/MM/YYYY HH:mm:ss format
      const parts = dateString.split(' ')
      const datePart = parts[0].split('/')
      const timePart = parts[1] ? parts[1].split(':') : ['00', '00', '00']
      
      // Create date: month is 0-indexed, so subtract 1
      date = new Date(
        parseInt(datePart[2]), // year
        parseInt(datePart[1]) - 1, // month (0-indexed)
        parseInt(datePart[0]), // day
        parseInt(timePart[0]) || 0, // hour
        parseInt(timePart[1]) || 0, // minute
        parseInt(timePart[2]) || 0 // second
      )
    } else {
      // Try to parse as ISO 8601 or other standard formats
      date = new Date(dateString)
    }
    
    // Check if date is valid
    if (isNaN(date.getTime())) {
      return 'Invalid date'
    }
    
    return new Intl.DateTimeFormat('vi-VN', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    }).format(date)
  }

  // Get role badge color
  const getRoleBadgeColor = (roleId: string) => {
    switch (roleId) {
      case 'RL0001': // Student
        return 'bg-blue-100 text-blue-700'
      case 'RL0002': // Lecturer
        return 'bg-purple-100 text-purple-700'
      case 'RL0003': // Facility_Admin
        return 'bg-orange-100 text-orange-700'
      default:
        return 'bg-gray-100 text-gray-700'
    }
  }

  // Calculate stats from current users
  const stats = useMemo(() => {
    return {
      total: users.length,
      students: users.filter((u) => u.roleId === 'RL0001').length,
      lecturers: users.filter((u) => u.roleId === 'RL0002').length,
      admins: users.filter((u) => u.roleId === 'RL0003').length,
      active: users.filter((u) => u.status === 'Active').length,
      inactive: users.filter((u) => u.status === 'Inactive').length,
    }
  }, [users])

  const totalPages = Math.ceil(totalItems / itemsPerPage)

  // Get user by ID for delete confirmation
  const getUserById = (userId: string): User | undefined => {
    return users.find((u) => u.userId === userId)
  }

  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <div className="mx-auto max-w-7xl px-4">
        {/* Header */}
        <div className="mb-6">
          <h1 className="mb-2 text-3xl font-bold text-gray-900">Quản lý người dùng</h1>
          <p className="text-gray-600">Quản lý và theo dõi tất cả người dùng hệ thống</p>
        </div>

        {/* Stats Cards */}
        <div className="mb-6 grid gap-4 md:grid-cols-2 lg:grid-cols-6">
          <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
            <div className="flex items-center gap-2 text-sm text-gray-600">
              <Users className="h-4 w-4" />
              <span>Tổng người dùng</span>
            </div>
            <p className="mt-1 text-2xl font-bold text-gray-900">{stats.total}</p>
          </div>
          <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
            <div className="text-sm text-gray-600">Sinh viên</div>
            <p className="mt-1 text-2xl font-bold text-blue-600">{stats.students}</p>
          </div>
          <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
            <div className="text-sm text-gray-600">Giảng viên</div>
            <p className="mt-1 text-2xl font-bold text-purple-600">{stats.lecturers}</p>
          </div>
          <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
            <div className="text-sm text-gray-600">Quản trị viên</div>
            <p className="mt-1 text-2xl font-bold text-orange-600">{stats.admins}</p>
          </div>
          <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
            <div className="text-sm text-gray-600">Đang hoạt động</div>
            <p className="mt-1 text-2xl font-bold text-green-600">{stats.active}</p>
          </div>
          <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
            <div className="text-sm text-gray-600">Ngừng hoạt động</div>
            <p className="mt-1 text-2xl font-bold text-gray-600">{stats.inactive}</p>
          </div>
        </div>

        {/* Filters and Search */}
        <div className="mb-6 rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
          <div className="flex flex-wrap items-center gap-3">
            {/* Search by name */}
            <div className="relative flex-1 min-w-[200px]">
              <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-gray-400" />
              <input
                type="text"
                placeholder="Tìm theo tên..."
                value={nameFilter}
                onChange={(e) => {
                  setNameFilter(e.target.value)
                  handleFilterChange()
                }}
                className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-4 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
              />
            </div>

            {/* Search by email */}
            <div className="relative flex-1 min-w-[200px]">
              <input
                type="text"
                placeholder="Tìm theo email..."
                value={emailFilter}
                onChange={(e) => {
                  setEmailFilter(e.target.value)
                  handleFilterChange()
                }}
                className="w-full rounded-lg border border-gray-300 py-2 px-4 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
              />
            </div>

            {/* Role Filter */}
            <div className="flex items-center gap-2">
              <Filter className="h-4 w-4 text-gray-400 flex-shrink-0" />
              <select
                value={roleIdFilter}
                onChange={(e) => {
                  setRoleIdFilter(e.target.value)
                  handleFilterChange()
                }}
                className="rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
              >
                <option value="">Tất cả vai trò</option>
                {Array.from(roles.entries()).map(([roleId, role]) => (
                  <option key={roleId} value={roleId}>
                    {role.roleName}
                  </option>
                ))}
              </select>
            </div>

            {/* Campus Filter */}
            <select
              value={campusIdFilter}
              onChange={(e) => {
                setCampusIdFilter(e.target.value)
                handleFilterChange()
              }}
              className="rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
            >
              <option value="">Tất cả campus</option>
              {campuses.map((campus) => (
                <option key={campus.campusId} value={campus.campusId}>
                  {campus.name}
                </option>
              ))}
            </select>

            {/* Status Filter */}
            <select
              value={statusFilter}
              onChange={(e) => {
                setStatusFilter(e.target.value as 'Active' | 'Inactive' | '')
                handleFilterChange()
              }}
              className="rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
            >
              <option value="">Tất cả trạng thái</option>
              <option value="Active">Đang hoạt động</option>
              <option value="Inactive">Ngừng hoạt động</option>
            </select>

            {/* Clear Filters */}
            {(nameFilter || emailFilter || roleIdFilter || campusIdFilter || statusFilter) && (
              <button
                onClick={() => {
                  setNameFilter('')
                  setEmailFilter('')
                  setRoleIdFilter('')
                  setCampusIdFilter('')
                  setStatusFilter('')
                  handleFilterChange()
                }}
                className="flex items-center gap-1 rounded-lg border border-gray-300 px-3 py-2 text-sm text-gray-700 hover:bg-gray-50 flex-shrink-0"
              >
                <X className="h-4 w-4" />
                <span className="hidden sm:inline">Xóa bộ lọc</span>
              </button>
            )}
          </div>
        </div>

        {/* Loading State */}
        {loading && (
          <div className="flex flex-col items-center justify-center rounded-lg border border-gray-200 bg-white p-12">
            <Loader2 className="h-8 w-8 animate-spin text-orange-500" />
            <p className="mt-4 text-sm text-gray-600">Đang tải danh sách users...</p>
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
              onClick={fetchUsers}
              className="mt-4 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white hover:bg-red-700 transition-colors"
            >
              Thử lại
            </button>
          </div>
        )}

        {/* Users Table */}
        {!loading && !error && users.length > 0 && (
          <>
            <div className="overflow-hidden rounded-lg border border-gray-200 bg-white shadow-sm">
              <table className="w-full divide-y divide-gray-200">
                <thead className="bg-gray-50">
                  <tr>
                    <th className="px-3 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700 w-24">
                      Mã
                    </th>
                    <th className="px-3 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700 min-w-[180px]">
                      Thông tin người dùng
                    </th>
                    <th className="px-3 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700 min-w-[160px]">
                      Liên hệ
                    </th>
                    <th className="px-3 py-3 text-right text-xs font-semibold uppercase tracking-wider text-gray-700 w-32">
                      Vai trò
                    </th>
                    <th className="px-3 py-3 text-right text-xs font-semibold uppercase tracking-wider text-gray-700 w-32">
                      Trạng thái
                    </th>
                    <th className="px-3 py-3 text-right text-xs font-semibold uppercase tracking-wider text-gray-700 w-28 hidden xl:table-cell">
                      Ngày tạo
                    </th>
                    <th className="px-3 py-3 text-right text-xs font-semibold uppercase tracking-wider text-gray-700 w-20">
                      Thao tác
                    </th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-gray-200 bg-white">
                  {users.map((user) => (
                    <tr key={user.userId} className="hover:bg-gray-50">
                      <td className="px-3 py-3 text-xs font-mono text-gray-900">
                        {user.userId}
                      </td>
                      <td className="px-3 py-3">
                        <div className="flex items-center gap-2">
                          {user.avatarUrl ? (
                            <img
                              src={user.avatarUrl}
                              alt={user.fullName}
                              className="h-8 w-8 rounded-full flex-shrink-0"
                            />
                          ) : (
                            <div className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 text-xs font-semibold text-gray-600 flex-shrink-0">
                              {user.fullName.charAt(0).toUpperCase()}
                            </div>
                          )}
                          <div className="min-w-0">
                            <p className="text-sm font-semibold text-gray-900 truncate">{user.fullName}</p>
                            <p className="text-xs text-gray-500 truncate">{user.userName}</p>
                          </div>
                        </div>
                      </td>
                      <td className="px-3 py-3">
                        <p className="text-sm text-gray-900 truncate">{user.email}</p>
                        {user.phoneNumber && (
                          <p className="text-xs text-gray-500 truncate">{user.phoneNumber}</p>
                        )}
                      </td>
                      <td className="px-3 py-3 text-right">
                        <span
                          className={`inline-flex rounded-full px-2 py-1 text-xs font-semibold ${getRoleBadgeColor(
                            user.roleId
                          )}`}
                        >
                          {getRoleName(user.roleId)}
                        </span>
                      </td>
                      <td className="px-3 py-3 text-right">
                        <span
                          className={`inline-flex rounded-full px-2 py-1 text-xs font-semibold ${
                            user.status === 'Active'
                              ? 'bg-green-100 text-green-700'
                              : 'bg-gray-100 text-gray-700'
                          }`}
                        >
                          {user.status === 'Active' ? 'Hoạt động' : 'Ngừng'}
                        </span>
                      </td>
                      <td className="px-3 py-3 text-right text-xs text-gray-600 hidden xl:table-cell">
                        <span className="truncate block">{formatDate(user.createdAt)}</span>
                      </td>
                      <td className="px-3 py-3 text-right text-sm font-medium">
                        <button
                          onClick={() => setDeleteConfirm(user.userId)}
                          disabled={user.roleId === 'RL0003'}
                          className="inline-flex items-center justify-center rounded-lg p-1.5 text-red-600 hover:bg-red-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                          title={
                            user.roleId === 'RL0003'
                              ? 'Không thể xóa người dùng quản trị viên'
                              : 'Xóa người dùng'
                          }
                        >
                          <Trash2 className="h-4 w-4" />
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
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

        {/* Empty State */}
        {!loading && !error && users.length === 0 && (
          <div className="rounded-lg border border-gray-200 bg-white p-12 text-center shadow-sm">
            <Users className="mx-auto h-12 w-12 text-gray-400" />
            <h3 className="mt-4 text-lg font-semibold text-gray-900">Không tìm thấy user nào</h3>
            <p className="mt-2 text-sm text-gray-600">
              {nameFilter || emailFilter || roleIdFilter || campusIdFilter || statusFilter
                ? 'Thử điều chỉnh bộ lọc để tìm kiếm.'
                : 'Hiện tại không có user nào trong hệ thống.'}
            </p>
          </div>
        )}
      </div>

      {/* Delete Confirmation Modal */}
      {deleteConfirm && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
          <div className="w-full max-w-md rounded-lg border border-gray-200 bg-white p-6 shadow-xl">
            <div className="mb-4 flex items-center gap-3">
              <div className="flex h-10 w-10 items-center justify-center rounded-full bg-red-100">
                <Trash2 className="h-5 w-5 text-red-600" />
              </div>
              <div>
                <h3 className="text-lg font-semibold text-gray-900">Xác nhận xóa</h3>
                <p className="text-sm text-gray-600">Bạn có chắc chắn muốn xóa người dùng này?</p>
              </div>
            </div>
            {getUserById(deleteConfirm) && (
              <div className="mb-6 rounded-lg bg-gray-50 p-4">
                <p className="text-sm text-gray-700">
                  <strong>Tên:</strong> {getUserById(deleteConfirm)?.fullName}
                </p>
                <p className="mt-1 text-sm text-gray-700">
                  <strong>Email:</strong> {getUserById(deleteConfirm)?.email}
                </p>
                <p className="mt-1 text-sm text-gray-700">
                  <strong>User ID:</strong> {deleteConfirm}
                </p>
              </div>
            )}
            <p className="mb-6 text-sm text-red-600">
              <strong>Lưu ý:</strong> Hành động này không thể hoàn tác. Tài khoản sẽ bị xóa vĩnh viễn.
            </p>
            <div className="flex justify-end gap-3">
              <button
                onClick={() => setDeleteConfirm(null)}
                disabled={deleteLoading}
                className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
              >
                Hủy
              </button>
              <button
                onClick={() => handleDelete(deleteConfirm)}
                disabled={deleteLoading}
                className="flex items-center gap-2 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-red-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {deleteLoading && <Loader2 className="h-4 w-4 animate-spin" />}
                Xác nhận xóa
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  )
}

export default UserDashboard
