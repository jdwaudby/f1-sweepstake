using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace F1.Sweepstake.Domain.Services
{
    public class DriverService : IDriverService
    {
        private readonly IConstructorService _constructorService;

        public DriverService(IConstructorService constructorService)
        {
            _constructorService = constructorService;
        }

        public async Task<IEnumerable<Assignment>> Assign(IEnumerable<Player> players)
        {
            return await Assign(0, players);
        }

        public async Task<IEnumerable<Assignment>> Assign(int round, IEnumerable<Player> players)
        {
            using var rng = RandomNumberGenerator.Create();
            var drivers = await Get(round);

            var driverList = new List<DriverConstructor>();
            driverList.AddRange(drivers.OrderBy(x =>
            {
                var bytes = new byte[4];
                rng.GetBytes(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }));

            var assignments = players.Select(player => new Assignment {Player = player}).ToList();
            for (int i = 0; i < assignments.Count; i++)
            {
                assignments[i].Driver = driverList[i].Driver;
                assignments[i].Constructor = driverList[i].Constructor;
            }

            return assignments;
        }

        private async Task<IEnumerable<DriverConstructor>> Get(int round)
        {
            return from constructor in await _constructorService.Get(round)
                from driver in constructor.Drivers
                select new DriverConstructor {Driver = driver, Constructor = constructor};
        }
    }
}
