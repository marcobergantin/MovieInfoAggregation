using MovieAggregator.DTOs;

namespace MovieAggregator.Contracts
{
    public interface IMovieCache
    {
        void AddToCache(string searchString, MovieAggregatedContentDTO content);
        MovieAggregatedContentDTO GetFromCache(string searchString);
    }
}
