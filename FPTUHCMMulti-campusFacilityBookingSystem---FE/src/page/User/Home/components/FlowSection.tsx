/**
 * FlowSection Component - Phần mô tả quy trình đặt phòng
 * 
 * Component này hiển thị 6 bước trong quy trình đặt phòng:
 * 1. Đăng nhập
 * 2. Trang chủ & chọn campus
 * 3. Chọn cơ sở vật chất & thời gian
 * 4. Chi tiết cơ sở & đặt phòng
 * 5. Phê duyệt của quản trị viên
 * 6. Quay lại trang chủ
 * 
 * Mỗi step card hiển thị:
 * - Số thứ tự bước (1-6)
 * - Category label (Bắt đầu / Đặt phòng / Kết quả)
 * - Tiêu đề bước
 * - Mô tả chi tiết
 * 
 * Component sử dụng:
 * - Grid layout responsive (3 cột trên desktop, 1 cột trên mobile)
 * - Hover effects để tăng tương tác
 * - Conditional rendering cho category labels
 */

/**
 * Array chứa dữ liệu cho các bước trong quy trình
 * 
 * Mỗi object đại diện cho một bước với:
 * - id: Số thứ tự bước (1-6)
 * - title: Tiêu đề ngắn gọn của bước
 * - description: Mô tả chi tiết về bước đó
 */
const steps = [
  {
    id: 1, // Bước 1
    title: 'Đăng nhập',
    description: 'Đăng nhập bằng tài khoản đại học (K19+) hoặc email FPT cho sinh viên và giảng viên K18.',
  },
  {
    id: 2, // Bước 2
    title: 'Trang chủ & chọn campus',
    description: 'Bắt đầu từ trang chủ, chọn campus của bạn (HCM hoặc NVH) để xem các cơ sở vật chất liên quan.',
  },
  {
    id: 3, // Bước 3
    title: 'Chọn cơ sở vật chất & thời gian',
    description: 'Duyệt các phòng học, phòng lab và sân thể thao với các khung giờ trống theo thời gian thực.',
  },
  {
    id: 4, // Bước 4
    title: 'Chi tiết cơ sở & đặt phòng',
    description: 'Xem xét sức chứa phòng, thiết bị và quy định, sau đó gửi yêu cầu đặt phòng.',
  },
  {
    id: 5, // Bước 5
    title: 'Phê duyệt của quản trị viên',
    description: 'Quản trị viên cơ sở vật chất duyệt hoặc từ chối yêu cầu của bạn, dựa trên chính sách và xung đột.',
  },
  {
    id: 6, // Bước 6
    title: 'Quay lại trang chủ',
    description: 'Nhận thông báo trên trang chủ về trạng thái đặt phòng và việc sử dụng sắp tới.',
  },
]

/**
 * FlowSection Component Function
 * 
 * Functional component không nhận props
 * Render danh sách các bước trong quy trình đặt phòng
 * 
 * @returns {JSX.Element} - JSX element chứa section mô tả quy trình
 */
const FlowSection = () => {
  return (
    // Section chính
    // bg-gray-50: Background xám rất nhạt
    // py-10: Padding trên-dưới 10 units (40px)
    <section className="bg-gray-50 py-10">
      {/* Container chính
          mx-auto: Căn giữa theo chiều ngang
          max-w-6xl: Chiều rộng tối đa 6xl (1152px)
          px-4: Padding trái-phải 4 units (16px) */}
      <div className="mx-auto max-w-6xl px-4">
        {/* Header section với tiêu đề và mô tả
            mb-6: Margin bottom 6 units (24px) để tạo khoảng cách với grid
            flex: Sử dụng flexbox
            flex-col: Hướng flex là column (dọc) trên mobile
            gap-3: Khoảng cách giữa các flex items là 3 units (12px)
            sm:flex-row: Trên màn hình small, hướng flex là row (ngang)
            sm:items-end: Trên màn hình small, căn items về cuối (bottom)
            sm:justify-between: Trên màn hình small, khoảng cách giữa các items là space-between */}
        <div className="mb-6 flex flex-col gap-3 sm:flex-row sm:items-end sm:justify-between">
          {/* Container cho tiêu đề chính và mô tả */}
          <div>
            {/* Tiêu đề chính
                text-xl: Font size xl (20px) mặc định
                font-bold: Font weight bold (700)
                text-gray-900: Màu xám rất đậm
                sm:text-2xl: Trên màn hình small, font size 2xl (24px) */}
            <h2 className="text-xl font-bold text-gray-900 sm:text-2xl">Quy trình đặt phòng cho sinh viên & giảng viên</h2>
            
            {/* Mô tả phụ
                mt-1: Margin top 1 unit (4px)
                text-sm: Font size nhỏ (14px)
                text-gray-600: Màu xám vừa phải */}
            <p className="mt-1 text-sm text-gray-600">
              Từ lần đăng nhập đầu tiên đến đặt phòng được xác nhận, hệ thống hướng dẫn bạn từng bước.
            </p>
          </div>
          
          {/* Note phụ ở bên phải (chỉ hiển thị trên desktop)
              max-w-xs: Chiều rộng tối đa xs (320px)
              text-xs: Font size rất nhỏ (12px)
              text-gray-500: Màu xám nhạt */}
          <p className="max-w-xs text-xs text-gray-500">
            Quy trình này được chia sẻ cho cả campus HCM và NVH, đảm bảo trải nghiệm nhất quán trên tất cả địa điểm.
          </p>
        </div>

        {/* Grid container cho các step cards
            grid: Sử dụng CSS Grid layout
            gap-4: Khoảng cách giữa các grid items là 4 units (16px)
            md:grid-cols-3: Trên màn hình medium trở lên, hiển thị 3 cột
            Mặc định (mobile): 1 cột */}
        <div className="grid gap-4 md:grid-cols-3">
          {/* Map qua array steps để render từng step card
              steps.map(): Duyệt qua từng phần tử trong array steps
              (step): Mỗi phần tử được gọi là step
              key={step.id}: Key prop bắt buộc, dùng id vì nó unique */}
          {steps.map((step) => (
            // Container cho mỗi step card
            <div
              key={step.id} // Key prop để React track element
              // Styling cho card với hover effects
              // group: Cho phép child elements sử dụng group-hover: (không dùng ở đây nhưng có thể dùng sau)
              // relative: Để các element con có thể dùng absolute positioning
              // overflow-hidden: Ẩn phần nội dung tràn ra ngoài
              // rounded-xl: Bo góc lớn (12px)
              // border: Có border
              // border-orange-100: Màu border cam nhạt
              // bg-white/80: Background trắng với độ trong suốt 80%
              // p-4: Padding tất cả các phía 4 units (16px)
              // shadow-sm: Shadow nhỏ
              // transition: Thêm transition cho các thay đổi (smooth animation)
              // hover:-translate-y-0.5: Khi hover, dịch lên trên 0.5 units (2px) - hiệu ứng nổi lên
              // hover:border-orange-300: Khi hover, border đậm hơn (cam vừa)
              // hover:shadow-md: Khi hover, shadow lớn hơn - tạo hiệu ứng nổi lên rõ ràng hơn
              className="group relative overflow-hidden rounded-xl border border-orange-100 bg-white/80 p-4 shadow-sm transition hover:-translate-y-0.5 hover:border-orange-300 hover:shadow-md"
            >
              {/* Container cho số bước và category label
                  mb-3: Margin bottom 3 units (12px) để tạo khoảng cách với content
                  flex: Sử dụng flexbox
                  items-center: Căn items vào giữa theo cross-axis
                  justify-between: Khoảng cách giữa các items là space-between (2 đầu) */}
              <div className="mb-3 flex items-center justify-between">
                {/* Số thứ tự bước
                    flex: Sử dụng flexbox
                    h-7 w-7: Chiều cao và rộng 7 units (28px) - hình vuông
                    items-center: Căn items vào giữa theo cross-axis
                    justify-center: Căn items vào giữa theo main-axis
                    rounded-full: Bo tròn hoàn toàn (hình tròn)
                    bg-orange-500: Background cam đậm
                    text-xs: Font size rất nhỏ (12px)
                    font-semibold: Font weight semi-bold (600)
                    text-white: Màu chữ trắng */}
                <span className="flex h-7 w-7 items-center justify-center rounded-full bg-orange-500 text-xs font-semibold text-white">
                  {step.id} {/* Hiển thị số thứ tự bước (1-6) */}
                </span>
                
                {/* Category label (Bắt đầu / Đặt phòng / Kết quả)
                    text-[11px]: Font size 11px (custom size)
                    font-semibold: Font weight semi-bold
                    uppercase: Chữ in hoa
                    tracking-wide: Letter spacing rộng
                    text-orange-500: Màu cam đậm */}
                <span className="text-[11px] font-semibold uppercase tracking-wide text-orange-500">
                  {/* Conditional rendering: Hiển thị label dựa trên id
                      step.id <= 2: Bước 1-2 → "Bắt đầu"
                      step.id <= 4: Bước 3-4 → "Đặt phòng"
                      step.id > 4: Bước 5-6 → "Kết quả"
                      
                      Toán tử ternary lồng nhau:
                      condition1 ? value1 : condition2 ? value2 : value3
                      
                      Nếu step.id <= 2 → "Bắt đầu"
                      Nếu không, kiểm tra step.id <= 4 → "Đặt phòng"
                      Nếu không → "Kết quả" */}
                  {step.id <= 2 ? 'Bắt đầu' : step.id <= 4 ? 'Đặt phòng' : 'Kết quả'}
                </span>
              </div>
              
              {/* Tiêu đề của bước
                  mb-1: Margin bottom 1 unit (4px) để tạo khoảng cách với description
                  text-sm: Font size nhỏ (14px)
                  font-semibold: Font weight semi-bold (600)
                  text-gray-900: Màu xám rất đậm */}
              <h3 className="mb-1 text-sm font-semibold text-gray-900">{step.title}</h3>
              
              {/* Mô tả chi tiết của bước
                  text-xs: Font size rất nhỏ (12px)
                  text-gray-600: Màu xám vừa phải */}
              <p className="text-xs text-gray-600">{step.description}</p>
            </div>
          ))}
        </div>
      </div>
    </section>
  )
}

// Export component để có thể import ở nơi khác
export default FlowSection


