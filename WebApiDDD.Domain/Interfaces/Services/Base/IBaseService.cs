using System.Linq.Expressions;
using WebApiDDD.Domain.FilterParams.Base;
using WebApiDDD.Domain.Models.Base;
using WebApiDDD.Infra.CrossCutting.Common.Operacao;

namespace WebApiDDD.Domain.Interfaces.Services.Base
{
    public interface IBaseService<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
    {
        Task<TModel> GetByIdAsync(Guid id);
        Task<IEnumerable<TModel>> ListAsync(TFilterParams filterParams);
        Task<IEnumerable<TSelect>> ListAsync<TSelect>(TFilterParams filterParams, Expression<Func<TModel, TSelect>> selectExpression);
        Task<Operacao<TModel>> CreateAsync(Operacao<TModel> operacao);
        Task<Operacao<TModel>> UpdateAsync(Operacao<TModel> operacao);
        Task<Operacao<Guid>> DeleteAsync(Operacao<Guid> operacao);
    }
}
