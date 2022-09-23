using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Domain.Interfaces.Repositories;
using WebApiDDD.Domain.Models;
using WebApiDDD.Infra.Data.Context;
using WebApiDDD.Infra.Data.Repositories.Base;

namespace WebApiDDD.Infra.Data.Repositories
{
    public class MarcasRepository : BaseRepository<Marcas, MarcasFilterParams>, IMarcasRepository
    {
        public MarcasRepository(WebApiDDDContext context) : base(context)
        {
        }

        public override IQueryable<Marcas> Query(MarcasFilterParams filterParams)
        {
            var query = base.Query(filterParams);

            if (!string.IsNullOrEmpty(filterParams.Nome))
                query = query.Where(x => x.Nome.Contains(filterParams.Nome));

            if (!string.IsNullOrEmpty(filterParams.NomeExato))
                query = query.Where(x => x.Nome == filterParams.NomeExato);

            return query;
        }
    }
}
