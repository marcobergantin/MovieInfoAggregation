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

        public MovieContentEntryViewModel(MovieContentEntryDTO movieContent)
        {
            if (movieContent.Info != null)
            {
                Info = new MovieInfoViewModel(movieContent.Info);
            }

            if (movieContent.Trailer != null)
            {
                Trailer = new MovieTrailerViewModel(movieContent.Trailer);
            }
        }

        public bool HasTrailer()
        {
            return Trailer != null && !string.IsNullOrWhiteSpace(Trailer.EmbedUrl);
        }
    }
}