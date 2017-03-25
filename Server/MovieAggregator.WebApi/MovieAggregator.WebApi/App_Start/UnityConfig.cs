using Microsoft.Practices.Unity;
using MovieAggregator.Contracts;
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

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IMovieInfoProvider, OMDbMovieInfoProvider>();
            container.RegisterType<ITrailerProvider, YoutubeVideoProvider>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}