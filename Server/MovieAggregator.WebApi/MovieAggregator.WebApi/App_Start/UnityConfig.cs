using Microsoft.Practices.Unity;
using MovieAggregator.Caching.MongoDB;
using MovieAggregator.Caching.MongoDB.Entities;
using MovieAggregator.Contracts;
using MovieAggregator.WebApi.Cache;
using MovieAggregator.WebApi.Services;
using MovieInfoProvider.OMDb.ApiInteraction;
using System.Web.Http;
using Unity.WebApi;
using VideoProvider.Youtube.ApiInteraction;

namespace MovieAggregator.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            container.RegisterType<IMovieInfoProvider, OMDbMovieInfoProvider>();
            container.RegisterType<ITrailerProvider, YoutubeVideoProvider>();
            //SetupMongoDBCache(container);
            SetupInMemoryCache(container);
            container.RegisterType<IMovieCacheService, MovieCacheService>();

            //singleton
            container.RegisterType<IMovieInfoAggregator, MovieAggregatorService>(new ContainerControlledLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
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
    }
}