using System.Drawing.Printing;

namespace webapinews.Helpers
{
    public class OwnerStringParameter
    {
        
        const int maxPageSize = 50;
        private string _search = string.Empty;
        private string _sortBy = string.Empty;
        public string SortBy
        {
            get { return _sortBy; }
            set { _sortBy = value; }
        }
        public string search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
            }
        }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }


    }

}
