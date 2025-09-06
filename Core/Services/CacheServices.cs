using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    internal class CacheServices(ICacheRepository cacheRepository) : ICacheServices
    {
        public async Task<string?> GetCacheValueAsync(string key)
        {
            var value = await cacheRepository.GetAsync(key);

            return value  == null ? null : value;
        }

        public async Task SetCacheValueAsync(string key, object value, TimeSpan duration)
        {
            await cacheRepository.SetAsync(key, value, duration);
        }
    }
}
