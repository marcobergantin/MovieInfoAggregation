using MovieAggregator.WebApi.Cache;
using System;
using System.Configuration;

namespace MovieAggregator.WebApi.App_Start
{
    public static class CacheConfig
    {
        public static CacheType GetConfigurableCache()
        {
            string configCacheType = ConfigurationManager.AppSettings["CacheType"];
            CacheType cacheType;
            Enum.TryParse(configCacheType, out cacheType);
            return cacheType;
        }
    }
}