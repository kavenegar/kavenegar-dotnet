using System;
using System.Linq;
namespace Kavenegar.Utils
{

    public static class StringExtensions
    {
        public static string Join(this string[] items, string delimeter) =>
            items.Aggregate("", (current, obj) => current + (obj + ","))
            .TrimEnd(new char[] { ',' });

        public static string Join(this long[] items, string delimiter) =>
            items.Aggregate("", (current, obj) => current + (obj.ToString() + ","))
            .TrimEnd(new char[] { ',' });
    }

    public class StringHelper
    {
        [Obsolete(message: "This method is deprecated. Use Join extension method from StringExtensions class.")]
        public static String Join(String delimeter, string[] items)
        {
            var result = items.Aggregate("", (current, obj) => current + (obj + ","));
            return result.Substring(0, result.Length - 1);
        }

        [Obsolete(message: "This method is deprecated. Use Join extension method from StringExtensions class.")]
        public static String Join(String delimeter, long[] items)
        {
            string result = items.Aggregate("", (current, obj) => current + (obj.ToString() + ","));
            return result.Substring(0, result.Length - 1);
        }
    }
}