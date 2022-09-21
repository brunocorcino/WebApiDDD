using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Domain.Interfaces.Repositories;
using WebApiDDD.Domain.Models;
using WebApiDDD.Infra.Data.Context;
using WebApiDDD.Infra.Data.Repositories.Base;

namespace WebApiDDD.Infra.Data.Repositories
{
    public class CarrosRepository : BaseRepository<Carros, CarrosFilterParams>, ICarrosRepository
    {
        public CarrosRepository(WebApiDDDContext context) : base(context)
        {
        }

        public override IQueryable<Carros> Query(CarrosFilterParams filterParams)
        {
            var query = base.Query(filterParams);

            if (filterParams.IdMarca.HasValue)
                query = query.Where(x => x.IdMarca == filterParams.IdMarca);

            if (!string.IsNullOrEmpty(filterParams.Modelo))
                query = query.Where(x => x.Modelo.Contains(filterParams.Modelo));

            return query;
        }
    }
}
