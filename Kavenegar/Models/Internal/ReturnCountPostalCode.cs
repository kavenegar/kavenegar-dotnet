using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnCountPostalCode
    {
        public Result Return { get; set; }
        public List<CountPostalCodeResult> Entries { get; set; }
    }
}