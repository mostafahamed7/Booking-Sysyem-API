using Domain.Contracts;
using Domain.Entites;
using System.Collections.Concurrent;

namespace Presistance.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new();
        }
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>

            => (IGenaricRepository<TEntity, TKey>)_repository.GetOrAdd(typeof(TEntity).Name, _ => new GenaricRepository<TEntity, TKey>(_dbContext));

    }
}
