using System;
using System.Collections.Generic;
using System.Text;

namespace F1.Sweepstake.Domain.Models
{
    public class Result : Assignment
    {
        public int Position { get; set; }
        public int Points { get; set; }
        public bool FastestLap { get; set; }
        public bool Finished { get; set; }
    }
}
