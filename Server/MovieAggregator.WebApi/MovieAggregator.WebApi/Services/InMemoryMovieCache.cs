using MovieAggregator.Contracts;
using System;
using System.Collections.Generic;
using MovieAggregator.DTOs;

namespace MovieAggregator.WebApi.Services
{
    public class InMemoryMovieCache : IMovieCache
    {
        private const int CacheInitialSize = 32;
        Dictionary<string, MovieAggregatedContentDTO> _cache;

        public InMemoryMovieCache()
        {
            _cache = new Dictionary<string, MovieAggregatedContentDTO>(CacheInitialSize);
        }

        public void AddToCache(string searchString, MovieAggregatedContentDTO content)
        {
            if (_cache.ContainsKey(searchString) == false)
            {
                _cache.Add(searchString, content);
            }
        }

        public MovieAggregatedContentDTO GetFromCache(string searchString)
        {
            if (_cache.ContainsKey(searchString))
            {
                return _cache[searchString];
            }

            return null;
        }
    }
}