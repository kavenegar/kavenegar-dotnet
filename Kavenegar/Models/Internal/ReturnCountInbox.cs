using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnCountInbox
    {
        public Result Return { get; set; }
        public List<CountInboxResult> Entries { get; set; }

    }
}