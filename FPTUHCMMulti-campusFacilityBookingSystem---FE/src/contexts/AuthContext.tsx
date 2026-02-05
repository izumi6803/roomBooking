import React, { useState, useEffect, type ReactNode } from 'react';
import { type AuthUser as LoginAuthUser, logoutAPI } from '../layout/Login/api/loginAPI';
import { type AuthUser } from '../types';
import { clearAuth } from '../utils/auth';
import { AuthContext, type AuthContextType } from './authContext';

interface AuthProviderProps {
  children: ReactNode;
}

// Map roleId từ API sang role name
const mapRoleIdToRole = (roleId: string): AuthUser['role'] => {
  // Mapping từ roleId trong database sang role trong code
  // RL0001: Student, RL0002: Lecturer, RL0003: Facility_Admin -> Facility_Manager
  // Có thể có thêm roleId khác cho Admin (ví dụ: RL0004) nếu cần
  const roleMap: Record<string, AuthUser['role']> = {
    'RL0001': 'Student',
    'RL0002': 'Lecturer',
    'RL0003': 'Facility_Manager', // Facility_Admin trong DB được map sang Facility_Manager
    // Có thể thêm roleId khác cho Admin nếu có trong database
    // 'RL0004': 'Admin', // Ví dụ: nếu có roleId riêng cho Admin
  };
  return roleMap[roleId] || 'Student';
};

// Helper function to get initial user from sessionStorage (riêng biệt cho mỗi tab)
const getInitialUser = (): AuthUser | null => {
  try {
    const token = sessionStorage.getItem('auth_token');
    const savedUser = sessionStorage.getItem('auth_user');
    
    if (!token || !savedUser) {
      return null;
    }

    const loginAuthUser: LoginAuthUser = JSON.parse(savedUser);
    
    // Convert LoginAuthUser từ API sang AuthUser interface
    const user: AuthUser = {
      user_id: loginAuthUser.userId,
      email: loginAuthUser.email,
      full_name: loginAuthUser.fullName,
      user_name: loginAuthUser.email.split('@')[0], // Extract username from email
      role: mapRoleIdToRole(loginAuthUser.roleId),
      campus_id: 1, // Default, có thể cần lấy từ API sau
      status: 'Active',
      avatar_url: undefined,
      created_at: new Date().toISOString(),
    };

    return user;
  } catch (error) {
    console.error('Error parsing auth user:', error);
    // Clear invalid data
    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('auth_user');
    return null;
  }
};

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [user, setUser] = useState<AuthUser | null>(getInitialUser);
  const [isLoading] = useState(false);

  // Listen for custom events (when login/logout happens in same tab)
  // LƯU Ý: sessionStorage không chia sẻ giữa các tab, nên không cần listen storage event
  // Mỗi tab có session riêng biệt
  useEffect(() => {
    const handleLoginSuccess = () => {
      const newUser = getInitialUser();
      setUser(newUser);
    };

    window.addEventListener('auth:loginSuccess', handleLoginSuccess);
    
    return () => {
      window.removeEventListener('auth:loginSuccess', handleLoginSuccess);
    };
  }, []);

  // Login functions are deprecated - use loginAPI directly in LoginPage
  // Keeping for backward compatibility but they won't work
  const login = async (): Promise<{ success: boolean; message: string }> => {
    console.warn('login() is deprecated. Use loginAPI from LoginPage instead.');
    return { success: false, message: 'Vui lòng sử dụng API login trực tiếp' };
  };

  const loginWithGoogle = async (): Promise<{ success: boolean; message: string }> => {
    console.warn('loginWithGoogle() is deprecated. Use loginAPI from LoginPage instead.');
    return { success: false, message: 'Vui lòng sử dụng API login trực tiếp' };
  };

  // Logout - gọi API logout và xóa token/user data
  const logout = async () => {
    try {
      // Gọi API logout trước
      await logoutAPI();
    } catch (error) {
      // Nếu API call thất bại, vẫn xóa token ở client side
      console.error('Logout API error:', error);
    } finally {
      // Luôn xóa user state và localStorage để đảm bảo logout hoàn tất
      // Sử dụng clearAuth để revoke Google session nếu cần
      setUser(null);
      clearAuth();
      
      // Dispatch event để các component khác biết đã logout
      window.dispatchEvent(new Event('auth:logoutSuccess'));
    }
  };

  const value: AuthContextType = {
    user,
    isAuthenticated: !!user,
    isLoading,
    login,
    loginWithGoogle,
    logout,
  };

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
};
