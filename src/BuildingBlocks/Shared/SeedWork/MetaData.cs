namespace Shared.SeedWork
{
    public class MetaData
    {

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public long TotalItem { get; set; }
        public int PageSize { get; set; }

        public bool HasNext => CurrentPage > 1;
        public bool HasPrevious => CurrentPage < TotalPages;
        public int firstRowOnPage => TotalItem > 0 ? (CurrentPage - 1) * PageSize + 1 : 0;
        public int lastRowOnPage => (int)Math.Min(CurrentPage * PageSize, TotalItem);
    }
}
