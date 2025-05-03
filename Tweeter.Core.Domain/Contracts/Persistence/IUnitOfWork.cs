using Tweeter.Core.Domain.Common;

namespace Tweeter.Core.Domain.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
            where Tkey : IEquatable<Tkey>;

        Task<int> CompleteAsync();

    }
}
