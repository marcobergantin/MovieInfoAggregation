using MovieAggregator.DTOs;

namespace MovieAggregator.Contracts
{
    public interface IMovieCacheEntityFactory
    {
        IMovieCacheEntry CreateEntry(MovieAggregatedContentDTO data);
    }
}
