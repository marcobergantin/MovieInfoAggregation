using MovieAggregator.Client.DTOs;
using System;

namespace MovieAggregator.Client.ViewModels
{
    public class MovieInfoViewModel
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public DateTime? Released { get; set; }
        public string Plot { get; set; }
        public string PosterUrl { get; set; }

        public MovieInfoViewModel(MovieInfoDTO movieInfo)
        {
            if (movieInfo != null)
            {
                Title = movieInfo.Title;
                Year = movieInfo.Year;
                Released = movieInfo.Released;
                Plot = movieInfo.Plot;

                if (Uri.IsWellFormedUriString(movieInfo.Poster, UriKind.Absolute))
                {
                    PosterUrl = movieInfo.Poster;
                }
            }
        }

        public string GetDetailsHeader()
        {
            return $"{Title}{GetReleaseYearString()}";
        }

        private string GetReleaseYearString()
        {
            string yearString = GetReleaseYear();
            if (string.IsNullOrWhiteSpace(yearString) == false)
            {
                return $" ({yearString})";
            }

            return string.Empty;
        }

        private string GetReleaseYear()
        {
            if (Released.HasValue)
                return Released.Value.Year.ToString();

            if (string.IsNullOrWhiteSpace(Year) == false)
            {
                return Year;
            }

            return string.Empty;
        }
    }
}