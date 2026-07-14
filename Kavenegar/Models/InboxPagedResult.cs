using System.Collections.Generic;

namespace Kavenegar.Models
{
    public class InboxPagedResult
    {
        public Dictionary<string, string> Metadata { get; set; }
        public List<ReceiveResult> Entries { get; set; }
    }
}
