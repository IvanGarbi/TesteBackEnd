using TesteBackEnd.Core.Models.Base;
using TesteBackEnd.Core.Models.Enums;

namespace TesteBackEnd.Core.Models
{
    public class Cliente : Entity
    {
        public string NomeEmpresa { get; set; }
        public PorteEmpresa PorteEmpresa { get; set; }
    }
}
