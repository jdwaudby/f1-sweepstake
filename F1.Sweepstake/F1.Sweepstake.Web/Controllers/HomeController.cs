using System.Collections.Generic;
using System.Linq;
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
            assignments = assignments.ToList();

            foreach (Assignment assignment in assignments.Where(assignment => assignment.Player.Hidden))
            {
                assignment.Driver = null;
                assignment.Constructor = null;
            }

            return View(assignments.OrderBy(assignment => assignment.Player.Code));
        }

        public IActionResult Results([FromBody] IEnumerable<Result> results)
        {
            foreach (Result result in results.Where(result => result.Player?.Hidden == true))
            {
                result.Player = null;
            }

            return View(results);
        }

        public IActionResult Standings([FromBody] IEnumerable<Standing> standings)
        {
            return View(standings.Where(standing => !standing.Player.Hidden));
        }
    }
}
