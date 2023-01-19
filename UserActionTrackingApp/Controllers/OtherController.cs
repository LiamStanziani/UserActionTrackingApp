using Microsoft.AspNetCore.Mvc;

using UserActionTrackingApp.Models;

namespace UserActionTrackingApp.Controllers
{
    public class OtherController : AbstractBaseController
    {
        // gives the page name to the GenerateUserTrackingMessage in the AbstractBaseController
        public IActionResult Index()
        {
            ViewBag.UserTrackingMessage = GenerateUserTrackingMessage("OtherIndex");
            return View();
        }
    }
}
