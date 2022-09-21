using WebApiDDD.Application.ViewModels.Base;

namespace WebApiDDD.Application.ViewModels.Carros
{
    public class GetViewModel : BaseViewModel
    {
        public Guid IdMarca { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public int QuantidadePortas { get; set; }
        public bool Automatico { get; set; }
    }
}
