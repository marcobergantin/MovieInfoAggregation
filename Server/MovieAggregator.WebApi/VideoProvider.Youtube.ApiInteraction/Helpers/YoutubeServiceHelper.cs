using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Configuration;

namespace VideoProvider.Youtube.ApiInteraction.Helpers
{
    public static class YoutubeServiceHelper
    {
        private static readonly string ClientID = ConfigurationManager.AppSettings["GoogleApisClientID"];
        private static readonly string ClientSecret = ConfigurationManager.AppSettings["GoogleApisClientSecret"];
        private static YouTubeService youtubeService;

        public static YouTubeService Instance
        {
            get
            {
                return youtubeService ?? (youtubeService = 
                    new YouTubeService(
                        new BaseClientService.Initializer()
                            {
                                ApiKey = ClientSecret,
                                ApplicationName = ClientID
                            }   
                        ));
            }
        }
    }
}