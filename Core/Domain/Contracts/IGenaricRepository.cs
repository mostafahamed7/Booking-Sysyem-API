using Domain.Entites;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<IEnumerable<TEntity?>> GetAllAsync(bool asNoTracking = false, Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

        public Task<TEntity?> GetByIdAsync(TKey id);

        public Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        public Task<TEntity?> GetByIdAsync(Specification<TEntity> specification);
        public Task<IEnumerable<TEntity>> GetAllAsync(Specification<TEntity> specification);

        IQueryable<TEntity> GetQueryable();
    }
}
