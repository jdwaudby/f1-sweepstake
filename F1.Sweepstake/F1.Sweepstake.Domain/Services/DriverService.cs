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
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://ergast.com/api/f1/") };
        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

        public async Task<IEnumerable<Driver>> GetAll()
        {
            string constructorsJson = await Client.GetStringAsync($"current/constructors.json");
            var constructors = JsonConvert.DeserializeObject<Models.Ergast.RootObject>(constructorsJson).MRData.ConstructorTable.Constructors;

            var drivers = new List<Driver>();
            foreach (Models.Ergast.Constructor constructor in constructors)
            {
                string driversJson = await Client.GetStringAsync($"current/constructors/{constructor.ConstructorId}/drivers.json");
                var constructorDrivers = JsonConvert.DeserializeObject<Models.Ergast.RootObject>(driversJson).MRData.DriverTable.Drivers.Select(driver => new Driver
                {
                    DriverId = driver.DriverId,
                    PermanentNumber = driver.PermanentNumber,
                    GivenName = driver.GivenName,
                    FamilyName = driver.FamilyName,
                    Constructor = constructor
                });
                drivers.AddRange(constructorDrivers);
            }

            return drivers;
        }

        public async Task<IEnumerable<Driver>> GetAll(int round)
        {
            string constructorsJson = await Client.GetStringAsync($"current/{round}/constructors.json");
            var constructors = JsonConvert.DeserializeObject<Models.Ergast.RootObject>(constructorsJson).MRData.ConstructorTable.Constructors;

            var drivers = new List<Driver>();
            foreach (Models.Ergast.Constructor constructor in constructors)
            {
                string driversJson = await Client.GetStringAsync($"current/{round}/constructors/{constructor.ConstructorId}/drivers.json");
                var constructorDrivers = JsonConvert.DeserializeObject<Models.Ergast.RootObject>(driversJson).MRData.DriverTable.Drivers.Select(driver => new Driver
                {
                    DriverId = driver.DriverId,
                    PermanentNumber = driver.PermanentNumber,
                    GivenName = driver.GivenName,
                    FamilyName = driver.FamilyName,
                    Constructor = constructor
                });
                drivers.AddRange(constructorDrivers);
            }

            return drivers;
        }

        public async Task<IEnumerable<Player>> Assign(IEnumerable<Player> players)
        {
            var drivers = await GetAll();

            var driverList = new List<Driver>();
            driverList.AddRange(drivers.OrderBy(x =>
            {
                var bytes = new byte[4];
                Provider.GetBytes(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }));

            var playerList = players.ToList();
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].Assignment = driverList[i];
            }

            return playerList;
        }

        public async Task<IEnumerable<Player>> Assign(int round, IEnumerable<Player> players)
        {
            var drivers = await GetAll(round);

            var driverList = new List<Driver>();
            driverList.AddRange(drivers.OrderBy(x =>
            {
                var bytes = new byte[4];
                Provider.GetBytes(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }));

            var playerList = players.ToList();
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].Assignment = driverList[i];
            }

            return playerList;
        }
    }
}
