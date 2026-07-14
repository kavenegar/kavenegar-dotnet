namespace Kavenegar.Models
{
    public class GroupResult
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public long LastModified { get; set; }
        public string[] Tags { get; set; }
        public int Parent { get; set; }
        public int Count { get; set; }
    }
}
