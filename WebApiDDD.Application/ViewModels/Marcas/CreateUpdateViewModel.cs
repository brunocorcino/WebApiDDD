using WebApiDDD.Application.ViewModels.Base;

namespace WebApiDDD.Application.ViewModels.Marcas
{
    public class CreateUpdateViewModel : BaseViewModel
    {
        public string Nome { get; set; }
        public int AnoFundacao { get; set; }
    }
}
