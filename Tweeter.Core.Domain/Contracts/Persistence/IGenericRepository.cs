using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Contracts.Specifications;

namespace Tweeter.Core.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity, TKey>
       where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTraching = false);

        Task<TEntity?> GetAsync(TKey id);


        IQueryable<TEntity> GetQueryable();

        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> Spec, bool WithTraching = false);

        Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> spec);

        Task<int> GetCountAsync(ISpecification<TEntity, TKey> spec);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
