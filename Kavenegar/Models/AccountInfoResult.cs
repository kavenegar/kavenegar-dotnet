using Kavenegar.Utils;
using System;
namespace Kavenegar.Models
{
    public class AccountInfoResult
    {
        public long RemainCredit { get; set; }

        public long Expiredate { get; set; }

        public DateTime GregorianExpiredate => Expiredate.UnixTimestampToDateTime();

        public string Type { get; set; }
    }
}