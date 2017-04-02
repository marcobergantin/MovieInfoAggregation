using MovieAggregator.Client.DTOs;
using System;

namespace MovieAggregator.Client.ViewModels
{
    public class MovieTrailerViewModel
    {
        public string EmbedUrl { get; set; }

        public MovieTrailerViewModel(MovieTrailerDTO trailer)
        {
            if (trailer != null)
            {
                if (Uri.IsWellFormedUriString(trailer.EmbedURL, UriKind.Absolute))
                {
                    EmbedUrl = trailer.EmbedURL;
                }
            }
        }
    }
}