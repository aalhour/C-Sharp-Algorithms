using Algorithms.Sorting;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class InsertionSortTest
    {
        #region Basic Sort Tests

        [Fact]
        public void InsertionSort_RandomOrder_SortsCorrectly()
        {
            var list = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            var expected = list.OrderBy(x => x).ToList();
            
            list.InsertionSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void InsertionSort_AlreadySorted_RemainsUnchanged()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            
            list.InsertionSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void InsertionSort_ReverseSorted_SortsCorrectly()
        {
            var list = new List<int> { 5, 4, 3, 2, 1 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            
            list.InsertionSort();
            
            Assert.Equal(expected, list);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void InsertionSort_EmptyList_ReturnsEmptyList()
        {
            var list = new List<int>();
            
            list.InsertionSort();
            
            Assert.Empty(list);
        }

        [Fact]
        public void InsertionSort_SingleElement_ReturnsSameElement()
        {
            var list = new List<int> { 42 };
            
            list.InsertionSort();
            
            Assert.Single(list);
            Assert.Equal(42, list[0]);
        }

        [Fact]
        public void InsertionSort_WithDuplicates_SortsCorrectly()
        {
            var list = new List<int> { 5, 2, 5, 1, 2, 5, 3 };
            var expected = new List<int> { 1, 2, 2, 3, 5, 5, 5 };
            
            list.InsertionSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void InsertionSort_NegativeNumbers_SortsCorrectly()
        {
            var list = new List<int> { -5, 3, -1, 0, -10, 7 };
            var expected = new List<int> { -10, -5, -1, 0, 3, 7 };
            
            list.InsertionSort();
            
            Assert.Equal(expected, list);
        }

        #endregion
    }
}
