using WebApiDDD.Infra.CrossCutting.Common.Operacao;

namespace WebApiDDD.Application.Interfaces.Base
{
    public interface IBaseAppService
    {
    }

    public interface IListBaseAppService<TListViewModel, TFilterParams>
    {
        Task<IEnumerable<TListViewModel>> ListAsync(TFilterParams filterParams);
    }

    public interface IGetBaseAppService<TGetViewModel>
    {
        Task<TGetViewModel> GetByIdAsync(Guid id);
    }

    public interface ICreateBaseAppService<TCreateViewModel>
    {
        Task<Operacao<TCreateViewModel>> CreateAsync(Operacao<TCreateViewModel> operacao);
    }

    public interface IUpdateBaseAppService<TUpdateViewModel>
    {
        Task<Operacao<TUpdateViewModel>> UpdateAsync(Operacao<TUpdateViewModel> operacao);
    }

    public interface IDeleteBaseAppService
    {
        Task<Operacao<Guid>> DeleteAsync(Operacao<Guid> operacao);
    }

    public interface ICRUDBaseAppService<TFilterParams, TGetViewModel, TListViewModel, TCreateViewModel, TUpdateViewModel> :
        IListBaseAppService<TFilterParams, TListViewModel>,
        IGetBaseAppService<TGetViewModel>,
        ICreateBaseAppService<TCreateViewModel>,
        IUpdateBaseAppService<TUpdateViewModel>,
        IDeleteBaseAppService
    {
    }
}
