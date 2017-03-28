namespace MovieAggregator.Contracts
{
    public interface IMovieCacheRepository
    {
        void Add(string searchString, IMovieCacheEntry content);
        IMovieCacheEntry Get(string searchString);
        void Remove(IMovieCacheEntry content);
    }
}
