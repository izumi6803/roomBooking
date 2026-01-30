/**
 * ProfileForm Component - Form chỉnh sửa thông tin cá nhân
 * 
 * Component này cho phép user xem và chỉnh sửa thông tin profile của mình:
 * - Họ và tên: Read-only (không thể thay đổi)
 * - Email: Read-only (không thể thay đổi)
 * - Số điện thoại: Editable (có thể chỉnh sửa, validate format)
 * - MSSV (Mã số sinh viên): Chỉ hiển thị cho Student, editable
 * - Avatar: Upload và preview ảnh đại diện
 * 
 * Tính năng:
 * - Validation số điện thoại (10 số, bắt đầu bằng 0)
 * - Validation MSSV (format: SE/SS/IB/MC + 6 số)
 * - Upload avatar với preview
 * - Chỉ gửi API khi có thay đổi
 * - Hỗ trợ cả Student và Lecturer
 */

// Import React hooks: useState để quản lý state, useEffect để xử lý side effects, useRef để reference DOM element
import { useState, useEffect, useRef } from 'react';
// Import type và API function từ profileApi
import { type UserProfile, updateProfileWithAvatar } from '../api/profileApi';
// Import toast hook để hiển thị thông báo
import { useToast } from '../../../../components/toast';
// Import icons từ lucide-react
import { Loader2, Upload, X, Camera } from 'lucide-react';

/**
 * Interface định nghĩa props của ProfileForm component
 * 
 * @property {UserProfile | null} profile - Thông tin profile hiện tại (null nếu chưa load)
 * @property {(updatedProfile: UserProfile) => void} onUpdateSuccess - Callback được gọi khi update thành công
 */
interface ProfileFormProps {
  profile: UserProfile | null; // Profile data từ parent component
  onUpdateSuccess: (updatedProfile: UserProfile) => void; // Callback để thông báo parent component khi update thành công
}

/**
 * ProfileForm Component Function
 * 
 * Component form để chỉnh sửa thông tin cá nhân
 * 
 * @param {ProfileFormProps} props - Props của component
 * @returns {JSX.Element} - JSX element chứa form chỉnh sửa profile
 */
const ProfileForm = ({ profile, onUpdateSuccess }: ProfileFormProps) => {
  // Lấy các function từ toast hook để hiển thị thông báo
  // showSuccess: Hiển thị toast thành công (màu xanh)
  // showError: Hiển thị toast lỗi (màu đỏ)
  const { showSuccess, showError } = useToast();
  
  // useRef để reference đến file input element
  // HTMLInputElement: Type của DOM element
  // null: Giá trị mặc định (chưa có reference)
  // Dùng để trigger click programmatically
  const fileInputRef = useRef<HTMLInputElement>(null);
  
  // State quản lý dữ liệu form
  // Object chứa các field của form
  const [formData, setFormData] = useState({
    fullName: '',      // Họ và tên (read-only)
    email: '',        // Email (read-only)
    phoneNumber: '',  // Số điện thoại (editable)
    studentId: '',    // MSSV (editable, chỉ cho Student)
    avatarUrl: '',    // URL của avatar hiện tại
  });
  
  // State lưu file avatar được chọn (chưa upload)
  // File | null: Có thể là File object hoặc null (chưa chọn file)
  const [avatarFile, setAvatarFile] = useState<File | null>(null);
  
  // State lưu preview URL của avatar
  // string | null: Có thể là data URL (từ FileReader) hoặc URL từ server, hoặc null
  // Dùng để hiển thị preview trước khi upload
  const [avatarPreview, setAvatarPreview] = useState<string | null>(null);
  
  // State quản lý trạng thái editing (đang chỉnh sửa hay không)
  // false: Đang ở chế độ xem (read-only)
  // true: Đang ở chế độ chỉnh sửa (editable)
  const [isEditing, setIsEditing] = useState(false);
  
  // State quản lý trạng thái loading (đang submit form)
  // false: Không đang submit
  // true: Đang submit (hiển thị spinner)
  const [isLoading, setIsLoading] = useState(false);
  
  // State lưu thông báo lỗi
  // string | null: Có thể là string (thông báo lỗi) hoặc null (không có lỗi)
  const [error, setError] = useState<string | null>(null);
  
  // Debug: log profile roleName để kiểm tra
  // console.log: In ra console để debug (có thể xóa trong production)
  // profile?.roleName: Optional chaining - chỉ truy cập nếu profile không null
  console.log('Profile roleName:', profile?.roleName);
  
  /**
   * Kiểm tra xem user có phải Student không
   * 
   * Hỗ trợ nhiều cách kiểm tra để đảm bảo tương thích:
   * - roleName.toLowerCase() === 'student': Kiểm tra lowercase
   * - roleName === 'Student': Kiểm tra exact match
   * - roleName === 'Sinh viên': Kiểm tra tiếng Việt
   * - roleId === 'RL0001': Fallback - kiểm tra roleId (nếu roleName không có)
   * 
   * @returns {boolean} - true nếu là Student, false nếu không
   */
  const isStudent = profile?.roleName?.toLowerCase() === 'student' || 
                    profile?.roleName === 'Student' ||
                    profile?.roleName === 'Sinh viên' ||
                    profile?.roleId === 'RL0001'; // Fallback: check roleId

  /**
   * useEffect: Cập nhật formData khi profile thay đổi
   * 
   * Side effect này chạy khi:
   * - Component mount lần đầu
   * - profile prop thay đổi
   * 
   * Logic:
   * 1. Kiểm tra nếu profile tồn tại
   * 2. Cập nhật formData với data từ profile
   * 3. Cập nhật avatarPreview với avatarUrl từ profile
   */
  useEffect(() => {
    // Chỉ chạy nếu profile tồn tại
    if (profile) {
      // Debug: log profile data
      console.log('Profile data:', profile);
      
      // Cập nhật formData với data từ profile
      // || '': Fallback về empty string nếu giá trị là null/undefined
      setFormData({
        fullName: profile.fullName || '',
        email: profile.email || '',
        phoneNumber: profile.phoneNumber || '',
        studentId: profile.studentId || '',
        avatarUrl: profile.avatarUrl || '',
      });
      
      // Cập nhật avatarPreview với avatarUrl từ profile
      // || null: Fallback về null nếu không có avatarUrl
      setAvatarPreview(profile.avatarUrl || null);
    }
  }, [profile]); // Dependency array: Chạy lại khi profile thay đổi

  /**
   * Function: Validate số điện thoại
   * 
   * Kiểm tra format số điện thoại Việt Nam:
   * - Phải có đúng 10 chữ số
   * - Phải bắt đầu bằng số 0
   * - Cho phép để trống (optional field)
   * 
   * @param {string} phone - Số điện thoại cần validate
   * @returns {string | null} - null nếu hợp lệ, string (error message) nếu không hợp lệ
   */
  const validatePhoneNumber = (phone: string): string | null => {
    // Cho phép để trống (optional field)
    if (!phone || phone.trim() === '') {
      return null; // Cho phép để trống
    }
    
    // Chỉ cho phép số
    // replace(/\D/g, ''): Xóa tất cả ký tự không phải số
    // \D: Match bất kỳ ký tự nào không phải số (digit)
    // g: Global flag (thay thế tất cả, không chỉ cái đầu tiên)
    const digitsOnly = phone.replace(/\D/g, '');
    
    // Kiểm tra format: bắt đầu bằng 0 và có đúng 10 chữ số
    if (digitsOnly.length !== 10) {
      return 'Số điện thoại phải có đúng 10 chữ số';
    }
    
    // Kiểm tra phải bắt đầu bằng số 0
    if (!digitsOnly.startsWith('0')) {
      return 'Số điện thoại phải bắt đầu bằng số 0';
    }
    
    // Hợp lệ
    return null;
  };

  /**
   * Function: Validate MSSV (Mã số sinh viên)
   * 
   * Kiểm tra format MSSV FPT:
   * - Format: SE/SS/IB/MC + 6 số
   * - 2 số đầu phải >= 14 (năm nhập học từ 2014 trở đi)
   * - Cho phép để trống (optional field)
   * 
   * Ví dụ hợp lệ: SE173162, SS180123, IB191234, MC201234
   * 
   * Regex breakdown:
   * - ^(SE|SS|IB|MC): Bắt đầu bằng SE, SS, IB, hoặc MC
   * - (1[4-9]|[2-9][0-9]): 2 số đầu từ 14-99
   *   - 1[4-9]: 14-19
   *   - [2-9][0-9]: 20-99
   * - [0-9]{4}: 4 số cuối (0000-9999)
   * - $: Kết thúc chuỗi
   * 
   * @param {string} studentId - MSSV cần validate
   * @returns {string | null} - null nếu hợp lệ, string (error message) nếu không hợp lệ
   */
  const validateStudentId = (studentId: string): string | null => {
    // Cho phép để trống (optional field)
    if (!studentId || studentId.trim() === '') {
      return null; // Cho phép để trống
    }
    
    // Format: SE/SS/IB/MC + 6 số, 2 số đầu >= 14
    // Regex pattern để validate format
    const regex = /^(SE|SS|IB|MC)(1[4-9]|[2-9][0-9])[0-9]{4}$/;
    
    // Test với uppercase để không phân biệt hoa thường
    // toUpperCase(): Chuyển thành chữ hoa
    // test(): Kiểm tra xem string có match pattern không
    if (!regex.test(studentId.toUpperCase())) {
      return 'MSSV không hợp lệ. Ví dụ đúng: SE173162, SS180123';
    }
    
    // Hợp lệ
    return null;
  };

  /**
   * Function: Handle khi user chọn file avatar
   * 
   * Xử lý khi user chọn file từ file input:
   * 1. Validate file type (chỉ cho phép image)
   * 2. Validate file size (max 10MB)
   * 3. Tạo preview bằng FileReader
   * 
   * @param {React.ChangeEvent<HTMLInputElement>} e - Event object từ file input
   */
  const handleFileSelect = (e: React.ChangeEvent<HTMLInputElement>) => {
    // Lấy file đầu tiên từ file list
    // e.target.files: FileList object chứa các file được chọn
    // ?.[0]: Optional chaining - lấy file đầu tiên nếu có, undefined nếu không
    const file = e.target.files?.[0];
    
    // Nếu không có file, dừng lại
    if (!file) return;

    // Validate file type
    // Chỉ cho phép các định dạng ảnh phổ biến
    const allowedTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/webp'];
    
    // Kiểm tra xem file type có trong danh sách cho phép không
    // file.type: MIME type của file (ví dụ: 'image/jpeg')
    if (!allowedTypes.includes(file.type)) {
      // Hiển thị error toast
      showError('Chỉ chấp nhận file ảnh (jpg, png, gif, webp)');
      return; // Dừng lại, không xử lý file
    }

    // Validate file size (max 10MB)
    // file.size: Kích thước file tính bằng bytes
    // 10 * 1024 * 1024: 10MB = 10 * 1024KB * 1024B = 10,485,760 bytes
    if (file.size > 10 * 1024 * 1024) {
      // Hiển thị error toast
      showError('Kích thước file không được vượt quá 10MB');
      return; // Dừng lại
    }

    // Lưu file vào state
    setAvatarFile(file);
    
    // Create preview bằng FileReader
    // FileReader: Browser API để đọc file content
    const reader = new FileReader();
    
    // onloadend: Event handler khi đọc file xong (thành công hoặc thất bại)
    reader.onloadend = () => {
      // reader.result: Kết quả đọc file (data URL string)
      // as string: Type assertion - TypeScript biết đây là string
      setAvatarPreview(reader.result as string);
    };
    
    // readAsDataURL: Đọc file và convert thành data URL (base64)
    // Data URL format: data:image/jpeg;base64,/9j/4AAQSkZJRg...
    // Dùng để hiển thị preview ngay lập tức mà không cần upload lên server
    reader.readAsDataURL(file);
  };

  /**
   * Function: Handle khi user xóa avatar đã chọn
   * 
   * Xóa file đã chọn và reset về avatar cũ từ profile
   */
  const handleRemoveAvatar = () => {
    // Xóa file đã chọn
    setAvatarFile(null);
    
    // Reset preview về avatar cũ từ profile
    // profile?.avatarUrl: Optional chaining - lấy avatarUrl nếu profile tồn tại
    // || null: Fallback về null nếu không có
    setAvatarPreview(profile?.avatarUrl || null);
    
    // Reset file input value để có thể chọn lại file cùng tên
    // fileInputRef.current: Reference đến file input DOM element
    if (fileInputRef.current) {
      // value = '': Reset value của input
      // Cần thiết vì browser không trigger onChange nếu chọn lại file cùng tên
      fileInputRef.current.value = '';
    }
  };

  /**
   * Function: Handle khi user nhập liệu vào input
   * 
   * Xử lý input change cho tất cả các field:
   * - Số điện thoại: Chỉ cho phép nhập số (tự động filter)
   * - Các field khác: Cho phép nhập bình thường
   * - Tự động clear error khi user bắt đầu nhập
   * 
   * @param {React.ChangeEvent<HTMLInputElement>} e - Event object từ input
   */
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    // Destructuring để lấy name và value từ event target
    // name: Tên của input field (ví dụ: 'phoneNumber', 'studentId')
    // value: Giá trị mới được nhập
    const { name, value } = e.target;
    
    // Chỉ cho phép nhập số cho số điện thoại
    if (name === 'phoneNumber') {
      // Chỉ giữ lại số, xóa tất cả ký tự khác
      // replace(/\D/g, ''): Xóa tất cả ký tự không phải số
      const phoneValue = value.replace(/\D/g, '');
      
      // Cập nhật state với giá trị đã filter
      // setFormData với function updater để đảm bảo dùng state mới nhất
      // prev: State hiện tại
      // ...prev: Spread operator - giữ nguyên các field khác
      // [name]: phoneValue: Cập nhật field có tên = name với giá trị mới
      setFormData((prev) => ({
        ...prev,
        [name]: phoneValue, // Computed property name - dùng biến làm key
      }));
    } else {
      // Các field khác: Cho phép nhập bình thường
      setFormData((prev) => ({
        ...prev,
        [name]: value,
      }));
    }
    
    // Clear error khi user bắt đầu nhập
    setError(null);
  };

  /**
   * Function: Handle khi user submit form
   * 
   * Xử lý submit form:
   * 1. Validate tất cả fields
   * 2. Kiểm tra xem có thay đổi nào không
   * 3. Gọi API update profile
   * 4. Xử lý response và hiển thị thông báo
   * 
   * @param {React.FormEvent} e - Form submit event
   */
  const handleSubmit = async (e: React.FormEvent) => {
    // Prevent default form submission (reload page)
    e.preventDefault();
    
    // Clear error và bắt đầu loading
    setError(null);
    setIsLoading(true);

    try {
      // Validate phone number trước khi submit
      // Chỉ validate nếu có giá trị (cho phép để trống)
      if (formData.phoneNumber && formData.phoneNumber.trim() !== '') {
        const phoneError = validatePhoneNumber(formData.phoneNumber);
        if (phoneError) {
          // Nếu có lỗi, hiển thị và dừng lại
          setError(phoneError);
          showError(phoneError);
          setIsLoading(false);
          return; // Dừng lại, không submit
        }
      }

      // Validate studentId nếu là student
      // Chỉ validate nếu là student VÀ có giá trị
      if (isStudent && formData.studentId && formData.studentId.trim() !== '') {
        const studentIdError = validateStudentId(formData.studentId);
        if (studentIdError) {
          // Nếu có lỗi, hiển thị và dừng lại
          setError(studentIdError);
          showError(studentIdError);
          setIsLoading(false);
          return; // Dừng lại
        }
      }

      /**
       * Helper function: Normalize giá trị để so sánh
       * 
       * Chuẩn hóa giá trị bằng cách:
       * - Convert null/undefined thành empty string
       * - Trim whitespace
       * 
       * @param {string | null | undefined} value - Giá trị cần normalize
       * @returns {string} - Giá trị đã được normalize
       */
      const normalizeValue = (value: string | null | undefined): string => {
        return (value || '').trim();
      };
      
      // Normalize các giá trị để so sánh
      const currentPhone = normalizeValue(formData.phoneNumber);
      const originalPhone = normalizeValue(profile?.phoneNumber || null);
      
      const currentStudentId = normalizeValue(formData.studentId);
      const originalStudentId = normalizeValue(profile?.studentId || null);
      
      // Kiểm tra xem có thay đổi nào không
      const phoneChanged = currentPhone !== originalPhone;
      const studentIdChanged = isStudent && currentStudentId !== originalStudentId;
      const hasNewAvatar = avatarFile !== null; // Có file mới được chọn
      
      // Nếu không có thay đổi nào, không cần gọi API
      if (!phoneChanged && !studentIdChanged && !hasNewAvatar) {
        showSuccess('Không có thay đổi nào để lưu');
        setIsEditing(false); // Tắt chế độ editing
        setIsLoading(false);
        return; // Dừng lại
      }
      
      // Gọi API update profile với form-data (để upload avatar)
      // updateProfileWithAvatar: API function hỗ trợ upload file
      const response = await updateProfileWithAvatar({
        phoneNumber: currentPhone || undefined, // Chỉ gửi nếu có giá trị
        studentId: isStudent ? (currentStudentId || undefined) : undefined, // Chỉ gửi nếu là student
        avatar: avatarFile || undefined, // Chỉ gửi nếu có file mới
      });

      // Xử lý response
      if (response.success && response.data) {
        // Thành công
        showSuccess('Cập nhật profile thành công!');
        
        // Gọi callback để thông báo parent component
        // response.data: Profile object đã được cập nhật từ server
        onUpdateSuccess(response.data);
        
        // Tắt chế độ editing
        setIsEditing(false);
        
        // Reset avatar file
        setAvatarFile(null);
        
        // Reset file input
        if (fileInputRef.current) {
          fileInputRef.current.value = '';
        }
      } else {
        // Thất bại
        const errorMsg = response.error?.message || 'Cập nhật profile thất bại';
        setError(errorMsg);
        showError(errorMsg);
      }
    } catch (err: unknown) {
      // Xử lý exception (lỗi network, lỗi không mong đợi)
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi không xác định';
      setError(errorMessage);
      showError(errorMessage);
    } finally {
      // Luôn chạy, dù thành công hay lỗi
      setIsLoading(false);
    }
  };

  /**
   * Function: Handle khi user cancel (hủy chỉnh sửa)
   * 
   * Reset form về giá trị ban đầu từ profile và tắt chế độ editing
   */
  const handleCancel = () => {
    // Chỉ reset nếu có profile
    if (profile) {
      // Reset formData về giá trị ban đầu từ profile
      setFormData({
        fullName: profile.fullName || '',
        email: profile.email || '',
        phoneNumber: profile.phoneNumber || '',
        studentId: profile.studentId || '',
        avatarUrl: profile.avatarUrl || '',
      });
      
      // Reset avatar preview về avatar cũ
      setAvatarPreview(profile.avatarUrl || null);
      
      // Reset avatar file
      setAvatarFile(null);
      
      // Reset file input
      if (fileInputRef.current) {
        fileInputRef.current.value = '';
      }
    }
    
    // Tắt chế độ editing
    setIsEditing(false);
    
    // Clear error
    setError(null);
  };

  /**
   * Early return: Hiển thị loading nếu chưa có profile
   * 
   * Nếu profile chưa được load (null), hiển thị spinner
   */
  if (!profile) {
    return (
      <div className="flex items-center justify-center py-8">
        <Loader2 className="w-6 h-6 text-orange-500 animate-spin" />
      </div>
    );
  }

  /**
   * Render UI
   * 
   * JSX structure:
   * - Container với header và nút "Chỉnh sửa"
   * - Form với các input fields
   * - Avatar upload section
   * - Error message
   * - Action buttons (Lưu/Hủy)
   */
  return (
    // Container chính
    // bg-white: Background trắng
    // rounded-lg: Bo góc lớn
    // border border-gray-200: Border xám nhạt
    // p-6: Padding tất cả các phía 6 units (24px)
    <div className="bg-white rounded-lg border border-gray-200 p-6">
      {/* Header section với tiêu đề và nút "Chỉnh sửa"
          flex: Sử dụng flexbox
          items-center: Căn items vào giữa theo cross-axis
          justify-between: Khoảng cách giữa các items là space-between
          mb-6: Margin bottom 6 units (24px) */}
      <div className="flex items-center justify-between mb-6">
        {/* Tiêu đề
            text-xl: Font size xl (20px)
            font-semibold: Font weight semi-bold (600)
            text-gray-800: Màu xám đậm */}
        <h2 className="text-xl font-semibold text-gray-800">Thông tin cá nhân</h2>
        
        {/* Nút "Chỉnh sửa" - chỉ hiển thị khi không đang editing
            Conditional rendering: Chỉ render nếu !isEditing */}
        {!isEditing && (
          <button
            // onClick handler: Bật chế độ editing
            onClick={() => setIsEditing(true)}
            // Styling cho nút
            // px-4 py-2: Padding ngang 4 units, dọc 2 units
            // text-sm: Font size nhỏ
            // font-medium: Font weight medium (500)
            // text-orange-600: Màu chữ cam đậm
            // bg-orange-50: Background cam rất nhạt
            // rounded-lg: Bo góc lớn
            // hover:bg-orange-100: Khi hover, background đậm hơn
            // transition-colors: Smooth transition cho màu
            className="px-4 py-2 text-sm font-medium text-orange-600 bg-orange-50 rounded-lg hover:bg-orange-100 transition-colors"
          >
            Chỉnh sửa
          </button>
        )}
      </div>

      {/* Form element
          onSubmit: Handler khi submit form (Enter hoặc click nút submit)
          space-y-4: Khoảng cách dọc giữa các children là 4 units (16px) */}
      <form onSubmit={handleSubmit} className="space-y-4">
        {/* Full Name Field - Read Only
            Không thể chỉnh sửa vì được lấy từ hệ thống */}
        <div>
          {/* Label
              block: Display block (chiếm toàn bộ chiều rộng)
              text-sm: Font size nhỏ
              font-medium: Font weight medium
              text-gray-700: Màu xám vừa phải
              mb-1: Margin bottom 1 unit (4px) */}
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Họ và tên
          </label>
          
          {/* Input field
              type="text": Text input
              value: Giá trị từ formData
              disabled: Disabled (không thể chỉnh sửa)
              className: Styling cho disabled state
              - w-full: Chiều rộng 100%
              - px-3 py-2: Padding
              - border border-gray-300: Border xám
              - rounded-lg: Bo góc
              - bg-gray-50: Background xám nhạt (để phân biệt với editable)
              - text-gray-600: Màu chữ xám
              - cursor-not-allowed: Cursor "not allowed" */}
          <input
            type="text"
            value={formData.fullName}
            disabled
            className="w-full px-3 py-2 border border-gray-300 rounded-lg bg-gray-50 text-gray-600 cursor-not-allowed"
          />
          
          {/* Helper text
              mt-1: Margin top 1 unit
              text-xs: Font size rất nhỏ
              text-gray-500: Màu xám nhạt */}
          <p className="mt-1 text-xs text-gray-500">Họ và tên không thể thay đổi</p>
        </div>

        {/* Email Field - Read Only
            Tương tự Full Name, không thể chỉnh sửa */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Email
          </label>
          <input
            type="email"
            value={formData.email}
            disabled
            className="w-full px-3 py-2 border border-gray-300 rounded-lg bg-gray-50 text-gray-600 cursor-not-allowed"
          />
          <p className="mt-1 text-xs text-gray-500">Email không thể thay đổi</p>
        </div>

        {/* Phone Number Field - Editable
            Có thể chỉnh sửa khi isEditing = true */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Số điện thoại
          </label>
          <input
            type="tel" // tel: Input type cho số điện thoại (mobile keyboard sẽ hiển thị số)
            name="phoneNumber" // name: Dùng để identify field trong handleInputChange
            value={formData.phoneNumber}
            onChange={handleInputChange} // onChange handler
            disabled={!isEditing} // Disabled nếu không đang editing
            placeholder="Nhập số điện thoại"
            maxLength={10} // Giới hạn 10 ký tự
            // Dynamic className dựa trên isEditing
            className={`w-full px-3 py-2 border rounded-lg ${
              isEditing
                // Styling cho editable state
                ? 'border-gray-300 focus:border-orange-500 focus:ring-1 focus:ring-orange-500'
                // Styling cho disabled state
                : 'border-gray-300 bg-gray-50 text-gray-600 cursor-not-allowed'
            }`}
          />
          <p className="mt-1 text-xs text-gray-500">
            Số điện thoại phải bắt đầu bằng 0 và có đúng 10 chữ số
          </p>
        </div>

        {/* Student ID Field - Only for Students
            Chỉ hiển thị nếu user là Student
            Conditional rendering: Chỉ render nếu isStudent = true */}
        {isStudent && (
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              MSSV (Mã số sinh viên) <span className="text-orange-500">*</span>
            </label>
            <input
              type="text"
              name="studentId"
              value={formData.studentId}
              onChange={handleInputChange}
              disabled={!isEditing}
              placeholder="Ví dụ: SE173162"
              maxLength={8} // Giới hạn 8 ký tự (SE + 6 số)
              // uppercase: Tự động chuyển thành chữ hoa
              className={`w-full px-3 py-2 border rounded-lg uppercase ${
                isEditing
                  ? 'border-gray-300 focus:border-orange-500 focus:ring-1 focus:ring-orange-500'
                  : 'border-gray-300 bg-gray-50 text-gray-600 cursor-not-allowed'
              }`}
            />
            <p className="mt-1 text-xs text-gray-500">
              Format: SE/SS/IB/MC + 6 số. Ví dụ: SE173162, SS180123
            </p>
            
            {/* Warning nếu chưa có MSSV
                Conditional rendering: Chỉ hiển thị nếu chưa có studentId */}
            {!formData.studentId && (
              <p className="mt-1 text-xs text-orange-600">
                ⚠️ Bạn cần cập nhật MSSV để có thể đặt phòng
              </p>
            )}
          </div>
        )}

        {/* Avatar Upload Section */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            Ảnh đại diện
          </label>
          
          {/* Container cho avatar preview và upload button
              flex: Sử dụng flexbox
              items-start: Căn items về đầu (top)
              gap-4: Khoảng cách giữa các items là 4 units (16px) */}
          <div className="flex items-start gap-4">
            {/* Avatar Preview Container
                relative: Để nút remove có thể dùng absolute positioning */}
            <div className="relative">
              {/* Conditional rendering: Hiển thị preview nếu có, placeholder nếu không */}
              {avatarPreview ? (
                // Avatar image
                <img
                  src={avatarPreview} // Source: Data URL hoặc URL từ server
                  alt="Avatar preview"
                  // Styling cho avatar
                  // w-24 h-24: Kích thước 24x24 units (96px x 96px)
                  // rounded-full: Bo tròn hoàn toàn (hình tròn)
                  // border-2 border-gray-200: Border dày 2px, màu xám nhạt
                  // object-cover: Crop ảnh để vừa khung (giữ tỷ lệ)
                  className="w-24 h-24 rounded-full border-2 border-gray-200 object-cover"
                  // onError handler: Nếu ảnh không load được, dùng fallback
                  onError={(e) => {
                    // e.target: Image element
                    // as HTMLImageElement: Type assertion
                    // src: Set source mới
                    // ui-avatars.com: Service tạo avatar từ tên
                    // encodeURIComponent: Encode tên để dùng trong URL
                    (e.target as HTMLImageElement).src = 'https://ui-avatars.com/api/?name=' + encodeURIComponent(formData.fullName);
                  }}
                />
              ) : (
                // Placeholder nếu không có avatar
                <div className="w-24 h-24 rounded-full border-2 border-gray-200 bg-gray-100 flex items-center justify-center">
                  <Camera className="w-8 h-8 text-gray-400" />
                </div>
              )}
              
              {/* Remove button - chỉ hiển thị khi đang editing và có file mới
                  Conditional rendering */}
              {isEditing && avatarFile && (
                <button
                  type="button" // type="button": Không submit form khi click
                  onClick={handleRemoveAvatar}
                  // Styling cho nút remove
                  // absolute: Vị trí tuyệt đối
                  // -top-1 -right-1: Dịch lên trên và sang phải 1 unit (để nằm trên góc)
                  // p-1: Padding 1 unit
                  // bg-red-500: Background đỏ
                  // text-white: Màu chữ trắng
                  // rounded-full: Bo tròn hoàn toàn
                  // hover:bg-red-600: Khi hover, đậm hơn
                  className="absolute -top-1 -right-1 p-1 bg-red-500 text-white rounded-full hover:bg-red-600"
                >
                  <X className="w-3 h-3" />
                </button>
              )}
            </div>

            {/* Upload Button Section - chỉ hiển thị khi đang editing
                Conditional rendering */}
            {isEditing && (
              <div className="flex-1">
                {/* Hidden file input
                    type="file": File input
                    ref: Reference đến DOM element
                    onChange: Handler khi chọn file
                    accept: Chỉ cho phép chọn file ảnh
                    className="hidden": Ẩn input (dùng custom button) */}
                <input
                  type="file"
                  ref={fileInputRef}
                  onChange={handleFileSelect}
                  accept="image/jpeg,image/png,image/gif,image/webp"
                  className="hidden"
                />
                
                {/* Custom button để trigger file input
                    type="button": Không submit form
                    onClick: Trigger click trên file input
                    fileInputRef.current?.click(): Optional chaining - chỉ click nếu ref tồn tại */}
                <button
                  type="button"
                  onClick={() => fileInputRef.current?.click()}
                  className="flex items-center gap-2 px-4 py-2 text-sm font-medium text-orange-600 bg-orange-50 rounded-lg hover:bg-orange-100 transition-colors"
                >
                  <Upload className="w-4 h-4" />
                  Chọn ảnh
                </button>
                
                {/* Helper text */}
                <p className="mt-2 text-xs text-gray-500">
                  Định dạng: JPG, PNG, GIF, WebP. Tối đa 10MB.
                </p>
                
                {/* Hiển thị tên file đã chọn
                    Conditional rendering: Chỉ hiển thị nếu có file */}
                {avatarFile && (
                  <p className="mt-1 text-xs text-green-600">
                    ✓ Đã chọn: {avatarFile.name}
                  </p>
                )}
              </div>
            )}
          </div>
        </div>

        {/* Error Message
            Conditional rendering: Chỉ hiển thị nếu có error */}
        {error && (
          <div className="p-3 bg-red-50 border border-red-200 rounded-lg">
            <p className="text-sm text-red-600">{error}</p>
          </div>
        )}

        {/* Action Buttons - chỉ hiển thị khi đang editing
            Conditional rendering */}
        {isEditing && (
          <div className="flex items-center gap-3 pt-4">
            {/* Submit Button
                type="submit": Submit form khi click
                disabled: Disable khi đang loading */}
            <button
              type="submit"
              disabled={isLoading}
              className="flex-1 px-4 py-2 text-sm font-medium text-white bg-orange-500 rounded-lg hover:bg-orange-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors flex items-center justify-center gap-2"
            >
              {/* Conditional rendering: Hiển thị spinner nếu đang loading */}
              {isLoading ? (
                <>
                  <Loader2 className="w-4 h-4 animate-spin" />
                  Đang lưu...
                </>
              ) : (
                'Lưu thay đổi'
              )}
            </button>
            
            {/* Cancel Button
                type="button": Không submit form
                onClick: Handler để cancel */}
            <button
              type="button"
              onClick={handleCancel}
              disabled={isLoading}
              className="px-4 py-2 text-sm font-medium text-gray-700 bg-gray-100 rounded-lg hover:bg-gray-200 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
            >
              Hủy
            </button>
          </div>
        )}
      </form>
    </div>
  );
};

// Export component để có thể import ở nơi khác
export default ProfileForm;
