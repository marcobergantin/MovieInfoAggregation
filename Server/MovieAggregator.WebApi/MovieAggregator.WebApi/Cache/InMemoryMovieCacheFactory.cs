using MovieAggregator.Contracts;
using MovieAggregator.DTOs;

namespace MovieAggregator.WebApi.Cache
{
    public class InMemoryMovieCacheFactory : IMovieCacheEntityFactory
    {
        public IMovieCacheEntry CreateEntry(string searchString, MovieContentDTO data)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return null;
            }

            return new InMemoryMovieCacheEntry(searchString, data);
        }
    }
}