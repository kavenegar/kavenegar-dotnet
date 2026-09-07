using System;
using System.Collections.Generic;
using Kavenegar.Models.Enums;

namespace Kavenegar.Models
{
    public class SendRequest
    {
        public string Sender { get; set; }
        public List<string> Receptor { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; } = MessageType.MobileMemory;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public List<string> LocalIds { get; set; }
        public string Tag { get; set; }
        public string Text { get; set; }
        public string Hide { get; set; }
        public string LocalMessageId { get; set; }
        public string Policy { get; set; }
        public Guid? MediaId { get; set; }
    }
}
