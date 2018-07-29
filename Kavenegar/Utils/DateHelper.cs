using System;
using System.Globalization;
namespace Kavenegar.Utils
{
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
