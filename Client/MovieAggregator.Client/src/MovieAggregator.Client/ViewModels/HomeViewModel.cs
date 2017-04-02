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

        public MovieContentViewModel MovieInfo
        {
            get;
            set;
        }

        public bool HasEntries()
        {
            return MovieInfo != null && MovieInfo.Entries != null && MovieInfo.Entries.Count() > 0;
        }

        public bool CanShowPages()
        {
            return HasEntries() && MovieInfo.PageIndex >= 0 && MovieInfo.NumberOfPages > 0;
        }
    }
}