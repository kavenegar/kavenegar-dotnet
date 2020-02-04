using Kavenegar.Exceptions;
using Kavenegar.Models;
using Kavenegar.Models.Enums;
using Kavenegar.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Kavenegar.Models.Internal;

namespace Kavenegar
{
    public class KavenegarApi : IKavenegarApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiPath = "https://api.kavenegar.com/v1/{0}/{1}/{2}.{3}";

        public KavenegarApi(
            IOptions<KavenegarSettings> options, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            ApiKey = options.Value.ApiKey;
        }

        public string ApiKey { get; }

        private string GetApiPath(string @base, string method, string output)
        {
            return string.Format(ApiPath, ApiKey, @base, method, output);
        }

        private async Task<string> ExecuteAsync(string path, Dictionary<string, object> @params, CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient(Constants.HttpClientName);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var keyValues = @params?.Select(x => new KeyValuePair<string, string>(x.Key, x.Value?.ToString()));
            var content = keyValues == null ? null : new FormUrlEncodedContent(keyValues);
            var request = new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = content
            };
            var resp = await client.SendAsync(request, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            if (!resp.IsSuccessStatusCode)
            {
                try
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    var result = System.Text.Json.JsonSerializer.Deserialize<ReturnResult>(body);
                    throw new ApiException(result.Return.message, result.Return.status);
                }
                catch (ApiException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new HttpException(ex.Message, (int)resp.StatusCode);
                }
            }

            return await resp.Content.ReadAsStringAsync();
        }

        #region Send

        public async Task<SendResult> SendAsync(string sender, string receptor, string message, CancellationToken cancellationToken = default)
        {
            return await SendAsync(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue, cancellationToken);
        }
        public async Task<SendResult> SendAsync(string sender, string receptor, string message, MessageType type, DateTime date, CancellationToken cancellationToken = default)
        {
            List<string> receptors = new List<string> { receptor };
            return (await SendAsync(sender, receptors, message, type, date, cancellationToken))[0];
        }
        public async Task<SendResult> SendAsync(string sender, string receptor, string message, MessageType type, DateTime date, string localid, CancellationToken cancellationToken = default)
        {
            var receptors = new List<string> { receptor };
            var localids = new List<string> { localid };
            return (await SendAsync(sender, receptors, message, type, date, localids, cancellationToken))[0];
        }
        public async Task<SendResult> SendAsync(string sender, string receptor, string message, string localid, CancellationToken cancellationToken = default)
        {
            return await SendAsync(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue, localid, cancellationToken);
        }
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, CancellationToken cancellationToken = default)
        {
            return await SendAsync(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue, cancellationToken);
        }
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptors, string message, string localid, CancellationToken cancellationToken = default)
        {
            List<string> localids = new List<string>();
            for (var i = 0; i <= receptors.Count - 1; i++)
            {
                localids.Add(localid);
            }
            return await SendAsync(sender, receptors, message, MessageType.MobileMemory, DateTime.MinValue, localids, cancellationToken);
        }
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, MessageType type, DateTime date, CancellationToken cancellationToken = default)
        {
            return await SendAsync(sender, receptor, message, type, date, null, cancellationToken);
        }
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, MessageType type, DateTime date, List<string> localids, CancellationToken cancellationToken = default)
        {
            var path = GetApiPath("sms", "send", "json");
            var param = new Dictionary<string, object>
        {
            {"sender", System.Web.HttpUtility.UrlEncode(sender)},
            {"receptor", System.Web.HttpUtility.UrlEncode(string.Join(",", receptor.ToArray()))},
            {"message", System.Web.HttpUtility.UrlEncode(message)},
            {"type", (int) type},
            {"date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date)}
        };
            if (localids != null && localids.Count > 0)
            {
                param.Add("localid", string.Join(",", localids.ToArray()));
            }
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnSend>(responseBody);
            return l.entries;
        }
        public async Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages, CancellationToken cancellationToken = default)
        {
            var types = new List<MessageType>();
            for (var i = 0; i <= senders.Count - 1; i++)
            {
                types.Add(MessageType.MobileMemory);
            }
            return await SendArrayAsync(senders, receptors, messages, types, DateTime.MinValue, null, cancellationToken);
        }
        public async Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, string localmessageid, CancellationToken cancellationToken = default)
        {
            List<string> senders = new List<string>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }

            return await SendArrayAsync(senders, receptors, messages, localmessageid, cancellationToken);
        }
        public async Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date, CancellationToken cancellationToken = default)
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
            return await SendArrayAsync(senders, receptors, messages, types, date, null, cancellationToken);
        }
        public async Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date, string localmessageids, CancellationToken cancellationToken = default)
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
            return await SendArrayAsync(senders, receptors, messages, types, date, new List<string>() { localmessageids }, cancellationToken);
        }
        public async Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages, string localmessageid, CancellationToken cancellationToken = default)
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
            return await SendArrayAsync(senders, receptors, messages, types, DateTime.MinValue, localmessageids, cancellationToken);
        }
        public async Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages, List<MessageType> types, DateTime date, List<string> localmessageids, CancellationToken cancellationToken = default)
        {
            string path = GetApiPath("sms", "sendarray", "json");
            var jsonSenders = System.Text.Json.JsonSerializer.Serialize(senders);
            var jsonReceptors = System.Text.Json.JsonSerializer.Serialize(receptors);
            var jsonMessages = System.Text.Json.JsonSerializer.Serialize(messages);
            var jsonTypes = System.Text.Json.JsonSerializer.Serialize(types);
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
                param.Add("localmessageids", string.Join(",", localmessageids.ToArray()));
            }

            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnSend>(responseBody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }
            return l.entries;
        }

        #endregion

        #region Status

        public async Task<StatusResult> StatusAsync(string messageid, CancellationToken cancellationToken = default)
        {
            var ids = new List<string> { messageid };
            var result = await StatusAsync(ids, cancellationToken);
            return result.Count == 1 ? result[0] : null;
        }
        public async Task<List<StatusResult>> StatusAsync(List<string> messageids, CancellationToken cancellationToken = default)
        {
            string path = GetApiPath("sms", "status", "json");
            var param = new Dictionary<string, object>
        {
            {"messageid", string.Join(",", messageids.ToArray())}
        };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnStatus>(responseBody);
            if (l.entries == null)
            {
                return new List<StatusResult>();
            }
            return l.entries;
        }

        public async Task<List<StatusLocalMessageIdResult>> StatusLocalMessageIdAsync(List<string> messageids, CancellationToken cancellationToken = default)
        {
            string path = GetApiPath("sms", "statuslocalmessageid", "json");
            var param = new Dictionary<string, object> { { "localid", string.Join(",", messageids.ToArray()) } };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnStatusLocalMessageId>(responseBody);
            return l.entries;
        }

        public async Task<StatusLocalMessageIdResult> StatusLocalMessageIdAsync(string messageid, CancellationToken cancellationToken = default)
        {
            List<StatusLocalMessageIdResult> result = await StatusLocalMessageIdAsync(new List<string>() { messageid }, cancellationToken);
            return result.Count == 1 ? result[0] : null;
        }

        #endregion

        public async Task<SendResult> SelectAsync(string messageid, CancellationToken cancellationToken = default)
        {
            var ids = new List<string> { messageid };
            var result = await SelectAsync(ids, cancellationToken);
            return result.Count == 1 ? result[0] : null;
        }

        public async Task<List<SendResult>> SelectAsync(List<string> messageids, CancellationToken cancellationToken = default)
        {
            var path = GetApiPath("sms", "select", "json");
            var param = new Dictionary<string, object> { { "messageid", string.Join(",", messageids.ToArray()) } };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnSend>(responseBody);
            return l.entries ?? new List<SendResult>();
        }

        public async Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, CancellationToken cancellationToken = default)
        {
            return await SelectOutboxAsync(startdate, DateTime.MaxValue, cancellationToken);
        }

        public async Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate, CancellationToken cancellationToken = default)
        {
            return await SelectOutboxAsync(startdate, enddate, null, cancellationToken);
        }

        public async Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate, string sender, CancellationToken cancellationToken = default)
        {
            string path = GetApiPath("sms", "selectoutbox", "json");
            var param = new Dictionary<string, object>
         {
             {"startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate)},
             {"enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate)},
             {"sender", sender}
         };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnSend>(responseBody);
            return l.entries;
        }

        public async Task<List<SendResult>> LatestOutboxAsync(long pagesize, CancellationToken cancellationToken = default)
        {
            return await LatestOutboxAsync(pagesize, string.Empty, cancellationToken);
        }

        public async Task<List<SendResult>> LatestOutboxAsync(long pagesize, string sender, CancellationToken cancellationToken = default)
        {
            var path = GetApiPath("sms", "latestoutbox", "json");
            var param = new Dictionary<string, object> { { "pagesize", pagesize }, { "sender", sender } };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnSend>(responseBody);
            return l.entries;
        }

        public async Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, CancellationToken cancellationToken = default)
        {
            return await CountOutboxAsync(startdate, DateTime.MaxValue, 10, cancellationToken);
        }

        public async Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate, CancellationToken cancellationToken = default)
        {
            return await CountOutboxAsync(startdate, enddate, 0, cancellationToken);
        }

        public async Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate, int status, CancellationToken cancellationToken = default)
        {
            string path = GetApiPath("sms", "countoutbox", "json");
            var param = new Dictionary<string, object>
         {
             {"startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate)},
             {"enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate)},
             {"status", status}
         };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnCountOutbox>(responseBody);
            if (l.entries?[0] == null)
            {
                return new CountOutboxResult();
            }
            return l.entries[0];
        }

        public async Task<List<StatusResult>> CancelAsync(List<string> ids, CancellationToken cancellationToken = default)
        {
            string path = GetApiPath("sms", "cancel", "json");
            var param = new Dictionary<string, object>
        {
            {"messageid", string.Join(",", ids.ToArray())}
        };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnStatus>(responseBody);
            return l.entries;
        }

        public async Task<StatusResult> CancelAsync(string messageid, CancellationToken cancellationToken = default)
        {
            var ids = new List<string> { messageid };
            var result = await CancelAsync(ids, cancellationToken);
            return result.Count == 1 ? result[0] : null;
        }

        public async Task<List<ReceiveResult>> ReceiveAsync(string line, int isread, CancellationToken cancellationToken = default)
        {
            string path = GetApiPath("sms", "receive", "json");
            var param = new Dictionary<string, object> { { "linenumber", line }, { "isread", isread } };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnReceive>(responseBody);
            if (l.entries == null)
            {
                return new List<ReceiveResult>();
            }
            return l.entries;
        }

        public async Task<CountInboxResult> CountInboxAsync(DateTime startdate, string linenumber, CancellationToken cancellationToken = default)
        {
            return await CountInboxAsync(startdate, DateTime.MaxValue, linenumber, 0, cancellationToken);
        }

        public async Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, string linenumber, CancellationToken cancellationToken = default)
        {
            return await CountInboxAsync(startdate, enddate, linenumber, 0, cancellationToken);
        }

        public async Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, string linenumber, int isread, CancellationToken cancellationToken = default)
        {
            var path = GetApiPath("sms", "countoutbox", "json");
            var param = new Dictionary<string, object>
        {
            {"startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate)},
            {"enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate)},
            {"linenumber", linenumber},
            {"isread", isread}
        };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnCountInbox>(responseBody);
            return l.entries[0];
        }

        public async Task<List<CountPostalCodeResult>> CountPostalCodeAsync(long postalcode, CancellationToken cancellationToken = default)
        {
            string path = GetApiPath("sms", "countpostalcode", "json");
            var param = new Dictionary<string, object> { { "postalcode", postalcode } };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnCountPostalCode>(responseBody);
            return l.entries;
        }

        public async Task<List<SendResult>> SendByPostalCodeAsync(long postalcode, string sender, string message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount, CancellationToken cancellationToken = default)
        {
            return await SendByPostalCodeAsync(postalcode, sender, message, mcistartIndex, mcicount, mtnstartindex, mtncount, DateTime.MinValue, cancellationToken);
        }

        public async Task<List<SendResult>> SendByPostalCodeAsync(long postalcode, string sender, string message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount, DateTime date, CancellationToken cancellationToken = default)
        {
            var path = GetApiPath("sms", "sendbypostalcode", "json");
            var param = new Dictionary<string, object>
        {
            {"postalcode", postalcode},
            {"sender", sender},
            {"message", System.Web.HttpUtility.UrlEncode(message)},
            {"mcistartIndex", mcistartIndex},
            {"mcicount", mcicount},
            {"mtnstartindex", mtnstartindex},
            {"mtncount", mtncount},
            {"date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date)}
        };
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnSend>(responseBody);
            return l.entries;
        }

        #region Account

        public async Task<AccountInfoResult> AccountInfoAsync(CancellationToken cancellationToken = default)
        {
            var path = GetApiPath("account", "info", "json");
            var responseBody = await ExecuteAsync(path, null, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnAccountInfo>(responseBody);
            return l.entries;
        }
        public async Task<AccountConfigResult> AccountConfigAsync(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed, CancellationToken cancellationToken = default)
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
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnAccountConfig>(responseBody);
            return l.entries;
        }

        #endregion

        #region Verify

        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string template, CancellationToken cancellationToken = default)
        {
            return await VerifyLookupAsync(receptor, token, null, null, template, VerifyLookupType.Sms, cancellationToken);
        }
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string template, VerifyLookupType type, CancellationToken cancellationToken = default)
        {
            return await VerifyLookupAsync(receptor, token, null, null, template, type, cancellationToken);
        }
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string template, CancellationToken cancellationToken = default)
        {
            return await VerifyLookupAsync(receptor, token, token2, token3, template, VerifyLookupType.Sms, cancellationToken);
        }
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string template, CancellationToken cancellationToken = default)
        {
            return await VerifyLookupAsync(receptor, token, token2, token3, token10, template, VerifyLookupType.Sms, cancellationToken);
        }
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string template, VerifyLookupType type, CancellationToken cancellationToken = default)
        {
            return await VerifyLookupAsync(receptor, token, token2, token3, null, template, type, cancellationToken);
        }
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string template, VerifyLookupType type, CancellationToken cancellationToken = default)
        {
            return await VerifyLookupAsync(receptor, token, token2, token3, token10, null, template, type, cancellationToken);
        }
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type, CancellationToken cancellationToken = default)
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
            var responseBody = await ExecuteAsync(path, param, cancellationToken);
            var l = System.Text.Json.JsonSerializer.Deserialize<ReturnSend>(responseBody);
            return l.entries[0];
        }

        #endregion

        #region << CallMakeTTS >>

        public async Task<SendResult> CallMakeTTSAsync(string message, string receptor, CancellationToken cancellationToken = default)
        {
            return (await CallMakeTTSAsync(message, new List<string> { receptor }, null, null, cancellationToken))[0];
        }
        public async Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor, CancellationToken cancellationToken = default)
        {
            return await CallMakeTTSAsync(message, receptor, null, null, cancellationToken);
        }
        public async Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor, DateTime? date, List<string> localid, CancellationToken cancellationToken = default)
        {
            var path = GetApiPath("call", "maketts", "json");
            var param = new Dictionary<string, object>
            {
                {"receptor", string.Join(",", receptor.ToArray())},
                {"message", System.Web.HttpUtility.UrlEncode(message)},
            };
            if (date != null)
                param.Add("date", DateHelper.DateTimeToUnixTimestamp(date.Value));
            if (localid != null && localid.Count > 0)
                param.Add("localid", string.Join(",", localid.ToArray()));
            var responseBody = await ExecuteAsync(path, param, cancellationToken);

            return System.Text.Json.JsonSerializer.Deserialize<ReturnSend>(responseBody).entries;
        }

        #endregion << CallMakeTTS >>

    }
}