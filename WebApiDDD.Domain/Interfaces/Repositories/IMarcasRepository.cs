using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Domain.Interfaces.Repositories.Base;
using WebApiDDD.Domain.Models;

namespace WebApiDDD.Domain.Interfaces.Repositories
{
    public interface IMarcasRepository : IBaseRepository<Marcas, MarcasFilterParams>
    {
    }
}
