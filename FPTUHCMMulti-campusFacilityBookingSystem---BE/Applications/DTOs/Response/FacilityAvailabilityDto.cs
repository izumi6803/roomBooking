namespace Applications.DTOs.Response
{
    public class FacilityAvailabilityDto
    {
        public string FacilityId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public string? RoomNumber { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public List<BookingSlot>? BookedSlots { get; set; }
    }

    public class BookingSlot
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}

