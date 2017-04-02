using Microsoft.Extensions.Configuration;
using MovieAggregator.Client.DTOs;
using MovieAggregator.Client.Interfaces;
using System.Threading.Tasks;
using MovieAggregator.Client.Services.Helpers;

namespace MovieAggregator.Client.Services
{
    public class MovieInfoService : IMovieService
    {
        private static string MovieInfoEndpoint;

        public MovieInfoService(IConfiguration configuration)
        {
            if (MovieInfoEndpoint == null)
            {
                MovieInfoEndpoint = configuration["Endpoints:MovieInfoEndpoint"];
            }
        }

        public async Task<MovieContentDTO> GetMovieInfo(string movieTitle)
        {
            try
            {
                return await HttpHelper.GetFromApi<MovieContentDTO>(GetUrlWithMovieTitleParameter(movieTitle));
            }
            catch
            {
                return null;
            }            
        }

        public async Task<MovieContentDTO> GetMovieInfo(string movieTitle, byte pageIndex)
        {
            try
            {
                return await HttpHelper.GetFromApi<MovieContentDTO>(GetPagedUrl(movieTitle, pageIndex));
            }
            catch
            {
                return null;
            }
        }

        private string GetUrlWithMovieTitleParameter(string movieTitle)
        {
            return $"{MovieInfoEndpoint}api/Movie?movieTitle={movieTitle}";
        }

        private string GetPagedUrl(string movieTitle, byte pageIndex)
        {
            return $"{GetUrlWithMovieTitleParameter(movieTitle)}&pageIndex={pageIndex}";
        }
    }
}