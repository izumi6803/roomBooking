import { Navigate, useLocation } from 'react-router-dom';
import { useAuthState } from '../hooks/useAuthState';
import { Loader2 } from 'lucide-react';

interface ProtectedRouteProps {
  children: React.ReactNode;
  allowedRoles?: ('Student' | 'Lecturer' | 'Admin' | 'Facility_Admin' | 'Facility_Manager')[];
}

const ProtectedRoute = ({ children, allowedRoles }: ProtectedRouteProps) => {
  const { user, isAuthenticated, isLoading } = useAuthState();
  const location = useLocation();


  // Show loading while checking authentication
  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="text-center">
          <Loader2 className="w-10 h-10 text-orange-500 animate-spin mx-auto mb-3" />
          <p className="text-gray-600">Đang kiểm tra đăng nhập...</p>
        </div>
      </div>
    );
  }

  // Redirect to login if not authenticated
  if (!isAuthenticated) {
    // Save the attempted URL to redirect back after login
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // Check role-based access if allowedRoles is specified
  if (allowedRoles && user) {
    // Debug logging để kiểm tra role
    console.log('ProtectedRoute - User role:', user.role);
    console.log('ProtectedRoute - Allowed roles:', allowedRoles);
    console.log('ProtectedRoute - Has access:', allowedRoles.includes(user.role));
    
    if (!allowedRoles.includes(user.role)) {
      console.warn('ProtectedRoute - Access denied. User role:', user.role, 'not in allowed roles:', allowedRoles);
      
      // Nếu là Admin hoặc Facility_Manager cố truy cập user routes, redirect về admin dashboard
      if ((user.role === 'Admin' || user.role === 'Facility_Manager') && 
          !allowedRoles.includes('Admin') && !allowedRoles.includes('Facility_Manager')) {
        return <Navigate to="/admin/dashboard" replace />;
      }
      
      // Nếu là Student hoặc Lecturer cố truy cập admin routes, redirect về 403
      return <Navigate to="/403" replace />;
    }
  }

  return <>{children}</>;
};

export default ProtectedRoute;



