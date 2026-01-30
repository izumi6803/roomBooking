import { useState } from 'react';
import { X, Loader2, AlertCircle, Lock, Eye, EyeOff } from 'lucide-react';
import { useToast } from '../../../components/toast';
import { resetPassword } from '../api/emailLoginApi';

interface ResetPasswordModalProps {
  email: string;
  onClose: () => void;
  onSuccess: () => void;
}

const ResetPasswordModal = ({ email, onClose, onSuccess }: ResetPasswordModalProps) => {
  const { showSuccess, showError } = useToast();
  
  const [code, setCode] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const validatePassword = (password: string): { valid: boolean; message: string } => {
    if (password.length < 6) {
      return { valid: false, message: 'Mật khẩu phải có ít nhất 6 ký tự' };
    }
    return { valid: true, message: '' };
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    // Validate code
    if (!code.trim()) {
      setError('Vui lòng nhập mã xác thực');
      return;
    }

    if (code.trim().length !== 6) {
      setError('Mã xác thực phải có 6 số');
      return;
    }

    // Validate password
    if (!newPassword) {
      setError('Vui lòng nhập mật khẩu mới');
      return;
    }

    const passwordValidation = validatePassword(newPassword);
    if (!passwordValidation.valid) {
      setError(passwordValidation.message);
      return;
    }

    // Validate confirm password
    if (!confirmPassword) {
      setError('Vui lòng xác nhận mật khẩu');
      return;
    }

    if (newPassword !== confirmPassword) {
      setError('Mật khẩu xác nhận không khớp');
      return;
    }

    setLoading(true);
    const result = await resetPassword(email, code.trim(), newPassword);
    setLoading(false);

    if (result.success) {
      showSuccess(result.message);
      setTimeout(() => {
        onSuccess();
      }, 1500);
    } else {
      setError(result.message);
      showError(result.message);
    }
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
      <div className="w-full max-w-md rounded-2xl bg-white p-6 shadow-xl">
        {/* Header */}
        <div className="mb-4 flex items-center justify-between">
          <div className="flex items-center gap-3">
            <div className="rounded-full bg-orange-100 p-2">
              <Lock className="h-5 w-5 text-orange-600" />
            </div>
            <div>
              <h2 className="text-lg font-semibold text-gray-900">Đặt lại mật khẩu</h2>
              <p className="text-xs text-gray-500">{email}</p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="rounded-lg p-1 text-gray-400 hover:bg-gray-100 hover:text-gray-600"
          >
            <X className="h-5 w-5" />
          </button>
        </div>

        {/* Content */}
        <div className="mb-4 space-y-3">
          <p className="text-sm text-gray-600">
            Nhập mã 6 số đã được gửi đến email <strong>{email}</strong> và mật khẩu mới của bạn.
          </p>
          <p className="text-xs text-gray-500">
            Mã có hiệu lực trong 1 giờ. Nếu không nhận được email, vui lòng kiểm tra thư mục spam.
          </p>
        </div>

        {/* Error Message */}
        {error && (
          <div className="mb-4 flex items-center gap-2 rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">
            <AlertCircle className="h-4 w-4 shrink-0" />
            <span>{error}</span>
          </div>
        )}

        {/* Form */}
        <form onSubmit={handleSubmit} className="space-y-4">
          {/* Code Input */}
          <div>
            <label className="mb-1 block text-xs font-semibold text-gray-700" htmlFor="code">
              Mã xác thực (6 số)
            </label>
            <input
              id="code"
              type="text"
              value={code}
              onChange={(e) => {
                // Chỉ cho phép nhập số và tối đa 6 ký tự
                const value = e.target.value.replace(/\D/g, '').slice(0, 6);
                setCode(value);
              }}
              placeholder="000000"
              disabled={loading}
              maxLength={6}
              className="w-full rounded-lg border border-gray-200 px-3 py-2 text-center text-2xl font-mono tracking-widest outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100"
            />
          </div>

          {/* New Password Input */}
          <div>
            <label className="mb-1 block text-xs font-semibold text-gray-700" htmlFor="newPassword">
              Mật khẩu mới
            </label>
            <div className="relative">
              <input
                id="newPassword"
                type={showPassword ? 'text' : 'password'}
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
                placeholder="Nhập mật khẩu mới (tối thiểu 6 ký tự)"
                disabled={loading}
                className="w-full rounded-lg border border-gray-200 px-3 py-2 pr-10 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100"
              />
              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600"
              >
                {showPassword ? <EyeOff className="h-4 w-4" /> : <Eye className="h-4 w-4" />}
              </button>
            </div>
          </div>

          {/* Confirm Password Input */}
          <div>
            <label className="mb-1 block text-xs font-semibold text-gray-700" htmlFor="confirmPassword">
              Xác nhận mật khẩu
            </label>
            <div className="relative">
              <input
                id="confirmPassword"
                type={showConfirmPassword ? 'text' : 'password'}
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
                placeholder="Nhập lại mật khẩu mới"
                disabled={loading}
                className="w-full rounded-lg border border-gray-200 px-3 py-2 pr-10 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100"
              />
              <button
                type="button"
                onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600"
              >
                {showConfirmPassword ? <EyeOff className="h-4 w-4" /> : <Eye className="h-4 w-4" />}
              </button>
            </div>
          </div>

          <div className="flex gap-2">
            <button
              type="button"
              onClick={onClose}
              disabled={loading}
              className="flex-1 rounded-lg border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50 disabled:opacity-50"
            >
              Hủy
            </button>
            <button
              type="submit"
              disabled={loading || code.length !== 6 || !newPassword || !confirmPassword}
              className="flex-1 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {loading ? (
                <>
                  <Loader2 className="mr-2 inline h-4 w-4 animate-spin" />
                  Đang xử lý...
                </>
              ) : (
                'Đặt lại mật khẩu'
              )}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default ResetPasswordModal;

