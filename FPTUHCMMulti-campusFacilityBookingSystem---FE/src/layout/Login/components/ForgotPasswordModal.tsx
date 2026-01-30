import { useState } from 'react';
import { X, Loader2, AlertCircle, Mail, ArrowLeft } from 'lucide-react';
import { useToast } from '../../../components/toast';
import { forgotPassword } from '../api/emailLoginApi';

interface ForgotPasswordModalProps {
  onClose: () => void;
  onCodeSent: (email: string) => void;
}

const ForgotPasswordModal = ({ onClose, onCodeSent }: ForgotPasswordModalProps) => {
  const { showSuccess, showError } = useToast();
  
  const [email, setEmail] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const validateEmail = (email: string): boolean => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!email.trim()) {
      setError('Vui lòng nhập email');
      return;
    }

    if (!validateEmail(email.trim())) {
      setError('Email không hợp lệ');
      return;
    }

    setLoading(true);
    const result = await forgotPassword(email.trim());
    setLoading(false);

    if (result.success) {
      showSuccess(result.message);
      setTimeout(() => {
        onCodeSent(email.trim());
      }, 1000);
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
              <Mail className="h-5 w-5 text-orange-600" />
            </div>
            <div>
              <h2 className="text-lg font-semibold text-gray-900">Quên mật khẩu</h2>
              <p className="text-xs text-gray-500">Nhập email để nhận mã đặt lại</p>
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
            Nhập email tài khoản của bạn. Chúng tôi sẽ gửi mã đặt lại mật khẩu 6 số đến email này.
          </p>
          <p className="text-xs text-gray-500">
            <strong>Lưu ý:</strong> Chức năng này không áp dụng cho tài khoản đăng nhập bằng Google.
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
          <div>
            <label className="mb-1 block text-xs font-semibold text-gray-700" htmlFor="email">
              Email
            </label>
            <input
              id="email"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Email của bạn"
              disabled={loading}
              className="w-full rounded-lg border border-gray-200 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100"
            />
          </div>

          <div className="flex gap-2">
            <button
              type="button"
              onClick={onClose}
              disabled={loading}
              className="flex-1 rounded-lg border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50 disabled:opacity-50"
            >
              <ArrowLeft className="mr-2 inline h-4 w-4" />
              Quay lại
            </button>
            <button
              type="submit"
              disabled={loading || !email.trim()}
              className="flex-1 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {loading ? (
                <>
                  <Loader2 className="mr-2 inline h-4 w-4 animate-spin" />
                  Đang gửi...
                </>
              ) : (
                'Gửi mã'
              )}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default ForgotPasswordModal;

