/**
 * ChangePasswordForm Component - Form đổi mật khẩu
 * 
 * Component này cho phép user đổi mật khẩu của mình với các tính năng:
 * - Nhập mật khẩu cũ
 * - Nhập mật khẩu mới (với validation mạnh)
 * - Xác nhận mật khẩu mới
 * - Toggle visibility cho tất cả password fields (hiện/ẩn mật khẩu)
 * - Real-time validation với visual feedback
 * - Hiển thị yêu cầu mật khẩu với checkmarks
 * 
 * Yêu cầu mật khẩu:
 * - Ít nhất 8 ký tự
 * - Có ít nhất 1 chữ cái viết hoa
 * - Có ít nhất 1 chữ cái viết thường
 * - Có ít nhất 1 chữ số
 * - Có ít nhất 1 ký tự đặc biệt
 * 
 * Lưu ý: Component này chỉ hiển thị cho user đăng nhập bằng email/password,
 * không hiển thị cho user đăng nhập bằng Google (được xử lý ở Profile.tsx)
 */

// Import useState hook để quản lý state
import { useState } from 'react';
// Import API function và type từ profileApi
import { changePassword, type ChangePasswordRequest } from '../api/profileApi';
// Import toast hook để hiển thị thông báo
import { useToast } from '../../../../components/toast';
// Import icons từ lucide-react
import { Loader2, Eye, EyeOff } from 'lucide-react';

/**
 * ChangePasswordForm Component Function
 * 
 * Component form để đổi mật khẩu
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa form đổi mật khẩu
 */
const ChangePasswordForm = () => {
  // Lấy các function từ toast hook
  const { showSuccess, showError } = useToast();
  
  // State quản lý dữ liệu form
  // ChangePasswordRequest: Type từ API, đảm bảo type safety
  const [formData, setFormData] = useState<ChangePasswordRequest>({
    oldPassword: '',      // Mật khẩu cũ
    newPassword: '',      // Mật khẩu mới
    confirmPassword: '',  // Xác nhận mật khẩu mới
  });
  
  // State quản lý visibility của các password fields
  // Object với 3 keys, mỗi key là boolean
  // false: Ẩn mật khẩu (hiển thị dấu *)
  // true: Hiện mật khẩu (hiển thị plain text)
  const [showPasswords, setShowPasswords] = useState({
    oldPassword: false,      // Visibility của field mật khẩu cũ
    newPassword: false,      // Visibility của field mật khẩu mới
    confirmPassword: false,  // Visibility của field xác nhận mật khẩu
  });
  
  // State quản lý trạng thái loading (đang submit)
  const [isLoading, setIsLoading] = useState(false);
  
  // State lưu thông báo lỗi chung (từ API)
  const [error, setError] = useState<string | null>(null);
  
  // State lưu validation errors cho từng field
  // Object với optional properties cho mỗi field
  // Dùng để hiển thị lỗi validation riêng cho từng field
  const [validationErrors, setValidationErrors] = useState<{
    oldPassword?: string;      // Lỗi validation cho mật khẩu cũ
    newPassword?: string;      // Lỗi validation cho mật khẩu mới
    confirmPassword?: string;  // Lỗi validation cho xác nhận mật khẩu
  }>({});

  /**
   * Function: Validate mật khẩu theo yêu cầu
   * 
   * Kiểm tra mật khẩu có đáp ứng các yêu cầu:
   * 1. Ít nhất 8 ký tự
   * 2. Có ít nhất 1 chữ cái viết hoa (A-Z)
   * 3. Có ít nhất 1 chữ cái viết thường (a-z)
   * 4. Có ít nhất 1 chữ số (0-9)
   * 5. Có ít nhất 1 ký tự đặc biệt
   * 
   * Regex patterns:
   * - /[A-Z]/.test(password): Kiểm tra có chữ hoa không
   * - /[a-z]/.test(password): Kiểm tra có chữ thường không
   * - /[0-9]/.test(password): Kiểm tra có số không
   * - /[!@#$%^&*()_+\-={};':"\\|,.<>/?]/.test(password): Kiểm tra có ký tự đặc biệt không
   * 
   * @param {string} password - Mật khẩu cần validate
   * @returns {string | null} - null nếu hợp lệ, string (error message) nếu không hợp lệ
   */
  const validatePassword = (password: string): string | null => {
    // Kiểm tra độ dài: phải có ít nhất 8 ký tự
    if (password.length < 8) {
      return 'Mật khẩu phải có ít nhất 8 ký tự';
    }
    
    // Kiểm tra có chữ cái viết hoa không
    // /[A-Z]/: Regex pattern match bất kỳ chữ cái hoa nào
    // .test(password): Kiểm tra xem password có chứa pattern này không
    if (!/[A-Z]/.test(password)) {
      return 'Mật khẩu phải có ít nhất 1 chữ cái viết hoa';
    }
    
    // Kiểm tra có chữ cái viết thường không
    // /[a-z]/: Regex pattern match bất kỳ chữ cái thường nào
    if (!/[a-z]/.test(password)) {
      return 'Mật khẩu phải có ít nhất 1 chữ cái viết thường';
    }
    
    // Kiểm tra có chữ số không
    // /[0-9]/: Regex pattern match bất kỳ số nào (0-9)
    if (!/[0-9]/.test(password)) {
      return 'Mật khẩu phải có ít nhất 1 chữ số';
    }
    
    // Kiểm tra có ký tự đặc biệt không
    // /[!@#$%^&*()_+\-={};':"\\|,.<>/?]/: Regex pattern match các ký tự đặc biệt
    // Lưu ý: - cần escape thành \- trong character class
    if (!/[!@#$%^&*()_+\-={};':"\\|,.<>/?]/.test(password)) {
      return 'Mật khẩu phải có ít nhất 1 ký tự đặc biệt';
    }
    
    // Hợp lệ
    return null;
  };

  /**
   * Function: Handle khi user nhập liệu vào input
   * 
   * Xử lý input change cho tất cả các password fields:
   * 1. Cập nhật formData
   * 2. Clear errors khi user bắt đầu nhập
   * 3. Real-time validation cho newPassword
   * 4. Real-time validation cho confirmPassword (so sánh với newPassword)
   * 
   * @param {React.ChangeEvent<HTMLInputElement>} e - Event object từ input
   */
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    // Destructuring để lấy name và value
    const { name, value } = e.target;
    
    // Cập nhật formData
    setFormData((prev) => ({
      ...prev,
      [name]: value, // Computed property name
    }));
    
    // Clear errors khi user bắt đầu nhập
    setError(null);
    setValidationErrors((prev) => ({
      ...prev,
      [name]: undefined, // Clear error cho field này
    }));

    // Validate new password in real-time
    // Chỉ validate khi đang nhập vào field newPassword
    if (name === 'newPassword') {
      const validationError = validatePassword(value);
      if (validationError) {
        // Nếu có lỗi, lưu vào validationErrors
        setValidationErrors((prev) => ({
          ...prev,
          newPassword: validationError,
        }));
      } else {
        // Nếu hợp lệ, clear error
        setValidationErrors((prev) => ({
          ...prev,
          newPassword: undefined,
        }));
      }
    }

    // Validate confirm password in real-time
    // Chỉ validate khi đang nhập vào field confirmPassword
    if (name === 'confirmPassword') {
      // So sánh với newPassword
      if (value !== formData.newPassword) {
        // Nếu không khớp, hiển thị lỗi
        setValidationErrors((prev) => ({
          ...prev,
          confirmPassword: 'Mật khẩu xác nhận không khớp',
        }));
      } else {
        // Nếu khớp, clear error
        setValidationErrors((prev) => ({
          ...prev,
          confirmPassword: undefined,
        }));
      }
    }
  };

  /**
   * Function: Handle khi user submit form
   * 
   * Xử lý submit form:
   * 1. Validate tất cả fields
   * 2. Gọi API changePassword
   * 3. Xử lý response và hiển thị thông báo
   * 4. Reset form nếu thành công
   * 
   * @param {React.FormEvent} e - Form submit event
   */
  const handleSubmit = async (e: React.FormEvent) => {
    // Prevent default form submission
    e.preventDefault();
    
    // Clear errors
    setError(null);
    setValidationErrors({});

    // Validate all fields
    // Tạo object errors để lưu tất cả lỗi validation
    const errors: typeof validationErrors = {};

    // Validate oldPassword: Bắt buộc phải có
    if (!formData.oldPassword) {
      errors.oldPassword = 'Vui lòng nhập mật khẩu cũ';
    }

    // Validate newPassword: Bắt buộc phải có và phải hợp lệ
    if (!formData.newPassword) {
      errors.newPassword = 'Vui lòng nhập mật khẩu mới';
    } else {
      // Validate format
      const passwordError = validatePassword(formData.newPassword);
      if (passwordError) {
        errors.newPassword = passwordError;
      }
    }

    // Validate confirmPassword: Bắt buộc phải có và phải khớp với newPassword
    if (!formData.confirmPassword) {
      errors.confirmPassword = 'Vui lòng xác nhận mật khẩu mới';
    } else if (formData.confirmPassword !== formData.newPassword) {
      errors.confirmPassword = 'Mật khẩu xác nhận không khớp';
    }

    // Nếu có lỗi, hiển thị và dừng lại
    if (Object.keys(errors).length > 0) {
      setValidationErrors(errors);
      return; // Dừng lại, không submit
    }

    // Bắt đầu loading
    setIsLoading(true);

    try {
      // Gọi API để đổi mật khẩu
      const response = await changePassword(formData);

      if (response.success) {
        // Thành công
        showSuccess('Đổi mật khẩu thành công!');
        
        // Reset form về trạng thái ban đầu
        setFormData({
          oldPassword: '',
          newPassword: '',
          confirmPassword: '',
        });
      } else {
        // Thất bại
        const errorMsg = response.error?.message || 'Đổi mật khẩu thất bại';
        setError(errorMsg);
        showError(errorMsg);
      }
    } catch (err: unknown) {
      // Xử lý exception
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi không xác định';
      setError(errorMessage);
      showError(errorMessage);
    } finally {
      // Luôn chạy
      setIsLoading(false);
    }
  };

  /**
   * Function: Toggle visibility của password field
   * 
   * Chuyển đổi giữa hiện và ẩn mật khẩu cho một field cụ thể
   * 
   * @param {keyof typeof showPasswords} field - Tên field cần toggle (oldPassword, newPassword, hoặc confirmPassword)
   */
  const togglePasswordVisibility = (field: keyof typeof showPasswords) => {
    // Cập nhật state với function updater
    setShowPasswords((prev) => ({
      ...prev,
      [field]: !prev[field], // Toggle giá trị (true -> false, false -> true)
    }));
  };

  /**
   * Render UI
   * 
   * JSX structure:
   * - Container với tiêu đề
   * - Form với 3 password fields
   * - Password requirements checklist (cho newPassword)
   * - Error messages
   * - Submit button
   */
  return (
    // Container chính
    <div className="bg-white rounded-lg border border-gray-200 p-6">
      {/* Tiêu đề
          text-xl: Font size xl (20px)
          font-semibold: Font weight semi-bold (600)
          text-gray-800: Màu xám đậm
          mb-6: Margin bottom 6 units (24px) */}
      <h2 className="text-xl font-semibold text-gray-800 mb-6">Đổi mật khẩu</h2>

      {/* Form element
          onSubmit: Handler khi submit
          space-y-4: Khoảng cách dọc giữa các children */}
      <form onSubmit={handleSubmit} className="space-y-4">
        {/* Old Password Field */}
        <div>
          {/* Label với dấu * (required) */}
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Mật khẩu cũ <span className="text-red-500">*</span>
          </label>
          
          {/* Container với input và toggle button
              relative: Để toggle button có thể dùng absolute positioning */}
          <div className="relative">
            {/* Password input
                type: Dynamic - 'text' nếu show, 'password' nếu ẩn
                pr-10: Padding right 10 units để chừa chỗ cho toggle button */}
            <input
              type={showPasswords.oldPassword ? 'text' : 'password'}
              name="oldPassword"
              value={formData.oldPassword}
              onChange={handleInputChange}
              placeholder="Nhập mật khẩu cũ"
              // Dynamic className dựa trên validation error
              className={`w-full px-3 py-2 pr-10 border rounded-lg ${
                validationErrors.oldPassword
                  // Styling khi có lỗi (màu đỏ)
                  ? 'border-red-300 focus:border-red-500 focus:ring-1 focus:ring-red-500'
                  // Styling bình thường (màu cam khi focus)
                  : 'border-gray-300 focus:border-orange-500 focus:ring-1 focus:ring-orange-500'
              }`}
            />
            
            {/* Toggle visibility button
                absolute: Vị trí tuyệt đối
                right-3: Cách phải 3 units
                top-1/2 -translate-y-1/2: Căn giữa theo chiều dọc */}
            <button
              type="button"
              onClick={() => togglePasswordVisibility('oldPassword')}
              className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-gray-700"
            >
              {/* Conditional rendering: Hiển thị EyeOff nếu đang show, Eye nếu đang ẩn */}
              {showPasswords.oldPassword ? (
                <EyeOff className="w-4 h-4" />
              ) : (
                <Eye className="w-4 h-4" />
              )}
            </button>
          </div>
          
          {/* Validation error message
              Conditional rendering: Chỉ hiển thị nếu có lỗi */}
          {validationErrors.oldPassword && (
            <p className="mt-1 text-xs text-red-600">{validationErrors.oldPassword}</p>
          )}
        </div>

        {/* New Password Field */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Mật khẩu mới <span className="text-red-500">*</span>
          </label>
          <div className="relative">
            <input
              type={showPasswords.newPassword ? 'text' : 'password'}
              name="newPassword"
              value={formData.newPassword}
              onChange={handleInputChange}
              placeholder="Nhập mật khẩu mới"
              className={`w-full px-3 py-2 pr-10 border rounded-lg ${
                validationErrors.newPassword
                  ? 'border-red-300 focus:border-red-500 focus:ring-1 focus:ring-red-500'
                  : 'border-gray-300 focus:border-orange-500 focus:ring-1 focus:ring-orange-500'
              }`}
            />
            <button
              type="button"
              onClick={() => togglePasswordVisibility('newPassword')}
              className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-gray-700"
            >
              {showPasswords.newPassword ? (
                <EyeOff className="w-4 h-4" />
              ) : (
                <Eye className="w-4 h-4" />
              )}
            </button>
          </div>
          {validationErrors.newPassword && (
            <p className="mt-1 text-xs text-red-600">{validationErrors.newPassword}</p>
          )}
          
          {/* Password Requirements Checklist
              Hiển thị danh sách yêu cầu với visual feedback (màu xanh khi đạt) */}
          <div className="mt-2 p-3 bg-gray-50 rounded-lg">
            <p className="text-xs font-medium text-gray-700 mb-1">Yêu cầu mật khẩu:</p>
            <ul className="text-xs text-gray-600 space-y-0.5">
              {/* Requirement 1: Độ dài >= 8
                  Dynamic className: text-green-600 nếu đạt, text-gray-600 nếu chưa */}
              <li className={formData.newPassword.length >= 8 ? 'text-green-600' : ''}>
                • Ít nhất 8 ký tự
              </li>
              
              {/* Requirement 2: Có chữ hoa
                  /[A-Z]/.test(): Kiểm tra có chữ hoa không */}
              <li className={/[A-Z]/.test(formData.newPassword) ? 'text-green-600' : ''}>
                • Có chữ cái viết hoa
              </li>
              
              {/* Requirement 3: Có chữ thường */}
              <li className={/[a-z]/.test(formData.newPassword) ? 'text-green-600' : ''}>
                • Có chữ cái viết thường
              </li>
              
              {/* Requirement 4: Có số */}
              <li className={/[0-9]/.test(formData.newPassword) ? 'text-green-600' : ''}>
                • Có chữ số
              </li>
              
              {/* Requirement 5: Có ký tự đặc biệt */}
              <li className={/[!@#$%^&*()_+\-={};':"\\|,.<>/?]/.test(formData.newPassword) ? 'text-green-600' : ''}>
                • Có ký tự đặc biệt
              </li>
            </ul>
          </div>
        </div>

        {/* Confirm Password Field */}
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Xác nhận mật khẩu mới <span className="text-red-500">*</span>
          </label>
          <div className="relative">
            <input
              type={showPasswords.confirmPassword ? 'text' : 'password'}
              name="confirmPassword"
              value={formData.confirmPassword}
              onChange={handleInputChange}
              placeholder="Nhập lại mật khẩu mới"
              className={`w-full px-3 py-2 pr-10 border rounded-lg ${
                validationErrors.confirmPassword
                  ? 'border-red-300 focus:border-red-500 focus:ring-1 focus:ring-red-500'
                  : 'border-gray-300 focus:border-orange-500 focus:ring-1 focus:ring-orange-500'
              }`}
            />
            <button
              type="button"
              onClick={() => togglePasswordVisibility('confirmPassword')}
              className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-gray-700"
            >
              {showPasswords.confirmPassword ? (
                <EyeOff className="w-4 h-4" />
              ) : (
                <Eye className="w-4 h-4" />
              )}
            </button>
          </div>
          {validationErrors.confirmPassword && (
            <p className="mt-1 text-xs text-red-600">{validationErrors.confirmPassword}</p>
          )}
        </div>

        {/* Error Message (từ API) */}
        {error && (
          <div className="p-3 bg-red-50 border border-red-200 rounded-lg">
            <p className="text-sm text-red-600">{error}</p>
          </div>
        )}

        {/* Submit Button */}
        <div className="pt-4">
          <button
            type="submit"
            disabled={isLoading}
            className="w-full px-4 py-2 text-sm font-medium text-white bg-orange-500 rounded-lg hover:bg-orange-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors flex items-center justify-center gap-2"
          >
            {isLoading ? (
              <>
                <Loader2 className="w-4 h-4 animate-spin" />
                Đang đổi mật khẩu...
              </>
            ) : (
              'Đổi mật khẩu'
            )}
          </button>
        </div>
      </form>
    </div>
  );
};

// Export component
export default ChangePasswordForm;
