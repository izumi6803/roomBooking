import { useState } from 'react';
import { X, Loader2, AlertCircle, Mail } from 'lucide-react';
import { useToast } from '../../../components/toast';
import { verifyEmail, resendVerificationEmail } from '../api/emailLoginApi';

interface EmailVerificationModalProps {
  email: string;
  onVerified: () => void;
  onClose: () => void;
}

const EmailVerificationModal = ({ email, onVerified, onClose }: EmailVerificationModalProps) => {
  const { showSuccess, showError } = useToast();
  
  const [code, setCode] = useState('');
  const [loading, setLoading] = useState(false);
  const [resending, setResending] = useState(false);
  const [error, setError] = useState('');

  const handleVerify = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!code.trim()) {
      setError('Vui lòng nhập mã xác thực');
      return;
    }

    if (code.trim().length !== 6) {
      setError('Mã xác thực phải có 6 số');
      return;
    }

    setLoading(true);
    const result = await verifyEmail(email, code.trim());
    setLoading(false);

    if (result.success) {
      showSuccess(result.message);
      setTimeout(() => {
        onVerified();
      }, 1000);
    } else {
      setError(result.message);
      showError(result.message);
    }
  };

  const handleResend = async () => {
    setError('');
    setResending(true);

    const result = await resendVerificationEmail(email);
    setResending(false);

    if (result.success) {
      showSuccess(result.message);
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
              <h2 className="text-lg font-semibold text-gray-900">Xác thực email</h2>
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
            Chúng tôi đã gửi mã xác thực 6 số đến email <strong>{email}</strong>. 
            Vui lòng kiểm tra hộp thư và nhập mã bên dưới.
          </p>
          <p className="text-xs text-gray-500">
            Mã có hiệu lực trong 24 giờ. Nếu không nhận được email, vui lòng kiểm tra thư mục spam.
          </p>
        </div>

        {/* Error Message */}
        {error && (
          <div className="mb-4 flex items-center gap-2 rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">
            <AlertCircle className="h-4 w-4 flex-shrink-0" />
            <span>{error}</span>
          </div>
        )}

        {/* Verification Form */}
        <form onSubmit={handleVerify} className="space-y-4">
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

          <div className="flex gap-2">
            <button
              type="button"
              onClick={handleResend}
              disabled={resending || loading}
              className="flex-1 rounded-lg border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50 disabled:opacity-50"
            >
              {resending ? (
                <>
                  <Loader2 className="mr-2 inline h-4 w-4 animate-spin" />
                  Đang gửi...
                </>
              ) : (
                'Gửi lại mã'
              )}
            </button>
            <button
              type="submit"
              disabled={loading || code.length !== 6}
              className="flex-1 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {loading ? (
                <>
                  <Loader2 className="mr-2 inline h-4 w-4 animate-spin" />
                  Đang xác thực...
                </>
              ) : (
                'Xác thực'
              )}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default EmailVerificationModal;

