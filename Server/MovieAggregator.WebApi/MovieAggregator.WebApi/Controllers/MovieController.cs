using MovieAggregator.Contracts;
using System.Threading.Tasks;
using System.Web.Http;

namespace MovieAggregator.WebApi.Controllers
{
    public class MovieController : ApiController
    {
        IMovieInfoAggregator _movieService;

        public MovieController(IMovieInfoAggregator movieService)
        {
            _movieService = movieService;
        }

        public async Task<IHttpActionResult> Get(string movieTitle)
        {
            return Ok(await _movieService.GetAggregatedInfo(movieTitle));
        }
    }
}
