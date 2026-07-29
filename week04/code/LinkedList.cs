using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A simple linked list implementation.
/// </summary>
public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    // ============================================================
    // PROPERTIES
    // ============================================================

    /// <summary>
    /// Returns true if both head and tail are null.
    /// </summary>
    public bool HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    /// <summary>
    /// Returns true if both head and tail are not null.
    /// </summary>
    public bool HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }

    // ============================================================
    // INSERT METHODS
    // ============================================================

    /// <summary>
    /// Insert a new node at the front (head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        Node newNode = new Node(value);
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head = newNode;
        }
    }

    /// <summary>
    /// Insert a new node at the end (tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        Node newNode = new Node(value);

        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail!.Next = newNode;
            _tail = newNode;
        }
    }

    /// <summary>
    /// Insert a new node after the first node that contains the specified value.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        if (_head is null)
        {
            return;
        }

        Node current = _head;
        while (current is not null)
        {
            if (current.Data == value)
            {
                Node newNode = new Node(newValue);
                newNode.Next = current.Next;
                current.Next = newNode;

                // If we inserted after the tail, update tail
                if (current == _tail)
                {
                    _tail = newNode;
                }
                return;
            }
            current = current.Next;
        }
    }

    // ============================================================
    // REMOVE METHODS
    // ============================================================

    /// <summary>
    /// Remove the node at the front (head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        if (_head is null)
        {
            return;
        }

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _head = _head.Next;
        }
    }

    /// <summary>
    /// Remove the node at the end (tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        if (_head is null)
        {
            return;
        }

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
            return;
        }

        Node current = _head;
        while (current.Next != _tail)
        {
            current = current.Next;
        }

        current.Next = null;
        _tail = current;
    }

    /// <summary>
    /// Remove the first node that contains the specified value.
    /// </summary>
    public void Remove(int value)
    {
        if (_head is null)
        {
            return;
        }

        if (_head.Data == value)
        {
            RemoveHead();
            return;
        }

        Node current = _head;
        while (current.Next is not null)
        {
            if (current.Next.Data == value)
            {
                if (current.Next == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    current.Next = current.Next.Next;
                }
                return;
            }
            current = current.Next;
        }
    }

    // ============================================================
    // REPLACE METHODS
    // ============================================================

    /// <summary>
    /// Replace all occurrences of oldValue with newValue.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        if (_head is null)
        {
            return;
        }

        Node current = _head;
        while (current is not null)
        {
            if (current.Data == oldValue)
            {
                current.Data = newValue;
            }
            current = current.Next;
        }
    }

    // ============================================================
    // ITERATORS
    // ============================================================

    /// <summary>
    /// Forward iterator (foreach support).
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        Node? current = _head;
        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    /// <summary>
    /// Reverse iterator (for Reverse() method).
    /// </summary>
    public IEnumerable<int> Reverse()
    {
        List<int> values = new List<int>();
        Node? current = _head;

        while (current is not null)
        {
            values.Add(current.Data);
            current = current.Next;
        }

        for (int i = values.Count - 1; i >= 0; i--)
        {
            yield return values[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// ============================================================
// EXTENSION METHODS
// ============================================================

/// <summary>
/// Extension methods for IEnumerable<int> to help with testing.
/// </summary>
public static class LinkedListExtensions
{
    /// <summary>
    /// Converts an IEnumerable<int> to a string representation.
    /// </summary>
    public static string AsString(this IEnumerable<int> source)
    {
        return string.Join(", ", source);
    }
}