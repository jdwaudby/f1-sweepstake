using System.Collections.Generic;

namespace F1.Sweepstake.Domain.Models
{
    public class Constructor
    {
        public string ConstructorId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
    }
}
