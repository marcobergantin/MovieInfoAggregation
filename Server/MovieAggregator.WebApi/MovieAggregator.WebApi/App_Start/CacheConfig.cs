using MovieAggregator.WebApi.Cache;
using MovieAggregator.WebApi.Helpers;

namespace MovieAggregator.WebApi.App_Start
{
    public static class CacheConfig
    {
        public static CacheType GetConfigurableCacheType()
        {
            return ConfigurationHelper.GetValueFromConfiguration<CacheType>("CacheType");
        }

        public static uint GetConfigurableCacheExpirationInterval()
        {
            return ConfigurationHelper.GetValueFromConfiguration<uint>("CacheExiprationInterval");
        }
    }
}