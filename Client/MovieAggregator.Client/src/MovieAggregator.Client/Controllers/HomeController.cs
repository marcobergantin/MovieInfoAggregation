using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MovieAggregator.Client.Interfaces;
using MovieAggregator.Client.ViewModels;

namespace MovieAggregator.Client.Controllers
{
    public class HomeController : Controller
    {
        IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index", new HomeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel inputViewModel)
        {
            var movieInfo = await _movieService.GetMovieInfo(inputViewModel.SearchQuery);
            inputViewModel.MovieInfo = movieInfo;
            return View("Index", inputViewModel);
        }
    }
}
