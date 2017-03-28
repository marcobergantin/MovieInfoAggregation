﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System;

namespace MovieAggregator.Caching.MongoDB.Entities
{
    public class MongoDBMovieCacheEntry : IMovieCacheEntry
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public MovieAggregatedContentDTO Data { get; set; }
        public DateTime TimeStamp { get; set; }

        public MongoDBMovieCacheEntry(MovieAggregatedContentDTO data)
        {
            _id = ObjectId.GenerateNewId();
            Data = data;
            TimeStamp = DateTime.UtcNow;
        }
    }
}
