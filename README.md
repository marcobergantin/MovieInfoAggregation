# MovieInfoAggregation
WebApi and client to retrieve and aggregate movie information and trailer

Assignment

Create a webpage on which you can search for movie trailers.

_Use an API of an online movie database (e.g. IMDB or Rotten Tomatoes);

_Use an API of an online video service (e.g. YouTube or Vimeo);

_Create your own WebAPI as middleware to retrieve the results of both services and aggregate them;

_Cache the aggregated data for performance;

_Make the search as smart as you can;

MovieAggregator.Client is a ASP.NET Core MVC project (so .NET Core and the VS tooling for it are prerequisites)

MovieAggregator.WebApi is a ASP.NET WebApi 2 project, targeting .NET framework 4.6.2. 

It has no UI at all, so when running it and browsing to the main page, you'll get a 403.14 error with the relative page. Simply add "/swagger" to the URL in order to see the documentation.

Would have liked to make this a .NET Core project as well. Didn't do that because at the moment there are no libraries for the interaction with youtube apis in that platform. Same goes for TMDb's api.
I would have also liked to share the DTOs project between the two solutions, but the difference of target framework didn't allow it. 
In a real case, a good practice might have been to publish a nuget package for it and install it in both solutions (would still need the same target framework for that though).

The api used for the movie information is TMDb: https://developers.themoviedb.org/3/getting-started
In the project you can also see that the OMDb one can be used (that's the one with which I started the project), http://www.omdbapi.com/
I decided to prefer the TMDb one because, altough it takes multiple calls to retrieve as much information as with OMDb, this one returns multiple entries per search: OMDb only returns one movie per query.

The provider chosen for the trailes is Youtube: https://developers.google.com/youtube/
For the interaction with the Google Apis the project uses the official libraries.
I tried to make the search as smart as possible by setting all the parameters for the request that I though that made sense, see the file YoutubeRequestHelper.cs in the project VideoProvider.Youtube.ApiInteraction.
On top of that, I filter the results after the search has returned the data from the api, in order to try and get the video that is most likely to match the actual movie trailer, see YoutubeSearchResultsHelper.cs in the same project.

The project supports two types of cache: in memory and with MongoDB.
The cache expiration interval and the cache type to be used can be configured in the web.config file in the MovieAggregator.WebApi project.
Default values are: 1 hour for the interval, and InMemoryfor the CacheType (so the project will work right away even if the machine on which it runs has no MongoDB istalled).
In order to use MongoDB as cache, please make sure that an instance of mongod is running at the default port and uncomment line 12 in the web.config file of the main project before starting it.
