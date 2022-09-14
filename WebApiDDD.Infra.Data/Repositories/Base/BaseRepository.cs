using Microsoft.EntityFrameworkCore;
using WebApiDDD.Domain.FilterParams.Base;
using WebApiDDD.Domain.Interfaces.Repositories.Base;
using WebApiDDD.Domain.Models.Base;
using WebApiDDD.Infra.Data.Context;

namespace WebApiDDD.Infra.Data.Repositories.Base
{
    public abstract class BaseRepository<TModel, TFilterParams> : IBaseRepository<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
    {
        protected readonly WebApiDDDContext _context;
        protected readonly DbSet<TModel> _dbSet;

        public BaseRepository(WebApiDDDContext context)
        {
            _context = context;
            _dbSet = _context.Set<TModel>();
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<TModel> Query(TFilterParams filterParams)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TModel> QueryPagination<TSelect>(IQueryable<TSelect> query, TFilterParams filterParams)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}
