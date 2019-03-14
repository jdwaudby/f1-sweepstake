namespace F1.Sweepstake.Domain.Models.Ergast
{
    public class Result
    {
        public string Position { get; set; }
        public string Points { get; set; }
        public Driver Driver { get; set; }
        public Constructor Constructor { get; set; }
        public string Grid { get; set; }
        public string Laps { get; set; }
        public string Status { get; set; }
        public FastestLap FastestLap { get; set; }
    }
}
