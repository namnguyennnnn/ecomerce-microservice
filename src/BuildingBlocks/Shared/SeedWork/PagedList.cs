namespace Shared.SeedWork
{
    public class PagedList<T>: List<T>
    {
        private MetaData _metaData;

        public PagedList(IEnumerable<T> items, long totalItems, int pageNumber, int pageSize)
        {
            _metaData = new MetaData
            {
                CurrentPage = pageNumber,
                TotalItem = totalItems,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
            AddRange(items);
        }

        public MetaData MetaData => _metaData; 
    }
}
