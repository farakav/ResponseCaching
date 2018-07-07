using System;

namespace Microsoft.AspNetCore.ResponseCaching
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ServiceCacheFilter : Attribute
    {
        public ServiceCacheFilter(Type filterType)
        {
            FilterType = filterType;
        }

        public Type FilterType { get;}
    }
}