using System.Collections.Generic;
using Kavenegar.Models;

namespace Kavenegar
{
    internal class ReturnStatusLocalMessageId
    {
        public Result result { get; set; }

        public List<StatusLocalMessageIdResult> entries { get; set; }
    }
}
