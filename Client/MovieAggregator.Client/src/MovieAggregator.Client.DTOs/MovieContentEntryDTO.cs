using System;

namespace MovieAggregator.Client.DTOs
{
    public class MovieContentEntryDTO
    {
        public MovieInfoDTO Info { get; set; }
        public MovieTrailerDTO Trailer { get; set; }

        public bool HasTrailer()
        {
            return Trailer != null && Uri.IsWellFormedUriString(Trailer.EmbedURL, UriKind.Absolute);
        }

        public string GetReleaseYearString()
        {
            if (Info != null)
            {
                string yearString = Info.GetReleaseYear();
                if (string.IsNullOrWhiteSpace(yearString) == false)
                {
                    return $"({yearString})";
                }
            }

            return string.Empty;
        }
    }
}