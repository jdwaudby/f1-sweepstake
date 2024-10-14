﻿using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1.Sweepstake.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    // POST api/drivers/assign
    [HttpPost("assign")]
    public async Task<ActionResult<IEnumerable<Assignment>>> Assign([FromBody] IEnumerable<Standing> standings)
    {
        var assignments = await _driverService.Assign(standings.Select(standing => standing.Player));
        return assignments.ToList();
    }

    // POST api/drivers/round/5/assign
    [HttpPost("round/{round}/assign")]
    public async Task<ActionResult<IEnumerable<Assignment>>> Assign(int round, [FromBody] IEnumerable<Standing> standings)
    {
        var assignments = await _driverService.Assign(round, standings.Select(standing => standing.Player));
        return assignments.ToList();
    }
}
