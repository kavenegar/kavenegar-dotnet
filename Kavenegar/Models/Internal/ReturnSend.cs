using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnSend
    {
        public Result @Return { get; set; }
        public List<SendResult> entries { get; set; }
    }
}