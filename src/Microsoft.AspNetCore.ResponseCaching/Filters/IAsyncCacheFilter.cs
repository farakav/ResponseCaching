using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.ResponseCaching
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class AsyncCacheFilter : Attribute
    {
        public virtual Task OnBeforeServerAsync(AsyncCacheFilterContext context)
            => Task.CompletedTask;

        public virtual Task OnCacheServedAsync(AsyncCacheFilterContext context) 
            => Task.CompletedTask;
    }
}