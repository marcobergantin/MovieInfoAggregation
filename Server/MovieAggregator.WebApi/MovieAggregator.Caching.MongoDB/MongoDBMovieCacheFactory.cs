using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using MovieAggregator.Caching.MongoDB.Entities;

namespace MovieAggregator.Caching.MongoDB
{
    public class MongoDBMovieCacheFactory : IMovieCacheEntityFactory
    {
        public IMovieCacheEntry CreateEntry(string searchString, MovieContentDTO data)
        {
            if (string.IsNullOrWhiteSpace(searchString) || data == null)
                return null;

            return new MongoDBMovieCacheEntry(searchString, data);
        }
    }
}