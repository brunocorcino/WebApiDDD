using WebApiDDD.Domain.FilterParams.Base;

namespace WebApiDDD.Domain.FilterParams
{
    public class MarcasFilterParams : BaseFilterParams
    {
        public string Nome { get; set; }
        public string NomeExato { get; set; }
    }
}
