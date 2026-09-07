namespace Kavenegar.Models
{
    public class GroupSendReportResult
    {
        public int PartyId { get; set; }
        public string ApprovalStatus { get; set; }
        public string Message { get; set; }
        public long Date { get; set; }
        public int ReceptorsCount { get; set; }
        public int MessageCount { get; set; }
        public string Status { get; set; }
        public string MessageLanguage { get; set; }
        public string Sender { get; set; }
    }
}
