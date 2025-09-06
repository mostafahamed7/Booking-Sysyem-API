using Domain.Contracts;
using Domain.Entites;
using StackExchange.Redis;
using System.Text.Json;

namespace Presistance.Data.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
        => await _database.KeyDeleteAsync(id);

        public async Task<Basket?> GetBasketAsync(string id)
        {
            var value = await _database.StringGetAsync(id);
            if (value.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<Basket>(value);
        }

        public async Task<Basket?> UpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null)
        {
            var JsonData = JsonSerializer.Serialize(basket);
            var isCreated = await _database.StringSetAsync(basket.Id, JsonData,
                timeToLive ?? TimeSpan.FromDays(30));
            return isCreated ? await GetBasketAsync(basket.Id) : null;
        }
    }
}
