import { useState, useEffect } from 'react'
import { Building2, MapPin, Users, FileText, X, Loader2 } from 'lucide-react'
import type { Facility, FacilityRequest } from '../api/facilityApi'
import { getCampuses, type Campus } from '../../CampusManagement/api/campusApi'
import { getFacilityTypes, type FacilityType } from '../api/facilityTypeApi'

interface FacilityFormProps {
  facility: Facility | null
  onClose: () => void
  onSave: (facilityData: FacilityRequest) => Promise<void>
  loading?: boolean
}

const FacilityForm = ({ facility, onClose, onSave, loading = false }: FacilityFormProps) => {
  const isEdit = facility !== null

  // State cho dropdowns
  const [campuses, setCampuses] = useState<Campus[]>([])
  const [facilityTypes, setFacilityTypes] = useState<FacilityType[]>([])
  const [loadingOptions, setLoadingOptions] = useState(true)
  const [selectedTypeId, setSelectedTypeId] = useState<string>(facility?.typeId || '')
  const [capacity, setCapacity] = useState<number | string>(facility?.capacity || '')
  
  // Giá trị đặc biệt cho sức chứa không xác định (sân thể thao)
  const UNLIMITED_CAPACITY = -1

  // Load campuses và facility types
  useEffect(() => {
    const loadOptions = async () => {
      setLoadingOptions(true)
      try {
        const [campusesRes, typesRes] = await Promise.all([
          getCampuses({ page: 1, limit: 100 }), // Lấy tất cả campuses
          getFacilityTypes({ page: 1, limit: 100 }), // Lấy tất cả facility types
        ])

        if (campusesRes.success && campusesRes.data) {
          // Chỉ lấy campuses Active
          setCampuses(campusesRes.data.filter((c) => c.status === 'Active'))
        }

        if (typesRes.success && typesRes.data) {
          setFacilityTypes(typesRes.data)
        }
      } catch (error) {
        console.error('Error loading options:', error)
        alert('Không thể tải danh sách campuses và facility types')
      } finally {
        setLoadingOptions(false)
      }
    }

    loadOptions()
  }, [])

  // Kiểm tra xem loại facility có phải là sân thể thao không
  const isSportsFieldType = (typeId: string): boolean => {
    if (!typeId) return false
    const selectedType = facilityTypes.find((type) => type.typeId === typeId)
    if (!selectedType) return false
    
    // Kiểm tra tên loại có chứa từ khóa liên quan đến sân thể thao
    const typeName = selectedType.name.toLowerCase()
    return (
      typeName.includes('sân thể thao') ||
      typeName.includes('sân thể thao') ||
      typeName.includes('sports field') ||
      typeName.includes('sport field') ||
      typeName.includes('sân bóng') ||
      typeName.includes('sân tennis') ||
      typeName.includes('sân cầu lông') ||
      typeName.includes('sân bóng rổ') ||
      typeName.includes('sân bóng chuyền')
    )
  }

  // Xử lý khi thay đổi loại facility
  const handleTypeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const newTypeId = e.target.value
    setSelectedTypeId(newTypeId)
    
    // Nếu là sân thể thao, tự động set capacity = -1 (không xác định)
    if (isSportsFieldType(newTypeId)) {
      setCapacity(UNLIMITED_CAPACITY)
    } else {
      // Nếu không phải sân thể thao và đang là -1, reset về rỗng hoặc giá trị mặc định
      if (capacity === UNLIMITED_CAPACITY) {
        setCapacity('')
      }
    }
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const formData = new FormData(e.currentTarget)

    const facilityData: FacilityRequest = {
      name: formData.get('name')?.toString() || '',
      description: formData.get('description')?.toString() || '',
      capacity: Number(formData.get('capacity')) || 0,
      roomNumber: formData.get('roomNumber')?.toString() || '',
      floorNumber: formData.get('floorNumber')?.toString() || '',
      campusId: formData.get('campusId')?.toString() || '',
      typeId: formData.get('typeId')?.toString() || '',
      status: (formData.get('status')?.toString() || 'Available') as 'Available' | 'Under_Maintenance',
      amenities: formData.get('amenities')?.toString() || '',
      facilityManagerId: formData.get('facilityManagerId')?.toString() || undefined,
      maxConcurrentBookings: Number(formData.get('maxConcurrentBookings')) || 1,
    }

    // Validation
    if (!facilityData.campusId) {
      alert('Vui lòng chọn campus')
      return
    }

    if (!facilityData.typeId) {
      alert('Vui lòng chọn loại facility')
      return
    }

    // Validation capacity: cho phép -1 (không xác định) cho sân thể thao, hoặc > 0 cho loại khác
    if (facilityData.capacity === UNLIMITED_CAPACITY) {
      // Đã được set tự động cho sân thể thao, không cần validate
    } else if (facilityData.capacity <= 0) {
      alert('Sức chứa phải lớn hơn 0')
      return
    }

    try {
      await onSave(facilityData)
    } catch (error) {
      // Error đã được xử lý trong parent component
      console.error('Error saving facility:', error)
    }
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
      <div className="w-full max-w-3xl rounded-lg border border-gray-200 bg-white shadow-xl max-h-[90vh] overflow-y-auto">
        <div className="sticky top-0 flex items-center justify-between border-b border-gray-200 bg-white p-6">
          <h2 className="text-xl font-semibold text-gray-900">
            {isEdit ? 'Chỉnh sửa cơ sở vật chất' : 'Thêm cơ sở vật chất mới'}
          </h2>
          <button
            onClick={onClose}
            disabled={loading}
            className="rounded-lg p-1 text-gray-400 hover:bg-gray-100 hover:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <X className="h-5 w-5" />
          </button>
        </div>

        {loadingOptions ? (
          <div className="flex items-center justify-center p-12">
            <Loader2 className="h-8 w-8 animate-spin text-orange-500" />
            <p className="ml-3 text-sm text-gray-600">Đang tải dữ liệu...</p>
          </div>
        ) : (
          <form onSubmit={handleSubmit} className="p-6">
            <div className="space-y-4">
              <div className="grid gap-4 sm:grid-cols-2">
                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    <Building2 className="mr-1 inline h-4 w-4" />
                    Tên cơ sở vật chất <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="text"
                    name="name"
                    defaultValue={facility?.name || ''}
                    required
                    disabled={loading}
                    className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                  />
                </div>

                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    Loại <span className="text-red-500">*</span>
                  </label>
                  <select
                    name="typeId"
                    value={selectedTypeId}
                    onChange={handleTypeChange}
                    required
                    disabled={loading || loadingOptions}
                    className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                  >
                    <option value="">Chọn loại</option>
                    {facilityTypes.map((type) => (
                      <option key={type.typeId} value={type.typeId}>
                        {type.name}
                      </option>
                    ))}
                  </select>
                </div>
              </div>

              <div className="grid gap-4 sm:grid-cols-2">
                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    <MapPin className="mr-1 inline h-4 w-4" />
                    Campus <span className="text-red-500">*</span>
                  </label>
                  <select
                    name="campusId"
                    defaultValue={facility?.campusId || ''}
                    required
                    disabled={loading || loadingOptions}
                    className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                  >
                    <option value="">Chọn campus</option>
                    {campuses.map((campus) => (
                      <option key={campus.campusId} value={campus.campusId}>
                        {campus.name}
                      </option>
                    ))}
                  </select>
                </div>

                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    <Users className="mr-1 inline h-4 w-4" />
                    Sức chứa <span className="text-red-500">*</span>
                  </label>
                  {isSportsFieldType(selectedTypeId) ? (
                    <div className="w-full rounded-lg border border-gray-300 bg-gray-100 px-3 py-2 text-sm text-gray-600">
                      Nhiều người (Không xác định)
                    </div>
                  ) : (
                    <input
                      type="number"
                      name="capacity"
                      value={capacity === UNLIMITED_CAPACITY ? '' : capacity}
                      onChange={(e) => setCapacity(e.target.value ? Number(e.target.value) : '')}
                      min="1"
                      required
                      disabled={loading}
                      className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                    />
                  )}
                  <input
                    type="hidden"
                    name="capacity"
                    value={isSportsFieldType(selectedTypeId) ? UNLIMITED_CAPACITY : capacity}
                  />
                  {isSportsFieldType(selectedTypeId) && (
                    <p className="mt-1 text-xs text-gray-500">
                      Sân thể thao có sức chứa không xác định (nhiều người)
                    </p>
                  )}
                </div>
              </div>

              <div className="grid gap-4 sm:grid-cols-3">
                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    Số phòng <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="text"
                    name="roomNumber"
                    defaultValue={facility?.roomNumber || ''}
                    required
                    disabled={loading}
                    placeholder="VD: A101"
                    className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                  />
                </div>

                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    Tầng <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="text"
                    name="floorNumber"
                    defaultValue={facility?.floorNumber || ''}
                    required
                    disabled={loading}
                    placeholder="VD: 3"
                    className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                  />
                </div>

                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    Trạng thái <span className="text-red-500">*</span>
                  </label>
                  <select
                    name="status"
                    defaultValue={facility?.status || 'Available'}
                    required
                    disabled={loading}
                    className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                  >
                    <option value="Available">Available</option>
                    <option value="Under_Maintenance">Under Maintenance</option>
                  </select>
                </div>
              </div>

              <div>
                <label className="mb-2 block text-sm font-semibold text-gray-700">
                  <FileText className="mr-1 inline h-4 w-4" />
                  Mô tả
                </label>
                <textarea
                  name="description"
                  defaultValue={facility?.description || ''}
                  rows={3}
                  disabled={loading}
                  className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                />
              </div>

              <div>
                <label className="mb-2 block text-sm font-semibold text-gray-700">Tiện ích</label>
                <input
                  type="text"
                  name="amenities"
                  defaultValue={facility?.amenities || ''}
                  disabled={loading}
                  placeholder="VD: Máy chiếu, WiFi, Điều hòa (phân cách bằng dấu phẩy)"
                  className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                />
                <p className="mt-1 text-xs text-gray-500">Phân cách các tiện ích bằng dấu phẩy</p>
              </div>

              <div className="grid gap-4 sm:grid-cols-2">
                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    Facility Manager ID (Optional)
                  </label>
                  <input
                    type="text"
                    name="facilityManagerId"
                    defaultValue={facility?.facilityManagerId || ''}
                    disabled={loading}
                    placeholder="VD: U0001"
                    className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                  />
                </div>

                <div>
                  <label className="mb-2 block text-sm font-semibold text-gray-700">
                    Số booking đồng thời tối đa <span className="text-red-500">*</span>
                  </label>
                  <input
                    type="number"
                    name="maxConcurrentBookings"
                    defaultValue={facility?.maxConcurrentBookings || 1}
                    min="1"
                    required
                    disabled={loading}
                    className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                  />
                </div>
              </div>
            </div>

            <div className="mt-6 flex justify-end gap-3">
              <button
                type="button"
                onClick={onClose}
                disabled={loading}
                className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
              >
                Hủy
              </button>
              <button
                type="submit"
                disabled={loading || loadingOptions}
                className="flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {loading && <Loader2 className="h-4 w-4 animate-spin" />}
                {isEdit ? 'Cập nhật' : 'Tạo mới'}
              </button>
            </div>
          </form>
        )}
      </div>
    </div>
  )
}

export default FacilityForm
