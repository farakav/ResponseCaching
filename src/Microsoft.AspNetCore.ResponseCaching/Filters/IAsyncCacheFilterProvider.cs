using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.ResponseCaching
{
    public interface IAsyncCacheFilterProvider
    {
        /// <summary>
        /// Might return null
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        ICollection<AsyncCacheFilter> GetFilters(HttpContext httpContext, 
            out AsyncCacheFilterContext filterContext);
    }
}