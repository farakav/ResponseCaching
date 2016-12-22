// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.ResponseCaching.Internal
{
    public class ResponseCachingBaseKey : IResponseCachingKey
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public bool CaseSensitivePath { get; set; }

        public override bool Equals(object obj)
        {
            var baseKey = obj as ResponseCachingBaseKey;
            if (baseKey == null)
            {
                return false;
            }

            if (!string.Equals(Method, baseKey.Method, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (CaseSensitivePath)
            {
                if (!string.Equals(Path, baseKey.Path, StringComparison.Ordinal))
                {
                    return false;
                }
            }
            else if (!string.Equals(Path, baseKey.Path, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            // This needs to be different
            return Method.Length + Path.Length;
        }
    }
}
