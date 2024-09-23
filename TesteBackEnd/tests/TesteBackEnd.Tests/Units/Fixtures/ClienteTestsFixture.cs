using TesteBackEnd.Core.Models.Enums;

namespace TesteBackEnd.Tests.Units.Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    { }

    public class ClienteTestsFixture : IDisposable
    {
        public Core.Models.Cliente ClienteValido()
        {
            var cliente = new Core.Models.Cliente
            {
                Id = Guid.NewGuid(),
                NomeEmpresa = "Empresa Legal",
                PorteEmpresa = PorteEmpresa.Pequena,
            };

            return cliente;
        }

        public Core.Models.Cliente ClienteInvalido()
        {
            var cliente = new Core.Models.Cliente
            {
                Id = Guid.NewGuid(),
                NomeEmpresa = string.Empty,
                PorteEmpresa = PorteEmpresa.Pequena,
            };

            return cliente;
        }

        public void Dispose()
        {
        }
    }
}
