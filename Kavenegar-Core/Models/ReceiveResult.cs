using System;
namespace Kavenegar.Models
{
 public class ReceiveResult
 {
	public long Date { get; set; }

	public DateTime GregorianDate
	{
	 get
	 {
		return Utils.DateHelper.UnixTimestampToDateTime(Date);
	 }
	}
	
	public long MessageId { get; set; }

	public string Sender { get; set; }

	public string Message { get; set; }

	public string Receptor { get; set; }
 }
}