using MovieAggregator.DTOs;
using System;
using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface ITrailerProvider
    {
        Task<MovieTrailerDTO> GetTrailer(string searchString, DateTime? releaseDate);
    }
}
