using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MovieAggregator.DTOs;
using MovieAggregator.Contracts;
using System.Threading.Tasks;
using System.Configuration;
using System.Linq;
using System;
using Google.Apis.YouTube.v3.Data;
using System.Collections.Generic;

namespace VideoProvider.Youtube.ApiInteraction
{
    public class YoutubeVideoProvider : ITrailerProvider
    {
        const string DefaultSearchResultProperty = "snippet";
        const string SearchResultType = "video";
        const string TrailerVideoCategoryId = "44";

        const int MaxResultsPerRequest = 10;

        private static readonly string ClientID = ConfigurationManager.AppSettings["GoogleApisClientID"];
        private static readonly string ClientSecret = ConfigurationManager.AppSettings["GoogleApisClientSecret"];
        private static YouTubeService youtubeService;

        private static YouTubeService YouTubeService
        {
            get
            {
                return youtubeService ?? (youtubeService = new YouTubeService(new BaseClientService.Initializer()
                                                {
                                                    ApiKey = ClientSecret,
                                                    ApplicationName = ClientID
                                                }
                                         ));
            }
        }

        public async Task<MovieTrailerDTO> GetTrailer(string movieTitle, DateTime? releaseDate)
        {
            //let's include all the movies from the beginning of cinema to unreleased with a 5 year scope
            if (ValidateReleaseDate(releaseDate) == false)
                throw new ArgumentException("ReleaseDate is not valid");

            return await SearchVideo(movieTitle, releaseDate);
        }

        private bool ValidateReleaseDate(DateTime? releaseDate)
        {
            //let's include all the movies from the beginning of cinema to unreleased with a 5 year scope
            return (releaseDate.HasValue && (releaseDate.Value.Year < 1890 || releaseDate.Value > DateTime.Now.AddYears(5)));
        }

        public async Task<MovieTrailerDTO> SearchVideo(string searchQuery, DateTime? releaseDate)
        {
            var searchListRequest = BuildRequest(searchQuery, releaseDate);
            var searchListResponse = await searchListRequest.ExecuteAsync();
            if (searchListResponse != null && searchListResponse.Items != null)
            {
                var searchResult = FilterSearchResults(searchQuery,
                                                       (releaseDate.HasValue ? releaseDate.Value.Year.ToString() : null), 
                                                       searchListResponse.Items);
                if (searchResult != null)
                {
                    return MapToDTO(searchResult);
                }
            }

            return null;
        }

        private SearchResource.ListRequest BuildRequest(string searchString, DateTime? releaseDate)
        {
            var searchListRequest = YouTubeService.Search.List(DefaultSearchResultProperty);
            searchListRequest.Q = searchString;
            searchListRequest.MaxResults = MaxResultsPerRequest;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            searchListRequest.Type = SearchResultType;
            searchListRequest.VideoCategoryId = TrailerVideoCategoryId;
            searchListRequest.VideoDuration = SearchResource.ListRequest.VideoDurationEnum.Short__;
            searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
            //trailers usually come out in the year prior the release of the movie
            //no upper bound, a post-release trailer is still good (old movies for example, 
            //also youtube started in 2005)
            if (releaseDate.HasValue)
            {
                searchListRequest.PublishedAfter = releaseDate.Value.Subtract(new TimeSpan(365, 0, 0, 0));
            }
            return searchListRequest;
        }

        private SearchResult FilterSearchResults(string movieTitle, string yearString, IList<SearchResult> results)
        {
            var subset = results.Where(r => r.TitleContains(movieTitle));
            if (subset == null || subset.Count() == 0)
            {
                subset = results;
            }

            if (string.IsNullOrWhiteSpace(yearString) == false)
            {
                var yearInTitleSubset = subset.Where(r => r.Snippet.Title.Contains(yearString));
                if (yearInTitleSubset != null && yearInTitleSubset.Count() > 0)
                {
                    subset = yearInTitleSubset;
                }

                var yearInDescriptionTooSubset = subset.Where(r => r.Snippet.Description.Contains(yearString));
                if (yearInDescriptionTooSubset != null && yearInDescriptionTooSubset.Count() > 0)
                {
                    subset = yearInDescriptionTooSubset;
                }
            }

            return subset.FirstOrDefault();
        }

        private MovieTrailerDTO MapToDTO(SearchResult searchResult)
        {
            return new MovieTrailerDTO
            {
                VideoTitle = searchResult.Snippet.Title,
                VideoURL = $"https://www.youtube.com/watch?v={searchResult.Id.VideoId}",
                EmbedURL = $"https://www.youtube.com/embed/{searchResult.Id.VideoId}"
            };
        }
    }
}