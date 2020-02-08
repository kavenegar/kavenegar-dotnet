using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnCountOutbox
    {
        public Result Return { get; set; }
        public List<CountOutboxResult> Entries { get; set; }
    }
}