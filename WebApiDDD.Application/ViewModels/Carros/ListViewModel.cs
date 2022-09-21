using WebApiDDD.Application.ViewModels.Base;

namespace WebApiDDD.Application.ViewModels.Carros
{
    public class ListViewModel : BaseViewModel
    {
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int AnoModelo { get; set; }
        public int AnoFabricacao { get; set; }
        public bool Automatico { get; set; }
    }
}
