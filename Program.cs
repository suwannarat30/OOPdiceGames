//importing the fundamental namespaces
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        //printing menu options for the user, to the console
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Play Sevens Out");
        Console.WriteLine("2. Play Three or More");
        Console.WriteLine("3. View Statistics");
        Console.WriteLine("4. Run Tests");
        Console.WriteLine("0. Exit");

        //using a switch statement in order to handle the user's selection of the menu
        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Would you like to play against another player (1) or against the computer (2)?");
                bool againstComputerSevens = Console.ReadLine() == "2";
                var sevensOut = new SevensOut(againstComputerSevens);
                sevensOut.Play();
                break;
          
            case "2":
                Console.WriteLine("Would you like to play against another player (1) or against the computer (2)?");
                bool againstComputerThree = Console.ReadLine() == "2";
                var threeOrMore = new ThreeOrMore(againstComputerThree);
                threeOrMore.Play();
                break;
          
            case "3":
                ShowSevensOutStatistics("SevensOutStats.txt");
                ShowThreeOrMoreStatistics("ThreeOrMoreStats.txt");
                break;
          
            case "4":
                var testing = new Testing();
                testing.TestSevensOut();
                testing.TestThreeOrMore();
                break;
          
            case "0":
                Console.WriteLine("Exiting the application.");
                return;
          
            default:
                Console.WriteLine("Invalid option, please try again.");
                break;
        }
    }

    //a method that displays the statistics for the Sevens Out game
    private static void ShowSevensOutStatistics(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0 && int.TryParse(lines[0], out int gamesPlayed))
                {
                    Console.WriteLine($"Sevens Out has been played {gamesPlayed / 2.0} times.");
                }
                else
                {
                    Console.WriteLine("Statistics for Sevens Out are currently unavailable.");
                }
            }
            else
            {
                Console.WriteLine("Statistics file for Sevens Out not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the statistics file for Sevens Out: " + ex.Message);
        }
    }
  
  //a method that displays the statistics for the Three Or More game
    private static void ShowThreeOrMoreStatistics(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0 && int.TryParse(lines[0], out int gamesPlayed))
                {
                    Console.WriteLine($"Three Or More has been played {gamesPlayed} times.");
                }
                else
                {
                    Console.WriteLine("Statistics for Three Or More are currently unavailable.");
                }
            }
            else
            {
                Console.WriteLine("Statistics file for Three Or More not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the statistics file for Three Or More: " + ex.Message);
        }
    }
}
