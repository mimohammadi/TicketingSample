using Domain.Models;
using System.Linq.Expressions;

namespace Domain
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll(int skip, int take, Expression<Func<TEntity, bool>> prediction = null);
        Task<TEntity> Get(Guid id);
        Task<TEntity> GetByPrediction(Expression<Func<TEntity, bool>> prediction);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task Remove(TEntity entity);
        Task SaveChages();
    }
}
