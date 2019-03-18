using System.Collections.Generic;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Models.Ergast;

namespace F1.Sweepstake.Domain.Services.Interfaces
{
    public interface IResultService
    {
        Task<IEnumerable<Result>> Get();
        Task<IEnumerable<Result>> Get(int round);
        Task<IEnumerable<Player>> Get(IEnumerable<Player> players);
        Task<IEnumerable<Player>> Get(int round, IEnumerable<Player> players);
    }
}
