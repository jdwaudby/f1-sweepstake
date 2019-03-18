using System.Collections.Generic;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Models.Ergast;
using F1.Sweepstake.Domain.Services.Interfaces;

namespace F1.Sweepstake.Domain.Services
{
    public class ResultService : IResultService
    {
        public async Task<IEnumerable<Result>> Get()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Result>> Get(int round)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Player>> Get(IEnumerable<Player> players)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Player>> Get(int round, IEnumerable<Player> players)
        {
            throw new System.NotImplementedException();
        }
    }
}
