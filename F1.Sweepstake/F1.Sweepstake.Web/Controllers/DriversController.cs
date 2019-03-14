using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace F1.Sweepstake.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        // GET api/drivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> Get()
        {
            var drivers = await _driverService.GetAll();
            return drivers.ToList();
        }

        // GET api/drivers/round/5
        [HttpGet("round/{round}")]
        public async Task<ActionResult<IEnumerable<Driver>>> Get(int round)
        {
            var drivers = await _driverService.GetAll(round);
            return drivers.ToList();
        }

        // POST api/drivers/assign
        [HttpPost("assign")]
        public async Task<ActionResult<IEnumerable<Player>>> Assign([FromBody] IEnumerable<Player> players)
        {
            players = await _driverService.Assign(players);
            return players.ToList();
        }

        // POST api/drivers/round/5/assign
        [HttpPost("round/{round}/assign")]
        public async Task<ActionResult<IEnumerable<Player>>> Assign(int round, [FromBody] IEnumerable<Player> players)
        {
            players = await _driverService.Assign(round, players);
            return players.ToList();
        }
    }
}
