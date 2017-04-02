using System.Collections.Generic;

namespace MovieAggregator.Client.DTOs
{
    public class MovieContentDTO
    {
        public IEnumerable<MovieContentEntryDTO> Entries { get; set; }
    }
}
