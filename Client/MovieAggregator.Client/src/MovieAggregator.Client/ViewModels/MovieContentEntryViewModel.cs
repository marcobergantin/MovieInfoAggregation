using MovieAggregator.Client.DTOs;

namespace MovieAggregator.Client.ViewModels
{
    public class MovieContentEntryViewModel
    {
        public MovieInfoViewModel Info { get; set; }
        public MovieTrailerViewModel Trailer { get; set; }

        public MovieContentEntryViewModel(MovieContentEntryDTO movieContent)
        {
            if (movieContent.Info != null)
            {
                Info = new MovieInfoViewModel(movieContent.Info);
            }

            if (movieContent.Trailer != null)
            {
                Trailer = new MovieTrailerViewModel(movieContent.Trailer);
            }
        }

        public string GetDetailsHeader()
        {
            if (Info == null)
                return string.Empty;

            return $"{Info.Title}{Info.GetReleaseYearString()}";
        }

        public string GetPosterUrl()
        {
            if (Info == null)
                return string.Empty;

            return Info.PosterUrl;
        }

        public bool HasTrailer()
        {
            return Trailer != null && !string.IsNullOrWhiteSpace(Trailer.EmbedUrl);
        }
    }
}