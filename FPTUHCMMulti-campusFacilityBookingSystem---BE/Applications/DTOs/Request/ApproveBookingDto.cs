using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class ApproveBookingDto
    {
        /// <summary>
        /// MSSV, format: SE/SS/IB/MC + 6 số, 2 số đầu &gt;= 14 (ví dụ: SE173162)
        /// </summary>
        [Required(ErrorMessage = "MSSV là bắt buộc khi duyệt booking.")]
        [RegularExpression(@"^(SE|SS|IB|MC)(1[4-9]|[2-9][0-9])[0-9]{4}$",
            ErrorMessage = "MSSV không hợp lệ. Ví dụ đúng: SE173162. Ví dụ sai: AB000111.")]
        public string StudentId { get; set; } = string.Empty;
    }
}


