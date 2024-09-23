using System.Linq.Expressions;
using TesteBackEnd.Core.Models.Base;

namespace TesteBackEnd.Business.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task<TEntity> ReadById(Guid id);
        Task<IEnumerable<TEntity>> Read();
        Task<IEnumerable<TEntity>> ReadExpression(Expression<Func<TEntity, bool>> predicateExpression);
        Task<int> SaveChanges();
    }
}
