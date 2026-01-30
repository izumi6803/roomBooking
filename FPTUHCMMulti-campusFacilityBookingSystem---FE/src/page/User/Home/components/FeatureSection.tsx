/**
 * FeatureSection Component - Phần giới thiệu các tính năng chính
 * 
 * Component này hiển thị 4 cards mô tả các tính năng chính của hệ thống:
 * 1. Lên kế hoạch theo phòng thực tế
 * 2. Chọn đúng campus và không gian
 * 3. Tránh xung đột thời gian
 * 4. Cập nhật thông tin từ một nơi
 * 
 * Mỗi card có:
 * - Icon từ lucide-react
 * - Tiêu đề (title)
 * - Mô tả (description)
 * 
 * Component sử dụng:
 * - lucide-react icons cho các icon đẹp
 * - Tailwind CSS cho styling
 * - Grid layout responsive (2 cột trên desktop, 1 cột trên mobile)
 */

// Import các icon từ lucide-react library
// lucide-react là thư viện icon đẹp, nhẹ và dễ sử dụng
import { CalendarRange, MapPin, Clock, Bell } from 'lucide-react'

/**
 * Array chứa dữ liệu cho các feature cards
 * 
 * Mỗi object trong array đại diện cho một feature card với:
 * - title: Tiêu đề của tính năng
 * - description: Mô tả chi tiết về tính năng
 * - icon: Component icon từ lucide-react (sẽ được render như một React component)
 * 
 * Lưu ý: icon được đặt tên là Icon (với I viết hoa) vì trong React,
 * component phải bắt đầu bằng chữ hoa
 */
const cards = [
  {
    title: 'Lên kế hoạch theo phòng thực tế',
    description: 'Nhanh chóng xem phòng học, phòng lab hoặc sân thể thao nào còn trống trước khi bạn lên lịch.',
    icon: CalendarRange, // Icon lịch để đại diện cho việc lên kế hoạch
  },
  {
    title: 'Chọn đúng campus và không gian',
    description: 'Lọc theo campus HCM hoặc NVH và tập trung vào các cơ sở vật chất phù hợp với nhu cầu giảng dạy hoặc học tập của bạn.',
    icon: MapPin, // Icon địa điểm để đại diện cho việc chọn campus
  },
  {
    title: 'Tránh xung đột thời gian',
    description: 'Hệ thống làm nổi bật các khung giờ trống để bạn không đến phòng đã được đặt.',
    icon: Clock, // Icon đồng hồ để đại diện cho việc quản lý thời gian
  },
  {
    title: 'Cập nhật thông tin từ một nơi',
    description: 'Từ trang chủ, bạn có thể xem các đặt phòng sắp tới và thông báo quan trọng về cơ sở vật chất của bạn.',
    icon: Bell, // Icon chuông để đại diện cho thông báo
  },
]

/**
 * FeatureSection Component Function
 * 
 * Functional component không nhận props
 * Render danh sách các feature cards từ array cards
 * 
 * @returns {JSX.Element} - JSX element chứa section giới thiệu tính năng
 */
const FeatureSection = () => {
  return (
    // Section chính
    // bg-white: Background trắng
    // py-12: Padding trên-dưới 12 units (48px)
    <section className="bg-white py-12">
      {/* Container chính
          mx-auto: Căn giữa theo chiều ngang
          max-w-6xl: Chiều rộng tối đa 6xl (1152px)
          px-4: Padding trái-phải 4 units (16px) */}
      <div className="mx-auto max-w-6xl px-4">
        {/* Header section với tiêu đề và mô tả
            mb-8: Margin bottom 8 units (32px) để tạo khoảng cách với grid
            text-center: Căn giữa text */}
        <div className="mb-8 text-center">
          {/* Tiêu đề chính
              text-xl: Font size xl (20px) mặc định
              font-bold: Font weight bold (700)
              text-gray-900: Màu xám rất đậm (gần đen)
              sm:text-2xl: Trên màn hình small, font size 2xl (24px) */}
          <h2 className="text-xl font-bold text-gray-900 sm:text-2xl">Những gì chúng tôi mang lại</h2>
          
          {/* Mô tả phụ
              mt-2: Margin top 2 units (8px) để tạo khoảng cách với tiêu đề
              text-sm: Font size nhỏ (14px)
              text-gray-600: Màu xám vừa phải */}
          <p className="mt-2 text-sm text-gray-600">
            Là sinh viên hoặc giảng viên, trang này là điểm khởi đầu để bạn hiểu về campus, tìm đúng
            cơ sở vật chất và hành động nhanh chóng.
          </p>
        </div>

        {/* Grid container cho các feature cards
            grid: Sử dụng CSS Grid layout
            gap-5: Khoảng cách giữa các grid items là 5 units (20px)
            md:grid-cols-2: Trên màn hình medium trở lên, hiển thị 2 cột
            Mặc định (mobile): 1 cột */}
        <div className="grid gap-5 md:grid-cols-2">
          {/* Map qua array cards để render từng card
              cards.map(): Duyệt qua từng phần tử trong array cards
              ({ title, description, icon: Icon }): Destructuring để lấy các properties
              icon: Icon: Đổi tên icon thành Icon (với I viết hoa) vì React component phải viết hoa chữ cái đầu
              key={title}: Key prop bắt buộc trong React để optimize re-rendering
              title được dùng làm key vì nó unique cho mỗi card */}
          {cards.map(({ title, description, icon: Icon }) => (
            // Article element cho mỗi feature card
            // Semantic HTML: <article> dùng cho nội dung độc lập
            <article
              key={title} // Key prop để React track element trong list
              // Styling cho card
              // flex: Sử dụng flexbox
              // gap-4: Khoảng cách giữa các flex items là 4 units (16px)
              // rounded-xl: Bo góc lớn (12px)
              // border: Có border
              // border-gray-100: Màu border xám rất nhạt
              // bg-gradient-to-br: Background gradient từ trên-trái đến dưới-phải
              // from-gray-50 to-white: Gradient từ xám rất nhạt đến trắng
              // p-5: Padding tất cả các phía 5 units (20px)
              // shadow-sm: Shadow nhỏ để tạo độ sâu
              className="flex gap-4 rounded-xl border border-gray-100 bg-gradient-to-br from-gray-50 to-white p-5 shadow-sm"
            >
              {/* Container cho icon
                  mt-1: Margin top 1 unit (4px) để căn chỉnh với text
                  flex: Sử dụng flexbox
                  h-10 w-10: Chiều cao và rộng 10 units (40px) - hình vuông
                  items-center: Căn items vào giữa theo cross-axis
                  justify-center: Căn items vào giữa theo main-axis
                  rounded-full: Bo tròn hoàn toàn (hình tròn)
                  bg-orange-500/10: Background cam với độ trong suốt 10%
                  text-orange-600: Màu icon là cam đậm */}
              <div className="mt-1 flex h-10 w-10 items-center justify-center rounded-full bg-orange-500/10 text-orange-600">
                {/* Render icon component
                    Icon: Component icon từ lucide-react (CalendarRange, MapPin, Clock, hoặc Bell)
                    className="h-5 w-5": Kích thước icon 5x5 units (20px x 20px) */}
                <Icon className="h-5 w-5" />
              </div>
              
              {/* Container cho text content (title và description) */}
              <div>
                {/* Tiêu đề của feature
                    mb-1: Margin bottom 1 unit (4px) để tạo khoảng cách với description
                    text-sm: Font size nhỏ (14px)
                    font-semibold: Font weight semi-bold (600)
                    text-gray-900: Màu xám rất đậm */}
                <h3 className="mb-1 text-sm font-semibold text-gray-900">{title}</h3>
                
                {/* Mô tả của feature
                    text-xs: Font size rất nhỏ (12px)
                    text-gray-600: Màu xám vừa phải */}
                <p className="text-xs text-gray-600">{description}</p>
              </div>
            </article>
          ))}
        </div>
      </div>
    </section>
  )
}

// Export component để có thể import ở nơi khác
export default FeatureSection


