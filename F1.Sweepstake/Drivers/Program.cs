using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Ergast.Library;
using Newtonsoft.Json;

namespace Drivers
{
    internal class Program
    {
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://ergast.com/api/f1/") };
        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

        private static async Task Main()
        {
            Console.WriteLine("Enter number of participants:");
            string input = Console.ReadLine();
            int total = int.Parse(input);

            Console.WriteLine("Enter current round number:");
            input = Console.ReadLine();
            int round = int.Parse(input);

            string constructorsJson = await Client.GetStringAsync($"current/{round}/constructors.json");
            var constructors = JsonConvert.DeserializeObject<RootObject>(constructorsJson).MRData.ConstructorTable.Constructors;

            var drivers = new List<string>();
            foreach (Constructor constructor in constructors)
            {
                string driversJson = await Client.GetStringAsync($"current/{round}/constructors/{constructor.ConstructorId}/drivers.json");
                var constructorDrivers = JsonConvert.DeserializeObject<RootObject>(driversJson).MRData.DriverTable.Drivers.Select(driver => $"{driver.GivenName} {driver.FamilyName}, {constructor.Name}");
                drivers.AddRange(constructorDrivers);
            }

            int listsRequired = (total - 1) / drivers.Count + 1;

            var driverList = new List<string>();
            for (int i = 0; i < listsRequired; i++)
            {
                driverList.AddRange(drivers.OrderBy(x =>
                {
                    var bytes = new byte[4];
                    Provider.GetBytes(bytes);
                    return BitConverter.ToUInt32(bytes, 0);
                }));
            }

            // Final list
            foreach (var item in driverList.Take(total).Select((driver, i) => new { index = i + 1, driver }))
            {
                Console.WriteLine($"{item.index} {item.driver}");
            }
        }
    }
}
