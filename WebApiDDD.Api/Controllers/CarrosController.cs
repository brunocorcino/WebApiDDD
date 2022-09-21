using Microsoft.AspNetCore.Mvc;
using WebApiDDD.Api.Controllers.Base;
using WebApiDDD.Application.Interfaces;
using WebApiDDD.Application.ViewModels.Carros;
using WebApiDDD.Domain.FilterParams;

namespace WebApiDDD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : BaseController<ICarrosAppService, GetViewModel, ListViewModel, CarrosFilterParams, CreateUpdateViewModel, CreateUpdateViewModel>
    {
        public CarrosController(ICarrosAppService appService) : base(appService)
        {
        }
    }
}
