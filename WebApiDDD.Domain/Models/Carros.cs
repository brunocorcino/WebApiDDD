using WebApiDDD.Domain.Models.Base;

namespace WebApiDDD.Domain.Models
{
    public class Carros : BaseModel
    {
        public Guid IdMarca { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public int QuantidadePortas { get; set; }
        public bool Automatico { get; set; }

        public virtual Marcas Marca { get; set; }
    }
}
