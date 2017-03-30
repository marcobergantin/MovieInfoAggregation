using MovieAggregator.Contracts;
using System;
using MovieAggregator.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.WebApi.Services
{
    public class MovieCacheService : IMovieCacheService
    {
        const uint DefaultExpirationInterval = 30;

        IMovieCacheRepository _cacheRepository;
        IMovieCacheEntityFactory _entityFactory;
        TimeSpan _expirationInterval;

        public MovieCacheService(IMovieCacheRepository cacheRepository, IMovieCacheEntityFactory entityFactory)
        {
            _cacheRepository = cacheRepository;
            _entityFactory = entityFactory;
            _expirationInterval = TimeSpan.FromSeconds(DefaultExpirationInterval);
        }

        public Task AddToCache(string searchString, MovieAggregatedContentDTO content)
        {
            return _cacheRepository.Add(searchString, _entityFactory.CreateEntry(searchString, content));
        }

        public async Task<MovieAggregatedContentDTO> GetFromCache(string searchString)
        {
            var cachedObj = await _cacheRepository.Get(searchString);
            if (cachedObj == null)
            {
                return null;
            }

            if ((DateTime.UtcNow - cachedObj.TimeStamp) > _expirationInterval)
            {
                await _cacheRepository.Remove(searchString);
                return null;
            }

            return cachedObj.Data;
        }

        public void SetExpirationInterval(uint seconds)
        {
            _expirationInterval = TimeSpan.FromSeconds(seconds);
        }
    }
}