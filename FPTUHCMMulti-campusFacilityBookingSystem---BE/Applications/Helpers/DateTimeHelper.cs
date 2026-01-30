namespace Applications.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo VietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        /// <summary>
        /// Lấy thời gian hiện tại theo múi giờ Việt Nam (UTC+7)
        /// </summary>
        public static DateTime VietnamNow => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, VietnamTimeZone);

        /// <summary>
        /// Chuyển đổi từ UTC sang múi giờ Việt Nam
        /// </summary>
        public static DateTime ToVietnamTime(DateTime utcDateTime)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
            {
                utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
            }
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, VietnamTimeZone);
        }

        /// <summary>
        /// Chuyển đổi từ múi giờ Việt Nam sang UTC
        /// </summary>
        public static DateTime ToUtc(DateTime vietnamDateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(vietnamDateTime, VietnamTimeZone);
        }
    }
}