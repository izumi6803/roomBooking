/**
 * Index file cho Home module
 * 
 * File này là entry point (điểm vào) của Home module.
 * Mục đích: Re-export HomePage component để có thể import ngắn gọn hơn
 * 
 * Thay vì import: import HomePage from './HomePage/HomePage'
 * Có thể import: import HomePage from './HomePage'
 * 
 * Đây là pattern phổ biến trong React để giữ cấu trúc folder rõ ràng
 * nhưng vẫn có cách import đơn giản.
 */

// Import HomePage component từ file HomePage.tsx trong cùng folder
import HomePage from './HomePage'

// Re-export để có thể import trực tiếp từ folder
// Khi import từ './HomePage', sẽ tự động import từ index.tsx này
export default HomePage


