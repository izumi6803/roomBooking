interface UserInfoSectionProps {
  userName: string
  userStudentId?: string | null
  userEmail?: string | null
  userPhoneNumber?: string | null
}

/**
 * UserInfoSection Component - Hiển thị thông tin người đặt phòng
 * 
 * @description Component này hiển thị:
 *   - Avatar: Chữ cái đầu của tên (màu cam)
 *   - Tên người dùng
 *   - Badge "Lecturer" nếu không có MSSV (giảng viên)
 *   - MSSV (nếu có - sinh viên)
 *   - Email (nếu có)
 *   - SĐT (nếu có, hoặc thông báo "Chưa cập nhật")
 * 
 * @param userName - Tên người dùng (bắt buộc)
 * @param userStudentId - MSSV (optional, null nếu là giảng viên)
 * @param userEmail - Email (optional)
 * @param userPhoneNumber - Số điện thoại (optional)
 * 
 * @example
 * <UserInfoSection
 *   userName="Nguyễn Văn A"
 *   userStudentId="SE123456"
 *   userEmail="a.nguyen@student.example.com"
 *   userPhoneNumber="0901234567"
 * />
 */
export const UserInfoSection = ({
  userName,
  userStudentId,
  userEmail,
  userPhoneNumber,
}: UserInfoSectionProps) => {
  return (
    <div className="flex items-center gap-3 rounded-lg bg-gray-50 p-3">
      <div className="flex h-10 w-10 items-center justify-center rounded-full bg-orange-100 text-sm font-semibold text-orange-600">
        {userName.charAt(0).toUpperCase()}
      </div>
      <div className="flex-1">
        <div className="flex items-center gap-2 flex-wrap">
          <p className="text-sm font-semibold text-gray-900">{userName}</p>
          {!userStudentId && (
            <span className="inline-flex rounded-full px-2 py-0.5 text-xs font-semibold bg-purple-100 text-purple-700">
              Lecturer
            </span>
          )}
        </div>
        <div className="flex flex-col gap-0.5 mt-1">
          {userStudentId && <p className="text-xs text-gray-500">MSSV: {userStudentId}</p>}
          {userEmail && <p className="text-xs text-gray-500">Email: {userEmail}</p>}
          {userPhoneNumber ? (
            <p className="text-xs text-gray-500">SĐT: {userPhoneNumber}</p>
          ) : (
            <p className="text-xs text-gray-400 italic">Chưa cập nhật số điện thoại</p>
          )}
        </div>
      </div>
    </div>
  )
}
