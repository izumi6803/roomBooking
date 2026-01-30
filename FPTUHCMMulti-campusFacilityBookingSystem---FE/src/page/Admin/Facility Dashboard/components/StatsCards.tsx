import { Clock } from 'lucide-react'

interface StatsCardsProps {
  total: number
  hcm: number
  nvh: number
  meetingRooms: number
}

const StatsCards = ({ total, hcm, nvh, meetingRooms }: StatsCardsProps) => {
  return (
    <div className="mb-6 grid gap-4 md:grid-cols-4">
      <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
        <div className="flex items-center gap-2 text-sm text-gray-600">
          <Clock className="h-4 w-4 text-orange-500" />
          <span>Yêu cầu đang chờ</span>
        </div>
        <p className="mt-1 text-2xl font-bold text-orange-600">{total}</p>
      </div>
      <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
        <div className="text-sm text-gray-600">Campus HCM</div>
        <p className="mt-1 text-2xl font-bold text-gray-900">{hcm}</p>
      </div>
      <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
        <div className="text-sm text-gray-600">Campus NVH</div>
        <p className="mt-1 text-2xl font-bold text-gray-900">{nvh}</p>
      </div>
      <div className="rounded-lg border border-gray-200 bg-white p-4 shadow-sm">
        <div className="text-sm text-gray-600">Phòng họp</div>
        <p className="mt-1 text-2xl font-bold text-purple-600">{meetingRooms}</p>
      </div>
    </div>
  )
}

export default StatsCards

