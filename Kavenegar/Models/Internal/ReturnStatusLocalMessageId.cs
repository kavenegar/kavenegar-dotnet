using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnStatusLocalMessageId
    {
        public Result result { get; set; }
        public List<StatusLocalMessageIdResult> entries { get; set; }
    }
}