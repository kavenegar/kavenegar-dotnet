using System.Collections.Generic;
using Kavenegar.Models;

namespace Kavenegar
{
    internal class ReturnCountOutbox
    {
        public Result result { get; set; }

        public List<CountOutboxResult> entries { get; set; }
    }
}
