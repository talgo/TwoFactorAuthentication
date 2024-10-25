namespace TwoFactorAuthentication.Application.Interfaces
{
    public interface IRedisService
    {
        Task<T> GetAsync<T>(string key) where T : class;
        Task AddOrUpdateAsync<T>(string key, T value, TimeSpan? cacheExpiration = null) where T : class;
        Task RemoveAsync(string key);
        Task<TimeSpan?> GetExpirationAsync(string key);
    }
}
