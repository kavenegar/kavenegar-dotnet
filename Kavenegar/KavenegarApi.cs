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

    public class KavenegarApi
    {
        private string _apikey;
        private int _returnCode = 200;
        private string _returnMessage = "";
        private const string Apipath = "https://api.kavenegar.com/v1/{0}/{1}/{2}.{3}";
        private static readonly JavaScriptSerializer JsonSerialiser = new JavaScriptSerializer();
        public KavenegarApi(string apikey)
        {
            _apikey = apikey;
        }

        public string ApiKey
        {
            set => _apikey = value;
            get => _apikey;
        }

        public int ReturnCode
        {
            get { return _returnCode; }

        }

        public string ReturnMessage
        {
            get { return _returnMessage; }

        }

        private string GetApiPath(string _base, string method, string output)
        {
            return string.Format(Apipath, _apikey, _base, method, output);
        }

        private static string Execute(string path, Dictionary<string, object> _params)
        {

            string responseBody = "";
            string postdata = "";

            byte[] byteArray;
            if (_params != null)
            {
                postdata = _params.Keys.Aggregate(postdata,
                    (current, key) => current + string.Format("{0}={1}&", key, _params[key]));
                byteArray = Encoding.UTF8.GetBytes(postdata);
            }
            else
            {
                byteArray = new byte[0];
            }
            var webRequest = (HttpWebRequest)WebRequest.Create(path);
            webRequest.Method = "POST";
            webRequest.Timeout = -1;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            using (Stream webpageStream = webRequest.GetRequestStream())
            {
                webpageStream.Write(byteArray, 0, byteArray.Length);
            }
            HttpWebResponse webResponse;
            try
            {
                using (webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
                JsonSerialiser.Deserialize<ReturnResult>(responseBody);
                return responseBody;
            }
            catch (WebException webException)
            {
                webResponse = (HttpWebResponse)webException.Response;
                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    responseBody = reader.ReadToEnd();
                }
                try
                {
                    var result = JsonSerialiser.Deserialize<ReturnResult>(responseBody);
                    throw new ApiException(result.Return.message, result.Return.status);
                }
                catch (ApiException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new HttpException(ex.Message, (int)((HttpWebResponse)webException.Response).StatusCode);
                }
            }
        }
        public List<SendResult> Send(string sender, List<string> receptor, string message)
        {
            return Send(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue);
        }

        public SendResult Send(string sender, String receptor, string message)
        {
            return Send(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue);
        }
        public SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date)
        {
            List<String> receptors = new List<String> { receptor };
            return Send(sender, receptors, message, type, date)[0];
        }
        public List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date)
        {
            return Send(sender, receptor, message, type, date, null);
        }
        public SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date, string localid)
        {
            var receptors = new List<String> { receptor };
            var localids = new List<String> { localid };
            return Send(sender, receptors, message, type, date, localids)[0];
        }
        public SendResult Send(string sender, string receptor, string message, string localid)
        {
            return Send(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue, localid);
        }
        public List<SendResult> Send(string sender, List<string> receptors, string message, string localid)
        {
            List<String> localids = new List<String>();
            for (var i = 0; i <= receptors.Count - 1; i++)
            {
                localids.Add(localid);
            }
            return Send(sender, receptors, message, MessageType.MobileMemory, DateTime.MinValue, localids);
        }
        public List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date, List<string> localids)
        {
            var path = GetApiPath("sms", "send", "json");
            var param = new Dictionary<string, object>
        {
            {"sender", System.Web.HttpUtility.UrlEncodeUnicode(sender)},
            {"receptor", System.Web.HttpUtility.UrlEncodeUnicode(StringHelper.Join(",", receptor.ToArray()))},
            {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
            {"type", (int) type},
            {"date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date)}
        };
            if (localids != null && localids.Count > 0)
            {
                param.Add("localid", StringHelper.Join(",", localids.ToArray()));
            }
            var responseBody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnSend>(responseBody);
            return l.entries;
        }

        public List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages)
        {
            var types = new List<MessageType>();
            for (var i = 0; i <= senders.Count - 1; i++)
            {
                types.Add(MessageType.MobileMemory);
            }
            return SendArray(senders, receptors, messages, types, DateTime.MinValue, null);
        }

        public List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date)
        {
            var senders = new List<string>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }
            var types = new List<MessageType>();
            for (var i = 0; i <= senders.Count - 1; i++)
            {
                types.Add(MessageType.MobileMemory);
            }
            return SendArray(senders, receptors, messages, types, date, null);
        }

        public List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date, string localmessageids)
        {
            var senders = new List<String>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }
            List<MessageType> types = new List<MessageType>();
            for (var i = 0; i <= senders.Count - 1; i++)
            {
                types.Add(MessageType.MobileMemory);
            }
            return SendArray(senders, receptors, messages, types, date, new List<String>() { localmessageids });
        }

        public List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, string localmessageid)
        {
            List<String> senders = new List<String>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }

            return SendArray(senders, receptors, messages, localmessageid);
        }

        public List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages, string localmessageid)
        {
            var types = new List<MessageType>();
            for (var i = 0; i <= receptors.Count - 1; i++)
            {
                types.Add(MessageType.MobileMemory);
            }
            var localmessageids = new List<string>();
            for (var i = 0; i <= receptors.Count - 1; i++)
            {
                localmessageids.Add(localmessageid);
            }
            return SendArray(senders, receptors, messages, types, DateTime.MinValue, localmessageids);
        }

        public List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages, List<MessageType> types, DateTime date, List<string> localmessageids)
        {
            String path = GetApiPath("sms", "sendarray", "json");
            var jsonSerialiser = new JavaScriptSerializer();
            var jsonSenders = jsonSerialiser.Serialize(senders);
            var jsonReceptors = jsonSerialiser.Serialize(receptors);
            var jsonMessages = jsonSerialiser.Serialize(messages);
            var jsonTypes = jsonSerialiser.Serialize(types);
            var param = new Dictionary<string, object>
        {
            {"message", jsonMessages},
            {"sender", jsonSenders},
            {"receptor", jsonReceptors},
            {"type", jsonTypes},
            {"date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date)}
        };
            if (localmessageids != null && localmessageids.Count > 0)
            {
                param.Add("localmessageids", StringHelper.Join(",", localmessageids.ToArray()));
            }

            var responsebody = Execute(path, param);
            var l = jsonSerialiser.Deserialize<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }
            return l.entries;
        }

        public List<StatusResult> Status(List<string> messageids)
        {
            string path = GetApiPath("sms", "status", "json");
            var param = new Dictionary<string, object>
        {
            {"messageid", StringHelper.Join(",", messageids.ToArray())}
        };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnStatus>(responsebody);
            if (l.entries == null)
            {
                return new List<StatusResult>();
            }
            return l.entries;
        }

        public StatusResult Status(string messageid)
        {
            var ids = new List<String> { messageid };
            var result = Status(ids);
            return result.Count == 1 ? result[0] : null;
        }

        public List<StatusLocalMessageIdResult> StatusLocalMessageId(List<string> messageids)
        {
            string path = GetApiPath("sms", "statuslocalmessageid", "json");
            var param = new Dictionary<string, object> { { "localid", StringHelper.Join(",", messageids.ToArray()) } };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnStatusLocalMessageId>(responsebody);
            return l.entries;
        }

        public StatusLocalMessageIdResult StatusLocalMessageId(string messageid)
        {
            List<StatusLocalMessageIdResult> result = StatusLocalMessageId(new List<String>() { messageid });
            return result.Count == 1 ? result[0] : null;
        }

        public List<SendResult> Select(List<string> messageids)
        {
            var path = GetApiPath("sms", "select", "json");
            var param = new Dictionary<string, object> { { "messageid", StringHelper.Join(",", messageids.ToArray()) } };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }
            return l.entries;
        }

        public SendResult Select(string messageid)
        {
            var ids = new List<String> { messageid };
            var result = Select(ids);
            return result.Count == 1 ? result[0] : null;
        }

        public List<SendResult> SelectOutbox(DateTime startdate)
        {
            return SelectOutbox(startdate, DateTime.MaxValue);
        }

        public List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate)
        {
            return SelectOutbox(startdate, enddate, null);
        }

        public List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate, String sender)
        {
            String path = GetApiPath("sms", "selectoutbox", "json");
            var param = new Dictionary<string, object>
         {
             {"startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate)},
             {"enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate)},
             {"sender", sender}
         };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnSend>(responsebody);
            return l.entries;
        }

        public List<SendResult> LatestOutbox(long pagesize)
        {
            return LatestOutbox(pagesize, "");
        }

        public List<SendResult> LatestOutbox(long pagesize, String sender)
        {
            var path = GetApiPath("sms", "latestoutbox", "json");
            var param = new Dictionary<string, object> { { "pagesize", pagesize }, { "sender", sender } };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnSend>(responsebody);
            return l.entries;
        }

        public CountOutboxResult CountOutbox(DateTime startdate)
        {
            return CountOutbox(startdate, DateTime.MaxValue, 10);
        }

        public CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate)
        {
            return CountOutbox(startdate, enddate, 0);
        }

        public CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate, int status)
        {
            string path = GetApiPath("sms", "countoutbox", "json");
            var param = new Dictionary<string, object>
         {
             {"startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate)},
             {"enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate)},
             {"status", status}
         };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnCountOutbox>(responsebody);
            if (l.entries == null || l.entries[0] == null)
            {
                return new CountOutboxResult();
            }
            return l.entries[0];
        }

        public List<StatusResult> Cancel(List<String> ids)
        {
            string path = GetApiPath("sms", "cancel", "json");
            var param = new Dictionary<string, object>
        {
            {"messageid", StringHelper.Join(",", ids.ToArray())}
        };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnStatus>(responsebody);
            return l.entries;
        }

        public StatusResult Cancel(String messageid)
        {
            var ids = new List<String> { messageid };
            var result = Cancel(ids);
            return result.Count == 1 ? result[0] : null;
        }

        public List<ReceiveResult> Receive(string line, int isread)
        {
            String path = GetApiPath("sms", "receive", "json");
            var param = new Dictionary<string, object> { { "linenumber", line }, { "isread", isread } };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnReceive>(responsebody);
            if (l.entries == null)
            {
                return new List<ReceiveResult>();
            }
            return l.entries;
        }

        public CountInboxResult CountInbox(DateTime startdate, string linenumber)
        {
            return CountInbox(startdate, DateTime.MaxValue, linenumber, 0);
        }

        public CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber)
        {
            return CountInbox(startdate, enddate, linenumber, 0);
        }

        public CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber, int isread)
        {
            var path = GetApiPath("sms", "countoutbox", "json");
            var param = new Dictionary<string, object>
        {
            {"startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate)},
            {"enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate)},
            {"linenumber", linenumber},
            {"isread", isread}
        };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnCountInbox>(responsebody);
            return l.entries[0];
        }

        public List<CountPostalCodeResult> CountPostalCode(long postalcode)
        {
            String path = GetApiPath("sms", "countpostalcode", "json");
            var param = new Dictionary<string, object> { { "postalcode", postalcode } };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnCountPostalCode>(responsebody);
            return l.entries;
        }

        public List<SendResult> SendByPostalCode(long postalcode, String sender, String message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount)
        {
            return SendByPostalCode(postalcode, sender, message, mcistartIndex, mcicount, mtnstartindex, mtncount, DateTime.MinValue);
        }

        public List<SendResult> SendByPostalCode(long postalcode, String sender, String message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount, DateTime date)
        {
            var path = GetApiPath("sms", "sendbypostalcode", "json");
            var param = new Dictionary<string, object>
        {
            {"postalcode", postalcode},
            {"sender", sender},
            {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
            {"mcistartIndex", mcistartIndex},
            {"mcicount", mcicount},
            {"mtnstartindex", mtnstartindex},
            {"mtncount", mtncount},
            {"date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date)}
        };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnSend>(responsebody);
            return l.entries;
        }

        public AccountInfoResult AccountInfo()
        {
            var path = GetApiPath("account", "info", "json");
            var responsebody = Execute(path, null);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnAccountInfo>(responsebody);
            return l.entries;
        }

        public AccountConfigResult AccountConfig(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed)
        {
            var path = GetApiPath("account", "config", "json");
            var param = new Dictionary<string, object>
        {
            {"apilogs", apilogs},
            {"dailyreport", dailyreport},
            {"debugmode", debugmode},
            {"defaultsender", defaultsender},
            {"mincreditalarm", mincreditalarm},
            {"resendfailed", resendfailed}
        };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnAccountConfig>(responsebody);
            return l.entries;
        }

        public SendResult VerifyLookup(string receptor, string token, string template)
        {
            return VerifyLookup(receptor, token, null, null, template, VerifyLookupType.Sms);
        }
        public SendResult VerifyLookup(string receptor, string token, string template, VerifyLookupType type)
        {
            return VerifyLookup(receptor, token, null, null, template, type);
        }
        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template)
        {
            return VerifyLookup(receptor, token, token2, token3, template, VerifyLookupType.Sms);
        }
        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string template)
        {
            return VerifyLookup(receptor, token, token2, token3, token10, template, VerifyLookupType.Sms);
        }
        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template, VerifyLookupType type)
        {
            return VerifyLookup(receptor, token, token2, token3, null, template, type);
        }

        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string template, VerifyLookupType type)
        {
            return VerifyLookup(receptor, token, token2, token3, token10, null, template, type);
        }

        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type)
        {
            var path = GetApiPath("verify", "lookup", "json");
            var param = new Dictionary<string, object>
        {
            {"receptor", receptor},
            {"template", template},
            {"token", token},
            {"token2", token2},
            {"token3", token3},
            {"token10", token10},
            {"token20", token20},
            {"type", type},
        };
            var responsebody = Execute(path, param);
            var jsonSerialiser = new JavaScriptSerializer();
            var l = jsonSerialiser.Deserialize<ReturnSend>(responsebody);
            return l.entries[0];
        }
        
        
        #region << CallMakeTTS >>

        public SendResult CallMakeTTS(string message, string receptor)
        {
            return CallMakeTTS(message, new List<string> { receptor }, null, null)[0];
        }
        public List<SendResult> CallMakeTTS(string message, List<string> receptor)
        {
            return CallMakeTTS(message, receptor, null, null);
        }

        public List<SendResult> CallMakeTTS(string message, List<string> receptor, DateTime? date, List<string> localid)
        {
            var path = GetApiPath("call", "maketts", "json");
            var param = new Dictionary<string, object>
            {
                {"receptor", StringHelper.Join(",", receptor.ToArray())},
                {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
            };
            if (date != null)
                param.Add("date", DateHelper.DateTimeToUnixTimestamp(date.Value));
            if (localid != null && localid.Count > 0)
                param.Add("localid", StringHelper.Join(",", localid.ToArray()));
            var responseBody = Execute(path, param);

            return JsonSerialiser.Deserialize<ReturnSend>(responseBody).entries;
        }

        #endregion << CallMakeTTS >>

    }
}