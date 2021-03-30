using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace payfun.netcore.cache
{
    public class InMemoryCache : ICache
    {
        private CacheItemPolicy DefaultPolicy = null;
        public InMemoryCache(CacheItemPolicy policy = null)
        {
            this.DefaultPolicy = policy;
        }


        public T GetObject<T>(string key)
        {
            var value = MemoryCache.Default.Get(key);
            if (value == null)
            {
                return default(T);
            }
            return (T)value;
        }
        public void SetObject(string key, object value, int expireSeconds = -1)
        {
            if (expireSeconds != -1)
            {
                var time = DateTimeOffset.Now.AddSeconds(expireSeconds);
                MemoryCache.Default.Set(key, value, time);
            }
            else
                MemoryCache.Default.Set(key, value, this.GetPolicy());
        }


        public async Task<T> GetObjectAsync<T>(string key)
        {
            var value = MemoryCache.Default.Get(key);
            if (value == null)
            {
                return default(T);
            }
            return await Task.FromResult((T)value);
        }
        public async Task SetObjectAsync(string key, object value, int expireSeconds = -1)
        {
            if (expireSeconds != -1)
            {
                var time = DateTimeOffset.Now.AddSeconds(expireSeconds);
                MemoryCache.Default.Set(key, value, time);
            }
            else
                MemoryCache.Default.Set(key, value, this.GetPolicy());
            await Task.CompletedTask;
        }

        public async Task RemoveObjectsAsync(params string[] keys)
        {
            foreach (var key in keys)
            {
                MemoryCache.Default.Remove(key);
            }
            await Task.CompletedTask;
        }

        private CacheItemPolicy GetPolicy()
        {
            if (this.DefaultPolicy != null)
                return this.DefaultPolicy;

            return new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddDays(7),
            };
        }
    }
}
