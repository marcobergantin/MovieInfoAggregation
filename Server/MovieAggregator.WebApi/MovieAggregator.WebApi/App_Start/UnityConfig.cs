using Microsoft.Practices.Unity;
using MovieAggregator.Contracts;
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
            container.RegisterType<IMovieCache, InMemoryMovieCache>();

            //singleton
            container.RegisterType<IMovieInfoAggregator, MovieAggregatorService>(new ContainerControlledLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}