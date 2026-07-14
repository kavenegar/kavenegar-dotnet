namespace Kavenegar.Models
{
    public class SubClientResult
    {
        public long RemainCredit { get; set; }
        public long ExpireDate { get; set; }
        public string ApiKey { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PricingName { get; set; }
        public string SmsFarsiCost { get; set; }
        public string SmsEnglishCost { get; set; }
        public string CalllocalCost { get; set; }
        public long LastLoginDate { get; set; }
        public string AssignedLines { get; set; }
        public int MininumAllowedCredit { get; set; }
        public string Status { get; set; }
        public string PwdHashed { get; set; }
        public string Mobile { get; set; }
        public string LocalId { get; set; }
    }
}
