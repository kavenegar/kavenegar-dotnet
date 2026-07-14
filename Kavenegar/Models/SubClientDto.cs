namespace Kavenegar.Models
{
    public class SubClientDto
    {
        public string ApiKey { get; set; }
        public string Ip { get; set; }
        public string LocalId { get; set; }
        public double? Credit { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int? PlanId { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }
        public int? MininumAllowedCredit { get; set; }
        public string Lines { get; set; }
        public bool? CanUseParentLines { get; set; }
        public string ExpireDate { get; set; }
        public bool? EnableLink { get; set; }
        public bool? CanCharge { get; set; }
    }
}
