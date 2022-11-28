using System;
using System.Collections.Generic;
using Kavenegar.Models;
using Kavenegar.Models.Enums;

namespace Kavenegar
{
    public interface IKavenegarApi
    {
        string ApiKey { set; get; }
        int ReturnCode { get; }
        string ReturnMessage { get; }
        List<SendResult> Send(string sender, List<string> receptor, string message);
        SendResult Send(string sender, String receptor, string message);
        SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date);
        List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date);
        SendResult Send(string sender, string receptor, string message, MessageType type, DateTime date, string localid);
        SendResult Send(string sender, string receptor, string message, string localid);
        List<SendResult> Send(string sender, List<string> receptors, string message, string localid);
        List<SendResult> Send(string sender, List<string> receptor, string message, MessageType type, DateTime date, List<string> localids);
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages);
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date);
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, MessageType type, DateTime date, string localmessageids);
        List<SendResult> SendArray(string sender, List<string> receptors, List<string> messages, string localmessageid);
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages, string localmessageid);
        List<SendResult> SendArray(List<string> senders, List<string> receptors, List<string> messages, List<MessageType> types, DateTime date, List<string> localmessageids);
        List<StatusResult> Status(List<string> messageids);
        StatusResult Status(string messageid);
        List<StatusLocalMessageIdResult> StatusLocalMessageId(List<string> messageids);
        StatusLocalMessageIdResult StatusLocalMessageId(string messageid);
        List<SendResult> Select(List<string> messageids);
        SendResult Select(string messageid);
        List<SendResult> SelectOutbox(DateTime startdate);
        List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate);
        List<SendResult> SelectOutbox(DateTime startdate, DateTime enddate, String sender);
        List<SendResult> LatestOutbox(long pagesize);
        List<SendResult> LatestOutbox(long pagesize, String sender);
        CountOutboxResult CountOutbox(DateTime startdate);
        CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate);
        CountOutboxResult CountOutbox(DateTime startdate, DateTime enddate, int status);
        List<StatusResult> Cancel(List<String> ids);
        StatusResult Cancel(String messageid);
        List<ReceiveResult> Receive(string line, int isread);
        CountInboxResult CountInbox(DateTime startdate, string linenumber);
        CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber);
        CountInboxResult CountInbox(DateTime startdate, DateTime enddate, String linenumber, int isread);
        List<CountPostalCodeResult> CountPostalCode(long postalcode);
        List<SendResult> SendByPostalCode(long postalcode, String sender, String message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount);
        List<SendResult> SendByPostalCode(long postalcode, String sender, String message, long mcistartIndex, long mcicount, long mtnstartindex, long mtncount, DateTime date);
        AccountInfoResult AccountInfo();
        AccountConfigResult AccountConfig(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed);
        SendResult VerifyLookup(string receptor, string token, string template);
        SendResult VerifyLookup(string receptor, string token, string template, VerifyLookupType type);
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template);
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string template);
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string template, VerifyLookupType type);
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string template, VerifyLookupType type);
        SendResult VerifyLookup(string receptor, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type);
        SendResult CallMakeTTS(string message, string receptor);
        List<SendResult> CallMakeTTS(string message, List<string> receptor);
        List<SendResult> CallMakeTTS(string message, List<string> receptor, DateTime? date, List<string> localid);
    }
}