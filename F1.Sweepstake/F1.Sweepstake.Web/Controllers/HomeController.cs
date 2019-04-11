using System.Collections.Generic;
using F1.Sweepstake.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1.Sweepstake.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Assignments([FromBody] IEnumerable<Assignment> assignments)
        {
            return View(assignments);
        }

        public IActionResult Results([FromBody] IEnumerable<Result> results)
        {
            return View(results);
        }

        public IActionResult Standings([FromBody] IEnumerable<Standing> standings)
        {
            return View(standings);
        }
    }
}
