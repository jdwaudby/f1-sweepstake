using System.Collections.Generic;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;

namespace F1.Sweepstake.Domain.Services.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> Get();
        Task<IEnumerable<Driver>> Get(int round);
        Task<IEnumerable<Player>> Assign(IEnumerable<Player> players);
        Task<IEnumerable<Player>> Assign(int round, IEnumerable<Player> players);
    }
}
