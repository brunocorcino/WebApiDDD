using WebApiDDD.Domain.Models.Base;

namespace WebApiDDD.Domain.Models
{
    public class Marcas : BaseModel
    {
        public string Nome { get; set; }
        public int AnoFundacao { get; set; }

        public virtual ICollection<Carros> Carros { get; set; }
    }
}
