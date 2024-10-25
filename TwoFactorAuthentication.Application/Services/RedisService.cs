using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwoFactorAuthentication.Application.Interfaces;

namespace TwoFactorAuthentication.Application.Services
{
    public class RedisService : IRedisService
    {
        private readonly IConfiguration _configuration;
        private readonly ConfigurationOptions _configurationOptions;
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(IConfiguration configuration)
        {
            _configuration = configuration;
            _configurationOptions = new ConfigurationOptions()
            {
                ConnectTimeout = 100000,
                SyncTimeout = 100000,
                AbortOnConnectFail = false,
                DefaultDatabase = 0
            };
            _configurationOptions.EndPoints.Add(_configuration.GetConnectionString("Redis"));
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_configurationOptions);
        }

        public async Task AddOrUpdateAsync<T>(string key, T value, TimeSpan? cacheExpiration = null) where T : class
        {
            RedisKey redisKey = new RedisKey(key.ToLowerInvariant());
            RedisValue redisValue = new RedisValue(JsonConvert.SerializeObject(value));
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(redisKey, redisValue, cacheExpiration);
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            RedisKey redisKey = new RedisKey(key.ToLowerInvariant());
            var db = _connectionMultiplexer.GetDatabase();
            RedisValue redisValue = await db.StringGetAsync(redisKey);
            if (string.IsNullOrEmpty(redisValue))
                return null;

            return JsonConvert.DeserializeObject<T>(redisValue);
        }

        public async Task<TimeSpan?> GetExpirationAsync(string key)
        {
            RedisKey redisKey = new RedisKey(key.ToLowerInvariant());
            var db = _connectionMultiplexer.GetDatabase();
            return await db.KeyTimeToLiveAsync(redisKey);
        }

        public async Task RemoveAsync(string key)
        {
            RedisKey redisKey = new RedisKey(key.ToLowerInvariant());
            var db = _connectionMultiplexer.GetDatabase();
            await db.KeyDeleteAsync(redisKey);
        }
    }
}
