using DataStructures.Heaps;
using System;
using Xunit;

// Alias to avoid conflict with System.Collections.Generic.PriorityQueue
using KeyedPriorityQueue = DataStructures.Heaps.PriorityQueue<int, int, int>;

namespace UnitTest.DataStructuresTests
{
    public static class PriorityQueuesTest
    {
        internal class Process : IComparable<Process>
        {
            public Process(int id, string desc)
            {
                Id = id;
                Description = desc;
            }

            public int Id { get; set; }
            public string Description { get; set; }

            public int CompareTo(Process other)
            {
                if (other == null)
                    return -1;
                return Id.CompareTo(other.Id);
            }
        }

        #region Keyed Priority Queue Tests

        [Fact]
        public static void KeyedPriorityQueue_Dequeue_ReturnsHighestPriority()
        {
            // PriorityQueue<K, V, P> where K=key, V=value, P=priority
            // Dequeue returns V (value), not K (key)
            var queue = new KeyedPriorityQueue(10);

            for (var i = 0; i < 20; ++i)
            {
                // Enqueue(key, value, priority) - key=i, value=i, priority=i/3+1
                queue.Enqueue(i, i, i / 3 + 1);
            }

            // Dequeue returns the value of the highest priority item
            var highest = queue.Dequeue();

            Assert.Equal(18, highest);
        }

        [Fact]
        public static void KeyedPriorityQueue_EnqueueDequeue_MaintainsOrder()
        {
            var queue = new KeyedPriorityQueue(5);

            // Enqueue with different priorities (higher number = higher priority)
            queue.Enqueue(1, 100, 1);   // low priority
            queue.Enqueue(2, 200, 10);  // high priority
            queue.Enqueue(3, 300, 5);   // medium priority

            // Should come out highest priority first
            Assert.Equal(200, queue.Dequeue());  // priority 10
            Assert.Equal(300, queue.Dequeue());  // priority 5
            Assert.Equal(100, queue.Dequeue());  // priority 1
        }

        #endregion

        #region MinPriorityQueue with String Tests

        [Fact]
        public static void MinPriorityQueue_DequeueMin_ReturnsLowestPriority()
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var queue = new MinPriorityQueue<string, int>((uint)alphabet.Length);

            for (var i = 0; i < alphabet.Length; ++i)
            {
                queue.Enqueue(alphabet[i].ToString(), i / 3 + 1);
            }

            var min = queue.DequeueMin();

            Assert.Equal("a", min);
        }

        [Fact]
        public static void MinPriorityQueue_EnqueueDequeueMin_MaintainsOrder()
        {
            var queue = new MinPriorityQueue<string, int>(5);

            queue.Enqueue("high", 10);
            queue.Enqueue("low", 1);
            queue.Enqueue("medium", 5);

            Assert.Equal("low", queue.DequeueMin());
            Assert.Equal("medium", queue.DequeueMin());
            Assert.Equal("high", queue.DequeueMin());
        }

        #endregion

        #region MinPriorityQueue with Process Tests

        [Fact]
        public static void MinPriorityQueue_WithProcesses_ReturnsInPriorityOrder()
        {
            var queue = new MinPriorityQueue<Process, int>();

            var process1 = new Process(432654, "Process 1");
            var process2 = new Process(123456, "Process 2");
            var process3 = new Process(345098, "Process 3");
            var process4 = new Process(109875, "Process 4");
            var process5 = new Process(13579, "Process 5");
            var process6 = new Process(24680, "Process 6");

            queue.Enqueue(process1, 1);   // priority 1 (lowest = first out)
            queue.Enqueue(process2, 10);  // priority 10 (highest = last out)
            queue.Enqueue(process3, 5);
            queue.Enqueue(process4, 7);
            queue.Enqueue(process5, 3);
            queue.Enqueue(process6, 6);

            // Should come out in priority order: 1, 3, 5, 6, 7, 10
            Assert.Equal(process1.Id, queue.DequeueMin().Id);  // priority 1
            Assert.Equal(process5.Id, queue.DequeueMin().Id);  // priority 3
            Assert.Equal(process3.Id, queue.DequeueMin().Id);  // priority 5
            Assert.Equal(process6.Id, queue.DequeueMin().Id);  // priority 6
            Assert.Equal(process4.Id, queue.DequeueMin().Id);  // priority 7
            Assert.Equal(process2.Id, queue.DequeueMin().Id);  // priority 10
        }

        [Fact]
        public static void MinPriorityQueue_PeekAtMinPriority_DoesNotRemove()
        {
            var queue = new MinPriorityQueue<Process, int>();

            var process1 = new Process(1, "First");
            var process2 = new Process(2, "Second");

            queue.Enqueue(process1, 1);
            queue.Enqueue(process2, 10);

            var peeked = queue.PeekAtMinPriority();
            var dequeued = queue.DequeueMin();

            Assert.Equal(process1.Id, peeked.Id);
            Assert.Equal(process1.Id, dequeued.Id);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public static void MinPriorityQueue_Empty_CountIsZero()
        {
            var queue = new MinPriorityQueue<string, int>();

            Assert.Equal(0, queue.Count);
            Assert.True(queue.IsEmpty);
        }

        [Fact]
        public static void MinPriorityQueue_AfterEnqueue_CountIncreases()
        {
            var queue = new MinPriorityQueue<string, int>();

            queue.Enqueue("a", 1);
            queue.Enqueue("b", 2);

            Assert.Equal(2, queue.Count);
            Assert.False(queue.IsEmpty);
        }

        #endregion
    }
}
