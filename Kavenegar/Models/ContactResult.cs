namespace Kavenegar.Models
{
    public class ContactResult
    {
        public int ContactId { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public int GroupId { get; set; }
        public long LastModified { get; set; }
        public long Birthdate { get; set; }
        public string Email { get; set; }
        public string[] Tags { get; set; }
    }
}
