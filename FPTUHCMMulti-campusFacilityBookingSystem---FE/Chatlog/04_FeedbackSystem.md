# Chatlog - Tạo luồng Feedback cho người đặt phòng

## Ngày: 4/12/2025

## Yêu cầu
Giúp tôi làm thêm luồng feedback cho người đặt phòng.

---

## Các file đã tạo

### 1. `src/page/User/MyBookings/api/api.ts` (Mới tạo)
API service cho trang Lịch sử đặt phòng và Feedback:

```typescript
export type BookingStatus = 'pending' | 'confirmed' | 'completed' | 'cancelled' | 'rejected';

export interface Feedback {
  id: string;
  rating: number; // 1-5
  comment: string;
  createdAt: string;
}

export interface UserBooking {
  id: string;
  facilityId: string;
  facility: Facility;
  date: string;
  startTime: string;
  endTime: string;
  purpose: string;
  numberOfPeople: number;
  status: BookingStatus;
  createdAt: string;
  feedback?: Feedback;
  rejectionReason?: string;
}
```

**Các hàm API:**
- `getMyBookings(status?)` - Lấy danh sách booking của user
- `getBookingById(id)` - Lấy chi tiết một booking
- `submitFeedback(request)` - Gửi đánh giá
- `cancelBooking(id)` - Hủy đặt phòng
- `getBookingStats()` - Lấy thống kê booking

### 2. `src/page/User/MyBookings/MyBookings.tsx` (Mới tạo)
Component trang Lịch sử đặt phòng với đầy đủ tính năng:

#### Giao diện bao gồm:
1. **Header** - Tiêu đề và mô tả trang
2. **Stats Cards** - Thống kê số liệu:
   - Tổng đơn
   - Chờ duyệt
   - Đã duyệt
   - Hoàn thành
   - Đã hủy
   - Đã đánh giá
3. **Filter Tabs** - Lọc theo trạng thái
4. **Bookings List** - Danh sách các đơn đặt phòng:
   - Icon loại phòng
   - Tên phòng + badge trạng thái
   - Ngày, giờ, vị trí, số người
   - Mục đích sử dụng
   - Lý do từ chối (nếu có)
   - Feedback đã gửi (nếu có)
   - Nút Đánh giá / Hủy đặt

#### Modal Feedback:
- Header gradient vàng-cam
- Thông tin booking đang đánh giá
- **Rating 5 sao** (interactive - click để chọn)
- Mô tả mức độ hài lòng theo số sao
- **Comment textarea** để nhập nhận xét
- Nút Hủy / Gửi đánh giá
- Loading state khi đang gửi
- **Success state** với animation và thông báo

### 3. `src/page/User/MyBookings/index.tsx` (Mới tạo)
Export file cho MyBookings page

### 4. `src/routes/User Route/index.tsx` (Cập nhật)
Thêm route mới:
```tsx
<Route 
  path="/my-bookings" 
  element={
    <ProtectedRoute allowedRoles={['Student', 'Lecturer']}>
      <MyBookingsPage />
    </ProtectedRoute>
  } 
/>
```

### 5. `src/layout/Header/Header.tsx` (Cập nhật)
Thêm link "Lịch sử đặt" trong navigation (chỉ hiện khi đã đăng nhập):
```tsx
{isAuthenticated && (
  <NavLink to="/my-bookings" ...>
    <Calendar className="w-4 h-4" />
    <span>Lịch sử đặt</span>
  </NavLink>
)}
```

---

## Cấu trúc thư mục

```
src/page/User/MyBookings/
├── api/
│   └── api.ts              # API service
├── MyBookings.tsx          # Component chính
└── index.tsx               # Export file
```

---

## Luồng Feedback

```
1. User đăng nhập thành công
   ↓
2. Click "Lịch sử đặt" trên Header
   ↓
3. Hiển thị danh sách các booking của user
   ↓
4. Với booking có status "completed" và chưa feedback
   ↓
5. Hiện nút "Đánh giá" màu vàng-cam
   ↓
6. User click → Mở Modal Feedback
   ↓
7. User chọn số sao (1-5)
   ↓
8. User nhập nhận xét (tùy chọn)
   ↓
9. User click "Gửi đánh giá"
   ↓
10. API submitFeedback được gọi
   ↓
11. Hiện success state + reload danh sách
   ↓
12. Booking hiển thị feedback đã gửi (sao + comment)
```

---

## Trạng thái Booking

| Status | Label | Màu | Hành động |
|--------|-------|-----|-----------|
| pending | Chờ duyệt | Vàng | Có thể Hủy |
| confirmed | Đã duyệt | Xanh dương | Có thể Hủy |
| completed | Hoàn thành | Xanh lá | Có thể Đánh giá (nếu chưa) |
| cancelled | Đã hủy | Xám | Không có hành động |
| rejected | Bị từ chối | Đỏ | Hiện lý do từ chối |

---

## Mock Data có sẵn

| ID | Phòng | Trạng thái | Feedback |
|----|-------|------------|----------|
| BK001 | Phòng họp A1 | Hoàn thành | ✅ 5 sao |
| BK002 | Lab Máy tính 1 | Hoàn thành | ❌ Chưa |
| BK003 | Phòng họp B2 | Đã duyệt | - |
| BK004 | Sân bóng đá | Chờ duyệt | - |
| BK005 | Phòng họp A1 | Bị từ chối | - |
| BK006 | Phòng họp X1 | Hoàn thành | ❌ Chưa |

---

## Rating Labels

| Số sao | Mô tả |
|--------|-------|
| 1 | Rất không hài lòng |
| 2 | Không hài lòng |
| 3 | Bình thường |
| 4 | Hài lòng |
| 5 | Rất hài lòng |

---

## Truy cập

```
http://localhost:5173/my-bookings
```
(Yêu cầu đăng nhập với role Student hoặc Lecturer)


