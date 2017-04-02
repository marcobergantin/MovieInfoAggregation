using MovieAggregator.DTOs;
using MovieAggregator.Contracts;
using System.Threading.Tasks;
using System;
using Google.Apis.YouTube.v3.Data;
using VideoProvider.Youtube.ApiInteraction.Helpers;

namespace VideoProvider.Youtube.ApiInteraction
{
    public class YoutubeVideoProvider : ITrailerProvider
    {       
        public async Task<MovieTrailerDTO> GetTrailer(string movieTitle, DateTime? releaseDate)
        {
            if (ValidateReleaseDate(releaseDate) == false)
                throw new ArgumentException("ReleaseDate is not valid");

            return await SearchVideo(movieTitle, releaseDate);
        }

        private bool ValidateReleaseDate(DateTime? releaseDate)
        {
            //let's include all the movies from the beginning of cinema to unreleased with a 5 year scope
            return releaseDate.HasValue == false ||
                (releaseDate.HasValue && (releaseDate.Value.Year > 1890 || releaseDate.Value < DateTime.Now.AddYears(5)));
        }

        public async Task<MovieTrailerDTO> SearchVideo(string searchQuery, DateTime? releaseDate)
        {
            try
            {
                var searchListRequest = YoutubeRequestHelper.BuildRequest(searchQuery, releaseDate);
                var searchListResponse = await searchListRequest.ExecuteAsync();
                if (searchListResponse != null && searchListResponse.Items != null)
                {
                    var searchResult = YoutubeSearchResultsHelper.FilterSearchResults(searchQuery,
                                                                                      (releaseDate.HasValue ? releaseDate.Value.Year.ToString() : null),
                                                                                      searchListResponse.Items);
                    if (searchResult != null)
                    {
                        return MapToDTO(searchResult);
                    }
                }
            }
            catch
            {
                //log exception
            }

            return null;
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