using Algorithms.Sorting;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class BubbleSorterTest
    {
        [Fact]
        public static void DoTest()
        {
            var list = new List<int>() { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            list.BubbleSort();
            Assert.True(list.SequenceEqual(list.OrderBy(x => x)), "Wrong BubbleSort ascending");

            list.BubbleSortDescending(Comparer<int>.Default);
            Assert.True(list.SequenceEqual(list.OrderByDescending(x => x)), "Wrong BubbleSort descending");
        }
    }
}
