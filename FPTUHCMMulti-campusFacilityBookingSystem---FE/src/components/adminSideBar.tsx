import { NavLink, useNavigate } from 'react-router-dom'
import { LayoutDashboard, Building2, Users, LogOut, MapPin, BarChart3, Settings } from 'lucide-react'
import { logoutAPI } from '../layout/Login/api/loginAPI'
import { clearAuth } from '../utils/auth'

const AdminSideBar = () => {
  const navigate = useNavigate()

  const handleLogout = async () => {
    try {
      await logoutAPI()
    } catch (error) {
      console.error('Logout API error:', error)
    } finally {
      // Xóa auth data và dispatch event
      clearAuth()
      window.dispatchEvent(new Event('auth:logoutSuccess'))
      // Redirect về trang chủ
      navigate('/')
    }
  }
  return (
    <aside className="fixed left-0 top-0 h-screen w-64 border-r border-gray-200 bg-white shadow-sm">
      <div className="flex h-full flex-col">
        {/* Logo/Brand */}
        <div className="border-b border-gray-200 p-6">
          <div className="flex items-center gap-2">
            <span className="rounded bg-orange-500 px-2 py-1 text-sm font-semibold text-white">FPTU</span>
            <div className="flex flex-col leading-tight">
              <span className="text-sm font-semibold text-gray-800">Trang quản lý - Admin</span>
              <span className="text-xs text-gray-500">Đặt cơ sở vật chất</span>
            </div>
          </div>
        </div>

        {/* Navigation */}
        <nav className="flex-1 space-y-1 p-4">
          <NavLink
            to="/admin/dashboard"
            className={({ isActive }) =>
              `flex items-center gap-3 rounded-lg px-4 py-3 text-sm font-medium transition-colors ${
                isActive
                  ? 'bg-orange-50 text-orange-600'
                  : 'text-gray-700 hover:bg-gray-50 hover:text-orange-600'
              }`
            }
          >
            <LayoutDashboard className="h-5 w-5" />
            <span>Quản lý yêu cầu</span>
          </NavLink>

          <NavLink
            to="/admin/facilities"
            className={({ isActive }) =>
              `flex items-center gap-3 rounded-lg px-4 py-3 text-sm font-medium transition-colors ${
                isActive
                  ? 'bg-orange-50 text-orange-600'
                  : 'text-gray-700 hover:bg-gray-50 hover:text-orange-600'
              }`
            }
          >
            <Building2 className="h-5 w-5" />
            <span>Quản lý cơ sở vật chất</span>
          </NavLink>

          <NavLink
            to="/admin/campuses"
            className={({ isActive }) =>
              `flex items-center gap-3 rounded-lg px-4 py-3 text-sm font-medium transition-colors ${
                isActive
                  ? 'bg-orange-50 text-orange-600'
                  : 'text-gray-700 hover:bg-gray-50 hover:text-orange-600'
              }`
            }
          >
            <MapPin className="h-5 w-5" />
            <span>Quản lý campus</span>
          </NavLink>

          <NavLink
            to="/admin/users"
            className={({ isActive }) =>
              `flex items-center gap-3 rounded-lg px-4 py-3 text-sm font-medium transition-colors ${
                isActive
                  ? 'bg-orange-50 text-orange-600'
                  : 'text-gray-700 hover:bg-gray-50 hover:text-orange-600'
              }`
            }
          >
            <Users className="h-5 w-5" />
            <span>Quản lý user</span>
          </NavLink>

          <NavLink
            to="/admin/reports"
            className={({ isActive }) =>
              `flex items-center gap-3 rounded-lg px-4 py-3 text-sm font-medium transition-colors ${
                isActive
                  ? 'bg-orange-50 text-orange-600'
                  : 'text-gray-700 hover:bg-gray-50 hover:text-orange-600'
              }`
            }
          >
            <BarChart3 className="h-5 w-5" />
            <span>Báo cáo & thống kê</span>
          </NavLink>

          <NavLink
            to="/admin/settings"
            className={({ isActive }) =>
              `flex items-center gap-3 rounded-lg px-4 py-3 text-sm font-medium transition-colors ${
                isActive
                  ? 'bg-orange-50 text-orange-600'
                  : 'text-gray-700 hover:bg-gray-50 hover:text-orange-600'
              }`
            }
          >
            <Settings className="h-5 w-5" />
            <span>Cài đặt hệ thống</span>
          </NavLink>
        </nav>

        {/* Logout */}
        <div className="border-t border-gray-200 p-4">
          <button
            onClick={handleLogout}
            className="flex w-full items-center gap-3 rounded-lg px-4 py-3 text-sm font-medium text-gray-700 transition-colors hover:bg-gray-50 hover:text-red-600"
          >
            <LogOut className="h-5 w-5" />
            <span>Đăng xuất</span>
          </button>
        </div>
      </div>
    </aside>
  )
}

export default AdminSideBar

