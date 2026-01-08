using Algorithms.Sorting;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class CountingSortTest
    {
        #region Basic Sort Tests

        [Fact]
        public void CountingSort_RandomOrder_SortsCorrectly()
        {
            var list = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            var expected = list.OrderBy(x => x).ToList();
            
            list.CountingSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void CountingSort_AlreadySorted_RemainsUnchanged()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            
            list.CountingSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void CountingSort_ReverseSorted_SortsCorrectly()
        {
            var list = new List<int> { 5, 4, 3, 2, 1 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            
            list.CountingSort();
            
            Assert.Equal(expected, list);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void CountingSort_EmptyList_ReturnsEmptyList()
        {
            var list = new List<int>();
            
            list.CountingSort();
            
            Assert.Empty(list);
        }

        [Fact]
        public void CountingSort_SingleElement_ReturnsSameElement()
        {
            var list = new List<int> { 42 };
            
            list.CountingSort();
            
            Assert.Single(list);
            Assert.Equal(42, list[0]);
        }

        [Fact]
        public void CountingSort_WithDuplicates_SortsCorrectly()
        {
            var list = new List<int> { 5, 2, 5, 1, 2, 5, 3 };
            var expected = new List<int> { 1, 2, 2, 3, 5, 5, 5 };
            
            list.CountingSort();
            
            Assert.Equal(expected, list);
        }

        [Fact]
        public void CountingSort_AllSameElements_RemainsUnchanged()
        {
            var list = new List<int> { 7, 7, 7, 7, 7 };
            var expected = new List<int> { 7, 7, 7, 7, 7 };
            
            list.CountingSort();
            
            Assert.Equal(expected, list);
        }

        #endregion
    }
}
