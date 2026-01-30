/**
 * HeroSection Component - Phần banner hero của trang chủ
 * 
 * Component này hiển thị phần banner đầu trang với:
 * - Background gradient đẹp mắt (từ cam đến tím)
 * - Tiêu đề và mô tả về hệ thống đặt phòng
 * - Nút CTA (Call To Action) thay đổi dựa trên trạng thái đăng nhập:
 *   + Chưa đăng nhập: Hiển thị nút "Đăng nhập để bắt đầu đặt phòng"
 *   + Đã đăng nhập: Hiển thị nút "Xem cơ sở vật chất"
 * 
 * Component sử dụng:
 * - useAuthState hook để kiểm tra trạng thái đăng nhập
 * - Link từ react-router-dom để điều hướng
 * - Tailwind CSS cho styling
 */

// Import Link component từ react-router-dom để điều hướng giữa các trang
// Link tương tự <a> tag nhưng không reload trang (Single Page Application)
import { Link } from 'react-router-dom'
// Import custom hook để lấy trạng thái authentication
// Hook này trả về thông tin về user đã đăng nhập hay chưa
import { useAuthState } from '../../../../hooks/useAuthState'

/**
 * HeroSection Component Function
 * 
 * Functional component không nhận props
 * Sử dụng hook để lấy trạng thái authentication và render UI tương ứng
 * 
 * @returns {JSX.Element} - JSX element chứa hero section với banner và CTA button
 */
const HeroSection = () => {
  // Sử dụng useAuthState hook để lấy trạng thái đăng nhập
  // isAuthenticated: boolean - true nếu user đã đăng nhập, false nếu chưa
  // Destructuring để chỉ lấy isAuthenticated từ object trả về
  const { isAuthenticated } = useAuthState()

  return (
    // Section chính của hero banner
    // relative: Để các element con có thể dùng absolute positioning
    // overflow-hidden: Ẩn phần nội dung tràn ra ngoài (cho hiệu ứng blur)
    // bg-gradient-to-br: Background gradient từ trên-trái đến dưới-phải
    // from-orange-500 via-orange-600 to-purple-700: Màu gradient (cam -> cam đậm -> tím)
    // text-white: Màu chữ trắng cho toàn bộ section
    <section className="relative overflow-hidden bg-gradient-to-br from-orange-500 via-orange-600 to-purple-700 text-white">
      {/* Decorative blur circle ở góc trái trên
          absolute: Vị trí tuyệt đối so với section cha (relative)
          -left-20: Dịch sang trái 20 units (một phần nằm ngoài viewport)
          top-10: Cách top 10 units
          h-64 w-64: Kích thước 64x64 (256px x 256px)
          rounded-full: Bo tròn thành hình tròn
          bg-white/10: Màu trắng với độ trong suốt 10%
          blur-3xl: Hiệu ứng blur mạnh (tạo hiệu ứng mờ đẹp mắt) */}
      <div className="absolute -left-20 top-10 h-64 w-64 rounded-full bg-white/10 blur-3xl" />
      
      {/* Decorative blur circle ở góc phải dưới
          Tương tự như circle trên nhưng ở vị trí khác và màu tím */}
      <div className="absolute -right-10 bottom-0 h-72 w-72 rounded-full bg-purple-900/30 blur-3xl" />

      {/* Container chính cho nội dung
          relative: Để nội dung hiển thị trên các blur circles
          mx-auto: Căn giữa theo chiều ngang (margin left-right auto)
          flex: Sử dụng flexbox
          max-w-6xl: Chiều rộng tối đa 6xl (1152px trong Tailwind)
          flex-col: Hướng flex là column (dọc)
          items-center: Căn items vào giữa theo cross-axis (ngang)
          gap-10: Khoảng cách giữa các items là 10 units
          px-4: Padding trái-phải 4 units (16px)
          py-16: Padding trên-dưới 16 units (64px)
          lg:py-24: Trên màn hình large, padding trên-dưới là 24 units (96px)
          text-center: Căn giữa text */}
      <div className="relative mx-auto flex max-w-6xl flex-col items-center gap-10 px-4 py-16 text-center lg:py-24">
        {/* Container cho text content
            max-w-2xl: Chiều rộng tối đa 2xl (672px) để text không quá rộng
            space-y-6: Khoảng cách dọc giữa các children là 6 units */}
        <div className="max-w-2xl space-y-6">
          {/* Badge/Label phía trên tiêu đề
              text-xs: Font size nhỏ
              font-semibold: Font weight semi-bold
              uppercase: Chữ in hoa
              tracking-[0.2em]: Letter spacing 0.2em (khoảng cách giữa các chữ cái)
              text-orange-100: Màu cam nhạt */}
          <p className="text-xs font-semibold uppercase tracking-[0.2em] text-orange-100">
            FPT University HCMC · Multi‑campus
          </p>
          
          {/* Tiêu đề chính (H1)
              text-3xl: Font size 3xl (30px) mặc định
              font-black: Font weight rất đậm (900)
              leading-tight: Line height chặt (khoảng cách dòng nhỏ)
              sm:text-4xl: Trên màn hình small, font size 4xl (36px)
              lg:text-5xl: Trên màn hình large, font size 5xl (48px) */}
          <h1 className="text-3xl font-black leading-tight sm:text-4xl lg:text-5xl">
            Đặt phòng học, phòng lab, sân thể thao nhanh chóng với{' '}
            {/* Span để highlight phần quan trọng
                underline: Gạch chân
                decoration-orange-200: Màu gạch chân là cam nhạt
                decoration-4: Độ dày gạch chân là 4px
                underline-offset-4: Khoảng cách giữa text và gạch chân là 4px */}
            <span className="underline decoration-orange-200 decoration-4 underline-offset-4">
              hệ thống đặt phòng thông minh
            </span>
            .
          </h1>
          
          {/* Mô tả/Description
              text-sm: Font size nhỏ mặc định
              text-orange-50: Màu cam rất nhạt (gần trắng)
              sm:text-base: Trên màn hình small, font size base (16px) */}
          <p className="text-sm text-orange-50 sm:text-base">
            Một nơi duy nhất để sinh viên và giảng viên khám phá các phòng học, phòng lab và sân thể thao có sẵn tại các campus HCM &
            NVH, gửi yêu cầu đặt phòng và theo dõi phê duyệt theo thời gian thực.
          </p>

          {/* Container cho các nút CTA (Call To Action)
              flex: Sử dụng flexbox
              flex-wrap: Cho phép wrap (xuống dòng) khi không đủ chỗ
              items-center: Căn items vào giữa theo cross-axis
              justify-center: Căn items vào giữa theo main-axis
              gap-3: Khoảng cách giữa các items là 3 units */}
          <div className="flex flex-wrap items-center justify-center gap-3">
            {/* Conditional rendering: Chỉ hiển thị nút đăng nhập nếu CHƯA đăng nhập
                !isAuthenticated: Nếu isAuthenticated = false (chưa đăng nhập)
                &&: Logical AND operator - chỉ render nếu điều kiện true */}
            {!isAuthenticated && (
              // Link component từ react-router-dom
              // to="/login": Điều hướng đến trang /login khi click
              <Link
                to="/login"
                // Styling cho nút đăng nhập
                // rounded-full: Bo tròn hoàn toàn (pill shape)
                // bg-white: Background trắng
                // px-6 py-3: Padding ngang 6 units, dọc 3 units
                // text-sm: Font size nhỏ
                // font-semibold: Font weight semi-bold
                // text-orange-600: Màu chữ cam đậm
                // shadow-lg: Shadow lớn
                // shadow-orange-900/30: Màu shadow cam rất đậm với độ trong suốt 30%
                // hover:bg-orange-50: Khi hover, background chuyển sang cam rất nhạt
                className="rounded-full bg-white px-6 py-3 text-sm font-semibold text-orange-600 shadow-lg shadow-orange-900/30 hover:bg-orange-50"
              >
                Đăng nhập để bắt đầu đặt phòng
              </Link>
            )}
            
            {/* Conditional rendering: Chỉ hiển thị nút xem facilities nếu ĐÃ đăng nhập
                isAuthenticated: Nếu isAuthenticated = true (đã đăng nhập)
                &&: Logical AND operator - chỉ render nếu điều kiện true */}
            {isAuthenticated && (
              // Link component điều hướng đến trang facilities
              <Link
                to="/facilities"
                // Styling cho nút xem facilities (outline style)
                // rounded-full: Bo tròn hoàn toàn
                // border: Có border
                // border-orange-100/70: Màu border cam nhạt với độ trong suốt 70%
                // bg-orange-500/10: Background cam với độ trong suốt 10% (semi-transparent)
                // px-5 py-2.5: Padding ngang 5 units, dọc 2.5 units
                // text-sm: Font size nhỏ
                // font-semibold: Font weight semi-bold
                // text-orange-50: Màu chữ cam rất nhạt
                // hover:bg-orange-500/20: Khi hover, background đậm hơn (20% opacity)
                className="rounded-full border border-orange-100/70 bg-orange-500/10 px-5 py-2.5 text-sm font-semibold text-orange-50 hover:bg-orange-500/20"
              >
                Xem cơ sở vật chất
              </Link>
            )}
          </div>
        </div>
      </div>
    </section>
  )
}

// Export component để có thể import ở nơi khác
export default HeroSection


