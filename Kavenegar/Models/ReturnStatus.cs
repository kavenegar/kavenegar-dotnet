using System.Collections.Generic;
using Kavenegar.Models;

namespace Kavenegar
{
    internal class ReturnStatus
    {
        public Result result { get; set; }

        public List<StatusResult> entries { get; set; }
    }
}
