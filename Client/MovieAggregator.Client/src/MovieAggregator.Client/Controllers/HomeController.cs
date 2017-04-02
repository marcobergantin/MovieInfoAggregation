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

        public async Task<IActionResult> ShowPage(string searchQuery, byte pageIndex)
        {
            var movieInfo = await _movieService.GetMovieInfo(searchQuery, pageIndex);
            var homeViewModel = new HomeViewModel();
            homeViewModel.SearchQuery = searchQuery;
            homeViewModel.MovieInfo = new MovieContentViewModel(movieInfo);
            return View("Index", homeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel inputViewModel)
        {
            var movieInfo = await _movieService.GetMovieInfo(inputViewModel.SearchQuery);
            inputViewModel.MovieInfo = new MovieContentViewModel(movieInfo);
            return View("Index", inputViewModel);
        }
    }
}
