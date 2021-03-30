using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
namespace payfun.netcore.cache
{
    public interface ICache
    {
        Task SetObjectAsync(string key, object value, int expireSeconds = -1);
        Task<T> GetObjectAsync<T>(string key);
        Task RemoveObjectsAsync(params string[] key);
    }
}
