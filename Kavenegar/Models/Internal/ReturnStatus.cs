using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnStatus
    {
        public Result Return { get; set; }
        public List<StatusResult> Entries { get; set; }
    }
}