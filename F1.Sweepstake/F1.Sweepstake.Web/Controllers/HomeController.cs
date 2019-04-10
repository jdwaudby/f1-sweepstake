using Microsoft.AspNetCore.Mvc;

namespace F1.Sweepstake.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
