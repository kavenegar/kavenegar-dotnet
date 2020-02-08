using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnReceive
    {
        public Result Return { get; set; }
        public List<ReceiveResult> Entries { get; set; }
    }
}