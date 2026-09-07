using Kavenegar.Models.Enums;

namespace Kavenegar.Models
{
    public class MediaReviewResult
    {
        public MediaReviewStatus Status { get; set; }
        public string Status_Desc { get; set; }
        public MediaReviewReason Reason { get; set; }
        public string Reason_Desc { get; set; }
    }
}
