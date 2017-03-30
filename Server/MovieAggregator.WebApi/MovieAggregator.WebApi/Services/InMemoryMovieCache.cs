using MovieAggregator.Contracts;
using System;
using System.Collections.Generic;
using MovieAggregator.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.WebApi.Services
{
    //public class InMemoryMovieCache : IMovieCacheService
    //{
    //    private const int CacheInitialSize = 32;
    //    Dictionary<string, MovieAggregatedContentDTO> _cache;

    //    public InMemoryMovieCache()
    //    {
    //        _cache = new Dictionary<string, MovieAggregatedContentDTO>(CacheInitialSize);
    //    }

    //    public Task AddToCache(string searchString, MovieAggregatedContentDTO content)
    //    {
    //        if (_cache.ContainsKey(searchString) == false)
    //        {
    //            return Task.FromResult(_cache.Add(searchString, content));
    //        }
    //    }

    //    public Task<MovieAggregatedContentDTO> GetFromCache(string searchString)
    //    {
    //        if (_cache.ContainsKey(searchString))
    //        {
    //            return Task.FromResult(_cache[searchString]);
    //        }

    //        return null;
    //    }

    //    public Task SetExpirationInterval(uint seconds)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}