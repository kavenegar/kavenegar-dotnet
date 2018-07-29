using System;
using System.Globalization;
namespace Kavenegar.Utils
{

    public class DateHelper
    {
        [Obsolete(message: "This method is deprecated. Use UnixTimestampToDateTime extension method from DateExtensions class.")]
        public static DateTime UnixTimestampToDateTime(long unixTimeStamp) => unixTimeStamp.UnixTimestampToDateTime();

        [Obsolete(message: "This method is deprecated. Use DateTimeToUnixTimestamp extension method from DateExtensions class.")]
        public static long DateTimeToUnixTimestamp(DateTime idateTime) => idateTime.DateTimeToUnixTimestamp();
    }

    public static class DateExtensions
    {
        public static DateTime UnixTimestampToDateTime(this long unixTimeStamp)
        {
            try   { return (new DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds(unixTimeStamp); }
            catch { return DateTime.MaxValue; }
        }

        public static long DateTimeToUnixTimestamp(this DateTime idateTime)
        {
            try
            {
                idateTime = new DateTime(idateTime.Year, idateTime.Month, idateTime.Day, idateTime.Hour, idateTime.Minute, idateTime.Second);
                TimeSpan unixTimeSpan = (idateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).ToLocalTime());
                return long.Parse(unixTimeSpan.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            catch
            {
                return 0;
            }
        }
    }
}
