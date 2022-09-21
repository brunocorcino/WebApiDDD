using WebApiDDD.Domain.FilterParams.Base;

namespace WebApiDDD.Domain.FilterParams
{
    public class CarrosFilterParams : BaseFilterParams
    {
        public Guid? IdMarca { get; set; }
        public string Modelo { get; set; }
    }
}
