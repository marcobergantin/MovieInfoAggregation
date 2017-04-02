using System.Configuration;
using TMDbLib.Client;
using TMDbLib.Objects.General;

namespace MovieAggregator.TMDb.ApiInteraction.Helpers
{
    public static class TMDbClientHelper
    {
        static readonly string ApiKey = ConfigurationManager.AppSettings["tmdbApiKey"];
        static readonly string BaseImagesUrl = ConfigurationManager.AppSettings["tmdbImagesBaseUrl"];
        static TMDbClient client;

        private static TMDbClient Client
        {
            get
            {
                return client ?? (client = new TMDbClient(ApiKey));
            }
        }

        public static TMDbClient GetConfifuredClient()
        {
            var client = Client;
            if (client.HasConfig == false)
            {
                var config = new TMDbConfig();
                config.Images = new ConfigImageTypes();
                config.Images.BaseUrl = BaseImagesUrl;
                client.SetConfig(config);
            }
            return client;
        }
    }
}