using MovieAggregator.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface IMovieCacheService
    {
        Task AddToCache(string searchString, MovieAggregatedContentDTO content);
        Task<MovieAggregatedContentDTO> GetFromCache(string searchString);
        void SetExpirationInterval(uint seconds);
    }
}
