﻿namespace F1.Sweepstake.Domain.Models;

public class Player
{
    public string Code { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public int TotalPoints { get; set; }
    public int TotalRetirements { get; set; }
    public int TotalWinnings { get; set; }
    public bool Hidden { get; set; }
}
