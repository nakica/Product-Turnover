using Microsoft.Extensions.Caching.Memory;
using ProductTurnover.Infra;
using System;

namespace ProductTurnover.Business
{
    public class ProductTurnoverCache : IProductTurnoverCache
    {
        private readonly IMemoryCache _cache;

        public ProductTurnoverCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Add<T>(T item, string key)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));

            _cache.Set(key, item, cacheEntryOptions);
        }

        public bool TryGetValue<T>(string key, out T item)
        {
            var result = false;
            if (_cache.TryGetValue(key, out T cachedItem))
            {
                item = cachedItem;
                result = true;
            }
            else
            {
                item = default;
            }

            return result;
        }
    }
}
