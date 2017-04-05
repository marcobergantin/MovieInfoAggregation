using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using MovieAggregator.TMDb.ApiInteraction.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAggregator.TMDb.ApiInteraction
{
    public class TMDbMovieInfoProvider : IMovieInfoProvider
    {    
        public async Task<IEnumerable<MovieInfoDTO>> GetInfo(string searchString)
        {
            var client = TMDbClientHelper.GetConfifuredClient();
            var searchResults = await client.SearchMovieAsync(searchString);
            List<MovieInfoDTO> returnList = new List<MovieInfoDTO>();
            foreach (var result in searchResults.Results)
            {
                returnList.Add(new MovieInfoDTO()
                {
                    Title = result.Title,
                    Plot = result.Overview,
                    Released = result.ReleaseDate,
                    Language = result.OriginalLanguage,
                    Poster = TMDbImageHelper.GetImageUrl(result.PosterPath)
                });
            }

            return returnList;
        }
    }
}