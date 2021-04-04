using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Algorithms.Sorting;
using System.Linq;

namespace UnitTest.AlgorithmsTests
{
    public class QuickSortDemoTests
    {
        [Fact]
        public static void DoTest()
        {
            var list = new List<long>() { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            list.QuickSortDemoSort();
            long[] sortedList = { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 };
            Assert.True(list.SequenceEqual(sortedList));
        }
    }
}
