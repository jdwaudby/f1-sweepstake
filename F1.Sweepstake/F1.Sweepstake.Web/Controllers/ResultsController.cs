using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
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

        // POST api/results
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Player>>> Post([FromBody]IEnumerable<Player> players)
        {
            throw new NotImplementedException();
        }

        // POST api/results/round/5
        [HttpPost("round/{round}")]
        public async Task<ActionResult<IEnumerable<Player>>> Post(int round, [FromBody]IEnumerable<Player> players)
        {
            throw new NotImplementedException();
        }
    }
}
