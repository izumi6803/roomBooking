namespace Applications.DTOs.Request
{
    public class CheckInDto
    {
        /// <summary>
        /// Ghi chú khi check-in (ví dụ: số lượng bàn, ghế khi thuê phòng học)
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Danh sách URL ảnh đính kèm (JSON array)
        /// </summary>
        public List<string>? ImageUrls { get; set; }
    }
}

