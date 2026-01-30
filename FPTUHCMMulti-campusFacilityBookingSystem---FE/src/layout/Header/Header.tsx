import { useState, useRef, useEffect } from 'react'
import { Link, NavLink, useNavigate } from 'react-router-dom'
import { useAuthState } from '../../hooks/useAuthState'
import { logoutAPI } from '../../layout/Login/api/loginAPI'
import { clearAuth } from '../../utils/auth'
import { LogOut, User, ChevronDown, Calendar, Bell } from 'lucide-react'
import NotificationDropdown from '../../components/NotificationDropdown'
import { getUnreadCount } from '../../page/User/Notification/api/notificationApi'

const Header = () => {
  const { user, isAuthenticated } = useAuthState()
  const navigate = useNavigate()
  const [showDropdown, setShowDropdown] = useState(false)
  const [showNotificationDropdown, setShowNotificationDropdown] = useState(false)
  const [unreadCount, setUnreadCount] = useState(0)
  const dropdownRef = useRef<HTMLDivElement>(null)
  const notificationRef = useRef<HTMLDivElement>(null)
  
  // Check if user is Student or Lecturer
  const isStudentOrLecturer = user && (user.role === 'Student' || user.role === 'Lecturer')

  // Load unread notification count
  const loadUnreadCount = async () => {
    try {
      const response = await getUnreadCount()
      if (response.success) {
        setUnreadCount(response.data || 0)
      }
    } catch (error) {
      console.error('Error loading unread count:', error)
    }
  }

  // Load unread count when authenticated and is Student/Lecturer
  useEffect(() => {
    if (isAuthenticated && isStudentOrLecturer) {
      loadUnreadCount()
      // Refresh unread count every 30 seconds
      const interval = setInterval(loadUnreadCount, 30000)
      return () => clearInterval(interval)
    }
  }, [isAuthenticated, isStudentOrLecturer])

  // Close dropdown when clicking outside
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setShowDropdown(false)
      }
      if (notificationRef.current && !notificationRef.current.contains(event.target as Node)) {
        setShowNotificationDropdown(false)
      }
    }

    if (showDropdown || showNotificationDropdown) {
      document.addEventListener('mousedown', handleClickOutside)
    }

    return () => {
      document.removeEventListener('mousedown', handleClickOutside)
    }
  }, [showDropdown, showNotificationDropdown])

  const handleLogout = async () => {
    try {
      await logoutAPI()
    } catch (error) {
      console.error('Logout API error:', error)
    } finally {
      // Xóa auth data và dispatch event
      clearAuth()
      window.dispatchEvent(new Event('auth:logoutSuccess'))
      setShowDropdown(false)
      navigate('/')
    }
  }

  const handleProfileClick = () => {
    setShowDropdown(false)
    navigate('/profile')
  }

  return (
    <header className="bg-white border-b shadow-sm">
      <div className="mx-auto flex max-w-6xl items-center justify-between px-4 py-3">
        <Link to="/" className="flex items-center gap-2">
          <span className="rounded bg-orange-500 px-2 py-1 text-sm font-semibold text-white">FPTU</span>
          <div className="flex flex-col leading-tight">
            <span className="text-sm font-semibold text-gray-800">Multi‑campus Facility Booking</span>
            <span className="text-xs text-gray-500">Campus Khu Công Nghệ Cao & NVH</span>
          </div>
        </Link>

        <nav className="flex items-center gap-4 text-sm font-medium text-gray-600">
          <NavLink
            to="/"
            className={({ isActive }) => `hover:text-orange-600 ${isActive ? 'text-orange-600' : ''}`}
          >
            Trang chủ
          </NavLink>
          <NavLink
            to="/facilities"
            className={({ isActive }) => `hover:text-orange-600 ${isActive ? 'text-orange-600' : ''}`}
          >
            Cơ sở vật chất
          </NavLink>
          {isAuthenticated && (
            <NavLink
              to="/my-bookings"
              className={({ isActive }) => `flex items-center gap-1 hover:text-orange-600 ${isActive ? 'text-orange-600' : ''}`}
            >
              <Calendar className="w-4 h-4" />
              <span>Lịch sử đặt</span>
            </NavLink>
          )}
        </nav>

        <div className="flex items-center gap-3">
          {/* Notification Bell - Only for Student/Lecturer */}
          {isAuthenticated && isStudentOrLecturer && (
            <div className="relative" ref={notificationRef}>
              <button
                onClick={() => {
                  setShowNotificationDropdown(!showNotificationDropdown)
                  setShowDropdown(false)
                }}
                className="relative p-2 rounded-lg hover:bg-gray-100 transition-colors"
                aria-label="Thông báo"
              >
                <Bell className="w-5 h-5 text-gray-600" />
                {unreadCount > 0 && (
                  <span className="absolute -top-1 -right-1 w-5 h-5 bg-orange-500 text-white text-xs font-semibold rounded-full flex items-center justify-center">
                    {unreadCount > 9 ? '9+' : unreadCount}
                  </span>
                )}
              </button>
              <NotificationDropdown
                isOpen={showNotificationDropdown}
                onClose={() => {
                  setShowNotificationDropdown(false)
                  loadUnreadCount() // Refresh count when closing
                }}
              />
            </div>
          )}

          {isAuthenticated && user ? (
            <div className="relative" ref={dropdownRef}>
              {/* User Icon/Avatar Button */}
              <button
                onClick={() => setShowDropdown(!showDropdown)}
                className="flex items-center gap-2 rounded-lg p-1.5 hover:bg-gray-100 transition-colors"
                aria-label="User menu"
              >
                {user.avatar_url ? (
                  <img 
                    src={user.avatar_url} 
                    alt={user.full_name}
                    className="w-8 h-8 rounded-full border-2 border-orange-200"
                  />
                ) : (
                  <div className="w-8 h-8 rounded-full bg-orange-100 flex items-center justify-center">
                    <User className="w-4 h-4 text-orange-600" />
                  </div>
                )}
                <ChevronDown className={`w-4 h-4 text-gray-600 transition-transform ${showDropdown ? 'rotate-180' : ''}`} />
              </button>

              {/* Dropdown Menu */}
              {showDropdown && (
                <div className="absolute right-0 mt-2 w-48 rounded-lg border border-gray-200 bg-white shadow-lg z-50">
                  <div className="py-1">
                    <button
                      onClick={handleProfileClick}
                      className="flex w-full items-center gap-2 px-4 py-2 text-sm text-gray-700 hover:bg-gray-50 transition-colors"
                    >
                      <User className="w-4 h-4" />
                      Hồ sơ
                    </button>
                    <button
                      onClick={handleLogout}
                      className="flex w-full items-center gap-2 px-4 py-2 text-sm text-red-600 hover:bg-red-50 transition-colors"
                    >
                      <LogOut className="w-4 h-4" />
                      Đăng xuất
                    </button>
                  </div>
                </div>
              )}
            </div>
          ) : (
            <Link
              to="/login"
              className="rounded-lg border border-orange-500 px-3 py-1.5 text-sm font-semibold text-orange-600 hover:bg-orange-50"
            >
              Đăng nhập
            </Link>
          )}
        </div>
      </div>
    </header>
  )
}

export default Header
