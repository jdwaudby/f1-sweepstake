using System.Collections.Generic;

namespace F1.Sweepstake.Domain.Models.Ergast
{
    public class DriverTable
    {
        public string ConstructorId { get; set; }
        public List<Driver> Drivers { get; set; }
    }
}
