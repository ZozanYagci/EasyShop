using Core.CrossCuttingConcerns.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Api.IntegrationTests.Services
{
    /// <summary>
    /// integration testlerde redis yerine InMemory cache implementasyonu
    /// </summary>
    public class InMemoryCacheService : ICacheService, IDisposable
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheService()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public void Dispose()
        {
            _memoryCache.Dispose();
        }

        public Task<bool> ExistsAsync(string key)
        {
            return Task.FromResult(_memoryCache.TryGetValue(key, out _));
        }

        public Task<T?> GetAsync<T>(string key)
        {
            return Task.FromResult(
                _memoryCache.TryGetValue(key, out var cached) ? (T?)cached : default);
        }

        public Task<bool> RemoveAsync(string key)
        {
            if (_memoryCache.TryGetValue(key, out _))
            {
                _memoryCache.Remove(key);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new MemoryCacheEntryOptions();
            if (expiration.HasValue)
            {
                options.SetAbsoluteExpiration(expiration.Value);
            }

            _memoryCache.Set(key, value!, options);
            return Task.CompletedTask;
        }
    }
}
