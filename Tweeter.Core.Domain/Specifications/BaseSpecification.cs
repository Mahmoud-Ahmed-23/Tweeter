using System.Linq.Expressions;
using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Contracts.Specifications;

namespace Tweeter.Core.Domain.Specifications
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey>
        where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();

        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>>? OrderByDesending { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification()
        {
        }
        private protected virtual void AddIncludes()
        {

        }
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecification(TKey id)
        {
            Criteria = x => x.Id.Equals(id);

        }

        private protected void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        private protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
        {
            OrderByDesending = orderByDescending;
        }
        private protected void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;

            Skip = skip;
            Take = take;
        }
    }
}
