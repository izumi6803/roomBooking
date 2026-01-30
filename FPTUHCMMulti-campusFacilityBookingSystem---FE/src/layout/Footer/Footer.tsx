import { Link } from 'react-router-dom'

const Footer = () => {
  const year = new Date().getFullYear()

  return (
    <footer className="mt-10 border-t bg-gray-900 text-gray-300">
      <div className="mx-auto max-w-6xl px-4 py-8">
        <div className="grid gap-8 md:grid-cols-3">
          <div className="space-y-3 text-sm">
            <p className="text-xs font-semibold uppercase tracking-[0.2em] text-orange-400">
              Về chúng tôi
            </p>
            <p className="text-sm font-semibold text-white">FPTU Multi‑campus Facility Booking</p>
            <p className="text-xs text-gray-400">
              Một hệ thống đặt phòng thông minh được thiết kế cho sinh viên và giảng viên để khám phá, yêu cầu và quản lý việc sử dụng cơ sở vật chất trên các campus HCM & NVH. Rút ngắn thời gian học tập, giảng dạy và lập kế hoạch sự kiện với sự sẵn sàng thời gian thực và lập lịch thông minh.
            </p>
          </div>

          <div className="space-y-3 text-xs">
            <p className="text-[11px] font-semibold uppercase tracking-wide text-gray-400">Điều hướng</p>
            <ul className="space-y-2">
              <li>
                <Link to="/" className="hover:text-orange-400 transition-colors">
                  Trang chủ
                </Link>
              </li>
              <li>
                <Link to="/user/facilities" className="hover:text-orange-400 transition-colors">
                  Cơ sở vật chất
                </Link>
              </li>
            </ul>
          </div>

          <div className="space-y-3 text-xs">
            <p className="text-[11px] font-semibold uppercase tracking-wide text-gray-400">Liên hệ chúng tôi</p>
            <div className="space-y-2 text-gray-400">
              <p>
                <span className="font-semibold text-gray-300">Campus Khu Công Nghệ Cao:</span> phường Tân Phú, Thành phố Thủ Đức, TPHCM
              </p>
              <p>
                <span className="font-semibold text-gray-300">Campus NVH:</span> Nhà văn hóa Sinh viên, phường Dĩ An, TPHCM
              </p>
              <p className="pt-2 text-gray-500">
                Để được hỗ trợ đặt phòng hoặc yêu cầu về cơ sở vật chất, vui lòng liên hệ văn phòng cơ sở vật chất hoặc tuân theo hướng dẫn của campus
                guidelines.
              </p>
            </div>
          </div>
        </div>

        <div className="mt-6 border-t border-gray-800 pt-4 text-[11px] text-gray-500">
          <p>
            © {year} FPT University HCMC. All rights reserved. Facility Booking System v1.0 – designed for students &
            lecturers of multi‑campus HCM.
          </p>
        </div>
      </div>
    </footer>
  )
}

export default Footer


