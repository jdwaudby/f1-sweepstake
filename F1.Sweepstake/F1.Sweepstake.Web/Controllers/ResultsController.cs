﻿using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1.Sweepstake.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResultsController : ControllerBase
{
    private readonly IResultService _resultService;

    public ResultsController(IResultService resultService)
    {
        _resultService = resultService;
    }

    // POST api/results
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Result>>> Post([FromBody] IEnumerable<Assignment> assignments)
    {
        var results = await _resultService.Get(assignments);
        return results.ToList();
    }

    // POST api/results/round/5
    [HttpPost("round/{round}")]
    public async Task<ActionResult<IEnumerable<Result>>> Post(int round, [FromBody] IEnumerable<Assignment> assignments)
    {
        var results = await _resultService.Get(round, assignments);
        return results.ToList();
    }

    // POST api/results/standings
    [HttpPost("standings")]
    public ActionResult<IEnumerable<Standing>> Standings([FromBody] IEnumerable<Result> results)
    {
        return results.Where(result => result.Player != null)
            .Select(result => result.Player)
            .OrderByDescending(player => player.TotalPoints)
            .GroupBy(player => player.TotalPoints)
            .SelectMany((group, index) => group.Select(player => new Standing
            {
                Rank = index + 1,
                Player = player
            }))
            .OrderBy(standing => standing.Rank)
            .ThenBy(standing => standing.Player.TotalRetirements)
            .ThenBy(standing => standing.Player.Code)
            .ToList();
    }
}
