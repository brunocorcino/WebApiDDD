namespace WebApiDDD.Domain.FilterParams.Base
{
    public class BaseFilterParams
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 100;

        private int _pageNumber;
        private int _pageSize;

        public BaseFilterParams()
        {
            _pageNumber = DefaultPageNumber;
            _pageSize = DefaultPageSize;
        }

        public Guid? Id { get; set; }
        public List<Guid> Ids { get; set; }

        public int PageNumber 
        {
            get => _pageNumber <= 0 ? DefaultPageNumber : _pageNumber;
            set => _pageNumber = value;
        }
        public int PageSize
        { 
            get
            {
                if (IgnorePagination) return int.MaxValue;

                return _pageSize <= 0 ? DefaultPageSize : _pageSize;
            }
            set => _pageSize = value;
        }

        public bool IgnorePagination { get; set; }
    }
}
