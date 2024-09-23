using TesteBackEnd.Core.Models;

namespace TesteBackEnd.Business.Interfaces.Services
{
    public interface IClienteService
    {
        Task Create(Cliente cliente);
        Task Update(Cliente cliente);
        Task Delete(Guid id);
    }
}
