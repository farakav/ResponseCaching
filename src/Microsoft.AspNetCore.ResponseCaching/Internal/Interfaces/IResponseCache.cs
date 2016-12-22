﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.ResponseCaching.Internal
{
    public interface IResponseCache
    {
        Task<IResponseCacheEntry> GetAsync(object key);
        Task SetAsync(object key, IResponseCacheEntry entry, TimeSpan validFor);
    }
}
