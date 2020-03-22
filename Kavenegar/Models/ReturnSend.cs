using System.Collections.Generic;
using Kavenegar.Models;

namespace Kavenegar
{
    internal class ReturnSend
    {
        public Result @Return { get; set; }

        public List<SendResult> entries { get; set; }
    }
}
