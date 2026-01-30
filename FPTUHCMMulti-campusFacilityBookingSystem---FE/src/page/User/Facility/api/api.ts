import { API_BASE_URL, API_ENDPOINTS, apiFetch, buildUrl } from '../../../../services/api.config';
import type { Facility, Campus, FacilityType } from '../../../../types';

// Backend response types
export interface CampusResponse {
  campusId: string;
  name: string;
  imageUrl?: string;
  address?: string;
  phoneNumber?: string;
  email?: string;
  status: string;
}

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

export interface FacilityFilters {
  campus?: Campus;
  type?: FacilityType;
  searchQuery?: string;
}

// Map backend response to frontend type
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
    'Phòng học': 'Classroom',
    'Meeting Room': 'Meeting Room',
    'Phòng họp': 'Meeting Room',
    'Computer Lab': 'Laboratory',
    'Phòng máy tính': 'Laboratory',
    'Laboratory': 'Laboratory',
    'Sports Court': 'Sport Facility',
    'Sân thể thao': 'Sport Facility', // Backend returns Vietnamese name
    'Sport Facility': 'Sport Facility',
  };
  return typeMap[typeName] || 'Classroom';
};

// Map campusId to Campus type
const mapCampusIdToCampus = (campusId: string): Campus => {
  // C0001 = HCM, C0002 = NVH
  if (campusId === 'C0001') return 'HCM';
  if (campusId === 'C0002') return 'NVH';
  // Fallback: check if name contains NVH
  return 'HCM'; // Default to HCM
};

// Map CampusResponse to CampusInfo
export interface CampusInfo {
  id: Campus;
  name: string;
  fullName: string;
  description: string;
  gradient: string;
  imageUrl: string;
  imageAlt: string;
}

export const mapCampusResponse = (c: CampusResponse): CampusInfo => {
  const campusId = mapCampusIdToCampus(c.campusId);
  
  // Default values based on campus
  const defaults: Record<Campus, { gradient: string; imageUrl: string; description: string; fullName: string }> = {
    HCM: {
      gradient: 'from-orange-500 to-amber-600',
      imageUrl: '/images/HCM.webp',
      description: 'Campus chính tại Quận 9 với đầy đủ cơ sở vật chất hiện đại',
      fullName: 'FPT University HCMC - Quận 9'
    },
    NVH: {
      gradient: 'from-violet-500 to-purple-600',
      imageUrl: '/images/nvh.jpg',
      description: 'Campus NVH với không gian học tập đa dạng',
      fullName: 'FPT University NVH'
    }
  };
  
  const defaultValues = defaults[campusId];
  
  return {
    id: campusId,
    name: c.name || (campusId === 'HCM' ? 'HCM Campus' : 'NVH Campus'),
    fullName: c.address || defaultValues.fullName,
    description: defaultValues.description,
    gradient: defaultValues.gradient,
    imageUrl: c.imageUrl || defaultValues.imageUrl,
    imageAlt: `FPTU ${c.name || campusId} Campus`
  };
};

export const userFacilityApi = {
  // Get all available facilities for booking - API ONLY
  getAvailableFacilities: async (filters?: FacilityFilters): Promise<Facility[]> => {
    try {
      const params: Record<string, string | number | undefined> = {
        page: 1,
        limit: 100,
      };
      
      if (filters?.searchQuery) {
        params.name = filters.searchQuery;
      }
      
      const url = buildUrl(API_ENDPOINTS.FACILITY.GET_ALL, params);
      console.log('Fetching facilities from:', url);
      
      const response = await apiFetch<FacilityResponse[]>(url);
      console.log('Facilities response:', response);
      
      if (response.success && response.data) {
        // Filter only Available facilities on client-side
        let facilities = response.data
          .filter(f => f.status === 'Available')
          .map(mapFacilityResponse);
        
        // Apply client-side filters
        if (filters?.campus) {
          facilities = facilities.filter(f => f.campus === filters.campus);
        }
        
        if (filters?.type) {
          facilities = facilities.filter(f => f.type === filters.type);
        }
        
        return facilities;
      }
      
      console.error('API Error:', response.error);
      return [];
    } catch (error) {
      console.error('Error fetching facilities:', error);
      return [];
    }
  },

  // Get facility by ID - API ONLY
  getFacilityById: async (id: string): Promise<Facility | null> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.FACILITY.GET_BY_ID(id)}`;
      const response = await apiFetch<FacilityResponse>(url);
      
      if (response.success && response.data) {
        return mapFacilityResponse(response.data);
      }
      
      return null;
    } catch (error) {
      console.error('Error fetching facility:', error);
      return null;
    }
  },

  // Get all campuses - API ONLY
  getCampuses: async (): Promise<CampusResponse[]> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.CAMPUS.GET_ALL}`;
      const response = await apiFetch<CampusResponse[]>(url);
      
      if (response.success && response.data) {
        return response.data;
      }
      
      return [];
    } catch (error) {
      console.error('Error fetching campuses:', error);
      return [];
    }
  },

  // Get all campuses mapped to CampusInfo - API ONLY
  getCampusesInfo: async (): Promise<CampusInfo[]> => {
    try {
      const url = `${API_BASE_URL}${API_ENDPOINTS.CAMPUS.GET_ALL}`;
      const response = await apiFetch<CampusResponse[]>(url);
      
      if (response.success && response.data) {
        return response.data
          .filter(c => c.status === 'Active') // Only return active campuses
          .map(mapCampusResponse);
      }
      
      return [];
    } catch (error) {
      console.error('Error fetching campuses info:', error);
      return [];
    }
  },

  // Get facilities count by campus - API ONLY
  getFacilitiesCountByCampus: async (): Promise<{ HCM: number; NVH: number }> => {
    try {
      const url = buildUrl(API_ENDPOINTS.FACILITY.GET_ALL, { limit: 1000 });
      const response = await apiFetch<FacilityResponse[]>(url);
      
      if (response.success && response.data) {
        // Filter only Available facilities
        const facilities = response.data.filter(f => f.status === 'Available');
        return {
          HCM: facilities.filter(f => !f.campusName?.includes('NVH') && !f.campusName?.includes('Nhà Văn Hóa') && !f.campusName?.includes('Sinh Viên')).length,
          NVH: facilities.filter(f => f.campusName?.includes('NVH') || f.campusName?.includes('Nhà Văn Hóa') || f.campusName?.includes('Sinh Viên')).length,
        };
      }
      
      return { HCM: 0, NVH: 0 };
    } catch (error) {
      console.error('Error fetching facility count:', error);
      return { HCM: 0, NVH: 0 };
    }
  }
};
