using webapinews.Entities;

namespace webapinews.Helpers
{

    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPreviousPage { get { return (CurrentPage > 1); } }
        public bool HasNextPage { get { return (CurrentPage < TotalPages); } }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            CurrentPage = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            AddRange(items);
        }

        public static PaginatedList<T> Create(IQueryable<T> source,OwnerStringParameter ownerStringParameter)
        {
            var count = source.Count();
            var items = source.Skip((ownerStringParameter.PageNumber - 1) 
                * ownerStringParameter.PageSize).Take(ownerStringParameter.PageSize).ToList();
            return new PaginatedList<T>(items, count, ownerStringParameter.PageNumber,ownerStringParameter.PageSize);
        }
    }

}

