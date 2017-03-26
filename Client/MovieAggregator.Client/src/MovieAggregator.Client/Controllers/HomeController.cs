using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MovieAggregator.Client.Interfaces;

namespace MovieAggregator.Client.Controllers
{
    public class HomeController : Controller
    {
        IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index(string movieTitle)
        {
            return View("Index", await _movieService.GetMovieInfo(movieTitle));
        }
    }
}
