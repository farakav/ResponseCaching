using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.ResponseCaching
{
    public class AsyncCacheFilterContext
    {
        public AsyncCacheFilterContext(HttpContext httpContext, 
            RouteValueDictionary routeValues)
        {
            HttpContext = httpContext;
            RouteValues = routeValues;
        }

        public HttpContext HttpContext { get; }

        public RouteValueDictionary RouteValues { get; }
    }
}