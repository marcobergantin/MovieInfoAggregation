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

        public async Task<MovieContentDTO> GetMovieInfo(string movieTitle, byte pageIndex = 0)
        {
            try
            {
                return await HttpHelper.GetFromApi<MovieContentDTO>(GetPagedUrl(movieTitle, pageIndex));
            }
            catch
            {
                //log exception
                return null;
            }
        }

        private string GetPagedUrl(string movieTitle, byte pageIndex)
        {
            return $"{MovieInfoEndpoint}api/Movie?movieTitle={movieTitle}&pageIndex={pageIndex}";
        }
    }
}