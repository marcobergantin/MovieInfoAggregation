using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace MovieAggregator.Client.Controllers
{
    public class HomeController : Controller
    {
        private static string MovieInfoEndpoint;

        public HomeController(IConfiguration configuration)
        {
            if (MovieInfoEndpoint == null)
            {
                MovieInfoEndpoint = configuration["Endpoints:MovieInfoEndpoint"];
            }
        }

        public async Task<IActionResult> Index()
        {
            string resultString = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(MovieInfoEndpoint + "api/Movie?movieTitle=" + "spiderman");
                response.EnsureSuccessStatusCode();
                resultString = await response.Content.ReadAsStringAsync();
            }

            return View("Index", resultString);
        }
    }
}
