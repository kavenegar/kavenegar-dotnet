using System.Linq;

namespace Kavenegar.Utils
{
    public class StringHelper
    {
        public static string Join(string delimeter, string[] items)
        {
            var result = items.Aggregate("", (current, obj) => current + (obj + ","));
            return result.Substring(0, result.Length - 1);
        }

        public static string Join(string delimeter, long[] items)
        {
            string result = items.Aggregate("", (current, obj) => current + (obj.ToString() + ","));
            return result.Substring(0, result.Length - 1);
        }
    }
}
