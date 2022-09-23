using Microsoft.AspNetCore.Mvc;
using WebApiDDD.Api.Controllers.Base;
using WebApiDDD.Application.Interfaces;
using WebApiDDD.Application.ViewModels.Marcas;
using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Infra.CrossCutting.Common.Helper;

namespace WebApiDDD.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : BaseController<IMarcasAppService, GetViewModel, ListViewModel, MarcasFilterParams, CreateUpdateViewModel, CreateUpdateViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appService"></param>
        public MarcasController(IMarcasAppService appService) : base(appService)
        {
        }

        /// <summary>
        /// Executa uma pesquisa, baseada no identificador, retornando a quantidade de carros para a marca
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Quantidade de carros</returns>
        [HttpGet("quantidade-carros")]
        public async Task<IActionResult> GetQuantidadeCarrosAsync([FromQuery] Guid id)
        {
            ApiResult<int> apiResult = new();

            try
            {
                if (id == Guid.Empty)
                {
                    apiResult.Status.AdicionarErro(string.Format(Messages.CampoObrigatorio, "Id"));
                    return HandlerError(apiResult);
                }

                apiResult.Data = await AppService.GetQuantidadeCarrosAsync(id);
            }
            catch (Exception ex)
            {
                return HandlerError(apiResult, ex);
            }

            return Ok(apiResult);
        }
    }
}
