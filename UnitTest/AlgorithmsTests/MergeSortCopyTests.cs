using Algorithms.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class MergeSortCopyTests
    {
        [Fact]
        public static void DoTest()
        {
            List<int> numbersList = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            var sortedList = numbersList.MergeSortCopySort();
            int[] expectedSortedList = { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 };
            Assert.True(sortedList.SequenceEqual(expectedSortedList));
        }
    }
}
