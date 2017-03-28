using MongoDB.Driver;
using MovieAggregator.Caching.MongoDB.Entities;
using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System.Linq;
using System;

namespace MovieAggregator.Caching.MongoDB
{
    public class MongoDBMovieCacheRepository : IMovieCacheRepository
    {
        const string DatabaseName = "movieAggregator";
        const string CollectionName = "cache";

        static MongoClient _client;
        static IMongoDatabase _database;
        static IMongoCollection<IMovieCacheEntry> _cacheCollection;

        IMovieCacheEntityFactory _entityFactory;

        public MongoDBMovieCacheRepository()
        {
            _entityFactory = new MongoDBMovieCacheFactory();

            _client = new MongoClient();
            _database = _client.GetDatabase(DatabaseName);
            _cacheCollection = _database.GetCollection<IMovieCacheEntry>(CollectionName);
        }

        public void Add(string searchString, IMovieCacheEntry content)
        {
            if (content == null)
                return;

            _cacheCollection.InsertOne(_entityFactory.CreateEntry(content.Data));
        }

        public IMovieCacheEntry Get(string searchString)
        {
            return _cacheCollection.AsQueryable().FirstOrDefault(c => 
                (c as MongoDBMovieCacheEntry).Data.Info.Title == searchString);
        }

        public void Remove(IMovieCacheEntry content)
        {
            _cacheCollection.DeleteOne(c => 
                (c as MongoDBMovieCacheEntry).Data.Info.Title == content.Data.Info.Title);
        }
    }
}
