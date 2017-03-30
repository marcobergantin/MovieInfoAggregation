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
            var cachedObj = await _cache.GetFromCache(searchString);
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
                Trailer = await _trailerProvider.GetTrailer(info.Title, 
                                                    GetReleasedDateParameter(info))
            };

            await _cache.AddToCache(searchString, result);
            return result;
        }

        private DateTime? GetReleasedDateParameter(MovieInfoDTO info)
        {
            DateTime releasedDate;
            if (DateTime.TryParse(info.Released, out releasedDate))
            {
                return releasedDate;
            }

            int year;
            if (int.TryParse(info.Year, out year))
            {
                return new DateTime(year, 6, 15);
            }

            return null;
        }
    }
}