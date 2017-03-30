using System.Threading.Tasks;

namespace MovieAggregator.Contracts
{
    public interface IMovieCacheRepository
    {
        Task Add(string searchString, IMovieCacheEntry content);
        Task<IMovieCacheEntry> Get(string searchString);
        Task Remove(string searchString);
    }
}
