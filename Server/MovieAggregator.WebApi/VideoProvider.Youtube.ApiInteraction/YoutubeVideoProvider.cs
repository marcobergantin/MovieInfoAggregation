using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MovieAggregator.DTOs;
using MovieAggregator.Contracts;
using System.Threading.Tasks;
using System.Configuration;
using System.Linq;
using System;

namespace VideoProvider.Youtube.ApiInteraction
{
    public class YoutubeVideoProvider : ITrailerProvider
    {
        private static readonly string ClientID = ConfigurationManager.AppSettings["GoogleApisClientID"];
        private static readonly string ClientSecret = ConfigurationManager.AppSettings["GoogleApisClientSecret"];
        private static YouTubeService youtubeService;

        public async Task<MovieTrailerDTO> GetTrailer(string movieTitle, DateTime releaseDate)
        {
            //let's include all the movies from the beginning of cinema to unreleased with a 5 year scope
            if (releaseDate.Year < 1890 || releaseDate > DateTime.Now.AddYears(5))
                throw new ArgumentException("ReleaseDate is not valid");

            return await SearchVideo(movieTitle + " trailer " + releaseDate.Year, releaseDate);
        }

        public async Task<MovieTrailerDTO> SearchVideo(string searchQuery, DateTime releaseDate)
        {
            if (youtubeService == null)
            {
                youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = ClientSecret,
                    ApplicationName = ClientID
                });
            }

            var searchListRequest = BuildRequest(searchQuery, releaseDate);
            var searchListResponse = await searchListRequest.ExecuteAsync();
            if (searchListResponse != null && searchListResponse.Items != null)
            {
                var searchResult = searchListResponse.Items.FirstOrDefault();
                if (searchResult != null)
                {
                    return new MovieTrailerDTO
                    {
                        VideoTitle = searchResult.Snippet.Title,
                        VideoURL = $"https://www.youtube.com/watch?v={searchResult.Id.VideoId}",
                        EmbedURL = $"https://www.youtube.com/embed/{searchResult.Id.VideoId}"
                    };
                }
            }

            return null;
        }

        private SearchResource.ListRequest BuildRequest(string searchString, DateTime releaseDate)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchString;
            searchListRequest.MaxResults = 1;           
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            searchListRequest.Type = "video";
            searchListRequest.VideoCategoryId = "44"; //Trailers
            searchListRequest.VideoDuration = SearchResource.ListRequest.VideoDurationEnum.Short__;
            searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
            //trailers usually come out in the year prior the release of the movie
            //no upper bound, a post-release trailer is still good (old movies for example, 
            //also youtube started in 2005)
            searchListRequest.PublishedAfter = releaseDate.Subtract(new TimeSpan(365, 0, 0, 0));
            return searchListRequest;
        }
    }
}