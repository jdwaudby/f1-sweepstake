using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace F1.Sweepstake.Domain.Services
{
    public class DriverService : IDriverService
    {
        private readonly IConstructorService _constructorService;

        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

        public DriverService(IConstructorService constructorService)
        {
            _constructorService = constructorService;
        }

        public async Task<IEnumerable<Driver>> Get()
        {
            return await Get(0);
        }

        public async Task<IEnumerable<Driver>> Get(int round)
        {
            IEnumerable<Constructor> constructors = await _constructorService.Get(round);
            return constructors.SelectMany(constructor => constructor.Drivers);
        }

        public async Task<IEnumerable<Player>> Assign(IEnumerable<Player> players)
        {
            return await Assign(0, players);
        }

        public async Task<IEnumerable<Player>> Assign(int round, IEnumerable<Player> players)
        {
            IEnumerable<Driver> drivers = await Get(round);

            var driverList = new List<Driver>();
            driverList.AddRange(drivers.OrderBy(x =>
            {
                var bytes = new byte[4];
                Provider.GetBytes(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }));

            List<Player> playerList = players.ToList();
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].Driver = driverList[i];
            }

            return playerList;
        }
    }
}
