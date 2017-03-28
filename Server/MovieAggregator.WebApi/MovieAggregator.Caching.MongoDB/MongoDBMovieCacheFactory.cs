using MovieAggregator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieAggregator.DTOs;
using MovieAggregator.Caching.MongoDB.Entities;

namespace MovieAggregator.Caching.MongoDB
{
    public class MongoDBMovieCacheFactory : IMovieCacheEntityFactory
    {
        public IMovieCacheEntry CreateEntry(MovieAggregatedContentDTO data)
        {
            if (data == null)
                return null;

            return new MongoDBMovieCacheEntry(data);
        }
    }
}
