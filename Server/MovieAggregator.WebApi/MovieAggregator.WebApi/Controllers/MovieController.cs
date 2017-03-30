using MovieAggregator.Contracts;
using System;
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
            if (string.IsNullOrWhiteSpace(movieTitle))
                return BadRequest();

            try
            {
                var data = await _movieService.GetAggregatedInfo(movieTitle);
                if (data != null)
                {
                    return Ok(data);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
