using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Kavenegar.Utils
{
 public class RestHelper
 {
	public static string SendPost(string url, Dictionary<String, Object> param)
	{

	 byte[] byteArray;
	 string webpageContent;
	 string postdata = "";
	 if (param != null)
	 {
		postdata = param.Keys.Aggregate(postdata, (current, key) => current + String.Format("{0}={1}&", key, param[key]));
		byteArray = Encoding.UTF8.GetBytes(postdata);
	 }
	 else
	 {
		byteArray = new byte[0];
	 }
	 try
	 {
		HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
		webRequest.Method = "POST";
		webRequest.Timeout = -1;
		webRequest.ContentType = "application/x-www-form-urlencoded";
		webRequest.ContentLength = byteArray.Length;
		using (Stream webpageStream = webRequest.GetRequestStream())
		{
		 webpageStream.Write(byteArray, 0, byteArray.Length);
		}
		using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
		{
		 using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
		 {
			webpageContent = reader.ReadToEnd();
		 }
		}
	 }
	 catch (Exception ex)
	 {
		//throw or return an appropriate response/exception
		throw ex;
	 }
	 return webpageContent;
	}
 }
}
