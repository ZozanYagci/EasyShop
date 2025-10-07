using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Core.CrossCuttingConcerns.Caching
{
    public class RedisCacheManager : ICacheService
    {
        private readonly IDatabase _database;
        private readonly ILogger<RedisCacheManager> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public RedisCacheManager(IConnectionMultiplexer connectionMultiplexer, ILogger<RedisCacheManager> logger = null, JsonSerializerOptions jsonOptions = null)
        {
            if (connectionMultiplexer == null)
                throw new ArgumentNullException(nameof(connectionMultiplexer));

            _database = connectionMultiplexer.GetDatabase();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true,
            };
        }

        public async Task<bool> ExistsAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return false;
            try
            {
                var exists = await _database.KeyExistsAsync(key);
                _logger.LogInformation("Cache exists check for key: {Key} = {Exists}", key, exists);
                return exists;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking existence for key :{Key}", key);
                return false;
            }
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return default;
            try
            {
                var value = await _database.StringGetAsync(key);
                if (value.IsNullOrEmpty)
                {
                    _logger.LogInformation("Cache miss for : {Key", key);
                    return default;
                }
                _logger.LogInformation("Cache hit for key:{Key}", key);
                return JsonSerializer.Deserialize<T>(value!, _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cache for key: {Key}", key);
                return default;
            }

        }

        public async Task<bool> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return false;
            try
            {
                var removed = await _database.KeyDeleteAsync(key);
                if (removed)

                    _logger.LogInformation("Cache removed for key: {Key}", key);

                else _logger.LogWarning("Attempted to remove non-existing cache key: {Key}", key);

                return removed;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache for key: {Key}", key);
                return false;
            }

        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "Cache key cannot be null or empty");
            try
            {
                var json = JsonSerializer.Serialize(value, _jsonOptions);
                await _database.StringSetAsync(key, json, expiration ?? TimeSpan.FromHours(1));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting cache for key:{Key}", key);
                throw;
            }
        }
    }
}
