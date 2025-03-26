//using the necessary namespaces
using System;
using System.Linq;
using DiceGames;



//this class is used to represent the game logic for the Three or More game
public class ThreeOrMore : Game
{

    private Die[] dice=new Die[5]; //array to hold the five dice that will be rolled

    private int[] scores=new int[2]; //array to keep track of the scores for the two players or the player up against the computer

    private int currentPlayer=0; //using indexing to track the current player/user, starting with player 1 as index 0

    private bool playAgainstComputer; //a boolean that is used to check if the game is against the computer or not

    private Statistics stats; //a statistics object used for recording the game statistics

    //constructor used to initialize the game
    public ThreeOrMore (bool againstComputer)
    {
        playAgainstComputer=againstComputer; //making the game mode such that the user plays against computer, or not (against another player)
      
        stats=new Statistics ("ThreeOrMoreStats.txt");
      
        stats.UpdateGameStats(0, true); //this increments the game count at the start of a new game session, and is used in statistics


      
        for (int i = 0; i < dice.Length; i++)
        {
            dice[i]=new Die();
        }
    }

    //this method contains the main gameplay loop
    public override void Play()
    {
        bool gameContinue=true;
        while (gameContinue) //means that the game will continue until a break condition is met
        {
            //this outputs which player's turn it is
            Console.WriteLine($"\nPlayer {(currentPlayer+1)}'s turn. {(currentPlayer == 0 || !playAgainstComputer ? "Press Enter to roll the dice..." : "")}");
            if (currentPlayer==0 || !playAgainstComputer) Console.ReadLine();

            //rolls dice, calculating the points earned and outputting them
            int[] rolls=RollDice();
          
            Console.WriteLine($"Rolls: {string.Join(", ", rolls)}");
          
            int points=CalculatePoints(rolls);
            scores[currentPlayer] += points;
          
            Console.WriteLine($"Current score for Player {(currentPlayer + 1)}: {scores[currentPlayer]}");

            if (ContainsTwoOfAKind(rolls))
            {
                if (currentPlayer == 1 && playAgainstComputer)
                {
                    ComputerRerollDecision(rolls);
                }
                else
                {
                    PlayerRerollDecision(rolls);
                }
            }

            //this checks if the score reaches or exceeds 20, and if so the game ends
            if (scores[currentPlayer] >= 20)
            {
              
                Console.WriteLine($"Player {(currentPlayer + 1)} reaches 20 points, game over!");
                gameContinue=false;
              
            }

          
            //this switches the players at the end of each turn
            currentPlayer=(currentPlayer + 1) % 2;
          
        }
    }

    //this rolls all the dice and returns the values obtained in the form of an array of integers
    private int[] RollDice()
    {
        return dice.Select(d => d.Roll()).ToArray();
    }

    //this calculates the score based on the number of identical dice rolled as mentioned in the game rules
    private int CalculatePoints(int[] rolls)
    {
        var counts=new int[7];
        foreach (var roll in rolls)
        {
            counts[roll]++;
        }

        //this returns points to the players based on the game rules mentioned in the text file
        if (counts.Contains(5)) return 12;
      
        if (counts.Contains(4)) return 6;
      
        if (counts.Contains(3)) return 3;
      
        return 0;
    }


  
    //this is used to check if the roll contains two of any kind of dice, in which case the user gets a choice
  
    private bool ContainsTwoOfAKind(int[] rolls)
    {
        var counts=new int[7];
        foreach (var roll in rolls)
        {
            counts[roll]++;
        }
        return counts.Any(c => c == 2);
    }


  
    //this tells the player about their choice to reroll the dice
    private void PlayerRerollDecision(int[] rolls)
    {
        Console.WriteLine("You rolled two of a kind. Please choose whether you'd like to reroll:");
        Console.WriteLine("1: All dice");
        Console.WriteLine("2: Only the 3 remaining dice");
        string choice=Console.ReadLine();


      
        //here the program is rerolling based on the player's input
        if(choice == "1")
        {
            Console.WriteLine("Rerolling all dice...");
            rolls = RollDice();
        }
        else if(choice == "2")
        {
          
            RerollRemainingThreeDice(rolls);

          
        }
        Console.WriteLine($"After reroll: {string.Join(", ", rolls)}");
      
        int additionalPoints = CalculatePoints(rolls);
      
        scores[currentPlayer] += additionalPoints;
        Console.WriteLine($"Updated score for Player {(currentPlayer+1)}: {scores[currentPlayer]}");
    }
    private void ComputerRerollDecision(int[] rolls)
    {
        Random rnd=new Random();
        if (rnd.Next(0, 2) == 0)
        {
            Console.WriteLine("The computer has decided to reroll all the dice");
            rolls = RollDice();
        }
        else
        {
            Console.WriteLine("The computer has decided to reroll the 3 of the remaining dice...");
            RerollRemainingThreeDice(rolls);
        }

      
        Console.WriteLine($"Computer rerolls the following: {string.Join(", ", rolls)}");
        int additionalPoints = CalculatePoints(rolls);
        scores[currentPlayer] += additionalPoints;
        Console.WriteLine($"Updated score for Player 2: {scores[currentPlayer]}");
    }

    //rerolling the three non-pair dice.
    private void RerollRemainingThreeDice(int[] rolls)
    {
        var grouped=rolls.GroupBy(x => x).Where(g => g.Count() == 1).ToArray();
        var indicesToReroll=grouped.SelectMany(g => Enumerable.Repeat(Array.IndexOf(rolls, g.Key), g.Count())).Take(3);
        foreach(var index in indicesToReroll)
        {
            rolls[index] = dice[index].Roll();
        }
    }
}
   
    