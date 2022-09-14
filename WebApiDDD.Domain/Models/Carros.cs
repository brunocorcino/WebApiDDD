using WebApiDDD.Domain.Models.Base;

namespace WebApiDDD.Domain.Models
{
    public class Carros : BaseModel
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public int QuantidadePortas { get; set; }
        public bool Automatico { get; set; }
    }
}
