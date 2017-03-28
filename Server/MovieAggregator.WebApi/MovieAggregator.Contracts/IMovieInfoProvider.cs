using MovieAggregator.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface IMovieInfoProvider
    {
        Task<MovieInfoDTO> GetInfo(string searchString);
    }
}
