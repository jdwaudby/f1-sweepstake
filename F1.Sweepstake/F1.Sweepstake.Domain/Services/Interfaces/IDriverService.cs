using System.Collections.Generic;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;

namespace F1.Sweepstake.Domain.Services.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Assignment>> Assign(IEnumerable<Player> players);
        Task<IEnumerable<Assignment>> Assign(int round, IEnumerable<Player> players);
    }
}
