using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue a single item and then dequeue it
    // Expected Result: The item should be returned
    // Defect(s) Found: None
    public void TestPriorityQueue_EnqueueDequeue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 5);
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Item1", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with different priorities
    // Expected Result: Highest priority (10) should be returned first, then 5, then 1
    // Defect(s) Found: The loop was stopping early (index < _queue.Count - 1), missing the last item
    public void TestPriorityQueue_DifferentPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("Medium", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("Low", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same priority
    // Expected Result: Items should be returned in FIFO order (first in, first out)
    // Defect(s) Found: Used >= instead of >, which would remove the last item with same priority instead of the first
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("First", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("Second", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("Third", result);
    }

    [TestMethod]
    // Scenario: Enqueue items with same and different priorities mixed
    // Expected Result: Highest priority first, FIFO for ties
    // Defect(s) Found: Items were not being removed from the queue, causing infinite loops
    public void TestPriorityQueue_MixedPrioritiesWithTies()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low1", 1);
        priorityQueue.Enqueue("High1", 10);
        priorityQueue.Enqueue("High2", 10);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High3", 10);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("High1", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("High2", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("High3", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("Medium", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("Low1", result);
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None (exception was already implemented)
    [ExpectedException(typeof(InvalidOperationException), "The queue is empty.")]
    public void TestPriorityQueue_DequeueEmpty_ThrowsException()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Dequeue();
    }

    [TestMethod]
    // Scenario: Enqueue items with negative priorities
    // Expected Result: Items with higher priorities (less negative) should be returned first
    // Defect(s) Found: None
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Neg5", -5);
        priorityQueue.Enqueue("Neg10", -10);
        priorityQueue.Enqueue("Positive", 10);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Positive", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("Neg5", result);

        result = priorityQueue.Dequeue();
        Assert.AreEqual("Neg10", result);
    }

    [TestMethod]
    // Scenario: Enqueue many items with same priority to verify FIFO
    // Expected Result: All items returned in FIFO order
    // Defect(s) Found: None (if FIFO is working correctly)
    public void TestPriorityQueue_ManySamePriority()
    {
        var priorityQueue = new PriorityQueue();
        for (int i = 1; i <= 5; i++)
        {
            priorityQueue.Enqueue($"Item{i}", 1);
        }

        for (int i = 1; i <= 5; i++)
        {
            var result = priorityQueue.Dequeue();
            Assert.AreEqual($"Item{i}", result);
        }
    }

    [TestMethod]
    // Scenario: Verify queue empties correctly after all items removed
    // Expected Result: Queue should be empty after all items dequeued
    // Defect(s) Found: Items were not being removed from the queue
    public void TestPriorityQueue_QueueEmptiesCorrectly()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 1);
        priorityQueue.Enqueue("Item2", 2);
        priorityQueue.Enqueue("Item3", 3);

        // Dequeue all items
        priorityQueue.Dequeue();
        priorityQueue.Dequeue();
        priorityQueue.Dequeue();

        // Queue should now be empty - this should throw an exception
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }
}