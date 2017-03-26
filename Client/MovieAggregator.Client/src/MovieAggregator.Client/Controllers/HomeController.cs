using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAggregator.Client.Controllers
{
    public class HomeController
    {
        private static string MovieInfoEndpoint;

        public HomeController(IConfiguration configuration)
        {
            if (MovieInfoEndpoint == null)
            {
                MovieInfoEndpoint = configuration["Endpoints:MovieInfoEndpoint"];
            }
        }

        public string Index()
        {          
            return MovieInfoEndpoint;
        }
    }
}
