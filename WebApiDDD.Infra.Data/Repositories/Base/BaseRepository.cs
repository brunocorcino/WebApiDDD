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
            var query = _dbSet.AsQueryable();

            if (filterParams.Id.HasValue)
                query = query.Where(x => x.Id == filterParams.Id);

            if (filterParams.Ids != null && filterParams.Ids.Any())
                query = query.Where(x => filterParams.Ids.Contains(x.Id));

            return query;
        }

        public virtual IQueryable<TSelect> QueryPagination<TSelect>(IQueryable<TSelect> query, TFilterParams filterParams)
        {
            if (!filterParams.IgnorePagination && filterParams.PageSize > 0)
            {
                var take = filterParams.PageSize;
                var skip = (filterParams.PageNumber - 1) * take;

                skip = skip >= 0 ? skip : 0;
                query = query.Skip(skip).Take(take);
            }

            return query;
        }

        public virtual async Task AddAsync(TModel model)
        {
            await _dbSet.AddAsync(model);
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            await Task.Run(() =>
            {
                _dbSet.Update(model);
            });
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var model = await _dbSet.FindAsync(id);
            if (model != null) _dbSet.Remove(model);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
