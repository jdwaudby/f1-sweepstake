using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;
using Newtonsoft.Json;

namespace F1.Sweepstake.Domain.Services
{
    public class ResultService : IResultService
    {
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://ergast.com/api/f1/") };

        public async Task<IEnumerable<Result>> Get(IEnumerable<Assignment> assignments)
        {
            return await Get("last", assignments);
        }

        public async Task<IEnumerable<Result>> Get(int round, IEnumerable<Assignment> assignments)
        {
            return await Get(round.ToString(), assignments);
        }

        private static async Task<IEnumerable<Models.Ergast.Result>> Get(string round)
        {
            string resultsJson = await Client.GetStringAsync($"current/{round}/results.json");
            return JsonConvert.DeserializeObject<Models.Ergast.RootObject>(resultsJson).MRData.RaceTable.Races.SingleOrDefault()?.Results;
        }

        private static async Task<IEnumerable<Result>> Get(string round, IEnumerable<Assignment> assignments)
        {
            List<Models.Ergast.Result> results = (await Get(round)).ToList();

            List<Result> localResults = assignments.Select(assignment => new Result
            {
                Player = assignment.Player,
                Driver = assignment.Driver,
                Constructor = assignment.Constructor
            }).ToList();

            foreach (Result localResult in localResults)
            {
                var driverResult = results.Where(result => result.Driver.PermanentNumber == localResult.Driver.DriverNumber).Select(result => new
                {
                    Finished = result.Status == "Finished" || result.Status.StartsWith("+"),
                    Position = Convert.ToInt32(result.Position),
                    FastestLap = result.FastestLap.Rank == "1",
                    Points = Convert.ToInt32(result.Points)
                }).SingleOrDefault();

                if (driverResult == null)
                {
                    throw new NullReferenceException($"Could not find result for driver {localResult.Driver.DriverNumber}, {localResult.Driver.GivenName} {localResult.Driver.FamilyName}");
                }

                localResult.Finished = driverResult.Finished;
                localResult.Position = driverResult.Position;
                localResult.Points = driverResult.Points;
                localResult.FastestLap = driverResult.FastestLap;

                localResult.Player.TotalPoints += driverResult.Points;

                if (driverResult.Finished)
                {
                    continue;
                }

                localResult.Player.TotalRetirements += 1;
            }

            return localResults.OrderBy(result => result.Position);
        }
    }
}
