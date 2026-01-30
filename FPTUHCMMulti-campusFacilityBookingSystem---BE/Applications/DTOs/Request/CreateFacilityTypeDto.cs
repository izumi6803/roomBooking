using System.ComponentModel.DataAnnotations;

namespace Applications.DTOs.Request
{
    public class CreateFacilityTypeDto
    {
        [Required(ErrorMessage = "Tên loại cơ sở là bắt buộc")]
        [MaxLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? DefaultAmenities { get; set; }
        public int? DefaultCapacity { get; set; }
        public int? TypicalDurationHours { get; set; }
        public string? IconUrl { get; set; }
    }
}


