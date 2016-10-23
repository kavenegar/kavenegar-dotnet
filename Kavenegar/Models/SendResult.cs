using System;
using Kavenegar.Utils;

namespace Kavenegar.Models
{
 public class SendResult
 {
	public long Messageid { get; set; }

	public int Cost { get; set; }

	public DateTime GregorianDate
	{
	 get { return DateHelper.UnixTimestampToDateTime(Date); }
	 set { Date = DateHelper.DateTimeToUnixTimestamp(value); }
	}
	
	public long Date { get; set; }

	public string Message { get; set; }

	public string Receptor { get; set; }

	public string Sender { get; set; }
	public int Status { get; set; }

	public string StatusText { get; set; }
 }
}