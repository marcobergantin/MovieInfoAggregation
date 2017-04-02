using Google.Apis.YouTube.v3;
using System;

namespace VideoProvider.Youtube.ApiInteraction.Helpers
{
    public static class YoutubeRequestHelper
    {
        const string DefaultSearchResultProperty = "snippet";
        const string SearchResultType = "video";
        const string TrailerVideoCategoryId = "44";

        const int MaxResultsPerRequest = 10;

        public static SearchResource.ListRequest BuildRequest(string searchString, DateTime? releaseDate)
        {
            var searchListRequest = YoutubeServiceHelper.Instance.Search.List(DefaultSearchResultProperty);
            searchListRequest.Q = searchString;
            searchListRequest.MaxResults = MaxResultsPerRequest;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            searchListRequest.Type = SearchResultType;
            searchListRequest.VideoCategoryId = TrailerVideoCategoryId;
            searchListRequest.VideoDuration = SearchResource.ListRequest.VideoDurationEnum.Short__;
            searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
            HandleDateParameter(searchListRequest, releaseDate);
            return searchListRequest;
        }

        private static void HandleDateParameter(SearchResource.ListRequest searchRequest, DateTime? releaseDate)
        {
            //trailers usually come out in the year prior the release of the movie
            //no upper bound, a post-release trailer is still good (old movies for example, also youtube started in 2005)
            if (releaseDate.HasValue)
            {
                searchRequest.PublishedAfter = releaseDate.Value.Subtract(new TimeSpan(365, 0, 0, 0));
            }
        }
    }
}