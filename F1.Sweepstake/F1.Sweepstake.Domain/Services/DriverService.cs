using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace F1.Sweepstake.Domain.Services
{
    public class DriverService : IDriverService
    {
        public IEnumerable<Driver> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Driver> GetAll(int round)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> Assign(IEnumerable<Player> players)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> Assign(int round, IEnumerable<Player> players)
        {
            throw new NotImplementedException();
        }
    }
}
