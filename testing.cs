using System;
//this class is responsible for carrying out tests on the games' classes/functions  
public class Testing
{
    //testing out the logic of the SevensOut game
    public void TestSevensOut()
    {
        var game = new SevensOut(false);
        game.Play();
        Console.WriteLine("As you can see, a total of 7 has been reached so the game is over");
    }

    //testing the logic of the  Three or More game
    public void TestThreeOrMore()
    {
        var game = new ThreeOrMore(false);
        game.Play();
        Console.WriteLine("As you can see, a score of 20 has been reached so the game is over");

    }
}
