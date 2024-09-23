using FluentValidation;
using TesteBackEnd.Core.Models;

namespace TesteBackEnd.Business.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(x => x.PorteEmpresa)
                .IsInEnum();

            RuleFor(x => x.NomeEmpresa)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Nome da Empresa deve ser informado.")
                .MaximumLength(100)
                .WithMessage("O Nome da Empresa deve ter menos que 100 caracteres.");

        }
    }
}
