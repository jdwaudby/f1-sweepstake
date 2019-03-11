using System;
using System.Collections.Generic;
using System.Text;

namespace F1.Sweepstake.Domain.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DriverConstructor Assignment { get; set; }
    }
}
