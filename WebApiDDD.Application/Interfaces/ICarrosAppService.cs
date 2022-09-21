using WebApiDDD.Application.Interfaces.Base;
using WebApiDDD.Application.ViewModels.Carros;
using WebApiDDD.Domain.FilterParams;

namespace WebApiDDD.Application.Interfaces
{
    public interface ICarrosAppService : ICRUDBaseAppService<CarrosFilterParams, GetViewModel, ListViewModel, CreateUpdateViewModel, CreateUpdateViewModel>
    {
    }
}
