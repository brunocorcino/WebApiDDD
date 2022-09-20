using System.Linq.Expressions;
using System.Transactions;
using WebApiDDD.Application.Interfaces.Base;
using WebApiDDD.Application.ViewModels.Base;
using WebApiDDD.Domain.FilterParams.Base;
using WebApiDDD.Domain.Interfaces.Services.Base;
using WebApiDDD.Domain.Models.Base;
using WebApiDDD.Infra.CrossCutting.Common.Operacao;
using WebApiDDD.Infra.CrossCutting.Common.Transaction;

namespace WebApiDDD.Application.AppServices.Base
{
    public abstract class BaseAppService<TService, TModel, TFilterParams, TGetViewModel, TListViewModel, TCreateViewModel, TUpdateViewModel>
        : BaseAppService<TService, TModel, TFilterParams, TGetViewModel, TListViewModel, TCreateViewModel>, IUpdateBaseAppService<TUpdateViewModel>
        where TService : IBaseService<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
        where TCreateViewModel : BaseViewModel
        where TUpdateViewModel : BaseViewModel
    {
        protected BaseAppService(TService service) : base(service)
        {
        }

        public virtual async Task<Operacao<TUpdateViewModel>> UpdateAsync(Operacao<TUpdateViewModel> operacao)
        {
            operacao.AdicionarMensagem(operacao.ValidarPropriedadesObrigatorias());

            if (operacao.Erro)
                return operacao;

            var model = await GetModelToUpdateAsync(operacao.Entidade.Id);

            if (model == null)
            {
                operacao.AdicionarErro(WebApiDDD.Infra.CrossCutting.Common.Helper.Messages.EntidadeNaoEncontrada);
                return operacao;
            }

            model = await MapToUpdateModelAsync(operacao, model);

            await OnValidateUpdateViewModelAsync(operacao, model);

            if (operacao.Erro)
                return operacao;

            using (TransactionScope transaction = TransactionScopeFactory.Create(true))
            {
                var operacaoService = operacao.CopiarOperacao<TModel>(model);

                operacaoService = await Service.UpdateAsync(operacaoService);

                operacao.AdicionarMensagem(operacaoService);

                if (operacao.Erro)
                    return operacao;

                await OnBeforeUpdateTransactionCompleteAsync(operacao, model);

                if (operacao.Erro)
                    return operacao;

                transaction.Complete();
            }

            return operacao;
        }

        protected virtual async Task<TModel> GetModelToUpdateAsync(Guid id)
        {
            return await Service.GetByIdAsync(id);
        }

        protected abstract Task<TModel> MapToUpdateModelAsync(Operacao<TUpdateViewModel> operacao, TModel model);

        protected virtual async Task OnValidateUpdateViewModelAsync(Operacao<TUpdateViewModel> operacao, TModel model)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnBeforeUpdateTransactionCompleteAsync(Operacao<TUpdateViewModel> operacao, TModel model)
        {
            await Task.CompletedTask;
        }
    }

    public abstract class BaseAppService<TService, TModel, TFilterParams, TGetViewModel, TListViewModel, TCreateViewModel>
        : BaseAppService<TService, TModel, TFilterParams, TGetViewModel, TListViewModel>, ICreateBaseAppService<TCreateViewModel>, IDeleteBaseAppService
        where TService : IBaseService<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
        where TCreateViewModel : BaseViewModel
    {
        protected BaseAppService(TService service) : base(service)
        {
        }

        public virtual async Task<Operacao<TCreateViewModel>> CreateAsync(Operacao<TCreateViewModel> operacao)
        {
            operacao.AdicionarMensagem(operacao.ValidarPropriedadesObrigatorias());

            if (operacao.Erro)
                return operacao;

            var model = await MapToCreateModelAsync(operacao);

            await OnValidateCreateViewModelAsync(operacao, model);

            if (operacao.Erro)
                return operacao;

            using (TransactionScope transaction = TransactionScopeFactory.Create(true))
            {
                var operacaoService = operacao.CopiarOperacao<TModel>(model);

                operacaoService = await Service.CreateAsync(operacaoService);

                operacao.AdicionarMensagem(operacaoService);

                if (operacao.Erro)
                    return operacao;

                await OnBeforeCreateTransacionCompleteAsync(operacao, model);

                if (operacao.Erro)
                    return operacao;

                transaction.Complete();
            }

            return operacao;
        }

        protected abstract Task<TModel> MapToCreateModelAsync(Operacao<TCreateViewModel> operacao);

        protected virtual async Task OnValidateCreateViewModelAsync(Operacao<TCreateViewModel> operacao, TModel model)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnBeforeCreateTransacionCompleteAsync(Operacao<TCreateViewModel> operacao, TModel model)
        {
            await Task.CompletedTask;
        }

        public virtual async Task<Operacao<Guid>> DeleteAsync(Operacao<Guid> operacao)
        {
            operacao.AdicionarMensagem(operacao.ValidarPropriedadesObrigatorias());

            if (operacao.Erro)
                return operacao;

            await OnValidateDeleteViewModelAsync(operacao);

            if (operacao.Erro)
                return operacao;

            using (TransactionScope transaction = TransactionScopeFactory.Create(true))
            {
                await OnBeforeDeleteAsync(operacao);

                if (operacao.Erro)
                    return operacao;

                operacao = await Service.DeleteAsync(operacao);

                if (operacao.Erro)
                    return operacao;

                await OnAfterDeleteAndBeforeTransactionCompleteAsync(operacao);

                if (operacao.Erro)
                    return operacao;

                transaction.Complete();
            }

            return operacao;
        }

        protected virtual async Task OnValidateDeleteViewModelAsync(Operacao<Guid> operacao)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnBeforeDeleteAsync(Operacao<Guid> operacao)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnAfterDeleteAndBeforeTransactionCompleteAsync(Operacao<Guid> operacao)
        {
            await Task.CompletedTask;
        }
    }

    public abstract class BaseAppService<TService, TModel, TFilterParams, TGetViewModel, TListViewModel>
        : BaseAppService<TService, TModel, TFilterParams, TGetViewModel>, IListBaseAppService<TListViewModel, TFilterParams>
        where TService : IBaseService<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
    {
        protected BaseAppService(TService service) : base(service)
        {
        }

        public virtual async Task<IEnumerable<TListViewModel>> ListAsync(TFilterParams filterParams)
        {
            return await Service.ListAsync(filterParams, SelectToListViewModel());
        }

        protected abstract Expression<Func<TModel, TListViewModel>> SelectToListViewModel();
    }

    public abstract class BaseAppService<TService, TModel, TFilterParams, TGetViewModel>
        : BaseAppService<TService, TModel, TFilterParams>, IGetBaseAppService<TGetViewModel>
        where TService : IBaseService<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
    {
        protected BaseAppService(TService service) : base(service)
        {
        }

        public virtual async Task<TGetViewModel> GetByIdAsync(Guid id)
        {
            var data = await Service.GetByIdAsync(id);

            if (data == null)
                return default;

            return await MapToGetViewModelAsync(data);
        }

        protected abstract Task<TGetViewModel> MapToGetViewModelAsync(TModel model);
    }

    public abstract class BaseAppService<TService, TModel, TFilterParams> : BaseAppService
        where TService : IBaseService<TModel, TFilterParams>
        where TModel : BaseModel
        where TFilterParams : BaseFilterParams
    {
        protected readonly TService Service;

        public BaseAppService(TService service)
        {
            Service = service;
        }
    }

    public abstract class BaseAppService : IBaseAppService
    {
    }
}
