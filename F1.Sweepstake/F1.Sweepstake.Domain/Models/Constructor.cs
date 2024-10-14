using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace F1.Sweepstake.Domain.Models
{
    public class Constructor
    {
        public string Name { get; set; }
        [JsonIgnore]
        public IEnumerable<Driver> Drivers { get; set; }
    }
}
