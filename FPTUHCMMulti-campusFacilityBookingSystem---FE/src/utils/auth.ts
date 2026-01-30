import { type AuthUser as LoginAuthUser } from '../layout/Login/api/loginAPI';
import { type AuthUser } from '../types';

// Map roleId từ API sang role name
const mapRoleIdToRole = (roleId: string): AuthUser['role'] => {
  const roleMap: Record<string, AuthUser['role']> = {
    'RL0001': 'Student',
    'RL0002': 'Lecturer',
    'RL0003': 'Facility_Manager',
  };
  return roleMap[roleId] || 'Student';
};

/**
 * Lấy thông tin user từ sessionStorage (riêng biệt cho mỗi tab)
 * @returns AuthUser object hoặc null nếu chưa đăng nhập
 */
export const getCurrentUser = (): AuthUser | null => {
  try {
    const token = sessionStorage.getItem('auth_token');
    const savedUser = sessionStorage.getItem('auth_user');
    
    if (!token || !savedUser) {
      console.log('getCurrentUser: No token or savedUser found');
      return null;
    }

    const loginAuthUser: LoginAuthUser = JSON.parse(savedUser);
    console.log('getCurrentUser: AuthUser from sessionStorage:', loginAuthUser);
    
    // Convert LoginAuthUser từ API sang AuthUser interface
    // Note: avatarUrl và phoneNumber có thể được thêm vào authUser sau khi user cập nhật profile
    const user: AuthUser = {
      user_id: loginAuthUser.userId,
      email: loginAuthUser.email,
      full_name: loginAuthUser.fullName,
      user_name: loginAuthUser.email.split('@')[0],
      role: mapRoleIdToRole(loginAuthUser.roleId),
      campus_id: 1,
      status: 'Active',
      avatar_url: (loginAuthUser as { avatarUrl?: string }).avatarUrl || undefined, // Read from sessionStorage if available
      created_at: new Date().toISOString(),
    };

    console.log('getCurrentUser: Mapped user role:', user.role, 'from roleId:', loginAuthUser.roleId);
    return user;
  } catch (error) {
    console.error('Error parsing auth user:', error);
    // Clear invalid data
    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('auth_user');
    return null;
  }
};

/**
 * Kiểm tra user đã đăng nhập chưa
 * @returns true nếu đã đăng nhập, false nếu chưa
 */
export const isAuthenticated = (): boolean => {
  const token = sessionStorage.getItem('auth_token');
  const authUser = sessionStorage.getItem('auth_user');
  return !!(token && authUser);
};

/**
 * Lấy token từ sessionStorage
 * @returns JWT token hoặc null
 */
export const getToken = (): string | null => {
  return sessionStorage.getItem('auth_token');
};

/**
 * Lấy LoginAuthUser từ sessionStorage
 * @returns LoginAuthUser object hoặc null
 */
export const getAuthUser = (): LoginAuthUser | null => {
  try {
    const savedUser = sessionStorage.getItem('auth_user');
    if (!savedUser) return null;
    return JSON.parse(savedUser) as LoginAuthUser;
  } catch {
    return null;
  }
};

/**
 * Kiểm tra user có role được phép không
 * @param allowedRoles - Danh sách roles được phép
 * @returns true nếu user có role trong allowedRoles
 */
export const hasRole = (allowedRoles: AuthUser['role'][]): boolean => {
  const user = getCurrentUser();
  if (!user) return false;
  return allowedRoles.includes(user.role);
};

/**
 * Revoke Google session nếu có
 */
export const revokeGoogleSession = (): void => {
  try {
    // Kiểm tra xem Google Identity Services có sẵn không
    if (typeof window.google !== 'undefined' && window.google.accounts) {
      // Disable auto-select để đảm bảo user phải chọn lại account
      window.google.accounts.id.disableAutoSelect();
      // Có thể cần revoke token nếu có
      // Google Identity Services không có method revoke trực tiếp,
      // nhưng disableAutoSelect sẽ đảm bảo user phải chọn lại account
    }
  } catch (error) {
    console.warn('Error revoking Google session:', error);
  }
};

/**
 * Xóa thông tin đăng nhập khỏi sessionStorage (chỉ ảnh hưởng tab hiện tại)
 */
export const clearAuth = (): void => {
  // Revoke Google session nếu user đăng nhập bằng Google
  const wasGoogleLogin = sessionStorage.getItem('is_google_login') === 'true';
  if (wasGoogleLogin) {
    revokeGoogleSession();
  }
  
  sessionStorage.removeItem('auth_token');
  sessionStorage.removeItem('auth_user');
  sessionStorage.removeItem('is_google_login');
};

/**
 * Kiểm tra user có đăng nhập bằng Google không
 * @returns true nếu user đăng nhập bằng Google, false nếu đăng nhập bằng email/password
 */
export const isGoogleLogin = (): boolean => {
  return sessionStorage.getItem('is_google_login') === 'true';
};

