using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.ResponseCaching;

namespace ResponseCaching.Mvc
{
    internal class AsyncCacheFilterDescriptor
    {
        public string RouteTemplate { get; set; }

        public ICollection<AsyncCacheFilter> CacheFilters { get; set; }

        public ICollection<Type> ServiceFilters { get; set; }

        public string HttpMethod { get; set; }
    }
}