namespace Kavenegar.Models
{
    public class CursorPaginationModel
    {
        public int PrevCursor { get; set; }
        public int CurrentCursor { get; set; }
        public int NextCursor { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
    }
}
