using CSRedis;
using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace payfun.netcore.cache
{
    public class RedisCache : ICache
    {
        private CSRedisClient redis;
        public RedisCache(CSRedisClient client)
        {
            this.redis = client;
        }
        public T GetObject<T>(string key)
        {
            return this.redis.Get<T>(key);
        }
        public void SetObject(string key, object value, int expireSeconds = -1)
        {
            this.redis.Set(key, value, expireSeconds);
        }
        public async Task RemoveObjectsAsync(params string[] key)
        {
            await this.redis.DelAsync(key);
        }

        public async Task<T> GetObjectAsync<T>(string key)
        {
            return await this.redis.GetAsync<T>(key);
        }
        public async Task SetObjectAsync(string key, object value, int expireSeconds = -1)
        {
            await this.redis.SetAsync(key, value, expireSeconds);
        }
    }
}
