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

            Parallel.ForEach(searchResults.Results, r =>
                returnList.Add(new MovieInfoDTO()
                    {
                        Title = r.Title,
                        Plot = r.Overview,
                        Released = r.ReleaseDate,
                        Language = r.OriginalLanguage,
                        Poster = TMDbImageHelper.GetImageUrl(r.PosterPath)
                })
            );

            return returnList;
        }
    }
}