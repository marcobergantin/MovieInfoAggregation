﻿using MovieAggreagator.DTOs;
using MovieAggregator.Contracts;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieInfoProvider.OMDb.ApiInteraction
{
    public class OMDbMovieInfoProvider : IMovieInfoProvider
    {
        private const string BaseUrl = "http://www.omdbapi.com/?";
        private const string TitleParamUrlKey = "t";

        public async Task<MovieInfoDTO> GetInfo(string movieTitle)
        {
            using (HttpClient client = new HttpClient())
            {
                string urlEncodedMovieTitle = Uri.EscapeUriString(movieTitle);
                string requestUri = $"{BaseUrl}{TitleParamUrlKey}={urlEncodedMovieTitle}";
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                   return await response.Content.ReadAsAsync<MovieInfoDTO>();
                }
            }


            return null;
        }
    }
}
