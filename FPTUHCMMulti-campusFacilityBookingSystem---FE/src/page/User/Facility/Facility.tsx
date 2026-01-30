import { useState, useEffect, useCallback } from 'react';
import { Link } from 'react-router-dom';
import { MapPin, Users, Clock, Search, Building2, FlaskConical, Trophy, ChevronRight, Sparkles } from 'lucide-react';
import type { Facility, Campus, FacilityType } from '../../../types';
import { userFacilityApi, type CampusInfo } from './api/api';

const FacilityPage = () => {
  const [facilities, setFacilities] = useState<Facility[]>([]);
  const [loading, setLoading] = useState(false);
  const [campuses, setCampuses] = useState<CampusInfo[]>([]);
  const [campusesLoading, setCampusesLoading] = useState(true);
  const [selectedCampus, setSelectedCampus] = useState<Campus | null>(null);
  const [selectedType, setSelectedType] = useState<FacilityType | 'all'>('all');
  const [searchQuery, setSearchQuery] = useState('');
  const [campusCounts, setCampusCounts] = useState<{ HCM: number; NVH: number }>({ HCM: 0, NVH: 0 });

  useEffect(() => {
    loadCampuses();
    loadCampusCounts();
  }, []);

  const loadCampuses = async () => {
    setCampusesLoading(true);
    try {
      const data = await userFacilityApi.getCampusesInfo();
      setCampuses(data);
    } catch (error) {
      console.error('Error loading campuses:', error);
    } finally {
      setCampusesLoading(false);
    }
  };

  useEffect(() => {
    if (selectedCampus) {
      loadFacilities();
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedCampus, selectedType, searchQuery]);

  const loadCampusCounts = async () => {
    const counts = await userFacilityApi.getFacilitiesCountByCampus();
    setCampusCounts(counts);
  };

  const loadFacilities = useCallback(async () => {
    if (!selectedCampus) return;
    
    setLoading(true);
    try {
      const data = await userFacilityApi.getAvailableFacilities({
        campus: selectedCampus,
        type: selectedType !== 'all' ? selectedType : undefined,
        searchQuery: searchQuery || undefined,
      });
      setFacilities(data);
    } catch (error) {
      console.error('Error loading facilities:', error);
    } finally {
      setLoading(false);
    }
  }, [selectedCampus, selectedType, searchQuery]);

  const handleCampusSelect = (campus: Campus) => {
    setSelectedCampus(campus);
    setSelectedType('all');
    setSearchQuery('');
  };

  const handleBackToCampusSelection = () => {
    setSelectedCampus(null);
    setFacilities([]);
  };

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
        return <Building2 className="w-5 h-5" />;
      case 'Laboratory':
      case 'lab-room':
        return <FlaskConical className="w-5 h-5" />;
      case 'Sport Facility':
      case 'sports-field':
        return <Trophy className="w-5 h-5" />;
    }
  };

  const getFacilityTypeColor = (type: FacilityType) => {
    const colors: Record<string, { bg: string; text: string; border: string; accent: string }> = {
      'Classroom': { 
        bg: 'bg-blue-50', 
        text: 'text-blue-700', 
        border: 'border-blue-200',
        accent: 'bg-blue-500'
      },
      'Meeting Room': { 
        bg: 'bg-violet-50', 
        text: 'text-violet-700', 
        border: 'border-violet-200',
        accent: 'bg-violet-500'
      },
      'meeting-room': { 
        bg: 'bg-violet-50', 
        text: 'text-violet-700', 
        border: 'border-violet-200',
        accent: 'bg-violet-500'
      },
      'Laboratory': { 
        bg: 'bg-amber-50', 
        text: 'text-amber-700', 
        border: 'border-amber-200',
        accent: 'bg-amber-500'
      },
      'lab-room': { 
        bg: 'bg-amber-50', 
        text: 'text-amber-700', 
        border: 'border-amber-200',
        accent: 'bg-amber-500'
      },
      'Sport Facility': { 
        bg: 'bg-emerald-50', 
        text: 'text-emerald-700', 
        border: 'border-emerald-200',
        accent: 'bg-emerald-500'
      },
      'sports-field': { 
        bg: 'bg-emerald-50', 
        text: 'text-emerald-700', 
        border: 'border-emerald-200',
        accent: 'bg-emerald-500'
      },
    };
    return colors[type] || { bg: 'bg-gray-50', text: 'text-gray-700', border: 'border-gray-200', accent: 'bg-gray-500' };
  };

  const selectedCampusInfo = selectedCampus 
    ? campuses.find(c => c.id === selectedCampus) 
    : null;

  // Campus Selection View
  if (!selectedCampus) {
  return (
      <div className="min-h-screen bg-gradient-to-br from-slate-50 via-white to-orange-50/30">
        {/* Header Section */}
        <div className="relative overflow-hidden bg-gradient-to-r from-orange-500 via-orange-600 to-purple-600">
          <div className="absolute inset-0 bg-[url('data:image/svg+xml,%3Csvg%20width%3D%2260%22%20height%3D%2260%22%20viewBox%3D%220%200%2060%2060%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%3E%3Cg%20fill%3D%22none%22%20fill-rule%3D%22evenodd%22%3E%3Cg%20fill%3D%22%23ffffff%22%20fill-opacity%3D%220.05%22%3E%3Cpath%20d%3D%22M36%2034v-4h-2v4h-4v2h4v4h2v-4h4v-2h-4zm0-30V0h-2v4h-4v2h4v4h2V6h4V4h-4zM6%2034v-4H4v4H0v2h4v4h2v-4h4v-2H6zM6%204V0H4v4H0v2h4v4h2V6h4V4H6z%22%2F%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E')] opacity-30" />
          <div className="absolute -left-20 -top-20 w-72 h-72 bg-white/10 rounded-full blur-3xl" />
          <div className="absolute -right-20 -bottom-20 w-96 h-96 bg-purple-500/20 rounded-full blur-3xl" />
          
          <div className="relative max-w-6xl mx-auto px-4 py-16 sm:py-20 text-center">
            <div className="inline-flex items-center gap-2 px-4 py-1.5 rounded-full bg-white/20 backdrop-blur-sm text-white/90 text-xs font-medium mb-6">
              <Sparkles className="w-3.5 h-3.5" />
              <span>Đặt phòng dễ dàng</span>
            </div>
            <h1 className="text-3xl sm:text-4xl lg:text-5xl font-black text-white mb-4">
              Khám phá cơ sở vật chất
            </h1>
            <p className="text-orange-100 text-base sm:text-lg max-w-2xl mx-auto">
              Chọn campus để xem và đặt các phòng họp, phòng lab, sân thể thao có sẵn
            </p>
          </div>
        </div>

        {/* Campus Selection Cards */}
        <div className="max-w-5xl mx-auto px-4 -mt-12 pb-16">
          {campusesLoading ? (
            <div className="grid md:grid-cols-2 gap-6">
              {[1, 2].map((i) => (
                <div key={i} className="bg-white rounded-2xl shadow-lg shadow-gray-200/50 overflow-hidden animate-pulse">
                  <div className="h-36 bg-gray-200" />
                  <div className="p-6 space-y-4">
                    <div className="h-6 bg-gray-200 rounded w-3/4" />
                    <div className="h-4 bg-gray-200 rounded w-1/2" />
                    <div className="h-4 bg-gray-200 rounded w-full" />
                    <div className="h-4 bg-gray-200 rounded w-2/3" />
                  </div>
                </div>
              ))}
            </div>
          ) : campuses.length === 0 ? (
            <div className="text-center py-16 bg-white rounded-2xl shadow-sm">
              <div className="w-16 h-16 rounded-full bg-gray-100 flex items-center justify-center mx-auto mb-4">
                <Building2 className="w-8 h-8 text-gray-400" />
              </div>
              <h3 className="text-lg font-semibold text-gray-900 mb-2">
                Không có campus nào
              </h3>
              <p className="text-gray-500 text-sm">
                Hiện tại không có campus nào khả dụng
              </p>
            </div>
          ) : (
            <div className="grid md:grid-cols-2 gap-6">
              {campuses.map((campus) => (
              <button
                key={campus.id}
                onClick={() => handleCampusSelect(campus.id)}
                className="group relative bg-white rounded-2xl shadow-lg shadow-gray-200/50 overflow-hidden hover:shadow-xl hover:shadow-gray-300/50 transition-all duration-300 transform hover:-translate-y-1 text-left"
              >
                {/* Image Header */}
                <div className="relative h-36 overflow-hidden bg-gray-100">
                  <div className={`absolute inset-0 bg-gradient-to-br ${campus.gradient} opacity-70`} />
                  <img
                    src={campus.imageUrl}
                    alt={campus.imageAlt}
                    className="absolute inset-0 w-full h-full object-cover"
                    onError={(e) => { e.currentTarget.style.display = 'none'; }}
                    loading="lazy"
                  />
                  <div className="absolute inset-0 bg-black/15" />
                  <div className="absolute right-4 bottom-4 text-white/80 drop-shadow">
                    <MapPin className="w-16 h-16" />
                  </div>
                </div>

                {/* Content */}
                <div className="p-6">
                  <div className="flex items-start justify-between gap-4">
                    <div className="flex-1">
                      <h3 className="text-xl font-bold text-gray-900 mb-1 group-hover:text-orange-600 transition-colors">
                        {campus.name}
                      </h3>
                      <p className="text-sm text-gray-500 mb-3">{campus.fullName}</p>
                      <p className="text-sm text-gray-600 leading-relaxed">
                        {campus.description}
                      </p>
                    </div>
                    <div className={`flex-shrink-0 w-10 h-10 rounded-full bg-gradient-to-br ${campus.gradient} flex items-center justify-center text-white group-hover:scale-110 transition-transform`}>
                      <ChevronRight className="w-5 h-5" />
                    </div>
                  </div>

                  {/* Stats */}
                  <div className="mt-5 pt-5 border-t border-gray-100 flex items-center gap-4">
                    <div className="flex items-center gap-2 text-sm">
                      <Building2 className="w-4 h-4 text-gray-400" />
                      <span className="font-semibold text-gray-900">
                        {campusCounts[campus.id]}
                      </span>
                      <span className="text-gray-500">cơ sở khả dụng</span>
                    </div>
                  </div>
                </div>
              </button>
              ))}
            </div>
          )}

          {/* Info Section */}
          <div className="mt-12 bg-gradient-to-br from-slate-50 to-slate-100 rounded-2xl p-8 border border-slate-200/50">
            <div className="flex items-start gap-4">
              <div className="flex-shrink-0 w-12 h-12 rounded-xl bg-orange-100 flex items-center justify-center">
                <Clock className="w-6 h-6 text-orange-600" />
              </div>
              <div>
                <h4 className="font-semibold text-gray-900 mb-2">Quy trình đặt phòng</h4>
                <ul className="space-y-2 text-sm text-gray-600">
                  <li className="flex items-center gap-2">
                    <span className="w-5 h-5 rounded-full bg-orange-500 text-white text-xs flex items-center justify-center font-medium">1</span>
                    Chọn campus và loại cơ sở vật chất bạn cần
                  </li>
                  <li className="flex items-center gap-2">
                    <span className="w-5 h-5 rounded-full bg-orange-500 text-white text-xs flex items-center justify-center font-medium">2</span>
                    Xem thông tin chi tiết và lịch trống
                  </li>
                  <li className="flex items-center gap-2">
                    <span className="w-5 h-5 rounded-full bg-orange-500 text-white text-xs flex items-center justify-center font-medium">3</span>
                    Gửi yêu cầu đặt và chờ phê duyệt
                  </li>
                </ul>
              </div>
            </div>
          </div>
      </div>
    </div>
    );
  }

  // Facilities List View
  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-white to-orange-50/30">
      {/* Header with selected campus */}
      <div 
        className={`relative overflow-hidden bg-gradient-to-r ${selectedCampusInfo?.gradient || 'from-orange-500 to-purple-600'}`}
      >
        <div className="absolute inset-0 bg-[url('data:image/svg+xml,%3Csvg%20width%3D%2260%22%20height%3D%2260%22%20viewBox%3D%220%200%2060%2060%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%3E%3Cg%20fill%3D%22none%22%20fill-rule%3D%22evenodd%22%3E%3Cg%20fill%3D%22%23ffffff%22%20fill-opacity%3D%220.05%22%3E%3Cpath%20d%3D%22M36%2034v-4h-2v4h-4v2h4v4h2v-4h4v-2h-4zm0-30V0h-2v4h-4v2h4v4h2V6h4V4h-4zM6%2034v-4H4v4H0v2h4v4h2v-4h4v-2H6zM6%204V0H4v4H0v2h4v4h2V6h4V4H6z%22%2F%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E')] opacity-30" />
        
        <div className="relative max-w-6xl mx-auto px-4 py-8">
          <button
            onClick={handleBackToCampusSelection}
            className="inline-flex items-center gap-2 text-white/80 hover:text-white text-sm font-medium mb-4 transition-colors"
          >
            <ChevronRight className="w-4 h-4 rotate-180" />
            Đổi campus
          </button>
          
          <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
            <div>
              <h1 className="text-2xl sm:text-3xl font-bold text-white">
                {selectedCampusInfo?.name}
              </h1>
              <p className="text-white/80 text-sm mt-1">
                {selectedCampusInfo?.fullName}
              </p>
            </div>
            <div className="flex items-center gap-2 bg-white/20 backdrop-blur-sm rounded-full px-4 py-2">
              <Building2 className="w-4 h-4 text-white" />
              <span className="text-white font-medium">{facilities.length} cơ sở</span>
            </div>
          </div>
        </div>
      </div>

      {/* Filters */}
      <div className="max-w-6xl mx-auto px-4 mt-6">
        <div className="bg-white rounded-xl shadow-lg shadow-gray-200/50 p-4 sm:p-6">
          <div className="space-y-4">
            {/* Search */}
            <div className="relative">
              <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-gray-400" />
              <input
                type="text"
                placeholder="Tìm kiếm theo tên, vị trí..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="w-full pl-10 pr-4 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500 transition-all"
              />
            </div>

            {/* Type Filter */}
            <div className="flex gap-2 flex-wrap">
              <button
                onClick={() => setSelectedType('all')}
                className={`px-4 py-2.5 rounded-lg text-sm font-medium transition-all ${
                  selectedType === 'all'
                    ? 'bg-gray-900 text-white'
                    : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
                }`}
              >
                Tất cả
              </button>
              {(['Classroom', 'Meeting Room', 'Laboratory', 'Sport Facility'] as FacilityType[]).map((type) => {
                const colors = getFacilityTypeColor(type);
                const isSelected = selectedType === type;
                return (
                  <button
                    key={type}
                    onClick={() => setSelectedType(type)}
                    className={`px-4 py-2.5 rounded-lg text-sm font-medium transition-all flex items-center gap-2 ${
                      isSelected
                        ? `${colors.bg} ${colors.text} ring-2 ring-offset-1 ${colors.border}`
                        : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
                    }`}
                  >
                    {getFacilityTypeIcon(type)}
                    {getFacilityTypeLabel(type)}
                  </button>
                );
              })}
            </div>
          </div>
        </div>
      </div>

      {/* Facilities Grid */}
      <div className="max-w-6xl mx-auto px-4 py-8">
        {loading ? (
          <div className="grid sm:grid-cols-2 lg:grid-cols-3 gap-6">
            {[1, 2, 3, 4, 5, 6].map((i) => (
              <div key={i} className="bg-white rounded-xl shadow-sm animate-pulse">
                <div className="h-3 bg-gray-200 rounded-t-xl" />
                <div className="p-6 space-y-4">
                  <div className="flex gap-3">
                    <div className="w-12 h-12 bg-gray-200 rounded-lg" />
                    <div className="flex-1 space-y-2">
                      <div className="h-5 bg-gray-200 rounded w-3/4" />
                      <div className="h-4 bg-gray-200 rounded w-1/2" />
                    </div>
                  </div>
                  <div className="space-y-2">
                    <div className="h-4 bg-gray-200 rounded" />
                    <div className="h-4 bg-gray-200 rounded w-2/3" />
                  </div>
                </div>
              </div>
            ))}
          </div>
        ) : facilities.length === 0 ? (
          <div className="text-center py-16 bg-white rounded-2xl shadow-sm">
            <div className="w-16 h-16 rounded-full bg-gray-100 flex items-center justify-center mx-auto mb-4">
              <Search className="w-8 h-8 text-gray-400" />
            </div>
            <h3 className="text-lg font-semibold text-gray-900 mb-2">
              Không tìm thấy cơ sở vật chất
            </h3>
            <p className="text-gray-500 text-sm max-w-md mx-auto">
              Thử thay đổi bộ lọc hoặc từ khóa tìm kiếm để xem kết quả khác
            </p>
            <button
              onClick={() => {
                setSelectedType('all');
                setSearchQuery('');
              }}
              className="mt-4 px-4 py-2 bg-orange-500 text-white rounded-lg text-sm font-medium hover:bg-orange-600 transition-colors"
            >
              Xóa bộ lọc
            </button>
          </div>
        ) : (
          <div className="grid sm:grid-cols-2 lg:grid-cols-3 gap-6">
            {facilities.map((facility) => {
              const colors = getFacilityTypeColor(facility.type);
              return (
                <div
                  key={facility.id}
                  className="group bg-white rounded-xl shadow-sm hover:shadow-lg transition-all duration-300 overflow-hidden border border-gray-100 flex flex-col"
                >
                  {/* Type Indicator Bar */}
                  <div className={`h-1.5 ${colors.accent}`} />
                  
                  <div className="p-6 flex flex-col flex-1">
                    {/* Header */}
                    <div className="flex items-start gap-4 mb-4">
                      <div className={`flex-shrink-0 w-12 h-12 rounded-xl ${colors.bg} ${colors.text} flex items-center justify-center`}>
                        {getFacilityTypeIcon(facility.type)}
                      </div>
                      <div className="flex-1 min-w-0">
                        <h3 className="font-semibold text-gray-900 truncate group-hover:text-orange-600 transition-colors">
                          {facility.name}
                        </h3>
                        <span className={`inline-block px-2 py-0.5 rounded text-xs font-medium ${colors.bg} ${colors.text} mt-1`}>
                          {getFacilityTypeLabel(facility.type)}
                        </span>
                      </div>
                    </div>

                    {/* Details */}
                    <div className="space-y-2.5 mb-4">
                      <div className="flex items-center gap-2 text-sm text-gray-600">
                        <MapPin className="w-4 h-4 text-gray-400 flex-shrink-0" />
                        <span className="truncate">{facility.location}</span>
                      </div>
                      {/* Display capacity - show "Nhiều người" for sports fields with capacity -1 */}
                      {(facility.type === 'Sport Facility' || facility.type === 'sports-field') && facility.capacity === -1 ? (
                        <div className="flex items-center gap-2 text-sm text-gray-600">
                          <Users className="w-4 h-4 text-gray-400 flex-shrink-0" />
                          <span>Sức chứa: <strong className="text-gray-900">Nhiều người</strong></span>
                        </div>
                      ) : facility.type !== 'Sport Facility' && facility.type !== 'sports-field' ? (
                        <div className="flex items-center gap-2 text-sm text-gray-600">
                          <Users className="w-4 h-4 text-gray-400 flex-shrink-0" />
                          <span>Sức chứa: <strong className="text-gray-900">{facility.capacity}</strong> người</span>
                        </div>
                      ) : null}
                    </div>

                    {/* Description */}
                    {facility.description && (
                      <p className="text-sm text-gray-500 line-clamp-2 mb-4">
                        {facility.description}
                      </p>
                    )}

                    {/* Amenities */}
                    {facility.amenities && facility.amenities.length > 0 && (
                      <div className="flex flex-wrap gap-1.5 mb-4">
                        {facility.amenities.slice(0, 3).map((amenity, index) => (
                          <span
                            key={index}
                            className="px-2 py-1 bg-gray-100 text-gray-600 rounded text-xs"
                          >
                            {amenity}
                          </span>
                        ))}
                        {facility.amenities.length > 3 && (
                          <span className="px-2 py-1 bg-gray-100 text-gray-500 rounded text-xs">
                            +{facility.amenities.length - 3}
                          </span>
                        )}
                      </div>
                    )}

                    {/* Action Button - pushed to bottom with mt-auto */}
                    <Link
                      to={`/booking/${facility.id}`}
                      className={`mt-auto w-full inline-flex items-center justify-center gap-2 px-4 py-2.5 bg-gradient-to-r ${selectedCampusInfo?.gradient || 'from-orange-500 to-orange-600'} text-white rounded-lg text-sm font-medium hover:opacity-90 transition-opacity`}
                    >
                      Đặt ngay
                      <ChevronRight className="w-4 h-4" />
                    </Link>
                  </div>
                </div>
              );
            })}
          </div>
        )}

        {/* Stats Summary */}
        {!loading && facilities.length > 0 && (
          <div className="mt-8 grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-6 gap-4">
            {(['Classroom', 'Meeting Room', 'Laboratory', 'Sport Facility'] as FacilityType[]).map((type) => {
              const count = facilities.filter(f => f.type === type).length;
              if (count === 0) return null;
              const colors = getFacilityTypeColor(type);
              return (
                <div key={type} className={`${colors.bg} rounded-xl p-4 border ${colors.border}`}>
                  <div className="flex items-center gap-3">
                    <div className={`w-10 h-10 rounded-lg bg-white flex items-center justify-center ${colors.text}`}>
                      {getFacilityTypeIcon(type)}
                    </div>
                    <div>
                      <div className={`text-2xl font-bold ${colors.text}`}>{count}</div>
                      <div className="text-sm text-gray-600">{getFacilityTypeLabel(type)}</div>
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        )}
      </div>
    </div>
  );
};

export default FacilityPage;
