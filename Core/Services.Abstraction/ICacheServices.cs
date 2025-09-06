namespace Services.Abstractions
{
    public interface ICacheServices
    {
        public Task SetCacheValueAsync(string key, object value, TimeSpan duration);

        public Task<string?> GetCacheValueAsync(string key);
    }
}
