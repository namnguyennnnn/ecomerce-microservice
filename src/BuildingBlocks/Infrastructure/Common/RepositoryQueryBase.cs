using Contracts.Common;
using Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Common
{
    public class RepositoryQueryBase<T,K,TContext> : IRepositoryQueryBase<T,K ,TContext> where T :EntityBase<K> where TContext:DbContext 
    {
        private readonly TContext _dbContext;

        public RepositoryQueryBase(TContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IQueryable<T> FindAll(bool trackingChanges = false) =>
           !trackingChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        public IQueryable<T> FindAll(bool trackingChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackingChanges);
            items = includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges = false) =>
            !trackingChanges
                ? _dbContext.Set<T>().Where(expression).AsNoTracking()
                : _dbContext.Set<T>().Where(expression);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackingChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackingChanges);
            items = includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
            return items;
        }

        public async Task<T> GetByIdAsync(K id) =>
            await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task<T> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
            await FindByCondition(x => x.Id.Equals(id), trackingChanges: false, includeProperties).FirstOrDefaultAsync();
    }
}
           