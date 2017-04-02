using Microsoft.Extensions.Configuration;
using MovieAggregator.Client.DTOs;
using MovieAggregator.Client.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

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
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(MovieInfoEndpoint + "api/Movie?movieTitle=" + movieTitle);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<MovieContentDTO>();
                }
            }
            catch
            {
                return null;
            }            
        }
    }
}
