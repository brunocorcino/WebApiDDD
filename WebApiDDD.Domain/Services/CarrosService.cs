using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Domain.Interfaces.Repositories;
using WebApiDDD.Domain.Interfaces.Services;
using WebApiDDD.Domain.Models;
using WebApiDDD.Domain.Services.Base;
using WebApiDDD.Infra.CrossCutting.Common.Operacao;

namespace WebApiDDD.Domain.Services
{
    public class CarrosService : BaseService<Carros, CarrosFilterParams, ICarrosRepository>, ICarrosService
    {
        public CarrosService(ICarrosRepository repository) : base(repository)
        {
        }

        protected override async Task OnBeforeCreatedAsync(object sender, Operacao<Carros> operacao)
        {
            await Task.Run(() =>
            {
                operacao.AdicionarMensagem(ValidarAnoFabricacaoMaiorQueAnoModelo(operacao.Entidade));
            });
        }

        protected override async Task OnBeforeUpdatedAsync(object sender, Operacao<Carros> operacao)
        {
            await Task.Run(() =>
            {
                operacao.AdicionarMensagem(ValidarAnoFabricacaoMaiorQueAnoModelo(operacao.Entidade));
            });
        }

        private static ActionReturn ValidarAnoFabricacaoMaiorQueAnoModelo(Carros carro)
        {
            ActionReturn result = new();

            if (carro.AnoFabricacao > carro.AnoModelo)
                result.AdicionarErro("O ano fabricação não pode ser maior que o ano modelo");

            return result;
        }
    }
}
