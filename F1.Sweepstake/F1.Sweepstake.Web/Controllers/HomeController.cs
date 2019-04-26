using System.Collections.Generic;
using System.Linq;
using F1.Sweepstake.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1.Sweepstake.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string Temp = "MCG";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Assignments([FromBody] IEnumerable<Assignment> assignments)
        {
            foreach (Assignment assignment in assignments)
            {
                if (assignment.Player.Code != Temp)
                {
                    continue;
                }

                assignment.Driver = null;
                assignment.Constructor = null;
            }

            return View(assignments.OrderBy(assignment => assignment.Player.Code));
        }

        public IActionResult Results([FromBody] IEnumerable<Result> results)
        {
            foreach (Result result in results)
            {
                if (result.Player?.Code == Temp)
                {
                    result.Player = null;
                }
            }

            return View(results);
        }

        public IActionResult Standings([FromBody] IEnumerable<Standing> standings)
        {
            return View(standings.Where(standing => standing.Player.Code != Temp));
        }
    }
}
