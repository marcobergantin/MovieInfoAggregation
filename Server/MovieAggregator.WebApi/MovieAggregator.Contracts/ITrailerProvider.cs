using MovieAggregator.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface ITrailerProvider
    {
        Task<MovieTrailerDTO> GetTrailer(string movieTitle);
    }
}
