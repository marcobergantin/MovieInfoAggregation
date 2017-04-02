using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;

namespace MovieAggregator.TMDb.ApiInteraction
{
    public class TMDbMovieInfoProvider : IMovieInfoProvider
    {    
        static readonly string ApiKey = ConfigurationManager.AppSettings["tmdbApiKey"];
        static readonly string BaseImagesUrl = ConfigurationManager.AppSettings["tmdbImagesBaseUrl"];
        static TMDbClient client;

        const string ImageSizeParam = "w500";
        private static TMDbClient Client
        {
            get
            {
                return client ?? (client = new TMDbClient(ApiKey));
            }
        }

        private static TMDbClient GetConfifuredClient()
        {
            var client = Client;
            var config = new TMDbConfig();
            config.Images = new ConfigImageTypes();
            config.Images.BaseUrl = BaseImagesUrl;
            client.SetConfig(config);
            return client;
        }

        public async Task<IEnumerable<MovieInfoDTO>> GetInfo(string searchString)
        {
            var searchResults = await GetConfifuredClient().SearchMovieAsync(searchString);
            List<MovieInfoDTO> returnList = new List<MovieInfoDTO>();

            searchResults.Results.ForEach(r =>
                returnList.Add(new MovieInfoDTO()
                    {
                        Title = r.Title,
                        Plot = r.Overview,
                        Released = r.ReleaseDate,
                        Language = r.OriginalLanguage,
                        Poster = Client.GetImageUrl(ImageSizeParam, r.PosterPath).AbsoluteUri
                })
            );

            return returnList;
        }
    }
}