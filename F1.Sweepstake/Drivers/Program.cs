using System;
using System.Collections.Generic;
using System.Linq;

namespace Drivers
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Enter number of participants:");
            string input = Console.ReadLine();

            int total = int.Parse(input);

            string[] drivers = {
                "Sebastian Vettel, Ferrari",
                "Kimi Räikkönen, Ferrari",
                "Sergio Pérez, Force India-Mercedes",
                "Esteban Ocon, Force India-Mercedes",
                "Romain Grosjean, Haas-Ferrari",
                "Kevin Magnussen, Haas-Ferrari",
                "Stoffel Vandoorne, McLaren-Renault",
                "Fernando Alonso, McLaren-Renault",
                "Lewis Hamilton, Mercedes",
                "Valtteri Bottas, Mercedes",
                "Daniel Ricciardo, Red Bull Racing-TAG Heuer",
                "Max Verstappen, Red Bull Racing-TAG Heuer",
                "Nico Hülkenberg, Renault",
                "Carlos Sainz Jr., Renault",
                "Marcus Ericsson, Sauber-Ferrari",
                "Charles Leclerc, Sauber-Ferrari",
                "Pierre Gasly, Scuderia Toro Rosso-Honda",
                "Brendon Hartley, Scuderia Toro Rosso-Honda",
                "Lance Stroll, Williams-Mercedes",
                "Sergey Sirotkin, Williams-Mercedes"
            };

            var random = new Random();
            int listsRequired = (total - 1) / drivers.Length + 1;

            var driverList = new List<string>();
            for (int i = 0; i < listsRequired; i++)
            {
                driverList.AddRange(drivers.OrderBy(x => random.Next()));
            }

            // Final list
            foreach (var item in driverList.Take(total).Select((driver, i) => new { index = i + 1, driver }))
            {
                Console.WriteLine($"{item.index} {item.driver}");
            }
        }
    }
}
