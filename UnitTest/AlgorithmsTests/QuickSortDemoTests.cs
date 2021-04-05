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

        [Fact(DisplayName = "demo test 2")]
        public static void DoTest2()
        {
            var list = new List<long>() { 6, 9 , 34, 12, 25, 0, 9 , 18 , 11};
            list.QuickSortDemoSort();
            long[] sortedList = { 0, 6, 9, 9, 11, 12, 18, 25, 34};
            Assert.True(list.SequenceEqual(sortedList));
        }
    }
}
