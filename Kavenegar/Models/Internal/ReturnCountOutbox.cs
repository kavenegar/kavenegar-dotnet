using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnCountOutbox
    {
        public Result result { get; set; }
        public List<CountOutboxResult> entries { get; set; }
    }
}