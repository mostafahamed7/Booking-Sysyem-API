using Domain;
using Domain.Contracts;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Presistance.Data.Repositories
{
    public class GenaricRepository<TEntity, TKey> : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenaricRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity?>> GetAllAsync(bool asNoTracking = false, Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<TEntity?> GetByIdAsync(Specification<TEntity> specification)
            => await SpecificationEvaluator.GetQuery(_dbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(Specification<TEntity> specification)
            => await SpecificationEvaluator.GetQuery(_dbContext.Set<TEntity>(), specification).ToListAsync();

        public IQueryable<TEntity> GetQueryable()
            => _dbSet.AsQueryable();
    }
}
