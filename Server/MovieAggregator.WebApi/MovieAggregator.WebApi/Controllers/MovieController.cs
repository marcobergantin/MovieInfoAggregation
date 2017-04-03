using MovieAggregator.Contracts;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using MovieAggregator.DTOs;

namespace MovieAggregator.WebApi.Controllers
{
    public class MovieController : ApiController
    {
        const byte DefaultPageSize = 3;

        IMovieInfoAggregator _movieService;

        public MovieController(IMovieInfoAggregator movieService)
        {
            _movieService = movieService;
        }

        public async Task<IHttpActionResult> Get(string movieTitle, byte pageIndex = 0, byte pageSize = DefaultPageSize)
        {
            if (string.IsNullOrWhiteSpace(movieTitle))
                return BadRequest();

            try
            {
                var data = await _movieService.GetAggregatedInfo(movieTitle);
                if (data != null)
                {
                    return Ok(HandlePagination(data, pageIndex, pageSize));
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private MovieContentDTO HandlePagination(MovieContentDTO data, byte pageIndex, byte pageSize)
        {
            data.PageIndex = pageIndex;
            if (data.Entries != null)
            {
                data.NumberOfPages = (uint)(Math.Ceiling(data.Entries.Count() / (double)pageSize));
                //avoids skipping all results if pageIndex is too big
                data.PageIndex = (byte)Math.Min(pageIndex, data.NumberOfPages - 1); 
                data.Entries = data.Entries.Skip(data.PageIndex * pageSize).Take(pageSize);
            }
            return data;
        }
    }
}