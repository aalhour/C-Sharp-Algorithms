using Algorithms.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class BubbleSorterTest
    {
        #region Ascending Sort Tests

        [Fact]
        public void BubbleSort_RandomOrder_SortsAscending()
        {
            var list = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            var expected = list.OrderBy(x => x).ToList();
            
            list.BubbleSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void BubbleSort_AlreadySorted_RemainsUnchanged()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            
            list.BubbleSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void BubbleSort_ReverseSorted_SortsCorrectly()
        {
            var list = new List<int> { 5, 4, 3, 2, 1 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            
            list.BubbleSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void BubbleSort_EmptyList_ReturnsEmptyList()
        {
            var list = new List<int>();
            
            list.BubbleSort();
            
            Assert.Empty(list);
        }

        [Fact]
        public void BubbleSort_SingleElement_ReturnsSameElement()
        {
            var list = new List<int> { 42 };
            
            list.BubbleSort();
            
            Assert.Single(list);
            Assert.Equal(42, list[0]);
        }

        [Fact]
        public void BubbleSort_WithDuplicates_SortsCorrectly()
        {
            var list = new List<int> { 5, 2, 5, 1, 2, 5, 3 };
            var expected = new List<int> { 1, 2, 2, 3, 5, 5, 5 };
            
            list.BubbleSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void BubbleSort_AllSameElements_RemainsUnchanged()
        {
            var list = new List<int> { 7, 7, 7, 7, 7 };
            var expected = new List<int> { 7, 7, 7, 7, 7 };
            
            list.BubbleSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void BubbleSort_NegativeNumbers_SortsCorrectly()
        {
            var list = new List<int> { -5, 3, -1, 0, -10, 7 };
            var expected = new List<int> { -10, -5, -1, 0, 3, 7 };
            
            list.BubbleSort();
            
            Assert.Equal(expected, list);
        }

        #endregion

        #region Descending Sort Tests

        [Fact]
        public void BubbleSortDescending_RandomOrder_SortsDescending()
        {
            var list = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            var expected = list.OrderByDescending(x => x).ToList();
            
            list.BubbleSortDescending(Comparer<int>.Default);
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void BubbleSortDescending_EmptyList_ReturnsEmptyList()
        {
            var list = new List<int>();
            
            list.BubbleSortDescending(Comparer<int>.Default);
            
            Assert.Empty(list);
        }

        [Fact]
        public void BubbleSortDescending_SingleElement_ReturnsSameElement()
        {
            var list = new List<int> { 42 };
            
            list.BubbleSortDescending(Comparer<int>.Default);
            
            Assert.Single(list);
            Assert.Equal(42, list[0]);
        }

        #endregion

        #region String Sort Tests

        [Fact]
        public void BubbleSort_Strings_SortsAlphabetically()
        {
            var list = new List<string> { "banana", "apple", "cherry", "date" };
            var expected = new List<string> { "apple", "banana", "cherry", "date" };
            
            list.BubbleSort();
            
            Assert.Equal(expected, list);
        }

        #endregion
    }
}
