import { Building2, FileText, X, Loader2, Users, Clock, Image } from 'lucide-react'
import type { FacilityType, FacilityTypeRequest } from '../api/facilityTypeApi'

interface FacilityTypeFormProps {
  facilityType: FacilityType | null
  onClose: () => void
  onSave: (facilityTypeData: FacilityTypeRequest) => Promise<void>
  loading?: boolean
}

const FacilityTypeForm = ({ facilityType, onClose, onSave, loading = false }: FacilityTypeFormProps) => {
  const isEdit = facilityType !== null

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const formData = new FormData(e.currentTarget)

    const facilityTypeData: FacilityTypeRequest = {
      name: formData.get('name')?.toString() || '',
      description: formData.get('description')?.toString() || '',
      defaultAmenities: formData.get('defaultAmenities')?.toString() || '',
      defaultCapacity: Number(formData.get('defaultCapacity')) || 0,
      typicalDurationHours: Number(formData.get('typicalDurationHours')) || 0,
      iconUrl: formData.get('iconUrl')?.toString() || '',
    }

    // Validation
    if (facilityTypeData.defaultCapacity <= 0) {
      alert('Sức chứa mặc định phải lớn hơn 0')
      return
    }

    if (facilityTypeData.typicalDurationHours < 0) {
      alert('Thời lượng điển hình không được âm')
      return
    }

    try {
      await onSave(facilityTypeData)
    } catch (error) {
      // Error đã được xử lý trong parent component
      console.error('Error saving facility type:', error)
    }
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
      <div className="w-full max-w-2xl rounded-lg border border-gray-200 bg-white shadow-xl">
        <div className="flex items-center justify-between border-b border-gray-200 p-6">
          <h2 className="text-xl font-semibold text-gray-900">
            {isEdit ? 'Chỉnh sửa loại cơ sở vật chất' : 'Thêm loại cơ sở vật chất mới'}
          </h2>
          <button
            onClick={onClose}
            disabled={loading}
            className="rounded-lg p-1 text-gray-400 hover:bg-gray-100 hover:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <X className="h-5 w-5" />
          </button>
        </div>

        <form onSubmit={handleSubmit} className="p-6">
          <div className="space-y-4">
            <div>
              <label className="mb-2 block text-sm font-semibold text-gray-700">
                <Building2 className="mr-1 inline h-4 w-4" />
                Tên loại cơ sở vật chất <span className="text-red-500">*</span>
              </label>
              <input
                type="text"
                name="name"
                defaultValue={facilityType?.name || ''}
                required
                disabled={loading}
                placeholder="VD: Phòng họp, Phòng Lab"
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
              />
            </div>

            <div>
              <label className="mb-2 block text-sm font-semibold text-gray-700">
                <FileText className="mr-1 inline h-4 w-4" />
                Mô tả
              </label>
              <textarea
                name="description"
                defaultValue={facilityType?.description || ''}
                rows={3}
                disabled={loading}
                placeholder="Mô tả về loại cơ sở vật chất này"
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
              />
            </div>

            <div className="grid gap-4 sm:grid-cols-2">
              <div>
                <label className="mb-2 block text-sm font-semibold text-gray-700">
                  <Users className="mr-1 inline h-4 w-4" />
                  Sức chứa mặc định <span className="text-red-500">*</span>
                </label>
                <input
                  type="number"
                  name="defaultCapacity"
                  defaultValue={facilityType?.defaultCapacity || ''}
                  min="1"
                  required
                  disabled={loading}
                  className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                />
                <p className="mt-1 text-xs text-gray-500">Số người mặc định cho loại facility này</p>
              </div>

              <div>
                <label className="mb-2 block text-sm font-semibold text-gray-700">
                  <Clock className="mr-1 inline h-4 w-4" />
                  Thời lượng điển hình (giờ)
                </label>
                <input
                  type="number"
                  name="typicalDurationHours"
                  defaultValue={facilityType?.typicalDurationHours || ''}
                  min="0"
                  step="0.5"
                  disabled={loading}
                  className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
                />
                <p className="mt-1 text-xs text-gray-500">Thời lượng booking điển hình (có thể để 0)</p>
              </div>
            </div>

            <div>
              <label className="mb-2 block text-sm font-semibold text-gray-700">Tiện ích mặc định</label>
              <input
                type="text"
                name="defaultAmenities"
                defaultValue={facilityType?.defaultAmenities || ''}
                disabled={loading}
                placeholder="VD: Máy chiếu, WiFi, Điều hòa (phân cách bằng dấu phẩy)"
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
              />
              <p className="mt-1 text-xs text-gray-500">Các tiện ích mặc định cho loại facility này</p>
            </div>

            <div>
              <label className="mb-2 block text-sm font-semibold text-gray-700">
                <Image className="mr-1 inline h-4 w-4" />
                Icon URL
              </label>
              <input
                type="url"
                name="iconUrl"
                defaultValue={facilityType?.iconUrl || ''}
                disabled={loading}
                placeholder="https://example.com/icon.png"
                className="w-full rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1 disabled:bg-gray-100 disabled:cursor-not-allowed"
              />
              <p className="mt-1 text-xs text-gray-500">URL của icon cho loại facility này (optional)</p>
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
              disabled={loading}
              className="flex items-center gap-2 rounded-lg bg-orange-500 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {loading && <Loader2 className="h-4 w-4 animate-spin" />}
              {isEdit ? 'Cập nhật' : 'Tạo mới'}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}

export default FacilityTypeForm

