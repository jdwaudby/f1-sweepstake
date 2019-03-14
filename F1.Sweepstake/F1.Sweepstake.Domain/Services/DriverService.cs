using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace F1.Sweepstake.Domain.Services
{
    public class DriverService : IDriverService
    {
        public async Task<IEnumerable<Driver>> GetAll()
        {
            var client = new HttpClient {BaseAddress = new Uri("https://ergast.com/api/f1/")};

            string constructorsJson = await client.GetStringAsync($"current/constructors.json");
            var constructors = JsonConvert.DeserializeObject<Domain.Models.Ergast.RootObject>(constructorsJson).MRData.ConstructorTable.Constructors;

            var drivers = new List<Driver>();
            foreach (Domain.Models.Ergast.Constructor constructor in constructors)
            {
                string driversJson = await client.GetStringAsync($"current/constructors/{constructor.ConstructorId}/drivers.json");
                var constructorDrivers = JsonConvert.DeserializeObject<Domain.Models.Ergast.RootObject>(driversJson).MRData.DriverTable.Drivers.Select(driver => new Driver
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
