/**
 * HomePage Component - Trang chủ của ứng dụng
 * 
 * Component này là trang chủ chính của ứng dụng, hiển thị:
 * - HeroSection: Phần banner giới thiệu với nút đăng nhập/xem cơ sở vật chất
 * - FeatureSection: Phần giới thiệu các tính năng chính của hệ thống
 * 
 * Component này không nhận props và không có state, chỉ đơn giản là container
 * để render các section con.
 */

// Import HeroSection component - Phần banner hero ở đầu trang
import HeroSection from './components/HeroSection'
// Import FeatureSection component - Phần giới thiệu tính năng
import FeatureSection from './components/FeatureSection'

/**
 * HomePage Component Function
 * 
 * Đây là functional component (React component dạng function)
 * Không nhận props, không có state, chỉ render UI tĩnh
 * 
 * @returns {JSX.Element} - JSX element chứa layout của trang chủ
 */
const HomePage = () => {
  return (
    // Container chính của trang chủ
    // flex flex-col: Sử dụng flexbox với hướng dọc (column)
    // Các section sẽ xếp chồng lên nhau theo chiều dọc
    <div className="flex flex-col">
      {/* HeroSection: Phần banner đầu trang với gradient background
          Hiển thị tiêu đề, mô tả và nút CTA (Call To Action) */}
      <HeroSection />
      
      {/* FeatureSection: Phần giới thiệu các tính năng chính
          Hiển thị 4 cards mô tả các tính năng của hệ thống */}
      <FeatureSection />
    </div>
  )
}

// Export component để có thể import ở nơi khác
export default HomePage


