import { API_BASE_URL, API_ENDPOINTS, apiFetch, buildUrl } from '../../../../services/api.config';
import type { Facility, Campus, FacilityType } from '../../../../types';

export type BookingStatus = 'Pending' | 'Approved' | 'Rejected' | 'Finish' | 'Cancelled';

export interface Feedback {
  id: string;
  rating: number;
  comment: string;
  createdAt: string;
}

export interface UserBooking {
  id: string;
  lessonBookingId: string;
  lessonBookingDate: string;
  facilityId: string;
  facility: Facility;
  date: string;
  startTime: string;
  endTime: string;
  startDateTime?: string;
  checkInTime?: string;
  checkOutTime?: string;
  purpose: string;
  numberOfPeople: number;
  status: BookingStatus;
  createdAt: string;
  feedback?: Feedback;
  rejectionReason?: string;
}

// Backend response types
interface BackendBookingResponse {
  bookingId: string;
  facilityId: string;
  facilityName: string;
  facilityDescription?: string;
  facilityCapacity?: number;
  facilityRoomNumber?: string;
  facilityFloorNumber?: string;
  campusName?: string;
  typeName?: string;
  userId: string;
  userName?: string;
  startTime: string;
  endTime: string;
  checkInTime?: string;
  checkOutTime?: string;
  purpose: string;
  status: string;
  estimatedAttendees: number;
  specialRequirements?: string;
  createdAt: string;
  rejectionReason?: string;
}

interface BackendFeedbackResponse {
  feedbackId: string;
  bookingId: string;
  userId: string;
  rating: number;
  comments: string;
  createdAt: string;
}

export interface FeedbackRequest {
  bookingId: string;
  rating: number;
  comment: string;
}

// Map backend status to frontend status (5 statuses only)
const mapStatus = (status: string): BookingStatus => {
  const statusMap: Record<string, BookingStatus> = {
    'Draft': 'Pending',
    'Pending': 'Pending',
    'Pending_Approval': 'Pending',
    'Approved': 'Approved',
    'Completed': 'Finish',
    'Cancelled': 'Cancelled',
    'Rejected': 'Rejected',
  };
  return statusMap[status] || 'Pending';
};

// Map facility type
const mapFacilityType = (typeName: string): FacilityType => {
  const typeMap: Record<string, FacilityType> = {
    'Classroom': 'Classroom',
    'Meeting Room': 'Meeting Room',
    'Computer Lab': 'Laboratory',
    'Sports Court': 'Sport Facility',
    'Laboratory': 'Laboratory',
    'Sport Facility': 'Sport Facility',
  };
  return typeMap[typeName] || 'Classroom';
};

// Map backend booking to frontend UserBooking
const mapBookingResponse = (b: BackendBookingResponse, feedback?: BackendFeedbackResponse): UserBooking => {
  const parseBackendDateTime = (value: string): Date | null => {
    if (!value) return null;

    try {
      // Format 1: dd/MM/yyyy HH:mm:ss or dd/MM/yyyy HH:mm
      const ddMMyyyyTime = value.match(/^\s*(\d{2})\/(\d{2})\/(\d{4})\s+(\d{2}):(\d{2})(?::(\d{2}))?\s*$/);
      if (ddMMyyyyTime) {
        const [, day, month, year, hours, minutes, seconds] = ddMMyyyyTime;
        const date = new Date(
          Number(year),
          Number(month) - 1,
          Number(day),
          Number(hours),
          Number(minutes),
          Number(seconds ?? 0)
        );
        if (!Number.isNaN(date.getTime())) {
          return date;
        }
      }

      // Format 2: dd/MM/yyyy (date only)
      const ddMMyyyy = value.match(/^\s*(\d{2})\/(\d{2})\/(\d{4})\s*$/);
      if (ddMMyyyy) {
        const [, day, month, year] = ddMMyyyy;
        const date = new Date(Number(year), Number(month) - 1, Number(day));
        if (!Number.isNaN(date.getTime())) {
          return date;
        }
      }

      // Format 3: yyyy-MM-ddTHH:mm:ss (ISO format)
      const isoMatch = value.match(/^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})/);
      if (isoMatch) {
        const [, year, month, day, hours, minutes, seconds] = isoMatch;
        const date = new Date(
          Number(year),
          Number(month) - 1,
          Number(day),
          Number(hours),
          Number(minutes),
          Number(seconds)
        );
        if (!Number.isNaN(date.getTime())) {
          return date;
        }
      }

      // Format 4: yyyy-MM-dd HH:mm:ss
      const yyyyMMddTime = value.match(/^(\d{4})-(\d{2})-(\d{2})\s+(\d{2}):(\d{2}):(\d{2})/);
      if (yyyyMMddTime) {
        const [, year, month, day, hours, minutes, seconds] = yyyyMMddTime;
        const date = new Date(
          Number(year),
          Number(month) - 1,
          Number(day),
          Number(hours),
          Number(minutes),
          Number(seconds)
        );
        if (!Number.isNaN(date.getTime())) {
          return date;
        }
      }

      // Format 5: Fallback - try standard Date parsing
      const date = new Date(value);
      if (!Number.isNaN(date.getTime())) {
        return date;
      }

      return null;
    } catch {
      return null;
    }
  };

  const startDate = parseBackendDateTime(b.startTime);
  const endDate = parseBackendDateTime(b.endTime);

  const safeTime = (d: Date | null, fallback: string) => {
    if (!d) {
      // Try to extract time from fallback string if it's in format "dd/MM/yyyy HH:mm:ss"
      const timeMatch = fallback.match(/(\d{2}):(\d{2})(?::\d{2})?/);
      if (timeMatch) {
        return `${timeMatch[1]}:${timeMatch[2]}`;
      }
      return fallback;
    }
    try {
      // Check if date is valid
      if (isNaN(d.getTime())) {
        // Try to extract time from fallback string
        const timeMatch = fallback.match(/(\d{2}):(\d{2})(?::\d{2})?/);
        if (timeMatch) {
          return `${timeMatch[1]}:${timeMatch[2]}`;
        }
        return fallback;
      }
      return d.toLocaleTimeString('vi-VN', { hour: '2-digit', minute: '2-digit', hour12: false });
    } catch {
      // Try to extract time from fallback string
      const timeMatch = fallback.match(/(\d{2}):(\d{2})(?::\d{2})?/);
      if (timeMatch) {
        return `${timeMatch[1]}:${timeMatch[2]}`;
      }
      return fallback;
    }
  };

  const safeIso = (d: Date | null, fallback: string) => {
    if (!d) return fallback;
    try {
      return d.toISOString();
    } catch {
      return fallback;
    }
  };

  const derivedDate = startDate ? safeIso(startDate, b.startTime).split('T')[0] : (b.startTime.includes('T') ? b.startTime.split('T')[0] : b.startTime);
  
  return {
    id: b.bookingId,
    lessonBookingId: b.bookingId,
    lessonBookingDate: derivedDate,
    facilityId: b.facilityId,
    facility: {
      id: b.facilityId,
      name: b.facilityName || 'Unknown Facility',
      campus: (b.campusName?.includes('NVH') || b.campusName?.includes('Nhà Văn Hóa') || b.campusName?.includes('Sinh Viên') ? 'NVH' : 'HCM') as Campus,
      type: mapFacilityType(b.typeName || 'Classroom'),
      capacity: b.facilityCapacity || 0,
      location: `${b.facilityRoomNumber || ''}, Tầng ${b.facilityFloorNumber || ''}`,
      amenities: [],
      imageUrl: '/images/default-facility.jpg',
      isActive: true,
      description: b.facilityDescription || '',
    },
    date: derivedDate,
    startTime: safeTime(startDate, b.startTime),
    endTime: safeTime(endDate, b.endTime),
    startDateTime: safeIso(startDate, b.startTime),
    checkInTime: b.checkInTime,
    checkOutTime: b.checkOutTime,
    purpose: b.purpose,
    numberOfPeople: b.estimatedAttendees,
    status: mapStatus(b.status),
    createdAt: b.createdAt,
    rejectionReason: b.rejectionReason,
    feedback: feedback ? {
      id: feedback.feedbackId,
      rating: feedback.rating,
      comment: feedback.comments,
      createdAt: feedback.createdAt,
    } : undefined,
  };
};

// Helper to check if backend status matches frontend filter (5 statuses)
const statusMatchesFilter = (backendStatus: string, filterStatus: BookingStatus): boolean => {
  switch (filterStatus) {
    case 'Pending':
      return ['Draft', 'Pending', 'Pending_Approval'].includes(backendStatus);
    case 'Approved':
      return backendStatus === 'Approved';
    case 'Finish':
      return backendStatus === 'Completed';
    case 'Cancelled':
      return backendStatus === 'Cancelled';
    case 'Rejected':
      return backendStatus === 'Rejected';
    default:
      return true;
  }
};

// System Settings types
export interface SystemSettings {
  checkInMinutesBeforeStart: number;
  checkInMinutesAfterStart: number;
  checkOutMinutesAfterCheckIn: number;
}

export const myBookingsApi = {
  // Get system settings
  getSystemSettings: async (): Promise<SystemSettings | null> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.SYSTEM_SETTINGS.GET}`;
      const response = await apiFetch<SystemSettings & { minimumBookingHoursBeforeStart?: number; checkoutMinRatio?: number }>(url);
      
      if (response.success && response.data) {
        return {
          checkInMinutesBeforeStart: response.data.checkInMinutesBeforeStart || 15,
          checkInMinutesAfterStart: response.data.checkInMinutesAfterStart || 15,
          checkOutMinutesAfterCheckIn: response.data.checkOutMinutesAfterCheckIn || 0,
        };
      }
      
      // Return default values if API fails
      return {
        checkInMinutesBeforeStart: 15,
        checkInMinutesAfterStart: 15,
        checkOutMinutesAfterCheckIn: 0,
      };
    } catch (error) {
      console.error('Error fetching system settings:', error);
      // Return default values if API fails
      return {
        checkInMinutesBeforeStart: 15,
        checkInMinutesAfterStart: 15,
        checkOutMinutesAfterCheckIn: 0,
      };
    }
  },

  // Get all bookings for current user - API ONLY
  getMyBookings: async (status?: BookingStatus): Promise<UserBooking[]> => {
    try {
      // Always fetch all bookings, filter on client side
      const params: Record<string, string | number | undefined> = {
        page: 1,
        limit: 100,
      };
      
      const url = buildUrl(API_ENDPOINTS.BOOKING.GET_MY_BOOKINGS, params);
      console.log('Fetching my bookings from:', url);
      
      const response = await apiFetch<BackendBookingResponse[]>(url);
      console.log('My bookings full response:', response);
      
      if (!response.success) {
        console.error('API Error:', response.error);
        return [];
      }
      
      // Ensure data is an array - it should be BackendBookingResponse[]
      // Sometimes the API might wrap it differently
      let bookingData: BackendBookingResponse[] = [];
      
      if (Array.isArray(response.data)) {
        bookingData = response.data;
      } else if (response.data && typeof response.data === 'object') {
        // If data is an object (not array), check if it has a data property
        const dataObj = response.data as { data?: BackendBookingResponse[] };
        if (dataObj.data && Array.isArray(dataObj.data)) {
          bookingData = dataObj.data;
        } else {
          console.warn('Unexpected data structure:', response.data);
        }
      }
      
      console.log('Extracted bookings data:', bookingData, 'Length:', bookingData.length);
      
      if (bookingData.length === 0) {
        console.log('No bookings found for current user');
        return [];
      }
      
      // Get all feedbacks for these bookings
      const feedbackUrl = buildUrl(API_ENDPOINTS.FEEDBACK.GET_ALL, { limit: 100 });
      const feedbackResponse = await apiFetch<BackendFeedbackResponse[]>(feedbackUrl);
      const feedbacks = feedbackResponse.success && feedbackResponse.data 
        ? Array.isArray(feedbackResponse.data) ? feedbackResponse.data : [] 
        : [];
      
      console.log('Fetched feedbacks:', feedbacks.length);
      
      // Filter by status first (using original backend status), then map
      let filteredData = bookingData;
      if (status) {
        filteredData = bookingData.filter(b => statusMatchesFilter(b.status, status));
        console.log('Filtered bookings by status:', status, 'Count:', filteredData.length);
      }
      
      // Map bookings with their feedbacks
      const bookings = filteredData.map(booking => {
        const feedback = feedbacks.find(f => f.bookingId === booking.bookingId);
        return mapBookingResponse(booking, feedback);
      });
      
      // Sort by date descending
      bookings.sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
      
      console.log('Final mapped bookings:', bookings.length);
      return bookings;
    } catch (error) {
      console.error('Error fetching bookings:', error);
      return [];
    }
  },

  // Get booking by ID - API ONLY
  getBookingById: async (id: string): Promise<UserBooking | null> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.BOOKING.GET_BY_ID(id)}`;
      const response = await apiFetch<BackendBookingResponse>(url);
      
      if (response.success && response.data) {
        // Get feedback for this booking
        const feedbackUrl = buildUrl(API_ENDPOINTS.FEEDBACK.GET_ALL, { bookingId: id });
        const feedbackResponse = await apiFetch<BackendFeedbackResponse[]>(feedbackUrl);
        const feedback = feedbackResponse.success && feedbackResponse.data?.length 
          ? feedbackResponse.data[0] 
          : undefined;
        
        return mapBookingResponse(response.data, feedback);
      }
      
      return null;
    } catch (error) {
      console.error('Error fetching booking:', error);
      return null;
    }
  },

  // Submit feedback for a completed booking - API ONLY
  submitFeedback: async (request: FeedbackRequest): Promise<{ success: boolean; message: string }> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.FEEDBACK.CREATE}`;
      console.log('Submitting feedback:', request);
      
      const response = await apiFetch<BackendFeedbackResponse>(url, {
        method: 'POST',
        body: JSON.stringify({
          bookingId: request.bookingId,
          rating: request.rating,
          comments: request.comment, // Backend uses 'comments' field
        }),
      });
      
      console.log('Feedback response:', response);
      
      if (response.success) {
        return { success: true, message: 'Đánh giá thành công! Cảm ơn bạn đã góp ý.' };
      }
      
      return { 
        success: false, 
        message: response.error?.message || 'Không thể gửi đánh giá. Vui lòng thử lại.' 
      };
    } catch (error) {
      console.error('Error submitting feedback:', error);
      return { 
        success: false, 
        message: 'Không thể kết nối đến server.' 
      };
    }
  },

  // Cancel a pending/approved booking - API ONLY
  cancelBooking: async (id: string): Promise<{ success: boolean; message: string }> => {
    try {
      // Backend Controller expects reason parameter in query string
      // Format: DELETE /api/bookings/{id}?reason={reason}
      const reason = 'Hủy bởi người dùng';
      const url = `${API_BASE_URL}${API_ENDPOINTS.BOOKING.CANCEL(id)}?reason=${encodeURIComponent(reason)}`;
      console.log('Cancelling booking:', id);
      
      const response = await apiFetch(url, {
        method: 'DELETE',
      });
      
      if (response.success) {
        return { success: true, message: 'Đã hủy đặt phòng thành công' };
      }
      
      // Backend Controller returns NotFound for all errors, but error message contains details
      let errorMessage = 'Không thể hủy đặt phòng.';
      if (response.error) {
        // Use error message from backend if available
        errorMessage = response.error.message || errorMessage;
      }
      
      return { 
        success: false, 
        message: errorMessage
      };
    } catch (error) {
      console.error('Error cancelling booking:', error);
      return { 
        success: false, 
        message: 'Không thể kết nối đến server.' 
      };
    }
  },

  // Get booking statistics - API ONLY
  getBookingStats: async (): Promise<{
    total: number;
    pending: number;
    approved: number;
    finish: number;
    rejected: number;
    cancelled: number;
    feedbackGiven: number;
  }> => {
    try {
      // Fetch all bookings to calculate stats
      const url = buildUrl(API_ENDPOINTS.BOOKING.GET_MY_BOOKINGS, { limit: 1000 });
      const response = await apiFetch<BackendBookingResponse[]>(url);
      
      if (response.success && response.data) {
        const bookings = response.data;
        
        // Get feedbacks
        const feedbackUrl = buildUrl(API_ENDPOINTS.FEEDBACK.GET_ALL, { limit: 1000 });
        const feedbackResponse = await apiFetch<BackendFeedbackResponse[]>(feedbackUrl);
        const feedbacks = feedbackResponse.success && feedbackResponse.data ? feedbackResponse.data : [];
        
        // Map backend statuses to frontend statuses for stats
        const mappedBookings = bookings.map(b => mapStatus(b.status));
        
        return {
          total: bookings.length,
          pending: mappedBookings.filter(s => s === 'Pending').length,
          approved: mappedBookings.filter(s => s === 'Approved').length,
          finish: mappedBookings.filter(s => s === 'Finish').length,
          rejected: mappedBookings.filter(s => s === 'Rejected').length,
          cancelled: mappedBookings.filter(s => s === 'Cancelled').length,
          feedbackGiven: feedbacks.length,
        };
      }
      
      return {
        total: 0,
        pending: 0,
        approved: 0,
        finish: 0,
        rejected: 0,
        cancelled: 0,
        feedbackGiven: 0,
      };
    } catch (error) {
      console.error('Error fetching booking stats:', error);
      return {
        total: 0,
        pending: 0,
        approved: 0,
        finish: 0,
        rejected: 0,
        cancelled: 0,
        feedbackGiven: 0,
      };
    }
  },

  // Check-in for a booking
  checkIn: async (
    bookingId: string,
    note?: string,
    images?: File[]
  ): Promise<{ success: boolean; message: string }> => {
    try {
      const mockTime = localStorage.getItem('mockTime');
      const hasImages = !!(images && images.length > 0);
      const url = hasImages
        ? `${API_BASE_URL}${API_ENDPOINTS.BOOKING.CHECK_IN_WITH_IMAGES(bookingId)}`
        : `${API_BASE_URL}${API_ENDPOINTS.BOOKING.CHECK_IN(bookingId)}`;
      
      const body = (() => {
        if (!hasImages) {
          return note ? JSON.stringify({ note }) : undefined;
        }

        const formData = new FormData();
        if (note) formData.append('note', note);
        images!.forEach((image) => formData.append('images', image));
        return formData;
      })();

      const response = await apiFetch(url, {
        method: 'POST',
        body,
        headers: mockTime ? { 'X-Mock-Time': mockTime } : undefined,
      });
      
      if (response.success) {
        return { success: true, message: 'Check-in thành công!' };
      }
      
      return {
        success: false,
        message: response.error?.message || 'Không thể thực hiện check-in. Vui lòng thử lại.'
      };
    } catch (error) {
      console.error('Error checking in:', error);
      return {
        success: false,
        message: 'Không thể kết nối đến server.'
      };
    }
  },

  // Check-out for a booking
  checkOut: async (
    bookingId: string,
    note?: string,
    images?: File[]
  ): Promise<{ success: boolean; message: string }> => {
    try {
      const mockTime = localStorage.getItem('mockTime');
      const hasImages = !!(images && images.length > 0);
      const url = hasImages
        ? `${API_BASE_URL}${API_ENDPOINTS.BOOKING.CHECK_OUT_WITH_IMAGES(bookingId)}`
        : `${API_BASE_URL}${API_ENDPOINTS.BOOKING.CHECK_OUT(bookingId)}`;
      
      const body = (() => {
        if (!hasImages) {
          return note ? JSON.stringify({ note }) : undefined;
        }

        const formData = new FormData();
        if (note) formData.append('note', note);
        images!.forEach((image) => formData.append('images', image));
        return formData;
      })();

      const response = await apiFetch(url, {
        method: 'POST',
        body,
        headers: mockTime ? { 'X-Mock-Time': mockTime } : undefined,
      });
      
      if (response.success) {
        return { success: true, message: 'Check-out thành công!' };
      }
      
      return {
        success: false,
        message: response.error?.message || 'Không thể thực hiện check-out. Vui lòng thử lại.'
      };
    } catch (error) {
      console.error('Error checking out:', error);
      return {
        success: false,
        message: 'Không thể kết nối đến server.'
      };
    }
  },
};
