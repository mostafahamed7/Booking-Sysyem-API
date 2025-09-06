using Domain.Entites;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync();

        IGenaricRepository<TEntity, TKey>GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
