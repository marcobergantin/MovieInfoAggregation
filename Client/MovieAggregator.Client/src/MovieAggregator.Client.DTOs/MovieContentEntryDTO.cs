using System;

namespace MovieAggregator.Client.DTOs
{
    public class MovieContentEntryDTO
    {
        public MovieInfoDTO Info { get; set; }
        public MovieTrailerDTO Trailer { get; set; }
    }
}