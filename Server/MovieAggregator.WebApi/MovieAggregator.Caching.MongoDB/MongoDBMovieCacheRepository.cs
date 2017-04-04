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

        public MongoDBMovieCacheRepository()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase(DatabaseName);
            _cacheCollection = _database.GetCollection<IMovieCacheEntry>(CollectionName);
        }

        public Task Add(string searchString, IMovieCacheEntry entity)
        {
            if (entity == null)
                return null;

            return _cacheCollection.InsertOneAsync(entity);
        }

        public async Task<IMovieCacheEntry> Get(string searchString)
        {
            /* could not group the matching condition along with the one in the Remove method, 
               nor use string.Equals with the ignore case option, because the driver
               doens't seem to be able to translate such a lambda to a mongo db query
               also, having to cast the entries is not my favorite thing, but the driver requires this as well
               Moreover, if the MongoDBMovieCacheEntry class and the structure of the documents in the db do no match,
               we'll have some troubles here -> updates in documents structure have to be reflected in the corresponding class
               Also not thrilled about having to do this double await */

            var cursor = await _cacheCollection.FindAsync(c => ((MongoDBMovieCacheEntry)c).SeachString == searchString);
            return await cursor.FirstOrDefaultAsync();
        }

        public Task Remove(string searchString)
        {
            return _cacheCollection.DeleteOneAsync(c => ((MongoDBMovieCacheEntry)c).SeachString == searchString);
        }
    }
}