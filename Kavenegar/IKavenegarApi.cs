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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> Send(string sender, List<string> receptor, string message);
#if !NET35
        /// <summary>
        /// Sends a simple SMS message to a list of recipients.
        /// </summary>
        /// <param name="sender">The pre-registered Kavenegar sender line number.</param>
        /// <param name="receptor">A list of recipient mobile numbers.</param>
        /// <param name="message">The text body of the message.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries containing message IDs and cost.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message);
#endif

        /// <summary>
        /// Sends a simple SMS message to a single recipient.
        /// </summary>
        /// <param name="sender">The pre-registered Kavenegar sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <returns>A send result entry containing message ID, cost, and delivery status.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult Send(string sender, String receptor, string message);
#if !NET35
        /// <summary>
        /// Sends a simple SMS message to a single recipient.
        /// </summary>
        /// <param name="sender">The pre-registered Kavenegar sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <returns>A task representing the asynchronous operation. A send result entry containing message ID, cost, and delivery status.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date);
#if !NET35
        /// <summary>
        /// Sends an SMS message to a single recipient with a specific message type and scheduled date.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="type">The storage or transmission type of the message.</param>
        /// <param name="date">The scheduled dispatch date and time (use DateTime.MinValue for immediate delivery).</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SendResult> SendAsync(string sender, string receptor, string message, DateTime date);
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date);
#if !NET35
        /// <summary>
        /// Sends an SMS message to multiple recipients with a specific message type and scheduled date.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">A list of recipient mobile numbers.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="type">The storage or transmission type of the message.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, DateTime date);
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date, string localid);
#if !NET35
        /// <summary>
        /// Sends an SMS message to a single recipient with a custom local ID for duplicate protection.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="type">The storage or transmission type of the message.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <param name="localid">A unique local client ID to prevent double dispatch.</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SendResult> SendAsync(string sender, string receptor, string message, DateTime date, string localid);
#endif

        /// <summary>
        /// Sends an SMS message to a single recipient with a custom local ID.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="localid">A unique local client ID.</param>
        /// <returns>A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult Send(string sender, string receptor, string message, string localid);
#if !NET35
        /// <summary>
        /// Sends an SMS message to a single recipient with a custom local ID.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">The recipient mobile number.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="localid">A unique local client ID.</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> Send(string sender, List<string> receptors, string message, string localid);
#if !NET35
        /// <summary>
        /// Sends an SMS message to multiple recipients with a custom local ID.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="localid">A unique local client ID.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <param name="tag">A custom category tag.</param>
        /// <param name="text">Additional raw text context.</param>
        /// <param name="hide">Flag to hide message contents in logs.</param>
        /// <param name="localMessageId">A local client message identifier.</param>
        /// <param name="policy">Spam/dispatch filtration policy.</param>
        /// <param name="mediaId">MMS attachment media GUID.</param>
        /// <returns>A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date, string tag = null, string text = null, string hide = null, string localMessageId = null, string policy = null, Guid? mediaId = null);
#if !NET35
        /// <summary>
        /// Sends an SMS message to multiple recipients with the full suite of advanced optional configurations.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptor">A list of recipient mobile numbers.</param>
        /// <param name="message">The text body of the message.</param>
        /// <param name="type">The storage or transmission type of the message.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <param name="tag">A custom category tag.</param>
        /// <param name="text">Additional raw text context.</param>
        /// <param name="hide">Flag to hide message contents in logs.</param>
        /// <param name="localMessageId">A local client message identifier.</param>
        /// <param name="policy">Spam/dispatch filtration policy.</param>
        /// <param name="mediaId">MMS attachment media GUID.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SendAsync(string sender, List<string> receptor, string message, DateTime date, string tag = null, string text = null, string hide = null, string localMessageId = null, string policy = null, Guid? mediaId = null);
#endif

        /// <summary>
        /// Sends a bulk array of different messages to different recipients from different lines.
        /// </summary>
        /// <param name="senders">A list of sender line numbers matching the receptors.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies matching the receptors.</param>
        /// <returns>A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages);
#if !NET35
        /// <summary>
        /// Sends a bulk array of different messages to different recipients from different lines.
        /// </summary>
        /// <param name="senders">A list of sender line numbers matching the receptors.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies matching the receptors.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date);
#if !NET35
        /// <summary>
        /// Sends a bulk array of messages using a single sender line with custom type and dispatch time.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies.</param>
        /// <param name="type">The transmission type.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, DateTime date);
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date, string localmessageids);
#if !NET35
        /// <summary>
        /// Sends a bulk array of messages using a single sender line with custom type, dispatch time, and local IDs.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies.</param>
        /// <param name="type">The transmission type.</param>
        /// <param name="date">The scheduled dispatch date and time.</param>
        /// <param name="localmessageids">Comma-separated local message IDs.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(string sender, List<string> receptors, List<string> messages, DateTime date, string localmessageids);
#endif

        /// <summary>
        /// Sends a bulk array of messages using a single sender line with local message IDs.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message text bodies.</param>
        /// <param name="localmessageid">A unique local client ID.</param>
        /// <returns>A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, string localmessageid);
#if !NET35
        /// <summary>
        /// Sends a bulk array of messages using a single sender line with local message IDs.
        /// </summary>
        /// <param name="sender">The sender line number.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message text bodies.</param>
        /// <param name="localmessageid">A unique local client ID.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages, string localmessageid);
#if !NET35
        /// <summary>
        /// Sends a bulk array of messages matching multiple senders, receptors, and message texts with a local identifier.
        /// </summary>
        /// <param name="senders">A list of sender lines.</param>
        /// <param name="receptors">A list of recipient mobile numbers.</param>
        /// <param name="messages">A list of message bodies.</param>
        /// <param name="localmessageid">A unique local client ID.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <param name="hide">Flag to hide message content in logs.</param>
        /// <param name="policy">Filtering policies.</param>
        /// <param name="mediaId">MMS attachment GUID.</param>
        /// <returns>A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages, List<MessageType> types, DateTime date, List<string> localmessageids, string tag = null, string hide = null, string policy = null, Guid? mediaId = null);
#if !NET35
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
        /// <param name="hide">Flag to hide message content in logs.</param>
        /// <param name="policy">Filtering policies.</param>
        /// <param name="mediaId">MMS attachment GUID.</param>
        /// <returns>A task representing the asynchronous operation. A list of send result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>405</b>: Invalid HTTP method (POST required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>419</b>: Array length mismatch (senders, receptors, and messages size mismatch)</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SendArrayAsync(List<string> senders, List<string> receptors, List<string> messages, DateTime date, List<string> localmessageids, string tag = null, string hide = null, string policy = null, Guid? mediaId = null);
#endif

        /// <summary>
        /// Queries the delivery status of multiple messages using their Kavenegar message IDs.
        /// </summary>
        /// <param name="messageids">A list of message IDs.</param>
        /// <returns>A list of delivery status entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<StatusResult> Status(List<string> messageids);
#if !NET35
        /// <summary>
        /// Queries the delivery status of multiple messages using their Kavenegar message IDs.
        /// </summary>
        /// <param name="messageids">A list of message IDs.</param>
        /// <returns>A task representing the asynchronous operation. A list of delivery status entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<StatusResult>> StatusAsync(List<string> messageids);
#endif

        /// <summary>
        /// Queries the delivery status of a single message using its Kavenegar message ID.
        /// </summary>
        /// <param name="messageid">The message ID.</param>
        /// <returns>A delivery status entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        StatusResult Status(string messageid);
#if !NET35
        /// <summary>
        /// Queries the delivery status of a single message using its Kavenegar message ID.
        /// </summary>
        /// <param name="messageid">The message ID.</param>
        /// <returns>A task representing the asynchronous operation. A delivery status entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<StatusResult> StatusAsync(string messageid);
#endif

        /// <summary>
        /// Queries the delivery status of multiple messages using their local client identifiers.
        /// </summary>
        /// <param name="messageids">A list of local message IDs.</param>
        /// <returns>A list of status results.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<StatusLocalMessageIdResult> StatusLocalMessageId(List<string> messageids);
#if !NET35
        /// <summary>
        /// Queries the delivery status of multiple messages using their local client identifiers.
        /// </summary>
        /// <param name="messageids">A list of local message IDs.</param>
        /// <returns>A task representing the asynchronous operation. A list of status results.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<StatusLocalMessageIdResult>> StatusLocalMessageIdAsync(List<string> messageids);
#endif

        /// <summary>
        /// Queries the delivery status of a single message using its local client identifier.
        /// </summary>
        /// <param name="messageid">The local message ID.</param>
        /// <returns>A status result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        StatusLocalMessageIdResult StatusLocalMessageId(string messageid);
#if !NET35
        /// <summary>
        /// Queries the delivery status of a single message using its local client identifier.
        /// </summary>
        /// <param name="messageid">The local message ID.</param>
        /// <returns>A task representing the asynchronous operation. A status result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<StatusLocalMessageIdResult> StatusLocalMessageIdAsync(string messageid);
#endif

        /// <summary>
        /// Retrieves dispatch metadata for a list of messages.
        /// </summary>
        /// <param name="messageids">A list of message IDs.</param>
        /// <returns>A list of message log entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> Select(List<string> messageids);
#if !NET35
        /// <summary>
        /// Retrieves dispatch metadata for a list of messages.
        /// </summary>
        /// <param name="messageids">A list of message IDs.</param>
        /// <returns>A task representing the asynchronous operation. A list of message log entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SelectAsync(List<string> messageids);
#endif

        /// <summary>
        /// Retrieves dispatch metadata for a single message.
        /// </summary>
        /// <param name="messageid">The message ID.</param>
        /// <returns>A message log entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult Select(string messageid);
#if !NET35
        /// <summary>
        /// Retrieves dispatch metadata for a single message.
        /// </summary>
        /// <param name="messageid">The message ID.</param>
        /// <returns>A task representing the asynchronous operation. A message log entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SendResult> SelectAsync(string messageid);
#endif

        /// <summary>
        /// Retrieves outgoing outbox logs starting from a specific date.
        /// </summary>
        /// <param name="startdate">The query start date and time.</param>
        /// <returns>A list of outgoing message logs.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SelectOutbox(DateTime startdate);
#if !NET35
        /// <summary>
        /// Retrieves outgoing outbox logs starting from a specific date.
        /// </summary>
        /// <param name="startdate">The query start date and time.</param>
        /// <returns>A task representing the asynchronous operation. A list of outgoing message logs.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SelectOutboxAsync(DateTime startdate);
#endif

        /// <summary>
        /// Retrieves outgoing outbox logs within a specific date range.
        /// </summary>
        /// <param name="startdate">The query start date.</param>
        /// <param name="enddate">The query end date.</param>
        /// <returns>A list of outgoing message logs.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate);
#if !NET35
        /// <summary>
        /// Retrieves outgoing outbox logs within a specific date range.
        /// </summary>
        /// <param name="startdate">The query start date.</param>
        /// <param name="enddate">The query end date.</param>
        /// <returns>A task representing the asynchronous operation. A list of outgoing message logs.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate);
#endif

        /// <summary>
        /// Retrieves outgoing outbox logs within a date range sent from a specific sender line.
        /// </summary>
        /// <param name="startdate">The query start date.</param>
        /// <param name="enddate">The query end date.</param>
        /// <param name="sender">The sender line number.</param>
        /// <returns>A list of outgoing message logs.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate, String sender);
#if !NET35
        /// <summary>
        /// Retrieves outgoing outbox logs within a date range sent from a specific sender line.
        /// </summary>
        /// <param name="startdate">The query start date.</param>
        /// <param name="enddate">The query end date.</param>
        /// <param name="sender">The sender line number.</param>
        /// <returns>A task representing the asynchronous operation. A list of outgoing message logs.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> SelectOutboxAsync(DateTime startdate, DateTime enddate, String sender);
#endif

        /// <summary>
        /// Retrieves the most recent outgoing outbox logs.
        /// </summary>
        /// <param name="pagesize">The page size / maximum number of records to retrieve.</param>
        /// <returns>A list of outbox log entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> LatestOutbox(long pagesize);
#if !NET35
        /// <summary>
        /// Retrieves the most recent outgoing outbox logs.
        /// </summary>
        /// <param name="pagesize">The page size / maximum number of records to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. A list of outbox log entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> LatestOutboxAsync(long pagesize);
#endif

        /// <summary>
        /// Retrieves the most recent outgoing outbox logs filtered by a sender line.
        /// </summary>
        /// <param name="pagesize">The maximum number of records to retrieve.</param>
        /// <param name="sender">The sender line number.</param>
        /// <returns>A list of outbox log entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> LatestOutbox(long pagesize, String sender);
#if !NET35
        /// <summary>
        /// Retrieves the most recent outgoing outbox logs filtered by a sender line.
        /// </summary>
        /// <param name="pagesize">The maximum number of records to retrieve.</param>
        /// <param name="sender">The sender line number.</param>
        /// <returns>A task representing the asynchronous operation. A list of outbox log entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> LatestOutboxAsync(long pagesize, String sender);
#endif

        /// <summary>
        /// Counts outgoing outbox messages dispatched after a start date.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <returns>A count details entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        CountOutboxResult CountOutbox(DateTime startdate);
#if !NET35
        /// <summary>
        /// Counts outgoing outbox messages dispatched after a start date.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <returns>A task representing the asynchronous operation. A count details entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<CountOutboxResult> CountOutboxAsync(DateTime startdate);
#endif

        /// <summary>
        /// Counts outgoing outbox messages within a date range.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <returns>A count details entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate);
#if !NET35
        /// <summary>
        /// Counts outgoing outbox messages within a date range.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <returns>A task representing the asynchronous operation. A count details entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate);
#endif

        /// <summary>
        /// Counts outgoing outbox messages within a date range filtered by delivery status.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="status">The delivery status number.</param>
        /// <returns>A count details entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate, int status);
#if !NET35
        /// <summary>
        /// Counts outgoing outbox messages within a date range filtered by delivery status.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="status">The delivery status number.</param>
        /// <returns>A task representing the asynchronous operation. A count details entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<CountOutboxResult> CountOutboxAsync(DateTime startdate, DateTime enddate, int status);
#endif

        /// <summary>
        /// Cancels scheduled delivery for multiple messages before transmission.
        /// </summary>
        /// <param name="messageids">A list of scheduled message IDs.</param>
        /// <returns>A list of cancel status results.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<StatusResult> Cancel(List<String> messageids);
#if !NET35
        /// <summary>
        /// Cancels scheduled delivery for multiple messages before transmission.
        /// </summary>
        /// <param name="messageids">A list of scheduled message IDs.</param>
        /// <returns>A task representing the asynchronous operation. A list of cancel status results.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<StatusResult>> CancelAsync(List<String> messageids);
#endif

        /// <summary>
        /// Cancels scheduled delivery for a single message.
        /// </summary>
        /// <param name="messageid">The scheduled message ID.</param>
        /// <returns>A cancel status result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        StatusResult Cancel(String messageid);
#if !NET35
        /// <summary>
        /// Cancels scheduled delivery for a single message.
        /// </summary>
        /// <param name="messageid">The scheduled message ID.</param>
        /// <returns>A task representing the asynchronous operation. A cancel status result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>412</b>: Dispatch canceled or sender line unauthorized / duplicate</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<StatusResult> CancelAsync(String messageid);
#endif

        /// <summary>
        /// Retrieves incoming received messages from a line.
        /// </summary>
        /// <param name="line">Your incoming line number.</param>
        /// <param name="isread">0 for unread, 1 for read.</param>
        /// <returns>A list of inbox received messages.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<ReceiveResult> Receive(string line, int isread);
#if !NET35
        /// <summary>
        /// Retrieves incoming received messages from a line.
        /// </summary>
        /// <param name="line">Your incoming line number.</param>
        /// <param name="isread">0 for unread, 1 for read.</param>
        /// <returns>A task representing the asynchronous operation. A list of inbox received messages.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<ReceiveResult>> ReceiveAsync(string line, int isread);
#endif

        /// <summary>
        /// Counts incoming received messages after a start date.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="linenumber">Your line number.</param>
        /// <returns>An inbox count result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        CountInboxResult CountInbox(DateTime startdate, string linenumber);
#if !NET35
        /// <summary>
        /// Counts incoming received messages after a start date.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="linenumber">Your line number.</param>
        /// <returns>A task representing the asynchronous operation. An inbox count result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<CountInboxResult> CountInboxAsync(DateTime startdate, string linenumber);
#endif

        /// <summary>
        /// Counts incoming received messages within a date range.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="linenumber">Your line number.</param>
        /// <returns>An inbox count result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber);
#if !NET35
        /// <summary>
        /// Counts incoming received messages within a date range.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="linenumber">Your line number.</param>
        /// <returns>A task representing the asynchronous operation. An inbox count result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber, int isread);
#if !NET35
        /// <summary>
        /// Counts incoming received messages within a date range filtered by read state.
        /// </summary>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="linenumber">Your line number.</param>
        /// <param name="isread">0 for unread, 1 for read.</param>
        /// <returns>A task representing the asynchronous operation. An inbox count result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<CountInboxResult> CountInboxAsync(DateTime startdate, DateTime enddate, String linenumber, int isread);
#endif

        /// <summary>
        /// Retrieves account profile details, balance limits, and status.
        /// </summary>
        /// <returns>Account information details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        AccountInfoResult AccountInfo();
#if !NET35
        /// <summary>
        /// Retrieves account profile details, balance limits, and status.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Account information details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        AccountConfigResult AccountConfig(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed);
#if !NET35
        /// <summary>
        /// Configures operational behaviors, logging, alerts, and defaults on the account level.
        /// </summary>
        /// <param name="apilogs">Enable or disable API logging ("enabled" / "disabled").</param>
        /// <param name="dailyreport">Enable or disable daily metrics email.</param>
        /// <param name="debugmode">Enable or disable sandbox simulation mode.</param>
        /// <param name="defaultsender">Assign default sender line number.</param>
        /// <param name="mincreditalarm">Minimum credit threshold to trigger SMS notifications.</param>
        /// <param name="resendfailed">Enable or disable automatic fallback resends.</param>
        /// <returns>A task representing the asynchronous operation. Account config status details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>407</b>: Access denied (IP restriction in security settings required)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<AccountConfigResult> AccountConfigAsync(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed);
#endif

        /// <summary>
        /// Sends an immediate token-based OTP (One-Time Password) bypassing standard delivery filters.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">The verification code.</param>
        /// <param name="template">The pre-approved template name.</param>
        /// <returns>A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string template);
#if !NET35
        /// <summary>
        /// Sends an immediate token-based OTP (One-Time Password) bypassing standard delivery filters.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">The verification code.</param>
        /// <param name="template">The pre-approved template name.</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string template, VerifyLookupType type);
#if !NET35
        /// <summary>
        /// Sends an OTP verification token specifying the delivery medium (SMS or Call).
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">The verification code.</param>
        /// <param name="template">The template name.</param>
        /// <param name="type">Delivery medium (e.g. Sms or Call).</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string token2, string template);
#if !NET35
        /// <summary>
        /// Sends an OTP verification token with secondary parameter (two tokens).
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token parameter %token%.</param>
        /// <param name="token2">Token parameter %token2%.</param>
        /// <param name="template">The template name.</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string token2, string template, VerifyLookupType type);
#if !NET35
        /// <summary>
        /// Sends an OTP verification token with secondary parameter (two tokens) and specific transmission type.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token parameter %token%.</param>
        /// <param name="token2">Token parameter %token2%.</param>
        /// <param name="template">The template name.</param>
        /// <param name="type">Delivery medium (e.g. Sms or Call).</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template);
#if !NET35
        /// <summary>
        /// Sends an OTP verification token with secondary parameters.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token parameter %token%.</param>
        /// <param name="token2">Token parameter %token2%.</param>
        /// <param name="token3">Token parameter %token3%.</param>
        /// <param name="template">The template name.</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string template);
#if !NET35
        /// <summary>
        /// Sends an OTP verification token with secondary and ternary parameters.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token %token%.</param>
        /// <param name="token2">Token %token2%.</param>
        /// <param name="token3">Token %token3%.</param>
        /// <param name="token10">Token %token10%.</param>
        /// <param name="template">The template name.</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template, VerifyLookupType type);
#if !NET35
        /// <summary>
        /// Sends an OTP verification token with parameters and specific transmission type.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="token">Token %token%.</param>
        /// <param name="token2">Token %token2%.</param>
        /// <param name="token3">Token %token3%.</param>
        /// <param name="template">The template name.</param>
        /// <param name="type">Verification type (Sms / Call).</param>
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string template, VerifyLookupType type);
#if !NET35
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
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <returns>A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type, string tag = null);
#if !NET35
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
        /// <returns>A task representing the asynchronous operation. A send result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>422</b>: Unprocessable data due to invalid characters</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// <para>• <b>426</b>: Advanced service authorization required</para>
        /// <para>• <b>428</b>: Voice call OTP not allowed for specified phone number</para>
        /// <para>• <b>431</b>: Token parameter count mismatch with template requirements</para>
        /// <para>• <b>432</b>: Token parameter missing in template pattern definition</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SendResult> VerifyLookupAsync(string receptor, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type, string tag = null);
#endif

        /// <summary>
        /// Places an immediate text-to-speech voice call to a single recipient.
        /// </summary>
        /// <param name="message">The text to read aloud.</param>
        /// <param name="receptor">The target phone number.</param>
        /// <returns>A call result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SendResult CallMakeTTS(string message, string receptor);
#if !NET35
        /// <summary>
        /// Places an immediate text-to-speech voice call to a single recipient.
        /// </summary>
        /// <param name="message">The text to read aloud.</param>
        /// <param name="receptor">The target phone number.</param>
        /// <returns>A task representing the asynchronous operation. A call result entry.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SendResult> CallMakeTTSAsync(string message, string receptor);
#endif

        /// <summary>
        /// Places an immediate text-to-speech voice call to multiple recipients.
        /// </summary>
        /// <param name="message">The text to read aloud.</param>
        /// <param name="receptor">A list of recipient numbers.</param>
        /// <returns>A list of call result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> CallMakeTTS(string message, List<string> receptor);
#if !NET35
        /// <summary>
        /// Places an immediate text-to-speech voice call to multiple recipients.
        /// </summary>
        /// <param name="message">The text to read aloud.</param>
        /// <param name="receptor">A list of recipient numbers.</param>
        /// <returns>A task representing the asynchronous operation. A list of call result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor);
#endif

        /// <summary>
        /// Places a text-to-speech voice call with scheduling and routing configurations.
        /// </summary>
        /// <param name="message">The text to read.</param>
        /// <param name="receptor">A list of recipients.</param>
        /// <param name="date">Scheduled date and time.</param>
        /// <param name="localid">A list of client local IDs.</param>
        /// <param name="tag">A category tag.</param>
        /// <returns>A list of call result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SendResult> CallMakeTTS(string message, List<string> receptor, DateTime? date, List<string> localid, string tag = null);
#if !NET35
        /// <summary>
        /// Places a text-to-speech voice call with scheduling and routing configurations.
        /// </summary>
        /// <param name="message">The text to read.</param>
        /// <param name="receptor">A list of recipients.</param>
        /// <param name="date">Scheduled date and time.</param>
        /// <param name="localid">A list of client local IDs.</param>
        /// <param name="tag">A category tag.</param>
        /// <returns>A task representing the asynchronous operation. A list of call result entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SendResult>> CallMakeTTSAsync(string message, List<string> receptor, DateTime? date, List<string> localid, string tag = null);
#endif

        /// <summary>
        /// Retrieves message statuses sent to a specific recipient mobile number.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="startDate">Query window start date.</param>
        /// <param name="endDate">Query window end date.</param>
        /// <returns>A list of delivery status entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<StatusResult> StatusByReceptor(string receptor, DateTime? startDate = null, DateTime? endDate = null);
#if !NET35
        /// <summary>
        /// Retrieves message statuses sent to a specific recipient mobile number.
        /// </summary>
        /// <param name="receptor">The target phone number.</param>
        /// <param name="startDate">Query window start date.</param>
        /// <param name="endDate">Query window end date.</param>
        /// <returns>A task representing the asynchronous operation. A list of delivery status entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<StatusResult>> StatusByReceptorAsync(string receptor, DateTime? startDate = null, DateTime? endDate = null);
#endif

        /// <summary>
        /// Retrieves unread messages from a specific line number.
        /// </summary>
        /// <param name="lineNumber">The incoming line number.</param>
        /// <param name="isRead">0 for unread, 1 for read.</param>
        /// <returns>A list of inbox entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<ReceiveResult> Unreads(string lineNumber, int isRead);
#if !NET35
        /// <summary>
        /// Retrieves unread messages from a specific line number.
        /// </summary>
        /// <param name="lineNumber">The incoming line number.</param>
        /// <param name="isRead">0 for unread, 1 for read.</param>
        /// <returns>A task representing the asynchronous operation. A list of inbox entries.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<ReceiveResult>> UnreadsAsync(string lineNumber, int isRead);
#endif

        /// <summary>
        /// Retrieves paginated records of incoming messages in the account inbox.
        /// </summary>
        /// <param name="lineNumber">Sender receiver line mapping identifier.</param>
        /// <param name="isRead">Read state filter.</param>
        /// <param name="startDate">Filter window start.</param>
        /// <param name="endDate">Filter window end.</param>
        /// <param name="pageNumber">Page offset.</param>
        /// <returns>A paged inbox result detail block.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        InboxPagedResult InboxPaged(string lineNumber, int? isRead = null, DateTime? startDate = null, DateTime? endDate = null, int? pageNumber = null);
#if !NET35
        /// <summary>
        /// Retrieves paginated records of incoming messages in the account inbox.
        /// </summary>
        /// <param name="lineNumber">Sender receiver line mapping identifier.</param>
        /// <param name="isRead">Read state filter.</param>
        /// <param name="startDate">Filter window start.</param>
        /// <param name="endDate">Filter window end.</param>
        /// <param name="pageNumber">Page offset.</param>
        /// <returns>A task representing the asynchronous operation. A paged inbox result detail block.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<InboxPagedResult> InboxPagedAsync(string lineNumber, int? isRead = null, DateTime? startDate = null, DateTime? endDate = null, int? pageNumber = null);
#endif

        /// <summary>
        /// Generates aggregation reports for group message deliveries.
        /// </summary>
        /// <param name="startDate">Start report date.</param>
        /// <param name="endDate">End report date.</param>
        /// <returns>A list of delivery report details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<GroupSendReportResult> GroupSendReport(DateTime startDate, DateTime endDate);
#if !NET35
        /// <summary>
        /// Generates aggregation reports for group message deliveries.
        /// </summary>
        /// <param name="startDate">Start report date.</param>
        /// <param name="endDate">End report date.</param>
        /// <returns>A task representing the asynchronous operation. A list of delivery report details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// <para>• <b>417</b>: Invalid or expired dispatch date (or start date > end date)</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// <para>• <b>607</b>: Selected tag name is invalid or unapproved</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<GroupSendReportResult>> GroupSendReportAsync(DateTime startDate, DateTime endDate);
#endif


        /// <summary>
        /// Generates and provisions a new sub-client profile.
        /// </summary>
        /// <param name="dto">The sub-client details.</param>
        /// <returns>The created client record containing their API key.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SubClientResult AddClient(SubClientDto dto);
#if !NET35
        /// <summary>
        /// Generates and provisions a new sub-client profile.
        /// </summary>
        /// <param name="dto">The sub-client details.</param>
        /// <returns>A task representing the asynchronous operation. The created client record containing their API key.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SubClientResult> AddClientAsync(SubClientDto dto);
#endif

        /// <summary>
        /// Updates the profile settings of an existing sub-client.
        /// </summary>
        /// <param name="dto">The sub-client update fields.</param>
        /// <returns>The updated client record.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SubClientResult UpdateClient(SubClientDto dto);
#if !NET35
        /// <summary>
        /// Updates the profile settings of an existing sub-client.
        /// </summary>
        /// <param name="dto">The sub-client update fields.</param>
        /// <returns>A task representing the asynchronous operation. The updated client record.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SubClientResult> UpdateClientAsync(SubClientDto dto);
#endif

        /// <summary>
        /// Lists all sub-clients registered under this master account.
        /// </summary>
        /// <returns>A list of client records.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<SubClientResult> ListClients();
#if !NET35
        /// <summary>
        /// Lists all sub-clients registered under this master account.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. A list of client records.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<SubClientResult>> ListClientsAsync();
#endif

        /// <summary>
        /// Retrieves the profile details of a sub-client using their API key.
        /// </summary>
        /// <param name="apiKey">Sub-client key.</param>
        /// <returns>The client profile.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SubClientResult FetchClient(string apiKey);
#if !NET35
        /// <summary>
        /// Retrieves the profile details of a sub-client using their API key.
        /// </summary>
        /// <param name="apiKey">Sub-client key.</param>
        /// <returns>A task representing the asynchronous operation. The client profile.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SubClientResult> FetchClientAsync(string apiKey);
#endif

        /// <summary>
        /// Retrieves the profile details of a sub-client using their local identifier.
        /// </summary>
        /// <param name="localId">Sub-client local client ID.</param>
        /// <returns>The client profile.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SubClientResult FetchClientByLocalId(string localId);
#if !NET35
        /// <summary>
        /// Retrieves the profile details of a sub-client using their local identifier.
        /// </summary>
        /// <param name="localId">Sub-client local client ID.</param>
        /// <returns>A task representing the asynchronous operation. The client profile.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SubClientResult> FetchClientByLocalIdAsync(string localId);
#endif

        /// <summary>
        /// Regenerates a sub-client API authentication key.
        /// </summary>
        /// <param name="apiKey">Sub-client API key.</param>
        /// <param name="localId">Sub-client local ID.</param>
        /// <returns>The client record with the new API key.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SubClientResult RenewClientKey(string apiKey = null, string localId = null);
#if !NET35
        /// <summary>
        /// Regenerates a sub-client API authentication key.
        /// </summary>
        /// <param name="apiKey">Sub-client API key.</param>
        /// <param name="localId">Sub-client local ID.</param>
        /// <returns>A task representing the asynchronous operation. The client record with the new API key.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SubClientResult> RenewClientKeyAsync(string apiKey = null, string localId = null);
#endif

        /// <summary>
        /// Toggles client operational status (active/suspended).
        /// </summary>
        /// <param name="apiKey">Sub-client API key.</param>
        /// <param name="status">1 for active, 2 for suspended.</param>
        /// <returns>The client status details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SubClientResult SetClientStatus(string apiKey, int status);
#if !NET35
        /// <summary>
        /// Toggles client operational status (active/suspended).
        /// </summary>
        /// <param name="apiKey">Sub-client API key.</param>
        /// <param name="status">1 for active, 2 for suspended.</param>
        /// <returns>A task representing the asynchronous operation. The client status details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>414</b>: Receptor count exceeds maximum limit (max 200)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        SubClientResult ChargeClientCredit(string apiKey, long credit, string desc = null, int? vat = null, string ip = null);
#if !NET35
        /// <summary>
        /// Transfers credit balance points from the master account to the sub-client.
        /// </summary>
        /// <param name="apiKey">Sub-client API key.</param>
        /// <param name="credit">Number of points to transfer.</param>
        /// <param name="desc">Transfer notes/descriptions.</param>
        /// <param name="vat">Value added tax configuration.</param>
        /// <param name="ip">Originating request IP address.</param>
        /// <returns>A task representing the asynchronous operation. Transaction transfer details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<SubClientResult> ChargeClientCreditAsync(string apiKey, long credit, string desc = null, int? vat = null, string ip = null);
#endif

        /// <summary>
        /// Blocks a recipient from receiving messages from your sender line.
        /// </summary>
        /// <param name="receptor">Target phone number.</param>
        /// <param name="lineNumber">Your line number.</param>
        /// <returns>Blacklist details block.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<BlacklistResult> AddBlockedLine(string receptor, string lineNumber);
#if !NET35
        /// <summary>
        /// Blocks a recipient from receiving messages from your sender line.
        /// </summary>
        /// <param name="receptor">Target phone number.</param>
        /// <param name="lineNumber">Your line number.</param>
        /// <returns>A task representing the asynchronous operation. Blacklist details block.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<BlacklistResult>> AddBlockedLineAsync(string receptor, string lineNumber);
#endif

        /// <summary>
        /// Unblocks a recipient, allowing them to receive messages from your line.
        /// </summary>
        /// <param name="receptor">Target phone number.</param>
        /// <param name="lineNumber">Your line number.</param>
        /// <returns>Unblock details block.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        RemoveBlacklistResult RemoveBlockedLine(string receptor, string lineNumber);
#if !NET35
        /// <summary>
        /// Unblocks a recipient, allowing them to receive messages from your line.
        /// </summary>
        /// <param name="receptor">Target phone number.</param>
        /// <param name="lineNumber">Your line number.</param>
        /// <returns>A task representing the asynchronous operation. Unblock details block.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        LineBlockListResult ListBlockedLines(string lineNumber, int pageNumber, long startDate, byte? blockReason = null);
#if !NET35
        /// <summary>
        /// Lists all blocked numbers associated with a line.
        /// </summary>
        /// <param name="lineNumber">Your line number.</param>
        /// <param name="pageNumber">Page offset.</param>
        /// <param name="startDate">Filter block window start date (yyyy-MM-dd).</param>
        /// <param name="blockReason">Optional reason filter.</param>
        /// <returns>A task representing the asynchronous operation. Blocked lines query page.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<LineBlockListResult> ListBlockedLinesAsync(string lineNumber, int pageNumber, long startDate, byte? blockReason = null);
#endif

        /// <summary>
        /// Checks if a phone number is currently blacklisted on a line.
        /// </summary>
        /// <param name="lineNumber">Your line number.</param>
        /// <param name="receptor">Target phone number.</param>
        /// <returns>Blacklist status check details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<BlacklistResult> BlockedLineExists(string lineNumber, string receptor);
#if !NET35
        /// <summary>
        /// Checks if a phone number is currently blacklisted on a line.
        /// </summary>
        /// <param name="lineNumber">Your line number.</param>
        /// <param name="receptor">Target phone number.</param>
        /// <returns>A task representing the asynchronous operation. Blacklist status check details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<BlacklistResult>> BlockedLineExistsAsync(string lineNumber, string receptor);
#endif

        /// <summary>
        /// Lists approved OTP message templates on the account.
        /// </summary>
        /// <param name="apiKey">Optional sub-client filter key.</param>
        /// <param name="localId">Optional local client filter ID.</param>
        /// <param name="page">Page offset.</param>
        /// <returns>List of template configurations.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<TemplateResult> ListTemplates(string apiKey = null, string localId = null, int? page = null);
#if !NET35
        /// <summary>
        /// Lists approved OTP message templates on the account.
        /// </summary>
        /// <param name="apiKey">Optional sub-client filter key.</param>
        /// <param name="localId">Optional local client filter ID.</param>
        /// <param name="page">Page offset.</param>
        /// <returns>A task representing the asynchronous operation. List of template configurations.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        CloneTemplateResult CloneTemplate(int? sourceTemplateId = null, string sourceTemplateName = null, string newTemplateName = null, string apiKey = null, string localId = null);
#if !NET35
        /// <summary>
        /// Clones an approved template to draft a new pattern.
        /// </summary>
        /// <param name="sourceTemplateId">Source template ID.</param>
        /// <param name="sourceTemplateName">Source template name.</param>
        /// <param name="newTemplateName">New template duplicate name.</param>
        /// <param name="apiKey">Sub-client key.</param>
        /// <param name="localId">Sub-client local ID.</param>
        /// <returns>A task representing the asynchronous operation. Cloning result metadata.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<CloneTemplateResult> CloneTemplateAsync(int? sourceTemplateId = null, string sourceTemplateName = null, string newTemplateName = null, string apiKey = null, string localId = null);
#endif

        /// <summary>
        /// Submits a request to register a new OTP template.
        /// </summary>
        /// <param name="dto">Template details.</param>
        /// <returns>Registration metadata status.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        TemplateInfoResult AddTemplate(TemplateDto dto);
#if !NET35
        /// <summary>
        /// Submits a request to register a new OTP template.
        /// </summary>
        /// <param name="dto">Template details.</param>
        /// <returns>A task representing the asynchronous operation. Registration metadata status.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<TemplateInfoResult> AddTemplateAsync(TemplateDto dto);
#endif

        /// <summary>
        /// Submits modifications for an existing template.
        /// </summary>
        /// <param name="templateId">Template ID.</param>
        /// <param name="dto">New template details.</param>
        /// <returns>Update status result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        TemplateInfoResult UpdateTemplate(int templateId, TemplateDto dto);
#if !NET35
        /// <summary>
        /// Submits modifications for an existing template.
        /// </summary>
        /// <param name="templateId">Template ID.</param>
        /// <param name="dto">New template details.</param>
        /// <returns>A task representing the asynchronous operation. Update status result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<TemplateInfoResult> UpdateTemplateAsync(int templateId, TemplateDto dto);
#endif

        /// <summary>
        /// Retrieves the configuration structure of a specific template.
        /// </summary>
        /// <param name="id">Template ID.</param>
        /// <param name="apiKey">Optional sub-client filter.</param>
        /// <param name="localId">Optional local client filter.</param>
        /// <returns>Template configuration details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        TemplateResult GetTemplate(int id, string apiKey = null, string localId = null);
#if !NET35
        /// <summary>
        /// Retrieves the configuration structure of a specific template.
        /// </summary>
        /// <param name="id">Template ID.</param>
        /// <param name="apiKey">Optional sub-client filter.</param>
        /// <param name="localId">Optional local client filter.</param>
        /// <returns>A task representing the asynchronous operation. Template configuration details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<TemplateResult> GetTemplateAsync(int id, string apiKey = null, string localId = null);
#endif

        /// <summary>
        /// Deletes an approved template from the account.
        /// </summary>
        /// <param name="id">Template ID.</param>
        /// <param name="apiKey">Optional sub-client filter.</param>
        /// <param name="localId">Optional local client filter.</param>
        /// <returns>Deletion confirmation message status.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        string DeleteTemplate(int id, string apiKey = null, string localId = null);
#if !NET35
        /// <summary>
        /// Deletes an approved template from the account.
        /// </summary>
        /// <param name="id">Template ID.</param>
        /// <param name="apiKey">Optional sub-client filter.</param>
        /// <param name="localId">Optional local client filter.</param>
        /// <returns>A task representing the asynchronous operation. Deletion confirmation message status.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>424</b>: Template not found or pending approval</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<string> DeleteTemplateAsync(int id, string apiKey = null, string localId = null);
#endif

        /// <summary>
        /// Uploads a media audio/image file for voice attachments from a local file path.
        /// </summary>
        /// <param name="filePath">Absolute local system path to the file.</param>
        /// <returns>Upload status details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        MediaResult UploadMedia(string filePath);
#if !NET35
        /// <summary>
        /// Uploads a media audio/image file for voice attachments from a local file path.
        /// </summary>
        /// <param name="filePath">Absolute local system path to the file.</param>
        /// <returns>A task representing the asynchronous operation. Upload status details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<MediaResult> UploadMediaAsync(string filePath);
#endif

        /// <summary>
        /// Uploads a media audio/image file directly from raw byte data.
        /// </summary>
        /// <param name="name">Filename.</param>
        /// <param name="fileBytes">Binary array content.</param>
        /// <returns>Upload status details containing media GUID ID.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        MediaResult UploadMedia(string name, byte[] fileBytes);
#if !NET35
        /// <summary>
        /// Uploads a media audio/image file directly from raw byte data.
        /// </summary>
        /// <param name="name">Filename.</param>
        /// <param name="fileBytes">Binary array content.</param>
        /// <returns>A task representing the asynchronous operation. Upload status details containing media GUID ID.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<MediaResult> UploadMediaAsync(string name, byte[] fileBytes);
#endif

        /// <summary>
        /// Lists uploaded media files available on the account.
        /// </summary>
        /// <param name="page">Page offset.</param>
        /// <param name="size">Page size.</param>
        /// <returns>Paged media records list.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        MediaListResult ListMedia(int? page = null, int? size = null);
#if !NET35
        /// <summary>
        /// Lists uploaded media files available on the account.
        /// </summary>
        /// <param name="page">Page offset.</param>
        /// <param name="size">Page size.</param>
        /// <returns>A task representing the asynchronous operation. Paged media records list.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<MediaListResult> ListMediaAsync(int? page = null, int? size = null);
#endif

        /// <summary>
        /// Retrieves metadata for a media file.
        /// </summary>
        /// <param name="id">Media file GUID.</param>
        /// <param name="fileName">Alternative lookup filename.</param>
        /// <returns>Media metadata properties.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        MediaResult GetMedia(Guid? id = null, string fileName = null);
#if !NET35
        /// <summary>
        /// Retrieves metadata for a media file.
        /// </summary>
        /// <param name="id">Media file GUID.</param>
        /// <param name="fileName">Alternative lookup filename.</param>
        /// <returns>A task representing the asynchronous operation. Media metadata properties.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<MediaResult> GetMediaAsync(Guid? id = null, string fileName = null);
#endif

        /// <summary>
        /// Deletes a media file from storage.
        /// </summary>
        /// <param name="id">Media file GUID.</param>
        /// <returns>Deletion status details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        MediaDeleteResult DeleteMedia(Guid id);
#if !NET35
        /// <summary>
        /// Deletes a media file from storage.
        /// </summary>
        /// <param name="id">Media file GUID.</param>
        /// <returns>A task representing the asynchronous operation. Deletion status details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>800</b>: Media file parameter is required</para>
        /// <para>• <b>801</b>: Invalid media file or processing failure</para>
        /// <para>• <b>802</b>: Media file size exceeds maximum allowed limit</para>
        /// <para>• <b>803</b>: Media resolution or aspect ratio not supported</para>
        /// <para>• <b>804</b>: Media playback duration exceeds maximum limit</para>
        /// <para>• <b>805</b>: Media upload permission denied</para>
        /// <para>• <b>806</b>: Media upload quota limit exceeded</para>
        /// <para>• <b>807</b>: Duplicate media file already uploaded</para>
        /// <para>• <b>808</b>: Invalid media GUID ID</para>
        /// <para>• <b>809</b>: Media file not found</para>
        /// <para>• <b>810</b>: Media file pending review and approval</para>
        /// <para>• <b>811</b>: Media file rejected by system or operator</para>
        /// <para>• <b>812</b>: Media not allowed for selected dispatch policy</para>
        /// <para>• <b>813</b>: Media dispatch restricted to internal lines</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
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
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<ContactResult> AddContact(int groupId, string number, string title = null, string birthdate = null, string email = null, string tags = null);
#if !NET35
        /// <summary>
        /// Inserts a new contact to a phonebook group.
        /// </summary>
        /// <param name="groupId">Phonebook group ID.</param>
        /// <param name="number">Mobile phone number.</param>
        /// <param name="title">Contact display name/title.</param>
        /// <param name="birthdate">Optional birthday.</param>
        /// <param name="email">Optional email address.</param>
        /// <param name="tags">Custom tags.</param>
        /// <returns>A task representing the asynchronous operation. The added contact record.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<ContactResult>> AddContactAsync(int groupId, string number, string title = null, string birthdate = null, string email = null, string tags = null);
#endif

        /// <summary>
        /// Creates a new contact category group in the phonebook.
        /// </summary>
        /// <param name="name">Group label.</param>
        /// <param name="tag">Custom categorization tag.</param>
        /// <returns>The created phonebook group metadata.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<GroupResult> AddGroup(string name, string tag = null);
#if !NET35
        /// <summary>
        /// Creates a new contact category group in the phonebook.
        /// </summary>
        /// <param name="name">Group label.</param>
        /// <param name="tag">Custom categorization tag.</param>
        /// <returns>A task representing the asynchronous operation. The created phonebook group metadata.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<GroupResult>> AddGroupAsync(string name, string tag = null);
#endif

        /// <summary>
        /// Removes a contact from a group or deletes it entirely.
        /// </summary>
        /// <param name="contactId">Contact ID.</param>
        /// <param name="mobile">Phone number.</param>
        /// <param name="groupId">Filter group ID.</param>
        /// <returns>Operation confirmation details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<ContactResult> RemoveContact(string contactId = null, string mobile = null, int? groupId = null);
#if !NET35
        /// <summary>
        /// Removes a contact from a group or deletes it entirely.
        /// </summary>
        /// <param name="contactId">Contact ID.</param>
        /// <param name="mobile">Phone number.</param>
        /// <param name="groupId">Filter group ID.</param>
        /// <returns>A task representing the asynchronous operation. Operation confirmation details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<ContactResult>> RemoveContactAsync(string contactId = null, string mobile = null, int? groupId = null);
#endif

        /// <summary>
        /// Deletes a contact category group.
        /// </summary>
        /// <param name="groupId">Group ID.</param>
        /// <returns>Confirmation details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<GroupResult> RemoveGroup(int groupId);
#if !NET35
        /// <summary>
        /// Deletes a contact category group.
        /// </summary>
        /// <param name="groupId">Group ID.</param>
        /// <returns>A task representing the asynchronous operation. Confirmation details.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<GroupResult>> RemoveGroupAsync(int groupId);
#endif

        /// <summary>
        /// Lists contact groups in the phonebook.
        /// </summary>
        /// <returns>A list of phonebook group records.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<GroupResult> ListGroups();
#if !NET35
        /// <summary>
        /// Lists contact groups in the phonebook.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. A list of phonebook group records.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<GroupResult>> ListGroupsAsync();
#endif

        /// <summary>
        /// Modifies configuration settings of a phonebook group.
        /// </summary>
        /// <param name="groupId">Group ID.</param>
        /// <param name="groupName">New group name.</param>
        /// <param name="status">Group status.</param>
        /// <param name="tag">Category tags.</param>
        /// <returns>The updated group record.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<GroupResult> EditGroup(int groupId, string groupName = null, byte? status = null, string tag = null);
#if !NET35
        /// <summary>
        /// Modifies configuration settings of a phonebook group.
        /// </summary>
        /// <param name="groupId">Group ID.</param>
        /// <param name="groupName">New group name.</param>
        /// <param name="status">Group status.</param>
        /// <param name="tag">Category tags.</param>
        /// <returns>A task representing the asynchronous operation. The updated group record.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<GroupResult>> EditGroupAsync(int groupId, string groupName = null, byte? status = null, string tag = null);
#endif

        /// <summary>
        /// Searches contact groups matching a tag query.
        /// </summary>
        /// <param name="tag">Category tag.</param>
        /// <returns>List of matching groups.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        List<GroupResult> SearchGroups(string tag);
#if !NET35
        /// <summary>
        /// Searches contact groups matching a tag query.
        /// </summary>
        /// <param name="tag">Category tag.</param>
        /// <returns>A task representing the asynchronous operation. List of matching groups.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<List<GroupResult>> SearchGroupsAsync(string tag);
#endif

        /// <summary>
        /// Retrieves the current system date and time from Kavenegar servers.
        /// </summary>
        /// <returns>Server date result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        ServerDateResult GetServerDate();
#if !NET35
        /// <summary>
        /// Retrieves the current system date and time from Kavenegar servers.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Server date result.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<ServerDateResult> GetServerDateAsync();
#endif

        /// <summary>
        /// Tests network connection and credentials validation.
        /// </summary>
        /// <returns>Always returns string "pong" on success.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        string Ping();
#if !NET35
        /// <summary>
        /// Tests network connection and credentials validation.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Always returns string "pong" on success.</returns>
        /// <exception cref="Kavenegar.Exceptions.ApiException">
        /// Thrown when the Kavenegar API returns an error response:
        /// <para>• <b>400</b>: Invalid input parameters (e.g., page size > 500)</para>
        /// <para>• <b>403</b>: Invalid API Key</para>
        /// <para>• <b>418</b>: Insufficient account credit balance</para>
        /// </exception>
        /// <exception cref="Kavenegar.Exceptions.HttpException">Thrown when an HTTP network communication failure occurs.</exception>
        System.Threading.Tasks.Task<string> PingAsync();
#endif
    }
}

