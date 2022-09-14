using WebApiDDD.Domain.FilterParams.Base;
using WebApiDDD.Domain.Models.Base;

namespace WebApiDDD.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
    {
        Task<TModel> GetByIdAsync(Guid id);
        IQueryable<TModel> Query(TFilterParams filterParams);
        IQueryable<TModel> QueryPagination<TSelect>(IQueryable<TSelect> query, TFilterParams filterParams);
        Task AddAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
