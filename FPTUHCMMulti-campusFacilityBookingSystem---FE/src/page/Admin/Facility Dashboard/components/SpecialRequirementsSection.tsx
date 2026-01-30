import { FileText } from 'lucide-react'

interface SpecialRequirementsSectionProps {
  specialRequirements: Record<string, boolean | string | number | string[]>
}

/**
 * SpecialRequirementsSection Component - Hiển thị yêu cầu đặc biệt từ booking
 * 
 * @description Component này hiển thị các yêu cầu đặc biệt mà người dùng đã yêu cầu khi đặt phòng.
 * Backend có thể lưu dưới dạng:
 *   - JSON object: {"projector": true, "whiteboard": true, "microphone": false}
 *   - Plain text: {"note": "Cần máy chiếu và bảng trắng"}
 * 
 * Format hiển thị:
 *   - Boolean: "Có" hoặc "Không"
 *   - Array: Join bằng dấu phẩy
 *   - String/Number: Hiển thị giá trị
 *   - Key name: Capitalize và thay "_" bằng space
 * 
 * @param specialRequirements - Object chứa các key-value của yêu cầu đặc biệt
 * 
 * @example
 * // JSON từ backend: {"projector": true, "microphone": false, "notes": "Cần setup sớm"}
 * <SpecialRequirementsSection specialRequirements={{
 *   projector: true,
 *   microphone: false,
 *   notes: "Cần setup sớm"
 * }} />
 */
export const SpecialRequirementsSection = ({
  specialRequirements,
}: SpecialRequirementsSectionProps) => {
  // Nếu không có yêu cầu đặc biệt → không render
  if (!specialRequirements || Object.keys(specialRequirements).length === 0) {
    return null
  }

  return (
    <div className="rounded-lg border border-gray-200 bg-gray-50 p-3">
      <div className="mb-2 flex items-center gap-2">
        <FileText className="h-4 w-4 text-gray-400" />
        <p className="text-xs font-semibold text-gray-700">Yêu cầu đặc biệt</p>
      </div>
      <ul className="space-y-1">
        {Object.entries(specialRequirements).map(([key, value]) => (
          <li key={key} className="text-xs text-gray-600">
            <span className="font-medium capitalize">{key.replace(/_/g, ' ')}:</span>{' '}
            {typeof value === 'boolean'
              ? value
                ? 'Có'
                : 'Không'
              : Array.isArray(value)
                ? value.join(', ')
                : String(value)}
          </li>
        ))}
      </ul>
    </div>
  )
}
