using MovieAggregator.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface IMovieCacheService
    {
        Task AddToCache(string searchString, MovieContentDTO content);
        Task<MovieContentDTO> GetFromCache(string searchString);
        void SetExpirationInterval(uint seconds);
    }
}