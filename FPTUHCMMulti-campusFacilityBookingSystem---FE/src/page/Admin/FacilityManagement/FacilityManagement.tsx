/**
 * FacilityManagement Component - Quản lý Facility và Facility Type
 * 
 * Component này cho phép admin quản lý:
 * - Facilities: Tạo, chỉnh sửa, xóa, xem feedbacks và ratings
 * - Facility Types: Tạo và chỉnh sửa loại facility
 * 
 * Tính năng:
 * - Tab system: Chuyển đổi giữa "Facilities" và "Facility Types"
 * - CRUD operations cho cả Facilities và Facility Types
 * - Filters: Tên, status, type, campus
 * - Pagination cho danh sách facilities
 * - View feedbacks: Xem feedbacks của từng facility
 * - View ratings: Xem rating trung bình của facility
 * - Delete confirmation: Modal xác nhận trước khi xóa
 * 
 * Tabs:
 * - facilities: Quản lý facilities
 * - facility-types: Quản lý loại facilities
 */

// Import React hooks
import { useState, useEffect, useCallback } from 'react'
// Import icons
import { Plus, Edit2, Trash2, Loader2, AlertCircle, Building2, Search, Filter, X, Tag, Star, MessageSquare, Eye } from 'lucide-react'
// Import types và API functions cho Facilities
import type { Facility, FacilityRequest, GetFacilitiesParams, FacilityFeedback } from './api/facilityApi'
import { getFacilities, createFacility, updateFacility, deleteFacility, getFacilityFeedbacks, getFacilityRating } from './api/facilityApi'
// Import types và API functions cho Facility Types
import type { FacilityType, FacilityTypeRequest } from './api/facilityTypeApi'
import { getFacilityTypes, createFacilityType, updateFacilityType } from './api/facilityTypeApi'
// Import Campus API
import { getCampuses, type Campus } from '../CampusManagement/api/campusApi'
// Import toast hook
import { useToast } from '../../../components/toast'
// Import components
import FacilityForm from './components/FacilityForm'
import FacilityTypeForm from './components/FacilityTypeForm'
import Pagination from '../Facility Dashboard/components/Pagination'

/**
 * Type định nghĩa các tab có sẵn
 */
type TabType = 'facilities' | 'facility-types'

/**
 * FacilityManagement Component Function
 * 
 * Component chính để quản lý facilities và facility types
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa UI quản lý facilities
 */
const FacilityManagement = () => {
  const { showSuccess, showError } = useToast()
  
  // Tab state
  const [activeTab, setActiveTab] = useState<TabType>('facilities')

  // ========== FACILITIES STATE ==========
  const [facilities, setFacilities] = useState<Facility[]>([])
  const [facilitiesLoading, setFacilitiesLoading] = useState(true)
  const [facilitiesError, setFacilitiesError] = useState<string | null>(null)

  // Pagination for facilities
  const [currentPage, setCurrentPage] = useState(1)
  const [itemsPerPage] = useState(10)
  const [totalItems, setTotalItems] = useState(0)

  // Filters for facilities
  const [nameFilter, setNameFilter] = useState('')
  const [statusFilter, setStatusFilter] = useState<'Available' | 'Under_Maintenance' | ''>('')
  const [typeIdFilter, setTypeIdFilter] = useState('')
  const [campusIdFilter, setCampusIdFilter] = useState('')

  // Options for filters (campuses and facility types)
  const [campuses, setCampuses] = useState<Campus[]>([])
  const [facilityTypes, setFacilityTypes] = useState<FacilityType[]>([])

  // Form states for facilities
  const [showFacilityForm, setShowFacilityForm] = useState(false)
  const [editingFacility, setEditingFacility] = useState<Facility | null>(null)
  const [facilityFormLoading, setFacilityFormLoading] = useState(false)
  const [deleteConfirm, setDeleteConfirm] = useState<string | null>(null)
  const [deleteLoading, setDeleteLoading] = useState(false)

  // Feedback states
  const [showFeedbackModal, setShowFeedbackModal] = useState(false)
  const [selectedFacility, setSelectedFacility] = useState<Facility | null>(null)
  const [feedbacks, setFeedbacks] = useState<FacilityFeedback[]>([])
  const [feedbacksLoading, setFeedbacksLoading] = useState(false)
  const [facilityRatings, setFacilityRatings] = useState<Record<string, number>>({})

  // ========== FACILITY TYPES STATE ==========
  const [facilityTypesList, setFacilityTypesList] = useState<FacilityType[]>([])
  const [facilityTypesLoading, setFacilityTypesLoading] = useState(true)
  const [facilityTypesError, setFacilityTypesError] = useState<string | null>(null)

  // Form states for facility types
  const [showFacilityTypeForm, setShowFacilityTypeForm] = useState(false)
  const [editingFacilityType, setEditingFacilityType] = useState<FacilityType | null>(null)
  const [facilityTypeFormLoading, setFacilityTypeFormLoading] = useState(false)

  // Load campuses and facility types for filters
  useEffect(() => {
    const loadOptions = async () => {
      try {
        const [campusesRes, typesRes] = await Promise.all([
          getCampuses({ page: 1, limit: 100 }),
          getFacilityTypes({ page: 1, limit: 100 }),
        ])

        if (campusesRes.success && campusesRes.data) {
          setCampuses(campusesRes.data.filter((c) => c.status === 'Active'))
        }

        if (typesRes.success && typesRes.data) {
          setFacilityTypes(typesRes.data)
        }
      } catch (error) {
        console.error('Error loading options:', error)
      }
    }

    loadOptions()
  }, [])

  // Fetch facilities
  const fetchFacilities = useCallback(async () => {
    setFacilitiesLoading(true)
    setFacilitiesError(null)

    try {
      const params: GetFacilitiesParams = {
        page: currentPage,
        limit: itemsPerPage,
      }

      if (nameFilter.trim()) {
        params.name = nameFilter.trim()
      }

      if (statusFilter) {
        params.status = statusFilter
      }

      if (typeIdFilter) {
        params.typeId = typeIdFilter
      }

      if (campusIdFilter) {
        params.campusId = campusIdFilter
      }

      const response = await getFacilities(params)

      if (response.data) {
        setFacilities(response.data)
        if (response.pagination) {
          setTotalItems(response.pagination.total)
        } else {
          setTotalItems(response.data.length)
        }
        
        // Load ratings for all facilities
        const ratings: Record<string, number> = {}
        for (const facility of response.data) {
          try {
            const ratingResponse = await getFacilityRating(facility.facilityId)
            if (ratingResponse.success && ratingResponse.data !== undefined) {
              ratings[facility.facilityId] = ratingResponse.data
            }
          } catch {
            // Ignore errors for individual ratings
          }
        }
        setFacilityRatings(ratings)
      } else {
        setFacilitiesError('Không thể tải danh sách facilities')
        setFacilities([])
      }
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi tải danh sách facilities'
      setFacilitiesError(errorMessage)
      setFacilities([])
    } finally {
      setFacilitiesLoading(false)
    }
  }, [currentPage, itemsPerPage, nameFilter, statusFilter, typeIdFilter, campusIdFilter])

  // Load feedbacks for a facility
  const handleViewFeedbacks = async (facility: Facility) => {
    setSelectedFacility(facility)
    setShowFeedbackModal(true)
    setFeedbacksLoading(true)
    setFeedbacks([])

    try {
      const response = await getFacilityFeedbacks(facility.facilityId)
      if (response.success && response.data) {
        setFeedbacks(response.data)
      } else {
        showError(response.error?.message || 'Không thể tải danh sách feedbacks')
        setFeedbacks([])
      }
    } catch {
      showError('Đã xảy ra lỗi khi tải danh sách feedbacks')
      setFeedbacks([])
    } finally {
      setFeedbacksLoading(false)
    }
  }

  // Fetch facility types
  const fetchFacilityTypes = useCallback(async () => {
    setFacilityTypesLoading(true)
    setFacilityTypesError(null)

    try {
      const response = await getFacilityTypes({ page: 1, limit: 100 })

      if (response.success && response.data) {
        setFacilityTypesList(response.data)
      } else {
        setFacilityTypesError('Không thể tải danh sách facility types')
        setFacilityTypesList([])
      }
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi tải danh sách facility types'
      setFacilityTypesError(errorMessage)
      setFacilityTypesList([])
    } finally {
      setFacilityTypesLoading(false)
    }
  }, [])

  // Fetch data when tab changes or dependencies change
  useEffect(() => {
    if (activeTab === 'facilities') {
      fetchFacilities()
    } else {
      fetchFacilityTypes()
    }
  }, [activeTab, fetchFacilities, fetchFacilityTypes])

  // Handle page change
  const handlePageChange = (page: number) => {
    setCurrentPage(page)
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  // Handle filter changes - reset to page 1
  const handleFilterChange = () => {
    setCurrentPage(1)
  }

  // Handle save facility
  const handleSaveFacility = async (facilityData: FacilityRequest) => {
    setFacilityFormLoading(true)

    try {
      if (editingFacility) {
        await updateFacility(editingFacility.facilityId, facilityData, facilityData.status)
      } else {
        await createFacility(facilityData)
      }

      setShowFacilityForm(false)
      setEditingFacility(null)
      showSuccess(editingFacility ? 'Cập nhật facility thành công!' : 'Tạo facility mới thành công!')
      await fetchFacilities()
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi lưu facility'
      showError(errorMessage)
      throw err
    } finally {
      setFacilityFormLoading(false)
    }
  }

  // Handle delete facility
  const handleDeleteFacility = async (facilityId: string) => {
    setDeleteLoading(true)

    try {
      await deleteFacility(facilityId)
      setDeleteConfirm(null)
      showSuccess('Vô hiệu hóa facility thành công!')
      await fetchFacilities()
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi vô hiệu hóa facility'
      showError(errorMessage)
    } finally {
      setDeleteLoading(false)
    }
  }

  // Handle save facility type
  const handleSaveFacilityType = async (facilityTypeData: FacilityTypeRequest) => {
    setFacilityTypeFormLoading(true)

    try {
      if (editingFacilityType) {
        await updateFacilityType(editingFacilityType.typeId, facilityTypeData)
      } else {
        await createFacilityType(facilityTypeData)
      }

      setShowFacilityTypeForm(false)
      setEditingFacilityType(null)
      showSuccess(editingFacilityType ? 'Cập nhật loại facility thành công!' : 'Tạo loại facility mới thành công!')
      await fetchFacilityTypes()
      // Also refresh facility types for filter dropdown
      const typesRes = await getFacilityTypes({ page: 1, limit: 100 })
      if (typesRes.success && typesRes.data) {
        setFacilityTypes(typesRes.data)
      }
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi lưu facility type'
      showError(errorMessage)
      throw err
    } finally {
      setFacilityTypeFormLoading(false)
    }
  }

  // Get status badge color
  const getStatusColor = (status: string) => {
    return status === 'Available'
      ? 'bg-green-100 text-green-700'
      : 'bg-orange-100 text-orange-700'
  }

  const totalPages = Math.ceil(totalItems / itemsPerPage)

  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <div className="mx-auto max-w-7xl px-4">
        {/* Header */}
        <div className="mb-6">
          <h1 className="mb-2 text-3xl font-bold text-gray-900">Quản lý cơ sở vật chất</h1>
          <p className="text-gray-600">Quản lý tất cả cơ sở vật chất và loại cơ sở vật chất trong hệ thống</p>
        </div>

        {/* Tabs */}
        <div className="mb-6 border-b border-gray-200">
          <div className="flex gap-2">
            <button
              onClick={() => setActiveTab('facilities')}
              className={`px-4 py-2 text-sm font-semibold transition-colors ${
                activeTab === 'facilities'
                  ? 'border-b-2 border-orange-500 text-orange-600'
                  : 'text-gray-600 hover:text-gray-900'
              }`}
            >
              <Building2 className="mr-2 inline h-4 w-4" />
              Facilities
            </button>
            <button
              onClick={() => setActiveTab('facility-types')}
              className={`px-4 py-2 text-sm font-semibold transition-colors ${
                activeTab === 'facility-types'
                  ? 'border-b-2 border-orange-500 text-orange-600'
                  : 'text-gray-600 hover:text-gray-900'
              }`}
            >
              <Tag className="mr-2 inline h-4 w-4" />
              Loại Facility
            </button>
          </div>
        </div>

        {/* FACILITIES TAB */}
        {activeTab === 'facilities' && (
          <>
            {/* Header with Add button */}
            <div className="mb-6 flex items-center justify-between">
              <h2 className="text-xl font-semibold text-gray-900">Danh sách Facilities</h2>
              <button
                onClick={() => {
                  setEditingFacility(null)
                  setShowFacilityForm(true)
                }}
                className="flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors"
              >
                <Plus className="h-4 w-4" />
                Thêm Facility
              </button>
            </div>

            {/* Filters */}
            <div className="mb-6 rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
              <div className="flex flex-wrap items-center gap-4">
                <div className="relative flex-1 min-w-[200px]">
                  <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-gray-400" />
                  <input
                    type="text"
                    placeholder="Tìm theo tên..."
                    value={nameFilter}
                    onChange={(e) => {
                      setNameFilter(e.target.value)
                      handleFilterChange()
                    }}
                    className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-4 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                  />
                </div>

                <div className="flex items-center gap-2">
                  <Filter className="h-4 w-4 text-gray-400" />
                  <select
                    value={statusFilter}
                    onChange={(e) => {
                      setStatusFilter(e.target.value as 'Available' | 'Under_Maintenance' | '')
                      handleFilterChange()
                    }}
                    className="rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                  >
                    <option value="">Tất cả trạng thái</option>
                    <option value="Available">Available</option>
                    <option value="Under_Maintenance">Under Maintenance</option>
                  </select>
                </div>

                <select
                  value={typeIdFilter}
                  onChange={(e) => {
                    setTypeIdFilter(e.target.value)
                    handleFilterChange()
                  }}
                  className="rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                >
                  <option value="">Tất cả loại</option>
                  {facilityTypes.map((type) => (
                    <option key={type.typeId} value={type.typeId}>
                      {type.name}
                    </option>
                  ))}
                </select>

                <select
                  value={campusIdFilter}
                  onChange={(e) => {
                    setCampusIdFilter(e.target.value)
                    handleFilterChange()
                  }}
                  className="rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                >
                  <option value="">Tất cả campus</option>
                  {campuses.map((campus) => (
                    <option key={campus.campusId} value={campus.campusId}>
                      {campus.name}
                    </option>
                  ))}
                </select>

                {(nameFilter || statusFilter || typeIdFilter || campusIdFilter) && (
                  <button
                    onClick={() => {
                      setNameFilter('')
                      setStatusFilter('')
                      setTypeIdFilter('')
                      setCampusIdFilter('')
                      handleFilterChange()
                    }}
                    className="flex items-center gap-1 rounded-lg border border-gray-300 px-3 py-2 text-sm text-gray-700 hover:bg-gray-50"
                  >
                    <X className="h-4 w-4" />
                    Xóa bộ lọc
                  </button>
                )}
              </div>
            </div>

            {/* Loading State */}
            {facilitiesLoading && (
              <div className="flex flex-col items-center justify-center rounded-lg border border-gray-200 bg-white p-12">
                <Loader2 className="h-8 w-8 animate-spin text-orange-500" />
                <p className="mt-4 text-sm text-gray-600">Đang tải danh sách facilities...</p>
              </div>
            )}

            {/* Error State */}
            {!facilitiesLoading && facilitiesError && (
              <div className="rounded-lg border border-red-200 bg-red-50 p-6">
                <div className="flex items-center gap-3">
                  <AlertCircle className="h-5 w-5 text-red-600" />
                  <div>
                    <h3 className="text-sm font-semibold text-red-800">Lỗi khi tải dữ liệu</h3>
                    <p className="mt-1 text-sm text-red-600">{facilitiesError}</p>
                  </div>
                </div>
                <button
                  onClick={fetchFacilities}
                  className="mt-4 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white hover:bg-red-700 transition-colors"
                >
                  Thử lại
                </button>
              </div>
            )}

            {/* Facilities Table */}
            {!facilitiesLoading && !facilitiesError && facilities.length > 0 && (
              <>
                <div className="overflow-hidden rounded-lg border border-gray-200 bg-white shadow-sm">
                  <div className="overflow-x-auto">
                    <table className="min-w-full divide-y divide-gray-200">
                      <thead className="bg-gray-50">
                        <tr>
                          <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                            Tên Facility
                          </th>
                          <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                            Loại
                          </th>
                          <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                            Campus
                          </th>
                          <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                            Vị trí
                          </th>
                          <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                            Sức chứa
                          </th>
                          <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                            Trạng thái
                          </th>
                          <th className="px-6 py-3 text-left text-xs font-semibold uppercase tracking-wider text-gray-700">
                            Đánh giá
                          </th>
                          <th className="px-6 py-3 text-right text-xs font-semibold uppercase tracking-wider text-gray-700">
                            Thao tác
                          </th>
                        </tr>
                      </thead>
                      <tbody className="divide-y divide-gray-200 bg-white">
                        {facilities.map((facility) => (
                          <tr key={facility.facilityId} className="hover:bg-gray-50">
                            <td className="whitespace-nowrap px-6 py-4">
                              <div className="flex items-center">
                                <Building2 className="mr-2 h-5 w-5 text-orange-500" />
                                <div>
                                  <div className="text-sm font-semibold text-gray-900">{facility.name}</div>
                                  <div className="text-xs text-gray-500">ID: {facility.facilityId}</div>
                                </div>
                              </div>
                            </td>
                            <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-900">
                              {facility.typeName}
                            </td>
                            <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-900">
                              {facility.campusName}
                            </td>
                            <td className="px-6 py-4 text-sm text-gray-900">
                              {facility.floorNumber && facility.roomNumber
                                ? `Tầng ${facility.floorNumber}, Phòng ${facility.roomNumber}`
                                : facility.roomNumber || facility.floorNumber || '-'}
                            </td>
                            <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-900">
                              {facility.capacity === -1 ? (
                                <span className="text-gray-600 italic">Nhiều người</span>
                              ) : (
                                facility.capacity
                              )}
                            </td>
                            <td className="whitespace-nowrap px-6 py-4">
                              <span
                                className={`inline-flex rounded-full px-2 py-1 text-xs font-semibold ${getStatusColor(
                                  facility.status
                                )}`}
                              >
                                {facility.status === 'Available' ? 'Available' : 'Under Maintenance'}
                              </span>
                            </td>
                            <td className="whitespace-nowrap px-6 py-4 text-sm">
                              <div className="flex items-center gap-2">
                                {facilityRatings[facility.facilityId] !== undefined ? (
                                  <>
                                    <div className="flex items-center gap-1">
                                      <Star className="h-4 w-4 fill-yellow-400 text-yellow-400" />
                                      <span className="font-semibold text-gray-900">
                                        {facilityRatings[facility.facilityId].toFixed(1)}
                                      </span>
                                    </div>
                                    <button
                                      onClick={() => handleViewFeedbacks(facility)}
                                      className="flex items-center gap-1 rounded-lg bg-blue-50 px-2 py-1 text-xs text-blue-600 hover:bg-blue-100 transition-colors"
                                      title="Xem feedbacks"
                                    >
                                      <Eye className="h-3 w-3" />
                                      Xem
                                    </button>
                                  </>
                                ) : (
                                  <span className="text-gray-400 text-xs">Chưa có đánh giá</span>
                                )}
                              </div>
                            </td>
                            <td className="whitespace-nowrap px-6 py-4 text-right text-sm font-medium">
                              <div className="flex items-center justify-end gap-2">
                                <button
                                  onClick={() => {
                                    setEditingFacility(facility)
                                    setShowFacilityForm(true)
                                  }}
                                  className="rounded-lg bg-blue-50 p-2 text-blue-600 hover:bg-blue-100 transition-colors"
                                  title="Chỉnh sửa"
                                >
                                  <Edit2 className="h-4 w-4" />
                                </button>
                                <button
                                  onClick={() => setDeleteConfirm(facility.facilityId)}
                                  disabled={facility.status === 'Under_Maintenance'}
                                  className="rounded-lg bg-red-50 p-2 text-red-600 hover:bg-red-100 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                                  title={
                                    facility.status === 'Under_Maintenance'
                                      ? 'Đã vô hiệu hóa'
                                      : 'Vô hiệu hóa'
                                  }
                                >
                                  <Trash2 className="h-4 w-4" />
                                </button>
                              </div>
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </div>

                {/* Pagination */}
                {totalPages > 1 && (
                  <div className="mt-6">
                    <Pagination
                      currentPage={currentPage}
                      totalPages={totalPages}
                      onPageChange={handlePageChange}
                      totalItems={totalItems}
                      itemsPerPage={itemsPerPage}
                    />
                  </div>
                )}
              </>
            )}

            {/* Empty State */}
            {!facilitiesLoading && !facilitiesError && facilities.length === 0 && (
              <div className="rounded-lg border border-gray-200 bg-white p-12 text-center shadow-sm">
                <Building2 className="mx-auto h-12 w-12 text-gray-400" />
                <h3 className="mt-4 text-lg font-semibold text-gray-900">Không tìm thấy facility nào</h3>
                <p className="mt-2 text-sm text-gray-600">
                  {nameFilter || statusFilter || typeIdFilter || campusIdFilter
                    ? 'Thử điều chỉnh bộ lọc hoặc thêm facility mới.'
                    : 'Bắt đầu bằng cách thêm facility đầu tiên vào hệ thống.'}
                </p>
                <button
                  onClick={() => {
                    setEditingFacility(null)
                    setShowFacilityForm(true)
                  }}
                  className="mt-4 flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors mx-auto"
                >
                  <Plus className="h-4 w-4" />
                  Thêm Facility
                </button>
              </div>
            )}
          </>
        )}

        {/* FACILITY TYPES TAB */}
        {activeTab === 'facility-types' && (
          <>
            {/* Header with Add button */}
            <div className="mb-6 flex items-center justify-between">
              <h2 className="text-xl font-semibold text-gray-900">Danh sách Loại Facility</h2>
              <button
                onClick={() => {
                  setEditingFacilityType(null)
                  setShowFacilityTypeForm(true)
                }}
                className="flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors"
              >
                <Plus className="h-4 w-4" />
                Thêm Loại Facility
              </button>
            </div>

            {/* Loading State */}
            {facilityTypesLoading && (
              <div className="flex flex-col items-center justify-center rounded-lg border border-gray-200 bg-white p-12">
                <Loader2 className="h-8 w-8 animate-spin text-orange-500" />
                <p className="mt-4 text-sm text-gray-600">Đang tải danh sách facility types...</p>
              </div>
            )}

            {/* Error State */}
            {!facilityTypesLoading && facilityTypesError && (
              <div className="rounded-lg border border-red-200 bg-red-50 p-6">
                <div className="flex items-center gap-3">
                  <AlertCircle className="h-5 w-5 text-red-600" />
                  <div>
                    <h3 className="text-sm font-semibold text-red-800">Lỗi khi tải dữ liệu</h3>
                    <p className="mt-1 text-sm text-red-600">{facilityTypesError}</p>
                  </div>
                </div>
                <button
                  onClick={fetchFacilityTypes}
                  className="mt-4 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white hover:bg-red-700 transition-colors"
                >
                  Thử lại
                </button>
              </div>
            )}

            {/* Facility Types Grid */}
            {!facilityTypesLoading && !facilityTypesError && facilityTypesList.length > 0 && (
              <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
                {facilityTypesList.map((type) => (
                  <div
                    key={type.typeId}
                    className="rounded-lg border border-gray-200 bg-white p-5 shadow-sm transition-shadow hover:shadow-md"
                  >
                    <div className="mb-3 flex items-start justify-between">
                      <div className="flex-1">
                        <div className="flex items-center gap-2">
                          <Tag className="h-5 w-5 text-orange-500" />
                          <h3 className="text-lg font-semibold text-gray-900">{type.name}</h3>
                        </div>
                        <p className="mt-1 text-xs text-gray-500">ID: {type.typeId}</p>
                      </div>
                    </div>

                    <div className="mb-3 space-y-2 text-sm text-gray-600">
                      {type.description && <p className="text-xs">{type.description}</p>}
                      <p>
                        <span className="font-semibold">Sức chứa mặc định:</span> {type.defaultCapacity} người
                      </p>
                      {type.typicalDurationHours > 0 && (
                        <p>
                          <span className="font-semibold">Thời lượng điển hình:</span> {type.typicalDurationHours} giờ
                        </p>
                      )}
                      {type.defaultAmenities && (
                        <div>
                          <p className="font-semibold">Tiện ích mặc định:</p>
                          <p className="text-xs">{type.defaultAmenities}</p>
                        </div>
                      )}
                    </div>

                    <div className="flex gap-2">
                      <button
                        onClick={() => {
                          setEditingFacilityType(type)
                          setShowFacilityTypeForm(true)
                        }}
                        className="flex-1 rounded-lg border border-orange-300 bg-orange-50 px-3 py-2 text-sm font-semibold text-orange-600 hover:bg-orange-100 transition-colors"
                      >
                        <Edit2 className="mr-1 inline h-4 w-4" />
                        Chỉnh sửa
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            )}

            {/* Empty State */}
            {!facilityTypesLoading && !facilityTypesError && facilityTypesList.length === 0 && (
              <div className="rounded-lg border border-gray-200 bg-white p-12 text-center shadow-sm">
                <Tag className="mx-auto h-12 w-12 text-gray-400" />
                <h3 className="mt-4 text-lg font-semibold text-gray-900">Chưa có loại facility nào</h3>
                <p className="mt-2 text-sm text-gray-600">
                  Bắt đầu bằng cách thêm loại facility đầu tiên vào hệ thống.
                </p>
                <button
                  onClick={() => {
                    setEditingFacilityType(null)
                    setShowFacilityTypeForm(true)
                  }}
                  className="mt-4 flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors mx-auto"
                >
                  <Plus className="h-4 w-4" />
                  Thêm Loại Facility
                </button>
              </div>
            )}
          </>
        )}

        {/* Delete Confirmation Modal */}
        {deleteConfirm && (
          <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
            <div className="w-full max-w-md rounded-lg border border-gray-200 bg-white p-6 shadow-xl">
              <div className="mb-4 flex items-center gap-3">
                <div className="flex h-10 w-10 items-center justify-center rounded-full bg-red-100">
                  <Trash2 className="h-5 w-5 text-red-600" />
                </div>
                <div>
                  <h3 className="text-lg font-semibold text-gray-900">Vô hiệu hóa Facility</h3>
                  <p className="text-sm text-gray-600">Bạn có chắc chắn muốn vô hiệu hóa facility này?</p>
                </div>
              </div>
              <p className="mb-6 rounded-lg bg-gray-50 p-3 text-sm text-gray-700">
                <strong>Lưu ý:</strong> Đây là thao tác soft delete. Facility sẽ được đánh dấu là "Under Maintenance"
                nhưng dữ liệu vẫn được giữ lại trong hệ thống.
              </p>
              <div className="flex justify-end gap-3">
                <button
                  onClick={() => setDeleteConfirm(null)}
                  disabled={deleteLoading}
                  className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  Hủy
                </button>
                <button
                  onClick={() => handleDeleteFacility(deleteConfirm)}
                  disabled={deleteLoading}
                  className="flex items-center gap-2 rounded-lg bg-red-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-red-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  {deleteLoading && <Loader2 className="h-4 w-4 animate-spin" />}
                  Xác nhận vô hiệu hóa
                </button>
              </div>
            </div>
          </div>
        )}

        {/* Facility Form Modal */}
        {showFacilityForm && (
          <FacilityForm
            facility={editingFacility}
            onClose={() => {
              setShowFacilityForm(false)
              setEditingFacility(null)
            }}
            onSave={handleSaveFacility}
            loading={facilityFormLoading}
          />
        )}

        {/* Facility Type Form Modal */}
        {showFacilityTypeForm && (
          <FacilityTypeForm
            facilityType={editingFacilityType}
            onClose={() => {
              setShowFacilityTypeForm(false)
              setEditingFacilityType(null)
            }}
            onSave={handleSaveFacilityType}
            loading={facilityTypeFormLoading}
          />
        )}

        {/* Feedback Modal */}
        {showFeedbackModal && selectedFacility && (
          <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
            <div className="w-full max-w-4xl max-h-[90vh] overflow-hidden rounded-lg border border-gray-200 bg-white shadow-xl">
              {/* Modal Header */}
              <div className="flex items-center justify-between border-b border-gray-200 bg-gradient-to-r from-orange-500 to-purple-600 px-6 py-4">
                <div className="flex items-center gap-3">
                  <Building2 className="h-6 w-6 text-white" />
                  <div>
                    <h3 className="text-lg font-bold text-white">Feedbacks - {selectedFacility.name}</h3>
                    <p className="text-sm text-white/80">ID: {selectedFacility.facilityId}</p>
                  </div>
                </div>
                <button
                  onClick={() => {
                    setShowFeedbackModal(false)
                    setSelectedFacility(null)
                    setFeedbacks([])
                  }}
                  className="text-white/80 hover:text-white transition-colors"
                >
                  <X className="h-5 w-5" />
                </button>
              </div>

              {/* Modal Content */}
              <div className="overflow-y-auto max-h-[calc(90vh-120px)] p-6">
                {feedbacksLoading ? (
                  <div className="flex flex-col items-center justify-center py-12">
                    <Loader2 className="h-8 w-8 animate-spin text-orange-500" />
                    <p className="mt-4 text-sm text-gray-600">Đang tải feedbacks...</p>
                  </div>
                ) : feedbacks.length === 0 ? (
                  <div className="text-center py-12">
                    <MessageSquare className="h-12 w-12 text-gray-300 mx-auto mb-4" />
                    <h3 className="text-lg font-semibold text-gray-900 mb-2">Chưa có feedback nào</h3>
                    <p className="text-sm text-gray-600">Facility này chưa nhận được đánh giá từ người dùng.</p>
                  </div>
                ) : (
                  <div className="space-y-4">
                    {feedbacks.map((feedback) => (
                      <div
                        key={feedback.feedbackId}
                        className="rounded-lg border border-gray-200 bg-white p-4 hover:shadow-md transition-shadow"
                      >
                        <div className="flex items-start justify-between mb-3">
                          <div className="flex-1">
                            <div className="flex items-center gap-2 mb-1">
                              <span className="font-semibold text-gray-900">{feedback.userName || 'Người dùng'}</span>
                              <div className="flex items-center gap-1">
                                {[1, 2, 3, 4, 5].map((star) => (
                                  <Star
                                    key={star}
                                    className={`h-4 w-4 ${
                                      star <= feedback.rating
                                        ? 'fill-yellow-400 text-yellow-400'
                                        : 'text-gray-300'
                                    }`}
                                  />
                                ))}
                              </div>
                            </div>
                            <p className="text-xs text-gray-500">
                              {new Date(feedback.createdAt).toLocaleString('vi-VN', {
                                day: '2-digit',
                                month: '2-digit',
                                year: 'numeric',
                                hour: '2-digit',
                                minute: '2-digit',
                              })}
                            </p>
                          </div>
                          {feedback.reportIssue && (
                            <span
                              className={`inline-flex rounded-full px-2 py-1 text-xs font-semibold ${
                                feedback.isResolved
                                  ? 'bg-green-100 text-green-700'
                                  : 'bg-red-100 text-red-700'
                              }`}
                            >
                              {feedback.isResolved ? 'Đã xử lý' : 'Có vấn đề'}
                            </span>
                          )}
                        </div>
                        {feedback.comments && (
                          <p className="text-sm text-gray-700 bg-gray-50 rounded-lg p-3 mt-2">
                            {feedback.comments}
                          </p>
                        )}
                      </div>
                    ))}
                  </div>
                )}
              </div>

              {/* Modal Footer */}
              <div className="border-t border-gray-200 bg-gray-50 px-6 py-4 flex items-center justify-between">
                <div className="text-sm text-gray-600">
                  Tổng cộng: <span className="font-semibold">{feedbacks.length}</span> feedbacks
                </div>
                <button
                  onClick={() => {
                    setShowFeedbackModal(false)
                    setSelectedFacility(null)
                    setFeedbacks([])
                  }}
                  className="rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white hover:bg-orange-600 transition-colors"
                >
                  Đóng
                </button>
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  )
}

export default FacilityManagement
