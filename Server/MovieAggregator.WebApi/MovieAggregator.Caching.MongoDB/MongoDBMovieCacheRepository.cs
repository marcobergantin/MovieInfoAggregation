using MongoDB.Driver;
using MovieAggregator.Caching.MongoDB.Entities;
using MovieAggregator.Contracts;
using System.Threading.Tasks;

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

        public Task Add(string searchString, IMovieCacheEntry content)
        {
            if (content == null)
                return null;

            return _cacheCollection.InsertOneAsync(_entityFactory.CreateEntry(searchString, content.Data));
        }

        public async Task<IMovieCacheEntry> Get(string searchString)
        {
            var cursor = await _cacheCollection.FindAsync(c =>
                (c as MongoDBMovieCacheEntry).SeachString == searchString);

            return await cursor.FirstOrDefaultAsync();
        }

        public Task Remove(string seachString)
        {
            return _cacheCollection.DeleteOneAsync(c =>
                (c as MongoDBMovieCacheEntry).SeachString == seachString);
        }
    }
}
