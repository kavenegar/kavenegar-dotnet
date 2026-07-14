using System;
using Kavenegar.Models.Enums;

namespace Kavenegar.Models
{
    public class MediaResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mime_Type { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public int Duration { get; set; }
        public string Resolution { get; set; }
        public MediaFileStatus Status { get; set; }
        public string Status_Desc { get; set; }
    }
}
