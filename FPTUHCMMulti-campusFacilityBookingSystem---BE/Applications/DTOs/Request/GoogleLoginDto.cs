using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class GoogleLoginDto
    {
        [Required(ErrorMessage = "ID Token là bắt buộc")]
        public string IdToken { get; set; } = string.Empty;
    }
}


