using MongoDB.Bson;
using MovieAggregator.DTOs;
using System;

namespace MovieAggregator.Caching.MongoDB.Entities
{
    public class MovieCacheEntry
    {
        public ObjectId _id { get; set; }
        public MovieAggregatedContentDTO Data { get; set; }
        public DateTime TimeStamp { get; set; }

        public MovieCacheEntry(MovieAggregatedContentDTO data)
        {
            Data = data;
            TimeStamp = DateTime.UtcNow;
        }
    }
}
