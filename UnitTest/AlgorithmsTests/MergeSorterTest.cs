using Algorithms.Sorting;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class MergeSorterTest
    {
        #region Basic Sort Tests

        [Fact]
        public static void MergeSort_RandomOrder_SortsCorrectly()
        {
            var list = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            var expected = new List<int> { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 };
            
            var result = list.MergeSort();
            
            Assert.True(result.SequenceEqual(expected));
        }

        [Fact]
        public static void MergeSort_ReverseSorted_SortsCorrectly()
        {
            var list = new List<int> { 5, 4, 3, 2, 1 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            
            var result = list.MergeSort();
            
            Assert.True(result.SequenceEqual(expected));
        }

        [Fact]
        public static void MergeSort_AlreadySorted_RemainsSorted()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            
            var result = list.MergeSort();
            
            Assert.True(result.SequenceEqual(expected));
        }

        #endregion

        #region Edge Cases

        [Fact]
        public static void MergeSort_EmptyList_ReturnsEmptyList()
        {
            var list = new List<int>();
            
            var result = list.MergeSort();
            
            Assert.Empty(result);
        }

        [Fact]
        public static void MergeSort_SingleElement_ReturnsSameElement()
        {
            var list = new List<int> { 42 };
            
            var result = list.MergeSort();
            
            Assert.Single(result);
            Assert.Equal(42, result[0]);
        }

        [Fact]
        public static void MergeSort_TwoElements_SortsCorrectly()
        {
            var list = new List<int> { 2, 1 };
            
            var result = list.MergeSort();
            
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0]);
            Assert.Equal(2, result[1]);
        }

        #endregion

        #region Duplicate and Negative Values

        [Fact]
        public static void MergeSort_WithDuplicates_SortsCorrectly()
        {
            var list = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var expected = new List<int> { 1, 1, 2, 3, 3, 4, 5, 5, 5, 6, 9 };
            
            var result = list.MergeSort();
            
            Assert.True(result.SequenceEqual(expected));
        }

        [Fact]
        public static void MergeSort_AllSameValue_ReturnsIdenticalList()
        {
            var list = new List<int> { 5, 5, 5, 5, 5 };
            
            var result = list.MergeSort();
            
            Assert.Equal(5, result.Count);
            Assert.All(result, x => Assert.Equal(5, x));
        }

        [Fact]
        public static void MergeSort_NegativeNumbers_SortsCorrectly()
        {
            var list = new List<int> { -5, 3, -1, 0, -10, 7 };
            var expected = new List<int> { -10, -5, -1, 0, 3, 7 };
            
            var result = list.MergeSort();
            
            Assert.True(result.SequenceEqual(expected));
        }

        #endregion

        #region Immutability Tests

        [Fact]
        public static void MergeSort_DoesNotModifyOriginalList()
        {
            var original = new List<int> { 3, 1, 4, 1, 5 };
            var originalCopy = new List<int> { 3, 1, 4, 1, 5 };
            
            var result = original.MergeSort();
            
            // Original should be unchanged
            Assert.True(original.SequenceEqual(originalCopy));
            // Result should be sorted
            Assert.True(result.SequenceEqual(new List<int> { 1, 1, 3, 4, 5 }));
        }

        #endregion

        #region Large Dataset Test

        [Fact]
        public static void MergeSort_LargeDataset_SortsCorrectly()
        {
            var random = new System.Random(42); // Seeded for reproducibility
            var list = Enumerable.Range(0, 1000)
                .Select(_ => random.Next(-500, 500))
                .ToList();
            
            var result = list.MergeSort();
            
            // Verify sorted order
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i] <= result[i + 1], 
                    $"Not sorted at index {i}: {result[i]} > {result[i + 1]}");
            }
            
            // Verify same elements (by checking counts match)
            Assert.Equal(list.Count, result.Count);
        }

        #endregion

        #region String Sort Tests

        [Fact]
        public static void MergeSort_Strings_SortsAlphabetically()
        {
            var list = new List<string> { "banana", "apple", "cherry", "date" };
            var expected = new List<string> { "apple", "banana", "cherry", "date" };
            
            var result = list.MergeSort();
            
            Assert.True(result.SequenceEqual(expected));
        }

        #endregion

        #region Custom Comparer Tests

        [Fact]
        public static void MergeSort_WithDescendingComparer_SortsDescending()
        {
            var list = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6 };
            var expected = new List<int> { 9, 6, 5, 4, 3, 2, 1, 1 };
            
            var result = list.MergeSort(Comparer<int>.Create((a, b) => b.CompareTo(a)));
            
            Assert.True(result.SequenceEqual(expected));
        }

        #endregion
    }
}
