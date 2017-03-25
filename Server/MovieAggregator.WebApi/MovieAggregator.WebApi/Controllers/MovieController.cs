using MovieAggregator.Contracts;
using MovieInfoProvider.OMDb.ApiInteraction;
using System.Threading.Tasks;
using System.Web.Http;
using VideoProvider.Youtube.ApiInteraction;
using MovieAggregator.DTOs;

namespace MovieAggregator.WebApi.Controllers
{
    public class MovieController : ApiController
    {
        ITrailerProvider _videoProvider = new YoutubeVideoProvider();
        IMovieInfoProvider _infoProvider = new OMDbMovieInfoProvider();

        public async Task<IHttpActionResult> Get(string movieTitle)
        {
            var info = await _infoProvider.GetInfo(movieTitle);

            return Ok(new MovieAggregatedContentDTO()
                {
                    Info = info,
                    Trailer = await _videoProvider.GetTrailer(info.Title)
                }
            );
        }
    }
}
