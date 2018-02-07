using System.Collections.Generic;
using Algorithms.Sorting;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class HeapSorterTest
    {
        [Fact]
        public static void DoTest()
        {
            int[] numbersList1 = new int[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            List<long> numbersList2 = new List<long> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };

            numbersList1.HeapSort();

            // Sort Ascending (same as the method above);
            numbersList2.HeapSortAscending();

            Assert.True(numbersList2[numbersList2.Count - 1] == numbersList2[numbersList2.Count - 1]);

            // Sort Descending
            numbersList2.HeapSortDescending();

            Assert.True(numbersList2[0] > numbersList2[numbersList2.Count - 1]);
        }
    }
}
