using TesteBackEnd.Business.Interfaces.Notifications;
using TesteBackEnd.Business.Interfaces.Repository;
using TesteBackEnd.Business.Interfaces.Services;
using TesteBackEnd.Business.Notifications;
using TesteBackEnd.Business.Validations;
using TesteBackEnd.Core.Models;

namespace TesteBackEnd.Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly INotificador _notificador;

        public ClienteService(IClienteRepository clienteRepository, INotificador notificador)
        {
            _clienteRepository = clienteRepository;
            _notificador = notificador;
        }

        public async Task Create(Cliente cliente)
        {
            var validation = new ClienteValidation();
            var resultValidation = await validation.ValidateAsync(cliente);

            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {
                    _notificador.AdicionarNotificacao(new Notificacao(error.ErrorMessage));
                }

                return;
            }

            await _clienteRepository.Create(cliente);
        }

        public async Task Delete(Guid id)
        {
            var clienteDb = await _clienteRepository.ReadById(id);

            if (clienteDb == null)
            {
                _notificador.AdicionarNotificacao(new Notificacao("Nenhum cliente identificado."));
                return;
            }

            await _clienteRepository.Delete(id);
        }

        public async Task Update(Cliente cliente)
        {
            var validation = new ClienteValidation();
            var resultValidation = await validation.ValidateAsync(cliente);

            if (!resultValidation.IsValid)
            {
                foreach (var error in resultValidation.Errors)
                {
                    _notificador.AdicionarNotificacao(new Notificacao(error.ErrorMessage));
                }

                return;
            }

            await _clienteRepository.Update(cliente);
        }
    }
}
