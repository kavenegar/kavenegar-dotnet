using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnStatus
    {
        public Result result { get; set; }
        public List<StatusResult> entries { get; set; }
    }
}