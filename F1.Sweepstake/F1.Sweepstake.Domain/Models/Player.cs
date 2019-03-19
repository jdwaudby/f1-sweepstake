namespace F1.Sweepstake.Domain.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public Driver Driver { get; set; }
        public int Points { get; set; }
        public int Retirements { get; set; }
    }
}
