//importing the fundamental namespaces
using System;
using DiceGames;



//defining the SevensOut class
public class SevensOut : Game
{
    //declaring two dice for the game as this game involves two dice
    private Die _die1=new Die();
    private Die _die2=new Die();

    //an array to store the scores of the two players or the player and the computer
    private int[] Scores=new int[2];

    private int currentPlayer=0;

    //using a boolean to check whether or not the game is being played against the computer
    private bool playAgainstComputer;

    //statistics object to manage game statistics
    private Statistics stats;

  
    public SevensOut(bool againstComputer)
    {
      
        playAgainstComputer=againstComputer;
        //used to create a new text file to keep statistics for the game history and the players' performances
        stats=new Statistics("SevensOutStats.txt");
        //incrementing the game count at the start of a new game session
        stats.UpdateGameStats(0, true);
    }

  
    public override void Play()
    {
        bool gameEnded=false;
        while (!gameEnded)
        {
            //displaying who's turn it is
            string playerName = currentPlayer == 0 ? "Player 1" : (playAgainstComputer ? "Computer" : "Player 2");
            Console.WriteLine($"\n{playerName}'s turn. Press Enter to roll the dice...");
            if (!playAgainstComputer || currentPlayer == 0) Console.ReadLine();

            // Roll the dice
            int roll1=_die1.Roll();
            int roll2=_die2.Roll();
            int total=roll1 + roll2;

            //displaying the dice roll results
            Console.WriteLine($"Rolled {roll1} and {roll2}.");
            //checking if the total is 7, if so, game ends
            if (total == 7)
            {
                Console.WriteLine($"Rolled a seven with a combination of {roll1} + {roll2}, game over.");
                //condition where the game ends is met
                gameEnded=true;
                //updating high score
                stats.UpdateGameStats(Math.Max(Scores[0], Scores[1]), false);
                DeclareWinner();
            }
            else
            {
                //checking for doubles, and if so apply the game rules
                if (roll1 == roll2)
                {
                    Scores[currentPlayer] += total * 2;
                    Console.WriteLine($"Doubles! Score doubled to {Scores[currentPlayer]}.");
                }
                else
                {
                    Scores[currentPlayer] += total;
                    Console.WriteLine($"Current score for {playerName}: {Scores[currentPlayer]}.");
                }
                //switching to the next player once its their turn
                currentPlayer=(currentPlayer + 1) % 2;
            }
        }
    }

  
    //using a private method to declare the winner based on the scores
    private void DeclareWinner()
    {
        if (Scores[0]>Scores[1])
        {
            Console.WriteLine($"Player 1 wins with {Scores[0]} points!");
        }
        else if (Scores[1]>Scores[0])
        {
          
            Console.WriteLine($"{(playAgainstComputer ? "Computer" : "Player 2")} wins with {Scores[1]} points!");
          
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }
}
