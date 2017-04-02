using MovieAggregator.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface IMovieInfoProvider
    {
        Task<IEnumerable<MovieInfoDTO>> GetInfo(string searchString);
    }
}