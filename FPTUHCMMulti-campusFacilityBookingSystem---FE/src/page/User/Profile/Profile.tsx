/**
 * Profile Component - Trang quản lý hồ sơ người dùng
 * 
 * Component này cho phép user xem và chỉnh sửa thông tin cá nhân, đổi mật khẩu,
 * và xem lịch sử feedbacks của mình.
 * 
 * Tính năng chính:
 * - Tab "Thông tin": Chỉnh sửa thông tin cá nhân (tên, số điện thoại, avatar, MSSV)
 * - Tab "Đổi mật khẩu": Đổi mật khẩu (chỉ hiển thị nếu không phải Google login)
 * - Tab "Feedbacks": Xem lịch sử feedbacks đã gửi
 * 
 * Logic đặc biệt:
 * - Chỉ cho phép Student và Lecturer truy cập (Admin/Facility_Manager bị redirect)
 * - Ẩn tab "Đổi mật khẩu" nếu user đăng nhập bằng Google
 * - Tự động cập nhật sessionStorage khi profile được cập nhật
 */

// Import React hooks: useState để quản lý state, useEffect để xử lý side effects
import { useState, useEffect } from 'react';
// Import useNavigate hook từ react-router-dom để điều hướng
import { useNavigate } from 'react-router-dom';
// Import custom hook để lấy trạng thái authentication
import { useAuthState } from '../../../hooks/useAuthState';
// Import utility function để kiểm tra xem user có đăng nhập bằng Google không
import { isGoogleLogin } from '../../../utils/auth';
// Import API function và type từ profileApi
import { getProfile, type UserProfile } from './api/profileApi';
// Import các component con
import ProfileForm from './components/ProfileForm';
import ChangePasswordForm from './components/ChangePasswordForm';
import MyFeedbacksTab from './components/MyFeedbacksTab';
// Import icons từ lucide-react
import { Loader2, User, Lock, MessageSquare } from 'lucide-react';

/**
 * Type definition cho các tab types
 * 
 * Union type: chỉ có thể là một trong 3 giá trị
 * - 'profile': Tab thông tin cá nhân
 * - 'password': Tab đổi mật khẩu
 * - 'feedbacks': Tab lịch sử feedbacks
 */
type TabType = 'profile' | 'password' | 'feedbacks';

/**
 * Profile Component Function
 * 
 * Main component function quản lý toàn bộ logic và UI của trang Profile
 * 
 * @returns {JSX.Element} - JSX element chứa UI của trang Profile
 */
const Profile = () => {
  // Lấy thông tin user và trạng thái authentication từ custom hook
  // Destructuring để lấy user object và isAuthenticated boolean
  const { user, isAuthenticated } = useAuthState();
  
  // Hook để điều hướng (navigate) giữa các trang
  // navigate('/path') sẽ chuyển đến trang đó
  const navigate = useNavigate();
  
  // State quản lý tab đang active (đang được chọn)
  // useState<TabType>: TypeScript generic để đảm bảo chỉ nhận giá trị TabType
  // 'profile': Giá trị mặc định (tab đầu tiên)
  const [activeTab, setActiveTab] = useState<TabType>('profile');
  
  // State lưu thông tin profile của user
  // UserProfile | null: Có thể là UserProfile object hoặc null (chưa load)
  const [profile, setProfile] = useState<UserProfile | null>(null);
  
  // State quản lý trạng thái loading (đang tải dữ liệu)
  // true: Đang tải, false: Đã tải xong
  const [isLoading, setIsLoading] = useState(true);
  
  // State lưu thông báo lỗi (nếu có)
  // string | null: Có thể là string (thông báo lỗi) hoặc null (không có lỗi)
  const [error, setError] = useState<string | null>(null);
  
  // State kiểm tra xem user có đăng nhập bằng Google không
  // false: Mặc định (sẽ được cập nhật sau khi check)
  const [isGoogleUser, setIsGoogleUser] = useState(false);

  /**
   * useEffect: Redirect nếu user chưa đăng nhập hoặc là Admin/Facility_Manager
   * 
   * Side effect này chạy khi:
   * - Component mount lần đầu
   * - isAuthenticated thay đổi
   * - user object thay đổi
   * - navigate function thay đổi (ít khi xảy ra)
   * 
   * Logic:
   * 1. Nếu chưa đăng nhập → redirect đến /login
   * 2. Nếu là Admin hoặc Facility_Manager → redirect đến /admin/dashboard
   */
  useEffect(() => {
    // Kiểm tra nếu user chưa đăng nhập
    if (!isAuthenticated) {
      // Redirect đến trang login
      // state: { from: '/profile' }: Lưu thông tin trang trước đó để redirect lại sau khi login
      navigate('/login', { state: { from: '/profile' } });
      return; // Dừng lại, không chạy code phía dưới
    }

    // Kiểm tra nếu user là Admin hoặc Facility_Manager
    // user?.role: Optional chaining - chỉ truy cập role nếu user không null
    if (user && (user.role === 'Admin' || user.role === 'Facility_Manager')) {
      // Redirect đến admin dashboard
      navigate('/admin/dashboard');
      return; // Dừng lại
    }
  }, [isAuthenticated, user, navigate]); // Dependency array: Chạy lại khi các giá trị này thay đổi

  /**
   * useEffect: Load profile data khi user đã đăng nhập
   * 
   * Side effect này chạy khi:
   * - Component mount lần đầu
   * - isAuthenticated thay đổi
   * - user object thay đổi
   * 
   * Logic:
   * 1. Kiểm tra user đã đăng nhập và không phải Admin/Facility_Manager
   * 2. Kiểm tra xem có phải Google login không
   * 3. Gọi API để lấy thông tin profile
   */
  useEffect(() => {
    // Chỉ chạy nếu user đã đăng nhập và là Student/Lecturer
    if (isAuthenticated && user && user.role !== 'Admin' && user.role !== 'Facility_Manager') {
      // Kiểm tra xem user có đăng nhập bằng Google không
      // isGoogleLogin(): Trả về true nếu đăng nhập bằng Google, false nếu không
      setIsGoogleUser(isGoogleLogin());
      
      // Gọi function để fetch profile data từ API
      fetchProfile();
    }
  }, [isAuthenticated, user]); // Dependency array

  /**
   * Function: Fetch profile data từ API
   * 
   * Async function để gọi API lấy thông tin profile của user hiện tại
   * 
   * Flow:
   * 1. Set loading = true (hiển thị spinner)
   * 2. Clear error (xóa lỗi cũ nếu có)
   * 3. Gọi API getProfile()
   * 4. Nếu thành công → set profile data
   * 5. Nếu lỗi → set error message
   * 6. Cuối cùng → set loading = false (ẩn spinner)
   */
  const fetchProfile = async () => {
    // Bắt đầu loading
    setIsLoading(true);
    // Xóa error cũ
    setError(null);

    try {
      // Gọi API để lấy profile
      // getProfile(): Trả về Promise<ProfileResponse>
      const response = await getProfile();

      // Kiểm tra response thành công
      if (response.success && response.data) {
        // Lưu profile data vào state
        setProfile(response.data);
      } else {
        // Nếu không thành công, lấy error message
        // response.error?.message: Optional chaining - lấy message nếu error tồn tại
        // || '...': Fallback message nếu không có error message
        setError(response.error?.message || 'Không thể tải thông tin profile');
      }
    } catch (err: unknown) {
      // Xử lý exception (lỗi network, lỗi không mong đợi)
      // err: unknown: TypeScript yêu cầu type unknown cho catch
      // err instanceof Error: Kiểm tra xem có phải Error object không
      // ? err.message: Nếu là Error, lấy message
      // : '...': Nếu không, dùng message mặc định
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi không xác định';
      setError(errorMessage);
    } finally {
      // Luôn chạy, dù thành công hay lỗi
      // Dừng loading
      setIsLoading(false);
    }
  };

  /**
   * Function: Handle khi profile được cập nhật thành công
   * 
   * Function này được gọi từ ProfileForm component khi user cập nhật profile thành công
   * 
   * @param {UserProfile} updatedProfile - Profile object đã được cập nhật từ API
   * 
   * Logic:
   * 1. Cập nhật profile state
   * 2. Cập nhật auth_user trong sessionStorage để đồng bộ
   * 3. Dispatch event để các component khác biết profile đã thay đổi
   */
  const handleProfileUpdate = (updatedProfile: UserProfile) => {
    // Cập nhật profile state với data mới
    setProfile(updatedProfile);
    
    // Cập nhật auth user trong sessionStorage để đồng bộ
    // sessionStorage.getItem('auth_user'): Lấy string JSON từ storage
    // || '{}': Fallback về empty object nếu không có
    // JSON.parse(): Parse string thành object
    const authUser = JSON.parse(sessionStorage.getItem('auth_user') || '{}');
    
    // Kiểm tra nếu authUser tồn tại
    if (authUser) {
      // Cập nhật các field trong authUser với data mới từ updatedProfile
      authUser.fullName = updatedProfile.fullName;
      authUser.phoneNumber = updatedProfile.phoneNumber;
      authUser.avatarUrl = updatedProfile.avatarUrl;
      authUser.studentId = updatedProfile.studentId;
      
      // Lưu lại vào sessionStorage
      // JSON.stringify(): Convert object thành string JSON
      sessionStorage.setItem('auth_user', JSON.stringify(authUser));
      
      // Dispatch custom event để các component khác (như Header) biết profile đã thay đổi
      // 'auth:loginSuccess': Tên event (có thể dùng tên khác, nhưng dùng tên này để tương thích)
      // new Event(): Tạo event object mới
      window.dispatchEvent(new Event('auth:loginSuccess')); // Trigger update
    }
  };

  /**
   * Array định nghĩa tất cả các tabs có thể có
   * 
   * Mỗi tab có:
   * - id: TabType - ID của tab (dùng để identify và switch)
   * - label: string - Text hiển thị trên tab
   * - icon: React.ReactNode - Icon component từ lucide-react
   */
  const allTabs: { id: TabType; label: string; icon: React.ReactNode }[] = [
    { 
      id: 'profile', 
      label: 'Thông tin', 
      icon: <User className="w-4 h-4" /> // Icon User với size 4x4 (16px)
    },
    { 
      id: 'password', 
      label: 'Đổi mật khẩu', 
      icon: <Lock className="w-4 h-4" /> // Icon Lock
    },
    { 
      id: 'feedbacks', 
      label: 'Feedbacks', 
      icon: <MessageSquare className="w-4 h-4" /> // Icon MessageSquare
    },
  ];

  /**
   * Filter tabs: Ẩn tab "Đổi mật khẩu" nếu user đăng nhập bằng Google
   * 
   * allTabs.filter(): Lọc array, chỉ giữ lại các tab thỏa mãn điều kiện
   * 
   * Logic:
   * - Nếu tab là 'password' VÀ user là Google login → return false (bỏ tab này)
   * - Các trường hợp khác → return true (giữ tab)
   */
  const tabs = allTabs.filter(tab => {
    // Nếu tab là password và user đăng nhập bằng Google
    if (tab.id === 'password' && isGoogleUser) {
      return false; // Ẩn tab đổi mật khẩu cho Google login
    }
    return true; // Giữ các tab khác
  });

  /**
   * useEffect: Tự động chuyển tab nếu đang ở tab password nhưng user là Google login
   * 
   * Side effect này chạy khi:
   * - isGoogleUser thay đổi
   * - activeTab thay đổi
   * 
   * Logic:
   * - Nếu đang ở tab 'password' nhưng user là Google login
   * - → Tự động chuyển về tab 'profile'
   * 
   * Trường hợp xảy ra:
   * - User đăng nhập bằng Google nhưng URL có ?tab=password
   * - User đang ở tab password, sau đó đăng nhập lại bằng Google
   */
  useEffect(() => {
    // Kiểm tra nếu đang ở tab password và user là Google login
    if (activeTab === 'password' && isGoogleUser) {
      // Tự động chuyển về tab profile
      setActiveTab('profile');
    }
  }, [isGoogleUser, activeTab]); // Dependency array

  /**
   * Early return: Nếu chưa authenticated hoặc là Admin/Facility_Manager
   * 
   * Return null để không render gì (vì sẽ bị redirect bởi useEffect ở trên)
   * 
   * Lưu ý: useEffect redirect chạy trước, nhưng để an toàn vẫn check ở đây
   */
  if (!isAuthenticated || (user && (user.role === 'Admin' || user.role === 'Facility_Manager'))) {
    return null; // Will redirect
  }

  /**
   * Render UI
   * 
   * JSX structure:
   * - Container chính với background và padding
   * - Header với tiêu đề
   * - Loading state (spinner)
   * - Error state (thông báo lỗi)
   * - Content (tabs và tab content)
   */
  return (
    // Container chính
    // min-h-screen: Chiều cao tối thiểu bằng chiều cao màn hình
    // bg-gray-50: Background xám rất nhạt
    // py-8: Padding trên-dưới 8 units (32px)
    <div className="min-h-screen bg-gray-50 py-8">
      {/* Container nội dung
          mx-auto: Căn giữa theo chiều ngang
          max-w-6xl: Chiều rộng tối đa 6xl (1152px)
          px-4: Padding trái-phải 4 units (16px) */}
      <div className="mx-auto max-w-6xl px-4">
        {/* Header section */}
        <div className="mb-6">
          {/* Tiêu đề chính
              text-2xl: Font size 2xl (24px)
              font-bold: Font weight bold (700)
              text-gray-800: Màu xám đậm */}
          <h1 className="text-2xl font-bold text-gray-800">Hồ sơ của tôi</h1>
          
          {/* Mô tả phụ
              text-sm: Font size nhỏ (14px)
              text-gray-600: Màu xám vừa phải
              mt-1: Margin top 1 unit (4px) */}
          <p className="text-sm text-gray-600 mt-1">Quản lý thông tin cá nhân và hoạt động của bạn</p>
        </div>

        {/* Loading State: Hiển thị spinner khi đang tải dữ liệu
            Conditional rendering: Chỉ render nếu isLoading = true */}
        {isLoading && (
          <div className="flex items-center justify-center py-12">
            {/* Loader2 icon với animation spin
                w-8 h-8: Kích thước 8x8 units (32px)
                text-orange-500: Màu cam đậm
                animate-spin: Animation xoay tròn */}
            <Loader2 className="w-8 h-8 text-orange-500 animate-spin" />
          </div>
        )}

        {/* Error State: Hiển thị thông báo lỗi nếu có
            Conditional rendering: Chỉ render nếu có error VÀ không đang loading */}
        {error && !isLoading && (
          <div className="bg-red-50 border border-red-200 rounded-lg p-4 mb-6">
            {/* Text lỗi
                text-sm: Font size nhỏ
                text-red-600: Màu đỏ đậm */}
            <p className="text-sm text-red-600">{error}</p>
          </div>
        )}

        {/* Content: Chỉ hiển thị khi không đang loading
            Conditional rendering: Chỉ render nếu !isLoading (đã tải xong) */}
        {!isLoading && (
          <div className="space-y-6">
            {/* Tabs container
                bg-white: Background trắng
                rounded-lg: Bo góc lớn
                border border-gray-200: Border xám nhạt
                overflow-hidden: Ẩn phần tràn ra ngoài */}
            <div className="bg-white rounded-lg border border-gray-200 overflow-hidden">
              {/* Tabs header
                  flex: Sử dụng flexbox
                  flex-wrap: Cho phép wrap (xuống dòng)
                  border-b: Border bottom
                  border-gray-200: Màu border xám nhạt */}
              <div className="flex flex-wrap border-b border-gray-200">
                {/* Map qua array tabs để render từng tab button
                    tabs.map(): Duyệt qua từng tab
                    key={tab.id}: Key prop bắt buộc */}
                {tabs.map((tab) => (
                  <button
                    key={tab.id} // Key prop
                    // onClick handler: Chuyển sang tab được click
                    onClick={() => setActiveTab(tab.id)}
                    // Dynamic className dựa trên activeTab
                    // Template literal với ternary operator
                    className={`flex items-center gap-2 px-6 py-3 text-sm font-medium transition-colors ${
                      // Nếu tab này đang active
                      activeTab === tab.id
                        // Styling cho tab active
                        ? 'text-orange-600 border-b-2 border-orange-600 bg-orange-50'
                        // Styling cho tab không active
                        : 'text-gray-600 hover:text-gray-800 hover:bg-gray-50'
                    }`}
                  >
                    {/* Icon của tab */}
                    {tab.icon}
                    {/* Label của tab */}
                    {tab.label}
                  </button>
                ))}
              </div>
            </div>

            {/* Tab Content: Hiển thị nội dung của tab đang active
                Conditional rendering dựa trên activeTab */}
            <div>
              {/* Tab "profile": Hiển thị ProfileForm component
                  Chỉ render nếu activeTab === 'profile' */}
              {activeTab === 'profile' && (
                // ProfileForm component
                // profile={profile}: Truyền profile data vào component
                // onUpdateSuccess={handleProfileUpdate}: Callback khi update thành công
                <ProfileForm profile={profile} onUpdateSuccess={handleProfileUpdate} />
              )}

              {/* Tab "password": Hiển thị ChangePasswordForm component
                  Chỉ render nếu activeTab === 'password'
                  Lưu ý: Tab này đã bị ẩn nếu user là Google login */}
              {activeTab === 'password' && <ChangePasswordForm />}

              {/* Tab "feedbacks": Hiển thị MyFeedbacksTab component
                  Chỉ render nếu activeTab === 'feedbacks' */}
              {activeTab === 'feedbacks' && <MyFeedbacksTab />}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

// Export component để có thể import ở nơi khác
export default Profile;
