using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using Kavenegar.Exceptions;
using Kavenegar.Models;
using Kavenegar.Models.Enums;
using Kavenegar.Utils;

namespace Kavenegar
{
    internal class ReturnResult
    {
        public Result @Return { get; set; }
        public object entries { get; set; }
    }

    internal class Result
    {
        public int status { get; set; }
        public string message { get; set; }
    }

    internal class ReturnSend
    {
        public Result @Return { get; set; }
        public List<SendResult> entries { get; set; }
    }

    internal class ReturnStatus
    {
        public Result result { get; set; }
        public List<StatusResult> entries { get; set; }
    }

    internal class ReturnStatusLocalMessageId
    {
        public Result result { get; set; }
        public List<StatusLocalMessageIdResult> entries { get; set; }
    }

    internal class ReturnReceive
    {
        public Result result { get; set; }
        public List<ReceiveResult> entries { get; set; }
    }

    internal class ReturnCountOutbox
    {
        public Result result { get; set; }
        public List<CountOutboxResult> entries { get; set; }
    }

    internal class ReturnCountInbox
    {
        public Result result { get; set; }
        public List<CountInboxResult> entries { get; set; }

    }

    internal class ReturnCountPostalCode
    {
        public Result result { get; set; }
        public List<CountPostalCodeResult> entries { get; set; }
    }

    internal class ReturnAccountInfo
    {
        public Result result { get; set; }
        public AccountInfoResult entries { get; set; }
    }

    internal class ReturnAccountConfig
    {
        public Result result { get; set; }
        public AccountConfigResult entries { get; set; }
    }
}
