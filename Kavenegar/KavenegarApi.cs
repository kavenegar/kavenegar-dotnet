using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
#if !NET35
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
#endif
using System.Text;
using HttpUtility = System.Web.HttpUtility;
using Kavenegar.Exceptions;
using Kavenegar.Models;
using Kavenegar.Models.Enums;
using Kavenegar.Utils;
using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

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
        public CountOutboxResult entries { get; set; }
    }

    internal class ReturnCountInbox
    {
        public Result result { get; set; }
        public CountInboxResult entries { get; set; }
    }

    internal class ReturnAccountInfo
    {
        public Result result { get; set; }
        public AccountInfoResult entries { get; set; }
    }

    internal class ReturnAccountConfig
    {
        public Result result { get; set; }
        public List<AccountConfigResult> entries { get; set; }
    }

    internal class ReturnInboxPaged
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public Dictionary<string, string> metadata { get; set; }
        public List<ReceiveResult> entries { get; set; }
    }

    internal class ReturnGroupSendReport
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public List<GroupSendReportResult> entries { get; set; }
    }

    internal class ReturnSubClient
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public SubClientResult entries { get; set; }
    }

    internal class ReturnSubClientsList
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public List<SubClientResult> entries { get; set; }
    }

    internal class ReturnBlacklist
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public List<BlacklistResult> entries { get; set; }
    }

    internal class ReturnRemoveBlacklist
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public RemoveBlacklistResult entries { get; set; }
    }

    internal class ReturnLineBlockList
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public Dictionary<string, string> metadata { get; set; }
        public List<BlockedLineDetail> entries { get; set; }
    }

    internal class ReturnTemplatesList
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public List<TemplateResult> entries { get; set; }
    }

    internal class ReturnCloneTemplate
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public CloneTemplateResult entries { get; set; }
    }

    internal class ReturnTemplateInfo
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public TemplateInfoResult entries { get; set; }
    }

    internal class ReturnTemplate
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public TemplateResult entries { get; set; }
    }

    internal class ReturnMedia
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public MediaResult entries { get; set; }
    }

    internal class MediaListEntries
    {
        public int page { get; set; }
        public int size { get; set; }
        public long total { get; set; }
        public List<MediaResult> list { get; set; }
    }

    internal class ReturnMediaList
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public MediaListEntries entries { get; set; }
    }

    internal class ReturnMediaDelete
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public MediaDeleteResult entries { get; set; }
    }

    internal class ReturnContactsList
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public List<ContactResult> entries { get; set; }
    }

    internal class ReturnGroupsList
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public List<GroupResult> entries { get; set; }
    }

    internal class ReturnServerDate
    {
        public Result result { get; set; }
        public Result @Return { get; set; }
        public ServerDateResult entries { get; set; }
    }

    public class KavenegarApi : IKavenegarApi
    {
        private string _apikey;

        private string _apipath = "https://api.kavenegar.com/v1/{0}/{1}/{2}.{3}";

        public KavenegarApi(string apikey)
        {
            _apikey = apikey;
        }

        public KavenegarApi(KavenegarOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _apikey = options.ApiKey;
            var baseUrl = options.BaseUrl?.TrimEnd('/') ?? "https://api.kavenegar.com";
            _apipath = $"{baseUrl}/v1/{{0}}/{{1}}/{{2}}.{{3}}";
        }

#if !NET35
        private readonly HttpClient _httpClient;
        private static readonly HttpClient _defaultHttpClient = new HttpClient();

        public KavenegarApi(string apikey, HttpClient httpClient)
        {
            _apikey = apikey;
            _httpClient = httpClient;
        }

        public KavenegarApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public KavenegarApi(KavenegarOptions options, HttpClient httpClient)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _apikey = options.ApiKey;
            var baseUrl = options.BaseUrl?.TrimEnd('/') ?? "https://api.kavenegar.com";
            _apipath = $"{baseUrl}/v1/{{0}}/{{1}}/{{2}}.{{3}}";
            _httpClient = httpClient;
        }
#endif

        public string ApiKey
        {
            set => _apikey = value;
            get => _apikey;
        }


        private string GetApiPath(string _base, string method, string output)
        {
            return string.Format(_apipath, _apikey, _base, method, output);
        }

        private string Execute(string path, Dictionary<string, object> _params, string method = "POST")
        {
#if NET35
            return ExecuteLegacy(path, _params, method);
#else
            var client = _httpClient ?? _defaultHttpClient;
            var requestUri = path;
            HttpContent content = null;

            if (_params != null && _params.Count > 0)
            {
                var postdata = _params.Keys.Aggregate("",
                    (current, key) => current + string.Format("{0}={1}&", key, _params[key]));

                if (method == "GET" || method == "DELETE")
                {
                    requestUri += (requestUri.Contains("?") ? "&" : "?") + postdata.TrimEnd('&');
                }
                else
                {
                    content = new StringContent(postdata.TrimEnd('&'), Encoding.UTF8,
                        "application/x-www-form-urlencoded");
                }
            }

            var request = new HttpRequestMessage(new HttpMethod(method), requestUri);
            if (content != null)
            {
                request.Content = content;
            }

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = client.SendAsync(request).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new HttpException(ex.Message, 0);
            }

            string responseBody = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (!httpResponse.IsSuccessStatusCode)
            {
                ReturnResult result = null;
                try
                {
                    result = JsonConvert.DeserializeObject<ReturnResult>(responseBody);
                }
                catch
                {
                    // Ignore parse errors on bad requests
                }

                if (result != null && result.Return != null)
                {
                    throw new ApiException(result.Return.message, result.Return.status);
                }

                throw new HttpException(httpResponse.ReasonPhrase, (int)httpResponse.StatusCode);
            }

            JsonConvert.DeserializeObject<ReturnResult>(responseBody);
            return responseBody;
#endif
        }

#if !NET35

        private async Task<string> ExecuteAsync(string path, Dictionary<string, object> _params, string method = "POST", CancellationToken cancellationToken = default)
        {
            var client = _httpClient ?? _defaultHttpClient;
            var requestUri = path;
            HttpContent content = null;

            if (_params != null && _params.Count > 0)
            {
                var postdata = _params.Keys.Aggregate("",
                    (current, key) => current + string.Format("{0}={1}&", key, _params[key]));

                if (method == "GET" || method == "DELETE")
                {
                    requestUri += (requestUri.Contains("?") ? "&" : "?") + postdata.TrimEnd('&');
                }
                else
                {
                    content = new StringContent(postdata.TrimEnd('&'), Encoding.UTF8,
                        "application/x-www-form-urlencoded");
                }
            }

            var request = new HttpRequestMessage(new HttpMethod(method), requestUri);
            if (content != null)
            {
                request.Content = content;
            }

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpException(ex.Message, 0);
            }

            string responseBody = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                ReturnResult result = null;
                try
                {
                    result = JsonConvert.DeserializeObject<ReturnResult>(responseBody);
                }
                catch
                {
                    // Ignore parse errors on bad requests
                }

                if (result != null && result.Return != null)
                {
                    throw new ApiException(result.Return.message, result.Return.status);
                }

                throw new HttpException(httpResponse.ReasonPhrase, (int)httpResponse.StatusCode);
            }

            JsonConvert.DeserializeObject<ReturnResult>(responseBody);
            return responseBody;
        }
#endif


        private string ExecuteLegacy(string path, Dictionary<string, object> _params, string method = "POST")
        {
            string responseBody = "";
            string postdata = "";

            byte[] byteArray = new byte[0];
            if (_params != null && _params.Count > 0)
            {
                postdata = _params.Keys.Aggregate(postdata,
                    (current, key) => current + string.Format("{0}={1}&", key, _params[key]));

                if (method == "GET" || method == "DELETE")
                {
                    path += (path.Contains("?") ? "&" : "?") + postdata.TrimEnd('&');
                }
                else
                {
                    byteArray = Encoding.UTF8.GetBytes(postdata);
                }
            }

            var webRequest = (HttpWebRequest)WebRequest.Create(path);
            webRequest.Method = method;
            webRequest.Timeout = -1;
            if (method != "GET" && method != "DELETE")
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                using (Stream webpageStream = webRequest.GetRequestStream())
                {
                    webpageStream.Write(byteArray, 0, byteArray.Length);
                }
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
#if !NET35
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message)
        {
            return await SendAsync(sender, receptor, message, DateTime.MinValue).ConfigureAwait(false);
        }
#endif

        public SendResult Send(string sender, String receptor, string message)
        {
            return Send(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue);
        }
#if !NET35
        public async Task<SendResult> SendAsync(string sender, String receptor, string message)
        {
            return await SendAsync(sender, receptor, message, DateTime.MinValue).ConfigureAwait(false);
        }
#endif

        public SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date)
        {
            List<String> receptors = new List<String> { receptor };
            return Send(sender, receptors, message, type, date)[0];
        }
#if !NET35
        public async Task<SendResult> SendAsync(string sender, string receptor, string message, DateTime date)
        {
            List<String> receptors = new List<String> { receptor };
            return (await SendAsync(sender, receptors, message, date).ConfigureAwait(false))[0];
        }
#endif

        public List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type,
            DateTime date)
        {
            return Send(sender, receptor, message, type, date, null);
        }
#if !NET35
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message,
            DateTime date)
        {
            return await SendAsync(sender, receptor, message, date, null).ConfigureAwait(false);
        }
#endif

        public SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date,
            string localid)
        {
            var receptors = new List<String> { receptor };
            var localids = new List<String> { localid };
            return Send(sender, receptors, message, type, date, localids)[0];
        }
#if !NET35
        public async Task<SendResult> SendAsync(string sender, string receptor, string message, DateTime date,
            string localid)
        {
            var receptors = new List<String> { receptor };
            var localids = new List<String> { localid };
            return (await SendAsync(sender, receptors, message, date, localids).ConfigureAwait(false))[0];
        }
#endif

        public SendResult Send(string sender, string receptor, string message, string localid)
        {
            return Send(sender, receptor, message, MessageType.MobileMemory, DateTime.MinValue, localid);
        }
#if !NET35
        public async Task<SendResult> SendAsync(string sender, string receptor, string message, string localid)
        {
            return await SendAsync(sender, receptor, message, DateTime.MinValue, localid).ConfigureAwait(false);
        }
#endif

        public List<SendResult> Send(string sender, List<string> receptors, string message, string localid)
        {
            List<String> localids = new List<String>();
            for (var i = 0; i <= receptors.Count - 1; i++)
            {
                localids.Add(localid);
            }

            return Send(sender, receptors, message, MessageType.MobileMemory, DateTime.MinValue, localids);
        }
#if !NET35
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptors, string message, string localid)
        {
            List<String> localids = new List<String>();
            for (var i = 0; i <= receptors.Count - 1; i++)
            {
                localids.Add(localid);
            }

            return await SendAsync(sender, receptors, message, DateTime.MinValue, localids).ConfigureAwait(false);
        }
#endif

        public List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type,
            DateTime date, List<string> localids)
        {
            return Send(new SendRequest
            {
                Sender = sender,
                Receptor = receptor,
                Message = message,
                Type = type,
                Date = date,
                LocalIds = localids
            });
        }
#if !NET35
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message,
            DateTime date, List<string> localids)
        {
            return await SendAsync(new SendRequest
            {
                Sender = sender,
                Receptor = receptor,
                Message = message,
                Date = date,
                LocalIds = localids
            });
        }
#endif

        public List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type,
            DateTime date, List<string> localids, string tag = null, string text = null, string hide = null, string localMessageId = null,
            string policy = null, Guid? mediaId = null)
        {
            var path = GetApiPath("sms", "send", "json");
            var param = new Dictionary<string, object>
            {
                { "sender", HttpUtility.UrlEncodeUnicode(sender) },
                { "receptor", HttpUtility.UrlEncodeUnicode(StringHelper.Join(",", receptor.ToArray())) },
                { "message", HttpUtility.UrlEncodeUnicode(message) },
                { "type", (int)type },
                { "date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date) }
            };
            if (localids != null && localids.Count > 0)
            {
                param.Add("localid", StringHelper.Join(",", localids.ToArray()));
            }

            if (!string.IsNullOrEmpty(tag)) param.Add("tag", HttpUtility.UrlEncodeUnicode(tag));
            if (!string.IsNullOrEmpty(text)) param.Add("text", HttpUtility.UrlEncodeUnicode(text));
            if (!string.IsNullOrEmpty(hide)) param.Add("hide", HttpUtility.UrlEncodeUnicode(hide));
            if (!string.IsNullOrEmpty(localMessageId))
                param.Add("localmessageid", HttpUtility.UrlEncodeUnicode(localMessageId));
            if (!string.IsNullOrEmpty(policy)) param.Add("policy", HttpUtility.UrlEncodeUnicode(policy));
            if (mediaId.HasValue) param.Add("mediaid", mediaId.Value.ToString());

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnSend>(responseBody).entries;
        }
#if !NET35
        public async Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message,
            DateTime date, List<string> localids, string tag = null, string text = null, string hide = null, string localMessageId = null,
            string policy = null, Guid? mediaId = null)
        {
            var path = GetApiPath("sms", "send", "json");
            var param = new Dictionary<string, object>
            {
                { "sender", HttpUtility.UrlEncodeUnicode(sender) },
                { "receptor", HttpUtility.UrlEncodeUnicode(StringHelper.Join(",", receptor.ToArray())) },
                { "message", HttpUtility.UrlEncodeUnicode(message) },
                { "date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date) }
            };
            if (localids != null && localids.Count > 0)
            {
                param.Add("localid", StringHelper.Join(",", localids.ToArray()));
            }

            if (!string.IsNullOrEmpty(tag)) param.Add("tag", HttpUtility.UrlEncodeUnicode(tag));
            if (!string.IsNullOrEmpty(text)) param.Add("text", HttpUtility.UrlEncodeUnicode(text));
            if (!string.IsNullOrEmpty(hide)) param.Add("hide", HttpUtility.UrlEncodeUnicode(hide));
            if (!string.IsNullOrEmpty(localMessageId))
                param.Add("localmessageid", HttpUtility.UrlEncodeUnicode(localMessageId));
            if (!string.IsNullOrEmpty(policy)) param.Add("policy", HttpUtility.UrlEncodeUnicode(policy));
            if (mediaId.HasValue) param.Add("mediaid", mediaId.Value.ToString());

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSend>(responseBody).entries;
        }
#endif

        public List<SendResult> Send(SendRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var path = GetApiPath("sms", "send", "json");
            var param = new Dictionary<string, object>
            {
                { "sender", HttpUtility.UrlEncodeUnicode(request.Sender) },
                {
                    "receptor",
                    HttpUtility.UrlEncodeUnicode(StringHelper.Join(",", request.Receptor.ToArray()))
                },
                { "message", HttpUtility.UrlEncodeUnicode(request.Message) },
                { "type", (int)request.Type },
                { "date", request.Date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(request.Date) }
            };
            if (request.LocalIds != null && request.LocalIds.Count > 0)
            {
                param.Add("localid", StringHelper.Join(",", request.LocalIds.ToArray()));
            }

            if (!string.IsNullOrEmpty(request.Tag))
                param.Add("tag", HttpUtility.UrlEncodeUnicode(request.Tag));
            if (!string.IsNullOrEmpty(request.Text))
                param.Add("text", HttpUtility.UrlEncodeUnicode(request.Text));
            if (!string.IsNullOrEmpty(request.Hide))
                param.Add("hide", HttpUtility.UrlEncodeUnicode(request.Hide));
            if (!string.IsNullOrEmpty(request.LocalMessageId))
                param.Add("localmessageid", HttpUtility.UrlEncodeUnicode(request.LocalMessageId));
            if (!string.IsNullOrEmpty(request.Policy))
                param.Add("policy", HttpUtility.UrlEncodeUnicode(request.Policy));
            if (request.MediaId.HasValue) param.Add("mediaid", request.MediaId.Value.ToString());

            var responseBody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responseBody);
            return l.entries;
        }
#if !NET35
        public async Task<List<SendResult>> SendAsync(SendRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var path = GetApiPath("sms", "send", "json");
            var param = new Dictionary<string, object>
            {
                { "sender", HttpUtility.UrlEncodeUnicode(request.Sender) },
                {
                    "receptor",
                    HttpUtility.UrlEncodeUnicode(StringHelper.Join(",", request.Receptor.ToArray()))
                },
                { "message", HttpUtility.UrlEncodeUnicode(request.Message) },
                { "type", (int)request.Type },
                { "date", request.Date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(request.Date) }
            };
            if (request.LocalIds != null && request.LocalIds.Count > 0)
            {
                param.Add("localid", StringHelper.Join(",", request.LocalIds.ToArray()));
            }

            if (!string.IsNullOrEmpty(request.Tag))
                param.Add("tag", HttpUtility.UrlEncodeUnicode(request.Tag));
            if (!string.IsNullOrEmpty(request.Text))
                param.Add("text", HttpUtility.UrlEncodeUnicode(request.Text));
            if (!string.IsNullOrEmpty(request.Hide))
                param.Add("hide", HttpUtility.UrlEncodeUnicode(request.Hide));
            if (!string.IsNullOrEmpty(request.LocalMessageId))
                param.Add("localmessageid", HttpUtility.UrlEncodeUnicode(request.LocalMessageId));
            if (!string.IsNullOrEmpty(request.Policy))
                param.Add("policy", HttpUtility.UrlEncodeUnicode(request.Policy));
            if (request.MediaId.HasValue) param.Add("mediaid", request.MediaId.Value.ToString());

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responseBody);
            return l.entries;
        }
#endif

        public List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages)
        {
            var types = new List<MessageType>();
            for (var i = 0; i < (senders == null ? 0 : senders.Count); i++)
            {
                types.Add(MessageType.MobileMemory);
            }

            return SendArray(senders, receptors, messages, types, DateTime.MinValue, null);
        }
#if !NET35
        public async Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages)
        {
            return await SendArrayAsync(senders, receptors, messages, DateTime.MinValue, null).ConfigureAwait(false);
        }
#endif

        public List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages,
            MessageType type, DateTime date)
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
#if !NET35
        public async Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages,
            DateTime date)
        {
            var senders = new List<string>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }

            return await SendArrayAsync(senders, receptors, messages, date, null).ConfigureAwait(false);
        }
#endif

        public List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages,
            MessageType type, DateTime date, string localmessageids)
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
#if !NET35
        public async Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages,
            DateTime date, string localmessageids)
        {
            var senders = new List<String>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }

            return await SendArrayAsync(senders, receptors, messages, date, new List<String> { localmessageids }).ConfigureAwait(false);
        }
#endif

        public List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages,
            string localmessageid)
        {
            List<String> senders = new List<String>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }

            return SendArray(senders, receptors, messages, localmessageid);
        }
#if !NET35
        public async Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages,
            string localmessageid)
        {
            List<String> senders = new List<String>();
            for (var i = 0; i < receptors.Count; i++)
            {
                senders.Add(sender);
            }

            return await SendArrayAsync(senders, receptors, messages, localmessageid).ConfigureAwait(false);
        }
#endif

        public List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages,
            string localmessageid)
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
#if !NET35
        public async Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages,
            string localmessageid)
        {
            var localmessageids = new List<string>();
            for (var i = 0; i <= receptors.Count - 1; i++)
            {
                localmessageids.Add(localmessageid);
            }

            return await SendArrayAsync(senders, receptors, messages, DateTime.MinValue, localmessageids).ConfigureAwait(false);
        }
#endif

        public List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages,
            List<MessageType> types, DateTime date, List<string> localmessageids)
        {
            return SendArray(new SendArrayRequest
            {
                Senders = senders,
                Receptors = receptors,
                Messages = messages,
                Types = types,
                Date = date,
                LocalMessageIds = localmessageids
            });
        }
#if !NET35
        public async Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages,
            DateTime date, List<string> localmessageids)
        {
            return await SendArrayAsync(new SendArrayRequest
            {
                Senders = senders,
                Receptors = receptors,
                Messages = messages,
                Date = date,
                LocalMessageIds = localmessageids
            });
        }
#endif

        public List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages,
            List<MessageType> types, DateTime date, List<string> localmessageids, string tag = null, string hide = null,
            string policy = null, Guid? mediaId = null)
        {
            String path = GetApiPath("sms", "sendarray", "json");
            var jsonSenders = JsonConvert.SerializeObject(TrimSendArrayValues(senders));
            var jsonReceptors = JsonConvert.SerializeObject(TrimSendArrayValues(receptors));
            var jsonMessages = JsonConvert.SerializeObject(messages);
            var jsonTypes = JsonConvert.SerializeObject(types);
            var param = new Dictionary<string, object>
            {
                { "message", jsonMessages },
                { "sender", jsonSenders },
                { "receptor", jsonReceptors },
                { "type", jsonTypes },
                { "date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date) }
            };
            if (localmessageids != null && localmessageids.Count > 0)
            {
                param.Add("localmessageids", StringHelper.Join(",", localmessageids.ToArray()));
            }

            if (!string.IsNullOrEmpty(tag)) param.Add("tag", HttpUtility.UrlEncodeUnicode(tag));
            if (!string.IsNullOrEmpty(hide)) param.Add("hide", HttpUtility.UrlEncodeUnicode(hide));
            if (!string.IsNullOrEmpty(policy)) param.Add("policy", HttpUtility.UrlEncodeUnicode(policy));
            if (mediaId.HasValue) param.Add("mediaid", mediaId.Value.ToString());

            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }

            return l.entries;
        }
#if !NET35
        public async Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages,
            DateTime date, List<string> localmessageids, string tag = null, string hide = null,
            string policy = null, Guid? mediaId = null)
        {
            String path = GetApiPath("sms", "sendarray", "json");
            var jsonSenders = JsonConvert.SerializeObject(TrimSendArrayValues(senders));
            var jsonReceptors = JsonConvert.SerializeObject(TrimSendArrayValues(receptors));
            var jsonMessages = JsonConvert.SerializeObject(messages);
            var param = new Dictionary<string, object>
            {
                { "message", jsonMessages },
                { "sender", jsonSenders },
                { "receptor", jsonReceptors },
                { "date", date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(date) }
            };
            if (localmessageids != null && localmessageids.Count > 0)
            {
                param.Add("localmessageids", StringHelper.Join(",", localmessageids.ToArray()));
            }

            if (!string.IsNullOrEmpty(tag)) param.Add("tag", HttpUtility.UrlEncodeUnicode(tag));
            if (!string.IsNullOrEmpty(hide)) param.Add("hide", HttpUtility.UrlEncodeUnicode(hide));
            if (!string.IsNullOrEmpty(policy)) param.Add("policy", HttpUtility.UrlEncodeUnicode(policy));
            if (mediaId.HasValue) param.Add("mediaid", mediaId.Value.ToString());

            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }

            return l.entries;
        }
#endif

        public List<SendResult> SendArray(SendArrayRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            String path = GetApiPath("sms", "sendarray", "json");
            var jsonSenders = JsonConvert.SerializeObject(TrimSendArrayValues(request.Senders));
            var jsonReceptors = JsonConvert.SerializeObject(TrimSendArrayValues(request.Receptors));
            var jsonMessages = JsonConvert.SerializeObject(request.Messages);
            var jsonTypes = JsonConvert.SerializeObject(request.Types);
            var param = new Dictionary<string, object>
            {
                { "message", jsonMessages },
                { "sender", jsonSenders },
                { "receptor", jsonReceptors },
                { "type", jsonTypes },
                { "date", request.Date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(request.Date) }
            };
            if (request.LocalMessageIds != null && request.LocalMessageIds.Count > 0)
            {
                param.Add("localmessageids", StringHelper.Join(",", request.LocalMessageIds.ToArray()));
            }

            if (!string.IsNullOrEmpty(request.Tag))
                param.Add("tag", HttpUtility.UrlEncodeUnicode(request.Tag));
            if (!string.IsNullOrEmpty(request.Hide))
                param.Add("hide", HttpUtility.UrlEncodeUnicode(request.Hide));
            if (!string.IsNullOrEmpty(request.Policy))
                param.Add("policy", HttpUtility.UrlEncodeUnicode(request.Policy));
            if (request.MediaId.HasValue) param.Add("mediaid", request.MediaId.Value.ToString());

            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }

            return l.entries;
        }
#if !NET35
        public async Task<List<SendResult>> SendArrayAsync(SendArrayRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            String path = GetApiPath("sms", "sendarray", "json");
            var jsonSenders = JsonConvert.SerializeObject(TrimSendArrayValues(request.Senders));
            var jsonReceptors = JsonConvert.SerializeObject(TrimSendArrayValues(request.Receptors));
            var jsonMessages = JsonConvert.SerializeObject(request.Messages);
            var jsonTypes = JsonConvert.SerializeObject(request.Types);
            var param = new Dictionary<string, object>
            {
                { "message", jsonMessages },
                { "sender", jsonSenders },
                { "receptor", jsonReceptors },
                { "type", jsonTypes },
                { "date", request.Date == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(request.Date) }
            };
            if (request.LocalMessageIds != null && request.LocalMessageIds.Count > 0)
            {
                param.Add("localmessageids", StringHelper.Join(",", request.LocalMessageIds.ToArray()));
            }

            if (!string.IsNullOrEmpty(request.Tag))
                param.Add("tag", HttpUtility.UrlEncodeUnicode(request.Tag));
            if (!string.IsNullOrEmpty(request.Hide))
                param.Add("hide", HttpUtility.UrlEncodeUnicode(request.Hide));
            if (!string.IsNullOrEmpty(request.Policy))
                param.Add("policy", HttpUtility.UrlEncodeUnicode(request.Policy));
            if (request.MediaId.HasValue) param.Add("mediaid", request.MediaId.Value.ToString());

            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }

            return l.entries;
        }
#endif

        private static List<string> TrimSendArrayValues(List<string> values)
        {
            var trimmedValues = new List<string>();
            if (values == null)
            {
                return trimmedValues;
            }

            foreach (var value in values)
            {
                if (value != null)
                {
                    trimmedValues.Add(value.Trim());
                }
            }

            return trimmedValues;
        }

        public List<StatusResult> Status(List<string> messageids)
        {
            string path = GetApiPath("sms", "status", "json");
            var param = new Dictionary<string, object>
            {
                { "messageid", StringHelper.Join(",", messageids.ToArray()) }
            };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnStatus>(responsebody);
            if (l.entries == null)
            {
                return new List<StatusResult>();
            }

            return l.entries;
        }
#if !NET35
        public async Task<List<StatusResult>> StatusAsync(List<string> messageids)
        {
            string path = GetApiPath("sms", "status", "json");
            var param = new Dictionary<string, object>
            {
                { "messageid", StringHelper.Join(",", messageids.ToArray()) }
            };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnStatus>(responsebody);
            if (l.entries == null)
            {
                return new List<StatusResult>();
            }

            return l.entries;
        }
#endif

        public StatusResult Status(string messageid)
        {
            var ids = new List<String> { messageid };
            var result = Status(ids);
            return result.Count == 1 ? result[0] : null;
        }
#if !NET35
        public async Task<StatusResult> StatusAsync(string messageid)
        {
            var ids = new List<String> { messageid };
            var result = await StatusAsync(ids).ConfigureAwait(false);
            return result.Count == 1 ? result[0] : null;
        }
#endif

        public List<StatusLocalMessageIdResult> StatusLocalMessageId(List<string> messageids)
        {
            string path = GetApiPath("sms", "statuslocalmessageid", "json");
            var param = new Dictionary<string, object> { { "localid", StringHelper.Join(",", messageids.ToArray()) } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnStatusLocalMessageId>(responsebody);
            return l.entries;
        }
#if !NET35
        public async Task<List<StatusLocalMessageIdResult>> StatusLocalMessageIdAsync(List<string> messageids)
        {
            string path = GetApiPath("sms", "statuslocalmessageid", "json");
            var param = new Dictionary<string, object> { { "localid", StringHelper.Join(",", messageids.ToArray()) } };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnStatusLocalMessageId>(responsebody);
            return l.entries;
        }
#endif

        public StatusLocalMessageIdResult StatusLocalMessageId(string messageid)
        {
            List<StatusLocalMessageIdResult> result = StatusLocalMessageId(new List<String>() { messageid });
            return result.Count == 1 ? result[0] : null;
        }
#if !NET35
        public async Task<StatusLocalMessageIdResult> StatusLocalMessageIdAsync(string messageid)
        {
            List<StatusLocalMessageIdResult> result = await StatusLocalMessageIdAsync(new List<String> { messageid }).ConfigureAwait(false);
            return result.Count == 1 ? result[0] : null;
        }
#endif

        public List<SendResult> Select(List<string> messageids)
        {
            var path = GetApiPath("sms", "select", "json");
            var param = new Dictionary<string, object>
                { { "messageid", StringHelper.Join(",", messageids.ToArray()) } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }

            return l.entries;
        }
#if !NET35
        public async Task<List<SendResult>> SelectAsync(List<string> messageids)
        {
            var path = GetApiPath("sms", "select", "json");
            var param = new Dictionary<string, object>
                { { "messageid", StringHelper.Join(",", messageids.ToArray()) } };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            if (l.entries == null)
            {
                return new List<SendResult>();
            }

            return l.entries;
        }
#endif

        public SendResult Select(string messageid)
        {
            var ids = new List<String> { messageid };
            var result = Select(ids);
            return result.Count == 1 ? result[0] : null;
        }
#if !NET35
        public async Task<SendResult> SelectAsync(string messageid)
        {
            var ids = new List<String> { messageid };
            var result = await SelectAsync(ids).ConfigureAwait(false);
            return result.Count == 1 ? result[0] : null;
        }
#endif

        public List<SendResult> SelectOutbox(DateTime startdate)
        {
            return SelectOutbox(startdate, startdate.AddDays(1));
        }
#if !NET35
        public async Task<List<SendResult>> SelectOutboxAsync(DateTime startdate)
        {
            return await SelectOutboxAsync(startdate, startdate.AddDays(1)).ConfigureAwait(false);
        }
#endif

        public List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate)
        {
            return SelectOutbox(startdate, enddate, null);
        }
#if !NET35
        public async Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate)
        {
            return await SelectOutboxAsync(startdate, enddate, null).ConfigureAwait(false);
        }
#endif

        public List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate, String sender)
        {
            String path = GetApiPath("sms", "selectoutbox", "json");
            var param = new Dictionary<string, object>
            {
                { "startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate) },
                { "enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate) },
                { "sender", sender }
            };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            return l.entries;
        }
#if !NET35
        public async Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate, String sender)
        {
            String path = GetApiPath("sms", "selectoutbox", "json");
            var param = new Dictionary<string, object>
            {
                { "startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate) },
                { "enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate) },
                { "sender", sender }
            };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            return l.entries;
        }
#endif

        public List<SendResult> LatestOutbox(long pagesize)
        {
            return LatestOutbox(pagesize, "");
        }
#if !NET35
        public async Task<List<SendResult>> LatestOutboxAsync(long pagesize)
        {
            return await LatestOutboxAsync(pagesize, "").ConfigureAwait(false);
        }
#endif

        public List<SendResult> LatestOutbox(long pagesize, String sender)
        {
            var path = GetApiPath("sms", "latestoutbox", "json");
            var param = new Dictionary<string, object> { { "pagesize", pagesize }, { "sender", sender } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            return l.entries;
        }
#if !NET35
        public async Task<List<SendResult>> LatestOutboxAsync(long pagesize, String sender)
        {
            var path = GetApiPath("sms", "latestoutbox", "json");
            var param = new Dictionary<string, object> { { "pagesize", pagesize }, { "sender", sender } };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            return l.entries;
        }
#endif

        public CountOutboxResult CountOutbox(DateTime startdate)
        {
            return CountOutbox(startdate, startdate.AddDays(1), 10);
        }
#if !NET35
        public async Task<CountOutboxResult> CountOutboxAsync(DateTime startdate)
        {
            return await CountOutboxAsync(startdate,startdate.AddDays(1), 10).ConfigureAwait(false);
        }
#endif

        public CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate)
        {
            return CountOutbox(startdate, enddate, 0);
        }
#if !NET35
        public async Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate)
        {
            return await CountOutboxAsync(startdate, enddate, 0).ConfigureAwait(false);
        }
#endif

        public CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate, int status)
        {
            string path = GetApiPath("sms", "countoutbox", "json");
            var param = new Dictionary<string, object>
            {
                { "startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate) },
                { "enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate) },
                { "status", status }
            };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnCountOutbox>(responsebody);
            if (l.entries == null)
            {
                return new CountOutboxResult();
            }

            return l.entries;
        }
#if !NET35
        public async Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate, int status)
        {
            string path = GetApiPath("sms", "countoutbox", "json");
            var param = new Dictionary<string, object>
            {
                { "startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate) },
                { "enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate) },
                { "status", status }
            };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnCountOutbox>(responsebody);
            if (l.entries == null)
            {
                return new CountOutboxResult();
            }

            return l.entries;
        }
#endif

        public List<StatusResult> Cancel(List<String> ids)
        {
            string path = GetApiPath("sms", "cancel", "json");
            var param = new Dictionary<string, object>
            {
                { "messageid", StringHelper.Join(",", ids.ToArray()) }
            };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnStatus>(responsebody);
            return l.entries;
        }
#if !NET35
        public async Task<List<StatusResult>> CancelAsync(List<String> ids)
        {
            string path = GetApiPath("sms", "cancel", "json");
            var param = new Dictionary<string, object>
            {
                { "messageid", StringHelper.Join(",", ids.ToArray()) }
            };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnStatus>(responsebody);
            return l.entries;
        }
#endif

        public StatusResult Cancel(String messageid)
        {
            var ids = new List<String> { messageid };
            var result = Cancel(ids);
            return result.Count == 1 ? result[0] : null;
        }
#if !NET35
        public async Task<StatusResult> CancelAsync(String messageid)
        {
            var ids = new List<String> { messageid };
            var result = await CancelAsync(ids).ConfigureAwait(false);
            return result.Count == 1 ? result[0] : null;
        }
#endif

        public List<ReceiveResult> Receive(string line, int isread)
        {
            String path = GetApiPath("sms", "receive", "json");
            var param = new Dictionary<string, object> { { "linenumber", line }, { "isread", isread } };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnReceive>(responsebody);
            if (l.entries == null)
            {
                return new List<ReceiveResult>();
            }

            return l.entries;
        }
#if !NET35
        public async Task<List<ReceiveResult>> ReceiveAsync(string line, int isread)
        {
            String path = GetApiPath("sms", "receive", "json");
            var param = new Dictionary<string, object> { { "linenumber", line }, { "isread", isread } };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnReceive>(responsebody);
            if (l.entries == null)
            {
                return new List<ReceiveResult>();
            }

            return l.entries;
        }
#endif

        public CountInboxResult CountInbox(DateTime startdate, string linenumber)
        {
            return CountInbox(startdate, startdate.AddDays(1), linenumber, 0);
        }
#if !NET35
        public async Task<CountInboxResult> CountInboxAsync(DateTime startdate, string linenumber)
        {
            return await CountInboxAsync(startdate, startdate.AddDays(1), linenumber, 0).ConfigureAwait(false);
        }
#endif

        public CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber)
        {
            return CountInbox(startdate, enddate, linenumber, 0);
        }
#if !NET35
        public async Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, String linenumber)
        {
            return await CountInboxAsync(startdate, enddate, linenumber, 0).ConfigureAwait(false);
        }
#endif

        public CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber, int isread)
        {
            var path = GetApiPath("sms", "countinbox", "json");
            var param = new Dictionary<string, object>
            {
                { "startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate) },
                { "enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate) },
                { "linenumber", linenumber },
                { "isread", isread }
            };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnCountInbox>(responsebody);
            if (l.entries == null)
            {
                return new CountInboxResult();
            }
            return l.entries;
        }
#if !NET35
        public async Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, String linenumber, int isread)
        {
            var path = GetApiPath("sms", "countinbox", "json");
            var param = new Dictionary<string, object>
            {
                { "startdate", startdate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startdate) },
                { "enddate", enddate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(enddate) },
                { "linenumber", linenumber },
                { "isread", isread }
            };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnCountInbox>(responsebody);
            if (l.entries == null)
            {
                return new CountInboxResult();
            }
            return l.entries;
        }
#endif

        public AccountInfoResult AccountInfo()
        {
            var path = GetApiPath("account", "info", "json");
            var responsebody = Execute(path, null);
            var l = JsonConvert.DeserializeObject<ReturnAccountInfo>(responsebody);
            return l.entries;
        }
#if !NET35
        public async Task<AccountInfoResult> AccountInfoAsync()
        {
            var path = GetApiPath("account", "info", "json");
            var responsebody = await ExecuteAsync(path, null).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnAccountInfo>(responsebody);
            return l.entries;
        }
#endif

        public AccountConfigResult AccountConfig(string apilogs, string dailyreport, string debugmode,
            string defaultsender, int? mincreditalarm, string resendfailed)
        {
            var path = GetApiPath("account", "config", "json");
            var param = new Dictionary<string, object>
            {
                { "apilogs", apilogs },
                { "dailyreport", dailyreport },
                { "debugmode", debugmode },
                { "defaultsender", defaultsender },
                { "mincreditalarm", mincreditalarm },
                { "resendfailed", resendfailed }
            };
            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnAccountConfig>(responsebody);
            return l.entries != null && l.entries.Count > 0 ? l.entries[0] : null;
        }
#if !NET35
        public async Task<AccountConfigResult> AccountConfigAsync(string apilogs, string dailyreport, string debugmode,
            string defaultsender, int? mincreditalarm, string resendfailed)
        {
            var path = GetApiPath("account", "config", "json");
            var param = new Dictionary<string, object>
            {
                { "apilogs", apilogs },
                { "dailyreport", dailyreport },
                { "debugmode", debugmode },
                { "defaultsender", defaultsender },
                { "mincreditalarm", mincreditalarm },
                { "resendfailed", resendfailed }
            };
            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnAccountConfig>(responsebody);
            return l.entries != null && l.entries.Count > 0 ? l.entries[0] : null;
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string template)
        {
            return VerifyLookup(receptor, token, null, null, template, VerifyLookupType.Sms);
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string template)
        {
            return await VerifyLookupAsync(receptor, token, null, null, template, VerifyLookupType.Sms).ConfigureAwait(false);
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string template, VerifyLookupType type)
        {
            return VerifyLookup(receptor, token, null, null, template, type);
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string template, VerifyLookupType type)
        {
            return await VerifyLookupAsync(receptor, token, null, null, template, type).ConfigureAwait(false);
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string token2, string template)
        {
            return VerifyLookup(receptor, token, token2, null, template, VerifyLookupType.Sms);
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string template)
        {
            return await VerifyLookupAsync(receptor, token, token2, null, template, VerifyLookupType.Sms).ConfigureAwait(false);
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string token2, string template,
            VerifyLookupType type)
        {
            return VerifyLookup(receptor, token, token2, null, template, type);
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string template,
            VerifyLookupType type)
        {
            return await VerifyLookupAsync(receptor, token, token2, null, template, type).ConfigureAwait(false);
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template)
        {
            return VerifyLookup(receptor, token, token2, token3, template, VerifyLookupType.Sms);
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string template)
        {
            return await VerifyLookupAsync(receptor, token, token2, token3, template, VerifyLookupType.Sms).ConfigureAwait(false);
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10,
            string template)
        {
            return VerifyLookup(receptor, token, token2, token3, token10, template, VerifyLookupType.Sms);
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10,
            string template)
        {
            return await VerifyLookupAsync(receptor, token, token2, token3, token10, template, VerifyLookupType.Sms).ConfigureAwait(false);
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template,
            VerifyLookupType type)
        {
            return VerifyLookup(receptor, token, token2, token3, null, template, type);
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string template,
            VerifyLookupType type)
        {
            return await VerifyLookupAsync(receptor, token, token2, token3, null, template, type).ConfigureAwait(false);
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10,
            string template, VerifyLookupType type)
        {
            return VerifyLookup(receptor, token, token2, token3, token10, null, template, type);
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10,
            string template, VerifyLookupType type)
        {
            return await VerifyLookupAsync(receptor, token, token2, token3, token10, null, template, type).ConfigureAwait(false);
        }
#endif

        public SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10,
            string token20, string template, VerifyLookupType type, string tag = null)
        {
            var path = GetApiPath("verify", "lookup", "json");
            var param = new Dictionary<string, object>
            {
                { "receptor", receptor },
                { "template", template },
                { "token", token },
                { "token2", token2 },
                { "token3", token3 },
                { "token10", token10 },
                { "token20", token20 },
                { "type", type },
            };
            if (!string.IsNullOrEmpty(tag)) param.Add("tag", HttpUtility.UrlEncodeUnicode(tag));

            var responsebody = Execute(path, param);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            return l.entries[0];
        }
#if !NET35
        public async Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10,
            string token20, string template, VerifyLookupType type, string tag = null)
        {
            var path = GetApiPath("verify", "lookup", "json");
            var param = new Dictionary<string, object>
            {
                { "receptor", receptor },
                { "template", template },
                { "token", token },
                { "token2", token2 },
                { "token3", token3 },
                { "token10", token10 },
                { "token20", token20 },
                { "type", type },
            };
            if (!string.IsNullOrEmpty(tag)) param.Add("tag", HttpUtility.UrlEncodeUnicode(tag));

            var responsebody = await ExecuteAsync(path, param).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnSend>(responsebody);
            return l.entries[0];
        }
#endif

        #region << CallMakeTTS >>

        public SendResult CallMakeTTS(string message, string receptor)
        {
            return CallMakeTTS(message, new List<string> { receptor }, null, null)[0];
        }
#if !NET35
        public async Task<SendResult> CallMakeTTSAsync(string message, string receptor)
        {
            return (await CallMakeTTSAsync(message, new List<string> { receptor }, null, null).ConfigureAwait(false))[0];
        }
#endif

        public List<SendResult> CallMakeTTS(string message, List<string> receptor)
        {
            return CallMakeTTS(message, receptor, null, null);
        }
#if !NET35
        public async Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor)
        {
            return await CallMakeTTSAsync(message, receptor, null, null).ConfigureAwait(false);
        }
#endif

        public List<SendResult> CallMakeTTS(string message, List<string> receptor, DateTime? date, List<string> localid)
        {
            return CallMakeTTS(message, receptor, date, localid, null);
        }
#if !NET35
        public async Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor, DateTime? date, List<string> localid)
        {
            return await CallMakeTTSAsync(message, receptor, date, localid, null).ConfigureAwait(false);
        }
#endif

        public List<SendResult> CallMakeTTS(string message, List<string> receptor, DateTime? date, List<string> localid,
            string tag = null)
        {
            var path = GetApiPath("call", "maketts", "json");
            var param = new Dictionary<string, object>
            {
                { "receptor", StringHelper.Join(",", receptor.ToArray()) },
                { "message", HttpUtility.UrlEncodeUnicode(message) },
            };
            if (date != null)
                param.Add("date", DateHelper.DateTimeToUnixTimestamp(date.Value));
            if (localid != null && localid.Count > 0)
                param.Add("localid", StringHelper.Join(",", localid.ToArray()));
            if (!string.IsNullOrEmpty(tag)) param.Add("tag", HttpUtility.UrlEncodeUnicode(tag));

            var responseBody = Execute(path, param);

            return JsonConvert.DeserializeObject<ReturnSend>(responseBody).entries;
        }
#if !NET35
        public async Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor, DateTime? date, List<string> localid,
            string tag = null)
        {
            var path = GetApiPath("call", "maketts", "json");
            var param = new Dictionary<string, object>
            {
                { "receptor", StringHelper.Join(",", receptor.ToArray()) },
                { "message", HttpUtility.UrlEncodeUnicode(message) },
            };
            if (date != null)
                param.Add("date", DateHelper.DateTimeToUnixTimestamp(date.Value));
            if (localid != null && localid.Count > 0)
                param.Add("localid", StringHelper.Join(",", localid.ToArray()));
            if (!string.IsNullOrEmpty(tag)) param.Add("tag", HttpUtility.UrlEncodeUnicode(tag));

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ReturnSend>(responseBody).entries;
        }
#endif

        #endregion << CallMakeTTS >>


        // SMS API Extensions
        public List<StatusResult> StatusByReceptor(string receptor, DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var path = GetApiPath("sms", "statusbyreceptor", "json");
            var param = new Dictionary<string, object>
            {
                { "receptor", receptor }
            };
            if (startDate.HasValue)
                param.Add("startdate",
                    startDate.Value == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate.Value));
            if (endDate.HasValue)
                param.Add("enddate",
                    endDate.Value == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate.Value));

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnStatus>(responseBody).entries;
        }
#if !NET35
        public async Task<List<StatusResult>> StatusByReceptorAsync(string receptor, DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var path = GetApiPath("sms", "statusbyreceptor", "json");
            var param = new Dictionary<string, object>
            {
                { "receptor", receptor }
            };
            if (startDate.HasValue)
                param.Add("startdate",
                    startDate.Value == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate.Value));
            if (endDate.HasValue)
                param.Add("enddate",
                    endDate.Value == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate.Value));

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnStatus>(responseBody).entries;
        }
#endif

        public List<ReceiveResult> Unreads(string lineNumber, int isRead)
        {
            var path = GetApiPath("sms", "unreads", "json");
            var param = new Dictionary<string, object>
            {
                { "linenumber", lineNumber },
                { "isread", isRead }
            };
            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnReceive>(responseBody).entries;
        }
#if !NET35
        public async Task<List<ReceiveResult>> UnreadsAsync(string lineNumber, int isRead)
        {
            var path = GetApiPath("sms", "unreads", "json");
            var param = new Dictionary<string, object>
            {
                { "linenumber", lineNumber },
                { "isread", isRead }
            };
            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnReceive>(responseBody).entries;
        }
#endif

        public InboxPagedResult InboxPaged(string lineNumber, int? isRead = null,
            DateTime? startDate = null, DateTime? endDate = null, int? pageNumber = null)
        {
            var path = GetApiPath("sms", "inboxpaged", "json");
            var param = new Dictionary<string, object>
            {
                { "linenumber", lineNumber }
            };
            if (isRead.HasValue)
                param.Add("isread", isRead.Value);
            if (startDate.HasValue)
                param.Add("startdate",
                    startDate.Value == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate.Value));
            if (endDate.HasValue)
                param.Add("enddate",
                    endDate.Value == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate.Value));
            if (pageNumber.HasValue)
                param.Add("pagenumber", pageNumber.Value);

            var responseBody = Execute(path, param, "GET");
            var r = JsonConvert.DeserializeObject<ReturnInboxPaged>(responseBody);
            return new InboxPagedResult
            {
                Metadata = r.metadata,
                Entries = r.entries
            };
        }
#if !NET35
        public async Task<InboxPagedResult> InboxPagedAsync(string lineNumber, int? isRead = null,
            DateTime? startDate = null, DateTime? endDate = null, int? pageNumber = null)
        {
            var path = GetApiPath("sms", "inboxpaged", "json");
            var param = new Dictionary<string, object>
            {
                { "linenumber", lineNumber }
            };
            if (isRead.HasValue)
                param.Add("isread", isRead.Value);
            if (startDate.HasValue)
                param.Add("startdate",
                    startDate.Value == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate.Value));
            if (endDate.HasValue)
                param.Add("enddate",
                    endDate.Value == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate.Value));
            if (pageNumber.HasValue)
                param.Add("pagenumber", pageNumber.Value);

            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            var r = JsonConvert.DeserializeObject<ReturnInboxPaged>(responseBody);
            return new InboxPagedResult
            {
                Metadata = r.metadata,
                Entries = r.entries
            };
        }
#endif

        public List<GroupSendReportResult> GroupSendReport(DateTime startDate, DateTime endDate)
        {
            var path = GetApiPath("sms/report", "groupsend", "json");
            var param = new Dictionary<string, object>
            {
                { "startdate", startDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate) },
                { "enddate", endDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate) }
            };
            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnGroupSendReport>(responseBody).entries;
        }
#if !NET35
        public async Task<List<GroupSendReportResult>> GroupSendReportAsync(DateTime startDate, DateTime endDate)
        {
            var path = GetApiPath("sms/report", "groupsend", "json");
            var param = new Dictionary<string, object>
            {
                { "startdate", startDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(startDate) },
                { "enddate", endDate == DateTime.MinValue ? 0 : DateHelper.DateTimeToUnixTimestamp(endDate) }
            };
            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnGroupSendReport>(responseBody).entries;
        }
#endif

        // Client / Sub-customer API
        public SubClientResult AddClient(SubClientDto dto)
        {
            var path = GetApiPath("client", "add", "json");
            var param = new Dictionary<string, object>();
            if (dto.Ip != null) param.Add("ip", dto.Ip);
            if (dto.LocalId != null) param.Add("localid", dto.LocalId);
            if (dto.Credit.HasValue) param.Add("credit", dto.Credit.Value);
            if (dto.UserName != null) param.Add("username", dto.UserName);
            if (dto.Password != null) param.Add("password", dto.Password);
            if (dto.FullName != null) param.Add("fullname", dto.FullName);
            if (dto.PlanId.HasValue) param.Add("planid", dto.PlanId.Value);
            if (dto.Mobile != null) param.Add("mobile", dto.Mobile);
            if (dto.Status != null) param.Add("status", dto.Status);
            if (dto.MininumAllowedCredit.HasValue) param.Add("mininumallowedcredit", dto.MininumAllowedCredit.Value);
            if (dto.Lines != null) param.Add("lines", dto.Lines);
            if (dto.CanUseParentLines.HasValue) param.Add("canuseparentlines", dto.CanUseParentLines.Value ? 1 : 0);
            if (dto.ExpireDate != null) param.Add("expiredate", dto.ExpireDate);
            if (dto.EnableLink.HasValue) param.Add("enablelink", dto.EnableLink.Value ? 1 : 0);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#if !NET35
        public async Task<SubClientResult> AddClientAsync(SubClientDto dto)
        {
            var path = GetApiPath("client", "add", "json");
            var param = new Dictionary<string, object>();
            if (dto.Ip != null) param.Add("ip", dto.Ip);
            if (dto.LocalId != null) param.Add("localid", dto.LocalId);
            if (dto.Credit.HasValue) param.Add("credit", dto.Credit.Value);
            if (dto.UserName != null) param.Add("username", dto.UserName);
            if (dto.Password != null) param.Add("password", dto.Password);
            if (dto.FullName != null) param.Add("fullname", dto.FullName);
            if (dto.PlanId.HasValue) param.Add("planid", dto.PlanId.Value);
            if (dto.Mobile != null) param.Add("mobile", dto.Mobile);
            if (dto.Status != null) param.Add("status", dto.Status);
            if (dto.MininumAllowedCredit.HasValue) param.Add("mininumallowedcredit", dto.MininumAllowedCredit.Value);
            if (dto.Lines != null) param.Add("lines", dto.Lines);
            if (dto.CanUseParentLines.HasValue) param.Add("canuseparentlines", dto.CanUseParentLines.Value ? 1 : 0);
            if (dto.ExpireDate != null) param.Add("expiredate", dto.ExpireDate);
            if (dto.EnableLink.HasValue) param.Add("enablelink", dto.EnableLink.Value ? 1 : 0);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#endif

        public SubClientResult UpdateClient(SubClientDto dto)
        {
            var path = GetApiPath("client", "update", "json");
            var param = new Dictionary<string, object>();
            if (dto.ApiKey != null) param.Add("apikey", dto.ApiKey);
            if (dto.Ip != null) param.Add("ip", dto.Ip);
            if (dto.LocalId != null) param.Add("localid", dto.LocalId);
            if (dto.UserName != null) param.Add("username", dto.UserName);
            if (dto.Password != null) param.Add("password", dto.Password);
            if (dto.FullName != null) param.Add("fullname", dto.FullName);
            if (dto.Mobile != null) param.Add("mobile", dto.Mobile);
            if (dto.PlanId.HasValue) param.Add("planid", dto.PlanId.Value);
            if (dto.Lines != null) param.Add("lines", dto.Lines);
            if (dto.Status != null) param.Add("status", dto.Status);
            if (dto.ExpireDate != null) param.Add("expiredate", dto.ExpireDate);
            if (dto.CanUseParentLines.HasValue) param.Add("canuseparentlines", dto.CanUseParentLines.Value ? 1 : 0);
            if (dto.EnableLink.HasValue) param.Add("enablelink", dto.EnableLink.Value ? 1 : 0);
            if (dto.CanCharge.HasValue) param.Add("cancharge", dto.CanCharge.Value ? 1 : 0);
            if (dto.MininumAllowedCredit.HasValue) param.Add("mininumallowedcredit", dto.MininumAllowedCredit.Value);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#if !NET35
        public async Task<SubClientResult> UpdateClientAsync(SubClientDto dto)
        {
            var path = GetApiPath("client", "update", "json");
            var param = new Dictionary<string, object>();
            if (dto.ApiKey != null) param.Add("apikey", dto.ApiKey);
            if (dto.Ip != null) param.Add("ip", dto.Ip);
            if (dto.LocalId != null) param.Add("localid", dto.LocalId);
            if (dto.UserName != null) param.Add("username", dto.UserName);
            if (dto.Password != null) param.Add("password", dto.Password);
            if (dto.FullName != null) param.Add("fullname", dto.FullName);
            if (dto.Mobile != null) param.Add("mobile", dto.Mobile);
            if (dto.PlanId.HasValue) param.Add("planid", dto.PlanId.Value);
            if (dto.Lines != null) param.Add("lines", dto.Lines);
            if (dto.Status != null) param.Add("status", dto.Status);
            if (dto.ExpireDate != null) param.Add("expiredate", dto.ExpireDate);
            if (dto.CanUseParentLines.HasValue) param.Add("canuseparentlines", dto.CanUseParentLines.Value ? 1 : 0);
            if (dto.EnableLink.HasValue) param.Add("enablelink", dto.EnableLink.Value ? 1 : 0);
            if (dto.CanCharge.HasValue) param.Add("cancharge", dto.CanCharge.Value ? 1 : 0);
            if (dto.MininumAllowedCredit.HasValue) param.Add("mininumallowedcredit", dto.MininumAllowedCredit.Value);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#endif

        public List<SubClientResult> ListClients()
        {
            var path = GetApiPath("client", "list", "json");
            var responseBody = Execute(path, null, "GET");
            return JsonConvert.DeserializeObject<ReturnSubClientsList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<SubClientResult>> ListClientsAsync()
        {
            var path = GetApiPath("client", "list", "json");
            var responseBody = await ExecuteAsync(path, null, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSubClientsList>(responseBody).entries;
        }
#endif

        public SubClientResult FetchClient(string apiKey)
        {
            var path = GetApiPath("client", "fetch", "json");
            var param = new Dictionary<string, object>
            {
                { "apikey", apiKey }
            };
            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#if !NET35
        public async Task<SubClientResult> FetchClientAsync(string apiKey)
        {
            var path = GetApiPath("client", "fetch", "json");
            var param = new Dictionary<string, object>
            {
                { "apikey", apiKey }
            };
            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#endif

        public SubClientResult FetchClientByLocalId(string localId)
        {
            var path = GetApiPath("client", "fetchbylocalid", "json");
            var param = new Dictionary<string, object>
            {
                { "localid", localId }
            };
            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#if !NET35
        public async Task<SubClientResult> FetchClientByLocalIdAsync(string localId)
        {
            var path = GetApiPath("client", "fetchbylocalid", "json");
            var param = new Dictionary<string, object>
            {
                { "localid", localId }
            };
            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#endif

        public SubClientResult RenewClientKey(string apiKey = null, string localId = null)
        {
            var path = GetApiPath("client", "renewkey", "json");
            var param = new Dictionary<string, object>();
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#if !NET35
        public async Task<SubClientResult> RenewClientKeyAsync(string apiKey = null, string localId = null)
        {
            var path = GetApiPath("client", "renewkey", "json");
            var param = new Dictionary<string, object>();
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#endif

        public SubClientResult SetClientStatus(string apiKey, int status)
        {
            var path = GetApiPath("client", "setstatus", "json");
            var param = new Dictionary<string, object>
            {
                { "apikey", apiKey },
                { "status", status }
            };
            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#if !NET35
        public async Task<SubClientResult> SetClientStatusAsync(string apiKey, int status)
        {
            var path = GetApiPath("client", "setstatus", "json");
            var param = new Dictionary<string, object>
            {
                { "apikey", apiKey },
                { "status", status }
            };
            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#endif

        public SubClientResult ChargeClientCredit(string apiKey, long credit, string desc = null, int? vat = null,
            string ip = null)
        {
            var path = GetApiPath("client", "chargecredit", "json");
            var param = new Dictionary<string, object>
            {
                { "apikey", apiKey },
                { "credit", credit }
            };
            if (desc != null) param.Add("desc", desc);
            if (vat.HasValue) param.Add("vat", vat.Value);
            if (ip != null) param.Add("ip", ip);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#if !NET35
        public async Task<SubClientResult> ChargeClientCreditAsync(string apiKey, long credit, string desc = null, int? vat = null,
            string ip = null)
        {
            var path = GetApiPath("client", "chargecredit", "json");
            var param = new Dictionary<string, object>
            {
                { "apikey", apiKey },
                { "credit", credit }
            };
            if (desc != null) param.Add("desc", desc);
            if (vat.HasValue) param.Add("vat", vat.Value);
            if (ip != null) param.Add("ip", ip);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnSubClient>(responseBody).entries;
        }
#endif

        // Line Blocked / Blacklist API
        public List<BlacklistResult> AddBlockedLine(string receptor, string lineNumber)
        {
            var path = GetApiPath("line/blocked", "add", "json");
            var param = new Dictionary<string, object>();
            if (receptor != null) param.Add("receptor", receptor);
            if (lineNumber != null) param.Add("linenumber", lineNumber);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnBlacklist>(responseBody).entries;
        }
#if !NET35
        public async Task<List<BlacklistResult>> AddBlockedLineAsync(string receptor, string lineNumber)
        {
            var path = GetApiPath("line/blocked", "add", "json");
            var param = new Dictionary<string, object>();
            if (receptor != null) param.Add("receptor", receptor);
            if (lineNumber != null) param.Add("linenumber", lineNumber);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnBlacklist>(responseBody).entries;
        }
#endif

        public RemoveBlacklistResult RemoveBlockedLine(string receptor, string lineNumber)
        {
            var path = GetApiPath("line/blocked", "remove", "json");
            var param = new Dictionary<string, object>();
            if (receptor != null) param.Add("receptor", receptor);
            if (lineNumber != null) param.Add("linenumber", lineNumber);

            var responseBody = Execute(path, param, "DELETE");
            return JsonConvert.DeserializeObject<ReturnRemoveBlacklist>(responseBody).entries;
        }
#if !NET35
        public async Task<RemoveBlacklistResult> RemoveBlockedLineAsync(string receptor, string lineNumber)
        {
            var path = GetApiPath("line/blocked", "remove", "json");
            var param = new Dictionary<string, object>();
            if (receptor != null) param.Add("receptor", receptor);
            if (lineNumber != null) param.Add("linenumber", lineNumber);

            var responseBody = await ExecuteAsync(path, param, "DELETE").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnRemoveBlacklist>(responseBody).entries;
        }
#endif

        public LineBlockListResult ListBlockedLines(string lineNumber, int pageNumber, long startDate,
            byte? blockReason = null)
        {
            var path = GetApiPath("line/blocked", "list", "json");
            var param = new Dictionary<string, object>
            {
                { "lineNumber", lineNumber },
                { "pageNumber", pageNumber },
                { "StartDate", startDate }
            };
            if (blockReason.HasValue) param.Add("blockReason", blockReason.Value);

            var responseBody = Execute(path, param, "GET");
            var r = JsonConvert.DeserializeObject<ReturnLineBlockList>(responseBody);
            return new LineBlockListResult
            {
                Metadata = r.metadata,
                Entries = r.entries
            };
        }
#if !NET35
        public async Task<LineBlockListResult> ListBlockedLinesAsync(string lineNumber, int pageNumber, long startDate,
            byte? blockReason = null)
        {
            var path = GetApiPath("line/blocked", "list", "json");
            var param = new Dictionary<string, object>
            {
                { "lineNumber", lineNumber },
                { "pageNumber", pageNumber },
                { "StartDate", startDate }
            };
            if (blockReason.HasValue) param.Add("blockReason", blockReason.Value);

            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            var r = JsonConvert.DeserializeObject<ReturnLineBlockList>(responseBody);
            return new LineBlockListResult
            {
                Metadata = r.metadata,
                Entries = r.entries
            };
        }
#endif

        public List<BlacklistResult> BlockedLineExists(string lineNumber, string receptor)
        {
            var path = GetApiPath("line/blocked", "exists", "json");
            var param = new Dictionary<string, object>
            {
                { "lineNumber", lineNumber },
                { "receptor", receptor }
            };
            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnBlacklist>(responseBody).entries;
        }
#if !NET35
        public async Task<List<BlacklistResult>> BlockedLineExistsAsync(string lineNumber, string receptor)
        {
            var path = GetApiPath("line/blocked", "exists", "json");
            var param = new Dictionary<string, object>
            {
                { "lineNumber", lineNumber },
                { "receptor", receptor }
            };
            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnBlacklist>(responseBody).entries;
        }
#endif

        // Verification Template Management API
        public List<TemplateResult> ListTemplates(string apiKey = null, string localId = null, int? page = null)
        {
            var path = GetApiPath("verify", "templatelist", "json");
            var param = new Dictionary<string, object>();
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);
            if (page.HasValue) param.Add("page", page.Value);

            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnTemplatesList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<TemplateResult>> ListTemplatesAsync(string apiKey = null, string localId = null, int? page = null)
        {
            var path = GetApiPath("verify", "templatelist", "json");
            var param = new Dictionary<string, object>();
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);
            if (page.HasValue) param.Add("page", page.Value);

            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnTemplatesList>(responseBody).entries;
        }
#endif

        public CloneTemplateResult CloneTemplate(int? sourceTemplateId = null, string sourceTemplateName = null,
            string newTemplateName = null, string apiKey = null, string localId = null)
        {
            var path = GetApiPath("verify", "clonetemplate", "json");
            var param = new Dictionary<string, object>();
            if (sourceTemplateId.HasValue) param.Add("sourcetemplateid", sourceTemplateId.Value);
            if (sourceTemplateName != null) param.Add("sourcetemplatename", sourceTemplateName);
            if (newTemplateName != null) param.Add("newtemplatename", newTemplateName);
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnCloneTemplate>(responseBody).entries;
        }
#if !NET35
        public async Task<CloneTemplateResult> CloneTemplateAsync(int? sourceTemplateId = null, string sourceTemplateName = null,
            string newTemplateName = null, string apiKey = null, string localId = null)
        {
            var path = GetApiPath("verify", "clonetemplate", "json");
            var param = new Dictionary<string, object>();
            if (sourceTemplateId.HasValue) param.Add("sourcetemplateid", sourceTemplateId.Value);
            if (sourceTemplateName != null) param.Add("sourcetemplatename", sourceTemplateName);
            if (newTemplateName != null) param.Add("newtemplatename", newTemplateName);
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnCloneTemplate>(responseBody).entries;
        }
#endif

        public TemplateInfoResult AddTemplate(TemplateDto dto)
        {
            var path = GetApiPath("verify", "addtemplate", "json");
            var param = new Dictionary<string, object>();
            if (dto.SourceType.HasValue) param.Add("sourcetype", (byte)dto.SourceType.Value);
            if (dto.SendMethod.HasValue) param.Add("sendmethod", (int)dto.SendMethod.Value);
            if (dto.FallBackMethod.HasValue) param.Add("fallbackmethod", (int)dto.FallBackMethod.Value);
            if (dto.PrimaryLineNumber != null) param.Add("primarylinenumber", dto.PrimaryLineNumber);
            if (dto.SecondaryLineNumber != null) param.Add("secondarylinenumber", dto.SecondaryLineNumber);
            if (dto.SwitchTTL.HasValue) param.Add("switchttl", dto.SwitchTTL.Value);
            if (dto.SourceUrl != null) param.Add("sourceurl", dto.SourceUrl);
            if (dto.SourceName != null) param.Add("sourcename", dto.SourceName);
            if (dto.Name != null) param.Add("name", dto.Name);
            if (dto.TextMessage != null)
                param.Add("textmessage", HttpUtility.UrlEncodeUnicode(dto.TextMessage));
            if (dto.VoiceMessage != null)
                param.Add("voicemessage", HttpUtility.UrlEncodeUnicode(dto.VoiceMessage));
            if (dto.LocalId != null) param.Add("localid", dto.LocalId);
            if (dto.ApiKey != null) param.Add("apikey", dto.ApiKey);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnTemplateInfo>(responseBody).entries;
        }
#if !NET35
        public async Task<TemplateInfoResult> AddTemplateAsync(TemplateDto dto)
        {
            var path = GetApiPath("verify", "addtemplate", "json");
            var param = new Dictionary<string, object>();
            if (dto.SourceType.HasValue) param.Add("sourcetype", (byte)dto.SourceType.Value);
            if (dto.SendMethod.HasValue) param.Add("sendmethod", (int)dto.SendMethod.Value);
            if (dto.FallBackMethod.HasValue) param.Add("fallbackmethod", (int)dto.FallBackMethod.Value);
            if (dto.PrimaryLineNumber != null) param.Add("primarylinenumber", dto.PrimaryLineNumber);
            if (dto.SecondaryLineNumber != null) param.Add("secondarylinenumber", dto.SecondaryLineNumber);
            if (dto.SwitchTTL.HasValue) param.Add("switchttl", dto.SwitchTTL.Value);
            if (dto.SourceUrl != null) param.Add("sourceurl", dto.SourceUrl);
            if (dto.SourceName != null) param.Add("sourcename", dto.SourceName);
            if (dto.Name != null) param.Add("name", dto.Name);
            if (dto.TextMessage != null)
                param.Add("textmessage", HttpUtility.UrlEncodeUnicode(dto.TextMessage));
            if (dto.VoiceMessage != null)
                param.Add("voicemessage", HttpUtility.UrlEncodeUnicode(dto.VoiceMessage));
            if (dto.LocalId != null) param.Add("localid", dto.LocalId);
            if (dto.ApiKey != null) param.Add("apikey", dto.ApiKey);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnTemplateInfo>(responseBody).entries;
        }
#endif

        public TemplateInfoResult UpdateTemplate(int templateId, TemplateDto dto)
        {
            var path = GetApiPath("verify", "updatetemplate", "json");
            var param = new Dictionary<string, object>
            {
                { "templateid", templateId }
            };
            if (dto.SourceType.HasValue) param.Add("sourcetype", (byte)dto.SourceType.Value);
            if (dto.SendMethod.HasValue) param.Add("sendmethod", (int)dto.SendMethod.Value);
            if (dto.FallBackMethod.HasValue) param.Add("fallbackmethod", (int)dto.FallBackMethod.Value);
            if (dto.PrimaryLineNumber != null) param.Add("primarylinenumber", dto.PrimaryLineNumber);
            if (dto.SecondaryLineNumber != null) param.Add("secondarylinenumber", dto.SecondaryLineNumber);
            if (dto.SwitchTTL.HasValue) param.Add("switchttl", dto.SwitchTTL.Value);
            if (dto.SourceUrl != null) param.Add("sourceurl", dto.SourceUrl);
            if (dto.SourceName != null) param.Add("sourcename", dto.SourceName);
            if (dto.Name != null) param.Add("name", dto.Name);
            if (dto.TextMessage != null)
                param.Add("textmessage", HttpUtility.UrlEncodeUnicode(dto.TextMessage));
            if (dto.VoiceMessage != null)
                param.Add("voicemessage", HttpUtility.UrlEncodeUnicode(dto.VoiceMessage));
            if (dto.LocalId != null) param.Add("localid", dto.LocalId);
            if (dto.ApiKey != null) param.Add("apikey", dto.ApiKey);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnTemplateInfo>(responseBody).entries;
        }
#if !NET35
        public async Task<TemplateInfoResult> UpdateTemplateAsync(int templateId, TemplateDto dto)
        {
            var path = GetApiPath("verify", "updatetemplate", "json");
            var param = new Dictionary<string, object>
            {
                { "templateid", templateId }
            };
            if (dto.SourceType.HasValue) param.Add("sourcetype", (byte)dto.SourceType.Value);
            if (dto.SendMethod.HasValue) param.Add("sendmethod", (int)dto.SendMethod.Value);
            if (dto.FallBackMethod.HasValue) param.Add("fallbackmethod", (int)dto.FallBackMethod.Value);
            if (dto.PrimaryLineNumber != null) param.Add("primarylinenumber", dto.PrimaryLineNumber);
            if (dto.SecondaryLineNumber != null) param.Add("secondarylinenumber", dto.SecondaryLineNumber);
            if (dto.SwitchTTL.HasValue) param.Add("switchttl", dto.SwitchTTL.Value);
            if (dto.SourceUrl != null) param.Add("sourceurl", dto.SourceUrl);
            if (dto.SourceName != null) param.Add("sourcename", dto.SourceName);
            if (dto.Name != null) param.Add("name", dto.Name);
            if (dto.TextMessage != null)
                param.Add("textmessage", HttpUtility.UrlEncodeUnicode(dto.TextMessage));
            if (dto.VoiceMessage != null)
                param.Add("voicemessage", HttpUtility.UrlEncodeUnicode(dto.VoiceMessage));
            if (dto.LocalId != null) param.Add("localid", dto.LocalId);
            if (dto.ApiKey != null) param.Add("apikey", dto.ApiKey);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnTemplateInfo>(responseBody).entries;
        }
#endif

        public TemplateResult GetTemplate(int id, string apiKey = null, string localId = null)
        {
            var path = GetApiPath("verify", "gettemplate", "json");
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);

            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnTemplate>(responseBody).entries;
        }
#if !NET35
        public async Task<TemplateResult> GetTemplateAsync(int id, string apiKey = null, string localId = null)
        {
            var path = GetApiPath("verify", "gettemplate", "json");
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);

            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnTemplate>(responseBody).entries;
        }
#endif

        public string DeleteTemplate(int id, string apiKey = null, string localId = null)
        {
            var path = GetApiPath("verify", "deletetemplate", "json");
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);

            var responseBody = Execute(path, param, "DELETE");
            var r = JsonConvert.DeserializeObject<ReturnResult>(responseBody);
            return r.Return?.message;
        }
#if !NET35
        public async Task<string> DeleteTemplateAsync(int id, string apiKey = null, string localId = null)
        {
            var path = GetApiPath("verify", "deletetemplate", "json");
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };
            if (apiKey != null) param.Add("apikey", apiKey);
            if (localId != null) param.Add("localid", localId);

            var responseBody = await ExecuteAsync(path, param, "DELETE").ConfigureAwait(false);
            var r = JsonConvert.DeserializeObject<ReturnResult>(responseBody);
            return r.Return?.message;
        }
#endif

        // Media API
        public MediaResult UploadMedia(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string name = Path.GetFileName(filePath);
            return UploadMedia(name, fileBytes);
        }
#if !NET35
        public async Task<MediaResult> UploadMediaAsync(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string name = Path.GetFileName(filePath);
            return await UploadMediaAsync(name, fileBytes).ConfigureAwait(false);
        }
#endif

        public MediaResult UploadMedia(string name, byte[] fileBytes)
        {
            var path = GetApiPath("media", "upload", "json");
            var responseBody = ExecuteMultipart(path, "File", name, fileBytes);
            var l = JsonConvert.DeserializeObject<ReturnMedia>(responseBody);
            return l.entries;
        }
#if !NET35
        public async Task<MediaResult> UploadMediaAsync(string name, byte[] fileBytes)
        {
            var path = GetApiPath("media", "upload", "json");
            var responseBody = await ExecuteMultipartAsync(path, "File", name, fileBytes).ConfigureAwait(false);
            var l = JsonConvert.DeserializeObject<ReturnMedia>(responseBody);
            return l.entries;
        }
#endif

        public MediaListResult ListMedia(int? page = null, int? size = null)
        {
            var path = GetApiPath("media", "list", "json");
            var param = new Dictionary<string, object>();
            if (page.HasValue) param.Add("page", page.Value);
            if (size.HasValue) param.Add("size", size.Value);

            var responseBody = Execute(path, param, "GET");
            var r = JsonConvert.DeserializeObject<ReturnMediaList>(responseBody);
            return new MediaListResult
            {
                Page = r.entries?.page ?? 0,
                Size = r.entries?.size ?? 0,
                Total = r.entries?.total ?? 0,
                List = r.entries?.list
            };
        }
#if !NET35
        public async Task<MediaListResult> ListMediaAsync(int? page = null, int? size = null)
        {
            var path = GetApiPath("media", "list", "json");
            var param = new Dictionary<string, object>();
            if (page.HasValue) param.Add("page", page.Value);
            if (size.HasValue) param.Add("size", size.Value);

            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            var r = JsonConvert.DeserializeObject<ReturnMediaList>(responseBody);
            return new MediaListResult
            {
                Page = r.entries?.page ?? 0,
                Size = r.entries?.size ?? 0,
                Total = r.entries?.total ?? 0,
                List = r.entries?.list
            };
        }
#endif

        public MediaResult GetMedia(Guid? id = null, string fileName = null)
        {
            var path = GetApiPath("media", "get", "json");
            var param = new Dictionary<string, object>();
            if (id.HasValue) param.Add("id", id.Value);
            if (fileName != null) param.Add("filename", fileName);

            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnMedia>(responseBody).entries;
        }
#if !NET35
        public async Task<MediaResult> GetMediaAsync(Guid? id = null, string fileName = null)
        {
            var path = GetApiPath("media", "get", "json");
            var param = new Dictionary<string, object>();
            if (id.HasValue) param.Add("id", id.Value);
            if (fileName != null) param.Add("filename", fileName);

            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnMedia>(responseBody).entries;
        }
#endif

        public MediaDeleteResult DeleteMedia(Guid id)
        {
            var path = GetApiPath("media", "delete", "json");
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };
            var responseBody = Execute(path, param, "DELETE");
            return JsonConvert.DeserializeObject<ReturnMediaDelete>(responseBody).entries;
        }
#if !NET35
        public async Task<MediaDeleteResult> DeleteMediaAsync(Guid id)
        {
            var path = GetApiPath("media", "delete", "json");
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };
            var responseBody = await ExecuteAsync(path, param, "DELETE").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnMediaDelete>(responseBody).entries;
        }
#endif

        // Contact & Groups API
        public List<ContactResult> AddContact(int groupId, string number, string title = null, string birthdate = null,
            string email = null, string tags = null)
        {
            var path = GetApiPath("group", "add", "json");
            var param = new Dictionary<string, object>
            {
                { "groupid", groupId },
                { "number", number }
            };
            if (title != null) param.Add("title", title);
            if (birthdate != null) param.Add("birthdate", birthdate);
            if (email != null) param.Add("email", email);
            if (tags != null) param.Add("tags", tags);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnContactsList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<ContactResult>> AddContactAsync(int groupId, string number, string title = null, string birthdate = null,
            string email = null, string tags = null)
        {
            var path = GetApiPath("group", "add", "json");
            var param = new Dictionary<string, object>
            {
                { "groupid", groupId },
                { "number", number }
            };
            if (title != null) param.Add("title", title);
            if (birthdate != null) param.Add("birthdate", birthdate);
            if (email != null) param.Add("email", email);
            if (tags != null) param.Add("tags", tags);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnContactsList>(responseBody).entries;
        }
#endif

        public List<GroupResult> AddGroup(string name, string tag = null)
        {
            var path = GetApiPath("group", "addgroup", "json");
            var param = new Dictionary<string, object>
            {
                { "name", name }
            };
            if (tag != null) param.Add("tag", tag);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<GroupResult>> AddGroupAsync(string name, string tag = null)
        {
            var path = GetApiPath("group", "addgroup", "json");
            var param = new Dictionary<string, object>
            {
                { "name", name }
            };
            if (tag != null) param.Add("tag", tag);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#endif

        public List<ContactResult> RemoveContact(string contactId = null, string mobile = null, int? groupId = null)
        {
            var path = GetApiPath("contact", "remove", "json");
            var param = new Dictionary<string, object>();
            if (contactId != null) param.Add("contactid", contactId);
            if (mobile != null) param.Add("mobile", mobile);
            if (groupId.HasValue) param.Add("groupid", groupId.Value);

            var responseBody = Execute(path, param, "DELETE");
            return JsonConvert.DeserializeObject<ReturnContactsList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<ContactResult>> RemoveContactAsync(string contactId = null, string mobile = null, int? groupId = null)
        {
            var path = GetApiPath("contact", "remove", "json");
            var param = new Dictionary<string, object>();
            if (contactId != null) param.Add("contactid", contactId);
            if (mobile != null) param.Add("mobile", mobile);
            if (groupId.HasValue) param.Add("groupid", groupId.Value);

            var responseBody = await ExecuteAsync(path, param, "DELETE").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnContactsList>(responseBody).entries;
        }
#endif

        public List<GroupResult> RemoveGroup(int groupId)
        {
            var path = GetApiPath("group", "removegroup", "json");
            var param = new Dictionary<string, object>
            {
                { "groupid", groupId }
            };
            var responseBody = Execute(path, param, "DELETE");
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<GroupResult>> RemoveGroupAsync(int groupId)
        {
            var path = GetApiPath("group", "removegroup", "json");
            var param = new Dictionary<string, object>
            {
                { "groupid", groupId }
            };
            var responseBody = await ExecuteAsync(path, param, "DELETE").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#endif

        public List<GroupResult> ListGroups()
        {
            var path = GetApiPath("group", "listofgroups", "json");
            var param = new Dictionary<string, object>();

            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<GroupResult>> ListGroupsAsync()
        {
            var path = GetApiPath("group", "listofgroups", "json");
            var param = new Dictionary<string, object>();

            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#endif

        public List<GroupResult> EditGroup(int groupId, string groupName = null, byte? status = null, string tag = null)
        {
            var path = GetApiPath("group", "editgroup", "json");
            var param = new Dictionary<string, object>
            {
                { "groupid", groupId }
            };
            if (groupName != null) param.Add("groupname", groupName);
            if (status.HasValue)
            {
                param.Add("status", status.Value);
            }
            else
            {
                param.Add("status", 1);
            }
                
            if (tag != null) param.Add("tag", tag);

            var responseBody = Execute(path, param);
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<GroupResult>> EditGroupAsync(int groupId, string groupName = null, byte? status = null, string tag = null)
        {
            var path = GetApiPath("group", "editgroup", "json");
            var param = new Dictionary<string, object>
            {
                { "groupid", groupId }
            };
            if (groupName != null) param.Add("groupname", groupName);
            if (status.HasValue)
            {
                param.Add("status", status.Value);
            }
            else
            {
                param.Add("status", 1);
            }
            if (tag != null) param.Add("tag", tag);

            var responseBody = await ExecuteAsync(path, param).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#endif

        public List<GroupResult> SearchGroups(string tag)
        {
            var path = GetApiPath("group", "search", "json");
            var param = new Dictionary<string, object>
            {
                { "tag", tag }
            };
            var responseBody = Execute(path, param, "GET");
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#if !NET35
        public async Task<List<GroupResult>> SearchGroupsAsync(string tag)
        {
            var path = GetApiPath("group", "search", "json");
            var param = new Dictionary<string, object>
            {
                { "tag", tag }
            };
            var responseBody = await ExecuteAsync(path, param, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnGroupsList>(responseBody).entries;
        }
#endif

        // Utilities API
        public ServerDateResult GetServerDate()
        {
            var path = GetApiPath("utils", "getdate", "json");
            var responseBody = Execute(path, null, "GET");
            return JsonConvert.DeserializeObject<ReturnServerDate>(responseBody).entries;
        }
#if !NET35
        public async Task<ServerDateResult> GetServerDateAsync()
        {
            var path = GetApiPath("utils", "getdate", "json");
            var responseBody = await ExecuteAsync(path, null, "GET").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ReturnServerDate>(responseBody).entries;
        }
#endif

        public string Ping()
        {
            var path = GetApiPath("utils", "ping", "json");
            var responseBody = Execute(path, null, "GET");
            var r = JsonConvert.DeserializeObject<ReturnPing>(responseBody);
            return r.entries?.Status;
        }
#if !NET35
        public async Task<string> PingAsync()
        {
            var path = GetApiPath("utils", "ping", "json");
            var responseBody = await ExecuteAsync(path, null, "GET").ConfigureAwait(false);
            var r = JsonConvert.DeserializeObject<ReturnPing>(responseBody);
            return r.entries?.Status;
        }
#endif

        private string GetMimeType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return "application/octet-stream";
            }

            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            switch (extension)
            {
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".gif": return "image/gif";
                case ".bmp": return "image/bmp";
                case ".webp": return "image/webp";
                case ".mp4": return "video/mp4";
                case ".pdf": return "application/pdf";
                case ".txt": return "text/plain";
                default: return "application/octet-stream"; // Safe fallback for unknown types
            }
        }

        private string ExecuteMultipart(string path, string fileParamName, string fileName, byte[] fileBytes)
        {
            string mimeType = GetMimeType(fileName);

#if NET35
    return ExecuteMultipartLegacy(path, fileParamName, fileName, fileBytes);
#else
            var client = _httpClient ?? _defaultHttpClient;

            using (var request = new HttpRequestMessage(HttpMethod.Post, path))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                var content = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(fileBytes);

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                content.Add(fileContent, fileParamName, fileName);

                request.Content = content;

                HttpResponseMessage httpResponse;
                try
                {
                    httpResponse = client.SendAsync(request).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    throw new HttpException(ex.Message, 0);
                }

                string responseBody = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    ReturnResult result = null;
                    try
                    {
                        result = JsonConvert.DeserializeObject<ReturnResult>(responseBody);
                    }
                    catch
                    {
                    }

                    if (result != null && result.Return != null)
                    {
                        throw new ApiException(result.Return.message, result.Return.status);
                    }

                    throw new HttpException(httpResponse.ReasonPhrase, (int)httpResponse.StatusCode);
                }

                return responseBody;
            }
#endif
        }

#if !NET35
        private async Task<string> ExecuteMultipartAsync(string path, string fileParamName,
            string fileName, byte[] fileBytes, CancellationToken cancellationToken = default)
        {
            string mimeType = GetMimeType(fileName);
            var client = _httpClient ?? _defaultHttpClient;

            using (var request = new HttpRequestMessage(HttpMethod.Post, path))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                var content = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(fileBytes);

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                content.Add(fileContent, fileParamName, fileName);

                request.Content = content;

                HttpResponseMessage httpResponse;
                try
                {
                    httpResponse = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new HttpException(ex.Message, 0);
                }

                string responseBody = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    ReturnResult result = null;
                    try
                    {
                        result = JsonConvert.DeserializeObject<ReturnResult>(responseBody);
                    }
                    catch
                    {
                    }

                    if (result != null && result.Return != null)
                    {
                        throw new ApiException(result.Return.message, result.Return.status);
                    }

                    throw new HttpException(httpResponse.ReasonPhrase, (int)httpResponse.StatusCode);
                }

                return responseBody;
            }
        }
#endif


        private string ExecuteMultipartLegacy(string path, string fileParamName, string fileName, byte[] fileBytes)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            var webRequest = (HttpWebRequest)WebRequest.Create(path);
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            webRequest.Timeout = -1;

            using (Stream requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(boundarybytes, 0, boundarybytes.Length);
                string headerTemplate =
                    "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                string header = string.Format(headerTemplate, fileParamName, fileName);
                byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                requestStream.Write(headerbytes, 0, headerbytes.Length);
                requestStream.Write(fileBytes, 0, fileBytes.Length);

                byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(trailer, 0, trailer.Length);
            }

            HttpWebResponse webResponse;
            try
            {
                using (webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException webException)
            {
                webResponse = (HttpWebResponse)webException.Response;
                string responseBody = "";
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

        internal class ReturnPing
        {
            public Result result { get; set; }
            public Result @Return { get; set; }
            public PingResult entries { get; set; }
        }

        internal class PingResult
        {
            public string Status { get; set; }
        }
    }
}
