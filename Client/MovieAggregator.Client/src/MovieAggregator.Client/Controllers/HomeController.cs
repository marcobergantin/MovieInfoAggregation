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

        public async Task<IActionResult> Index(string searchQuery = null, byte pageIndex = 0)
        {
            var homeViewModel = new HomeViewModel();
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                var movieInfo = await _movieService.GetMovieInfo(searchQuery, pageIndex);
                homeViewModel.SearchQuery = searchQuery;
                homeViewModel.MovieInfo = new MovieContentViewModel(movieInfo);
            }

            return View("Index", homeViewModel);
        }
    }
}