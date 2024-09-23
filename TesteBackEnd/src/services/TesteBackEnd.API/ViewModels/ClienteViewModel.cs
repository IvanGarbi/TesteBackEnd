using System.ComponentModel.DataAnnotations;
using TesteBackEnd.Core.Models.Enums;

namespace TesteBackEnd.API.ViewModels
{
    public class GetClienteViewModel
    {
        public Guid Id { get; set; }
        public string NomeEmpresa { get; set; }
        public PorteEmpresa PorteEmpresa { get; set; }
    }

    public class PostClienteViewModel
    {
        [Required(ErrorMessage = "O campo Nome da Empresa é obrigatório.")]
        public string NomeEmpresa { get; set; }

        [Required(ErrorMessage = "O campo Porte da Empresa é obrigatório.")]
        public PorteEmpresa PorteEmpresa { get; set; }
    }
}
