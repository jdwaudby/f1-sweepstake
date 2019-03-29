using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Models.Ergast;
using F1.Sweepstake.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1.Sweepstake.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultsController(IResultService resultService)
        {
            _resultService = resultService;
        }

        // GET api/results
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Result>>> Get()
        //{
        //    var results = await _resultService.Get();
        //    return results.ToList();
        //}

        //// GET api/results
        //[HttpGet("round/{round}")]
        //public async Task<ActionResult<IEnumerable<Result>>> Get(int round)
        //{
        //    var results = await _resultService.Get(round);
        //    return results.ToList();
        //}

        //// POST api/results
        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<Player>>> Post([FromBody]IEnumerable<Player> players)
        //{
        //    players = await _resultService.Get(players);
        //    return players.ToList();
        //}

        //// POST api/results/round/5
        //[HttpPost("round/{round}")]
        //public async Task<ActionResult<IEnumerable<Player>>> Post(int round, [FromBody]IEnumerable<Player> players)
        //{
        //    players = await _resultService.Get(round, players);
        //    return players.ToList();
        //}
    }
}
