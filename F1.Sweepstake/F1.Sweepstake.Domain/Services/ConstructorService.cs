using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using Newtonsoft.Json;

namespace F1.Sweepstake.Domain.Services
{
    public class ConstructorService : IConstructorService
    {
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://ergast.com/api/f1/") };

        public async Task<IEnumerable<Constructor>> Get(int round)
        {
            string constructorsJson = await Client.GetStringAsync($"current/{round}/constructors.json");
            IEnumerable<Task<Constructor>> constructors = JsonConvert.DeserializeObject<Models.Ergast.RootObject>(constructorsJson).MRData.ConstructorTable.Constructors.Select(async constructor =>
            {
                string driversJson = await Client.GetStringAsync($"current/{round}/constructors/{constructor.ConstructorId}/drivers.json");
                IEnumerable<Driver> drivers = JsonConvert.DeserializeObject<Models.Ergast.RootObject>(driversJson).MRData.DriverTable.Drivers.Select(driver => new Driver
                {
                    DriverNumber = driver.PermanentNumber,
                    GivenName = driver.GivenName,
                    FamilyName = driver.FamilyName,
                    Code = driver.Code
                });

                return new Constructor
                {
                    Name = constructor.Name,
                    Drivers = drivers
                };
            });

            return await Task.WhenAll(constructors);
        }
    }
}
