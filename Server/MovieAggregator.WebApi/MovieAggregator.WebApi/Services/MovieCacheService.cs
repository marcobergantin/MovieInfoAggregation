using MovieAggregator.Contracts;
using System;
using MovieAggregator.DTOs;

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

        public void AddToCache(string searchString, MovieAggregatedContentDTO content)
        {
            _cacheRepository.Add(searchString, _entityFactory.CreateEntry(content));
        }

        public MovieAggregatedContentDTO GetFromCache(string searchString)
        {
            var cachedObj = _cacheRepository.Get(searchString);
            if (cachedObj == null)
            {
                return null;
            }

            if ((DateTime.UtcNow - cachedObj.TimeStamp) > _expirationInterval)
            {
                _cacheRepository.Remove(cachedObj);
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