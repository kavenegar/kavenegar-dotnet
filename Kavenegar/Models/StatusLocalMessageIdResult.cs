using Kavenegar.Models.Enums;
namespace Kavenegar.Models
{
 public class StatusLocalMessageIdResult
 {
	public long Messageid { get; set; }
	public string Localid { get; set; }
	public MessageStatus Status { get; set; }
	public string Statustext { get; set; }
 }
}