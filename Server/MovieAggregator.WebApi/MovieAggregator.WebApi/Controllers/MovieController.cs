using MovieAggregator.Contracts;
using System.Threading.Tasks;
using System.Web.Http;
using MovieAggregator.DTOs;

namespace MovieAggregator.WebApi.Controllers
{
    public class MovieController : ApiController
    {
        ITrailerProvider _trailerProvider;
        IMovieInfoProvider _infoProvider;

        public MovieController(IMovieInfoProvider infoProvider, ITrailerProvider trailerProvider)
        {
            _infoProvider = infoProvider;
            _trailerProvider = trailerProvider;
        }

        public async Task<IHttpActionResult> Get(string movieTitle)
        {
            var info = await _infoProvider.GetInfo(movieTitle);

            return Ok(new MovieAggregatedContentDTO()
                {
                    Info = info,
                    Trailer = await _trailerProvider.GetTrailer(info.Title)
                }
            );
        }
    }
}
