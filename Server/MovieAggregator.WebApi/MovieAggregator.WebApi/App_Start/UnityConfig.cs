using Microsoft.Practices.Unity;
using MovieAggregator.Contracts;
using MovieAggregator.TMDb.ApiInteraction;
using MovieAggregator.WebApi.Services;
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

            container.RegisterType<IMovieInfoProvider, TMDbMovieInfoProvider>();
            container.RegisterType<ITrailerProvider, YoutubeVideoProvider>();
            container.RegisterType<IMovieCacheService, MovieCacheService>();

            CacheConfig.SetupCache(container);

            //singleton
            container.RegisterType<IMovieInfoAggregator, MovieAggregatorService>(new ContainerControlledLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}