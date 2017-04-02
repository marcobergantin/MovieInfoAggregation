using Microsoft.Practices.Unity;
using MovieAggregator.Caching.MongoDB;
using MovieAggregator.Caching.MongoDB.Entities;
using MovieAggregator.Contracts;
using MovieAggregator.WebApi.Cache;
using MovieAggregator.WebApi.Helpers;

namespace MovieAggregator.WebApi
{
    public static class CacheConfig
    {
        public static void SetupCache(IUnityContainer container)
        {
            switch (GetConfigurableCacheType())
            {
                case CacheType.MongoDB:
                    SetupMongoDBCache(container);
                    break;

                default:
                    SetupInMemoryCache(container);
                    break;
            }
        }

        private static void SetupMongoDBCache(IUnityContainer container)
        {
            container.RegisterType<IMovieCacheEntry, MongoDBMovieCacheEntry>();
            container.RegisterType<IMovieCacheRepository, MongoDBMovieCacheRepository>();
            container.RegisterType<IMovieCacheEntityFactory, MongoDBMovieCacheFactory>();
        }

        private static void SetupInMemoryCache(IUnityContainer container)
        {
            container.RegisterType<IMovieCacheEntry, InMemoryMovieCacheEntry>();
            container.RegisterType<IMovieCacheRepository, InMemoryCacheRepository>();
            container.RegisterType<IMovieCacheEntityFactory, InMemoryMovieCacheFactory>();
        }
        private static CacheType GetConfigurableCacheType()
        {
            return ConfigurationHelper.GetValueFromConfiguration<CacheType>("CacheType");
        }

        public static uint GetConfigurableCacheExpirationInterval()
        {
            return ConfigurationHelper.GetValueFromConfiguration<uint>("CacheExiprationInterval");
        }
    }
}