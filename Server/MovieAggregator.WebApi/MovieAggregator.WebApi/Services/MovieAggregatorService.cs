using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System;
using System.Threading.Tasks;

namespace MovieAggregator.WebApi.Services
{
    public class MovieAggregatorService : IMovieInfoAggregator
    {
        ITrailerProvider _trailerProvider;
        IMovieInfoProvider _infoProvider;
        IMovieCacheService _cache;

        public MovieAggregatorService(IMovieInfoProvider infoProvider, 
            ITrailerProvider trailerProvider,
            IMovieCacheService cache)
        {
            _infoProvider = infoProvider;
            _trailerProvider = trailerProvider;
            _cache = cache;
        }

        public async Task<MovieAggregatedContentDTO> GetAggregatedInfo(string searchString)
        {
            var cachedObj = _cache.GetFromCache(searchString);
            if (cachedObj != null)
            {
                return cachedObj;
            }

            var info = await _infoProvider.GetInfo(searchString);
            if (info == null || string.IsNullOrEmpty(info.Title))
            {
                return null;
            }

            var result = new MovieAggregatedContentDTO()
            {
                Info = info,
                Trailer = await _trailerProvider.GetTrailer(info.Title, DateTime.Parse(info.Released))
            };

            _cache.AddToCache(searchString, result);
            return result;
        }
    }
}