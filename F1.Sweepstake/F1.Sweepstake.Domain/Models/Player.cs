namespace F1.Sweepstake.Domain.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public Driver Assignment { get; set; }
    }
}
