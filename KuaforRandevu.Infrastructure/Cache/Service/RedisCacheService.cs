using Infrastructure.Cache.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Cache.Service
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }

        // Verilen anahtara karşılık objeyi JSON olarak Redis'e kaydeder ve TTL belirler.
        public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
        {
            // Değeri JSON string formatına dönüştür
            var json = JsonSerializer.Serialize(value);
            // JSON string'i, belirtilen TTL (Time To Live) ile cache'e ekle
            await _cache.SetStringAsync(key, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl
            });
        }

        // Redis'ten belirtilen anahtara karşılık gelen JSON veriyi alır ve T tipine deserialize eder.
        public async Task<T?> GetAsync<T>(string key)
        {
            // Cache'den JSON string'i çek
            var json = await _cache.GetStringAsync(key);
            // Eğer veri yoksa null döner, varsa JSON'u T tipine dönüştürür
            return json is null
                ? default
                : JsonSerializer.Deserialize<T>(json);
        }

        // Belirtilen anahtara ait cache verisini siler.
        public Task RemoveAsync(string key) =>
            _cache.RemoveAsync(key);
    }
}
