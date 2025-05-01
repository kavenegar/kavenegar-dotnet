namespace Kavenegar.Models.Enums
{
 public enum MessageStatus
 {
	Queued = 1,
	Schulded = 2,
	SentToCenter = 4,
    SentToTelecom = 5,
    Delivered = 10,
	Undelivered = 11,
	Canceled = 13,
	Filtered = 14,
	Received = 50,
	Incorrect = 100
 }
}
