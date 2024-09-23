using TesteBackEnd.Business.Interfaces.Repository;
using TesteBackEnd.Core.Models;
using TesteBackEnd.Data.Context;

namespace TesteBackEnd.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(TesteDbContext db) : base(db)
        {
        }
    }
}
