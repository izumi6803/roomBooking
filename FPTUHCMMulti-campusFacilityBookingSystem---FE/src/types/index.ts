export type Campus = 'HCM' | 'NVH';

export type FacilityType = 
  | 'Classroom' 
  | 'Meeting Room' 
  | 'Laboratory' 
  | 'Sport Facility'
  | 'meeting-room'  // Legacy
  | 'lab-room'      // Legacy
  | 'sports-field'; // Legacy

export type BookingStatus = 'pending' | 'approved' | 'rejected' | 'completed' | 'cancelled';

// User type for admin/booking context
export interface User {
  id: string;
  name: string;
  email: string;
  role: 'student' | 'teacher' | 'admin';
  studentId?: string;
  employeeId?: string;
}

// User type for auth context (based on database schema)
export interface AuthUser {
  user_id: string;
  email: string;
  full_name: string;
  phone_number?: string;
  user_name: string;
  role: 'Student' | 'Lecturer' | 'Admin' | 'Facility_Admin' | 'Facility_Manager';
  campus_id: number;
  status: 'Active' | 'Inactive';
  avatar_url?: string;
  last_login?: string;
  created_at: string;
}

export interface Facility {
  id: string;
  name: string;
  type: FacilityType;
  campus: Campus;
  capacity: number;
  location: string;
  description?: string;
  amenities?: string[];
  isActive: boolean;
  imageUrl?: string;
}

export interface Booking {
  id: string;
  facilityId: string;
  facility: Facility;
  userId: string;
  user: User;
  startTime: Date;
  endTime: Date;
  status: BookingStatus;
  purpose: string;
  createdAt: Date;
  updatedAt: Date;
  notes?: string;
}

export interface DashboardStats {
  totalBookings: number;
  pendingBookings: number;
  approvedBookings: number;
  totalFacilities: number;
  activeFacilities: number;
  bookingsByCampus: {
    HCM: number;
    NVH: number;
  };
  bookingsByType: {
    'meeting-room': number;
    'lab-room': number;
    'sports-field': number;
  };
}
