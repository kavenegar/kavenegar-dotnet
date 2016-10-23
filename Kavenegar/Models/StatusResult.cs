using Kavenegar.Models.Enums;
namespace Kavenegar.Models
{
 public class StatusResult
 {
	public long Messageid { get; set; }
	public MessageStatus Status { get; set; }
	public string Statustext { get; set; }
 }
}