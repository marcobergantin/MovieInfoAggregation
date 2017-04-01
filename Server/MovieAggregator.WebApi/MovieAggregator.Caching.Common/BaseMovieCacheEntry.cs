using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System;

namespace MovieAggregator.Caching.Common
{
    //base class for cache entities, provides the minimal functionalities required
    //did not provide a base factory class because of this http://stackoverflow.com/questions/35463287/generic-implementation-of-interface-with-specified-type
    public class BaseMovieCacheEntry : IMovieCacheEntry
    {
        public MovieAggregatedContentDTO Data { get; set; }
        public DateTime TimeStamp { get; set; }
        public string SeachString { get; set; }

        public BaseMovieCacheEntry(string searchString, MovieAggregatedContentDTO data)
        {
            SeachString = searchString;
            Data = data;
            TimeStamp = DateTime.UtcNow;
        }
    }
}
