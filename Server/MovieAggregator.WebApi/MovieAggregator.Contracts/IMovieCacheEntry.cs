using MovieAggregator.DTOs;
using System;

namespace MovieAggregator.Contracts
{
    public interface IMovieCacheEntry
    {
        MovieAggregatedContentDTO Data { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
