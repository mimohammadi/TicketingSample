using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity,
        IAggregateRoot
    {
        private readonly DataBaseContext _context;

        public Repository(DataBaseContext dataBaseContext)
        {
            _context = dataBaseContext;
        }

        /// <summary>
        /// soft delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(Guid id) 
        {            
            var val = await _context.Set<TEntity>().SingleOrDefaultAsync(x=>x.Id == id);
            if (val == null) throw new ArgumentException("شی یافت نشد");
            val.Delete();
            _context.Update(val);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAll(int skip, int take, Expression<Func<TEntity, bool>> prediction = null)
        {
            var entities = _context.Set<TEntity>().AsNoTracking().AsQueryable();

            if(prediction != null)
                entities = entities.Where(prediction).AsQueryable();

            return await entities.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<TEntity> GetByPrediction(Expression<Func<TEntity, bool>> prediction)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(prediction);
        }

        public async Task Insert(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(); //
        }

        public async Task SaveChages()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
