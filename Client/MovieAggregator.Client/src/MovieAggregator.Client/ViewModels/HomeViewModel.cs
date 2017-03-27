using MovieAggregator.Client.DTOs;

namespace MovieAggregator.Client.ViewModels
{
    public class HomeViewModel
    {
        public string SearchQuery
        {
            get;
            set;
        }

        public MovieAggregatedContentDTO MovieInfo
        {
            get;
            set;
        }
    }
}
