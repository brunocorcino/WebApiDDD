using System.Linq.Expressions;
using WebApiDDD.Domain.FilterParams.Base;
using WebApiDDD.Domain.Interfaces.Repositories.Base;
using WebApiDDD.Domain.Interfaces.Services.Base;
using WebApiDDD.Domain.Models.Base;
using WebApiDDD.Infra.CrossCutting.Common.Operacao;

namespace WebApiDDD.Domain.Services.Base
{
    public abstract class BaseService<TModel, TFilterParams, TRepository> : IBaseService<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
        where TRepository : IBaseRepository<TModel, TFilterParams>
    {
        protected TRepository Repository;

        protected BaseService(TRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<TModel>> ListAsync(TFilterParams filterParams)
        {
            return await ListAsync(filterParams, x => x);
        }

        public virtual async Task<IEnumerable<TSelect>> ListAsync<TSelect>(TFilterParams filterParams, Expression<Func<TModel, TSelect>> selectExpression)
        {
            return await Task.Run(() => 
            {
                var data = Repository
                    .Query(filterParams)
                    .Select(selectExpression);

                return Repository
                    .QueryPagination(data, filterParams)
                    .ToList();
            });
        }

        public virtual async Task<Operacao<TModel>> CreateAsync(Operacao<TModel> operacao)
        {
            operacao.AdicionarMensagem(operacao.ValidarPropriedadesObrigatorias());

            if (operacao.Erro)
                return operacao;

            await OnBeforeCreatedAsync(this, operacao);

            if (operacao.Erro)
                return operacao;

            var model = operacao.Entidade;

            await Repository.AddAsync(model);

            var linhasAfetadas = await Repository.SaveChangesAsync();

            if (linhasAfetadas > 0)
                await OnAfterCreatedAsync(this, operacao);

            return operacao;
        }

        public virtual async Task<Operacao<TModel>> UpdateAsync(Operacao<TModel> operacao)
        {
            operacao.AdicionarMensagem(operacao.ValidarPropriedadesObrigatorias());

            if (operacao.Erro)
                return operacao;

            await OnBeforeUpdatedAsync(this, operacao);

            if (operacao.Erro)
                return operacao;

            var model = operacao.Entidade;

            await Repository.UpdateAsync(model);

            var linhasAfetadas = await Repository.SaveChangesAsync();

            if (linhasAfetadas > 0)
                await OnAfterUpdatedAsync(this, operacao);

            return operacao;
        }

        public virtual async Task<Operacao<Guid>> DeleteAsync(Operacao<Guid> operacao)
        {
            operacao.AdicionarMensagem(operacao.ValidarPropriedadesObrigatorias());

            if (operacao.Erro)
                return operacao;

            await OnBeforeDeletedAsync(this, operacao);

            if (operacao.Erro)
                return operacao;

            var id = operacao.Entidade;

            await Repository.DeleteAsync(id);

            var linhasAfetadas = await Repository.SaveChangesAsync();

            if (linhasAfetadas > 0)
                await OnAfterDeletedAsync(this, operacao);

            return operacao;
        }

        protected virtual async Task OnBeforeCreatedAsync(object sender, Operacao<TModel> operacao)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnAfterCreatedAsync(object sender, Operacao<TModel> operacao)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnBeforeUpdatedAsync(object sender, Operacao<TModel> operacao)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnAfterUpdatedAsync(object sender, Operacao<TModel> operacao)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnBeforeDeletedAsync(object sender, Operacao<Guid> operacao)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnAfterDeletedAsync(object sender, Operacao<Guid> operacao)
        {
            await Task.CompletedTask;
        }
    }
}
