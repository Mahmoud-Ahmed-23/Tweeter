using Tweeter.Core.Domain.Common;

namespace Tweeter.Core.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity, TKey>
       where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTraching = false);

        Task<TEntity?> GetAsync(TKey id);


        IQueryable<TEntity> GetQueryable();



        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
