namespace F1.Sweepstake.Domain.Models
{
    public class Result
    {
        public int Position { get; set; }
        public Player Player { get; set; }
        public Driver Driver { get; set; }
        public Constructor Constructor { get; set; }
        public bool Finished { get; set; }
        public bool FastestLap { get; set; }
        public int Points { get; set; }
        public int Winnings { get; set; }
    }
}
