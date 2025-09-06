using Domain.Entites;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        public Task<Basket?> GetBasketAsync(string id);
        // Delete Basket
        public Task<bool> DeleteBasketAsync(string id);
        // Update Basket
        public Task<Basket?> UpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null);
    }
}
