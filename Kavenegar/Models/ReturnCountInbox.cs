using System.Collections.Generic;
using Kavenegar.Models;

namespace Kavenegar
{
    internal class ReturnCountInbox
    {
        public Result result { get; set; }

        public List<CountInboxResult> entries { get; set; }
    }
}
