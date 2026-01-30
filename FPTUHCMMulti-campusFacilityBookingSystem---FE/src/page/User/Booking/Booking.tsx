import { useState, useEffect, useCallback } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { 
  MapPin, Users, Clock, Calendar, ChevronLeft, ChevronRight, 
  Building2, FlaskConical, Trophy, Check, X, AlertCircle,
  FileText, UserCheck, Loader2, CheckCircle2, Info, Phone
} from 'lucide-react';
import type { Facility, FacilityType } from '../../../types';
import { bookingApi, type TimeSlot, type BookingRequest } from './api/api';
import { useAuthState } from '../../../hooks/useAuthState';
import { getProfile, updateProfile } from '../Profile/api/profileApi';

const BookingPage = () => {
  const { facilityId } = useParams<{ facilityId: string }>();
  const navigate = useNavigate();
  const { user } = useAuthState();
  
  // States
  const [facility, setFacility] = useState<Facility | null>(null);
  const [loading, setLoading] = useState(true);
  const [selectedDate, setSelectedDate] = useState<string>('');
  const [timeSlots, setTimeSlots] = useState<TimeSlot[]>([]);
  const [loadingSlots, setLoadingSlots] = useState(false);
  const [selectedSlot, setSelectedSlot] = useState<TimeSlot | null>(null);
  const [purpose, setPurpose] = useState('');
  const [numberOfPeople, setNumberOfPeople] = useState(1);
  const [notes, setNotes] = useState('');
  const [showConfirmModal, setShowConfirmModal] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [bookingSuccess, setBookingSuccess] = useState(false);
  const [bookingId, setBookingId] = useState('');
  const [minimumBookingHours, setMinimumBookingHours] = useState<number | undefined>(undefined);
  const [userPhoneNumber, setUserPhoneNumber] = useState<string | null>(null);
  const [showPhoneModal, setShowPhoneModal] = useState(false);
  const [phoneInput, setPhoneInput] = useState('');
  const [phoneError, setPhoneError] = useState<string | null>(null);
  const [updatingPhone, setUpdatingPhone] = useState(false);

  // Initialize with today's date
  useEffect(() => {
    const today = new Date();
    setSelectedDate(today.toISOString().split('T')[0]);
  }, []);

  // Load system settings once on mount
  useEffect(() => {
    const loadSystemSettings = async () => {
      try {
        const settings = await bookingApi.getSystemSettings();
        if (settings) {
          setMinimumBookingHours(settings.minimumBookingHoursBeforeStart);
        } else {
          console.error('Failed to load system settings - booking may not work correctly');
          // Don't set a default - let the user see an error or disable booking
          setMinimumBookingHours(undefined);
        }
      } catch (error) {
        console.error('Error loading system settings:', error);
        // Don't set a default - let the user see an error or disable booking
        setMinimumBookingHours(undefined);
      }
    };

    loadSystemSettings();
  }, []);

  // Load user profile to check phone number
  useEffect(() => {
    const loadUserProfile = async () => {
      if (!user) return;
      
      try {
        const response = await getProfile();
        if (response.success && response.data) {
          setUserPhoneNumber(response.data.phoneNumber);
        }
      } catch (error) {
        console.error('Error loading user profile:', error);
      }
    };

    loadUserProfile();
  }, [user]);

  // Load facility details
  useEffect(() => {
    const loadFacility = async () => {
      if (!facilityId) return;
      
      setLoading(true);
      try {
        const data = await bookingApi.getFacilityById(facilityId);
        setFacility(data);
      } catch (error) {
        console.error('Error loading facility:', error);
      } finally {
        setLoading(false);
      }
    };

    loadFacility();
  }, [facilityId]);

  // Load time slots when date changes
  const loadTimeSlots = useCallback(async () => {
    if (!facilityId || !selectedDate) return;
    
    // Don't load time slots if system settings haven't been loaded yet
    if (minimumBookingHours === undefined) {
      setTimeSlots([]);
      return;
    }
    
    setLoadingSlots(true);
    setSelectedSlot(null);
    try {
      const slots = await bookingApi.getAvailableTimeSlots(facilityId, selectedDate, minimumBookingHours);
      setTimeSlots(slots);
    } catch (error) {
      console.error('Error loading time slots:', error);
      setTimeSlots([]);
    } finally {
      setLoadingSlots(false);
    }
  }, [facilityId, selectedDate, minimumBookingHours]);

  useEffect(() => {
    loadTimeSlots();
  }, [loadTimeSlots]);

  // Helper functions
  const getFacilityTypeLabel = (type: FacilityType): string => {
    const labels: Record<string, string> = {
      'Classroom': 'Phòng học',
      'Meeting Room': 'Phòng họp',
      'Laboratory': 'Phòng Lab',
      'Sport Facility': 'Sân thể thao',
      'meeting-room': 'Phòng họp',
      'lab-room': 'Phòng Lab',
      'sports-field': 'Sân thể thao',
    };
    return labels[type] || type;
  };

  const getFacilityTypeIcon = (type: FacilityType) => {
    switch (type) {
      case 'Meeting Room':
      case 'meeting-room':
      case 'Classroom':
        return <Building2 className="w-6 h-6" />;
      case 'Laboratory':
      case 'lab-room':
        return <FlaskConical className="w-6 h-6" />;
      case 'Sport Facility':
      case 'sports-field':
        return <Trophy className="w-6 h-6" />;
    }
  };

  const getFacilityTypeColor = (type: FacilityType) => {
    const colors: Record<string, { bg: string; text: string; gradient: string }> = {
      'Classroom': { 
        bg: 'bg-blue-100', 
        text: 'text-blue-700',
        gradient: 'from-blue-500 to-indigo-600'
      },
      'Meeting Room': { 
        bg: 'bg-violet-100', 
        text: 'text-violet-700',
        gradient: 'from-violet-500 to-purple-600'
      },
      'meeting-room': { 
        bg: 'bg-violet-100', 
        text: 'text-violet-700',
        gradient: 'from-violet-500 to-purple-600'
      },
      'Laboratory': { 
        bg: 'bg-amber-100', 
        text: 'text-amber-700',
        gradient: 'from-amber-500 to-orange-600'
      },
      'lab-room': { 
        bg: 'bg-amber-100', 
        text: 'text-amber-700',
        gradient: 'from-amber-500 to-orange-600'
      },
      'Sport Facility': { 
        bg: 'bg-emerald-100', 
        text: 'text-emerald-700',
        gradient: 'from-emerald-500 to-teal-600'
      },
      'sports-field': { 
        bg: 'bg-emerald-100', 
        text: 'text-emerald-700',
        gradient: 'from-emerald-500 to-teal-600'
      },
    };
    return colors[type] || { bg: 'bg-gray-100', text: 'text-gray-700', gradient: 'from-gray-500 to-gray-600' };
  };

  const formatDate = (dateString: string): string => {
    const date = new Date(dateString);
    return date.toLocaleDateString('vi-VN', {
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  const getMinDate = (): string => {
    return new Date().toISOString().split('T')[0];
  };

  const getMaxDate = (): string => {
    const maxDate = new Date();
    maxDate.setDate(maxDate.getDate() + 14); // Allow booking up to 14 days in advance
    return maxDate.toISOString().split('T')[0];
  };

  const changeDate = (days: number) => {
    const current = new Date(selectedDate);
    current.setDate(current.getDate() + days);
    const minDate = new Date(getMinDate());
    const maxDate = new Date(getMaxDate());
    
    if (current >= minDate && current <= maxDate) {
      setSelectedDate(current.toISOString().split('T')[0]);
    }
  };

  // Check if facility is a sports field (capacity === -1 or type is Sport Facility/sports-field)
  const isSportsField = facility && (
    facility.capacity === -1 || 
    facility.type === 'Sport Facility' || 
    facility.type === 'sports-field'
  );
  
  const canSubmit = selectedSlot && purpose.trim() && (isSportsField || numberOfPeople > 0);

  // Validate phone number format
  const validatePhoneNumber = (phone: string): string | null => {
    if (!phone || phone.trim() === '') {
      return 'Vui lòng nhập số điện thoại';
    }
    
    // Chỉ cho phép số
    const digitsOnly = phone.replace(/\D/g, '');
    
    // Kiểm tra format: bắt đầu bằng 0 và có đúng 10 chữ số
    if (digitsOnly.length !== 10) {
      return 'Số điện thoại phải có đúng 10 chữ số';
    }
    
    if (!digitsOnly.startsWith('0')) {
      return 'Số điện thoại phải bắt đầu bằng số 0';
    }
    
    return null;
  };

  const handleOpenConfirmModal = () => {
    if (!canSubmit) return;

    // Kiểm tra nếu user là Student và chưa có số điện thoại
    if (user?.role === 'Student' && (!userPhoneNumber || userPhoneNumber.trim() === '')) {
      setShowPhoneModal(true);
      setPhoneInput('');
      setPhoneError(null);
      return;
    }

    // Nếu đã có số điện thoại hoặc không phải Student, cho phép đặt phòng
    setShowConfirmModal(true);
  };

  const handleUpdatePhone = async () => {
    // Validate phone number
    const error = validatePhoneNumber(phoneInput);
    if (error) {
      setPhoneError(error);
      return;
    }

    setUpdatingPhone(true);
    setPhoneError(null);

    try {
      // Normalize phone number (chỉ giữ số)
      const normalizedPhone = phoneInput.replace(/\D/g, '');
      
      const response = await updateProfile({ phoneNumber: normalizedPhone });
      
      if (response.success && response.data) {
        setUserPhoneNumber(response.data.phoneNumber);
        setShowPhoneModal(false);
        setPhoneInput('');
        // Sau khi cập nhật thành công, mở confirm modal
        setShowConfirmModal(true);
      } else {
        setPhoneError(response.error?.message || 'Có lỗi xảy ra khi cập nhật số điện thoại');
      }
    } catch (error: unknown) {
      console.error('Error updating phone:', error);
      const errorMessage = error instanceof Error ? error.message : 'Có lỗi xảy ra khi cập nhật số điện thoại';
      setPhoneError(errorMessage);
    } finally {
      setUpdatingPhone(false);
    }
  };

  const handleGoToProfile = () => {
    setShowPhoneModal(false);
    navigate('/profile');
  };

  const handleSubmitBooking = async () => {
    if (!facility || !selectedSlot || !canSubmit || !user) return;

    setSubmitting(true);
    try {
      const bookingRequest: BookingRequest = {
        facilityId: facility.id,
        userId: user.user_id,
        date: selectedDate,
        timeSlotId: selectedSlot.id,
        startTime: selectedSlot.startTime,
        endTime: selectedSlot.endTime,
        purpose: purpose.trim(),
        numberOfPeople: isSportsField ? 1 : numberOfPeople, // Default to 1 for sports fields
        notes: notes.trim() || undefined,
      };

      const response = await bookingApi.submitBooking(bookingRequest);
      
      // Xử lý khi booking bị rejected (lỗi validation từ backend)
      if (response.status === 'rejected') {
        // Kiểm tra nếu lỗi yêu cầu cập nhật profile (số điện thoại, email, MSSV)
        const profileRequiredErrors = [
          'số điện thoại',
          'phone',
          'email',
          'mssv',
          'hồ sơ cá nhân',
          'profile',
          'student'
        ];
        
        const isProfileError = profileRequiredErrors.some(
          keyword => response.message.toLowerCase().includes(keyword)
        );
        
        if (isProfileError) {
          // Hiển thị confirm dialog để chuyển đến trang profile
          const shouldRedirect = window.confirm(
            `${response.message}\n\nBạn có muốn đến trang Hồ sơ để cập nhật thông tin không?`
          );
          
          if (shouldRedirect) {
            setShowConfirmModal(false);
            navigate('/profile');
            return;
          }
        } else {
          // Lỗi khác, hiển thị thông báo
          alert(response.message);
        }
        return;
      }
      
      setBookingId(response.id);
      setBookingSuccess(true);
    } catch (error) {
      console.error('Error submitting booking:', error);
      alert('Có lỗi xảy ra khi đặt phòng. Vui lòng thử lại!');
    } finally {
      setSubmitting(false);
    }
  };

  const handleCloseSuccessAndRedirect = () => {
    setShowConfirmModal(false);
    setBookingSuccess(false);
    navigate('/facilities');
  };

  // Loading state
  if (loading) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-slate-50 via-white to-orange-50/30 flex items-center justify-center">
        <div className="text-center">
          <Loader2 className="w-12 h-12 text-orange-500 animate-spin mx-auto mb-4" />
          <p className="text-gray-600">Đang tải thông tin...</p>
        </div>
      </div>
    );
  }

  // Facility not found
  if (!facility) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-slate-50 via-white to-orange-50/30 flex items-center justify-center">
        <div className="text-center max-w-md mx-auto px-4">
          <div className="w-20 h-20 rounded-full bg-red-100 flex items-center justify-center mx-auto mb-4">
            <AlertCircle className="w-10 h-10 text-red-500" />
          </div>
          <h2 className="text-xl font-bold text-gray-900 mb-2">Không tìm thấy cơ sở</h2>
          <p className="text-gray-600 mb-6">
            Cơ sở vật chất này không tồn tại hoặc không khả dụng để đặt.
          </p>
          <Link
            to="/facilities"
            className="inline-flex items-center gap-2 px-4 py-2 bg-orange-500 text-white rounded-lg hover:bg-orange-600 transition-colors"
          >
            <ChevronLeft className="w-4 h-4" />
            Quay lại danh sách
          </Link>
        </div>
      </div>
    );
  }

  const colors = getFacilityTypeColor(facility.type);

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-white to-orange-50/30">
      {/* Header */}
      <div className={`relative overflow-hidden bg-gradient-to-r ${colors.gradient}`}>
        <div className="absolute inset-0 bg-[url('data:image/svg+xml,%3Csvg%20width%3D%2260%22%20height%3D%2260%22%20viewBox%3D%220%200%2060%2060%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%3E%3Cg%20fill%3D%22none%22%20fill-rule%3D%22evenodd%22%3E%3Cg%20fill%3D%22%23ffffff%22%20fill-opacity%3D%220.05%22%3E%3Cpath%20d%3D%22M36%2034v-4h-2v4h-4v2h4v4h2v-4h4v-2h-4zm0-30V0h-2v4h-4v2h4v4h2V6h4V4h-4zM6%2034v-4H4v4H0v2h4v4h2v-4h4v-2H6zM6%204V0H4v4H0v2h4v4h2V6h4V4H6z%22%2F%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E')] opacity-30" />
        
        <div className="relative max-w-6xl mx-auto px-4 py-8">
          <Link
            to="/facilities"
            className="inline-flex items-center gap-2 text-white/80 hover:text-white text-sm font-medium mb-4 transition-colors"
          >
            <ChevronLeft className="w-4 h-4" />
            Quay lại danh sách
          </Link>
          
          <div className="flex items-start gap-4">
            <div className={`flex-shrink-0 w-14 h-14 rounded-xl bg-white/20 backdrop-blur-sm flex items-center justify-center text-white`}>
              {getFacilityTypeIcon(facility.type)}
            </div>
            <div>
              <h1 className="text-2xl sm:text-3xl font-bold text-white mb-1">
                {facility.name}
              </h1>
              <p className="text-white/80 text-sm">
                {getFacilityTypeLabel(facility.type)} • {facility.campus} Campus
              </p>
            </div>
          </div>
        </div>
      </div>

      <div className="max-w-6xl mx-auto px-4 py-8">
        <div className="grid lg:grid-cols-3 gap-8">
          {/* Left Column - Facility Details */}
          <div className="lg:col-span-1 space-y-6">
            {/* Facility Info Card */}
            <div className="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
              <div className={`h-2 bg-gradient-to-r ${colors.gradient}`} />
              <div className="p-6">
                <h3 className="font-semibold text-gray-900 mb-4 flex items-center gap-2">
                  <Info className="w-5 h-5 text-gray-400" />
                  Thông tin chi tiết
                </h3>
                
                <div className="space-y-4">
                  <div className="flex items-start gap-3">
                    <MapPin className="w-5 h-5 text-gray-400 flex-shrink-0 mt-0.5" />
                    <div>
                      <p className="text-sm font-medium text-gray-900">Vị trí</p>
                      <p className="text-sm text-gray-600">{facility.location}</p>
                    </div>
                  </div>
                  
                  <div className="flex items-start gap-3">
                    <Users className="w-5 h-5 text-gray-400 flex-shrink-0 mt-0.5" />
                    <div>
                      <p className="text-sm font-medium text-gray-900">Sức chứa</p>
                      <p className="text-sm text-gray-600">
                        {facility.capacity === -1 ? 'Nhiều người' : `${facility.capacity} người`}
                      </p>
                    </div>
                  </div>
                  
                  <div className="flex items-start gap-3">
                    <Clock className="w-5 h-5 text-gray-400 flex-shrink-0 mt-0.5" />
                    <div>
                      <p className="text-sm font-medium text-gray-900">Giờ hoạt động</p>
                      <p className="text-sm text-gray-600">07:00 - 21:00</p>
                    </div>
                  </div>
                </div>

                {facility.description && (
                  <div className="mt-4 pt-4 border-t border-gray-100">
                    <p className="text-sm text-gray-600">{facility.description}</p>
                  </div>
                )}

                {facility.amenities && facility.amenities.length > 0 && (
                  <div className="mt-4 pt-4 border-t border-gray-100">
                    <p className="text-sm font-medium text-gray-900 mb-2">Tiện ích</p>
                    <div className="flex flex-wrap gap-2">
                      {facility.amenities.map((amenity, index) => (
                        <span
                          key={index}
                          className={`px-2.5 py-1 ${colors.bg} ${colors.text} rounded-full text-xs font-medium`}
                        >
                          {amenity}
                        </span>
                      ))}
                    </div>
                  </div>
                )}
              </div>
            </div>

            {/* Booking Tips */}
            <div className="bg-gradient-to-br from-orange-50 to-amber-50 rounded-xl p-5 border border-orange-100">
              <h4 className="font-semibold text-orange-900 mb-3 flex items-center gap-2">
                <AlertCircle className="w-5 h-5" />
                Lưu ý khi đặt phòng
              </h4>
              <ul className="space-y-2 text-sm text-orange-800">
                <li className="flex items-start gap-2">
                  <Check className="w-4 h-4 flex-shrink-0 mt-0.5" />
                  <span>Đặt phòng trước tối đa 14 ngày</span>
                </li>
                <li className="flex items-start gap-2">
                  <Check className="w-4 h-4 flex-shrink-0 mt-0.5" />
                  <span>Yêu cầu sẽ được xét duyệt trong 24h</span>
                </li>
                <li className="flex items-start gap-2">
                  <Check className="w-4 h-4 flex-shrink-0 mt-0.5" />
                  <span>Hủy đặt phòng trước 2 giờ sử dụng</span>
                </li>
              </ul>
            </div>
          </div>

          {/* Right Column - Booking Form */}
          <div className="lg:col-span-2">
            <div className="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
              <div className="p-6 border-b border-gray-100">
                <h2 className="text-xl font-bold text-gray-900">Đặt phòng</h2>
                <p className="text-sm text-gray-500 mt-1">Chọn ngày, giờ và điền thông tin để đặt phòng</p>
              </div>

              <div className="p-6 space-y-6">
                {/* Date Selection - Visual Calendar */}
                <div>
                  <label className="block text-sm font-medium text-gray-900 mb-3">
                    <Calendar className="w-4 h-4 inline mr-2" />
                    Chọn ngày đặt phòng
                  </label>
                  
                  {/* Week Navigation */}
                  <div className="flex items-center justify-between mb-3">
                    <button
                      onClick={() => changeDate(-7)}
                      disabled={new Date(selectedDate).getTime() - 7 * 24 * 60 * 60 * 1000 < new Date(getMinDate()).getTime()}
                      className="flex items-center gap-1 px-3 py-1.5 text-sm text-gray-600 hover:text-gray-900 hover:bg-gray-100 rounded-lg disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                    >
                      <ChevronLeft className="w-4 h-4" />
                      Tuần trước
                    </button>
                    <span className="text-sm font-medium text-gray-700">
                      {new Date(selectedDate).toLocaleDateString('vi-VN', { month: 'long', year: 'numeric' })}
                    </span>
                    <button
                      onClick={() => changeDate(7)}
                      disabled={new Date(selectedDate).getTime() + 7 * 24 * 60 * 60 * 1000 > new Date(getMaxDate()).getTime()}
                      className="flex items-center gap-1 px-3 py-1.5 text-sm text-gray-600 hover:text-gray-900 hover:bg-gray-100 rounded-lg disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                    >
                      Tuần sau
                      <ChevronRight className="w-4 h-4" />
                    </button>
                  </div>

                  {/* Calendar Grid - Show 1 week (7 days) */}
                  <div className="grid grid-cols-7 gap-2 mb-3">
                    {/* Day headers */}
                    {['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'].map((day) => (
                      <div key={day} className="text-center text-xs font-medium text-gray-500 py-1">
                        {day}
                      </div>
                    ))}
                    
                    {/* Date buttons - Show 1 week (7 days) starting from Sunday */}
                    {(() => {
                      const today = new Date();
                      const selected = new Date(selectedDate);
                      
                      // Find the Sunday of the week containing selectedDate
                      const sunday = new Date(selected);
                      const dayOfWeek = sunday.getDay(); // 0 = Sunday, 1 = Monday, ...
                      sunday.setDate(selected.getDate() - dayOfWeek);
                      
                      // Create array of 7 dates starting from Sunday
                      const dates = Array.from({ length: 7 }, (_, i) => {
                        const date = new Date(sunday);
                        date.setDate(sunday.getDate() + i);
                        return date;
                      });
                      
                      return dates.map((date) => {
                        const dateStr = date.toISOString().split('T')[0];
                        const isSelected = dateStr === selectedDate;
                        const isToday = dateStr === today.toISOString().split('T')[0];
                        const dayOfWeek = date.getDay();
                        const isWeekend = dayOfWeek === 0 || dayOfWeek === 6;
                        
                        // Check if date is within allowed range
                        const dateTime = date.getTime();
                        const minDate = new Date(getMinDate()).getTime();
                        const maxDate = new Date(getMaxDate()).getTime();
                        const isDisabled = dateTime < minDate || dateTime > maxDate;
                        
                        return (
                          <button
                            key={dateStr}
                            onClick={() => !isDisabled && setSelectedDate(dateStr)}
                            disabled={isDisabled}
                            className={`relative p-2 rounded-lg text-center transition-all ${
                              isDisabled
                                ? 'bg-gray-100 text-gray-300 cursor-not-allowed border border-gray-200'
                                : isSelected
                                ? `bg-gradient-to-r ${colors.gradient} text-white shadow-md`
                                : isToday
                                ? 'bg-orange-50 text-orange-700 border-2 border-orange-300 hover:bg-orange-100'
                                : isWeekend
                                ? 'bg-gray-50 text-gray-600 hover:bg-gray-100 border border-gray-200'
                                : 'bg-white text-gray-700 hover:bg-gray-50 border border-gray-200'
                            }`}
                          >
                            <div className="text-lg font-semibold">{date.getDate()}</div>
                            <div className={`text-[10px] ${isSelected ? 'text-white/80' : 'text-gray-400'}`}>
                              {date.toLocaleDateString('vi-VN', { weekday: 'short' })}
                            </div>
                            {isToday && !isSelected && !isDisabled && (
                              <div className="absolute -top-1 -right-1 w-2 h-2 bg-orange-500 rounded-full" />
                            )}
                          </button>
                        );
                      });
                    })()}
                  </div>

                  {/* Selected date display */}
                  <div className={`p-3 rounded-lg ${colors.bg} flex items-center justify-between`}>
                    <div className="flex items-center gap-2">
                      <Calendar className={`w-5 h-5 ${colors.text}`} />
                      <span className={`font-medium ${colors.text}`}>
                        {formatDate(selectedDate)}
                      </span>
                    </div>
                    {selectedDate === getMinDate() && (
                      <span className={`text-xs px-2 py-0.5 rounded-full bg-white/50 ${colors.text}`}>
                        Hôm nay
                      </span>
                    )}
                  </div>

                  {/* Alternative: Native date picker for accessibility */}
                  <div className="mt-3 flex items-center gap-2">
                    <span className="text-xs text-gray-500">Hoặc chọn ngày cụ thể:</span>
                    <input
                      type="date"
                      value={selectedDate}
                      onChange={(e) => setSelectedDate(e.target.value)}
                      min={getMinDate()}
                      max={getMaxDate()}
                      className="px-3 py-1.5 text-sm border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                    />
                  </div>
                </div>

                {/* Time Slots */}
                <div>
                  <label className="block text-sm font-medium text-gray-900 mb-3">
                    <Clock className="w-4 h-4 inline mr-2" />
                    Chọn khung giờ
                  </label>
                  
                  {minimumBookingHours === undefined ? (
                    <div className="p-4 bg-yellow-50 border border-yellow-200 rounded-lg">
                      <div className="flex items-start gap-2">
                        <AlertCircle className="w-5 h-5 text-yellow-600 flex-shrink-0 mt-0.5" />
                        <div>
                          <p className="text-sm font-medium text-yellow-900 mb-1">
                            Không thể tải cài đặt hệ thống
                          </p>
                          <p className="text-sm text-yellow-700">
                            Vui lòng làm mới trang hoặc liên hệ quản trị viên nếu vấn đề vẫn tiếp tục.
                          </p>
                        </div>
                      </div>
                    </div>
                  ) : loadingSlots ? (
                    <div className="flex items-center justify-center py-8">
                      <Loader2 className="w-6 h-6 text-orange-500 animate-spin" />
                      <span className="ml-2 text-gray-600">Đang tải lịch...</span>
                    </div>
                  ) : (
                    <div className="grid grid-cols-4 sm:grid-cols-7 gap-2">
                      {timeSlots.map((slot) => (
                        <button
                          key={slot.id}
                          onClick={() => slot.isAvailable && setSelectedSlot(slot)}
                          disabled={!slot.isAvailable}
                          className={`p-2.5 rounded-lg text-sm font-medium transition-all ${
                            selectedSlot?.id === slot.id
                              ? `bg-gradient-to-r ${colors.gradient} text-white shadow-md`
                              : slot.isAvailable
                              ? 'bg-gray-50 text-gray-700 hover:bg-gray-100 border border-gray-200'
                              : 'bg-gray-100 text-gray-400 cursor-not-allowed line-through'
                          }`}
                        >
                          {slot.startTime}
                        </button>
                      ))}
                    </div>
                  )}
                  
                  {selectedSlot && (
                    <div className={`mt-3 p-3 rounded-lg ${colors.bg} flex items-center gap-2`}>
                      <Check className={`w-4 h-4 ${colors.text}`} />
                      <span className={`text-sm font-medium ${colors.text}`}>
                        Đã chọn: {selectedSlot.startTime} - {selectedSlot.endTime}
                      </span>
                    </div>
                  )}
                  
                  <div className="mt-3 flex items-center gap-4 text-xs text-gray-500">
                    <div className="flex items-center gap-1.5">
                      <div className="w-3 h-3 rounded bg-gray-50 border border-gray-200" />
                      <span>Còn trống</span>
                    </div>
                    <div className="flex items-center gap-1.5">
                      <div className="w-3 h-3 rounded bg-gray-100" />
                      <span>Đã đặt</span>
                    </div>
                    <div className="flex items-center gap-1.5">
                      <div className={`w-3 h-3 rounded bg-gradient-to-r ${colors.gradient}`} />
                      <span>Đang chọn</span>
                    </div>
                  </div>
                </div>

                {/* Purpose */}
                <div>
                  <label className="block text-sm font-medium text-gray-900 mb-2">
                    <FileText className="w-4 h-4 inline mr-2" />
                    Mục đích sử dụng <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="text"
                    value={purpose}
                    onChange={(e) => setPurpose(e.target.value)}
                    placeholder="VD: Họp nhóm dự án, Thực hành môn học..."
                    className="w-full px-4 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                  />
                </div>

                {/* Number of People - Hidden for sports fields */}
                {!isSportsField && (
                  <div>
                    <label className="block text-sm font-medium text-gray-900 mb-2">
                      <UserCheck className="w-4 h-4 inline mr-2" />
                      Số người tham gia <span className="text-red-500">*</span>
                    </label>
                    <input
                      type="number"
                      value={numberOfPeople}
                      onChange={(e) => setNumberOfPeople(Math.max(1, Math.min(facility.capacity, parseInt(e.target.value) || 1)))}
                      min={1}
                      max={facility.capacity}
                      className="w-full px-4 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                    />
                    <p className="text-xs text-gray-500 mt-1">
                      Tối đa {facility.capacity} người
                    </p>
                  </div>
                )}

                {/* Notes */}
                <div>
                  <label className="block text-sm font-medium text-gray-900 mb-2">
                    Ghi chú thêm (tùy chọn)
                  </label>
                  <textarea
                    value={notes}
                    onChange={(e) => setNotes(e.target.value)}
                    placeholder="Yêu cầu đặc biệt, thiết bị cần chuẩn bị..."
                    rows={3}
                    className="w-full px-4 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500 resize-none"
                  />
                </div>

                {/* Submit Button */}
                <button
                  onClick={handleOpenConfirmModal}
                  disabled={!canSubmit}
                  className={`w-full py-3 rounded-xl font-semibold text-white transition-all ${
                    canSubmit
                      ? `bg-gradient-to-r ${colors.gradient} hover:opacity-90 shadow-lg`
                      : 'bg-gray-300 cursor-not-allowed'
                  }`}
                >
                  Xác nhận đặt phòng
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Phone Number Required Modal */}
      {showPhoneModal && (
        <div className="fixed inset-0 z-50 overflow-y-auto">
          <div className="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center sm:p-0">
            <div 
              className="fixed inset-0 bg-gray-900/60 backdrop-blur-sm transition-opacity"
              onClick={() => !updatingPhone && setShowPhoneModal(false)}
            />
            
            <div className="relative inline-block w-full max-w-md my-8 overflow-hidden text-left align-middle transition-all transform bg-white rounded-2xl shadow-xl">
              {/* Modal Header */}
              <div className="px-6 py-4 bg-gradient-to-r from-orange-500 to-amber-600">
                <div className="flex items-center justify-between">
                  <h3 className="text-lg font-bold text-white flex items-center gap-2">
                    <Phone className="w-5 h-5" />
                    Cập nhật số điện thoại
                  </h3>
                  {!updatingPhone && (
                    <button
                      onClick={() => setShowPhoneModal(false)}
                      className="text-white/80 hover:text-white transition-colors"
                    >
                      <X className="w-5 h-5" />
                    </button>
                  )}
                </div>
              </div>

              {/* Modal Content */}
              <div className="px-6 py-5">
                <div className="space-y-4">
                  {/* Warning Message */}
                  <div className="flex items-start gap-3 p-4 bg-amber-50 rounded-lg border border-amber-200">
                    <AlertCircle className="w-5 h-5 text-amber-600 flex-shrink-0 mt-0.5" />
                    <div>
                      <p className="text-sm font-medium text-amber-900 mb-1">
                        Số điện thoại là bắt buộc
                      </p>
                      <p className="text-sm text-amber-800">
                        Bạn cần cập nhật số điện thoại để có thể đặt phòng. Số điện thoại sẽ được sử dụng để liên hệ khi cần thiết.
                      </p>
                    </div>
                  </div>

                  {/* Phone Input */}
                  <div>
                    <label className="block text-sm font-medium text-gray-900 mb-2">
                      Số điện thoại <span className="text-red-500">*</span>
                    </label>
                    <input
                      type="tel"
                      value={phoneInput}
                      onChange={(e) => {
                        const value = e.target.value.replace(/\D/g, ''); // Chỉ cho phép số
                        if (value.length <= 10) {
                          setPhoneInput(value);
                          setPhoneError(null);
                        }
                      }}
                      placeholder="Nhập số điện thoại (VD: 0912345678)"
                      disabled={updatingPhone}
                      className={`w-full px-4 py-2.5 border rounded-lg focus:outline-none focus:ring-2 transition-colors ${
                        phoneError
                          ? 'border-red-300 focus:ring-red-500/20 focus:border-red-500'
                          : 'border-gray-200 focus:ring-orange-500/20 focus:border-orange-500'
                      } disabled:bg-gray-100 disabled:cursor-not-allowed`}
                    />
                    {phoneError && (
                      <p className="mt-1 text-sm text-red-600">{phoneError}</p>
                    )}
                    <p className="mt-1 text-xs text-gray-500">
                      Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0
                    </p>
                  </div>
                </div>
              </div>

              {/* Modal Footer */}
              <div className="px-6 py-4 bg-gray-50 flex gap-3">
                <button
                  onClick={handleGoToProfile}
                  disabled={updatingPhone}
                  className="flex-1 px-4 py-2.5 border border-gray-300 text-gray-700 rounded-xl font-medium hover:bg-gray-100 transition-colors disabled:opacity-50"
                >
                  Đến trang Hồ sơ
                </button>
                <button
                  onClick={handleUpdatePhone}
                  disabled={updatingPhone || !phoneInput.trim()}
                  className="flex-1 px-4 py-2.5 bg-gradient-to-r from-orange-500 to-amber-600 text-white rounded-xl font-medium hover:opacity-90 transition-opacity disabled:opacity-50 flex items-center justify-center gap-2"
                >
                  {updatingPhone ? (
                    <>
                      <Loader2 className="w-4 h-4 animate-spin" />
                      Đang cập nhật...
                    </>
                  ) : (
                    <>
                      <Check className="w-4 h-4" />
                      Cập nhật
                    </>
                  )}
                </button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Confirmation Modal */}
      {showConfirmModal && (
        <div className="fixed inset-0 z-50 overflow-y-auto">
          <div className="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center sm:p-0">
            <div 
              className="fixed inset-0 bg-gray-900/60 backdrop-blur-sm transition-opacity"
              onClick={() => !submitting && !bookingSuccess && setShowConfirmModal(false)}
            />
            
            <div className="relative inline-block w-full max-w-lg my-8 overflow-hidden text-left align-middle transition-all transform bg-white rounded-2xl shadow-xl">
              {!bookingSuccess ? (
                <>
                  {/* Modal Header */}
                  <div className={`px-6 py-4 bg-gradient-to-r ${colors.gradient}`}>
                    <div className="flex items-center justify-between">
                      <h3 className="text-lg font-bold text-white">
                        Xác nhận đặt phòng
                      </h3>
                      {!submitting && (
                        <button
                          onClick={() => setShowConfirmModal(false)}
                          className="text-white/80 hover:text-white transition-colors"
                        >
                          <X className="w-5 h-5" />
                        </button>
                      )}
                    </div>
                  </div>

                  {/* Modal Content */}
                  <div className="px-6 py-5">
                    <div className="space-y-4">
                      {/* Facility Info */}
                      <div className="flex items-center gap-3 p-4 bg-gray-50 rounded-xl">
                        <div className={`w-12 h-12 rounded-lg ${colors.bg} ${colors.text} flex items-center justify-center`}>
                          {getFacilityTypeIcon(facility.type)}
                        </div>
                        <div>
                          <h4 className="font-semibold text-gray-900">{facility.name}</h4>
                          <p className="text-sm text-gray-500">{facility.location}</p>
                        </div>
                      </div>

                      {/* Booking Details */}
                      <div className="space-y-3">
                        <div className="flex justify-between items-center py-2 border-b border-gray-100">
                          <span className="text-sm text-gray-600">Ngày</span>
                          <span className="font-medium text-gray-900">{formatDate(selectedDate)}</span>
                        </div>
                        <div className="flex justify-between items-center py-2 border-b border-gray-100">
                          <span className="text-sm text-gray-600">Thời gian</span>
                          <span className="font-medium text-gray-900">
                            {selectedSlot?.startTime} - {selectedSlot?.endTime}
                          </span>
                        </div>
                        <div className="flex justify-between items-center py-2 border-b border-gray-100">
                          <span className="text-sm text-gray-600">Mục đích</span>
                          <span className="font-medium text-gray-900">{purpose}</span>
                        </div>
                        {!isSportsField && (
                          <div className="flex justify-between items-center py-2 border-b border-gray-100">
                            <span className="text-sm text-gray-600">Số người</span>
                            <span className="font-medium text-gray-900">{numberOfPeople} người</span>
                          </div>
                        )}
                        {notes && (
                          <div className="py-2">
                            <span className="text-sm text-gray-600 block mb-1">Ghi chú</span>
                            <span className="text-sm text-gray-900">{notes}</span>
                          </div>
                        )}
                      </div>

                      {/* Warning */}
                      <div className="flex items-start gap-3 p-3 bg-amber-50 rounded-lg border border-amber-200">
                        <AlertCircle className="w-5 h-5 text-amber-600 flex-shrink-0 mt-0.5" />
                        <p className="text-sm text-amber-800">
                          Sau khi gửi, yêu cầu sẽ được xét duyệt. Bạn sẽ nhận thông báo qua email khi có kết quả.
                        </p>
                      </div>
                    </div>
                  </div>

                  {/* Modal Footer */}
                  <div className="px-6 py-4 bg-gray-50 flex gap-3">
                    <button
                      onClick={() => setShowConfirmModal(false)}
                      disabled={submitting}
                      className="flex-1 px-4 py-2.5 border border-gray-300 text-gray-700 rounded-xl font-medium hover:bg-gray-100 transition-colors disabled:opacity-50"
                    >
                      Hủy
                    </button>
                    <button
                      onClick={handleSubmitBooking}
                      disabled={submitting}
                      className={`flex-1 px-4 py-2.5 bg-gradient-to-r ${colors.gradient} text-white rounded-xl font-medium hover:opacity-90 transition-opacity disabled:opacity-50 flex items-center justify-center gap-2`}
                    >
                      {submitting ? (
                        <>
                          <Loader2 className="w-4 h-4 animate-spin" />
                          Đang gửi...
                        </>
                      ) : (
                        <>
                          <Check className="w-4 h-4" />
                          Xác nhận
                        </>
                      )}
                    </button>
                  </div>
                </>
              ) : (
                // Success State
                <div className="p-8 text-center">
                  <div className="w-20 h-20 rounded-full bg-green-100 flex items-center justify-center mx-auto mb-4">
                    <CheckCircle2 className="w-10 h-10 text-green-600" />
                  </div>
                  <h3 className="text-xl font-bold text-gray-900 mb-2">
                    Đặt phòng thành công!
                  </h3>
                  <p className="text-gray-600 mb-2">
                    Yêu cầu của bạn đã được gửi và đang chờ xét duyệt.
                  </p>
                  <p className="text-sm text-gray-500 mb-6">
                    Mã đặt phòng: <span className="font-mono font-semibold text-gray-900">{bookingId}</span>
                  </p>
                  <div className="flex flex-col sm:flex-row gap-3 justify-center">
                    <button
                      onClick={() => navigate('/my-bookings')}
                      className={`px-6 py-2.5 bg-gradient-to-r ${colors.gradient} text-white rounded-xl font-medium hover:opacity-90 transition-opacity`}
                    >
                      Xem lịch sử đặt
                    </button>
                    <button
                      onClick={handleCloseSuccessAndRedirect}
                      className="px-6 py-2.5 bg-gray-100 text-gray-700 rounded-xl font-medium hover:bg-gray-200 transition-colors"
                    >
                      Đặt phòng khác
                    </button>
                  </div>
                </div>
              )}
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default BookingPage;


