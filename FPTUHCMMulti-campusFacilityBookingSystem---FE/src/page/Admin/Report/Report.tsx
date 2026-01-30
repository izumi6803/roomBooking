/**
 * Report Component - Báo cáo thống kê
 * 
 * Component này hiển thị báo cáo thống kê chi tiết về bookings:
 * - Overall statistics: Tổng số bookings, tỷ lệ duyệt, hủy, hoàn thành, sử dụng
 * - Daily statistics: Thống kê theo ngày
 * - Facility statistics: Thống kê theo facility
 * - Campus statistics: Thống kê theo campus
 * 
 * Tính năng:
 * - Multiple period types: day, week, month, year, custom
 * - Filters: Campus, Facility (optional)
 * - Auto-refresh: Tự động load khi filter thay đổi
 * - Date parsing: Hỗ trợ nhiều định dạng date từ backend
 * - Visual cards: Hiển thị statistics với icons và màu sắc
 * - Tables: Hiển thị chi tiết theo ngày, facility, campus
 * 
 * Period Types:
 * - day: X ngày gần nhất (1, 3, 7, 14, 30 ngày)
 * - week: 7 ngày gần nhất
 * - month: Theo tháng (chọn tháng và năm)
 * - year: Theo năm (chọn năm)
 * - custom: Khoảng thời gian tùy chỉnh (chọn từ ngày - đến ngày)
 */

// Import React hooks
import { useState, useEffect, useCallback } from 'react';
// Import icons
import {
  Calendar,
  Filter,
  Loader2,
  AlertCircle,
  TrendingUp,
  Users,
  CheckCircle2,
  XCircle,
  Clock,
  FileText,
  Building2,
  MapPin,
} from 'lucide-react';
// Import types và API functions
import type {
  GetReportParams,
  ReportResponse,
} from './api/reportApi';
import { getReport } from './api/reportApi';
import { getCampuses } from '../CampusManagement/api/campusApi';
import { getFacilities } from '../FacilityManagement/api/facilityApi';
// Import toast hook
import { useToast } from '../../../components/toast';

/**
 * Period type options
 * 
 * Các loại thời gian có thể chọn để xem báo cáo
 */
const PERIOD_TYPES = [
  { value: 'day', label: 'X ngày gần nhất' },
  { value: 'week', label: '7 ngày gần nhất' },
  { value: 'month', label: 'Theo tháng' },
  { value: 'year', label: 'Theo năm' },
  { value: 'custom', label: 'Khoảng thời gian tùy chỉnh' },
] as const;

/**
 * Days options for periodType='day'
 * 
 * Các tùy chọn số ngày khi chọn periodType='day'
 */
const DAYS_OPTIONS = [1, 3, 7, 14, 30];

/**
 * Month options
 * 
 * Danh sách các tháng (1-12)
 */
const MONTHS = [
  { value: 1, label: 'Tháng 1' },
  { value: 2, label: 'Tháng 2' },
  { value: 3, label: 'Tháng 3' },
  { value: 4, label: 'Tháng 4' },
  { value: 5, label: 'Tháng 5' },
  { value: 6, label: 'Tháng 6' },
  { value: 7, label: 'Tháng 7' },
  { value: 8, label: 'Tháng 8' },
  { value: 9, label: 'Tháng 9' },
  { value: 10, label: 'Tháng 10' },
  { value: 11, label: 'Tháng 11' },
  { value: 12, label: 'Tháng 12' },
];

/**
 * Report Component Function
 * 
 * Component để hiển thị báo cáo thống kê
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa UI báo cáo thống kê
 */
const Report = () => {
  const { showError } = useToast();

  // State cho report data
  const [reportData, setReportData] = useState<ReportResponse['data'] | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // State cho filters
  const [periodType, setPeriodType] = useState<GetReportParams['periodType']>('week');
  const [days, setDays] = useState<number>(7);
  const [month, setMonth] = useState<number>(new Date().getMonth() + 1);
  const [year, setYear] = useState<number>(new Date().getFullYear());
  const [startDate, setStartDate] = useState<string>('');
  const [endDate, setEndDate] = useState<string>('');
  const [selectedCampusId, setSelectedCampusId] = useState<string>('');
  const [selectedFacilityId, setSelectedFacilityId] = useState<string>('');

  // State cho options (campuses, facilities)
  const [campuses, setCampuses] = useState<Array<{ campusId: string; name: string }>>([]);
  const [facilities, setFacilities] = useState<Array<{ facilityId: string; name: string }>>([]);

  // Load campuses và facilities
  useEffect(() => {
    const loadOptions = async () => {
      try {
        // Load campuses
        const campusesResponse = await getCampuses({ page: 1, limit: 100 });
        if (campusesResponse.success && campusesResponse.data) {
          setCampuses(
            campusesResponse.data.map((c) => ({
              campusId: c.campusId,
              name: c.name,
            }))
          );
        }

        // Load facilities
        const facilitiesResponse = await getFacilities({ page: 1, limit: 100 });
        if (facilitiesResponse.data) {
          setFacilities(
            facilitiesResponse.data.map((f) => ({
              facilityId: f.facilityId,
              name: f.name,
            }))
          );
        }
      } catch (err) {
        console.error('Error loading options:', err);
      }
    };

    loadOptions();
  }, []);

  // Fetch report data
  const fetchReport = useCallback(async () => {
    setLoading(true);
    setError(null);

    try {
      const params: GetReportParams = {
        periodType,
      };

      // Set parameters based on periodType
      if (periodType === 'day') {
        params.days = days;
      } else if (periodType === 'month') {
        params.month = month;
        params.year = year;
      } else if (periodType === 'year') {
        params.year = year;
      } else if (periodType === 'custom') {
        if (startDate) params.startDate = startDate;
        if (endDate) params.endDate = endDate;
      }

      // Add optional filters
      if (selectedCampusId) {
        params.campusId = selectedCampusId;
      }
      if (selectedFacilityId) {
        params.facilityId = selectedFacilityId;
      }

      const response = await getReport(params);

      // Debug logging
      console.log('Report API params:', params);
      console.log('Report API response:', response);

      if (response.success && response.data) {
        // Log facilityStats để debug
        if (response.data.facilityStats) {
          console.log('Facility stats count:', response.data.facilityStats.length);
          console.log('Facility stats:', response.data.facilityStats);
        } else {
          console.log('No facilityStats in response');
        }
        setReportData(response.data);
      } else {
        setError(response.error?.message || response.message || 'Không thể tải báo cáo');
        setReportData(null);
      }
    } catch (err) {
      console.error('Error fetching report:', err);
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi tải báo cáo';
      setError(errorMessage);
      setReportData(null);
      showError(errorMessage);
    } finally {
      setLoading(false);
    }
  }, [
    periodType,
    days,
    month,
    year,
    startDate,
    endDate,
    selectedCampusId,
    selectedFacilityId,
    showError,
  ]);

  // Fetch report on mount and when filters change
  useEffect(() => {
    fetchReport();
  }, [fetchReport]);

  // Get overall statistics from nested 'overall' object
  const overallStats = reportData?.overall
    ? {
        totalBookings: reportData.overall.totalBookings || 0,
        approvedBookings: reportData.overall.approvedBookings || 0,
        rejectedBookings: reportData.overall.rejectedBookings || 0,
        cancelledBookings: reportData.overall.cancelledBookings || 0,
        completedBookings: reportData.overall.completedBookings || 0,
        pendingBookings: reportData.overall.pendingBookings || 0,
        approvalRate: reportData.overall.approvalRate || 0,
        cancellationRate: reportData.overall.cancellationRate || 0,
        completionRate: reportData.overall.completionRate || 0,
        utilizationRate: reportData.overall.utilizationRate || 0,
      }
    : null;

  // Helper function to parse date string from backend
  // Backend returns format: "dd/MM/yyyy HH:mm:ss" (e.g., "10/12/2025 09:10:11")
  const parseDateString = (dateString: string | null | undefined): Date | null => {
    if (!dateString) return null;
    
    try {
      // Try parsing "dd/MM/yyyy HH:mm:ss" format
      const ddMMyyyyWithTimeMatch = dateString.match(/^(\d{2})\/(\d{2})\/(\d{4})\s+(\d{2}):(\d{2}):(\d{2})$/);
      if (ddMMyyyyWithTimeMatch) {
        const [, day, month, year, hours, minutes, seconds] = ddMMyyyyWithTimeMatch;
        return new Date(
          parseInt(year),
          parseInt(month) - 1,
          parseInt(day),
          parseInt(hours),
          parseInt(minutes),
          parseInt(seconds)
        );
      }
      
      // Try parsing "dd/MM/yyyy" format (date only)
      const ddMMyyyyMatch = dateString.match(/^(\d{2})\/(\d{2})\/(\d{4})$/);
      if (ddMMyyyyMatch) {
        const [, day, month, year] = ddMMyyyyMatch;
        return new Date(parseInt(year), parseInt(month) - 1, parseInt(day));
      }
      
      // Fallback: try standard Date parsing (for ISO 8601 format)
      const date = new Date(dateString);
      if (!isNaN(date.getTime())) {
        return date;
      }
      
      return null;
    } catch {
      return null;
    }
  };

  // Format date for display
  const formatDate = (dateString: string): string => {
    const date = parseDateString(dateString);
    if (!date) return 'N/A';
    try {
      return date.toLocaleDateString('vi-VN', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
      });
    } catch {
      return 'N/A';
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-white to-orange-50/30 p-6">
      <div className="max-w-7xl mx-auto">
        {/* Header */}
        <div className="mb-6">
          <h1 className="text-3xl font-bold text-gray-900 mb-2">Báo cáo thống kê</h1>
          <p className="text-gray-600">Xem và phân tích dữ liệu đặt phòng chi tiết</p>
        </div>

        {/* Filters */}
        <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6 mb-6">
          <div className="flex items-center gap-2 mb-4">
            <Filter className="w-5 h-5 text-gray-400" />
            <h2 className="text-lg font-semibold text-gray-900">Bộ lọc</h2>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
            {/* Period Type */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">
                Loại thời gian
              </label>
              <select
                value={periodType}
                onChange={(e) => setPeriodType(e.target.value as GetReportParams['periodType'])}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
              >
                {PERIOD_TYPES.map((type) => (
                  <option key={type.value} value={type.value}>
                    {type.label}
                  </option>
                ))}
              </select>
            </div>

            {/* Days (for periodType='day') */}
            {periodType === 'day' && (
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Số ngày</label>
                <select
                  value={days}
                  onChange={(e) => setDays(Number(e.target.value))}
                  className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                >
                  {DAYS_OPTIONS.map((d) => (
                    <option key={d} value={d}>
                      {d} ngày
                    </option>
                  ))}
                </select>
              </div>
            )}

            {/* Month (for periodType='month') */}
            {periodType === 'month' && (
              <>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">Tháng</label>
                  <select
                    value={month}
                    onChange={(e) => setMonth(Number(e.target.value))}
                    className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                  >
                    {MONTHS.map((m) => (
                      <option key={m.value} value={m.value}>
                        {m.label}
                      </option>
                    ))}
                  </select>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">Năm</label>
                  <input
                    type="number"
                    value={year}
                    onChange={(e) => setYear(Number(e.target.value))}
                    min={2020}
                    max={2030}
                    className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                  />
                </div>
              </>
            )}

            {/* Year (for periodType='year') */}
            {periodType === 'year' && (
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Năm</label>
                <input
                  type="number"
                  value={year}
                  onChange={(e) => setYear(Number(e.target.value))}
                  min={2020}
                  max={2030}
                  className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                />
              </div>
            )}

            {/* Custom Date Range */}
            {periodType === 'custom' && (
              <>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">Từ ngày</label>
                  <input
                    type="date"
                    value={startDate}
                    onChange={(e) => setStartDate(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">Đến ngày</label>
                  <input
                    type="date"
                    value={endDate}
                    onChange={(e) => setEndDate(e.target.value)}
                    className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
                  />
                </div>
              </>
            )}

            {/* Campus Filter */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">Campus (tùy chọn)</label>
              <select
                value={selectedCampusId}
                onChange={(e) => setSelectedCampusId(e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
              >
                <option value="">Tất cả</option>
                {campuses.map((campus) => (
                  <option key={campus.campusId} value={campus.campusId}>
                    {campus.name}
                  </option>
                ))}
              </select>
            </div>

            {/* Facility Filter */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">
                Facility (tùy chọn)
              </label>
              <select
                value={selectedFacilityId}
                onChange={(e) => setSelectedFacilityId(e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-orange-500/20 focus:border-orange-500"
              >
                <option value="">Tất cả</option>
                {facilities.map((facility) => (
                  <option key={facility.facilityId} value={facility.facilityId}>
                    {facility.name}
                  </option>
                ))}
              </select>
            </div>
          </div>

          {/* Refresh Button */}
          <div className="mt-4 flex justify-end">
            <button
              onClick={fetchReport}
              disabled={loading}
              className="px-4 py-2 bg-orange-500 text-white rounded-lg hover:bg-orange-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-2"
            >
              {loading ? (
                <>
                  <Loader2 className="w-4 h-4 animate-spin" />
                  Đang tải...
                </>
              ) : (
                <>
                  <Calendar className="w-4 h-4" />
                  Tải lại
                </>
              )}
            </button>
          </div>
        </div>

        {/* Error State */}
        {error && (
          <div className="bg-red-50 border border-red-200 rounded-xl p-4 mb-6 flex items-center gap-3">
            <AlertCircle className="w-5 h-5 text-red-500 flex-shrink-0" />
            <p className="text-red-700">{error}</p>
          </div>
        )}

        {/* Loading State */}
        {loading && !reportData && (
          <div className="flex items-center justify-center py-16">
            <Loader2 className="w-8 h-8 text-orange-500 animate-spin" />
            <span className="ml-3 text-gray-600">Đang tải dữ liệu...</span>
          </div>
        )}

        {/* Report Content */}
        {!loading && reportData && (
          <>
            {/* Overall Statistics Cards */}
            {overallStats && (
              <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-6">
                <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
                  <div className="flex items-center justify-between mb-2">
                    <span className="text-sm text-gray-600">Tổng số lượt đặt</span>
                    <Users className="w-5 h-5 text-gray-400" />
                  </div>
                  <p className="text-2xl font-bold text-gray-900">{overallStats.totalBookings}</p>
                </div>

                <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
                  <div className="flex items-center justify-between mb-2">
                    <span className="text-sm text-gray-600">Đã duyệt</span>
                    <CheckCircle2 className="w-5 h-5 text-green-500" />
                  </div>
                  <p className="text-2xl font-bold text-gray-900">
                    {overallStats.approvedBookings}
                  </p>
                  <p className="text-xs text-gray-500 mt-1">
                    Tỷ lệ: {overallStats.approvalRate.toFixed(1)}%
                  </p>
                </div>

                <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
                  <div className="flex items-center justify-between mb-2">
                    <span className="text-sm text-gray-600">Đã từ chối</span>
                    <XCircle className="w-5 h-5 text-red-500" />
                  </div>
                  <p className="text-2xl font-bold text-gray-900">
                    {overallStats.rejectedBookings}
                  </p>
                </div>

                <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
                  <div className="flex items-center justify-between mb-2">
                    <span className="text-sm text-gray-600">Đã hủy</span>
                    <XCircle className="w-5 h-5 text-orange-500" />
                  </div>
                  <p className="text-2xl font-bold text-gray-900">
                    {overallStats.cancelledBookings}
                  </p>
                  <p className="text-xs text-gray-500 mt-1">
                    Tỷ lệ: {overallStats.cancellationRate.toFixed(1)}%
                  </p>
                </div>

                <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
                  <div className="flex items-center justify-between mb-2">
                    <span className="text-sm text-gray-600">Đã hoàn thành</span>
                    <CheckCircle2 className="w-5 h-5 text-blue-500" />
                  </div>
                  <p className="text-2xl font-bold text-gray-900">
                    {overallStats.completedBookings}
                  </p>
                  <p className="text-xs text-gray-500 mt-1">
                    Tỷ lệ: {overallStats.completionRate.toFixed(1)}%
                  </p>
                </div>

                <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
                  <div className="flex items-center justify-between mb-2">
                    <span className="text-sm text-gray-600">Chờ duyệt</span>
                    <Clock className="w-5 h-5 text-yellow-500" />
                  </div>
                  <p className="text-2xl font-bold text-gray-900">
                    {overallStats.pendingBookings}
                  </p>
                </div>

                <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6">
                  <div className="flex items-center justify-between mb-2">
                    <span className="text-sm text-gray-600">Tỷ lệ sử dụng</span>
                    <TrendingUp className="w-5 h-5 text-orange-500" />
                  </div>
                  <p className="text-2xl font-bold text-gray-900">
                    {overallStats.utilizationRate.toFixed(1)}%
                  </p>
                </div>
              </div>
            )}

            {/* Daily Statistics Table */}
            {reportData.dailyStats && reportData.dailyStats.length > 0 && (
              <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6 mb-6">
                <div className="flex items-center gap-2 mb-4">
                  <Calendar className="w-5 h-5 text-gray-400" />
                  <h3 className="text-lg font-semibold text-gray-900">Thống kê theo ngày</h3>
                </div>
                <div className="overflow-x-auto">
                  <table className="w-full">
                    <thead>
                      <tr className="border-b border-gray-200">
                        <th className="text-left py-3 px-4 text-sm font-semibold text-gray-700">
                          Ngày
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Tổng đặt
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Đã hoàn thành
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Tỷ lệ sử dụng
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      {reportData.dailyStats.map((stat, index) => (
                        <tr
                          key={index}
                          className="border-b border-gray-100 hover:bg-gray-50 transition-colors"
                        >
                          <td className="py-3 px-4 text-sm text-gray-900">
                            {formatDate(stat.date)}
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {stat.totalBookings}
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {stat.completedBookings}
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {stat.utilizationRate.toFixed(1)}%
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            )}

            {/* Facility Statistics Table */}
            {reportData.facilityStats !== undefined && (
              <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6 mb-6">
                <div className="flex items-center gap-2 mb-4">
                  <Building2 className="w-5 h-5 text-gray-400" />
                  <h3 className="text-lg font-semibold text-gray-900">Thống kê theo Facility</h3>
                </div>
                {reportData.facilityStats.length > 0 ? (
                  <div className="overflow-x-auto">
                  <table className="w-full">
                    <thead>
                      <tr className="border-b border-gray-200">
                        <th className="text-left py-3 px-4 text-sm font-semibold text-gray-700">
                          Facility
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Tổng đặt
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Đã hoàn thành
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Tỷ lệ sử dụng
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Đánh giá TB
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      {reportData.facilityStats.map((facility) => (
                        <tr
                          key={facility.facilityId}
                          className="border-b border-gray-100 hover:bg-gray-50 transition-colors"
                        >
                          <td className="py-3 px-4 text-sm text-gray-900">
                            {facility.facilityName}
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {facility.totalBookings}
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {facility.completedBookings}
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {facility.utilizationRate.toFixed(1)}%
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {facility.averageRating > 0
                              ? facility.averageRating.toFixed(1)
                              : 'N/A'}
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
                ) : (
                  <div className="text-center py-8 text-gray-500">
                    <Building2 className="w-12 h-12 text-gray-300 mx-auto mb-2" />
                    <p>Không có dữ liệu thống kê theo Facility cho bộ lọc đã chọn</p>
                  </div>
                )}
              </div>
            )}

            {/* Campus Statistics Table */}
            {reportData.campusStats && reportData.campusStats.length > 0 && (
              <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-6 mb-6">
                <div className="flex items-center gap-2 mb-4">
                  <MapPin className="w-5 h-5 text-gray-400" />
                  <h3 className="text-lg font-semibold text-gray-900">Thống kê theo Campus</h3>
                </div>
                <div className="overflow-x-auto">
                  <table className="w-full">
                    <thead>
                      <tr className="border-b border-gray-200">
                        <th className="text-left py-3 px-4 text-sm font-semibold text-gray-700">
                          Campus
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Tổng đặt
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Đã hoàn thành
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Tỷ lệ sử dụng
                        </th>
                        <th className="text-center py-3 px-4 text-sm font-semibold text-gray-700">
                          Tổng số Facility
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      {reportData.campusStats.map((campus) => (
                        <tr
                          key={campus.campusId}
                          className="border-b border-gray-100 hover:bg-gray-50 transition-colors"
                        >
                          <td className="py-3 px-4 text-sm text-gray-900">{campus.campusName}</td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {campus.totalBookings}
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {campus.completedBookings}
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {campus.utilizationRate.toFixed(1)}%
                          </td>
                          <td className="py-3 px-4 text-sm text-gray-900 text-center">
                            {campus.totalFacilities}
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            )}
          </>
        )}

        {/* Empty State */}
        {!loading && !reportData && !error && (
          <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-12 text-center">
            <FileText className="w-12 h-12 text-gray-400 mx-auto mb-4" />
            <h3 className="text-lg font-semibold text-gray-900 mb-2">Chưa có dữ liệu</h3>
            <p className="text-gray-600">
              Vui lòng chọn bộ lọc và tải lại để xem báo cáo thống kê.
            </p>
          </div>
        )}
      </div>
    </div>
  );
};

export default Report;
