import { API_BASE_URL, API_ENDPOINTS, apiFetch, buildUrl } from '../../../../services/api.config';
import type { Facility, Campus, FacilityType } from '../../../../types';

export interface TimeSlot {
  id: string;
  startTime: string;
  endTime: string;
  isAvailable: boolean;
}

export interface BookingRequest {
  facilityId: string;
  userId: string;
  date: string;
  timeSlotId: string;
  startTime: string;
  endTime: string;
  purpose: string;
  numberOfPeople: number;
  notes?: string;
}

export interface BookingResponse {
  id: string;
  status: 'pending' | 'confirmed' | 'rejected';
  message: string;
}

// System Settings types
export interface SystemSettings {
  minimumBookingHoursBeforeStart: number;
  checkInMinutesBeforeStart: number;
  checkInMinutesAfterStart: number;
  checkoutMinRatio: number;
  checkOutMinutesAfterCheckIn: number;
}

// Backend response types
interface FacilityResponse {
  facilityId: string;
  name: string;
  description: string;
  capacity: number;
  roomNumber: string;
  floorNumber: string;
  campusId: string;
  campusName: string;
  typeId: string;
  typeName: string;
  status: string;
  amenities: string;
  imageUrl?: string;
}

interface BackendBookingResponse {
  bookingId: string;
  facilityId: string;
  userId: string;
  startTime: string;
  endTime: string;
  purpose: string;
  status: string;
  estimatedAttendees: number;
  specialRequirements: string;
}

const parseBackendDateTime = (value: string): Date | null => {
  if (!value) return null;

  // dd/MM/yyyy HH:mm:ss or dd/MM/yyyy HH:mm
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
    return Number.isNaN(date.getTime()) ? null : date;
  }

  // dd/MM/yyyy (date only)
  const ddMMyyyy = value.match(/^\s*(\d{2})\/(\d{2})\/(\d{4})\s*$/);
  if (ddMMyyyy) {
    const [, day, month, year] = ddMMyyyy;
    const date = new Date(Number(year), Number(month) - 1, Number(day));
    return Number.isNaN(date.getTime()) ? null : date;
  }

  // ISO (or any other format Date can parse)
  const date = new Date(value);
  return Number.isNaN(date.getTime()) ? null : date;
};

// Map backend facility response to frontend type
const mapFacilityResponse = (f: FacilityResponse): Facility => ({
  id: f.facilityId,
  name: f.name,
  campus: (f.campusName?.includes('NVH') || f.campusName?.includes('Nhà Văn Hóa') || f.campusName?.includes('Sinh Viên') ? 'NVH' : 'HCM') as Campus,
  type: mapFacilityType(f.typeName),
  capacity: f.capacity,
  location: `${f.roomNumber}, Tầng ${f.floorNumber}`,
  amenities: f.amenities ? f.amenities.split(',').map(a => a.trim()) : [],
  imageUrl: f.imageUrl || '/images/default-facility.jpg',
  isActive: f.status === 'Available',
  description: f.description,
});

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

/**
 * Tạo danh sách time slots cho một ngày cụ thể
 * 
 * @description Function này tạo 14 slots (7:00-21:00), mỗi slot 1 tiếng.
 *              Mỗi slot sẽ được đánh dấu available/unavailable dựa trên:
 *              1. Ngày đã qua (isPastDate)
 *              2. Slot đã qua trong ngày hôm nay (isPastSlot)
 *              3. Trong khoảng thời gian tối thiểu (isWithinMinimumHours)
 *              4. Đã có người đặt (isBooked)
 * 
 * @param date - Ngày cần tạo slots (format: "YYYY-MM-DD")
 * @param bookedSlots - Danh sách các giờ đã được đặt (ví dụ: ["09:00", "10:00"])
 * @param minimumBookingHours - Số giờ tối thiểu phải đặt trước (từ system settings)
 * @returns Array of TimeSlot objects
 * 
 * @example
 * // Ngày 2025-12-16, hiện tại 7:36, minimumBookingHours = 3
 * generateTimeSlots("2025-12-16", ["14:00"], 3)
 * // Kết quả:
 * // - 07:00: unavailable (đã qua)
 * // - 08:00: unavailable (8:00 - 7:36 = 0.4h < 3h)
 * // - 09:00: unavailable (9:00 - 7:36 = 1.4h < 3h)
 * // - 10:00: unavailable (10:00 - 7:36 = 2.4h < 3h)
 * // - 11:00: AVAILABLE (11:00 - 7:36 = 3.4h >= 3h)
 * // - 14:00: unavailable (đã được đặt)
 */
const generateTimeSlots = (date: string, bookedSlots: string[] = [], minimumBookingHours: number): TimeSlot[] => {
  const slots: TimeSlot[] = [];
  
  // ============================================
  // BƯỚC 1: PARSE VÀ CHUẨN HÓA NGÀY
  // ============================================
  // Parse ngày được chọn (format: YYYY-MM-DD)
  const baseDate = new Date(date + 'T00:00:00');
  
  // Lấy ngày hôm nay (reset về 00:00:00 để so sánh chỉ phần ngày)
  const today = new Date();
  today.setHours(0, 0, 0, 0);
  
  // Thời gian hiện tại (để tính toán slots đã qua)
  const now = new Date();
  
  // Chuẩn hóa ngày để so sánh (bỏ qua phần giờ)
  const baseDateOnly = new Date(baseDate.getFullYear(), baseDate.getMonth(), baseDate.getDate());
  const todayOnly = new Date(today.getFullYear(), today.getMonth(), today.getDate());
  
  // ============================================
  // BƯỚC 2: XÁC ĐỊNH NGÀY QUÁ KHỨ / HÔM NAY
  // ============================================
  const isPastDate = baseDateOnly.getTime() < todayOnly.getTime();  // Ngày đã qua?
  const isToday = baseDateOnly.getTime() === todayOnly.getTime();   // Là hôm nay?
  
  // ============================================
  // BƯỚC 3: TẠO 14 SLOTS (7:00 - 21:00)
  // Operating hours: 7:00 - 21:00
  // ============================================
  for (let hour = 7; hour < 21; hour++) {
    // Tạo thời gian bắt đầu của slot
    const slotStartTime = new Date(baseDate);
    slotStartTime.setHours(hour, 0, 0, 0);
    
    // Tính số giờ từ hiện tại đến slot
    // Ví dụ: hiện tại 7:36, slot 10:00 → hoursUntilSlot = 2.4 giờ
    const hoursUntilSlot = (slotStartTime.getTime() - now.getTime()) / (1000 * 60 * 60);
    
    // ============================================
    // KIỂM TRA 1: SLOT ĐÃ QUA CHƯA?
    // ============================================
    let isPastSlot = false;
    if (isPastDate) {
      // Toàn bộ ngày đã qua → tất cả slots unavailable
      isPastSlot = true;
    } else if (isToday) {
      // Cùng ngày → chỉ disable slots đã qua
      // Ví dụ: hiện tại 7:36, slot 7:00 đã qua (7:00 < 7:36)
      if (slotStartTime.getTime() < now.getTime()) {
        isPastSlot = true;
      }
    }
    
    // ============================================
    // KIỂM TRA 2: TRONG KHOẢNG THỜI GIAN TỐI THIỂU?
    // ============================================
    // Phải đặt trước ít nhất X giờ (lấy từ system settings)
    // Chỉ áp dụng cho slots trong tương lai (hoursUntilSlot > 0)
    // Ví dụ: minimumBookingHours = 3, hiện tại 7:36
    //   - Slot 10:00: 2.4h < 3h → unavailable
    //   - Slot 11:00: 3.4h >= 3h → available
    const isWithinMinimumHours = hoursUntilSlot > 0 && hoursUntilSlot < minimumBookingHours;
    
    // ============================================
    // KIỂM TRA 3: ĐÃ CÓ NGƯỜI ĐẶT?
    // ============================================
    const isBooked = bookedSlots.includes(`${hour.toString().padStart(2, '0')}:00`);
    
    // ============================================
    // TẠO SLOT OBJECT
    // ============================================
    slots.push({
      id: `slot-${hour}`,
      startTime: `${hour.toString().padStart(2, '0')}:00`,  // "07:00", "08:00", ...
      endTime: `${(hour + 1).toString().padStart(2, '0')}:00`,  // "08:00", "09:00", ...
      // Slot available khi: không phải quá khứ, không trong minimum hours, chưa được đặt
      isAvailable: !isPastSlot && !isWithinMinimumHours && !isBooked,
    });
  }
  
  return slots;
};

/**
 * Booking API - Tập hợp các API calls cho chức năng đặt phòng
 */
export const bookingApi = {
  /**
   * Lấy cấu hình hệ thống từ backend
   * 
   * @description Endpoint public, không cần auth.
   *              Trả về các settings như:
   *              - minimumBookingHoursBeforeStart: Số giờ tối thiểu phải đặt trước
   *              - checkInMinutesBeforeStart: Số phút được check-in trước giờ đặt
   *              - checkInMinutesAfterStart: Số phút được check-in sau giờ đặt
   * 
   * @returns SystemSettings object hoặc null nếu lỗi
   */
  getSystemSettings: async (): Promise<SystemSettings | null> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.SYSTEM_SETTINGS.GET}`;
      console.log('Fetching system settings:', url);
      
      const response = await apiFetch<SystemSettings>(url);
      
      if (response.success && response.data) {
        return response.data;
      }
      
      console.error('Failed to fetch system settings:', response.error);
      // Trả về null nếu API fail - caller sẽ xử lý (dùng default values)
      return null;
    } catch (error) {
      console.error('Error fetching system settings:', error);
      return null;
    }
  },

  /**
   * Lấy thông tin chi tiết facility theo ID
   * 
   * @param id - Facility ID (ví dụ: "F00001")
   * @returns Facility object hoặc null nếu không tìm thấy
   */
  getFacilityById: async (id: string): Promise<Facility | null> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.FACILITY.GET_BY_ID(id)}`;
      console.log('Fetching facility:', url);
      
      const response = await apiFetch<FacilityResponse>(url);
      
      if (response.success && response.data) {
        // Map response từ backend sang frontend Facility type
        return mapFacilityResponse(response.data);
      }
      
      console.error('Facility not found:', response.error);
      return null;
    } catch (error) {
      console.error('Error fetching facility:', error);
      return null;
    }
  },

  /**
   * Lấy danh sách time slots khả dụng cho facility vào ngày cụ thể
   * 
   * @description Flow:
   *   1. Gọi API lấy tất cả bookings của facility
   *   2. Filter bookings: chỉ lấy active (không Cancelled/Rejected)
   *   3. Filter bookings: chỉ lấy cùng ngày
   *   4. Tính toán các giờ đã bị đặt
   *   5. Gọi generateTimeSlots với danh sách booked
   * 
   * @param facilityId - ID của facility
   * @param date - Ngày cần kiểm tra (format: "YYYY-MM-DD")
   * @param minimumBookingHours - Số giờ tối thiểu phải đặt trước (từ system settings)
   * @returns Array of TimeSlot objects
   */
  getAvailableTimeSlots: async (facilityId: string, date: string, minimumBookingHours: number): Promise<TimeSlot[]> => {
    try {
      // ============================================
      // BƯỚC 1: LẤY TẤT CẢ BOOKINGS CỦA FACILITY
      // ============================================
      const url = buildUrl(API_ENDPOINTS.BOOKING.GET_ALL, {
        facilityId,
        page: 1,
        limit: 100,  // Lấy tối đa 100 bookings
      });
      
      const response = await apiFetch<BackendBookingResponse[]>(url);
      
      // ============================================
      // BƯỚC 2: TÍNH TOÁN CÁC GIỜ ĐÃ ĐƯỢC ĐẶT
      // ============================================
      const bookedSlots: string[] = [];
      if (response.success && response.data) {
        response.data.forEach(booking => {
          // Chỉ xét bookings active (không Cancelled/Rejected)
          if (booking.status !== 'Cancelled' && booking.status !== 'Rejected') {
            const bookingStart = parseBackendDateTime(booking.startTime);
            const bookingEnd = parseBackendDateTime(booking.endTime);

            // If backend returns an unexpected datetime format, skip this booking
            // instead of crashing the whole Booking page.
            if (!bookingStart || !bookingEnd) return;

            const bookingDate = bookingStart.toISOString().split('T')[0];
            
            // Chỉ xét bookings cùng ngày
            if (bookingDate === date) {
              const startHour = bookingStart.getHours();
              const endHour = bookingEnd.getHours();
              const endMinute = bookingEnd.getMinutes();
              
              // Đánh dấu tất cả giờ bị overlap
              // Ví dụ: booking 8:00-10:00 → disable slots 8:00 và 9:00
              for (let hour = startHour; hour < endHour; hour++) {
                const slotTime = `${hour.toString().padStart(2, '0')}:00`;
                if (!bookedSlots.includes(slotTime)) {
                  bookedSlots.push(slotTime);
                }
              }
              
              // Nếu booking kết thúc giữa giờ, cũng disable slot đó
              // Ví dụ: booking kết thúc 10:30 → disable slot 10:00
              if (endMinute > 0 && endHour < 21) {
                const slotTime = `${endHour.toString().padStart(2, '0')}:00`;
                if (!bookedSlots.includes(slotTime)) {
                  bookedSlots.push(slotTime);
                }
              }
            }
          }
        });
      }
      
      // ============================================
      // BƯỚC 3: TẠO SLOTS VỚI THÔNG TIN AVAILABILITY
      // ============================================
      return generateTimeSlots(date, bookedSlots, minimumBookingHours);
    } catch (error) {
      console.error('Error fetching time slots:', error);
      // Throw error để caller xử lý (hiển thị thông báo lỗi)
      throw error;
    }
  },

  /**
   * Gửi yêu cầu đặt phòng mới
   * 
   * @description Tạo booking với trạng thái "Pending_Approval".
   *              Admin sẽ duyệt/từ chối sau.
   * 
   * @param booking - Thông tin booking từ form
   * @returns BookingResponse với id, status, message
   * 
   * @example
   * submitBooking({
   *   facilityId: "F00001",
   *   userId: "U00001",
   *   date: "2025-12-16",
   *   startTime: "09:00",
   *   endTime: "10:00",
   *   purpose: "Họp nhóm",
   *   numberOfPeople: 5,
   *   notes: "Cần máy chiếu"
   * })
   */
  submitBooking: async (booking: BookingRequest): Promise<BookingResponse> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.BOOKING.CREATE}`;
      
      // ============================================
      // FORMAT DATETIME CHO BACKEND
      // ============================================
      // Combine date + time thành ISO 8601 format
      // "2025-12-16" + "09:00" → "2025-12-16T09:00:00"
      const startDateTime = `${booking.date}T${booking.startTime}:00`;
      const endDateTime = `${booking.date}T${booking.endTime}:00`;
      
      console.log('Submitting booking:', { url, booking });
      
      // ============================================
      // GỬI REQUEST TẠO BOOKING
      // ============================================
      const response = await apiFetch<BackendBookingResponse>(url, {
        method: 'POST',
        body: JSON.stringify({
          facilityId: booking.facilityId,
          userId: booking.userId,
          startTime: startDateTime,
          endTime: endDateTime,
          purpose: booking.purpose,
          estimatedAttendees: booking.numberOfPeople,
          specialRequirements: booking.notes || '',
        }),
      });
      
      console.log('Booking response:', response);
      
      // ============================================
      // XỬ LÝ RESPONSE
      // ============================================
      if (response.success && response.data) {
        return {
          id: response.data.bookingId,
          status: 'pending',
          message: 'Yêu cầu đặt phòng đã được gửi thành công! Vui lòng chờ phê duyệt.',
        };
      }
      
      // Trả về lỗi từ backend
      return {
        id: '',
        status: 'rejected',
        message: response.error?.message || 'Đã xảy ra lỗi khi đặt phòng. Vui lòng thử lại.',
      };
    } catch (error) {
      console.error('Error submitting booking:', error);
      return {
        id: '',
        status: 'rejected',
        message: 'Không thể kết nối đến server. Vui lòng kiểm tra Backend đang chạy.',
      };
    }
  },
};
