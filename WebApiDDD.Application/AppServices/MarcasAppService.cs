using System.Linq.Expressions;
using WebApiDDD.Application.AppServices.Base;
using WebApiDDD.Application.Interfaces;
using WebApiDDD.Application.ViewModels.Marcas;
using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Domain.Interfaces.Services;
using WebApiDDD.Domain.Models;
using WebApiDDD.Infra.CrossCutting.Common.Operacao;

namespace WebApiDDD.Application.AppServices
{
    public class MarcasAppService : BaseAppService<IMarcasService, Marcas, MarcasFilterParams, GetViewModel, ListViewModel, CreateUpdateViewModel, CreateUpdateViewModel>, IMarcasAppService
    {
        public MarcasAppService(IMarcasService service) : base(service)
        {
        }

        public async Task<int> GetQuantidadeCarrosAsync(Guid id)
        {
            var result = await Service.ListAsync(new MarcasFilterParams { Id = id }, x => x.Carros.Count);

            return result.FirstOrDefault();
        }

        protected override async Task<Marcas> MapToCreateModelAsync(Operacao<CreateUpdateViewModel> operacao)
        {
            var entidade = operacao.Entidade;

            return await Task.Run(() => new Marcas
            {
                Id = entidade.Id,
                Nome = entidade.Nome,
                AnoFundacao = entidade.AnoFundacao
            });
        }

        protected override async Task<GetViewModel> MapToGetViewModelAsync(Marcas model)
        {
            return await Task.Run(() => new GetViewModel
            {
                Id = model.Id,
                Nome = model.Nome,
                AnoFundacao = model.AnoFundacao
            });
        }

        protected override async Task MapToUpdateModelAsync(Operacao<CreateUpdateViewModel> operacao, Marcas model)
        {
            await Task.Run(() =>
            {
                var entidade = operacao.Entidade;

                model.AnoFundacao = entidade.AnoFundacao;
                model.Nome = entidade.Nome;
            });
        }

        protected override Expression<Func<Marcas, ListViewModel>> SelectToListViewModel()
        {
            return (model) => new ListViewModel
            {
                Id = model.Id,
                Nome = model.Nome
            };
        }
    }
}
