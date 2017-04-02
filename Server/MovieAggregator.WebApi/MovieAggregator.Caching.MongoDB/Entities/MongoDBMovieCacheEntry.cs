using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MovieAggregator.Caching.Common;
using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System;

namespace MovieAggregator.Caching.MongoDB.Entities
{
    public class MongoDBMovieCacheEntry : BaseMovieCacheEntry
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public MongoDBMovieCacheEntry(string searchString, MovieContentDTO data) :
            base(searchString, data)
        {
            _id = ObjectId.GenerateNewId();
        }
    }
}
