namespace Kavenegar.Models
{
    public class SendPartyDetailsResult
    {
        public string MessageId { get; set; }
        public string Message { get; set; }
        public string Receptor { get; set; }
        public string Sender { get; set; }
        public byte Status { get; set; }
        public string StatusText { get; set; }
        public long Cost { get; set; }
        public long Date { get; set; }
    }
}
