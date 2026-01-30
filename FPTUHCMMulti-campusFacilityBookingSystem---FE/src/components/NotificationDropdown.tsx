import { useState, useEffect, useRef } from 'react';
import { Bell, X, Check, CheckCheck, Trash2, Filter, ChevronLeft, ChevronRight, Loader2, ChevronDown, ChevronUp } from 'lucide-react';
import {
  getNotifications,
  getUnreadCount,
  markNotificationAsRead,
  markAllNotificationsAsRead,
  deleteNotification,
  type Notification,
  type NotificationFilters,
} from '../page/User/Notification/api/notificationApi';
import { useToast } from './toast';

interface NotificationDropdownProps {
  isOpen: boolean;
  onClose: () => void;
}

// Notification types mapping
const notificationTypes = {
  Booking_Pending_Approval: 'Chờ phê duyệt',
  Booking_Approved: 'Đã phê duyệt',
  Booking_Rejected: 'Đã từ chối',
  Booking_Reminder_Checkin: 'Nhắc nhở check-in',
  Booking_Reminder_CheckOut: 'Nhắc nhở check-out',
  Feedback_Received: 'Phản hồi mới',
  Booking_No_Show: 'Không đến',
} as const;

const NotificationDropdown = ({ isOpen, onClose }: NotificationDropdownProps) => {
  const { showSuccess, showError } = useToast();
  const dropdownRef = useRef<HTMLDivElement>(null);
  
  // State management
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [unreadCount, setUnreadCount] = useState(0);
  const [loading, setLoading] = useState(false);
  const [markingAsRead, setMarkingAsRead] = useState<string | null>(null);
  const [deleting, setDeleting] = useState<string | null>(null);
  
  // Filter and pagination state
  const [filters, setFilters] = useState<NotificationFilters>({
    page: 1,
    limit: 10,
  });
  const [pagination, setPagination] = useState({
    page: 1,
    limit: 10,
    total: 0,
  });
  const [showFilters, setShowFilters] = useState(false);
  const [selectedType, setSelectedType] = useState<string>('');
  const [selectedStatus, setSelectedStatus] = useState<string>('');
  const [expandedNotifications, setExpandedNotifications] = useState<Set<string>>(new Set());

  // Close dropdown when clicking outside
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        onClose();
      }
    };

    if (isOpen) {
      document.addEventListener('mousedown', handleClickOutside);
      // Load notifications when dropdown opens
      loadNotifications();
      loadUnreadCount();
    }

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isOpen]);

  // Load notifications when filters change
  useEffect(() => {
    if (isOpen) {
      loadNotifications();
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [filters.page, filters.limit, filters.type, filters.status, isOpen]);

  // Load notifications
  const loadNotifications = async () => {
    setLoading(true);
    try {
      const response = await getNotifications(filters);
      if (response.success) {
        setNotifications(response.data || []);
        if (response.pagination) {
          setPagination(response.pagination);
        }
      } else {
        showError(response.error?.message || 'Không thể tải thông báo');
      }
    } catch (error: unknown) {
      console.error('Error loading notifications:', error);
      const errorMessage = error && typeof error === 'object' && 'response' in error 
        ? ((error as { response?: { data?: { error?: { message?: string } } } }).response?.data?.error?.message || 'Có lỗi xảy ra khi tải thông báo')
        : 'Có lỗi xảy ra khi tải thông báo';
      showError(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  // Load unread count
  const loadUnreadCount = async () => {
    try {
      const response = await getUnreadCount();
      if (response.success) {
        setUnreadCount(response.data || 0);
      }
    } catch (error) {
      console.error('Error loading unread count:', error);
    }
  };

  // Handle filter changes
  const handleFilterChange = () => {
    setFilters((prev) => ({
      ...prev,
      type: (selectedType as NotificationFilters['type']) || undefined,
      status: (selectedStatus as NotificationFilters['status']) || undefined,
      page: 1, // Reset to first page when filtering
    }));
    setShowFilters(false);
  };

  // Clear filters
  const clearFilters = () => {
    setSelectedType('');
    setSelectedStatus('');
    setFilters((prev) => ({
      ...prev,
      type: undefined,
      status: undefined,
      page: 1,
    }));
    setShowFilters(false);
  };

  // Mark notification as read
  const handleMarkAsRead = async (notificationId: string) => {
    setMarkingAsRead(notificationId);
    try {
      const response = await markNotificationAsRead(notificationId);
      if (response.success) {
        // Update local state
        setNotifications((prev) =>
          prev.map((notif) =>
            notif.notificationId === notificationId
              ? { ...notif, status: 'Read' as const, readAt: new Date().toISOString() }
              : notif
          )
        );
        // Update unread count
        setUnreadCount((prev) => Math.max(0, prev - 1));
        showSuccess('Đã đánh dấu thông báo là đã đọc');
      } else {
        showError(response.error?.message || 'Không thể đánh dấu thông báo');
      }
    } catch (error: unknown) {
      console.error('Error marking notification as read:', error);
      const errorMessage = error instanceof Error && 'response' in error 
        ? ((error as { response?: { data?: { error?: { message?: string } } } }).response?.data?.error?.message || 'Có lỗi xảy ra')
        : 'Có lỗi xảy ra';
      showError(errorMessage);
    } finally {
      setMarkingAsRead(null);
    }
  };

  // Mark all as read
  const handleMarkAllAsRead = async () => {
    setMarkingAsRead('all');
    try {
      const response = await markAllNotificationsAsRead();
      if (response.success) {
        // Update all notifications to read
        setNotifications((prev) =>
          prev.map((notif) => ({
            ...notif,
            status: 'Read' as const,
            readAt: notif.readAt || new Date().toISOString(),
          }))
        );
        setUnreadCount(0);
        showSuccess('Đã đánh dấu tất cả thông báo là đã đọc');
      } else {
        showError(response.error?.message || 'Không thể đánh dấu tất cả thông báo');
      }
    } catch (error: unknown) {
      console.error('Error marking all as read:', error);
      const errorMessage = error instanceof Error && 'response' in error 
        ? ((error as { response?: { data?: { error?: { message?: string } } } }).response?.data?.error?.message || 'Có lỗi xảy ra')
        : 'Có lỗi xảy ra';
      showError(errorMessage);
    } finally {
      setMarkingAsRead(null);
    }
  };

  // Delete notification
  const handleDelete = async (notificationId: string) => {
    if (!window.confirm('Bạn có chắc chắn muốn xóa thông báo này?')) {
      return;
    }

    setDeleting(notificationId);
    try {
      const response = await deleteNotification(notificationId);
      if (response.success) {
        // Remove from local state
        const deletedNotification = notifications.find((n) => n.notificationId === notificationId);
        setNotifications((prev) => prev.filter((notif) => notif.notificationId !== notificationId));
        // Update unread count if deleted notification was unread
        if (deletedNotification?.status === 'Unread') {
          setUnreadCount((prev) => Math.max(0, prev - 1));
        }
        // Adjust pagination if needed
        if (notifications.length === 1 && pagination.page > 1) {
          setFilters((prev) => ({ ...prev, page: prev.page! - 1 }));
        }
        showSuccess('Đã xóa thông báo');
      } else {
        showError(response.error?.message || 'Không thể xóa thông báo');
      }
    } catch (error: unknown) {
      console.error('Error deleting notification:', error);
      const errorMessage = error instanceof Error && 'response' in error 
        ? ((error as { response?: { data?: { error?: { message?: string } } } }).response?.data?.error?.message || 'Có lỗi xảy ra')
        : 'Có lỗi xảy ra';
      showError(errorMessage);
    } finally {
      setDeleting(null);
    }
  };

  // Pagination handlers
  const handlePreviousPage = () => {
    if (pagination.page > 1) {
      setFilters((prev) => ({ ...prev, page: prev.page! - 1 }));
    }
  };

  const handleNextPage = () => {
    const totalPages = Math.ceil(pagination.total / pagination.limit);
    if (pagination.page < totalPages) {
      setFilters((prev) => ({ ...prev, page: prev.page! + 1 }));
    }
  };

  // Toggle expand/collapse notification message
  const toggleExpand = (notificationId: string) => {
    setExpandedNotifications((prev) => {
      const newSet = new Set(prev);
      if (newSet.has(notificationId)) {
        newSet.delete(notificationId);
      } else {
        newSet.add(notificationId);
      }
      return newSet;
    });
  };

  // Check if message should show expand button (if message is longer than ~100 characters)
  const shouldShowExpand = (message: string) => {
    return message.length > 100;
  };

  // Format date
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    const now = new Date();
    const diffMs = now.getTime() - date.getTime();
    const diffMins = Math.floor(diffMs / 60000);
    const diffHours = Math.floor(diffMs / 3600000);
    const diffDays = Math.floor(diffMs / 86400000);

    if (diffMins < 1) return 'Vừa xong';
    if (diffMins < 60) return `${diffMins} phút trước`;
    if (diffHours < 24) return `${diffHours} giờ trước`;
    if (diffDays < 7) return `${diffDays} ngày trước`;
    return date.toLocaleDateString('vi-VN', { day: '2-digit', month: '2-digit', year: 'numeric' });
  };

  if (!isOpen) return null;

  const totalPages = Math.ceil(pagination.total / pagination.limit);
  const hasUnreadNotifications = notifications.some((n) => n.status === 'Unread');

  return (
    <div
      ref={dropdownRef}
      className="absolute right-0 mt-2 w-96 max-h-[600px] bg-white rounded-lg border border-gray-200 shadow-xl z-50 flex flex-col"
    >
      {/* Header */}
      <div className="flex items-center justify-between p-4 border-b border-gray-200">
        <div className="flex items-center gap-2">
          <Bell className="w-5 h-5 text-orange-600" />
          <h3 className="text-lg font-semibold text-gray-800">Thông báo</h3>
          {unreadCount > 0 && (
            <span className="px-2 py-0.5 bg-orange-500 text-white text-xs font-semibold rounded-full">
              {unreadCount}
            </span>
          )}
        </div>
        <div className="flex items-center gap-2">
          {hasUnreadNotifications && (
            <button
              onClick={handleMarkAllAsRead}
              disabled={markingAsRead === 'all'}
              className="p-1.5 text-gray-600 hover:text-orange-600 hover:bg-orange-50 rounded transition-colors disabled:opacity-50"
              title="Đánh dấu tất cả là đã đọc"
            >
              {markingAsRead === 'all' ? (
                <Loader2 className="w-4 h-4 animate-spin" />
              ) : (
                <CheckCheck className="w-4 h-4" />
              )}
            </button>
          )}
          <button
            onClick={() => setShowFilters(!showFilters)}
            className={`p-1.5 rounded transition-colors ${
              showFilters || selectedType || selectedStatus
                ? 'text-orange-600 bg-orange-50'
                : 'text-gray-600 hover:text-orange-600 hover:bg-orange-50'
            }`}
            title="Lọc thông báo"
          >
            <Filter className="w-4 h-4" />
          </button>
          <button
            onClick={onClose}
            className="p-1.5 text-gray-600 hover:text-gray-800 hover:bg-gray-100 rounded transition-colors"
            title="Đóng"
          >
            <X className="w-4 h-4" />
          </button>
        </div>
      </div>

      {/* Filters */}
      {showFilters && (
        <div className="p-4 border-b border-gray-200 bg-gray-50">
          <div className="space-y-3">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Loại thông báo</label>
              <select
                value={selectedType}
                onChange={(e) => setSelectedType(e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-orange-500"
              >
                <option value="">Tất cả</option>
                {Object.entries(notificationTypes).map(([key, label]) => (
                  <option key={key} value={key}>
                    {label}
                  </option>
                ))}
              </select>
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Trạng thái</label>
              <select
                value={selectedStatus}
                onChange={(e) => setSelectedStatus(e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-orange-500"
              >
                <option value="">Tất cả</option>
                <option value="Unread">Chưa đọc</option>
                <option value="Read">Đã đọc</option>
              </select>
            </div>
            <div className="flex gap-2">
              <button
                onClick={handleFilterChange}
                className="flex-1 px-3 py-2 bg-orange-600 text-white text-sm font-medium rounded-lg hover:bg-orange-700 transition-colors"
              >
                Áp dụng
              </button>
              <button
                onClick={clearFilters}
                className="px-3 py-2 bg-gray-200 text-gray-700 text-sm font-medium rounded-lg hover:bg-gray-300 transition-colors"
              >
                Xóa
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Notifications List */}
      <div className="flex-1 overflow-y-auto">
        {loading ? (
          <div className="flex items-center justify-center py-12">
            <Loader2 className="w-8 h-8 text-orange-600 animate-spin" />
          </div>
        ) : notifications.length === 0 ? (
          <div className="flex flex-col items-center justify-center py-12 px-4">
            <Bell className="w-12 h-12 text-gray-300 mb-3" />
            <p className="text-gray-500 text-sm">Không có thông báo nào</p>
          </div>
        ) : (
          <div className="divide-y divide-gray-100">
            {notifications.map((notification) => (
              <div
                key={notification.notificationId}
                className={`p-4 hover:bg-gray-50 transition-colors ${
                  notification.status === 'Unread' ? 'bg-orange-50/50' : ''
                }`}
              >
                <div className="flex items-start gap-3">
                  <div className="flex-1 min-w-0">
                    <div className="flex items-start justify-between gap-2 mb-1">
                      <h4 className="text-sm font-semibold text-gray-800 line-clamp-1">
                        {notification.title}
                      </h4>
                      {notification.status === 'Unread' && (
                        <span className="shrink-0 w-2 h-2 bg-orange-500 rounded-full mt-1.5" />
                      )}
                    </div>
                    <div className="mb-2">
                      <p
                        className={`text-sm text-gray-600 ${
                          expandedNotifications.has(notification.notificationId)
                            ? ''
                            : 'line-clamp-2'
                        }`}
                      >
                        {notification.message}
                      </p>
                      {shouldShowExpand(notification.message) && (
                        <button
                          onClick={() => toggleExpand(notification.notificationId)}
                          className="mt-1 text-xs text-orange-600 hover:text-orange-700 font-medium flex items-center gap-1 transition-colors"
                        >
                          {expandedNotifications.has(notification.notificationId) ? (
                            <>
                              <span>Thu gọn</span>
                              <ChevronUp className="w-3 h-3" />
                            </>
                          ) : (
                            <>
                              <span>Xem thêm</span>
                              <ChevronDown className="w-3 h-3" />
                            </>
                          )}
                        </button>
                      )}
                    </div>
                    <div className="flex items-center justify-between">
                      <div className="flex items-center gap-2 text-xs text-gray-500">
                        <span>
                          {notificationTypes[notification.type as keyof typeof notificationTypes] ||
                            notification.type}
                        </span>
                        <span>•</span>
                        <span>{formatDate(notification.createdAt)}</span>
                      </div>
                    </div>
                  </div>
                  <div className="flex items-start gap-1 shrink-0">
                    {notification.status === 'Unread' && (
                      <button
                        onClick={() => handleMarkAsRead(notification.notificationId)}
                        disabled={markingAsRead === notification.notificationId}
                        className="p-1.5 text-gray-600 hover:text-orange-600 hover:bg-orange-50 rounded transition-colors disabled:opacity-50"
                        title="Đánh dấu là đã đọc"
                      >
                        {markingAsRead === notification.notificationId ? (
                          <Loader2 className="w-4 h-4 animate-spin" />
                        ) : (
                          <Check className="w-4 h-4" />
                        )}
                      </button>
                    )}
                    <button
                      onClick={() => handleDelete(notification.notificationId)}
                      disabled={deleting === notification.notificationId}
                      className="p-1.5 text-gray-600 hover:text-red-600 hover:bg-red-50 rounded transition-colors disabled:opacity-50"
                      title="Xóa thông báo"
                    >
                      {deleting === notification.notificationId ? (
                        <Loader2 className="w-4 h-4 animate-spin" />
                      ) : (
                        <Trash2 className="w-4 h-4" />
                      )}
                    </button>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>

      {/* Pagination */}
      {!loading && notifications.length > 0 && totalPages > 1 && (
        <div className="flex items-center justify-between p-4 border-t border-gray-200 bg-gray-50">
          <button
            onClick={handlePreviousPage}
            disabled={pagination.page === 1}
            className="flex items-center gap-1 px-3 py-1.5 text-sm text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
          >
            <ChevronLeft className="w-4 h-4" />
            Trước
          </button>
          <span className="text-sm text-gray-600">
            Trang {pagination.page} / {totalPages}
          </span>
          <button
            onClick={handleNextPage}
            disabled={pagination.page >= totalPages}
            className="flex items-center gap-1 px-3 py-1.5 text-sm text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
          >
            Sau
            <ChevronRight className="w-4 h-4" />
          </button>
        </div>
      )}
    </div>
  );
};

export default NotificationDropdown;

