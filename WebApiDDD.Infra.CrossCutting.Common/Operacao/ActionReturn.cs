namespace WebApiDDD.Infra.CrossCutting.Common.Operacao
{
    public class ActionReturn
    {
        private readonly List<Mensagem> _mensagens;

        public ActionReturn()
        {
            _mensagens = new List<Mensagem>();
        }

        public IReadOnlyCollection<Mensagem> Mensagens => _mensagens;

        public bool Erro => _mensagens.Any(x => x.Tipo == TipoMensagem.Erro);

        public void AdicionarMensagem(string texto, TipoMensagem tipo)
        {
            _mensagens.Add(new Mensagem(texto, tipo));
        }

        public void AdicionarMensagem(IEnumerable<Mensagem> mensagens)
        {
            foreach (var msg in mensagens)
            {
                AdicionarMensagem(msg.Texto, msg.Tipo);
            }
        }

        public void AdicionarMensagem(ActionReturn actionReturn)
        {
            foreach (var msg in actionReturn.Mensagens)
            {
                AdicionarMensagem(msg.Texto, msg.Tipo);
            }
        }

        public void AdicionarErro(string texto)
        {
            AdicionarMensagem(texto, TipoMensagem.Erro);
        }
    }
}
