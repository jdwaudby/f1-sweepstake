using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Services.Interfaces;

namespace F1.Sweepstake.Domain.Services;

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

    private static async Task<IEnumerable<Result>> Get(string round)
    {
        string resultsJson = await Client.GetStringAsync($"current/{round}/results.json");
        var results = JsonSerializer.Deserialize<Models.Ergast.RootObject>(resultsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, NumberHandling = JsonNumberHandling.AllowReadingFromString });
        return results.MRData.RaceTable.Races.SingleOrDefault()?.Results.Select(result => new Result
        {
            Driver = new Driver
            {
                Code = result.Driver.Code,
                DriverNumber = result.Driver.PermanentNumber,
                GivenName = result.Driver.GivenName,
                FamilyName = result.Driver.FamilyName
            },
            Constructor = new Constructor
            {
                Name = result.Constructor.Name
            },
            FastestLap = result.FastestLap != null && result.FastestLap.Rank == "1",
            Finished = result.Status == "Finished" || result.Status.StartsWith("+"),
            Points = Convert.ToInt32(result.Points),
            Position = Convert.ToInt32(result.Position)
        });
    }

    private static async Task<IEnumerable<Result>> Get(string round, IEnumerable<Assignment> assignments)
    {
        assignments = assignments.ToList();

        List<Result> results = (await Get(round)).ToList();

        decimal prizeFund = 6;
        foreach (Result result in results.OrderBy(result => result.Position))
        {
            result.Player = assignments.SingleOrDefault(assignment => assignment.Driver.DriverNumber == result.Driver.DriverNumber)?.Player;

            if (result.Player == null)
            {
                continue;
            }

            result.Player.TotalPoints += result.Points;

            if (prizeFund > 0 && !result.Player.Hidden)
            {
                result.Winnings = (int)Math.Ceiling(prizeFund / 2);
                prizeFund -= result.Winnings;

                result.Player.TotalWinnings += result.Winnings;
            }

            if (result.Finished)
            {
                continue;
            }

            result.Player.TotalRetirements++;
        }

        List<Assignment> missing = assignments.Where(assignment => results.All(result => result.Player != assignment.Player)).ToList();
        if (missing.Any())
        {
            throw new Exception($"Unable to determine results for {missing.Count} players.");
        }

        return results;
    }
}
