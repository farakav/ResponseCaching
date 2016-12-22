// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.AspNetCore.ResponseCaching.Internal
{
    public interface IResponseCachingKeyProvider
    {
        /// <summary>
        /// Create a base key for a response cache entry.
        /// </summary>
        /// <param name="context">The <see cref="ResponseCachingContext"/>.</param>
        /// <returns>The created base key.</returns>
        object CreateBaseKey(ResponseCachingContext context);

        /// <summary>
        /// Create a vary key for storing cached responses.
        /// </summary>
        /// <param name="context">The <see cref="ResponseCachingContext"/>.</param>
        /// <returns>The created vary key.</returns>
        object CreateStorageVaryByKey(ResponseCachingContext context);

        /// <summary>
        /// Create one or more vary keys for looking up cached responses.
        /// </summary>
        /// <param name="context">The <see cref="ResponseCachingContext"/>.</param>
        /// <returns>An ordered <see cref="IEnumerable{T}"/> containing the vary keys to try when looking up items.</returns>
        IEnumerable<object> CreateLookupVaryByKeys(ResponseCachingContext context);
    }
}
