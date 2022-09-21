using System.Linq.Expressions;
using WebApiDDD.Application.AppServices.Base;
using WebApiDDD.Application.Interfaces;
using WebApiDDD.Application.ViewModels.Carros;
using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Domain.Interfaces.Services;
using WebApiDDD.Domain.Models;
using WebApiDDD.Infra.CrossCutting.Common.Operacao;

namespace WebApiDDD.Application.AppServices
{
    public class CarrosAppService : BaseAppService<ICarrosService, Carros, CarrosFilterParams, GetViewModel, ListViewModel, CreateUpdateViewModel, CreateUpdateViewModel>, ICarrosAppService
    {
        public CarrosAppService(ICarrosService service) : base(service)
        {
        }

        protected override async Task<Carros> MapToCreateModelAsync(Operacao<CreateUpdateViewModel> operacao)
        {
            var entidade = operacao.Entidade;

            return await Task.Run(() => new Carros
            {
                Id = entidade.Id,
                IdMarca = entidade.IdMarca,
                Modelo = entidade.Modelo,
                AnoModelo = entidade.AnoModelo,
                AnoFabricacao = entidade.AnoFabricacao,
                Automatico = entidade.Automatico,
                QuantidadePortas = entidade.QuantidadePortas
            });
        }

        protected override async Task<GetViewModel> MapToGetViewModelAsync(Carros model)
        {
            return await Task.Run(() => new GetViewModel
            {
                Id = model.Id,
                QuantidadePortas = model.QuantidadePortas,
                AnoFabricacao = model.AnoFabricacao,
                AnoModelo = model.AnoModelo,
                Automatico = model.Automatico,
                Modelo = model.Modelo,
                IdMarca = model.IdMarca
            });
        }

        protected override async Task MapToUpdateModelAsync(Operacao<CreateUpdateViewModel> operacao, Carros model)
        {
            await Task.Run(() =>
            {
                var entidade = operacao.Entidade;

                model.QuantidadePortas = entidade.QuantidadePortas;
                model.AnoFabricacao = entidade.AnoFabricacao;
                model.AnoModelo = entidade.AnoModelo;
                model.IdMarca = entidade.IdMarca;
                model.Modelo = entidade.Modelo;
                model.Automatico = entidade.Automatico;
            });
        }

        protected override Expression<Func<Carros, ListViewModel>> SelectToListViewModel()
        {
            return (model) => new ListViewModel
            {
                Id = model.Id,
                Modelo = model.Modelo,
                Marca = model.Marca.Nome,
                Automatico = model.Automatico,
                AnoModelo = model.AnoModelo,
                AnoFabricacao = model.AnoFabricacao
            };
        }
    }
}
