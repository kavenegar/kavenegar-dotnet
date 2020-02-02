using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnCountInbox
    {
        public Result result { get; set; }
        public List<CountInboxResult> entries { get; set; }

    }
}