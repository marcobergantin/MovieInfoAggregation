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

        public MovieInfoViewModel(MovieInfoDTO dto)
        {
            if (dto == null)
                throw new ArgumentException($"{nameof(dto)} cannot be null");

            Title = dto.Title;
            Year = dto.Year;
            Released = dto.Released;
            Plot = dto.Plot;

            if (Uri.IsWellFormedUriString(dto.Poster, UriKind.Absolute))
            {
                PosterUrl = dto.Poster;
            }
        }

        public string GetDetailsHeader()
        {
            return $"{Title}{GetReleaseYearString()}";
        }

        public string GetReleaseYearString()
        {
            string yearString = GetReleaseYear();
            if (string.IsNullOrWhiteSpace(yearString) == false)
            {
                return $" ({yearString})";
            }

            return string.Empty;
        }

        public string GetReleaseYear()
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