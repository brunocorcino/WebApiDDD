using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDDD.Application.Interfaces.Base;
using WebApiDDD.Application.ViewModels.Base;
using WebApiDDD.Infra.CrossCutting.Common.Helper;
using WebApiDDD.Infra.CrossCutting.Common.Operacao;
using Microsoft.Data.SqlClient;

namespace WebApiDDD.Api.Controllers.Base
{
    public abstract class BaseController<TAppService, TGetViewModel, TListViewModel, TFilterParams, TCreateViewModel, TUpdateViewModel>
        : BaseController<TAppService, TGetViewModel, TListViewModel, TFilterParams, TCreateViewModel>
        where TAppService : ICRUDBaseAppService<TFilterParams, TGetViewModel, TListViewModel, TCreateViewModel, TUpdateViewModel>
    {
        protected BaseController(TAppService appService) : base(appService)
        {
        }

        /// <summary>
        /// Realiza a alteração dos dados
        /// </summary>
        /// <param name="viewModel">Informações a serem alteradas</param>
        /// <returns>Objeto que indica se ocorreram erros, uma lista de mensagens e os dados alterados</returns>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] TUpdateViewModel viewModel)
        {
            ApiResult<TUpdateViewModel> apiResult = new();

            try
            {
                var applicationResult = await UpdateAsync(viewModel);

                apiResult.Status.AdicionarMensagem(applicationResult);

                if (applicationResult.Erro)
                    return HandlerError(apiResult);

                apiResult.Data = applicationResult.Entidade;
            }
            catch (Exception ex)
            {
                return HandlerError(apiResult, ex);
            }

            return Ok(apiResult);
        }

        protected virtual async Task<Operacao<TUpdateViewModel>> UpdateAsync(TUpdateViewModel viewModel)
        {
            var operacao = CriarNovaOperacao(viewModel);

            return await AppService.UpdateAsync(operacao);
        }
    }

    public abstract class BaseController<TAppService, TGetViewModel, TListViewModel, TFilterParams, TCreateViewModel> :
        BaseController<TAppService, TGetViewModel, TListViewModel, TFilterParams>
        where TAppService : ICreateBaseAppService<TCreateViewModel>, IListBaseAppService<TListViewModel, TFilterParams>, IGetBaseAppService<TGetViewModel>, IDeleteBaseAppService, IBaseAppService
    {
        protected BaseController(TAppService appService) : base(appService)
        {
        }

        /// <summary>
        /// Realiza a inclusão das informações
        /// </summary>
        /// <param name="viewModel">Informações a ser incluídas</param>
        /// <returns>Objeto que indica se ocorreram erros, uma lista de mensagens e os dados salvos</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TCreateViewModel viewModel)
        {
            ApiResult<TCreateViewModel> apiResult = new();

            try
            {
                //Setar automaticamente o Id, caso esteja vazio
                if (viewModel is BaseViewModel idViewModel)
                    SetId(idViewModel);

                var applicationResult = await CreateAsync(viewModel);

                apiResult.Status.AdicionarMensagem(applicationResult);

                if (applicationResult.Erro)
                    return HandlerError(apiResult);

                apiResult.Data = applicationResult.Entidade;
            }
            catch (Exception ex)
            {
                return HandlerError(apiResult, ex);
            }

            return Ok(apiResult);
        }

        protected virtual void SetId(BaseViewModel viewModel)
        {
            if (viewModel.Id == Guid.Empty)
                viewModel.Id = Guid.NewGuid();
        }

        protected virtual async Task<Operacao<TCreateViewModel>> CreateAsync(TCreateViewModel viewModel)
        {
            var operacao = CriarNovaOperacao(viewModel);

            return await AppService.CreateAsync(operacao);
        }

        /// <summary>
        /// Realiza a exclusão da entidade que contém o identificador informado
        /// </summary>
        /// <param name="id">Identificador da entidade a ser excluída</param>
        /// <returns>Objeto que indica se ocorreram erros, uma lista de mensagens e o identificador da entidade excluída</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAsync(Guid id)
        {
            ApiResult<Guid> apiResult = new();

            try
            {
                var applicationResult = await DeleteAsync(id);

                apiResult.Status.AdicionarMensagem(applicationResult);

                if (applicationResult.Erro)
                    return HandlerError(apiResult);

                apiResult.Data = applicationResult.Entidade;
            }
            catch (DbUpdateException ex)
            {
                var exBase = ex.GetBaseException();
                var sqlEx = exBase as SqlException;

                if (sqlEx?.Number == Constants.NumberForeignKeyViolation)
                    return HandlerErrorForeignKeyViolation(apiResult, ex);

                return HandlerError(apiResult, ex);
            }
            catch (Exception ex)
            {
                return HandlerError(apiResult, ex);
            }

            return Ok(apiResult);
        }

        protected virtual async Task<Operacao<Guid>> DeleteAsync(Guid id)
        {
            var operacao = CriarNovaOperacao(id);

            return await AppService.DeleteAsync(operacao);
        }
    }

    public abstract class BaseController<TAppService, TGetViewModel, TListViewModel, TFilterParams>
        : BaseController<TAppService, TGetViewModel>
        where TAppService : IListBaseAppService<TListViewModel, TFilterParams>, IGetBaseAppService<TGetViewModel>, IBaseAppService
    {
        protected BaseController(TAppService appService) : base(appService)
        {
        }

        /// <summary>
        /// Executa uma pesquisa, baseada no filtro informado
        /// </summary>
        /// <param name="filterParams">Objeto para filtrar as informações</param>
        /// <returns>Objeto contendo uma lista com o resultado da pesquisa</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] TFilterParams filterParams)
        {
            ApiResult<IEnumerable<TListViewModel>> apiResult = new();

            try
            {
                var applicationResult = await ListAsync(filterParams);

                apiResult.Data = applicationResult;
            }
            catch (Exception ex)
            {
                return HandlerError(apiResult, ex);
            }

            return Ok(apiResult);
        }

        protected virtual async Task<IEnumerable<TListViewModel>> ListAsync(TFilterParams filterParams)
        {
            return await AppService.ListAsync(filterParams);
        }
    }

    public abstract class BaseController<TAppService, TGetViewModel> : BaseController<TAppService>
        where TAppService : IGetBaseAppService<TGetViewModel>, IBaseAppService
    {
        protected BaseController(TAppService appService) : base(appService)
        {
        }

        /// <summary>
        /// Realiza a busca de um único objeto. Caso não seja encontrado, retorna o código BadRequest
        /// </summary>
        /// <param name="id">Identificador da entidade a ser buscada</param>
        /// <returns>Objeto encontrado com base no identificador informado</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            ApiResult<TGetViewModel> apiResult = new();

            try
            {
                var applicationResult = await GetByIdAsync(id);

                if (applicationResult == null)
                    return NoContent();

                apiResult.Data = applicationResult;
            }
            catch (Exception ex)
            {
                return HandlerError(apiResult, ex);
            }

            return Ok(apiResult);
        }

        protected virtual async Task<TGetViewModel> GetByIdAsync(Guid id)
        {
            return await AppService.GetByIdAsync(id);
        }
    }

    public abstract class BaseController<TAppService> : BaseController
        where TAppService : IBaseAppService
    {
        protected TAppService AppService;

        public BaseController(TAppService appService)
        {
            AppService = appService;
        }
    }

    public abstract class BaseController : ControllerBase
    {
        protected virtual Operacao<T> CriarNovaOperacao<T>(T viewModel)
        {
            return new Operacao<T>(viewModel);
        }

        protected IActionResult HandlerError(ApiResult apiResult, Exception ex = null)
        {
            if (ex != null)
            {
                while (ex != null)
                {
                    apiResult.Status.AdicionarErro(ex.Message);

                    ex = ex.InnerException;
                }
            }

            return BadRequest(apiResult);
        }

        protected IActionResult HandlerErrorForeignKeyViolation(ApiResult apiResult, Exception ex = null)
        {
            if (ex != null)
                apiResult.Status.AdicionarErro(Messages.ViolacaoChaveEstrangeira);

            return BadRequest(apiResult);
        }
    }
}
