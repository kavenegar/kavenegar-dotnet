using Kavenegar.Utils;
using System;
namespace Kavenegar.Models
{
    public class ReceiveResult
    {
        public long Date { get; set; }

        public DateTime GregorianDate => Date.UnixTimestampToDateTime();

        public long MessageId { get; set; }

        public string Sender { get; set; }

        public string Message { get; set; }

        public string Receptor { get; set; }
    }
}