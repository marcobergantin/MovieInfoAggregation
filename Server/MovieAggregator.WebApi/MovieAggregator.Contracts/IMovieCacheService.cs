using MovieAggregator.DTOs;

namespace MovieAggregator.Contracts
{
    public interface IMovieCacheService
    {
        void AddToCache(string searchString, MovieAggregatedContentDTO content);
        MovieAggregatedContentDTO GetFromCache(string searchString);
        void SetExpirationInterval(uint seconds);
    }
}
