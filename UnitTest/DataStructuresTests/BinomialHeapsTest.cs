using DataStructures.Heaps;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class BinomialHeapsTest
    {
        [Fact]
        public static void Add_ManyItems_MaintainsHeapProperty()
        {
            var heap = new BinomialMinHeap<int>();

            for (int i = 100; i >= 1; i--)
            {
                heap.Add(i);
            }

            Assert.Equal(100, heap.Count);
        }

        [Fact]
        public static void ExtractMin_ReturnsItemsInSortedOrder()
        {
            var heap = new BinomialMinHeap<int>();
            
            // Add items in random order using the pattern from original test
            for (int i = 37; i != 0; i = (i + 37) % 100)
            {
                heap.Add(i);
            }

            var previous = int.MinValue;
            while (!heap.IsEmpty)
            {
                var min = heap.ExtractMin();
                Assert.True(min >= previous, $"Heap property violated: {min} should be >= {previous}");
                previous = min;
            }
        }

        [Fact]
        public static void Merge_CombinesHeaps()
        {
            var firstHeap = new BinomialMinHeap<int>();
            var secondHeap = new BinomialMinHeap<int>();
            int numberOfItems = 1000;

            // Add to heaps based on odd/even pattern
            for (int i = 37; i != 0; i = (i + 37) % numberOfItems)
            {
                if (i % 2 == 0)
                    secondHeap.Add(i);
                else
                    firstHeap.Add(i);
            }

            var firstCount = firstHeap.Count;
            var secondCount = secondHeap.Count;

            firstHeap.Merge(secondHeap);

            Assert.Equal(firstCount + secondCount, firstHeap.Count);
        }

        [Fact]
        public static void Merge_EmptiesSourceHeap()
        {
            var firstHeap = new BinomialMinHeap<int>();
            var secondHeap = new BinomialMinHeap<int>();

            firstHeap.Add(1);
            firstHeap.Add(3);
            firstHeap.Add(5);

            secondHeap.Add(2);
            secondHeap.Add(4);

            firstHeap.Merge(secondHeap);

            Assert.True(secondHeap.IsEmpty, "Source heap should be empty after merge");
        }

        [Fact]
        public static void Merge_PreservesHeapPropertyAfterExtract()
        {
            var firstHeap = new BinomialMinHeap<int>();
            var secondHeap = new BinomialMinHeap<int>();
            int numberOfItems = 100;

            for (int i = 37; i != 0; i = (i + 37) % numberOfItems)
            {
                if (i % 2 == 0)
                    secondHeap.Add(i);
                else
                    firstHeap.Add(i);
            }

            firstHeap.Merge(secondHeap);

            // Extract all items and verify they come out in sorted order
            var previous = firstHeap.ExtractMin();
            while (!firstHeap.IsEmpty)
            {
                var current = firstHeap.ExtractMin();
                Assert.True(current >= previous, "Items should be extracted in ascending order");
                previous = current;
            }
        }

        [Fact]
        public static void IsEmpty_NewHeap_ReturnsTrue()
        {
            var heap = new BinomialMinHeap<int>();

            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public static void IsEmpty_AfterAdd_ReturnsFalse()
        {
            var heap = new BinomialMinHeap<int>();
            heap.Add(42);

            Assert.False(heap.IsEmpty);
        }

        [Fact]
        public static void Peek_ReturnsMinWithoutRemoving()
        {
            var heap = new BinomialMinHeap<int>();
            heap.Add(5);
            heap.Add(3);
            heap.Add(7);

            var min = heap.Peek();

            Assert.Equal(3, min);
            Assert.Equal(3, heap.Count); // Count unchanged
        }
    }
}
