using MovieAggregator.Client.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.Client.Interfaces
{
    public interface IMovieService
    {
        Task<MovieAggregatedContentDTO> GetMovieInfo(string movieTitle);
    }
}
