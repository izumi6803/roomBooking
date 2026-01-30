import { Navigate, Route, Routes } from 'react-router-dom'
import HomePage from '../../page/User/Home'
import FacilityPage from '../../page/User/Facility'
import BookingPage from '../../page/User/Booking'
import ProfilePage from '../../page/User/Profile'
import MyBookingsPage from '../../page/User/MyBookings'
import ProtectedRoute from '../../components/ProtectedRoute'

const UserRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/home" element={<HomePage />} />
      <Route 
        path="/facilities" 
        element={
          <ProtectedRoute allowedRoles={['Student', 'Lecturer']}>
            <FacilityPage />
          </ProtectedRoute>
        } 
      />
      <Route 
        path="/booking/:facilityId" 
        element={
          <ProtectedRoute allowedRoles={['Student', 'Lecturer']}>
            <BookingPage />
          </ProtectedRoute>
        } 
      />
      <Route 
        path="/profile" 
        element={
          <ProtectedRoute allowedRoles={['Student', 'Lecturer']}>
            <ProfilePage />
          </ProtectedRoute>
        } 
      />
      <Route 
        path="/my-bookings" 
        element={
          <ProtectedRoute allowedRoles={['Student', 'Lecturer']}>
            <MyBookingsPage />
          </ProtectedRoute>
        } 
      />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  )
}

export default UserRoutes


