using System.Collections.Generic;
using System.Linq;
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
        [HttpGet()]
        public ActionResult<IEnumerable<Driver>> Get()
        {
            return _driverService.GetAll().ToList();
        }

        // GET api/drivers/round/5
        [HttpGet("round/{round}")]
        public ActionResult<IEnumerable<Driver>> Get(int round)
        {
            return _driverService.GetAll(round).ToList();
        }

        // POST api/drivers/assign
        [HttpPost("assign")]
        public ActionResult<IEnumerable<Player>> Assign([FromBody] IEnumerable<Player> players)
        {
            return _driverService.Assign(players).ToList();
        }

        // POST api/drivers/round/5/assign
        [HttpPost("round/{round}/assign")]
        public ActionResult<IEnumerable<Player>> Assign(int round, [FromBody] IEnumerable<Player> players)
        {
            return _driverService.Assign(round, players).ToList();
        }
    }
}
