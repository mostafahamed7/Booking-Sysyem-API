namespace Domain.Contracts
{
    public interface IDbIntializer
    {
        public Task InitializeIdentityAsync();
    }
}
