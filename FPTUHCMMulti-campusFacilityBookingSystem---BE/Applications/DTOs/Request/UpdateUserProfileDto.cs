using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class UpdateUserProfileDto
    {
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số và bắt đầu bằng 0.")]
        public string? PhoneNumber { get; set; }

        public string? AvatarUrl { get; set; }

        /// <summary>
        /// MSSV, format: SE/SS/IB/MC + 6 số, 2 số đầu >= 14 
        /// </summary>
        [RegularExpression(@"^(SE|SS|IB|MC)(1[4-9]|[2-9][0-9])[0-9]{4}$",
            ErrorMessage = "MSSV không hợp lệ. Ví dụ đúng: SE173162. Ví dụ sai: AB000111.")]
        public string? StudentId { get; set; }
    }
}



