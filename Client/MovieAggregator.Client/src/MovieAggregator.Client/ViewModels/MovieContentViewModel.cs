using MovieAggregator.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (dto == null)
                throw new ArgumentException($"{nameof(dto)} cannot be null");

            PageIndex = dto.PageIndex;
            if (dto.Entries != null)
            {
                PageSize = (uint)dto.Entries.Count();
                List<MovieContentEntryViewModel> entriesViewModels = new List<MovieContentEntryViewModel>();
                foreach (var e in dto.Entries)
                {
                    entriesViewModels.Add(new MovieContentEntryViewModel(e));
                }

                Entries = entriesViewModels;
            }
        }
    }
}
