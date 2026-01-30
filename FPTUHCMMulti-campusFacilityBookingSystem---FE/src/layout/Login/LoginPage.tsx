import { useState, useEffect, useRef } from 'react'
import { useNavigate, useLocation } from 'react-router-dom'
import { loginAPI } from './api/loginAPI'
import { loginWithGoogle } from './api/emailLoginApi'
import { useToast } from '../../components/toast'
import EmailVerificationModal from './components/EmailVerificationModal'
import ForgotPasswordModal from './components/ForgotPasswordModal'
import ResetPasswordModal from './components/ResetPasswordModal'
import { Loader2 } from 'lucide-react'

type LoginOption = 'account' | 'google'

interface LocationState {
  from?: {
    pathname: string;
  };
}

const LoginPage = () => {
  const { showSuccess, showError } = useToast()
  
  const [option, setOption] = useState<LoginOption>('account')
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [showPassword, setShowPassword] = useState(false)
  const [loading, setLoading] = useState(false)
  const [showVerificationModal, setShowVerificationModal] = useState(false)
  const [verificationEmail, setVerificationEmail] = useState('')
  const [pendingIdToken, setPendingIdToken] = useState<string | null>(null)
  const [showForgotPasswordModal, setShowForgotPasswordModal] = useState(false)
  const [showResetPasswordModal, setShowResetPasswordModal] = useState(false)
  const [resetPasswordEmail, setResetPasswordEmail] = useState('')

  const navigate = useNavigate()
  const location = useLocation()

  // Get the redirect path from location state, or default to /facilities
  // QUAN TRỌNG: Nếu from là admin route, chỉ dùng nếu user là Admin/Facility_Manager
  // Nếu không, dùng default route dựa trên role
  const getDefaultRoute = (userRole?: string): string => {
    if (userRole === 'Admin' || userRole === 'Facility_Manager') {
      return '/admin/dashboard';
    }
    return '/facilities';
  };

  const from = (location.state as LocationState)?.from?.pathname || '/facilities'

  // Check if user is already authenticated (has token in sessionStorage)
  // Sử dụng useEffect để tránh gọi navigate trong quá trình render
  useEffect(() => {
    const token = sessionStorage.getItem('auth_token')
    if (token) {
      navigate(from, { replace: true })
    }
  }, [navigate, from])

  const handleAccountLogin = async (e: React.FormEvent) => {
    e.preventDefault()

    if (!username.trim()) {
      showError('Vui lòng nhập email hoặc tên đăng nhập')
      return
    }

    if (!password) {
      showError('Vui lòng nhập mật khẩu')
      return
    }

    // QUAN TRỌNG: Đảm bảo clear tất cả token cũ trước khi login
    // Điều này ngăn chặn race condition với các request đang pending
    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('auth_user');
    sessionStorage.removeItem('is_google_login');

    setLoading(true)
    const result = await loginAPI(username.trim(), password)
    setLoading(false)

    if (result.success) {
      showSuccess(result.message)
      // Dispatch event to notify components to refresh user state
      window.dispatchEvent(new Event('auth:loginSuccess'))
      
      // Đợi một chút để đảm bảo sessionStorage và state đều được update
      setTimeout(() => {
        // Lấy user từ sessionStorage sau khi đã lưu
        const savedUser = sessionStorage.getItem('auth_user');
        let userRole = 'Student';
        
        if (savedUser) {
          try {
            const authUser = JSON.parse(savedUser);
            // Map roleId sang role
            const roleMap: Record<string, string> = {
              'RL0001': 'Student',
              'RL0002': 'Lecturer',
              'RL0003': 'Facility_Manager',
              // Có thể có roleId khác cho Admin, cần kiểm tra với backend
            };
            userRole = roleMap[authUser.roleId] || 'Student';
          } catch (e) {
            console.error('Error parsing user role:', e);
          }
        }
        
        // Xác định route redirect dựa trên role
        // Nếu from là admin route nhưng user không phải Admin/Facility_Manager, redirect về route mặc định
        let redirectPath = from;
        if (from.startsWith('/admin') && userRole !== 'Admin' && userRole !== 'Facility_Manager') {
          redirectPath = getDefaultRoute(userRole);
        } else if (!from.startsWith('/admin') && (userRole === 'Admin' || userRole === 'Facility_Manager')) {
          // Nếu user là Admin/Facility_Manager nhưng from không phải admin route, redirect về admin dashboard
          redirectPath = '/admin/dashboard';
        }
        
        navigate(redirectPath, { replace: true })
      }, 500)
    } else {
      showError(result.message)
    }
  }

  // Load Google Identity Services script
  const googleScriptLoaded = useRef(false);
  const googleButtonRef = useRef<HTMLDivElement>(null);

  /**
   * Initialize Google Sign In
   * 
   * Giải thích: Google OAuth được tích hợp như thế nào? Token được xử lý ra sao?
   * 
   * GOOGLE OAUTH INTEGRATION FLOW:
   * 
   * 1. FRONTEND SETUP (Chỉ làm UI và gọi Google API):
   *    - Load Google Identity Services script: https://accounts.google.com/gsi/client
   *    - Initialize với ClientID (PHẢI KHỚP với backend)
   *    - Render Google Sign-In button
   *    - Setup callback để nhận idToken từ Google
   * 
   * 2. USER INTERACTION:
   *    - User nhấp vào Google button
   *    - Google popup hiển thị, user chọn account và authorize
   *    - Google trả về idToken (JWT token từ Google) trong callback
   * 
   * 3. FRONTEND CHỈ GỌI API:
   *    - Frontend nhận idToken từ Google callback
   *    - Frontend KHÔNG verify token (không có secret key)
   *    - Frontend chỉ gửi idToken lên backend: POST /api/auth/login/google
   *    - Frontend không có business logic, chỉ pass-through
   * 
   * 4. BACKEND XỬ LÝ TẤT CẢ:
   *    - Backend nhận idToken từ frontend
   *    - Backend verify idToken với Google (có ClientID và secret)
   *    - Backend extract thông tin user từ idToken (email, name, etc.)
   *    - Backend check xem user đã tồn tại chưa:
   *      * Nếu chưa: Tạo user mới, gửi mã verify email
   *      * Nếu rồi: Lấy thông tin user từ database
   *    - Backend phân role dựa trên email domain (@fpt.edu.vn → Student, @fe.edu.vn → Lecturer)
   *    - Backend tạo JWT token riêng của hệ thống (KHÔNG phải Google token)
   *    - Backend trả về: { success, data: { token, userId, email, ... } }
   * 
   * 5. FRONTEND LƯU TOKEN:
   *    - Frontend nhận JWT token từ backend (token của hệ thống, không phải Google token)
   *    - Frontend lưu vào sessionStorage: 'auth_token' = JWT token từ backend
   *    - Token này được dùng cho tất cả API calls sau (qua apiClient interceptor)
   * 
   * TẠI SAO KHÔNG DÙNG GOOGLE TOKEN TRỰC TIẾP?
   * - Google token chỉ dùng để verify identity với Google
   * - Hệ thống cần token riêng để:
   *   * Kiểm soát expiration time
   *   * Thêm custom claims (role, permissions)
   *   * Revoke token khi cần
   *   * Không phụ thuộc vào Google
   * 
   * QUAN TRỌNG: ClientID ở frontend PHẢI KHỚP với ClientID trong appsettings.json của backend
   * Nếu không khớp sẽ gây lỗi "JWT contains untrusted 'aud' claim"
   * Backend đã config ClientID, frontend chỉ cần lấy idToken và gửi lên backend để verify
   */
  const initializeGoogleSignIn = () => {
    if (typeof window.google === 'undefined' || !window.google.accounts || !googleButtonRef.current) {
      return;
    }

    // Lấy ClientID từ env - PHẢI KHỚP với backend
    // ClientID này được Google cấp và phải được config ở cả frontend và backend
    const clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID || '';
    
    if (!clientId) {
      console.warn('Google Client ID chưa được cấu hình trong .env');
      showError('Google Client ID chưa được cấu hình. Vui lòng liên hệ quản trị viên.');
      return;
    }

    try {
      // Clear button trước khi render lại (tránh duplicate buttons)
      googleButtonRef.current.innerHTML = '';

      // FRONTEND: Initialize Google Identity Services
      // Chỉ setup UI và callback, KHÔNG verify token ở đây
      // Không verify JWT ở frontend, chỉ lấy idToken và gửi lên backend
      window.google.accounts.id.initialize({
        client_id: clientId, // ClientID phải khớp với backend
        callback: handleGoogleLoginSuccess, // Callback khi user chọn account
        auto_select: false, // Không tự động chọn account
        cancel_on_tap_outside: true, // Đóng popup khi click outside
      });

      // FRONTEND: Render Google Sign-In button vào div
      // Google sẽ tự động render button với styling của Google
      window.google.accounts.id.renderButton(
        googleButtonRef.current,
        {
          type: 'standard',
          theme: 'outline',
          size: 'large',
          text: 'signin_with',
          shape: 'rectangular',
          logo_alignment: 'left',
        }
      );
    } catch (error) {
      console.error('Google initialization error:', error);
    }
  };

  useEffect(() => {
    if (option === 'google' && !googleScriptLoaded.current) {
      const script = document.createElement('script');
      script.src = 'https://accounts.google.com/gsi/client';
      script.async = true;
      script.defer = true;
      script.onload = () => {
        googleScriptLoaded.current = true;
        // Đợi một chút để đảm bảo Google API đã sẵn sàng
        setTimeout(() => {
          initializeGoogleSignIn();
        }, 100);
      };
      document.body.appendChild(script);

      return () => {
        // Cleanup script if component unmounts
        const existingScript = document.querySelector('script[src="https://accounts.google.com/gsi/client"]');
        if (existingScript && existingScript.parentNode) {
          existingScript.parentNode.removeChild(existingScript);
        }
      };
    } else if (option === 'google' && googleScriptLoaded.current && googleButtonRef.current) {
      // Nếu script đã load, initialize ngay
      // Đảm bảo disable auto-select trước khi initialize lại để tránh sử dụng session cũ
      if (typeof window.google !== 'undefined' && window.google.accounts) {
        try {
          window.google.accounts.id.disableAutoSelect();
        } catch (error) {
          console.warn('Error disabling Google auto-select:', error);
        }
      }
      initializeGoogleSignIn();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [option]);

  /**
   * Xử lý khi Google OAuth login thành công
   * 
   * Giải thích: Frontend chỉ gọi API, backend xử lý logic
   * 
   * FLOW KHI GOOGLE CALLBACK:
   * 
   * 1. FRONTEND: Nhận idToken từ Google
   *    - Google callback trả về response.credential (idToken - JWT từ Google)
   *    - Frontend KHÔNG verify token này (không có secret key)
   *    - Frontend chỉ lưu tạm để gửi lên backend
   * 
   * 2. FRONTEND: Gọi API loginWithGoogle(idToken)
   *    - Chỉ làm nhiệm vụ gửi HTTP POST request
   *    - POST /api/auth/login/google với body: { idToken }
   *    - Không có business logic ở đây
   * 
   * 3. BACKEND: Xử lý tất cả logic
   *    - Nhận idToken từ request body
   *    - Verify idToken với Google (có ClientID và secret)
   *    - Extract user info từ token (email, name, picture)
   *    - Check database: User đã tồn tại chưa?
   *    - Nếu chưa: Tạo user mới, gửi mã verify email
   *    - Nếu rồi: Lấy user từ database
   *    - Phân role: @fpt.edu.vn → Student, @fe.edu.vn → Lecturer
   *    - Tạo JWT token riêng của hệ thống
   *    - Trả về response: { success, data: { token, userId, ... } }
   * 
   * 4. FRONTEND: Nhận response và lưu token
   *    - Nhận JWT token từ backend (token của hệ thống)
   *    - Lưu vào sessionStorage
   *    - Token này được dùng cho tất cả API calls sau
   * 
   * Nhận idToken từ Google và gửi lên backend (backend sẽ verify)
   */
  const handleGoogleLoginSuccess = async (response: { credential: string }) => {
    if (!response.credential) {
      showError('Không thể lấy thông tin từ Google. Vui lòng thử lại.');
      setLoading(false);
      return;
    }

    // QUAN TRỌNG: Đảm bảo clear tất cả token cũ trước khi login
    // Điều này ngăn chặn race condition với các request đang pending
    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('auth_user');
    sessionStorage.removeItem('is_google_login');

    // FRONTEND: idToken đã được Google trả về
    // Frontend KHÔNG verify token này, chỉ gửi trực tiếp lên backend
    // Backend sẽ verify JWT với ClientID và secret của nó
    try {
      setLoading(true);
      
      // FRONTEND CHỈ GỌI API: Gửi idToken lên backend
      // Backend sẽ xử lý tất cả: verify token, tạo/lấy user, tạo JWT token
      const result = await loginWithGoogle(response.credential);
      setLoading(false);

      if (result.success) {
        // Nếu cần xác thực email
        if (result.needsVerification && result.email) {
          setVerificationEmail(result.email);
          setPendingIdToken(response.credential); // Lưu idToken để dùng lại sau khi verify
          setShowVerificationModal(true);
          showSuccess(result.message);
        } 
        // Nếu đã verify, đăng nhập thành công
        else if (result.data) {
          showSuccess(result.message);
          // Dispatch event để update tất cả components
          window.dispatchEvent(new Event('auth:loginSuccess'));
          
          // Đợi một chút để đảm bảo sessionStorage và state đều được update
          setTimeout(() => {
            // Lấy user từ sessionStorage sau khi đã lưu
            const savedUser = sessionStorage.getItem('auth_user');
            let userRole = 'Student';
            
            if (savedUser) {
              try {
                const authUser = JSON.parse(savedUser);
                // Map roleId sang role
                const roleMap: Record<string, string> = {
                  'RL0001': 'Student',
                  'RL0002': 'Lecturer',
                  'RL0003': 'Facility_Manager',
                  // Có thể có roleId khác cho Admin, cần kiểm tra với backend
                };
                userRole = roleMap[authUser.roleId] || 'Student';
              } catch (e) {
                console.error('Error parsing user role:', e);
              }
            }
            
            // Xác định route redirect dựa trên role
            // Nếu from là admin route nhưng user không phải Admin/Facility_Manager, redirect về route mặc định
            let redirectPath = from;
            if (from.startsWith('/admin') && userRole !== 'Admin' && userRole !== 'Facility_Manager') {
              redirectPath = getDefaultRoute(userRole);
            } else if (!from.startsWith('/admin') && (userRole === 'Admin' || userRole === 'Facility_Manager')) {
              // Nếu user là Admin/Facility_Manager nhưng from không phải admin route, redirect về admin dashboard
              redirectPath = '/admin/dashboard';
            }
            
            navigate(redirectPath, { replace: true });
          }, 500);
        }
      } else {
        showError(result.message);
      }
    } catch (error) {
      console.error('Google login API error:', error);
      const errorMsg = 'Đã xảy ra lỗi khi đăng nhập. Vui lòng thử lại.';
      showError(errorMsg);
      setLoading(false);
    }
  };

  /**
   * Xử lý sau khi verify email thành công
   * Gọi lại loginWithGoogle để lấy token sau khi verify
   */
  const handleEmailVerified = async () => {
    if (!pendingIdToken) {
      showError('Không tìm thấy thông tin đăng nhập. Vui lòng thử lại.');
      setShowVerificationModal(false);
      return;
    }

    // QUAN TRỌNG: Đảm bảo clear tất cả token cũ trước khi login
    sessionStorage.removeItem('auth_token');
    sessionStorage.removeItem('auth_user');
    sessionStorage.removeItem('is_google_login');

    setShowVerificationModal(false);
    setLoading(true);
    showSuccess('Email đã được xác thực! Đang đăng nhập...');
    
    // Gọi lại loginWithGoogle với idToken đã lưu
    const result = await loginWithGoogle(pendingIdToken);
    setLoading(false);
    setPendingIdToken(null);

    if (result.success && result.data) {
      showSuccess(result.message);
      window.dispatchEvent(new Event('auth:loginSuccess'));
      
      // Đợi một chút để đảm bảo sessionStorage và state đều được update
      setTimeout(() => {
        // Lấy user từ sessionStorage sau khi đã lưu
        const savedUser = sessionStorage.getItem('auth_user');
        let userRole = 'Student';
        
        if (savedUser) {
          try {
            const authUser = JSON.parse(savedUser);
            // Map roleId sang role
            const roleMap: Record<string, string> = {
              'RL0001': 'Student',
              'RL0002': 'Lecturer',
              'RL0003': 'Facility_Manager',
              // Có thể có roleId khác cho Admin, cần kiểm tra với backend
            };
            userRole = roleMap[authUser.roleId] || 'Student';
          } catch (e) {
            console.error('Error parsing user role:', e);
          }
        }
        
        // Xác định route redirect dựa trên role
        // Nếu from là admin route nhưng user không phải Admin/Facility_Manager, redirect về route mặc định
        let redirectPath = from;
        if (from.startsWith('/admin') && userRole !== 'Admin' && userRole !== 'Facility_Manager') {
          redirectPath = getDefaultRoute(userRole);
        } else if (!from.startsWith('/admin') && (userRole === 'Admin' || userRole === 'Facility_Manager')) {
          // Nếu user là Admin/Facility_Manager nhưng from không phải admin route, redirect về admin dashboard
          redirectPath = '/admin/dashboard';
        }
        
        navigate(redirectPath, { replace: true });
      }, 500);
    } else {
      showError(result.message);
    }
  };

  return (
    <main className="relative flex min-h-screen items-center justify-center overflow-hidden  px-4 py-10">
      {/* Decorative blur effects */}
      <div className="absolute -left-20 top-10 h-64 w-64 rounded-full bg-white/10 blur-3xl" />
      <div className="absolute -right-10 bottom-0 h-72 w-72 rounded-full bg-purple-900/30 blur-3xl" />
      
      <div className="relative w-full max-w-md rounded-lg bg-white p-8 shadow-xl">
        <h1 className="mb-6 text-2xl font-bold text-gray-900">Đăng nhập</h1>

        {/* Login Options Toggle */}
        <div className="mb-6 flex rounded-lg border border-gray-200 bg-gray-50 p-1">
          <button
            type="button"
            onClick={() => {
              setOption('account')
            }}
            className={`flex-1 rounded-md px-4 py-2 text-sm font-medium transition ${
              option === 'account' 
                ? 'bg-white text-orange-600 shadow-sm' 
                : 'text-gray-600 hover:text-gray-900'
            }`}
          >
            Tài khoản được cấp
          </button>
          <button
            type="button"
            onClick={() => {
              setOption('google')
            }}
            className={`flex-1 rounded-md px-4 py-2 text-sm font-medium transition ${
              option === 'google' 
                ? 'bg-white text-orange-600 shadow-sm' 
                : 'text-gray-600 hover:text-gray-900'
            }`}
          >
            Email FPT
          </button>
        </div>

        {option === 'account' ? (
          <form className="space-y-4" onSubmit={handleAccountLogin}>
            <div>
              <label className="mb-1 block text-sm font-medium text-gray-700" htmlFor="username">
                Tên đăng nhập (cho sinh viên k19 hoặc quản trị viên)
              </label>
              <input
                id="username"
                type="text"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="Enter email"
                disabled={loading}
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:border-orange-500 focus:ring-1 focus:ring-orange-500 disabled:bg-gray-100"
              />
            </div>
            <div>
              <label className="mb-1 block text-sm font-medium text-gray-700" htmlFor="password">
                Password
              </label>
              <input
                id="password"
                type={showPassword ? 'text' : 'password'}
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Enter password"
                disabled={loading}
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:border-orange-500 focus:ring-1 focus:ring-orange-500 disabled:bg-gray-100"
              />
            </div>
            <div className="flex items-center">
              <input
                id="show-password"
                type="checkbox"
                checked={showPassword}
                onChange={(e) => setShowPassword(e.target.checked)}
                className="h-4 w-4 rounded border-gray-300 text-orange-600 focus:ring-orange-500"
              />
              <label htmlFor="show-password" className="ml-2 text-sm text-gray-700">
                Show Password
              </label>
            </div>
            <button
              type="submit"
              disabled={loading}
              className="w-full rounded-lg bg-orange-500 px-4 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 disabled:opacity-70 disabled:cursor-not-allowed flex items-center justify-center gap-2"
            >
              {loading ? (
                <>
                  <Loader2 className="h-4 w-4 animate-spin" />
                  <span>Đang đăng nhập...</span>
                </>
              ) : (
                'Đăng nhập'
              )}
            </button>
            <div className="space-y-2 text-center text-sm">
              <button 
                type="button" 
                onClick={() => setShowForgotPasswordModal(true)}
                className="text-orange-600 hover:text-orange-700 hover:underline"
              >
                Quên mật khẩu?
              </button>
            </div>
          </form>
        ) : (
          <div className="space-y-4">
            <p className="text-sm text-gray-600">
              Sử dụng tài khoản email FPT (<strong>@fpt.edu.vn hoặc @fe.edu.vn</strong>) để tiếp tục.
            </p>
            {loading ? (
              <div className="flex w-full items-center justify-center gap-2 rounded-lg border border-gray-300 bg-white px-4 py-3 text-sm font-semibold text-gray-800">
                <Loader2 className="h-5 w-5 animate-spin" />
                <span>Đang đăng nhập...</span>
              </div>
            ) : (
              <div className="flex justify-center">
                {/* Google sẽ render button vào đây */}
                <div ref={googleButtonRef} id="google-signin-button"></div>
              </div>
            )}
          </div>
        )}
      </div>

      {/* Email Verification Modal */}
      {showVerificationModal && verificationEmail && (
        <EmailVerificationModal
          email={verificationEmail}
          onVerified={handleEmailVerified}
          onClose={() => {
            setShowVerificationModal(false);
            setVerificationEmail('');
          }}
        />
      )}

      {/* Forgot Password Modal */}
      {showForgotPasswordModal && (
        <ForgotPasswordModal
          onClose={() => {
            setShowForgotPasswordModal(false);
            setResetPasswordEmail('');
          }}
          onCodeSent={(email) => {
            setResetPasswordEmail(email);
            setShowForgotPasswordModal(false);
            setShowResetPasswordModal(true);
          }}
        />
      )}

      {/* Reset Password Modal */}
      {showResetPasswordModal && resetPasswordEmail && (
        <ResetPasswordModal
          email={resetPasswordEmail}
          onClose={() => {
            setShowResetPasswordModal(false);
            setResetPasswordEmail('');
          }}
          onSuccess={() => {
            setShowResetPasswordModal(false);
            setResetPasswordEmail('');
            showSuccess('Đặt lại mật khẩu thành công! Bạn có thể đăng nhập với mật khẩu mới.');
          }}
        />
      )}
    </main>
  )
}

export default LoginPage
