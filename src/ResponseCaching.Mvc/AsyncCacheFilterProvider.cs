using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ResponseCaching.Mvc
{
    public class AsyncCacheFilterProvider : IAsyncCacheFilterProvider
    {
        private readonly List<Tuple<string, TemplateMatcher, List<AsyncCacheFilter>>>
            _filters;

        public AsyncCacheFilterProvider(IServiceProvider services,
            IOptions<AsyncCacheFilterProviderOptions> options)
        {
            IEnumerable<AsyncCacheFilterDescriptor> descriptors 
                = FindDescriptors(options.Value.Assemblies);

            _filters = descriptors
                .Select(d =>
                {
                    RouteTemplate template = TemplateParser.Parse(d.RouteTemplate);
                    var matcher = new TemplateMatcher(template, GetDefaults(template));
                    List<AsyncCacheFilter> filterInstances =
                        d.CacheFilters.Union(
                                d.ServiceFilters
                                    .Select(t =>
                                        (AsyncCacheFilter) services.GetRequiredService(t))
                            )
                            .ToList();



                    return Tuple.Create(d.HttpMethod, matcher, filterInstances);
                })
                .ToList();
        }

        public ICollection<AsyncCacheFilter> GetFilters(HttpContext httpContext,
            out AsyncCacheFilterContext filterContext)
        {
            // todo: performance

            var routeValues
                = new RouteValueDictionary();

            var match = _filters.FirstOrDefault(f =>
                f.Item1 == httpContext.Request.Method &&
                f.Item2.TryMatch(httpContext.Request.Path, routeValues)
            );

            if (match == null)
            {
                filterContext = null;
                return null;
            }

            filterContext = new AsyncCacheFilterContext(httpContext, routeValues);

            return match.Item3;
        }

        private static RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
        {
            var result = new RouteValueDictionary();

            foreach (TemplatePart parameter in parsedTemplate.Parameters)
            {
                if (parameter.DefaultValue != null)
                {
                    result.Add(parameter.Name, parameter.DefaultValue);
                }
            }

            return result;
        }

        private static IEnumerable<AsyncCacheFilterDescriptor> FindDescriptors(
            IEnumerable<Assembly> assemblies)
        {
            var filters
                = new List<AsyncCacheFilterDescriptor>();

            foreach (Assembly assembly in assemblies)
            {
                List<Type> controllerTypes = assembly.ExportedTypes
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                    .ToList();

                foreach (Type controllerType in controllerTypes)
                {
                    var routeAttribute = controllerType.GetCustomAttribute<RouteAttribute>();

                    MethodInfo[] methods = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public);

                    string baseRoute = routeAttribute?.Template ?? string.Empty;
                    foreach (MethodInfo methodInfo in methods)
                    {
                        List<AsyncCacheFilter> cacheFilters
                            = methodInfo.GetCustomAttributes<AsyncCacheFilter>()
                                .ToList();

                        List<ServiceCacheFilter> serviceCacheFilters
                            = methodInfo.GetCustomAttributes<ServiceCacheFilter>()
                                .ToList();

                        if (!cacheFilters.Any() && !serviceCacheFilters.Any())
                            continue;

                        IEnumerable<HttpMethodAttribute> httpMethodAttributes
                            = methodInfo.GetCustomAttributes<HttpMethodAttribute>()
                                .ToList();

                        foreach (HttpMethodAttribute httpMethodAttribute in httpMethodAttributes)
                        {
                            string routeTemplate = $"{baseRoute}/{httpMethodAttribute.Template}";
                            foreach (string httpMethod in httpMethodAttribute.HttpMethods)
                            {
                                Console.WriteLine(
                                    $"[{httpMethod}] {routeTemplate} is using cache filters: {string.Join(",", cacheFilters.Select(i => i.GetType().Name).Union(serviceCacheFilters.Select(cf => cf.FilterType.Name)))}");

                                filters.Add(new AsyncCacheFilterDescriptor
                                {
                                    HttpMethod = httpMethod,
                                    RouteTemplate = routeTemplate,
                                    CacheFilters = cacheFilters,
                                    ServiceFilters = serviceCacheFilters
                                        .Select(a => a.FilterType)
                                        .ToList()
                                });
                            }
                        }
                    }
                }
            }

            return filters;
        }
    }
}