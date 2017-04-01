using MovieAggregator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieAggregator.DTOs;

namespace MovieAggregator.WebApi.Cache
{
    public class InMemoryMovieCacheEntry : IMovieCacheEntry
    {
        public MovieAggregatedContentDTO Data { get; set; }
        public string SeachString { get; set; }
        public DateTime TimeStamp { get; set; }

        public InMemoryMovieCacheEntry(string searchString, MovieAggregatedContentDTO data)
        {
            SeachString = searchString;
            Data = data;
            TimeStamp = DateTime.UtcNow;
        }
    }
}