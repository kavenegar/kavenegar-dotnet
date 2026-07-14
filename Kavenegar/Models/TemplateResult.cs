namespace Kavenegar.Models
{
    public class TemplateResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmsMessage { get; set; }
        public string CallMessage { get; set; }
        public string PrimaryLineNumber { get; set; }
        public string SecondaryLineNumber { get; set; }
        public string SendPriority { get; set; }
        public int SwitchTTL { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
