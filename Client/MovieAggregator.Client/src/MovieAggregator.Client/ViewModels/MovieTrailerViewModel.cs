using MovieAggregator.Client.DTOs;
using System;

namespace MovieAggregator.Client.ViewModels
{
    public class MovieTrailerViewModel
    {
        public string EmbedUrl { get; set; }

        public MovieTrailerViewModel(MovieTrailerDTO dto)
        {
            if (dto == null)
                throw new ArgumentException($"{nameof(dto)} cannot be null");

            if (Uri.IsWellFormedUriString(dto.EmbedURL, UriKind.Absolute))
            {
                EmbedUrl = dto.EmbedURL;
            }
        }
    }
}