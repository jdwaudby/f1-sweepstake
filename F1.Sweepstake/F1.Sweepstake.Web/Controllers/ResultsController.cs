using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Models.Ergast;
using F1.Sweepstake.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Result = F1.Sweepstake.Domain.Models.Result;

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
        public async Task<ActionResult<IEnumerable<Result>>> Post([FromBody]IEnumerable<Assignment> assignments)
        {
            var results = await _resultService.Get(assignments);
            return results.ToList();
        }

        // POST api/results/round/5
        [HttpPost("round/{round}")]
        public async Task<ActionResult<IEnumerable<Result>>> Post(int round, [FromBody]IEnumerable<Assignment> assignments)
        {
            var results = await _resultService.Get(round, assignments);
            return results.ToList();
        }
    }
}
