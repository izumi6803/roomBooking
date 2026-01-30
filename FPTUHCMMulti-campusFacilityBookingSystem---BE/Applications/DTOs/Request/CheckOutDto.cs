namespace Applications.DTOs.Request
{
    public class CheckOutDto
    {
        /// <summary>
        /// Ghi chú khi check-out (ví dụ: tình trạng phòng sau khi sử dụng)
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Danh sách URL ảnh đính kèm (JSON array)
        /// </summary>
        public List<string>? ImageUrls { get; set; }
    }
}

