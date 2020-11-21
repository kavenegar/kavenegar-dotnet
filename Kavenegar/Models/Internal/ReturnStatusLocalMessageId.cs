using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnStatusLocalMessageId
    {
        public Result Result { get; set; }
        public List<StatusLocalMessageIdResult> Entries { get; set; }
    }
}