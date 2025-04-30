namespace Tweeter.Core.Domain.Contracts.Persistence
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();

    }
}
