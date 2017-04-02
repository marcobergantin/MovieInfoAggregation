using MovieAggregator.Client.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.Client.Interfaces
{
    public interface IMovieService
    {
        Task<MovieContentDTO> GetMovieInfo(string movieTitle);
    }
}
