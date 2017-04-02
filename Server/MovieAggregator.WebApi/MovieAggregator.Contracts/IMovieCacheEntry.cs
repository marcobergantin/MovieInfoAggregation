using MovieAggregator.DTOs;
using System;

namespace MovieAggregator.Contracts
{
    public interface IMovieCacheEntry
    {
        string SeachString { get; set; }
        MovieContentDTO Data { get; set; }
        DateTime TimeStamp { get; set; }
    }
}