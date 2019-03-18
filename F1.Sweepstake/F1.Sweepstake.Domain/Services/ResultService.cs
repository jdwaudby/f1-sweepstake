using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using F1.Sweepstake.Domain.Models;
using F1.Sweepstake.Domain.Models.Ergast;
using F1.Sweepstake.Domain.Services.Interfaces;
using Newtonsoft.Json;

namespace F1.Sweepstake.Domain.Services
{
    public class ResultService : IResultService
    {
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("https://ergast.com/api/f1/") };

        public async Task<IEnumerable<Result>> Get()
        {
            return await Get("last");
        }

        public async Task<IEnumerable<Result>> Get(int round)
        {
            return await Get(round.ToString());
        }

        public async Task<IEnumerable<Player>> Get(IEnumerable<Player> players)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Player>> Get(int round, IEnumerable<Player> players)
        {
            throw new System.NotImplementedException();
        }

        private static async Task<IEnumerable<Result>> Get(string round)
        {
            string resultsJson = await Client.GetStringAsync($"current/{round}/results.json");
            return JsonConvert.DeserializeObject<Models.Ergast.RootObject>(resultsJson).MRData.RaceTable.Races.SingleOrDefault()?.Results;
        }
    }
}
