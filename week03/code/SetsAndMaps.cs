using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text;

/// <summary>
/// Contains solutions for Problems 1, 2, 3, and 5 from the W03 Coding Assignment.
/// </summary>
public static class SetsAndMaps
{
    // ============================================================
    // Problem 1: Find Pairs with Sets
    // ============================================================

    /// <summary>
    /// Finds symmetric pairs of two-letter words in a list using a HashSet for O(n) performance.
    /// </summary>
    /// <param name="words">List of two-letter words (lowercase, no duplicates)</param>
    /// <returns>Array of formatted pairs like ["am & ma", "if & fi"]</returns>
    public static string[] FindPairs(string[] words)
    {
        // Use a HashSet for O(1) lookups
        HashSet<string> wordSet = new HashSet<string>(words);
        List<string> pairs = new List<string>();
        HashSet<string> used = new HashSet<string>();

        foreach (string word in words)
        {
            // Skip if word has already been used as a pair
            if (used.Contains(word))
                continue;

            // Reverse the word
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);

            // Skip if the word is the same when reversed (e.g., "aa")
            if (word == reversed)
                continue;

            // Check if the reversed word exists in the set
            if (wordSet.Contains(reversed))
            {
                // Add the pair in alphabetical order for consistency
                if (string.Compare(word, reversed) < 0)
                {
                    pairs.Add($"{word} & {reversed}");
                }
                else
                {
                    pairs.Add($"{reversed} & {word}");
                }

                // Mark both words as used
                used.Add(word);
                used.Add(reversed);
            }
        }

        return pairs.ToArray();
    }

    // ============================================================
    // Problem 2: Degree Summary - FIXED
    // ============================================================

    /// <summary>
    /// Reads a census file and creates a dictionary summarizing all degrees earned.
    /// </summary>
    /// <param name="filename">Path to the census.txt file</param>
    /// <returns>Dictionary with degree names as keys and counts as values</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        // Dictionary to store degree -> count
        Dictionary<string, int> degreeCounts = new Dictionary<string, int>();

        // Check if file exists
        if (!File.Exists(filename))
        {
            return degreeCounts;
        }

        // Read all lines from the file
        string[] lines = File.ReadAllLines(filename);

        // Skip header line (first line)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            // Split by comma
            string[] parts = line.Split(',');

            // FIXED: Column 3 (0-indexed) contains the degree
            if (parts.Length > 3)
            {
                string degree = parts[3].Trim();

                // Skip empty degrees
                if (!string.IsNullOrEmpty(degree))
                {
                    if (degreeCounts.ContainsKey(degree))
                    {
                        degreeCounts[degree]++;
                    }
                    else
                    {
                        degreeCounts[degree] = 1;
                    }
                }
            }
        }

        return degreeCounts;
    }

    // ============================================================
    // Problem 3: Anagrams
    // ============================================================

    /// <summary>
    /// Determines if two words are anagrams using a Dictionary.
    /// Ignores spaces and case.
    /// </summary>
    /// <param name="word1">First word</param>
    /// <param name="word2">Second word</param>
    /// <returns>True if the words are anagrams, false otherwise</returns>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert to lowercase
        string clean1 = word1.Replace(" ", "").ToLower();
        string clean2 = word2.Replace(" ", "").ToLower();

        // If lengths differ, they can't be anagrams
        if (clean1.Length != clean2.Length)
            return false;

        // Count characters in word1
        Dictionary<char, int> charCounts = new Dictionary<char, int>();

        foreach (char c in clean1)
        {
            if (charCounts.ContainsKey(c))
                charCounts[c]++;
            else
                charCounts[c] = 1;
        }

        // Subtract counts using word2
        foreach (char c in clean2)
        {
            if (!charCounts.ContainsKey(c))
                return false;

            charCounts[c]--;

            if (charCounts[c] < 0)
                return false;
        }

        // Check if all counts are zero
        foreach (int count in charCounts.Values)
        {
            if (count != 0)
                return false;
        }

        return true;
    }

    // ============================================================
    // Problem 5: Earthquake JSON Data - FIXED
    // ============================================================

    /// <summary>
    /// Classes for deserializing USGS earthquake JSON data.
    /// </summary>
    public class FeatureCollection
    {
        public string Type { get; set; }
        public Feature[] Features { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        public long Generated { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Api { get; set; }
        public int Count { get; set; }
    }

    public class Feature
    {
        public string Type { get; set; }
        public Properties Properties { get; set; }
        public Geometry Geometry { get; set; }
        public string Id { get; set; }
    }

    public class Properties
    {
        public double Mag { get; set; }
        public string Place { get; set; }
        public long Time { get; set; }
        public long Updated { get; set; }
        public int Tz { get; set; }
        public string Url { get; set; }
        public string Detail { get; set; }
        public int Felt { get; set; }
        public double Cdi { get; set; }
        public double Mmi { get; set; }
        public string Alert { get; set; }
        public string Status { get; set; }
        public int Tsunami { get; set; }
        public int Sig { get; set; }
        public string Net { get; set; }
        public string Code { get; set; }
        public string Ids { get; set; }
        public string Sources { get; set; }
        public string Types { get; set; }
        public int Nst { get; set; }
        public double Dmin { get; set; }
        public double Rms { get; set; }
        public double Gap { get; set; }
        public string MagType { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
    }

    public class Geometry
    {
        public string Type { get; set; }
        public double[] Coordinates { get; set; }
    }

    /// <summary>
    /// Gets earthquake data from USGS and returns formatted summary.
    /// </summary>
    /// <returns>Array of strings formatted as "Place - Mag X.XX"</returns>
    public static string[] EarthquakeDailySummary()
    {
        // USGS API endpoint for earthquakes in the last 24 hours
        string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    // Deserialize JSON
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    FeatureCollection data = JsonSerializer.Deserialize<FeatureCollection>(jsonResponse, options);

                    if (data?.Features != null)
                    {
                        List<string> results = new List<string>();

                        foreach (var feature in data.Features)
                        {
                            if (feature?.Properties != null)
                            {
                                string place = feature.Properties.Place ?? "Unknown location";
                                double mag = feature.Properties.Mag;
                                results.Add($"{place} - Mag {mag}");
                            }
                        }

                        return results.ToArray();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching earthquake data: {ex.Message}");
        }

        return Array.Empty<string>();
    }
}