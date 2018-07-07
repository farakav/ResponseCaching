using System.Reflection;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.DependencyInjection;

namespace ResponseCaching.Mvc
{
    public static class ResponseCachingServicesExtensions
    {
        public static IServiceCollection AddCacheFilters(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            services.Configure<AsyncCacheFilterProviderOptions>(
                options => { options.Assemblies = assemblies; });

            services.AddSingleton<IAsyncCacheFilterProvider, AsyncCacheFilterProvider>();

            return services;
        }
    }
}
