using System;
using System.Collections.Generic;

/// <summary>
/// Represents a maze with walls and movement validation.
/// </summary>
public class Maze
{
    private Dictionary<(int x, int y), (bool left, bool right, bool up, bool down)> _maze;

    public Maze(Dictionary<(int x, int y), (bool left, bool right, bool up, bool down)> maze)
    {
        _maze = maze;
    }

    /// <summary>
    /// Determines if moving left from the given position is valid.
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>True if left movement is valid, false otherwise</returns>
    public bool MoveLeft(int x, int y)
    {
        // Check if the current position exists in the maze
        if (!_maze.ContainsKey((x, y)))
            return false;

        // Check if the cell to the left exists and if the left movement is allowed
        if (_maze.ContainsKey((x - 1, y)))
        {
            return _maze[(x, y)].left;
        }
        return false;
    }

    /// <summary>
    /// Determines if moving right from the given position is valid.
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>True if right movement is valid, false otherwise</returns>
    public bool MoveRight(int x, int y)
    {
        if (!_maze.ContainsKey((x, y)))
            return false;

        if (_maze.ContainsKey((x + 1, y)))
        {
            return _maze[(x, y)].right;
        }
        return false;
    }

    /// <summary>
    /// Determines if moving up from the given position is valid.
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>True if up movement is valid, false otherwise</returns>
    public bool MoveUp(int x, int y)
    {
        if (!_maze.ContainsKey((x, y)))
            return false;

        if (_maze.ContainsKey((x, y + 1)))
        {
            return _maze[(x, y)].up;
        }
        return false;
    }

    /// <summary>
    /// Determines if moving down from the given position is valid.
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>True if down movement is valid, false otherwise</returns>
    public bool MoveDown(int x, int y)
    {
        if (!_maze.ContainsKey((x, y)))
            return false;

        if (_maze.ContainsKey((x, y - 1)))
        {
            return _maze[(x, y)].down;
        }
        return false;
    }
}