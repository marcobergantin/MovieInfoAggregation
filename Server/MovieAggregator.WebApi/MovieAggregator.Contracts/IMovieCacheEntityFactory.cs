using MovieAggregator.DTOs;

namespace MovieAggregator.Contracts
{
    public interface IMovieCacheEntityFactory
    {
        IMovieCacheEntry CreateEntry(string searchString, MovieContentDTO data);
    }
}