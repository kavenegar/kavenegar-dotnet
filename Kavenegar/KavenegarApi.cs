using Kavenegar.Exceptions;
using Kavenegar.Models;
using Kavenegar.Models.Enums;
using Kavenegar.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

#if NETSTANDARD2_0

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

#endif

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

        public KavenegarApi(string apikey)
        {
            _apikey = apikey;
        }

        public string ApiKey
        {
            set => _apikey = value;
            get => _apikey;
        }

        public int ReturnCode => _returnCode;

        public string ReturnMessage => _returnMessage;

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
                JsonConvert.DeserializeObject<ReturnResult>(responseBody);
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
                    var result = JsonConvert.DeserializeObject<ReturnResult>(responseBody);
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

        public SendResult Send(string sender, string receptor, string message)
        {
            return Send(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue);
        }

        public SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date)
        {
            List<string> receptors = new List<string> { receptor };
            return Send(sender, receptors, message, type, date)[0];
        }

        public List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date)
        {
            return Send(sender, receptor, message, type, date, null);
        }

        public SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date, string localid)
        {
            var receptors = new List<string> { receptor };
            var localids = new List<string> { localid };
            return Send(sender, receptors, message, type, date, localids)[0];
        }

        public SendResult Send(string sender, string receptor, string message, string localid)
        {
            return Send(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue, localid);
        }

        public List<SendResult> Send(string sender, List<string> receptors, string message, string localid)
        {
            List<string> localids = new List<string>();
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
            var l = JsonConvert.DeserializeObject<ReturnSend>(responseBody);
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
            var senders = new List<string>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }
            List<MessageType> types = new List<MessageType>();
            for (var i = 0; i <= senders.Count - 1; i++)
            {
                types.Add(MessageType.MobileMemory);
            }
            return SendArray(senders, receptors, messages, types, date, new List<string>() { localmessageids });
        }

        public List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, string localmessageid)
        {
            List<string> senders = new List<string>();
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
            string path = GetApiPath("sms", "sendarray", "json");
            var jsonSenders = JsonConvert.SerializeObject(senders);
            var jsonReceptors = JsonConvert.SerializeObject(receptors);
            var jsonMessages = JsonConvert.SerializeObject(messages);
            var jsonTypes = JsonConvert.SerializeObject(types);
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
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
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
            var l = JsonConvert.DeserializeObject<ReturnStatus>(responsebody);
            if (l.entries == null)
            {
                return new List<StatusResult>();
            }
            return l.entries;
        }

        public StatusResult Status(string messageid)
        {
            var ids = new List<string> { messageid };
            var result = Status(ids);
            return result.Count == 1 ? result[0] : null;
        }

        public List<StatusLocalMessageIdResult> StatusLocalMessageId(List<string> messageids)
        {
            string path = GetApiPath("sms", "statuslocalmessageid", "json");
            var param = new Dictionary<string, object> { { "localid", StringHelper.Join(",", messageids.ToArray()) } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnStatusLocalMessageId>(responsebody);
            return l.entries;
        }

        public StatusLocalMessageIdResult StatusLocalMessageId(string messageid)
        {
            List<StatusLocalMessageIdResult> result = StatusLocalMessageId(new List<string>() { messageid });
            return result.Count == 1 ? result[0] : null;
        }

        public List<SendResult> Select(List<string> messageids)
        {
            var path = GetApiPath("sms", "select", "json");
            var param = new Dictionary<string, object> { { "messageid", StringHelper.Join(",", messageids.ToArray()) } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }
            return l.entries;
        }

        public SendResult Select(string messageid)
        {
            var ids = new List<string> { messageid };
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

        public List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate, string sender)
        {
            string path = GetApiPath("sms", "selectoutbox", "json");
            var param = new Dictionary<string, object>
         {
             {"startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate)},
             {"enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate)},
             {"sender", sender}
         };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            return l.entries;
        }

        public List<SendResult> LatestOutbox(long pagesize)
        {
            return LatestOutbox(pagesize, "");
        }

        public List<SendResult> LatestOutbox(long pagesize, string sender)
        {
            var path = GetApiPath("sms", "latestoutbox", "json");
            var param = new Dictionary<string, object> { { "pagesize", pagesize }, { "sender", sender } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
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
            var l = JsonConvert.DeserializeObject<ReturnCountOutbox>(responsebody);
            if (l.entries == null || l.entries[0] == null)
            {
                return new CountOutboxResult();
            }
            return l.entries[0];
        }

        public List<StatusResult> Cancel(List<string> ids)
        {
            string path = GetApiPath("sms", "cancel", "json");
            var param = new Dictionary<string, object>
        {
            {"messageid", StringHelper.Join(",", ids.ToArray())}
        };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnStatus>(responsebody);
            return l.entries;
        }

        public StatusResult Cancel(string messageid)
        {
            var ids = new List<string> { messageid };
            var result = Cancel(ids);
            return result.Count == 1 ? result[0] : null;
        }

        public List<ReceiveResult> Receive(string line, int isread)
        {
            string path = GetApiPath("sms", "receive", "json");
            var param = new Dictionary<string, object> { { "linenumber", line }, { "isread", isread } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnReceive>(responsebody);
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

        public CountInboxResult CountInbox(DateTime startdate, DateTime enddate, string linenumber)
        {
            return CountInbox(startdate, enddate, linenumber, 0);
        }

        public CountInboxResult CountInbox(DateTime startdate, DateTime enddate, string linenumber, int isread)
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
            var l = JsonConvert.DeserializeObject<ReturnCountInbox>(responsebody);
            return l.entries[0];
        }

        public List<CountPostalCodeResult> CountPostalCode(long postalcode)
        {
            string path = GetApiPath("sms", "countpostalcode", "json");
            var param = new Dictionary<string, object> { { "postalcode", postalcode } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnCountPostalCode>(responsebody);
            return l.entries;
        }

        public List<SendResult> SendByPostalCode(long postalcode, string sender, string message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount)
        {
            return SendByPostalCode(postalcode, sender, message, mcistartIndex, mcicount, mtnstartindex, mtncount, DateTime.MinValue);
        }

        public List<SendResult> SendByPostalCode(long postalcode, string sender, string message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount, DateTime date)
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
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            return l.entries;
        }

        public AccountInfoResult AccountInfo()
        {
            var path = GetApiPath("account", "info", "json");
            var responsebody = Execute(path, null);
            var l = JsonConvert.DeserializeObject<ReturnAccountInfo>(responsebody);
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
            var l = JsonConvert.DeserializeObject<ReturnAccountConfig>(responsebody);
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
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
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

            return JsonConvert.DeserializeObject<ReturnSend>(responseBody).entries;
        }

        #endregion << CallMakeTTS >>

#if NETSTANDARD2_0

        public Task<SendResult> SendAsync(string sender, string receptor, string message)
        {
            return SendAsync(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue);
        }

        public async Task<SendResult> SendAsync(string sender, string receptor, string message, MessageType type, DateTime date)
        {
            var receptors = new List<string> { receptor };

            return (await SendAsync(sender, receptors, message, type, date))[0];
        }

        public Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, MessageType type, DateTime date)
        {
            return SendAsync(sender, receptor, message, type, date, null);
        }

        public async Task<SendResult> SendAsync(string sender, string receptor, string message, MessageType type, DateTime date, string localId)
        {
            var receptors = new List<string> { receptor };
            var localids = new List<string> { localId };

            return (await SendAsync(sender, receptors, message, type, date, localids))[0];
        }

        public Task<SendResult> SendAsync(string sender, string receptor, string message, string localId)
        {
            return SendAsync(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue, localId);
        }

        public Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message)
        {
            return SendAsync(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue);
        }

        public Task<List<SendResult>> SendAsync(string sender, List<string> receptors, string message, string localId)
        {
            var localIds = new List<string>();
            for (var i = 0; i <= receptors.Count - 1; i++)
                localIds.Add(localId);

            return SendAsync(sender, receptors, message, MessageType.MobileMemory, DateTime.MinValue, localIds);
        }

        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, MessageType type, DateTime date, List<string> localids)
        {
            var path = GetApiPath("sms", "send", "json");
            var param = new Dictionary<string, object>
            {
                {"sender", WebUtility.UrlEncode(sender)},
                {"receptor", WebUtility.UrlEncode(StringHelper.Join(",", receptor.ToArray()))},
                {"message", WebUtility.UrlEncode(message)},
                {"type", (int) type},
                {"date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date)}
            };

            if (localids != null && localids.Count > 0)
                param.Add("localid", StringHelper.Join(",", localids.ToArray()));

            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnSend>(responseBody);
            return result.entries;
        }

        public Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages)
        {
            var types = new List<MessageType>();
            for (var i = 0; i <= senders.Count - 1; i++)
                types.Add(MessageType.MobileMemory);

            return SendArrayAsync(senders, receptors, messages, types, DateTime.MinValue, null);
        }

        public Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date)
        {
            var senders = new List<string>();
            for (var i = 0; i < receptors.Count; i++)
                senders.Add(sender);

            var types = new List<MessageType>();
            for (var i = 0; i <= senders.Count - 1; i++)
                types.Add(MessageType.MobileMemory);

            return SendArrayAsync(senders, receptors, messages, types, date, null);
        }

        public Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date, string localmessageids)
        {
            var senders = new List<string>();
            for (var i = 0; i < receptors.Count; i++)
                senders.Add(sender);

            var types = new List<MessageType>();
            for (var i = 0; i <= senders.Count - 1; i++)
                types.Add(MessageType.MobileMemory);

            return SendArrayAsync(senders, receptors, messages, types, date, new List<string>() { localmessageids });
        }

        public Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, string localmessageid)
        {
            var senders = new List<string>();
            for (var i = 0; i < receptors.Count; i++)
                senders.Add(sender);

            return SendArrayAsync(senders, receptors, messages, localmessageid);
        }

        public Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages, string localmessageid)
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
            return SendArrayAsync(senders, receptors, messages, types, DateTime.MinValue, localmessageids);
        }

        public async Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages, List<MessageType> types, DateTime date, List<string> localmessageids)
        {
            var path = GetApiPath("sms", "sendarray", "json");
            var jsonSenders = JsonConvert.SerializeObject(senders);
            var jsonReceptors = JsonConvert.SerializeObject(receptors);
            var jsonMessages = JsonConvert.SerializeObject(messages);
            var jsonTypes = JsonConvert.SerializeObject(types);
            var param = new Dictionary<string, object>
            {
                {"message", jsonMessages},
                {"sender", jsonSenders},
                {"receptor", jsonReceptors},
                {"type", jsonTypes},
                {"date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date)}
            };

            if (localmessageids != null && localmessageids.Count > 0)
                param.Add("localmessageids", StringHelper.Join(",", localmessageids.ToArray()));

            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnSend>(responseBody);
            if (result.entries == null)
                return new List<SendResult>();

            return result.entries;
        }

        public async Task<StatusResult> StatusAsync(string messageId)
        {
            var ids = new List<string> { messageId };
            var result = await StatusAsync(ids);
            return result.Count == 1 ? result[0] : null;
        }

        public async Task<List<StatusResult>> StatusAsync(List<string> messageIds)
        {
            var path = GetApiPath("sms", "status", "json");
            var param = new Dictionary<string, object>
            {
                {"messageid", StringHelper.Join(",", messageIds.ToArray())}
            };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnStatus>(responseBody);
            if (result.entries == null)
                return new List<StatusResult>();

            return result.entries;
        }

        public async Task<StatusLocalMessageIdResult> StatusLocalMessageIdAsync(string messageId)
        {
            var result = await StatusLocalMessageIdAsync(new List<string>() { messageId });
            return result.Count == 1 ? result[0] : null;
        }

        public async Task<List<StatusLocalMessageIdResult>> StatusLocalMessageIdAsync(List<string> messageIds)
        {
            var path = GetApiPath("sms", "statuslocalmessageid", "json");
            var param = new Dictionary<string, object> { { "localid", StringHelper.Join(",", messageIds.ToArray()) } };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnStatusLocalMessageId>(responseBody);

            return result.entries;
        }

        public async Task<SendResult> SelectAsync(string messageId)
        {
            var ids = new List<string> { messageId };
            var result = await SelectAsync(ids);

            return result.Count == 1 ? result[0] : null;
        }

        public async Task<List<SendResult>> SelectAsync(List<string> messageIds)
        {
            var path = GetApiPath("sms", "select", "json");
            var param = new Dictionary<string, object> { { "messageid", StringHelper.Join(",", messageIds.ToArray()) } };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnSend>(responseBody);
            if (result.entries == null)
                return new List<SendResult>();

            return result.entries;
        }

        public Task<List<SendResult>> SelectOutboxAsync(DateTime startDate)
        {
            return SelectOutboxAsync(startDate, DateTime.MaxValue);
        }

        public Task<List<SendResult>> SelectOutboxAsync(DateTime startDate, DateTime endDate)
        {
            return SelectOutboxAsync(startDate, endDate, null);
        }

        public async Task<List<SendResult>> SelectOutboxAsync(DateTime startDate, DateTime endDate, string sender)
        {
            string path = GetApiPath("sms", "selectoutbox", "json");
            var param = new Dictionary<string, object>
             {
                 {"startdate", startDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate)},
                 {"enddate", endDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate)},
                 {"sender", sender}
             };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnSend>(responseBody);

            return result.entries;
        }

        public Task<List<SendResult>> LatestOutboxAsync(long pageSize)
        {
            return LatestOutboxAsync(pageSize, "");
        }

        public async Task<List<SendResult>> LatestOutboxAsync(long pageSize, string sender)
        {
            var path = GetApiPath("sms", "latestoutbox", "json");
            var param = new Dictionary<string, object> { { "pagesize", pageSize }, { "sender", sender } };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnSend>(responseBody);

            return result.entries;
        }

        public Task<CountOutboxResult> CountOutboxAsync(DateTime startDate)
        {
            return CountOutboxAsync(startDate, DateTime.MaxValue, 10);
        }

        public Task<CountOutboxResult> CountOutboxAsync(DateTime startDate, DateTime endDate)
        {
            return CountOutboxAsync(startDate, endDate, 0);
        }

        public async Task<CountOutboxResult> CountOutboxAsync(DateTime startDate, DateTime endDate, int status)
        {
            var path = GetApiPath("sms", "countoutbox", "json");
            var param = new Dictionary<string, object>
             {
                 {"startdate", startDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate)},
                 {"enddate", endDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate)},
                 {"status", status}
             };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnCountOutbox>(responseBody);
            if (result.entries?[0] == null)
                return new CountOutboxResult();

            return result.entries[0];
        }

        public async Task<StatusResult> CancelAsync(string messageId)
        {
            var ids = new List<string> { messageId };
            var result = await CancelAsync(ids);

            return result.Count == 1 ? result[0] : null;
        }

        public async Task<List<StatusResult>> CancelAsync(List<string> ids)
        {
            var path = GetApiPath("sms", "cancel", "json");
            var param = new Dictionary<string, object>
            {
                {"messageid", StringHelper.Join(",", ids.ToArray())}
            };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnStatus>(responseBody);

            return result.entries;
        }

        public async Task<List<ReceiveResult>> ReceiveAsync(string line, int isRead)
        {
            var path = GetApiPath("sms", "receive", "json");
            var param = new Dictionary<string, object> { { "linenumber", line }, { "isread", isRead } };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnReceive>(responseBody);
            if (result.entries == null)
                return new List<ReceiveResult>();

            return result.entries;
        }

        public Task<CountInboxResult> CountInboxACountInboxAsync(DateTime startDate, string lineNumber)
        {
            return CountInboxAsync(startDate, DateTime.MaxValue, lineNumber, 0);
        }

        public Task<CountInboxResult> CountInboxAsync(DateTime startDate, DateTime endDate, string lineNumber)
        {
            return CountInboxAsync(startDate, endDate, lineNumber, 0);
        }

        public async Task<CountInboxResult> CountInboxAsync(DateTime startDate, DateTime endDate, string lineNumber, int isRead)
        {
            var path = GetApiPath("sms", "countoutbox", "json");
            var param = new Dictionary<string, object>
            {
                {"startdate", startDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate)},
                {"enddate", endDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate)},
                {"linenumber", lineNumber},
                {"isread", isRead}
            };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnCountInbox>(responseBody);

            return result.entries[0];
        }

        public async Task<List<CountPostalCodeResult>> CountPostalCodeAsync(long postalCode)
        {
            var path = GetApiPath("sms", "countpostalcode", "json");
            var param = new Dictionary<string, object> { { "postalcode", postalCode } };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnCountPostalCode>(responseBody);

            return result.entries;
        }

        public Task<List<SendResult>> SendByPostalCodeAsync(long postalcode, string sender, string message, long mciStartIndex, long mciCount, long mtnStartIndex, long mtnCount)
        {
            return SendByPostalCodeAsync(postalcode, sender, message, mciStartIndex, mciCount, mtnStartIndex, mtnCount, DateTime.MinValue);
        }

        public async Task<List<SendResult>> SendByPostalCodeAsync(long postalcode, string sender, string message, long mciStartIndex, long mciCount, long mtnStartIndex, long mtnCount, DateTime date)
        {
            var path = GetApiPath("sms", "sendbypostalcode", "json");
            var param = new Dictionary<string, object>
            {
                {"postalcode", postalcode},
                {"sender", sender},
                {"message", WebUtility.UrlEncode(message)},
                {"mcistartIndex", mciStartIndex},
                {"mcicount", mciCount},
                {"mtnstartindex", mtnStartIndex},
                {"mtncount", mtnCount},
                {"date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date)}
            };
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnSend>(responseBody);

            return result.entries;
        }

        public async Task<AccountInfoResult> AccountInfoAsync()
        {
            var path = GetApiPath("account", "info", "json");
            var responseBody = await ExecuteAsync(path, null);
            var result = JsonConvert.DeserializeObject<ReturnAccountInfo>(responseBody);

            return result.entries;
        }

        public async Task<AccountConfigResult> AccountConfigAsync(string apiLogs, string dailyReport, string debugMode, string defaultSender, int? minCreditAlarm, string resendFailed)
        {
            var path = GetApiPath("account", "config", "json");
            var param = new Dictionary<string, object>
            {
                {"apilogs", apiLogs},
                {"dailyreport", dailyReport},
                {"debugmode", debugMode},
                {"defaultsender", defaultSender},
                {"mincreditalarm", minCreditAlarm},
                {"resendfailed", resendFailed}
            };

            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnAccountConfig>(responseBody);

            return result.entries;
        }

        public Task<SendResult> VerifyLookupAsync(string receptor, string token, string template)
        {
            return VerifyLookupAsync(receptor, token, null, null, template, VerifyLookupType.Sms);
        }

        public Task<SendResult> VerifyLookupAsync(string receptor, string token, string template, VerifyLookupType type)
        {
            return VerifyLookupAsync(receptor, token, null, null, template, type);
        }

        public Task<SendResult> VerifyLookupAsyncAsync(string receptor, string token, string token2, string token3, string template)
        {
            return VerifyLookupAsync(receptor, token, token2, token3, template, VerifyLookupType.Sms);
        }

        public Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string template)
        {
            return VerifyLookupAsync(receptor, token, token2, token3, token10, template, VerifyLookupType.Sms);
        }

        public Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string template, VerifyLookupType type)
        {
            return VerifyLookupAsync(receptor, token, token2, token3, null, template, type);
        }

        public Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string template, VerifyLookupType type)
        {
            return VerifyLookupAsync(receptor, token, token2, token3, token10, null, template, type);
        }

        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type)
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
            var responseBody = await ExecuteAsync(path, param);
            var result = JsonConvert.DeserializeObject<ReturnSend>(responseBody);

            return result.entries[0];
        }

        public async Task<SendResult> CallMakeTTSAsync(string message, string receptor)
        {
            return (await CallMakeTTSAsync(message, new List<string> { receptor }, null, null))[0];
        }

        public Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor)
        {
            return CallMakeTTSAsync(message, receptor, null, null);
        }

        public async Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor, DateTime? date, List<string> localId)
        {
            var path = GetApiPath("call", "maketts", "json");
            var param = new Dictionary<string, object>
            {
                {"receptor", StringHelper.Join(",", receptor.ToArray())},
                {"message", WebUtility.UrlEncode(message)},
            };

            if (date != null)
                param.Add("date", DateHelper.DateTimeToUnixTimestamp(date.Value));

            if (localId != null && localId.Count > 0)
                param.Add("localid", StringHelper.Join(",", localId.ToArray()));

            var responseBody = await ExecuteAsync(path, param);

            return JsonConvert.DeserializeObject<ReturnSend>(responseBody).entries;
        }

        private static async Task<string> ExecuteAsync(string path, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            var keyValues = parameters?.Select(x => new KeyValuePair<string, string>(x.Key, x.Value?.ToString()));
            var content = keyValues == null ? null : new FormUrlEncodedContent(keyValues);
            var request = new HttpRequestMessage(HttpMethod.Post, path) { Content = content };

            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await client.SendAsync(request, cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();

                    var body = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                        return body;

                    var result = JsonConvert.DeserializeObject<ReturnResult>(body);
                    throw new ApiException(result.Return.message, result.Return.status);
                }
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (response != null)
                    throw new HttpException(ex.Message, (int)response.StatusCode);

                throw new HttpException(ex.Message, 500);
            }
        }

#endif
    }
}