using System.Collections.Generic;

namespace Kavenegar.Models
{
    public class MediaListResult
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public long Total { get; set; }
        public List<MediaResult> List { get; set; }
    }
}
