using System.Collections.Concurrent;
using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Infrastructure.Persistence._Data;
using Tweeter.Infrastructure.Persistence.Repositories.Generic_Repository;

namespace Tweeter.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TweeterDbContext _tweeterDbContext;

        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(TweeterDbContext tweeterDbContext)
        {
            _tweeterDbContext = tweeterDbContext;
            _repositories = new ConcurrentDictionary<string, object>();
        }


        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
            where TEntity : BaseEntity<Tkey>
            where Tkey : IEquatable<Tkey>
        {
            return (IGenericRepository<TEntity, Tkey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, Tkey>(_tweeterDbContext));
        }


        public async Task<int> CompleteAsync()
        {
            return await _tweeterDbContext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _tweeterDbContext.DisposeAsync();
        }
    }
}
