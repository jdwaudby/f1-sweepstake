using System.Diagnostics;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1.Sweepstake.Web.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Assignments([FromBody] IList<Assignment> assignments)
    {
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

    public IActionResult Standings([FromBody] IList<Standing> standings)
    {
        int temp = 0;
        int lastRank = 0;
        for (int i = 0; i < standings.Count; i++)
        {
            if (standings[i].Player.Hidden)
            {
                int newRank = standings[i].Rank - temp;
                if (i + 1 < standings.Count && standings[i].Rank != standings[i + 1].Rank && newRank != lastRank)
                {
                    temp++;
                }
            }

            standings[i].Rank -= temp;
            lastRank = standings[i].Rank;
        }

        return View(standings.Where(standing => !standing.Player.Hidden));
    }
}
