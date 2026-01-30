namespace Applications.DTOs.Request
{
    public class UpdateSystemSettingsDto
    {
        /// <summary>
        /// thời gian tối thiểu (giờ) trước khi booking bắt đầu (mặc định: 3)
        /// </summary>
        public int? MinimumBookingHoursBeforeStart { get; set; }

        /// <summary>
        /// số phút cho phép check-in trước StartTime (mặc định: 15)
        /// </summary>
        public int? CheckInMinutesBeforeStart { get; set; }

        /// <summary>
        /// số phút cho phép check-in sau StartTime (mặc định: 15)
        /// </summary>
        public int? CheckInMinutesAfterStart { get; set; }

        /// <summary>
        /// số phút tối thiểu sau khi check-in để được phép check-out (mặc định: 0 - có thể check-out ngay sau khi check-in)
        /// </summary>
        public int? CheckoutMinMinutesAfterCheckIn { get; set; }
    }
}

