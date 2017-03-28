using MongoDB.Driver;
using MovieAggregator.Caching.MongoDB.Entities;
using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System.Linq;

namespace MovieAggregator.Caching.MongoDB
{
    public class MongoDBMovieCache : IMovieCache
    {
        const string DatabaseName = "movieAggregator";
        const string CollectionName = "cache";

        static MongoClient _client;
        static IMongoDatabase _database;
        static IMongoCollection<MovieCacheEntry> _cacheCollection;

        public MongoDBMovieCache()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase(DatabaseName);
            _cacheCollection = _database.GetCollection<MovieCacheEntry>(CollectionName);
        }

        public void AddToCache(string searchString, MovieAggregatedContentDTO content)
        {
            var cacheEntry = _cacheCollection.AsQueryable()
                                             .FirstOrDefault(c => c.Data.Info.Title == content.Info.Title);
            if (cacheEntry != null)
                return;

            _cacheCollection.InsertOne(new MovieCacheEntry(content));
        }
        public MovieAggregatedContentDTO GetFromCache(string searchString)
        {
            var cachedObj = _cacheCollection.AsQueryable().FirstOrDefault(c => c.Data.Info.Title == searchString);
            if (cachedObj != null)
            {
                return cachedObj.Data;
            }

            return null;
        }
    }
}
