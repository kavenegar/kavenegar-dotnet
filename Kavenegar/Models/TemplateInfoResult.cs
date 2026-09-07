namespace Kavenegar.Models
{
    public class TemplateInfoResult
    {
        public string Name { get; set; }
        public string SourceType { get; set; }
        public string SendMethod { get; set; }
        public string FallBackMethod { get; set; }
        public string PrimaryLineNumber { get; set; }
        public string SecondaryLineNumber { get; set; }
        public int SwitchTTL { get; set; }
        public string SourceUrl { get; set; }
        public string SourceName { get; set; }
        public string TextMessage { get; set; }
        public string VoiceMessage { get; set; }
        public string TemplateId { get; set; }
    }
}
