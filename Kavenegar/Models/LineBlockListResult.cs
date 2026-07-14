using System.Collections.Generic;

namespace Kavenegar.Models
{
    public class LineBlockListResult
    {
        public Dictionary<string, string> Metadata { get; set; }
        public List<BlockedLineDetail> Entries { get; set; }
    }
}
