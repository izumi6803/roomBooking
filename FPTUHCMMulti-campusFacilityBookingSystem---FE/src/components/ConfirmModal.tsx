import { X, AlertCircle, CheckCircle2, Info, AlertTriangle, Building2, Clock, Calendar } from 'lucide-react';
import { useEffect } from 'react';

interface ConfirmModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: () => void;
  title: string;
  message: string;
  confirmText?: string;
  cancelText?: string;
  confirmColor?: 'red' | 'blue' | 'green' | 'purple';
  isLoading?: boolean;
}

export const ConfirmModal = ({
  isOpen,
  onClose,
  onConfirm,
  title,
  message,
  confirmText = 'Xác nhận',
  cancelText = 'Hủy',
  confirmColor = 'red',
  isLoading = false,
}: ConfirmModalProps) => {
  // Prevent body scroll when modal is open
  useEffect(() => {
    if (isOpen) {
      document.body.style.overflow = 'hidden';
    } else {
      document.body.style.overflow = 'unset';
    }
    return () => {
      document.body.style.overflow = 'unset';
    };
  }, [isOpen]);

  // Handle ESC key
  useEffect(() => {
    const handleEscape = (e: KeyboardEvent) => {
      if (e.key === 'Escape' && isOpen && !isLoading) {
        onClose();
      }
    };
    document.addEventListener('keydown', handleEscape);
    return () => document.removeEventListener('keydown', handleEscape);
  }, [isOpen, isLoading, onClose]);

  if (!isOpen) return null;

  const getConfirmButtonStyles = () => {
    switch (confirmColor) {
      case 'red':
        return {
          gradient: 'from-red-500 to-red-600',
          hover: 'hover:from-red-600 hover:to-red-700',
          shadow: 'shadow-red-500/50',
          iconBg: 'bg-red-100',
          iconColor: 'text-red-600',
          icon: <AlertCircle className="w-6 h-6" />,
        };
      case 'blue':
        return {
          gradient: 'from-blue-500 to-blue-600',
          hover: 'hover:from-blue-600 hover:to-blue-700',
          shadow: 'shadow-blue-500/50',
          iconBg: 'bg-blue-100',
          iconColor: 'text-blue-600',
          icon: <Info className="w-6 h-6" />,
        };
      case 'green':
        return {
          gradient: 'from-green-500 to-green-600',
          hover: 'hover:from-green-600 hover:to-green-700',
          shadow: 'shadow-green-500/50',
          iconBg: 'bg-green-100',
          iconColor: 'text-green-600',
          icon: <CheckCircle2 className="w-6 h-6" />,
        };
      case 'purple':
        return {
          gradient: 'from-purple-500 to-purple-600',
          hover: 'hover:from-purple-600 hover:to-purple-700',
          shadow: 'shadow-purple-500/50',
          iconBg: 'bg-purple-100',
          iconColor: 'text-purple-600',
          icon: <AlertTriangle className="w-6 h-6" />,
        };
      default:
        return {
          gradient: 'from-red-500 to-red-600',
          hover: 'hover:from-red-600 hover:to-red-700',
          shadow: 'shadow-red-500/50',
          iconBg: 'bg-red-100',
          iconColor: 'text-red-600',
          icon: <AlertCircle className="w-6 h-6" />,
        };
    }
  };

  const styles = getConfirmButtonStyles();

  return (
    <div className="fixed inset-0 z-50 overflow-y-auto">
      {/* Backdrop with animation */}
      <div
        className={`fixed inset-0 bg-gradient-to-br from-gray-900/70 via-gray-900/60 to-gray-900/70 backdrop-blur-md transition-all duration-300 ${
          isOpen ? 'opacity-100' : 'opacity-0'
        }`}
        onClick={!isLoading ? onClose : undefined}
      />

      {/* Modal Container */}
      <div className="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center sm:p-0">
        {/* Modal with animation */}
        <div
          className={`relative inline-block w-full max-w-md my-8 overflow-hidden text-left align-middle transition-all duration-300 transform ${
            isOpen
              ? 'scale-100 opacity-100 translate-y-0'
              : 'scale-95 opacity-0 translate-y-4'
          }`}
        >
          {/* Modal Card */}
          <div className="bg-white rounded-3xl shadow-2xl overflow-hidden border border-gray-100">
            {/* Header with gradient */}
            <div
              className={`relative px-6 py-5 bg-gradient-to-r ${styles.gradient} text-white`}
            >
              {/* Decorative pattern */}
              <div className="absolute inset-0 opacity-10">
                <div className="absolute inset-0 bg-[url('data:image/svg+xml,%3Csvg width=%2260%22 height=%2260%22 viewBox=%220%200%2060%2060%22 xmlns=%22http://www.w3.org/2000/svg%22%3E%3Cg fill=%22none%22 fill-rule=%22evenodd%22%3E%3Cg fill=%22%23ffffff%22 fill-opacity=%221%22%3E%3Cpath d=%22M36%2034v-4h-2v4h-4v2h4v4h2v-4h4v-2h-4zm0-30V0h-2v4h-4v2h4v4h2V6h4V4h-4zM6%2034v-4H4v4H0v2h4v4h2v-4h4v-2H6zM6%204V0H4v4H0v2h4v4h2V6h4V4H6z%22/%3E%3C/g%3E%3C/g%3E%3C/svg%3E')]"></div>
              </div>

              <div className="relative flex items-center justify-between">
                <div className="flex items-center gap-4">
                  {/* Icon */}
                  <div
                    className={`w-12 h-12 rounded-2xl ${styles.iconBg} ${styles.iconColor} flex items-center justify-center shadow-lg backdrop-blur-sm bg-white/20`}
                  >
                    {styles.icon}
                  </div>
                  <h3 className="text-xl font-bold text-white drop-shadow-sm">
                    {title}
                  </h3>
                </div>
                {!isLoading && (
                  <button
                    onClick={onClose}
                    className="text-white/80 hover:text-white hover:bg-white/20 rounded-lg p-1.5 transition-all duration-200"
                    aria-label="Đóng"
                  >
                    <X className="w-5 h-5" />
                  </button>
                )}
              </div>
            </div>

            {/* Content */}
            <div className="px-6 py-6">
              {/* Parse message to extract facility name and time */}
              {(() => {
                // Try to extract facility name and time from message
                const facilityMatch = message.match(/"([^"]+)"/);
                const timeMatch = message.match(/Thời gian:\s*(\d{2}:\d{2})\s*-\s*(\d{2}:\d{2})/);
                const facilityName = facilityMatch ? facilityMatch[1] : null;
                const startTime = timeMatch ? timeMatch[1] : null;
                const endTime = timeMatch ? timeMatch[2] : null;

                // If we can parse the message, show it in a nice format
                if (facilityName || (startTime && endTime)) {
                  // Remove time information from main message text
                  let mainMessage = message.split('"')[0] || 'Bạn có chắc chắn muốn thực hiện hành động này?';
                  // Remove "Thời gian: ..." part if it exists
                  mainMessage = mainMessage.replace(/Thời gian:\s*\d{2}:\d{2}\s*-\s*\d{2}:\d{2}/g, '').trim();
                  // Clean up any extra newlines or spaces
                  mainMessage = mainMessage.replace(/\n+/g, ' ').replace(/\s+/g, ' ').trim();
                  
                  // Get question text (after ?)
                  let questionText = '';
                  if (message.includes('?')) {
                    questionText = message.split('?').slice(1).join('?').trim();
                    // Remove time info from question text too
                    questionText = questionText.replace(/Thời gian:\s*\d{2}:\d{2}\s*-\s*\d{2}:\d{2}/g, '').trim();
                    questionText = questionText.replace(/\n+/g, ' ').replace(/\s+/g, ' ').trim();
                  }

                  return (
                    <div className="space-y-4">
                      {/* Main message text */}
                      <p className="text-gray-700 leading-relaxed text-base">
                        {mainMessage}
                      </p>

                      {/* Information cards */}
                      <div className="space-y-3">
                        {facilityName && (
                          <div className="flex items-center gap-3 p-3 bg-gradient-to-r from-blue-50 to-indigo-50 rounded-xl border border-blue-100">
                            <div className="flex-shrink-0 w-10 h-10 rounded-lg bg-blue-100 flex items-center justify-center">
                              <Building2 className="w-5 h-5 text-blue-600" />
                            </div>
                            <div className="flex-1 min-w-0">
                              <p className="text-xs font-medium text-blue-600 mb-0.5">Cơ sở vật chất</p>
                              <p className="text-sm font-semibold text-gray-900 truncate">{facilityName}</p>
                            </div>
                          </div>
                        )}

                        {startTime && endTime && (
                          <div className="flex items-center gap-3 p-3 bg-gradient-to-r from-purple-50 to-pink-50 rounded-xl border border-purple-100">
                            <div className="flex-shrink-0 w-10 h-10 rounded-lg bg-purple-100 flex items-center justify-center">
                              <Clock className="w-5 h-5 text-purple-600" />
                            </div>
                            <div className="flex-1 min-w-0">
                              <p className="text-xs font-medium text-purple-600 mb-0.5">Thời gian</p>
                              <p className="text-sm font-semibold text-gray-900">
                                {startTime} - {endTime}
                              </p>
                            </div>
                          </div>
                        )}
                      </div>

                      {/* Question text (only if exists and not empty) */}
                      {questionText && (
                        <p className="text-gray-600 text-sm font-medium pt-2">
                          {questionText}
                        </p>
                      )}
                    </div>
                  );
                }

                // Fallback: show message as is but with better styling
                return (
                  <div className="space-y-3">
                    <p className="text-gray-700 whitespace-pre-wrap leading-relaxed text-base">
                      {message}
                    </p>
                  </div>
                );
              })()}
            </div>

            {/* Footer */}
            <div className="px-6 py-4 bg-gradient-to-b from-gray-50 to-white border-t border-gray-100 flex flex-col sm:flex-row gap-3">
              <button
                onClick={onClose}
                disabled={isLoading}
                className="flex-1 px-5 py-3 border-2 border-gray-200 text-gray-700 rounded-xl font-semibold hover:bg-gray-50 hover:border-gray-300 transition-all duration-200 disabled:opacity-50 disabled:cursor-not-allowed active:scale-95"
              >
                {cancelText}
              </button>
              <button
                onClick={onConfirm}
                disabled={isLoading}
                className={`flex-1 px-5 py-3 bg-gradient-to-r ${styles.gradient} ${styles.hover} text-white rounded-xl font-semibold transition-all duration-200 disabled:opacity-50 disabled:cursor-not-allowed shadow-lg ${styles.shadow} hover:shadow-xl active:scale-95 flex items-center justify-center gap-2`}
              >
                {isLoading ? (
                  <>
                    <div className="w-5 h-5 border-2 border-white border-t-transparent rounded-full animate-spin" />
                    <span>Đang xử lý...</span>
                  </>
                ) : (
                  confirmText
                )}
              </button>
            </div>
          </div>

          {/* Glow effect */}
          <div
            className={`absolute -inset-1 bg-gradient-to-r ${styles.gradient} rounded-3xl opacity-20 blur-xl -z-10`}
          />
        </div>
      </div>
    </div>
  );
};

