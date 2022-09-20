namespace WebApiDDD.Infra.CrossCutting.Common.Operacao
{
    public class Operacao<TEntidade> : ActionReturn
    {
        public Operacao()
        {
        }

        public Operacao(TEntidade entidade) : this()
        {
            Entidade = entidade;
        }

        public TEntidade Entidade { get; set; }

        public void AdicionarMensagem(Operacao<TEntidade> operacao)
        {
            foreach (var msg in operacao.Mensagens)
            {
                AdicionarMensagem(msg.Texto, msg.Tipo);
            }
        }

        public ActionReturn ValidarPropriedadesObrigatorias()
        {
            var actionReturn = new ActionReturn();

            if (Entidade == null)
                actionReturn.AdicionarErro(string.Format(Helper.Messages.CampoObrigatorio, "Entidade"));

            return actionReturn;
        }

        public Operacao<TNovaEntidade> CopiarOperacao<TNovaEntidade>(dynamic entidade)
        {
            Operacao<TNovaEntidade> novaOperacao = new()
            {
                Entidade = entidade
            };

            return novaOperacao;
        }
    }
}
