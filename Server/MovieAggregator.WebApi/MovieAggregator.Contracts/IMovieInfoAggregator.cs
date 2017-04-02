using MovieAggregator.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface IMovieInfoAggregator
    {
        Task<MovieContentDTO> GetAggregatedInfo(string searchString);
    }
}