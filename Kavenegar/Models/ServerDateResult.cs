namespace Kavenegar.Models
{
    public class ServerDateResult
    {
        public string Datetime { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public long Unixtime { get; set; }
    }
}
