using Microsoft.EntityFrameworkCore;
using Moq;
using TesteBackEnd.Business.Interfaces.Repository;
using TesteBackEnd.Business.Notifications;
using TesteBackEnd.Business.Services;
using TesteBackEnd.Data.Context;
using TesteBackEnd.Data.Repository;
using TesteBackEnd.Tests.Units.Fixtures;

namespace TesteBackEnd.Tests.Units.Cliente
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteServiceTests(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }


        [Fact(DisplayName = "Novo Cliente Com Sucesso")]
        [Trait("Category", "Cliente Service Tests")]
        public async void ClienteService_NovoCliente_ExecuteComSucesso()
        {
            //Arrange  
            var cliente = _clienteTestsFixture.ClienteValido();

            var options = new DbContextOptionsBuilder<TesteDbContext>()
                .UseInMemoryDatabase("TesteDb")
                .Options;
            TesteDbContext db = new TesteDbContext(options);

            var notificador = new Mock<Notificador>();


            ClienteRepository clienteRepository = new ClienteRepository(db);

            // Act
            ClienteService clienteService = new ClienteService(new ClienteRepository(db), notificador.Object);

            try
            {
                await clienteService.Create(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var result = await clienteRepository.Read();
            var listaNotificacao = notificador.Object.GetNotificacoes();
            var temNotificacao = notificador.Object.TemNotificacao();

            //Assert  
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Empty(listaNotificacao);
            Assert.False(temNotificacao);
        }
        
        [Fact(DisplayName = "Novo Cliente Como Sucesso")]
        [Trait("Category", "Cliente Service Tests")]
        public async void ClienteService_NovoCliente_ExecuteComoutSucesso()
        {
            //Arrange  
            var cliente = _clienteTestsFixture.ClienteInvalido();

            var options = new DbContextOptionsBuilder<TesteDbContext>()
                .UseInMemoryDatabase("TesteDb2")
                .Options;
            TesteDbContext db = new TesteDbContext(options);

            var notificador = new Mock<Notificador>();


            ClienteRepository clienteRepository = new ClienteRepository(db);

            // Act
            ClienteService clienteService = new ClienteService(new ClienteRepository(db), notificador.Object);

            try
            {
                await clienteService.Create(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var result = await clienteRepository.Read();
            var listaNotificacao = notificador.Object.GetNotificacoes();
            var temNotificacao = notificador.Object.TemNotificacao();

            //Assert  
            Assert.Empty(result);
            Assert.NotEmpty(listaNotificacao);
            Assert.Single(listaNotificacao);
            Assert.True(temNotificacao);
        }


        [Fact(DisplayName = "Update Cliente Com Sucesso")]
        [Trait("Category", "Cliente Service Tests")]
        public async void ClienteService_Update_ExecuteComSucesso()
        {
            //Arrange  
            var cliente = _clienteTestsFixture.ClienteValido();

            var options = new DbContextOptionsBuilder<TesteDbContext>()
                .UseInMemoryDatabase("TesteDb3")
                .Options;
            TesteDbContext db = new TesteDbContext(options);

            var notificador = new Mock<Notificador>();
            var notificadorAtual = new Mock<Notificador>();

            ClienteRepository clienteRepository = new ClienteRepository(db);

            // Act
            ClienteService clienteService = new ClienteService(new ClienteRepository(db), notificador.Object);

            try
            {
                await clienteService.Create(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var result = await clienteRepository.ReadById(cliente.Id);
            var listaNotificacao = notificador.Object.GetNotificacoes();
            var temNotificacao = notificador.Object.TemNotificacao();

            var clienteAtual = cliente;
            clienteAtual.PorteEmpresa = Core.Models.Enums.PorteEmpresa.Grande;
            clienteAtual.NomeEmpresa = "Nome Atualizado";

            ClienteService clienteServiceUpdate = new ClienteService(new ClienteRepository(db), notificadorAtual.Object);

            try
            {
                await clienteServiceUpdate.Update(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var listaNotificacaoAtual = notificadorAtual.Object.GetNotificacoes();
            var resultAtual = await clienteRepository.ReadById(clienteAtual.Id);
            var temNotificacaoAtual = notificadorAtual.Object.TemNotificacao();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultAtual);
            Assert.Equal("Nome Atualizado", resultAtual.NomeEmpresa);
            Assert.Equal(Core.Models.Enums.PorteEmpresa.Grande, resultAtual.PorteEmpresa);
            Assert.Empty(listaNotificacao);
            Assert.Empty(listaNotificacaoAtual);
            Assert.False(temNotificacao);
            Assert.False(temNotificacaoAtual);
        }

        [Fact(DisplayName = "Update Cliente Como Sucesso")]
        [Trait("Category", "Cliente Service Tests")]
        public async void ClienteService_Update_ExecuteComoutSucesso()
        {
            //Arrange  
            var cliente = _clienteTestsFixture.ClienteValido();

            var options = new DbContextOptionsBuilder<TesteDbContext>()
                .UseInMemoryDatabase("TesteDb4")
                .Options;
            TesteDbContext db = new TesteDbContext(options);

            var notificador = new Mock<Notificador>();
            var notificadorAtual = new Mock<Notificador>();

            ClienteRepository clienteRepository = new ClienteRepository(db);

            // Act
            ClienteService clienteService = new ClienteService(new ClienteRepository(db), notificador.Object);

            try
            {
                await clienteService.Create(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var result = await clienteRepository.ReadById(cliente.Id);
            var listaNotificacao = notificador.Object.GetNotificacoes();
            var temNotificacao = notificador.Object.TemNotificacao();

            var clienteAtual = cliente;
            clienteAtual.PorteEmpresa = Core.Models.Enums.PorteEmpresa.Grande;
            clienteAtual.NomeEmpresa = string.Empty;

            ClienteService clienteServiceUpdate = new ClienteService(new ClienteRepository(db), notificadorAtual.Object);

            try
            {
                await clienteServiceUpdate.Update(clienteAtual);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var listaNotificacaoAtual = notificadorAtual.Object.GetNotificacoes();
            var resultAtual = await clienteRepository.ReadById(clienteAtual.Id);
            var temNotificacaoAtual = notificadorAtual.Object.TemNotificacao();

            //Assert  
            Assert.NotNull(result);
            Assert.NotNull(resultAtual);
            Assert.NotEqual(string.Empty, resultAtual.NomeEmpresa);
            Assert.NotEqual(Core.Models.Enums.PorteEmpresa.Grande, resultAtual.PorteEmpresa);
            Assert.Empty(listaNotificacao);
            Assert.NotEmpty(listaNotificacaoAtual);
            Assert.Single(listaNotificacaoAtual);
            Assert.False(temNotificacao);
            Assert.True(temNotificacaoAtual);
        }


        [Fact(DisplayName = "Delete Cliente Com Sucesso")]
        [Trait("Category", "Cliente Service Tests")]
        public async void ClienteService_Delete_ExecuteComSucesso()
        {
            //Arrange  
            var cliente = _clienteTestsFixture.ClienteValido();

            var options = new DbContextOptionsBuilder<TesteDbContext>()
                .UseInMemoryDatabase("TesteDb5")
                .Options;
            TesteDbContext db = new TesteDbContext(options);

            var notificador = new Mock<Notificador>();
            var notificadorAtual = new Mock<Notificador>();

            ClienteRepository clienteRepository = new ClienteRepository(db);

            // Act
            ClienteService clienteService = new ClienteService(new ClienteRepository(db), notificador.Object);

            try
            {
                await clienteService.Create(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var result = await clienteRepository.ReadById(cliente.Id);
            var listaNotificacao = notificador.Object.GetNotificacoes();
            var temNotificacao = notificador.Object.TemNotificacao();

            ClienteService clienteServiceUpdate = new ClienteService(new ClienteRepository(db), notificadorAtual.Object);

            try
            {
                db.Entry(cliente).State = EntityState.Detached;

                await clienteServiceUpdate.Delete(cliente.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var listaNotificacaoAtual = notificadorAtual.Object.GetNotificacoes();
            var resultAtual = await clienteRepository.Read();
            var temNotificacaoAtual = notificadorAtual.Object.TemNotificacao();

            //Assert  
            Assert.NotNull(result);
            Assert.Empty(resultAtual);
            Assert.Empty(listaNotificacao);
            Assert.Empty(listaNotificacaoAtual);
            Assert.False(temNotificacao);
            Assert.False(temNotificacaoAtual);
        }

        [Fact(DisplayName = "Delete Cliente Como Sucesso")]
        [Trait("Category", "Cliente Service Tests")]
        public async void ClienteService_Delete_ExecuteComoutSucesso()
        {
            //Arrange  
            var cliente = _clienteTestsFixture.ClienteValido();

            var options = new DbContextOptionsBuilder<TesteDbContext>()
                .UseInMemoryDatabase("TesteDb6")
                .Options;
            TesteDbContext db = new TesteDbContext(options);

            var notificador = new Mock<Notificador>();
            var notificadorAtual = new Mock<Notificador>();

            ClienteRepository clienteRepository = new ClienteRepository(db);

            // Act
            ClienteService clienteService = new ClienteService(new ClienteRepository(db), notificador.Object);

            try
            {
                await clienteService.Create(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var result = await clienteRepository.ReadById(cliente.Id);
            var listaNotificacao = notificador.Object.GetNotificacoes();
            var temNotificacao = notificador.Object.TemNotificacao();

            ClienteService clienteServiceUpdate = new ClienteService(new ClienteRepository(db), notificadorAtual.Object);

            try
            {
                db.Entry(cliente).State = EntityState.Detached;

                await clienteServiceUpdate.Delete(Guid.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                db.Dispose();
                throw;
            }

            var listaNotificacoesAtualizado = notificadorAtual.Object.GetNotificacoes();
            var resultAtual = await clienteRepository.Read();
            var temNotificacaoAtual = notificadorAtual.Object.TemNotificacao();

            //Assert  
            Assert.NotNull(result);
            Assert.NotEmpty(resultAtual);
            Assert.Empty(listaNotificacao);
            Assert.NotEmpty(listaNotificacoesAtualizado);
            Assert.Single(listaNotificacoesAtualizado);
            Assert.False(temNotificacao);
            Assert.True(temNotificacaoAtual);
        }

        
    }
}
