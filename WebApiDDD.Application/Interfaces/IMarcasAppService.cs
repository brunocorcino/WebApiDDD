using WebApiDDD.Application.Interfaces.Base;
using WebApiDDD.Application.ViewModels.Marcas;
using WebApiDDD.Domain.FilterParams;

namespace WebApiDDD.Application.Interfaces
{
    public interface IMarcasAppService : ICRUDBaseAppService<MarcasFilterParams, GetViewModel, ListViewModel, CreateUpdateViewModel, CreateUpdateViewModel>
    {
        Task<int> GetQuantidadeCarrosAsync(Guid id);
    }
}
