using System.Collections.Generic;

namespace Kavenegar.Models.Internal
{
    internal class ReturnCountPostalCode
    {
        public Result result { get; set; }
        public List<CountPostalCodeResult> entries { get; set; }
    }
}