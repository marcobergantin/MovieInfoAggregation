using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<MovieContentDTO> GetAggregatedInfo(string searchString)
        {
            var cachedObj = await _cache.GetFromCache(searchString);
            if (cachedObj != null)
            {
                return cachedObj;
            }

            var infoCollection = await _infoProvider.GetInfo(searchString);
            if (infoCollection == null || infoCollection.Count() == 0)
            {
                return null;
            }

            List<MovieContentEntryDTO> results = new List<MovieContentEntryDTO>();
            foreach (var entry in infoCollection)
            {
                if (string.IsNullOrEmpty(entry.Title) == false)
                {
                    var dto = new MovieContentEntryDTO();
                    dto.Info = entry;
                    dto.Trailer = await _trailerProvider.GetTrailer(entry.Title, GetReleasedDateParameter(entry));
                    results.Add(dto);
                }
            }

            var returnObj = new MovieContentDTO()
            {
                Entries = results
            };

            await _cache.AddToCache(searchString, returnObj);
            return returnObj;
        }

        private DateTime? GetReleasedDateParameter(MovieInfoDTO info)
        {
            if (info.Released.HasValue)
            {
                return info.Released.Value;
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