import { useState, useEffect } from 'react';
import { getCurrentUser, isAuthenticated, type User } from '../utils/auth';

/**
 * Custom hook để quản lý authentication state mà không cần AuthContext
 * Hook này đọc trực tiếp từ sessionStorage và tự động update khi có thay đổi
 * LƯU Ý: sessionStorage riêng biệt cho mỗi tab, nên mỗi tab có session riêng
 */
export const useAuthState = () => {
  const [user, setUser] = useState<User | null>(getCurrentUser);
  const [authenticated, setAuthenticated] = useState(isAuthenticated);

  // Listen for custom events (khi login/logout ở cùng tab)
  // LƯU Ý: sessionStorage không chia sẻ giữa các tab, nên không cần listen storage event
  // Mỗi tab có session riêng biệt
  useEffect(() => {
    const handleLoginSuccess = () => {
      const newUser = getCurrentUser();
      setUser(newUser);
      setAuthenticated(isAuthenticated());
    };

    const handleLogoutSuccess = () => {
      setUser(null);
      setAuthenticated(false);
    };
    
    // Listen for custom events (khi login/logout ở cùng tab)
    window.addEventListener('auth:loginSuccess', handleLoginSuccess);
    window.addEventListener('auth:logoutSuccess', handleLogoutSuccess);
    
    return () => {
      window.removeEventListener('auth:loginSuccess', handleLoginSuccess);
      window.removeEventListener('auth:logoutSuccess', handleLogoutSuccess);
    };
  }, []);

  return {
    user,
    isAuthenticated: authenticated,
    isLoading: false, // Không cần loading vì đọc từ sessionStorage là sync
  };
};

