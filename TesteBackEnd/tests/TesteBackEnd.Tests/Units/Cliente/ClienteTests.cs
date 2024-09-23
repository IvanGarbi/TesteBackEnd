using TesteBackEnd.Business.Validations;
using TesteBackEnd.Tests.Units.Fixtures;

namespace TesteBackEnd.Tests.Units.Cliente
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTests(ClienteTestsFixture articleTestsFixture)
        {
            _clienteTestsFixture = articleTestsFixture;
        }

        [Fact(DisplayName = "New Cliente Valid")]
        [Trait("Category", "Cliente Tests Fixture")]
        public void Cliente_NewCliente_MustBeValid()
        {
            // Arrange
            var cliente = _clienteTestsFixture.ClienteValido();

            // Act
            var result = new ClienteValidation().Validate(cliente);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "New Cliente Invalid")]
        [Trait("Category", "Cliente Tests Fixture")]
        public void Cliente_NewCliente_MustBeInvalid()
        {
            // Arrange
            var cliente = _clienteTestsFixture.ClienteInvalido();

            // Act
            var result = new ClienteValidation().Validate(cliente);

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
        }
    }
}
