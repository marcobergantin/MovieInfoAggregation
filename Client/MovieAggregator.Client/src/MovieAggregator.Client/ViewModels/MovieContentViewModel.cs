using MovieAggregator.Client.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAggregator.Client.ViewModels
{
    public class MovieContentViewModel
    {
        public byte PageIndex { get; set; }
        public uint PageSize { get; set; }
        public uint NumberOfPages { get; set; }
        public IEnumerable<MovieContentEntryViewModel> Entries { get; set; }

        public MovieContentViewModel(MovieContentDTO dto)
        {
            if (dto != null)
            {
                PageIndex = dto.PageIndex;
                NumberOfPages = dto.NumberOfPages;
                if (dto.Entries != null)
                {
                    PageSize = (uint)dto.Entries.Count();
                    AddEntriesFromDTO(dto.Entries);
                }
            }
        }

        private void AddEntriesFromDTO(IEnumerable<MovieContentEntryDTO> entries)
        {
            List<MovieContentEntryViewModel> entriesViewModels = new List<MovieContentEntryViewModel>();
            foreach (var entry in entries)
            {
                entriesViewModels.Add(new MovieContentEntryViewModel(entry));
            }

            Entries = entriesViewModels;
        }
    }
}