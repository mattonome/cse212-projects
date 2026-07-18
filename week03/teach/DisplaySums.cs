/// <summary>
/// Display all pairs of numbers in the list that sum to 10.
/// No duplicates should be displayed.
/// Assumes the list of numbers has no duplicates.
/// </summary>
public static class DisplaySums
{
    public static void Run()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Console.WriteLine("Pairs that sum to 10:");
        DisplaySumPairs(numbers);
    }

    /// <summary>
    /// Finds and displays all pairs of numbers that sum to 10.
    /// Uses a HashSet for O(n) performance.
    /// </summary>
    /// <param name="numbers">List of numbers (assumed to have no duplicates)</param>
    public static void DisplaySumPairs(List<int> numbers)
    {
        // Use a HashSet to store numbers we've seen
        HashSet<int> seen = new HashSet<int>();
        int targetSum = 10;

        foreach (int num in numbers)
        {
            // Calculate the complement needed to reach the target sum
            int complement = targetSum - num;

            // Check if the complement is already in the set
            if (seen.Contains(complement))
            {
                // Found a pair! Display it (smaller number first for consistency)
                if (num < complement)
                {
                    Console.WriteLine($"{num} + {complement} = {targetSum}");
                }
                else
                {
                    Console.WriteLine($"{complement} + {num} = {targetSum}");
                }
            }

            // Add the current number to the set for future lookups
            seen.Add(num);
        }
    }
}