using System;
using System.Collections.Generic;

/// <summary>
/// Represents a maze with walls and movement validation.
/// </summary>
public class Maze
{
    private Dictionary<Tuple<int, int>, bool[]> _maze;
    private int _currentX = 1;
    private int _currentY = 1;

    public Maze(Dictionary<Tuple<int, int>, bool[]> maze)
    {
        _maze = maze;
    }

    public string GetStatus()
    {
        return $"Current location (x={_currentX}, y={_currentY})";
    }

    public bool MoveLeft(int x, int y)
    {
        var key = Tuple.Create(x, y);
        if (!_maze.ContainsKey(key))
            return false;
        return _maze[key][0];
    }

    public bool MoveRight(int x, int y)
    {
        var key = Tuple.Create(x, y);
        if (!_maze.ContainsKey(key))
            return false;
        return _maze[key][1];
    }

    public bool MoveUp(int x, int y)
    {
        var key = Tuple.Create(x, y);
        if (!_maze.ContainsKey(key))
            return false;
        return _maze[key][2];
    }

    public bool MoveDown(int x, int y)
    {
        var key = Tuple.Create(x, y);
        if (!_maze.ContainsKey(key))
            return false;
        return _maze[key][3];
    }

    public void MoveLeft()
    {
        if (MoveLeft(_currentX, _currentY))
        {
            _currentX--;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public void MoveRight()
    {
        if (MoveRight(_currentX, _currentY))
        {
            _currentX++;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public void MoveUp()
    {
        if (MoveUp(_currentX, _currentY))
        {
            _currentY++;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public void MoveDown()
    {
        if (MoveDown(_currentX, _currentY))
        {
            _currentY--;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }
}