using WebApiDDD.Domain.FilterParams;
using WebApiDDD.Domain.Interfaces.Repositories;
using WebApiDDD.Domain.Interfaces.Services;
using WebApiDDD.Domain.Models;
using WebApiDDD.Domain.Services.Base;
using WebApiDDD.Infra.CrossCutting.Common.Helper;
using WebApiDDD.Infra.CrossCutting.Common.Operacao;

namespace WebApiDDD.Domain.Services
{
    public class MarcasService : BaseService<Marcas, MarcasFilterParams, IMarcasRepository>, IMarcasService
    {
        public MarcasService(IMarcasRepository repository) : base(repository)
        {
        }

        protected override async Task OnBeforeCreatedAsync(object sender, Operacao<Marcas> operacao)
        {
            await Task.Run(() =>
            {
                operacao.AdicionarMensagem(ValidarDuplicidade(operacao.Entidade));
            });
        }

        protected override async Task OnBeforeUpdatedAsync(object sender, Operacao<Marcas> operacao)
        {
            await Task.Run(() =>
            {
                operacao.AdicionarMensagem(ValidarDuplicidade(operacao.Entidade));
            });
        }

        private ActionReturn ValidarDuplicidade(Marcas marca)
        {
            ActionReturn result = new();

            var existeRegistro = Repository
                .Query(new MarcasFilterParams { NomeExato = marca.Nome })
                .Where(x => x.Id != marca.Id)
                .Any();

            if (existeRegistro)
                result.AdicionarErro(string.Format(Messages.RegistrosDuplicados, "Marcas"));

            return result;
        }
    }
}
