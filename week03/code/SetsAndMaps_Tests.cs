using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// DO NOT MODIFY THIS FILE

[TestClass]
public class FindPairsTests
{
    [TestMethod]
    public void FindPairs_TwoPairs()
    {
        var actual = SetsAndMaps.FindPairs(new string[] { "am", "at", "ma", "if", "fi" });
        var expected = new string[] { "ma & am", "fi & if" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_OnePair()
    {
        var actual = SetsAndMaps.FindPairs(new string[] { "ab", "bc", "cd", "de", "ba" });
        var expected = new string[] { "ba & ab" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_SameChar()
    {
        var actual = SetsAndMaps.FindPairs(new string[] { "ab", "aa", "ba" });
        var expected = new string[] { "ba & ab" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_ThreePairs()
    {
        var actual = SetsAndMaps.FindPairs(new string[] { "ab", "ba", "ac", "ad", "da", "ca" });
        var expected = new string[] { "ba & ab", "da & ad", "ca & ac" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_ThreePairsNumbers()
    {
        var actual = SetsAndMaps.FindPairs(new string[] { "23", "84", "49", "13", "32", "46", "91", "99", "94", "31", "57", "14" });
        var expected = new string[] { "32 & 23", "94 & 49", "31 & 13" };

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod]
    public void FindPairs_NoPairs()
    {
        var actual = SetsAndMaps.FindPairs(new string[] { "ab", "ac" });
        var expected = new string[0];

        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(Canonicalize(expected), Canonicalize(actual));
    }

    [TestMethod, Timeout(60_000)]
    public void FindPairs_NoPairs_Efficiency()
    {
        // Calibrate baseline CPU performance
        double CalibrateCpuSpeed()
        {
            var sw = Stopwatch.StartNew();
            long sum = 0;
            for (int i = 0; i < 10_000_000; i++) sum += i;
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        double baseline = CalibrateCpuSpeed();

        // Create test data
        var count = 1_000_000;
        var input = new List<string>(count);
        for (int i = 0; i < count; ++i)
        {
            char[] chars = new char[] { 'a', 'b' };
            string s = new string(chars);
            input.Add(s);
        }

        // Measure student code
        var sw = Stopwatch.StartNew();
        var actual = SetsAndMaps.FindPairs(input.ToArray());
        sw.Stop();

        double elapsed = sw.Elapsed.TotalMilliseconds;
        double ratio = elapsed / baseline;

        Debug.WriteLine($"Elapsed: {elapsed:F2}ms | Baseline: {baseline:F2}ms | Ratio: {ratio:F2}");
        Assert.IsTrue(ratio < 15.0, "Your algorithm is too slow. Make sure it runs in O(n) time.");
        Assert.AreEqual(0, actual.Length);
    }

    private string Canonicalize(string[] array)
    {
        if (array.Length == 0)
        {
            return "";
        }

        var canonicalString = array.Select(item =>
        {
            var parts = item.Split('&');
            return parts
                .Select(part => part.Trim())
                .OrderBy(x => x)
                .Aggregate((current, next) => current + "&" + next);
        })
        .OrderBy(x => x)
        .Aggregate((current, next) => current + "," + next);

        return canonicalString;
    }
}

[TestClass]
public class SummarizeDegreesTests
{
    [TestMethod]
    public void SummarizeCensusDegrees()
    {
        var result = SetsAndMaps.SummarizeDegrees("../../../census.txt");
        var expected = new Dictionary<string, int> {
            {"Bachelors", 5355},
            {"HS-grad", 10501},
            {"11th", 1175},
            {"Masters", 1723},
            {"9th", 514},
            {"Some-college", 7291},
            {"Assoc-acdm", 1067},
            {"Assoc-voc", 1382},
            {"7th-8th", 646},
            {"Doctorate", 413},
            {"Prof-school", 576},
            {"5th-6th", 333},
            {"10th", 933},
            {"1st-4th", 168},
            {"Preschool", 51},
            {"12th", 433},
        };

        CollectionAssert.AreEqual(expected, result);
    }
}

[TestClass]
public class IsAnagramTests
{
    [TestMethod]
    public void IsAnagram_BasicCases()
    {
        Assert.IsTrue(SetsAndMaps.IsAnagram("CAT", "ACT"));
        Assert.IsFalse(SetsAndMaps.IsAnagram("DOG", "GOOD"));
        Assert.IsFalse(SetsAndMaps.IsAnagram("AABBCCDD", "ABCD"));
        Assert.IsFalse(SetsAndMaps.IsAnagram("ABCCD", "ABBCD"));
        Assert.IsFalse(SetsAndMaps.IsAnagram("BC", "AD"));
    }

    [TestMethod]
    public void IsAnagram_IgnoresCases()
    {
        Assert.IsTrue(SetsAndMaps.IsAnagram("Ab", "Ba"));
    }

    [TestMethod]
    public void IsAnagram_IgnoresSpaces()
    {
        Assert.IsTrue(SetsAndMaps.IsAnagram("tom marvolo riddle", "i am lord voldemort"));
    }

    [TestMethod]
    public void IsAnagram_IgnoresSpacesAndCases()
    {
        Assert.IsTrue(SetsAndMaps.IsAnagram("A Decimal Point", "Im a Dot in Place"));
        Assert.IsTrue(SetsAndMaps.IsAnagram("Eleven plus Two", "Twelve Plus One"));
        Assert.IsFalse(SetsAndMaps.IsAnagram("Eleven plus One", "Twelve Plus One"));
    }

    [TestMethod, Timeout(60_000)]
    public void IsAnagram_Efficiency()
    {
        // Calibrate baseline CPU performance
        double CalibrateCpuSpeed()
        {
            var sw = Stopwatch.StartNew();
            long sum = 0;
            for (int i = 0; i < 400_000_000; i++) sum += i;
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        double baseline = CalibrateCpuSpeed();

        // Create test data
        var rand = new Random();
        var length = 60_000_000;
        var a_array = new char[length];
        var b_array = new char[length];

        for (int i = 0; i < length; ++i)
        {
            char c = (char)rand.Next(256);
            a_array[i] = c;
            b_array[i] = c;
        }

        // Measure student code
        var sw = Stopwatch.StartNew();
        var actual = SetsAndMaps.IsAnagram(new string(a_array), new string(b_array));
        sw.Stop();

        double elapsed = sw.Elapsed.TotalMilliseconds;
        double ratio = elapsed / baseline;

        Debug.WriteLine($"Elapsed: {elapsed:F2}ms | Baseline: {baseline:F2}ms | Ratio: {ratio:F2}");
        Assert.IsTrue(ratio < 15.0, "Your algorithm is too slow. Make sure it runs in O(n) time.");
        Assert.IsTrue(actual);
    }
}

[TestClass]
public class MazeTests
{
    [TestMethod]
    public void Maze_Basic()
    {
        Dictionary<Tuple<int, int>, bool[]> map = SetupMazeMap();
        var maze = new Maze(map);
        Assert.AreEqual("Current location (x=1, y=1)", maze.GetStatus());
        AssertThrowsInvalidOperationException(maze.MoveUp);
        AssertThrowsInvalidOperationException(maze.MoveLeft);
        maze.MoveRight();
        AssertThrowsInvalidOperationException(maze.MoveRight);
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveRight();
        maze.MoveRight();
        maze.MoveUp();
        maze.MoveRight();
        maze.MoveDown();
        maze.MoveLeft();
        AssertThrowsInvalidOperationException(maze.MoveDown);
        maze.MoveRight();
        maze.MoveDown();
        maze.MoveDown();
        maze.MoveRight();
        Assert.AreEqual("Current location (x=6, y=6)", maze.GetStatus());
    }

    private void AssertThrowsInvalidOperationException(Action action)
    {
        try
        {
            action();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("Can't go that way!", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    private static Dictionary<Tuple<int, int>, bool[]> SetupMazeMap()
    {
        Dictionary<Tuple<int, int>, bool[]> map = new Dictionary<Tuple<int, int>, bool[]>();
        map.Add(Tuple.Create(1, 1), new bool[] { false, true, false, true });
        map.Add(Tuple.Create(1, 2), new bool[] { false, true, true, false });
        map.Add(Tuple.Create(1, 3), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(1, 4), new bool[] { false, true, false, true });
        map.Add(Tuple.Create(1, 5), new bool[] { false, false, true, true });
        map.Add(Tuple.Create(1, 6), new bool[] { false, false, true, false });
        map.Add(Tuple.Create(2, 1), new bool[] { true, false, false, true });
        map.Add(Tuple.Create(2, 2), new bool[] { true, false, true, true });
        map.Add(Tuple.Create(2, 3), new bool[] { false, false, true, true });
        map.Add(Tuple.Create(2, 4), new bool[] { true, true, true, false });
        map.Add(Tuple.Create(2, 5), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(2, 6), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(3, 1), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(3, 2), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(3, 3), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(3, 4), new bool[] { true, true, false, true });
        map.Add(Tuple.Create(3, 5), new bool[] { false, false, true, true });
        map.Add(Tuple.Create(3, 6), new bool[] { false, false, true, false });
        map.Add(Tuple.Create(4, 1), new bool[] { false, true, false, false });
        map.Add(Tuple.Create(4, 2), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(4, 3), new bool[] { false, true, false, true });
        map.Add(Tuple.Create(4, 4), new bool[] { true, true, true, false });
        map.Add(Tuple.Create(4, 5), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(4, 6), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(5, 1), new bool[] { true, true, false, true });
        map.Add(Tuple.Create(5, 2), new bool[] { false, false, true, true });
        map.Add(Tuple.Create(5, 3), new bool[] { true, true, true, true });
        map.Add(Tuple.Create(5, 4), new bool[] { true, false, true, true });
        map.Add(Tuple.Create(5, 5), new bool[] { false, false, true, true });
        map.Add(Tuple.Create(5, 6), new bool[] { false, true, true, false });
        map.Add(Tuple.Create(6, 1), new bool[] { true, false, false, false });
        map.Add(Tuple.Create(6, 2), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(6, 3), new bool[] { true, false, false, false });
        map.Add(Tuple.Create(6, 4), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(6, 5), new bool[] { false, false, false, false });
        map.Add(Tuple.Create(6, 6), new bool[] { true, false, false, false });
        return map;
    }
}

[TestClass]
public class EarthquakeDailySummaryTests
{
    [TestMethod]
    public void EarthquakeDailySummary_Basic()
    {
        var result = SetsAndMaps.EarthquakeDailySummary();
        Assert.IsTrue(result.Length > 5, "Too few earthquakes");

        foreach (string s in result)
        {
            Assert.IsTrue(s.Contains(" - Mag "), "String must contain a magnitude");
        }
    }
}