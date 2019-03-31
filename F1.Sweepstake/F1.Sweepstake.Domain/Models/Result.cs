namespace F1.Sweepstake.Domain.Models
{
    public class Result : Assignment
    {
        public bool Finished { get; set; }
        public int Position { get; set; }
        public bool FastestLap { get; set; }
        public int Points { get; set; }
    }
}
