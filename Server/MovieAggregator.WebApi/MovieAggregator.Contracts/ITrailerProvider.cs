using MovieAggreagator.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface ITrailerProvider
    {
        Task<MovieTrailerDTO> GetTrailer(string movieTitle);
    }
}
