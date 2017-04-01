using MovieAggregator.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAggregator.WebApi.Cache
{
    public class InMemoryCacheRepository : IMovieCacheRepository
    {
        const int CacheInitialSize = 32;
        Dictionary<string, IMovieCacheEntry> _cache;

        public InMemoryCacheRepository()
        {
            _cache = new Dictionary<string, IMovieCacheEntry>(CacheInitialSize);
        }

        public Task Add(string searchString, IMovieCacheEntry entry)
        {
            _cache.Add(searchString, entry);
            return Task.CompletedTask;
        }

        public Task<IMovieCacheEntry> Get(string searchString)
        {
            if (_cache.ContainsKey(searchString) == false)
                return Task.FromResult<IMovieCacheEntry>(null);

            return Task.FromResult(_cache[searchString]);
        }

        public Task Remove(string searchString)
        {
            _cache.Remove(searchString);
            return Task.CompletedTask;
        }
    }
}