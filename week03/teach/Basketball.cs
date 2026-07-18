using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Reads NBA basketball player statistics from a CSV file.
/// Uses a Dictionary (Map) to sum total points per player.
/// Displays the top 10 players with the highest total points.
/// </summary>
public class Basketball
{
    public static void Run()
    {
        // Dictionary to store player ID -> total points
        Dictionary<string, int> playerPoints = new Dictionary<string, int>();

        // Read the CSV file
        string filePath = "basketball.csv";

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File '{filePath}' not found.");
            return;
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(filePath);

        // Skip the header line (first line)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            // Split the line by comma
            string[] parts = line.Split(',');

            // Column 0: Player ID
            // Column 1: Year
            // Column 8: Points scored that year
            string playerId = parts[0];

            // Parse points (handle potential empty or invalid values)
            if (int.TryParse(parts[8], out int points))
            {
                // Add points to the player's total
                if (playerPoints.ContainsKey(playerId))
                {
                    playerPoints[playerId] += points;
                }
                else
                {
                    playerPoints[playerId] = points;
                }
            }
        }

        // Convert the dictionary to a list of KeyValuePair and sort by points (descending)
        var sortedPlayers = playerPoints
            .OrderByDescending(p => p.Value)
            .ToList();

        // Display the top 10 players
        Console.WriteLine("Top 10 Players by Total Points:");
        Console.WriteLine("--------------------------------");

        int rank = 1;
        foreach (var player in sortedPlayers.Take(10))
        {
            Console.WriteLine($"{rank}. Player {player.Key}: {player.Value} points");
            rank++;
        }
    }
}