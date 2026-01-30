/**
 * SystemSetting Component - Cấu hình hệ thống
 * 
 * Component này cho phép admin cấu hình các thông số thời gian của hệ thống:
 * - minimumBookingHoursBeforeStart: Thời gian tối thiểu trước khi bắt đầu booking (giờ)
 * - checkInMinutesBeforeStart: Số phút cho phép check-in trước thời gian bắt đầu
 * - checkInMinutesAfterStart: Số phút cho phép check-in sau thời gian bắt đầu
 * - checkOutMinutesAfterCheckIn: Thời gian tối thiểu sau khi check-in để được check-out (phút)
 * - checkoutMinRatio: Tỷ lệ tối thiểu để được check-out (%)
 * 
 * Tính năng:
 * - Load settings từ API khi component mount
 * - Track changes: Phát hiện thay đổi so với giá trị ban đầu
 * - Validation: Tất cả giá trị phải >= 0
 * - Save changes: Lưu cấu hình mới
 * - Reset changes: Hủy thay đổi và quay về giá trị ban đầu
 * - Visual feedback: Hiển thị trạng thái "Có thay đổi chưa lưu" hoặc "Đã lưu thành công"
 */

// Import React hooks
import { useState, useEffect } from 'react'
// Import icons
import { Settings, Save, Loader2, AlertCircle, CheckCircle2 } from 'lucide-react'
// Import API functions và types
import { getSystemSettings, updateSystemSettings, type SystemSettings } from './api/systemSettingApi'
// Import toast hook
import { useToast } from '../../../components/toast'

/**
 * SystemSetting Component Function
 * 
 * Component để quản lý cấu hình hệ thống
 * Không nhận props (self-contained)
 * 
 * @returns {JSX.Element} - JSX element chứa form cấu hình hệ thống
 */
const SystemSetting = () => {
  const { showSuccess, showError } = useToast()
  
  const [settings, setSettings] = useState<SystemSettings>({
    minimumBookingHoursBeforeStart: 3,
    checkInMinutesBeforeStart: 15,
    checkInMinutesAfterStart: 15,
    checkoutMinRatio: 0,
    checkOutMinutesAfterCheckIn: 0,
  })
  
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [hasChanges, setHasChanges] = useState(false)
  const [originalSettings, setOriginalSettings] = useState<SystemSettings | null>(null)

  // Load system settings
  useEffect(() => {
    const loadSettings = async () => {
      setLoading(true)
      setError(null)
      
      try {
        const response = await getSystemSettings()
        if (response.success && response.data) {
          setSettings(response.data)
          setOriginalSettings(response.data)
        } else {
          setError(response.error?.message || 'Không thể tải cấu hình hệ thống')
        }
      } catch (err) {
        const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi tải cấu hình hệ thống'
        setError(errorMessage)
        showError(errorMessage)
      } finally {
        setLoading(false)
      }
    }

    loadSettings()
  }, [showError])

  // Check for changes
  useEffect(() => {
    if (originalSettings) {
      const changed = 
        settings.minimumBookingHoursBeforeStart !== originalSettings.minimumBookingHoursBeforeStart ||
        settings.checkInMinutesBeforeStart !== originalSettings.checkInMinutesBeforeStart ||
        settings.checkInMinutesAfterStart !== originalSettings.checkInMinutesAfterStart ||
        settings.checkoutMinRatio !== originalSettings.checkoutMinRatio ||
        settings.checkOutMinutesAfterCheckIn !== originalSettings.checkOutMinutesAfterCheckIn
      setHasChanges(changed)
    }
  }, [settings, originalSettings])

  // Handle input change
  const handleChange = (field: keyof SystemSettings, value: number) => {
    // Validate: value must be >= 0
    if (value < 0) {
      return
    }
    
    setSettings((prev) => ({
      ...prev,
      [field]: value,
    }))
  }

  // Handle save
  const handleSave = async () => {
    // Validate all values
    if (
      settings.minimumBookingHoursBeforeStart < 0 ||
      settings.checkInMinutesBeforeStart < 0 ||
      settings.checkInMinutesAfterStart < 0 ||
      settings.checkoutMinRatio < 0 ||
      settings.checkOutMinutesAfterCheckIn < 0
    ) {
      showError('Tất cả các giá trị phải lớn hơn hoặc bằng 0')
      return
    }

    setSaving(true)
    setError(null)

    try {
      const response = await updateSystemSettings({
        minimumBookingHoursBeforeStart: settings.minimumBookingHoursBeforeStart,
        checkInMinutesBeforeStart: settings.checkInMinutesBeforeStart,
        checkInMinutesAfterStart: settings.checkInMinutesAfterStart,
        checkoutMinRatio: settings.checkoutMinRatio,
        checkOutMinutesAfterCheckIn: settings.checkOutMinutesAfterCheckIn,
      })

      if (response.success && response.data) {
        setSettings(response.data)
        setOriginalSettings(response.data)
        setHasChanges(false)
        showSuccess('Cập nhật cấu hình hệ thống thành công!')
      } else {
        const errorMsg = response.error?.message || 'Không thể cập nhật cấu hình hệ thống'
        setError(errorMsg)
        showError(errorMsg)
      }
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Đã xảy ra lỗi khi cập nhật cấu hình hệ thống'
      setError(errorMessage)
      showError(errorMessage)
    } finally {
      setSaving(false)
    }
  }

  // Handle reset
  const handleReset = () => {
    if (originalSettings) {
      setSettings(originalSettings)
      setHasChanges(false)
    }
  }

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50 p-6">
        <div className="mx-auto max-w-4xl">
          <div className="flex flex-col items-center justify-center rounded-lg border border-gray-200 bg-white p-12">
            <Loader2 className="h-8 w-8 animate-spin text-orange-500" />
            <p className="mt-4 text-sm text-gray-600">Đang tải cấu hình hệ thống...</p>
          </div>
        </div>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <div className="mx-auto max-w-4xl">
        {/* Header */}
        <div className="mb-6">
          <div className="flex items-center gap-3 mb-2">
            <Settings className="h-8 w-8 text-orange-500" />
            <h1 className="text-3xl font-bold text-gray-900">Cấu hình hệ thống</h1>
          </div>
          <p className="text-gray-600">Quản lý và điều chỉnh các cấu hình thời gian của hệ thống</p>
        </div>

        {/* Error State */}
        {error && (
          <div className="mb-6 rounded-lg border border-red-200 bg-red-50 p-4">
            <div className="flex items-center gap-3">
              <AlertCircle className="h-5 w-5 text-red-600 flex-shrink-0" />
              <div>
                <h3 className="text-sm font-semibold text-red-800">Lỗi</h3>
                <p className="mt-1 text-sm text-red-600">{error}</p>
              </div>
            </div>
          </div>
        )}

        {/* Settings Form */}
        <div className="rounded-lg border border-gray-200 bg-white shadow-sm">
          <div className="border-b border-gray-200 p-6">
            <h2 className="text-lg font-semibold text-gray-900">Cấu hình thời gian</h2>
            <p className="mt-1 text-sm text-gray-600">
              Điều chỉnh các thông số thời gian cho hoạt động booking và check-in
            </p>
          </div>

          <div className="p-6 space-y-6">
            {/* Minimum Booking Hours Before Start */}
            <div>
              <label
                htmlFor="minimumBookingHours"
                className="block text-sm font-semibold text-gray-900 mb-2"
              >
                Thời gian tối thiểu trước khi bắt đầu booking (giờ)
              </label>
              <p className="text-xs text-gray-500 mb-3">
                Thời gian tối thiểu (giờ) mà người dùng phải đặt trước khi bắt đầu booking
              </p>
              <div className="flex items-center gap-4">
                <input
                  type="number"
                  id="minimumBookingHours"
                  min="0"
                  step="1"
                  value={settings.minimumBookingHoursBeforeStart}
                  onChange={(e) => handleChange('minimumBookingHoursBeforeStart', parseInt(e.target.value) || 0)}
                  className="w-32 rounded-lg border border-gray-300 px-4 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                />
                <span className="text-sm text-gray-600">giờ</span>
              </div>
            </div>

            {/* Check-in Minutes Before Start */}
            <div>
              <label
                htmlFor="checkInBeforeStart"
                className="block text-sm font-semibold text-gray-900 mb-2"
              >
                Số phút cho phép check-in trước thời gian bắt đầu
              </label>
              <p className="text-xs text-gray-500 mb-3">
                Số phút trước thời gian bắt đầu mà người dùng có thể check-in
              </p>
              <div className="flex items-center gap-4">
                <input
                  type="number"
                  id="checkInBeforeStart"
                  min="0"
                  step="1"
                  value={settings.checkInMinutesBeforeStart}
                  onChange={(e) => handleChange('checkInMinutesBeforeStart', parseInt(e.target.value) || 0)}
                  className="w-32 rounded-lg border border-gray-300 px-4 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                />
                <span className="text-sm text-gray-600">phút</span>
              </div>
            </div>

            {/* Check-in Minutes After Start */}
            <div>
              <label
                htmlFor="checkInAfterStart"
                className="block text-sm font-semibold text-gray-900 mb-2"
              >
                Số phút cho phép check-in sau thời gian bắt đầu
              </label>
              <p className="text-xs text-gray-500 mb-3">
                Số phút sau thời gian bắt đầu mà người dùng vẫn có thể check-in
              </p>
              <div className="flex items-center gap-4">
                <input
                  type="number"
                  id="checkInAfterStart"
                  min="0"
                  step="1"
                  value={settings.checkInMinutesAfterStart}
                  onChange={(e) => handleChange('checkInMinutesAfterStart', parseInt(e.target.value) || 0)}
                  className="w-32 rounded-lg border border-gray-300 px-4 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                />
                <span className="text-sm text-gray-600">phút</span>
              </div>
            </div>

            {/* Check-out Minutes After Check-in */}
            <div>
              <label
                htmlFor="checkOutAfterCheckIn"
                className="block text-sm font-semibold text-gray-900 mb-2"
              >
                Thời gian tối thiểu sau khi check-in để được check-out (phút)
              </label>
              <p className="text-xs text-gray-500 mb-3">
                Số phút tối thiểu sau khi check-in mà người dùng mới được phép check-out (mặc định: 0 - có thể check-out ngay sau khi check-in)
              </p>
              <div className="flex items-center gap-4">
                <input
                  type="number"
                  id="checkOutAfterCheckIn"
                  min="0"
                  step="1"
                  value={settings.checkOutMinutesAfterCheckIn}
                  onChange={(e) => handleChange('checkOutMinutesAfterCheckIn', parseInt(e.target.value) || 0)}
                  className="w-32 rounded-lg border border-gray-300 px-4 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                />
                <span className="text-sm text-gray-600">phút</span>
              </div>
            </div>

            {/* Checkout Min Ratio */}
            <div>
              <label
                htmlFor="checkoutMinRatio"
                className="block text-sm font-semibold text-gray-900 mb-2"
              >
                Thời lượng tối thiểu để được check-out
              </label>
              <p className="text-xs text-gray-500 mb-3">
                Thời lượng tối thiểu (phút) thời lượng booking phải được sử dụng để cho phép check-out.
              </p>
              <div className="flex items-center gap-4">
                <input
                  type="number"
                  id="checkoutMinRatio"
                  min="0"
                  step="1"
                  value={settings.checkoutMinRatio}
                  onChange={(e) => handleChange('checkoutMinRatio', parseInt(e.target.value) || 0)}
                  className="w-32 rounded-lg border border-gray-300 px-4 py-2 text-sm outline-none ring-orange-500 focus:border-orange-400 focus:ring-1"
                />
                <span className="text-sm text-gray-600">phút</span>
              </div>
            </div>
          </div>

          {/* Action Buttons */}
          <div className="border-t border-gray-200 bg-gray-50 px-6 py-4">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-2">
                {hasChanges && (
                  <span className="text-xs text-orange-600 flex items-center gap-1">
                    <AlertCircle className="h-4 w-4" />
                    Có thay đổi chưa lưu
                  </span>
                )}
                {!hasChanges && originalSettings && (
                  <span className="text-xs text-green-600 flex items-center gap-1">
                    <CheckCircle2 className="h-4 w-4" />
                    Đã lưu thành công
                  </span>
                )}
              </div>
              <div className="flex items-center gap-3">
                {hasChanges && (
                  <button
                    onClick={handleReset}
                    disabled={saving}
                    className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-semibold text-gray-700 hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    Hủy thay đổi
                  </button>
                )}
                <button
                  onClick={handleSave}
                  disabled={saving || !hasChanges}
                  className="flex items-center gap-2 rounded-lg bg-orange-600 px-6 py-2 text-sm font-semibold text-white shadow-sm hover:bg-orange-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  {saving ? (
                    <>
                      <Loader2 className="h-4 w-4 animate-spin" />
                      Đang lưu...
                    </>
                  ) : (
                    <>
                      <Save className="h-4 w-4" />
                      Lưu thay đổi
                    </>
                  )}
                </button>
              </div>
            </div>
          </div>
        </div>

        {/* Info Box */}
        <div className="mt-6 rounded-lg border border-blue-200 bg-blue-50 p-4">
          <div className="flex items-start gap-3">
            <AlertCircle className="h-5 w-5 text-blue-600 flex-shrink-0 mt-0.5" />
            <div className="text-sm text-blue-800">
              <p className="font-semibold mb-1">Lưu ý:</p>
              <ul className="list-disc list-inside space-y-1 text-blue-700">
                <li>Tất cả các giá trị phải lớn hơn hoặc bằng 0</li>
                <li>Thay đổi sẽ có hiệu lực ngay sau khi lưu</li>
                <li>Chỉ cần gửi các field muốn cập nhật (các field khác giữ nguyên)</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default SystemSetting
