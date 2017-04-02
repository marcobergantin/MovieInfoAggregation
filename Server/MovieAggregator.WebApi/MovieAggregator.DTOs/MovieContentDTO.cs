using System.Collections.Generic;

namespace MovieAggregator.DTOs
{
    public class MovieContentDTO
    {
        public IEnumerable<MovieContentEntryDTO> Entries { get; set; }
    }
}