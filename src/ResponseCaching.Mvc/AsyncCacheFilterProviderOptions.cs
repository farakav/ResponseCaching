using System.Collections.Generic;
using System.Reflection;

namespace ResponseCaching.Mvc
{
    public class AsyncCacheFilterProviderOptions
    {
        public IEnumerable<Assembly> Assemblies { get; set; }
    }
}