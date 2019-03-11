using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Models.Ergast;
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
            var constructors = JsonConvert.DeserializeObject<RootObject>(constructorsJson).MRData.ConstructorTable.Constructors;

            var drivers = new List<DriverConstructor>();
            foreach (Constructor constructor in constructors)
            {
                string driversJson = await Client.GetStringAsync($"current/{round}/constructors/{constructor.ConstructorId}/drivers.json");
                var constructorDrivers = JsonConvert.DeserializeObject<RootObject>(driversJson).MRData.DriverTable.Drivers.Select(driver => new DriverConstructor { Driver = driver, Constructor = constructor });
                drivers.AddRange(constructorDrivers);
            }

            var driverList = new List<DriverConstructor>();
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
                Console.WriteLine($"{player.PlayerId} {player.GivenName} {player.FamilyName} - {player.Assignment.Driver.PermanentNumber} {player.Assignment.Driver.GivenName} {player.Assignment.Driver.FamilyName}, {player.Assignment.Constructor.Name}");
            }
        }
    }
}
