import { Navigate, Route, Routes } from 'react-router-dom'
import AdminDashboard from '../../page/Admin/Facility Dashboard'
import FacilityManagement from '../../page/Admin/FacilityManagement'
import UserDashboard from '../../page/Admin/User Dashboard'
import CampusManagement from '../../page/Admin/CampusManagement'
import Report from '../../page/Admin/Report'
import SystemSetting from '../../page/Admin/SystemSetting'
import AdminSideBar from '../../components/adminSideBar'

const AdminRoutes = () => {
  return (
    <div className="flex min-h-screen bg-gray-50">
      <AdminSideBar />
      <main className="ml-64 flex-1">
        <Routes>
          <Route path="/dashboard" element={<AdminDashboard />} />
          <Route path="/facilities" element={<FacilityManagement />} />
          <Route path="/campuses" element={<CampusManagement />} />
          <Route path="/users" element={<UserDashboard />} />
          <Route path="/reports" element={<Report />} />
          <Route path="/settings" element={<SystemSetting />} />
          <Route path="*" element={<Navigate to="/admin/dashboard" replace />} />
        </Routes>
      </main>
    </div>
  )
}

export default AdminRoutes

