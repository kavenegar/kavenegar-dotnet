using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnReceive
    {
        public Result result { get; set; }
        public List<ReceiveResult> entries { get; set; }
    }
}