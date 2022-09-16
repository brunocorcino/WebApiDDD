using WebApiDDD.Infra.CrossCutting.Common.Helper;

namespace WebApiDDD.Domain.FilterParams.Base
{
    public class BaseFilterParams
    {
        private int _pageNumber;
        private int _pageSize;

        public BaseFilterParams()
        {
            _pageNumber = Constants.DefaultPageNumber;
            _pageSize = Constants.DefaultPageSize;
        }

        public Guid? Id { get; set; }
        public List<Guid> Ids { get; set; }

        public int PageNumber 
        {
            get => _pageNumber <= 0 ? Constants.DefaultPageNumber : _pageNumber;
            set => _pageNumber = value;
        }
        public int PageSize
        { 
            get
            {
                if (IgnorePagination) return int.MaxValue;

                return _pageSize <= 0 ? Constants.DefaultPageSize : _pageSize;
            }
            set => _pageSize = value;
        }

        public bool IgnorePagination { get; set; }
    }
}
