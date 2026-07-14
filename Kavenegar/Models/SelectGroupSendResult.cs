using System.Collections.Generic;

namespace Kavenegar.Models
{
    public class SelectGroupSendResult
    {
        public CursorPaginationModel Pagination { get; set; }
        public List<SendPartyDetailsResult> Entries { get; set; }
    }
}
