namespace WebApiDDD.Infra.CrossCutting.Common.Operacao
{
    public class Mensagem
    {
        public Mensagem(string texto, TipoMensagem tipo)
        {
            Texto = texto;
            Tipo = tipo;
        }

        public TipoMensagem Tipo { get; set; }
        public string Texto { get; set; }
    }
}
