using System;
using System.Collections.Generic;
using Kavenegar.Models.Enums;

namespace Kavenegar.Models
{
    public class SendArrayRequest
    {
        public List<string> Senders { get; set; }
        public List<string> Receptors { get; set; }
        public List<string> Messages { get; set; }
        public List<MessageType> Types { get; set; }
        public DateTime Date { get; set; } = DateTime.MinValue;
        public List<string> LocalMessageIds { get; set; }
        public string Tag { get; set; }
        public string Hide { get; set; }
        public string Policy { get; set; }
        public Guid? MediaId { get; set; }
    }
}
