using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.ResponseCaching
{
    public interface ICacheProvider
    {
        /// <summary>
        ///     Stores an item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration">The timespan specifying object expiration.</param>
        /// <returns></returns>
        Task StoreAsync<T>(string key, T value, TimeSpan? expiration = null);

        Task<T> FetchAsync<T>(string key);
    }
}