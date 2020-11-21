using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kavenegar.Models;
using Kavenegar.Models.Enums;

namespace Kavenegar
{
    public interface IKavenegarApi
    {
        Task<SendResult> SelectAsync(string messageid, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SelectAsync(List<string> messageids,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> SelectOutboxAsync(DateTime startdate,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate, string sender,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> LatestOutboxAsync(long pagesize,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> LatestOutboxAsync(long pagesize, string sender,
            CancellationToken cancellationToken = default);

        Task<CountOutboxResult> CountOutboxAsync(DateTime startdate,
            CancellationToken cancellationToken = default);

        Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate,
            CancellationToken cancellationToken = default);

        Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate, int status,
            CancellationToken cancellationToken = default);

        Task<List<StatusResult>>
            CancelAsync(List<string> ids, CancellationToken cancellationToken = default);

        Task<StatusResult> CancelAsync(string messageid, CancellationToken cancellationToken = default);

        Task<List<ReceiveResult>> ReceiveAsync(string line, int isread,
            CancellationToken cancellationToken = default);

        Task<CountInboxResult> CountInboxAsync(DateTime startdate, string linenumber,
            CancellationToken cancellationToken = default);

        Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, string linenumber,
            CancellationToken cancellationToken = default);

        Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, string linenumber,
            int isread, CancellationToken cancellationToken = default);

        Task<List<CountPostalCodeResult>> CountPostalCodeAsync(long postalcode,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendByPostalCodeAsync(long postalcode, string sender, string message,
            long mcistartIndex, long mcicount, long mtnstartindex, long mtncount,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendByPostalCodeAsync(long postalcode, string sender, string message,
            long mcistartIndex, long mcicount, long mtnstartindex, long mtncount, DateTime date,
            CancellationToken cancellationToken = default);

        #region Send

        Task<SendResult> SendAsync(string sender, string receptor, string message,
            CancellationToken cancellationToken = default);

        Task<SendResult> SendAsync(string sender, string receptor, string message, MessageType type,
            DateTime date, CancellationToken cancellationToken = default);

        Task<SendResult> SendAsync(string sender, string receptor, string message, MessageType type,
            DateTime date, string localid, CancellationToken cancellationToken = default);

        Task<SendResult> SendAsync(string sender, string receptor, string message, string localid,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendAsync(string sender, List<string> receptors, string message,
            string localid, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message,
            MessageType type, DateTime date, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message,
            MessageType type, DateTime date, List<string> localids, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors,
            List<string> messages, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages,
            string localmessageid, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages,
            MessageType type, DateTime date, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages,
            MessageType type, DateTime date, string localmessageids, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors,
            List<string> messages, string localmessageid, CancellationToken cancellationToken = default);

        Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors,
            List<string> messages, List<MessageType> types, DateTime date, List<string> localmessageids,
            CancellationToken cancellationToken = default);

        #endregion

        #region Status

        Task<StatusResult> StatusAsync(string messageid, CancellationToken cancellationToken = default);

        Task<List<StatusResult>> StatusAsync(List<string> messageids,
            CancellationToken cancellationToken = default);

        Task<List<StatusLocalMessageIdResult>> StatusLocalMessageIdAsync(List<string> messageids,
            CancellationToken cancellationToken = default);

        Task<StatusLocalMessageIdResult> StatusLocalMessageIdAsync(string messageid,
            CancellationToken cancellationToken = default);

        #endregion

        #region Account

        Task<AccountInfoResult> AccountInfoAsync(CancellationToken cancellationToken = default);

        Task<AccountConfigResult> AccountConfigAsync(string apilogs, string dailyreport, string debugmode,
            string defaultsender, int? mincreditalarm, string resendfailed,
            CancellationToken cancellationToken = default);

        #endregion

        #region Verify

        Task<SendResult> VerifyLookupAsync(string receptor, string token, string template,
            CancellationToken cancellationToken = default);

        Task<SendResult> VerifyLookupAsync(string receptor, string token, string template,
            VerifyLookupType type, CancellationToken cancellationToken = default);

        Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3,
            string template, CancellationToken cancellationToken = default);

        Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3,
            string token10, string template, CancellationToken cancellationToken = default);

        Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3,
            string template, VerifyLookupType type, CancellationToken cancellationToken = default);

        Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3,
            string token10, string template, VerifyLookupType type, CancellationToken cancellationToken = default);

        Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3,
            string token10, string token20, string template, VerifyLookupType type,
            CancellationToken cancellationToken = default);

        #endregion

        #region << CallMakeTTS >>

        Task<SendResult> CallMakeTTSAsync(string message, string receptor,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor,
            CancellationToken cancellationToken = default);

        Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor, DateTime? date,
            List<string> localid, CancellationToken cancellationToken = default);

        #endregion << CallMakeTTS >>
    }
}