using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserActionTrackingApp.Models;

namespace UserActionTrackingApp.Controllers
{
    public class HomeController : AbstractBaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // gives the page name to the GenerateUserTrackingMessage in the AbstractBaseController
        public IActionResult Index()
        {
            ViewBag.UserTrackingMessage = GenerateUserTrackingMessage("HomeIndex");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}