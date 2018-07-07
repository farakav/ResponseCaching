using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ResponseCaching.Internal;

namespace Microsoft.AspNetCore.ResponseCaching
{
    public class CustomResponseCache : IResponseCache
    {
        private readonly ICacheProvider _cacheProvider;

        public CustomResponseCache(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public IResponseCacheEntry Get(string key)
        {
            throw new NotImplementedException();
        }

        public Task<IResponseCacheEntry> GetAsync(string key)
        {
            return _cacheProvider.FetchAsync<IResponseCacheEntry>(key);
        }

        public void Set(string key, IResponseCacheEntry entry, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, IResponseCacheEntry entry, TimeSpan validFor)
        {
            return _cacheProvider.StoreAsync(key, entry, validFor);
        }
    }
}