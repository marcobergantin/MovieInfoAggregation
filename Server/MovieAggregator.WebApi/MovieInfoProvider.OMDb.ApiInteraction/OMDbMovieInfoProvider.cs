using MovieAggregator.DTOs;
using MovieAggregator.Contracts;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MovieInfoProvider.OMDb.ApiInteraction
{
    public class OMDbMovieInfoProvider : IMovieInfoProvider
    {
        private const string BaseUrl = "http://www.omdbapi.com/";
        private const string TitleParamUrlKey = "t";
        private const string TypeParamKey = "type";
        private const string DefaultTypeValue = "movie";

        public async Task<IEnumerable<MovieInfoDTO>> GetInfo(string movieTitle)
        {
            List<MovieInfoDTO> resultList = new List<MovieInfoDTO>(1); //OMDb api always returns only 1 result
            using (HttpClient client = new HttpClient())
            {
                string urlEncodedMovieTitle = Uri.EscapeUriString(movieTitle);
                string requestUri = $"{BaseUrl}?{TitleParamUrlKey}={urlEncodedMovieTitle}&{TypeParamKey}={DefaultTypeValue}";
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var info = await response.Content.ReadAsAsync<MovieInfoDTO>();
                    if (info != null)
                    {
                        resultList.Add(info);
                    }
                }
            }

            return resultList;
        }
    }
}