namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        // Set
        public Task SetAsync(string key, object value, TimeSpan duration);

        // Get
        public Task<string?> GetAsync(string key);
    }
}
