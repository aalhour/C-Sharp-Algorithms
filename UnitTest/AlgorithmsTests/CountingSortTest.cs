using System;
using System.Diagnostics;

using Algorithms.Sorting;
using Xunit;
using System.Linq;

namespace UnitTest.AlgorithmsTests
{
    public static class CountingSortTest
    {
        [Fact]
        public static void DoTest()
        {
            int[] numbersList1 = new int[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            numbersList1.CountingSort();
            Assert.True(numbersList1.SequenceEqual(new[] { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 }));
        }
    }
}
