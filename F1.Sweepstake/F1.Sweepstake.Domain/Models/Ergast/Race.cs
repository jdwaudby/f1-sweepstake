using System.Collections.Generic;

namespace F1.Sweepstake.Domain.Models.Ergast
{
    public class Race
    {
        public IEnumerable<Result> Results { get; set; }
    }
}
