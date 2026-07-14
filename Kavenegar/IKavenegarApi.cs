using System;
using System.Collections.Generic;
using Kavenegar.Models;
using Kavenegar.Models.Enums;

namespace Kavenegar
{
    /// <summary>
    /// Kavenegar SMS, Voice, and Verification API client contract.
    /// Provides methods to send messages, manage templates, query inbox/outbox, manage sub-accounts, and utilize blacklist services.
    /// </summary>
    public interface IKavenegarApi
    {
        /// <summary>
        /// Gets or sets the Kavenegar API key used for request authentication.
        /// </summary>
        string ApiKey { set; get; }

        /// <summary>
        /// Sends a simple SMS message to a list of recipients.
        /// </summary>
        /// <param name="sender">The pre-registered Kavenegar sender line number.</param>
        /// <param name="receptor">A list of recipient mobile numbers.</param>
        /// <param name="message">The text body of the message.</param>
        /// <returns>A list of send result entries containing message IDs and cost.</returns>
        List<SendResult> Send(string sender, List<string> receptor, string message);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message);
#endif

        /// <summary>
        /// Sends a simple SMS message to a single recipient.
        /// </summary>
        /// <param name="sender">The pre-registered Kavenegar sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <returns>A send result entry containing message ID, cost, and delivery status.</returns>
        SendResult Send(string sender, String receptor, string message);
#if !NET35
        System.Threading.Tasks.Task<SendResult> SendAsync(string sender, String receptor, string message);
#endif

        /// <summary>
        /// Sends an SMS message to a single recipient with a specific message type and scheduled date.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="type">The storage or transmission type of the message.</param>
        /// <param name="date">The scheduled dispatch date and time (use DateTime.MinValue for immediate delivery).</param>
        /// <returns>A send result entry.</returns>
        SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date);
#if !NET35
        System.Threading.Tasks.Task<SendResult> SendAsync(string sender, string receptor, string message, MessageType type, DateTime date);
#endif

        /// <summary>
        /// Sends an SMS message to multiple recipients with a specific message type and scheduled date.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">A list of recipient mobile numbers.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="type">The storage or transmission type of the message.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, MessageType type, DateTime date);
#endif

        /// <summary>
        /// Sends an SMS message to a single recipient with a custom local ID for duplicate protection.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="type">The storage or transmission type of the message.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <param name="localid">A unique local client ID to prevent double dispatch.</param>
        /// <returns>A send result entry.</returns>
        SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date, string localid);
#if !NET35
        System.Threading.Tasks.Task<SendResult> SendAsync(string sender, string receptor, string message, MessageType type, DateTime date, string localid);
#endif

        /// <summary>
        /// Sends an SMS message to a single recipient with a custom local ID.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="localid">A unique local client ID.</param>
        /// <returns>A send result entry.</returns>
        SendResult Send(string sender, string receptor, string message, string localid);
#if !NET35
        System.Threading.Tasks.Task<SendResult> SendAsync(string sender, string receptor, string message, string localid);
#endif

        /// <summary>
        /// Sends an SMS message to multiple recipients with a custom local ID.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="localid">A unique local client ID.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> Send(string sender, List<string> receptors, string message, string localid);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendAsync(string sender, List<string> receptors, string message, string localid);
#endif

        /// <summary>
        /// Sends an SMS message to multiple recipients with the full suite of advanced optional configurations.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">A list of recipient mobile numbers.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="type">The storage or transmission type of the message.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <param name="localids">A list of unique local client IDs for each recipient.</param>
        /// <param name="tag">A custom category tag.</param>
        /// <param name="text">Additional raw text context.</param>
        /// <param name="moderator">The moderator username/id.</param>
        /// <param name="udh">User Data Header metadata string.</param>
        /// <param name="hide">Flag to hide message contents in logs.</param>
        /// <param name="checkMessageId">Flag to enforce message ID matching check.</param>
        /// <param name="localMessageId">A local client message identifier.</param>
        /// <param name="policy">Spam/dispatch filtration policy.</param>
        /// <param name="mediaId">MMS attachment media GUID.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date, List<string> localids, string tag = null, string text = null, string moderator = null, string udh = null, string hide = null, string checkMessageId = null, string localMessageId = null, string policy = null, Guid? mediaId = null);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, MessageType type, DateTime date, List<string> localids, string tag = null, string text = null, string moderator = null, string udh = null, string hide = null, string checkMessageId = null, string localMessageId = null, string policy = null, Guid? mediaId = null);
#endif

        /// <summary>
        /// Sends a bulk array of different messages to different recipients from different lines.
        /// </summary>
        /// <param name="senders">A list of sender line numbers matching the receptors.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies matching the receptors.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages);
#endif

        /// <summary>
        /// Sends a bulk array of messages using a single sender line with custom type and dispatch time.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies.</param>
        /// <param name="type">The transmission type.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date);
#endif

        /// <summary>
        /// Sends a bulk array of messages using a single sender line with custom type, dispatch time, and local IDs.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies.</param>
        /// <param name="type">The transmission type.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <param name="localmessageids">Comma-separated local message IDs.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date, string localmessageids);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date, string localmessageids);
#endif

        /// <summary>
        /// Sends a bulk array of messages using a single sender line with local message IDs.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message text bodies.</param>
        /// <param name="localmessageid">A unique local client ID.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, string localmessageid);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, string localmessageid);
#endif

        /// <summary>
        /// Sends a bulk array of messages matching multiple senders, receptors, and message texts with a local identifier.
        /// </summary>
        /// <param name="senders">A list of sender lines.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies.</param>
        /// <param name="localmessageid">A unique local client ID.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages, string localmessageid);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages, string localmessageid);
#endif

        /// <summary>
        /// Sends a bulk array of messages with advanced parameters and policy controls.
        /// </summary>
        /// <param name="senders">A list of sender lines.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies.</param>
        /// <param name="types">A list of transmission types matching the inputs.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <param name="localmessageids">A list of local message identifiers.</param>
        /// <param name="tag">A category tag for billing/reports.</param>
        /// <param name="moderator">The moderator username/id.</param>
        /// <param name="hide">Flag to hide message content in logs.</param>
        /// <param name="causal">The delivery prioritization speed.</param>
        /// <param name="policy">Filtering policies.</param>
        /// <param name="mediaId">MMS attachment GUID.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages, List<MessageType> types, DateTime date, List<string> localmessageids, string tag = null, string hide = null, string policy = null, Guid? mediaId = null);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages, List<MessageType> types, DateTime date, List<string> localmessageids, string tag = null, string hide = null, string policy = null, Guid? mediaId = null);
#endif

        /// <summary>
        /// Queries the delivery status of multiple messages using their Kavenegar message IDs.
        /// </summary>
        /// <param name="messageids">A list of message IDs.</param>
        /// <returns>A list of delivery status entries.</returns>
        List<StatusResult> Status(List<string> messageids);
#if !NET35
        System.Threading.Tasks.Task<List<StatusResult>> StatusAsync(List<string> messageids);
#endif

        /// <summary>
        /// Queries the delivery status of a single message using its Kavenegar message ID.
        /// </summary>
        /// <param name="messageid">The message ID.</param>
        /// <returns>A delivery status entry.</returns>
        StatusResult Status(string messageid);
#if !NET35
        System.Threading.Tasks.Task<StatusResult> StatusAsync(string messageid);
#endif

        /// <summary>
        /// Queries the delivery status of multiple messages using their local client identifiers.
        /// </summary>
        /// <param name="messageids">A list of local message IDs.</param>
        /// <returns>A list of status results.</returns>
        List<StatusLocalMessageIdResult> StatusLocalMessageId(List<string> messageids);
#if !NET35
        System.Threading.Tasks.Task<List<StatusLocalMessageIdResult>> StatusLocalMessageIdAsync(List<string> messageids);
#endif

        /// <summary>
        /// Queries the delivery status of a single message using its local client identifier.
        /// </summary>
        /// <param name="messageid">The local message ID.</param>
        /// <returns>A status result.</returns>
        StatusLocalMessageIdResult StatusLocalMessageId(string messageid);
#if !NET35
        System.Threading.Tasks.Task<StatusLocalMessageIdResult> StatusLocalMessageIdAsync(string messageid);
#endif

        /// <summary>
        /// Retrieves dispatch metadata for a list of messages.
        /// </summary>
        /// <param name="messageids">A list of message IDs.</param>
        /// <returns>A list of message log entries.</returns>
        List<SendResult> Select(List<string> messageids);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SelectAsync(List<string> messageids);
#endif

        /// <summary>
        /// Retrieves dispatch metadata for a single message.
        /// </summary>
        /// <param name="messageid">The message ID.</param>
        /// <returns>A message log entry.</returns>
        SendResult Select(string messageid);
#if !NET35
        System.Threading.Tasks.Task<SendResult> SelectAsync(string messageid);
#endif

        /// <summary>
        /// Retrieves outgoing outbox logs starting from a specific date.
        /// </summary>
        /// <param name="startdate">The query start date and time.</param>
        /// <returns>A list of outgoing message logs.</returns>
        List<SendResult> SelectOutbox(DateTime startdate);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SelectOutboxAsync(DateTime startdate);
#endif

        /// <summary>
        /// Retrieves outgoing outbox logs within a specific date range.
        /// </summary>
        /// <param name="startdate">The query start date.</param>
        /// <param name="enddate">The query end date.</param>
        /// <returns>A list of outgoing message logs.</returns>
        List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate);
#endif

        /// <summary>
        /// Retrieves outgoing outbox logs within a date range sent from a specific sender line.
        /// </summary>
        /// <param name="startdate">The query start date.</param>
        /// <param name="enddate">The query end date.</param>
        /// <param name="sender">The sender line number.</param>
        /// <returns>A list of outgoing message logs.</returns>
        List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate, String sender);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate, String sender);
#endif

        /// <summary>
        /// Retrieves the most recent outgoing outbox logs.
        /// </summary>
        /// <param name="pagesize">The page size / maximum number of records to retrieve.</param>
        /// <returns>A list of outbox log entries.</returns>
        List<SendResult> LatestOutbox(long pagesize);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> LatestOutboxAsync(long pagesize);
#endif

        /// <summary>
        /// Retrieves the most recent outgoing outbox logs filtered by a sender line.
        /// </summary>
        /// <param name="pagesize">The maximum number of records to retrieve.</param>
        /// <param name="sender">The sender line number.</param>
        /// <returns>A list of outbox log entries.</returns>
        List<SendResult> LatestOutbox(long pagesize, String sender);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> LatestOutboxAsync(long pagesize, String sender);
#endif

        /// <summary>
        /// Counts outgoing outbox messages dispatched after a start date.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <returns>A count details entry.</returns>
        CountOutboxResult CountOutbox(DateTime startdate);
#if !NET35
        System.Threading.Tasks.Task<CountOutboxResult> CountOutboxAsync(DateTime startdate);
#endif

        /// <summary>
        /// Counts outgoing outbox messages within a date range.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <returns>A count details entry.</returns>
        CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate);
#if !NET35
        System.Threading.Tasks.Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate);
#endif

        /// <summary>
        /// Counts outgoing outbox messages within a date range filtered by delivery status.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="status">The delivery status number.</param>
        /// <returns>A count details entry.</returns>
        CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate, int status);
#if !NET35
        System.Threading.Tasks.Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate, int status);
#endif

        /// <summary>
        /// Cancels scheduled delivery for multiple messages before transmission.
        /// </summary>
        /// <param name="ids">A list of scheduled message IDs.</param>
        /// <returns>A list of cancel status results.</returns>
        List<StatusResult> Cancel(List<String> ids);
#if !NET35
        System.Threading.Tasks.Task<List<StatusResult>> CancelAsync(List<String> ids);
#endif

        /// <summary>
        /// Cancels scheduled delivery for a single message.
        /// </summary>
        /// <param name="messageid">The scheduled message ID.</param>
        /// <returns>A cancel status result.</returns>
        StatusResult Cancel(String messageid);
#if !NET35
        System.Threading.Tasks.Task<StatusResult> CancelAsync(String messageid);
#endif

        /// <summary>
        /// Retrieves incoming received messages from a line.
        /// </summary>
        /// <param name="line">Your incoming line number.</param>
        /// <param name="isread">0 for unread, 1 for read.</param>
        /// <returns>A list of inbox received messages.</returns>
        List<ReceiveResult> Receive(string line, int isread);
#if !NET35
        System.Threading.Tasks.Task<List<ReceiveResult>> ReceiveAsync(string line, int isread);
#endif

        /// <summary>
        /// Counts incoming received messages after a start date.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="linenumber">Your line number.</param>
        /// <returns>An inbox count result.</returns>
        CountInboxResult CountInbox(DateTime startdate, string linenumber);
#if !NET35
        System.Threading.Tasks.Task<CountInboxResult> CountInboxAsync(DateTime startdate, string linenumber);
#endif

        /// <summary>
        /// Counts incoming received messages within a date range.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="linenumber">Your line number.</param>
        /// <returns>An inbox count result.</returns>
        CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber);
#if !NET35
        System.Threading.Tasks.Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, String linenumber);
#endif

        /// <summary>
        /// Counts incoming received messages within a date range filtered by read state.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="linenumber">Your line number.</param>
        /// <param name="isread">0 for unread, 1 for read.</param>
        /// <returns>An inbox count result.</returns>
        CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber, int isread);
#if !NET35
        System.Threading.Tasks.Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, String linenumber, int isread);
#endif

        /// <summary>
        /// Retrieves active phone counts matching a postal code area.
        /// </summary>
        /// <param name="postalcode">The postal code prefix.</param>
        /// <returns>A list of category count details.</returns>
        List<CountPostalCodeResult> CountPostalCode(long postalcode);
#if !NET35
        System.Threading.Tasks.Task<List<CountPostalCodeResult>> CountPostalCodeAsync(long postalcode);
#endif

        /// <summary>
        /// Dispatches a message to numbers registered within a specific postal code.
        /// </summary>
        /// <param name="postalcode">The postal code filter.</param>
        /// <param name="sender">The sender line number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="mcistartIndex">The start index for MCI operators.</param>
        /// <param name="mcicount">The dispatch count for MCI operators.</param>
        /// <param name="mtnstartindex">The start index for MTN operators.</param>
        /// <param name="mtncount">The dispatch count for MTN operators.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> SendByPostalCode(long postalcode, String sender, String message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendByPostalCodeAsync(long postalcode, String sender, String message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount);
#endif

        /// <summary>
        /// Dispatches a scheduled message to numbers registered within a specific postal code.
        /// </summary>
        /// <param name="postalcode">The postal code.</param>
        /// <param name="sender">The sender line number.</param>
        /// <param name="message">The text body.</param>
        /// <param name="mcistartIndex">MCI start index.</param>
        /// <param name="mcicount">MCI count.</param>
        /// <param name="mtnstartindex">MTN start index.</param>
        /// <param name="mtncount">MTN count.</param>
        /// <param name="date">The scheduled dispatch time.</param>
        /// <returns>A list of send result entries.</returns>
        List<SendResult> SendByPostalCode(long postalcode, String sender, String message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount, DateTime date);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> SendByPostalCodeAsync(long postalcode, String sender, String message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount, DateTime date);
#endif

        /// <summary>
        /// Retrieves account profile details, balance limits, and status.
        /// </summary>
        /// <returns>Account information details.</returns>
        AccountInfoResult AccountInfo();
#if !NET35
        System.Threading.Tasks.Task<AccountInfoResult> AccountInfoAsync();
#endif

        /// <summary>
        /// Configures operational behaviors, logging, alerts, and defaults on the account level.
        /// </summary>
        /// <param name="apilogs">Enable or disable API logging ("enabled" / "disabled").</param>
        /// <param name="dailyreport">Enable or disable daily metrics email.</param>
        /// <param name="debugmode">Enable or disable sandbox simulation mode.</param>
        /// <param name="defaultsender">Assign default sender line number.</param>
        /// <param name="mincreditalarm">Minimum credit threshold to trigger SMS notifications.</param>
        /// <param name="resendfailed">Enable or disable automatic fallback resends.</param>
        /// <returns>Account config status details.</returns>
        AccountConfigResult AccountConfig(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed);
#if !NET35
        System.Threading.Tasks.Task<AccountConfigResult> AccountConfigAsync(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed);
#endif

        /// <summary>
        /// Sends an immediate token-based OTP (One-Time Password) bypassing standard delivery filters.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">The verification code.</param>
        /// <param name="template">The pre-approved template name.</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string template);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string template);
#endif

        /// <summary>
        /// Sends an OTP verification token specifying the delivery medium (SMS or Call).
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">The verification code.</param>
        /// <param name="template">The template name.</param>
        /// <param name="type">Delivery medium (e.g. Sms or Call).</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string template, VerifyLookupType type);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string template, VerifyLookupType type);
#endif

        /// <summary>
        /// Sends an OTP verification token with secondary parameter (two tokens).
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token parameter %token%.</param>
        /// <param name="token2">Token parameter %token2%.</param>
        /// <param name="template">The template name.</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string token2, string template);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string template);
#endif

        /// <summary>
        /// Sends an OTP verification token with secondary parameter (two tokens) and specific transmission type.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token parameter %token%.</param>
        /// <param name="token2">Token parameter %token2%.</param>
        /// <param name="template">The template name.</param>
        /// <param name="type">Delivery medium (e.g. Sms or Call).</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string token2, string template, VerifyLookupType type);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string template, VerifyLookupType type);
#endif

        /// <summary>
        /// Sends an OTP verification token with secondary parameters.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token parameter %token%.</param>
        /// <param name="token2">Token parameter %token2%.</param>
        /// <param name="token3">Token parameter %token3%.</param>
        /// <param name="template">The template name.</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string template);
#endif

        /// <summary>
        /// Sends an OTP verification token with secondary and ternary parameters.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token %token%.</param>
        /// <param name="token2">Token %token2%.</param>
        /// <param name="token3">Token %token3%.</param>
        /// <param name="token10">Token %token10%.</param>
        /// <param name="template">The template name.</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string template);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string template);
#endif

        /// <summary>
        /// Sends an OTP verification token with parameters and specific transmission type.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token %token%.</param>
        /// <param name="token2">Token %token2%.</param>
        /// <param name="token3">Token %token3%.</param>
        /// <param name="template">The template name.</param>
        /// <param name="type">Verification type (Sms / Call).</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template, VerifyLookupType type);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string template, VerifyLookupType type);
#endif

        /// <summary>
        /// Sends an OTP verification token with four parameters and specific transmission type.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token %token%.</param>
        /// <param name="token2">Token %token2%.</param>
        /// <param name="token3">Token %token3%.</param>
        /// <param name="token10">Token %token10%.</param>
        /// <param name="template">The template name.</param>
        /// <param name="type">Verification type.</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string template, VerifyLookupType type);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string template, VerifyLookupType type);
#endif

        /// <summary>
        /// Sends an OTP verification token with full custom options and routing.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token %token%.</param>
        /// <param name="token2">Token %token2%.</param>
        /// <param name="token3">Token %token3%.</param>
        /// <param name="token10">Token %token10%.</param>
        /// <param name="token20">Token %token20%.</param>
        /// <param name="template">The template name.</param>
        /// <param name="type">Verification type (Sms / Call).</param>
        /// <param name="tag">Custom categorisation tag.</param>
        /// <param name="primaryCausal">Primary routing channel priority.</param>
        /// <param name="backupCausal">Fallback routing channel priority.</param>
        /// <returns>A send result entry.</returns>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type, string tag = null);
#if !NET35
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type, string tag = null);
#endif

        /// <summary>
        /// Places an immediate text-to-speech voice call to a single recipient.
        /// </summary>
        /// <param name="message">The text to read aloud.</param>
        /// <param name="receptor">The target phone number.</param>
        /// <returns>A call result entry.</returns>
        SendResult CallMakeTTS(string message, string receptor);
#if !NET35
        System.Threading.Tasks.Task<SendResult> CallMakeTTSAsync(string message, string receptor);
#endif

        /// <summary>
        /// Places an immediate text-to-speech voice call to multiple recipients.
        /// </summary>
        /// <param name="message">The text to read aloud.</param>
        /// <param name="receptor">A list of recipient numbers.</param>
        /// <returns>A list of call result entries.</returns>
        List<SendResult> CallMakeTTS(string message, List<string> receptor);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor);
#endif

        /// <summary>
        /// Places a text-to-speech voice call with scheduling and routing configurations.
        /// </summary>
        /// <param name="message">The text to read.</param>
        /// <param name="receptor">A list of recipients.</param>
        /// <param name="date">Scheduled date and time.</param>
        /// <param name="localid">A list of client local IDs.</param>
        /// <param name="sender">The caller id/sender number.</param>
        /// <param name="tag">A category tag.</param>
        /// <param name="causal">Call dispatch channel routing priority.</param>
        /// <param name="policy">Call logic policy.</param>
        /// <param name="mediaId">Audio file media attachment GUID.</param>
        /// <returns>A list of call result entries.</returns>
        List<SendResult> CallMakeTTS(string message, List<string> receptor, DateTime? date, List<string> localid, string sender = null, string tag = null, string policy = null, Guid? mediaId = null);
#if !NET35
        System.Threading.Tasks.Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor, DateTime? date, List<string> localid, string sender = null, string tag = null, string policy = null, Guid? mediaId = null);
#endif

        /// <summary>
        /// Retrieves message statuses sent to a specific recipient mobile number.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="startDate">Query window start date.</param>
        /// <param name="endDate">Query window end date.</param>
        /// <returns>A list of delivery status entries.</returns>
        List<StatusResult> StatusByReceptor(string receptor, DateTime? startDate = null, DateTime? endDate = null);
#if !NET35
        System.Threading.Tasks.Task<List<StatusResult>> StatusByReceptorAsync(string receptor, DateTime? startDate = null, DateTime? endDate = null);
#endif

        /// <summary>
        /// Simulates or posts a received message transaction for debugging and testing.
        /// </summary>
        /// <param name="lineNumber">Recipient line number.</param>
        /// <param name="sender">Sender phone number.</param>
        /// <param name="messageId">External message ID.</param>
        /// <param name="message">Message text content.</param>
        /// <param name="isRead">0 for unread, 1 for read.</param>
        /// <returns>A list of simulated inbox logs.</returns>
        List<ReceiveResult> MakeReceive(string lineNumber, string sender, string messageId, string message, int isRead);
#if !NET35
        System.Threading.Tasks.Task<List<ReceiveResult>> MakeReceiveAsync(string lineNumber, string sender, string messageId, string message, int isRead);
#endif

        /// <summary>
        /// Retrieves unread messages from a specific line number.
        /// </summary>
        /// <param name="lineNumber">The incoming line number.</param>
        /// <param name="isRead">0 for unread, 1 for read.</param>
        /// <returns>A list of inbox entries.</returns>
        List<ReceiveResult> Unreads(string lineNumber, int isRead);
#if !NET35
        System.Threading.Tasks.Task<List<ReceiveResult>> UnreadsAsync(string lineNumber, int isRead);
#endif

        /// <summary>
        /// Retrieves inbox messages with filters, page numbers, and dates.
        /// </summary>
        /// <param name="lineNumber">Your line number.</param>
        /// <param name="line">Inbox search query.</param>
        /// <param name="isRead">Read state filter.</param>
        /// <param name="startDate">Filter window start.</param>
        /// <param name="endDate">Filter window end.</param>
        /// <param name="pageNumber">Page offset.</param>
        /// <returns>A paged inbox result detail block.</returns>
        InboxPagedResult InboxPaged(string lineNumber, string line, int? isRead = null, DateTime? startDate = null, DateTime? endDate = null, int? pageNumber = null);
#if !NET35
        System.Threading.Tasks.Task<InboxPagedResult> InboxPagedAsync(string lineNumber, string line, int? isRead = null, DateTime? startDate = null, DateTime? endDate = null, int? pageNumber = null);
#endif

        /// <summary>
        /// Generates aggregation reports for group message deliveries.
        /// </summary>
        /// <param name="startDate">Start report date.</param>
        /// <param name="endDate">End report date.</param>
        /// <returns>A list of delivery report details.</returns>
        List<GroupSendReportResult> GroupSendReport(DateTime startDate, DateTime endDate);
#if !NET35
        System.Threading.Tasks.Task<List<GroupSendReportResult>> GroupSendReportAsync(DateTime startDate, DateTime endDate);
#endif

        /// <summary>
        /// Retrieves paginated dispatch details for group broadcasts.
        /// </summary>
        /// <param name="partyId">Broadcast party group ID.</param>
        /// <param name="cursor">Offset cursor ID.</param>
        /// <param name="direction">Search paging direction.</param>
        /// <returns>Group broadcast logs.</returns>
        SelectGroupSendResult SelectGroupSend(int partyId, int cursor, int? direction = null);
#if !NET35
        System.Threading.Tasks.Task<SelectGroupSendResult> SelectGroupSendAsync(int partyId, int cursor, int? direction = null);
#endif

        /// <summary>
        /// Generates and provisions a new sub-client profile.
        /// </summary>
        /// <param name="dto">The sub-client details.</param>
        /// <returns>The created client record containing their API key.</returns>
        SubClientResult AddClient(SubClientDto dto);
#if !NET35
        System.Threading.Tasks.Task<SubClientResult> AddClientAsync(SubClientDto dto);
#endif

        /// <summary>
        /// Updates the profile settings of an existing sub-client.
        /// </summary>
        /// <param name="dto">The sub-client update fields.</param>
        /// <returns>The updated client record.</returns>
        SubClientResult UpdateClient(SubClientDto dto);
#if !NET35
        System.Threading.Tasks.Task<SubClientResult> UpdateClientAsync(SubClientDto dto);
#endif

        /// <summary>
        /// Lists all sub-clients registered under this master account.
        /// </summary>
        /// <returns>A list of client records.</returns>
        List<SubClientResult> ListClients();
#if !NET35
        System.Threading.Tasks.Task<List<SubClientResult>> ListClientsAsync();
#endif

        /// <summary>
        /// Retrieves the profile details of a sub-client using their API key.
        /// </summary>
        /// <param name="apiKey">Sub-client key.</param>
        /// <returns>The client profile.</returns>
        SubClientResult FetchClient(string apiKey);
#if !NET35
        System.Threading.Tasks.Task<SubClientResult> FetchClientAsync(string apiKey);
#endif

        /// <summary>
        /// Retrieves the profile details of a sub-client using their local identifier.
        /// </summary>
        /// <param name="localId">Sub-client local client ID.</param>
        /// <returns>The client profile.</returns>
        SubClientResult FetchClientByLocalId(string localId);
#if !NET35
        System.Threading.Tasks.Task<SubClientResult> FetchClientByLocalIdAsync(string localId);
#endif

        /// <summary>
        /// Regenerates a sub-client API authentication key.
        /// </summary>
        /// <param name="apiKey">Sub-client API key.</param>
        /// <param name="localId">Sub-client local ID.</param>
        /// <returns>The client record with the new API key.</returns>
        SubClientResult RenewClientKey(string apiKey = null, string localId = null);
#if !NET35
        System.Threading.Tasks.Task<SubClientResult> RenewClientKeyAsync(string apiKey = null, string localId = null);
#endif

        /// <summary>
        /// Toggles client operational status (active/suspended).
        /// </summary>
        /// <param name="apiKey">Sub-client API key.</param>
        /// <param name="status">1 for active, 2 for suspended.</param>
        /// <returns>The client status details.</returns>
        SubClientResult SetClientStatus(string apiKey, int status);
#if !NET35
        System.Threading.Tasks.Task<SubClientResult> SetClientStatusAsync(string apiKey, int status);
#endif

        /// <summary>
        /// Transfers credit balance points from the master account to the sub-client.
        /// </summary>
        /// <param name="apiKey">Sub-client API key.</param>
        /// <param name="credit">Number of points to transfer.</param>
        /// <param name="desc">Transfer notes/descriptions.</param>
        /// <param name="vat">Value added tax configuration.</param>
        /// <param name="ip">Originating request IP address.</param>
        /// <returns>Transaction transfer details.</returns>
        SubClientResult ChargeClientCredit(string apiKey, long credit, string desc = null, int? vat = null, string ip = null);
#if !NET35
        System.Threading.Tasks.Task<SubClientResult> ChargeClientCreditAsync(string apiKey, long credit, string desc = null, int? vat = null, string ip = null);
#endif

        /// <summary>
        /// Blocks a recipient from receiving messages from your sender line.
        /// </summary>
        /// <param name="receptor">Target phone number.</param>
        /// <param name="lineNumber">Your line number.</param>
        /// <returns>Blacklist details block.</returns>
        List<BlacklistResult> AddBlockedLine(string receptor, string lineNumber);
#if !NET35
        System.Threading.Tasks.Task<List<BlacklistResult>> AddBlockedLineAsync(string receptor, string lineNumber);
#endif

        /// <summary>
        /// Unblocks a recipient, allowing them to receive messages from your line.
        /// </summary>
        /// <param name="receptor">Target phone number.</param>
        /// <param name="lineNumber">Your line number.</param>
        /// <returns>Unblock details block.</returns>
        RemoveBlacklistResult RemoveBlockedLine(string receptor, string lineNumber);
#if !NET35
        System.Threading.Tasks.Task<RemoveBlacklistResult> RemoveBlockedLineAsync(string receptor, string lineNumber);
#endif

        /// <summary>
        /// Lists all blocked numbers associated with a line.
        /// </summary>
        /// <param name="lineNumber">Your line number.</param>
        /// <param name="pageNumber">Page offset.</param>
        /// <param name="startDate">Filter block window start date (yyyy-MM-dd).</param>
        /// <param name="blockReason">Optional reason filter.</param>
        /// <returns>Blocked lines query page.</returns>
        LineBlockListResult ListBlockedLines(string lineNumber, int pageNumber, long startDate, byte? blockReason = null);
#if !NET35
        System.Threading.Tasks.Task<LineBlockListResult> ListBlockedLinesAsync(string lineNumber, int pageNumber, long startDate, byte? blockReason = null);
#endif

        /// <summary>
        /// Checks if a phone number is currently blacklisted on a line.
        /// </summary>
        /// <param name="lineNumber">Your line number.</param>
        /// <param name="receptor">Target phone number.</param>
        /// <returns>Blacklist status check details.</returns>
        List<BlacklistResult> BlockedLineExists(string lineNumber, string receptor);
#if !NET35
        System.Threading.Tasks.Task<List<BlacklistResult>> BlockedLineExistsAsync(string lineNumber, string receptor);
#endif

        /// <summary>
        /// Lists approved OTP message templates on the account.
        /// </summary>
        /// <param name="apiKey">Optional sub-client filter key.</param>
        /// <param name="localId">Optional local client filter ID.</param>
        /// <param name="page">Page offset.</param>
        /// <returns>List of template configurations.</returns>
        List<TemplateResult> ListTemplates(string apiKey = null, string localId = null, int? page = null);
#if !NET35
        System.Threading.Tasks.Task<List<TemplateResult>> ListTemplatesAsync(string apiKey = null, string localId = null, int? page = null);
#endif

        /// <summary>
        /// Clones an approved template to draft a new pattern.
        /// </summary>
        /// <param name="sourceTemplateId">Source template ID.</param>
        /// <param name="sourceTemplateName">Source template name.</param>
        /// <param name="newTemplateName">New template duplicate name.</param>
        /// <param name="apiKey">Sub-client key.</param>
        /// <param name="localId">Sub-client local ID.</param>
        /// <returns>Cloning result metadata.</returns>
        CloneTemplateResult CloneTemplate(int? sourceTemplateId = null, string sourceTemplateName = null, string newTemplateName = null, string apiKey = null, string localId = null);
#if !NET35
        System.Threading.Tasks.Task<CloneTemplateResult> CloneTemplateAsync(int? sourceTemplateId = null, string sourceTemplateName = null, string newTemplateName = null, string apiKey = null, string localId = null);
#endif

        /// <summary>
        /// Submits a request to register a new OTP template.
        /// </summary>
        /// <param name="dto">Template details.</param>
        /// <returns>Registration metadata status.</returns>
        TemplateInfoResult AddTemplate(TemplateDto dto);
#if !NET35
        System.Threading.Tasks.Task<TemplateInfoResult> AddTemplateAsync(TemplateDto dto);
#endif

        /// <summary>
        /// Submits modifications for an existing template.
        /// </summary>
        /// <param name="templateId">Template ID.</param>
        /// <param name="dto">New template details.</param>
        /// <returns>Update status result.</returns>
        TemplateInfoResult UpdateTemplate(int templateId, TemplateDto dto);
#if !NET35
        System.Threading.Tasks.Task<TemplateInfoResult> UpdateTemplateAsync(int templateId, TemplateDto dto);
#endif

        /// <summary>
        /// Retrieves the configuration structure of a specific template.
        /// </summary>
        /// <param name="id">Template ID.</param>
        /// <param name="apiKey">Optional sub-client filter.</param>
        /// <param name="localId">Optional local client filter.</param>
        /// <returns>Template configuration details.</returns>
        TemplateResult GetTemplate(int id, string apiKey = null, string localId = null);
#if !NET35
        System.Threading.Tasks.Task<TemplateResult> GetTemplateAsync(int id, string apiKey = null, string localId = null);
#endif

        /// <summary>
        /// Deletes an approved template from the account.
        /// </summary>
        /// <param name="id">Template ID.</param>
        /// <param name="apiKey">Optional sub-client filter.</param>
        /// <param name="localId">Optional local client filter.</param>
        /// <returns>Deletion confirmation message status.</returns>
        string DeleteTemplate(int id, string apiKey = null, string localId = null);
#if !NET35
        System.Threading.Tasks.Task<string> DeleteTemplateAsync(int id, string apiKey = null, string localId = null);
#endif

        /// <summary>
        /// Uploads a media audio/image file for voice attachments from a local file path.
        /// </summary>
        /// <param name="filePath">Absolute local system path to the file.</param>
        /// <returns>Upload status details.</returns>
        MediaResult UploadMedia(string filePath);
#if !NET35
        System.Threading.Tasks.Task<MediaResult> UploadMediaAsync(string filePath);
#endif

        /// <summary>
        /// Uploads a media audio/image file directly from raw byte data.
        /// </summary>
        /// <param name="name">Filename.</param>
        /// <param name="fileBytes">Binary array content.</param>
        /// <returns>Upload status details containing media GUID ID.</returns>
        MediaResult UploadMedia(string name, byte[] fileBytes);
#if !NET35
        System.Threading.Tasks.Task<MediaResult> UploadMediaAsync(string name, byte[] fileBytes);
#endif

        /// <summary>
        /// Lists uploaded media files available on the account.
        /// </summary>
        /// <param name="page">Page offset.</param>
        /// <param name="size">Page size.</param>
        /// <returns>Paged media records list.</returns>
        MediaListResult ListMedia(int? page = null, int? size = null);
#if !NET35
        System.Threading.Tasks.Task<MediaListResult> ListMediaAsync(int? page = null, int? size = null);
#endif

        /// <summary>
        /// Retrieves configurations and attachment sections for a media file.
        /// </summary>
        /// <param name="id">Media file GUID.</param>
        /// <param name="fileName">Alternative lookup filename.</param>
        /// <param name="section">Attachment section (Voice / Message / etc).</param>
        /// <returns>Media metadata properties.</returns>
        MediaResult GetMedia(Guid? id = null, string fileName = null, AttachFileSection? section = null);
#if !NET35
        System.Threading.Tasks.Task<MediaResult> GetMediaAsync(Guid? id = null, string fileName = null, AttachFileSection? section = null);
#endif

        /// <summary>
        /// Deletes a media file from storage.
        /// </summary>
        /// <param name="id">Media file GUID.</param>
        /// <returns>Deletion status details.</returns>
        MediaDeleteResult DeleteMedia(Guid id);
#if !NET35
        System.Threading.Tasks.Task<MediaDeleteResult> DeleteMediaAsync(Guid id);
#endif

        /// <summary>
        /// Inserts a new contact to a phonebook group.
        /// </summary>
        /// <param name="groupId">Phonebook group ID.</param>
        /// <param name="number">Mobile phone number.</param>
        /// <param name="title">Contact display name/title.</param>
        /// <param name="birthdate">Optional birthday.</param>
        /// <param name="email">Optional email address.</param>
        /// <param name="tags">Custom tags.</param>
        /// <returns>The added contact record.</returns>
        List<ContactResult> AddContact(int groupId, string number, string title = null, string birthdate = null, string email = null, string tags = null);
#if !NET35
        System.Threading.Tasks.Task<List<ContactResult>> AddContactAsync(int groupId, string number, string title = null, string birthdate = null, string email = null, string tags = null);
#endif

        /// <summary>
        /// Creates a new contact category group in the phonebook.
        /// </summary>
        /// <param name="name">Group label.</param>
        /// <param name="tag">Custom categorization tag.</param>
        /// <param name="parent">Optional parent folder group ID.</param>
        /// <returns>The created phonebook group metadata.</returns>
        List<GroupResult> AddGroup(string name, string tag = null, int? parent = null);
#if !NET35
        System.Threading.Tasks.Task<List<GroupResult>> AddGroupAsync(string name, string tag = null, int? parent = null);
#endif

        /// <summary>
        /// Removes a contact from a group or deletes it entirely.
        /// </summary>
        /// <param name="contactId">Contact ID.</param>
        /// <param name="mobile">Phone number.</param>
        /// <param name="groupId">Filter group ID.</param>
        /// <returns>Operation confirmation details.</returns>
        List<ContactResult> RemoveContact(string contactId = null, string mobile = null, int? groupId = null);
#if !NET35
        System.Threading.Tasks.Task<List<ContactResult>> RemoveContactAsync(string contactId = null, string mobile = null, int? groupId = null);
#endif

        /// <summary>
        /// Deletes a contact category group.
        /// </summary>
        /// <param name="groupId">Group ID.</param>
        /// <returns>Confirmation details.</returns>
        List<GroupResult> RemoveGroup(int groupId);
#if !NET35
        System.Threading.Tasks.Task<List<GroupResult>> RemoveGroupAsync(int groupId);
#endif

        /// <summary>
        /// Lists contact groups in the phonebook.
        /// </summary>
        /// <param name="parentId">Optional parent folder filter ID.</param>
        /// <returns>A list of phonebook group records.</returns>
        List<GroupResult> ListGroups(int? parentId = null);
#if !NET35
        System.Threading.Tasks.Task<List<GroupResult>> ListGroupsAsync(int? parentId = null);
#endif

        /// <summary>
        /// Modifies configuration settings of a phonebook group.
        /// </summary>
        /// <param name="groupId">Group ID.</param>
        /// <param name="groupName">New group name.</param>
        /// <param name="status">Group status.</param>
        /// <param name="tag">Category tags.</param>
        /// <returns>The updated group record.</returns>
        List<GroupResult> EditGroup(int groupId, string groupName = null, byte? status = null, string tag = null);
#if !NET35
        System.Threading.Tasks.Task<List<GroupResult>> EditGroupAsync(int groupId, string groupName = null, byte? status = null, string tag = null);
#endif

        /// <summary>
        /// Searches contact groups matching a tag query.
        /// </summary>
        /// <param name="tag">Category tag.</param>
        /// <returns>List of matching groups.</returns>
        List<GroupResult> SearchGroups(string tag);
#if !NET35
        System.Threading.Tasks.Task<List<GroupResult>> SearchGroupsAsync(string tag);
#endif

        /// <summary>
        /// Retrieves the current system date and time from Kavenegar servers.
        /// </summary>
        /// <returns>Server date result.</returns>
        ServerDateResult GetServerDate();
#if !NET35
        System.Threading.Tasks.Task<ServerDateResult> GetServerDateAsync();
#endif

        /// <summary>
        /// Tests network connection and credentials validation.
        /// </summary>
        /// <returns>Always returns string "pong" on success.</returns>
        string Ping();
#if !NET35
        System.Threading.Tasks.Task<string> PingAsync();
#endif
    }
}

