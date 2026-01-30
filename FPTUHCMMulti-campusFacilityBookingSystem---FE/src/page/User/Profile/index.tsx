/**
 * Index file cho Profile module
 * 
 * File này là entry point (điểm vào) của Profile module.
 * Mục đích: Re-export Profile component để có thể import ngắn gọn hơn
 * 
 * Thay vì import: import Profile from './Profile/Profile'
 * Có thể import: import Profile from './Profile'
 * 
 * Đây là pattern phổ biến trong React để giữ cấu trúc folder rõ ràng
 * nhưng vẫn có cách import đơn giản.
 */

// Re-export Profile component từ file Profile.tsx trong cùng folder
// default export từ Profile.tsx sẽ được re-export ở đây
export { default } from './Profile';

