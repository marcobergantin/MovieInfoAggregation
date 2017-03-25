﻿using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MovieAggreagator.DTOs;
using MovieAggregator.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoProvider.Youtube.ApiInteraction
{
    public class YoutubeVideoProvider : ITrailerProvider
    {
        private static readonly string ClientID = "tamtamassignment-162520";//ConfigurationManager.AppSettings["GoogleApisClientID"];
        private static readonly string ClientSecret = "AIzaSyC5YxkHzX9YHBbcWP981DbYZJcZdnAX45Q";//ConfigurationManager.AppSettings["GoogleApisClientSecret"];

        public async Task<MovieTrailerDTO> GetTrailer(string movieTitle)
        {
            return await SearchVideo(movieTitle + " trailer");
        }

        public async Task<MovieTrailerDTO> SearchVideo(string searchQuery)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ClientSecret,
                ApplicationName = ClientID
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchQuery;
            searchListRequest.MaxResults = 3;

            var searchListResponse = await searchListRequest.ExecuteAsync();
            //Items will contain matching videos, channels, and playlists. Filtering on videos only
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        return new MovieTrailerDTO
                        {
                            VideoTitle = searchResult.Snippet.Title,
                            VideoURL = $"https://www.youtube.com/watch?v={searchResult.Id.VideoId}"
                        };
                }
            }

            return null;
        }
    }
}
