using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace payfun.netcore.cache
{
    public static class CacheChell
    {
        public static async Task<T> GetObjectAsync<T>(this ICache cache, string key, Func<Task<T>> func, int expireSeconds = -1)
        {
            var result = await cache.GetObjectAsync<T>(key);
            if (result != null)
                return result;


            result = await func();
            await cache.SetObjectAsync(key, result, expireSeconds);

            return result;
        }
    }
}
