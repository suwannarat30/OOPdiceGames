using System;

public class Die
{
    private Random _random = new Random();
    public int CurrentValue { get; private set; }

    //simulates rolling the dice
    public int Roll()
    {
        CurrentValue = _random.Next(1, 7); //this generates a random number between 1 and 6 to decide what the user rolled
        return CurrentValue;
    }
}
