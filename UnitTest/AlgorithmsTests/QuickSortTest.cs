using Algorithms.Sorting;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class QuickSortTest
    {
        #region Basic Sort Tests

        [Fact]
        public void QuickSort_RandomOrder_SortsCorrectly()
        {
            var list = new List<long> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            var expected = new List<long> { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 };
            
            list.QuickSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void QuickSort_AlreadySorted_RemainsUnchanged()
        {
            var list = new List<long> { 1, 2, 3, 4, 5 };
            var expected = new List<long> { 1, 2, 3, 4, 5 };
            
            list.QuickSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void QuickSort_ReverseSorted_SortsCorrectly()
        {
            var list = new List<long> { 5, 4, 3, 2, 1 };
            var expected = new List<long> { 1, 2, 3, 4, 5 };
            
            list.QuickSort();
            
            Assert.Equal(expected, list);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void QuickSort_EmptyList_ReturnsEmptyList()
        {
            var list = new List<long>();
            
            list.QuickSort();
            
            Assert.Empty(list);
        }

        [Fact]
        public void QuickSort_SingleElement_ReturnsSameElement()
        {
            var list = new List<long> { 42 };
            
            list.QuickSort();
            
            Assert.Single(list);
            Assert.Equal(42, list[0]);
        }

        [Fact]
        public void QuickSort_TwoElements_SortsCorrectly()
        {
            var list = new List<long> { 2, 1 };
            var expected = new List<long> { 1, 2 };
            
            list.QuickSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void QuickSort_WithDuplicates_SortsCorrectly()
        {
            var list = new List<long> { 5, 2, 5, 1, 2, 5, 3 };
            var expected = new List<long> { 1, 2, 2, 3, 5, 5, 5 };
            
            list.QuickSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void QuickSort_AllSameElements_RemainsUnchanged()
        {
            var list = new List<long> { 7, 7, 7, 7, 7 };
            var expected = new List<long> { 7, 7, 7, 7, 7 };
            
            list.QuickSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void QuickSort_NegativeNumbers_SortsCorrectly()
        {
            var list = new List<long> { -5, 3, -1, 0, -10, 7 };
            var expected = new List<long> { -10, -5, -1, 0, 3, 7 };
            
            list.QuickSort();
            
            Assert.Equal(expected, list);
        }

        #endregion

        #region Large Dataset Tests

        [Fact]
        public void QuickSort_LargeDataset_SortsCorrectly()
        {
            var random = new System.Random(42); // Fixed seed for reproducibility
            var list = Enumerable.Range(0, 1000).Select(_ => (long)random.Next(-1000, 1000)).ToList();
            var expected = list.OrderBy(x => x).ToList();
            
            list.QuickSort();
            
            Assert.Equal(expected, list);
        }

        #endregion
    }
}
