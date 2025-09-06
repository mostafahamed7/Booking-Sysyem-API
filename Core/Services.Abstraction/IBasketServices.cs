using Shared.DTOs;

namespace Services.Abstraction
{
    public interface IBasketServices
    {
        // Get Basket
        public Task<BasketDTO?> GetBasketAsync(string id);

        // Delete Basket
        public Task<bool> DeleteBasketAsync(string id);

        // Update Basket
        public Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket);
    }
}
