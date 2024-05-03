//importing namespaces
using System;
using System.IO;

//defining the Statistics class here
public class Statistics
{
  
    private string statsFilePath;
    public int TotalGamesPlayed { get; private set; }

    //property to get the high score
    public int HighScore { get; private set; }

    //here, the constructor initialises the statistics
    public Statistics(string filePath)
    {
      
        statsFilePath=filePath;
        LoadStats();
    }

    //using a private method to scrape the statistics from a text file
    private void LoadStats()
    {
        if (File.Exists(statsFilePath))
        {
            //reading all the lines from the file
            string[] lines = File.ReadAllLines(statsFilePath);
            if (lines.Length >= 2)
            {
                TotalGamesPlayed = int.Parse(lines[0]);
                HighScore = int.Parse(lines[1]);
            }
        }
    }
    public void SaveStats()
    {
        //creating an array of strings that represent the statistics
        string[] lines=new string[]
        {
            TotalGamesPlayed.ToString(),
            HighScore.ToString()
        };
        File.WriteAllLines(statsFilePath, lines);
    }

    public void UpdateGameStats(int score, bool updateHighScore = false)
    {
        //this increments the total games played by 1
        TotalGamesPlayed++;
        if (updateHighScore && score > HighScore)
        {
            HighScore = score;
        }
        SaveStats();  //saving the updated statistics to the text file
    }
}
