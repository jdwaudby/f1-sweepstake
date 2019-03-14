using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using Newtonsoft.Json;

namespace F1.Sweepstake.Drivers
{
    internal class Program
    {
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://ergast.com/api/f1/") };
        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

        private static async Task Main()
        {
            List<Player> players;
            using (var reader = new StreamReader("players.json"))
            {
                string playersJson = reader.ReadToEnd();
                players = JsonConvert.DeserializeObject<List<Player>>(playersJson);
            }

            Console.WriteLine("Enter current round number:");
            string input = Console.ReadLine();
            int round = int.Parse(input);

            string constructorsJson = await Client.GetStringAsync($"current/{round}/constructors.json");
            var constructors = JsonConvert.DeserializeObject<Domain.Models.Ergast.RootObject>(constructorsJson).MRData.ConstructorTable.Constructors;

            var drivers = new List<Driver>();
            foreach (Domain.Models.Ergast.Constructor constructor in constructors)
            {
                string driversJson = await Client.GetStringAsync($"current/{round}/constructors/{constructor.ConstructorId}/drivers.json");
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

            var driverList = new List<Driver>();
            driverList.AddRange(drivers.OrderBy(x =>
            {
                var bytes = new byte[4];
                Provider.GetBytes(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }));

            for (int i = 0; i < players.Count; i++)
            {
                players[i].Assignment = driverList[i];
            }

            // Final list
            foreach (Player player in players)
            {
                Console.WriteLine($"{player.PlayerId} {player.GivenName} {player.FamilyName} - {player.Assignment.PermanentNumber} {player.Assignment.GivenName} {player.Assignment.FamilyName}, {player.Assignment.Constructor.Name}");
            }
        }
    }
}
