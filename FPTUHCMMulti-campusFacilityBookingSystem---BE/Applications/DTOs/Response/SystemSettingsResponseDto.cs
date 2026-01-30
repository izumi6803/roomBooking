namespace Applications.DTOs.Response
{
    public class SystemSettingsResponseDto
    {
        /// <summary>
        /// thời gian tối thiểu (giờ) trước khi booking bắt đầu
        /// </summary>
        public int MinimumBookingHoursBeforeStart { get; set; }

        /// <summary>
        /// số phút cho phép check-in trước StartTime
        /// </summary>
        public int CheckInMinutesBeforeStart { get; set; }

        /// <summary>
        /// số phút cho phép check-in sau StartTime
        /// </summary>
        public int CheckInMinutesAfterStart { get; set; }

        /// <summary>
        /// số phút tối thiểu sau khi check-in để được phép check-out
        /// mặc định: 0 (có thể check-out ngay sau khi check-in)
        /// </summary>
        public int CheckoutMinMinutesAfterCheckIn { get; set; }
    }
}

