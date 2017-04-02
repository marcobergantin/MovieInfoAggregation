using System.Collections.Generic;

namespace MovieAggregator.DTOs
{
    public class MovieContentDTO
    {
        public byte PageIndex { get; set; }
        public uint NumberOfPages { get; set; }
        public IEnumerable<MovieContentEntryDTO> Entries { get; set; }
    }
}