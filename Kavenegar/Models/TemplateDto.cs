using Kavenegar.Models.Enums;

namespace Kavenegar.Models
{
    public class TemplateDto
    {
        public VerificationUsageType? SourceType { get; set; }
        public VerificationType? SendMethod { get; set; }
        public VerificationType? FallBackMethod { get; set; }
        public string PrimaryLineNumber { get; set; }
        public string SecondaryLineNumber { get; set; }
        public int? SwitchTTL { get; set; }
        public string SourceUrl { get; set; }
        public string SourceName { get; set; }
        public string Name { get; set; }
        public string TextMessage { get; set; }
        public string VoiceMessage { get; set; }
        public string LocalId { get; set; }
        public string ApiKey { get; set; }
    }
}
