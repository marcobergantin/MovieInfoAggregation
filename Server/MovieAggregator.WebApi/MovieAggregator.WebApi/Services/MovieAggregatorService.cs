using MovieAggregator.Contracts;
using MovieAggregator.DTOs;
using System.Threading.Tasks;

namespace MovieAggregator.WebApi.Services
{
    public class MovieAggregatorService : IMovieInfoAggregator
    {
        ITrailerProvider _trailerProvider;
        IMovieInfoProvider _infoProvider;

        public MovieAggregatorService(IMovieInfoProvider infoProvider, ITrailerProvider trailerProvider)
        {
            _infoProvider = infoProvider;
            _trailerProvider = trailerProvider;
        }

        public async Task<MovieAggregatedContentDTO> GetAggregatedInfo(string movieTitle)
        {
            var info = await _infoProvider.GetInfo(movieTitle);

            return new MovieAggregatedContentDTO()
            {
                Info = info,
                Trailer = await _trailerProvider.GetTrailer(info.Title)
            };
        }
    }
}