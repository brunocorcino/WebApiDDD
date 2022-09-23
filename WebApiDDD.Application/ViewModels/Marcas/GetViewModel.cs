using WebApiDDD.Application.ViewModels.Base;

namespace WebApiDDD.Application.ViewModels.Marcas
{
    public class GetViewModel : BaseViewModel
    {
        public string Nome { get; set; }
        public int AnoFundacao { get; set; }
    }
}
