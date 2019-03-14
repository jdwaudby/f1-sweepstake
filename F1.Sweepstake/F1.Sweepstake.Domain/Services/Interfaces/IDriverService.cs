using System.Collections.Generic;
using F1.Sweepstake.Domain.Models;

namespace F1.Sweepstake.Domain.Services.Interfaces
{
    public interface IDriverService
    {
        IEnumerable<Driver> GetAll();
        IEnumerable<Driver> GetAll(int round);
        IEnumerable<Player> Assign(IEnumerable<Player> players);
        IEnumerable<Player> Assign(int round, IEnumerable<Player> players);
    }
}
