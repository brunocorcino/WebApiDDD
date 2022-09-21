using System.ComponentModel.DataAnnotations;
using WebApiDDD.Application.ViewModels.Base;

namespace WebApiDDD.Application.ViewModels.Carros
{
    public class CreateUpdateViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "O campo Marca é obrigatório")]
        public Guid IdMarca { get; set; }

        [Required(ErrorMessage = "O campo Modelo é obrigatório")]
        public string Modelo { get; set; }

        [Range(1800, 2200, ErrorMessage = "O campo Ano fabricação não pode ser menor que 1800 ou maior que 2200")]
        public int AnoFabricacao { get; set; }

        [Range(1800, 2200, ErrorMessage = "O campo Ano modelo não pode ser menor que 1800 ou maior que 2200")]
        public int AnoModelo { get; set; }

        [Range(2, 4, ErrorMessage = "O campo Quantidade de portas não pode ser menor que 2 ou maior que 4")]
        public int QuantidadePortas { get; set; }

        public bool Automatico { get; set; }
    }
}
