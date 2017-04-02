using MovieAggregator.Client.DTOs;
using System.Linq;

namespace MovieAggregator.Client.ViewModels
{
    public class HomeViewModel
    {
        public string SearchQuery
        {
            get;
            set;
        }

        public MovieContentDTO MovieInfo
        {
            get;
            set;
        }

        public bool HasEntries()
        {
            return MovieInfo != null && MovieInfo.Entries != null && MovieInfo.Entries.Count() > 0;
        }
    }
}