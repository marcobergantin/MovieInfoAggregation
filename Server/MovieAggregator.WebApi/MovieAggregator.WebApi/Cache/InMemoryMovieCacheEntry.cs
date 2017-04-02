using MovieAggregator.DTOs;
using MovieAggregator.Caching.Common;

namespace MovieAggregator.WebApi.Cache
{
    public class InMemoryMovieCacheEntry : BaseMovieCacheEntry
    {
        public InMemoryMovieCacheEntry(string searchString, MovieContentDTO data) :
            base(searchString, data)
        {
        }
    }
}