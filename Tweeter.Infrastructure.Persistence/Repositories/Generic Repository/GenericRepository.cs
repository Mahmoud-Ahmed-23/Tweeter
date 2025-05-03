using Microsoft.EntityFrameworkCore;
using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Infrastructure.Persistence._Data;

namespace Tweeter.Infrastructure.Persistence.Repositories.Generic_Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
       where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>

    {
        private readonly TweeterDbContext _dbContext;

        public GenericRepository(TweeterDbContext dbContext)
        {
            _dbContext = dbContext;
        }




        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTraching = false)
        {
            return WithTraching ? await _dbContext.Set<TEntity>().ToListAsync() : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }










        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }


        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
    }

}
