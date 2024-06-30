using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Trip2.Models;

namespace Trip2.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Destination()
        {
            return View("Destination");
        }
        public IActionResult SearchDestination()
        {

            return View();
        }
        public IActionResult About()
        {
            return View("About");
        }

        public IActionResult ContactUs()
        {
            return View("ContactUs");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}