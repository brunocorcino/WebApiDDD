using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Domain.Interfaces.Services.Base;
using WebApiDDD.Domain.Models;

namespace WebApiDDD.Domain.Interfaces.Services
{
    public interface IMarcasService : IBaseService<Marcas, MarcasFilterParams>
    {
    }
}
