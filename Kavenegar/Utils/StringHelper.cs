using System;
using System.Linq;
namespace Kavenegar.Utils
{
 public class StringHelper
 {
	public static String Join(String delimeter, string[] items)
	{
	 var result = items.Aggregate("", (current, obj) => current + (obj + ","));
	 return result.Substring(0, result.Length - 1);
	}
	public static String Join(String delimeter, long[] items)
	{
	 string result = items.Aggregate("", (current, obj) => current + (obj.ToString() + ","));
	 return result.Substring(0, result.Length - 1);
	}
 }
}