using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieAggregator.Client.DTOs;

namespace MovieAggregator.Client.ViewModels
{
    public class MovieContentEntryViewModel
    {
        public MovieInfoViewModel Info { get; set; }
        public MovieTrailerViewModel Trailer { get; set; }

        public MovieContentEntryViewModel(MovieContentEntryDTO dto)
        {
            if (dto == null)
                throw new ArgumentException($"{nameof(dto)} cannot be null");

            Info = new MovieInfoViewModel(dto.Info);
            Trailer = new MovieTrailerViewModel(dto.Trailer);
        }

        public bool HasTrailer()
        {
            return Trailer != null && !string.IsNullOrWhiteSpace(Trailer.EmbedUrl);
        }
    }
}