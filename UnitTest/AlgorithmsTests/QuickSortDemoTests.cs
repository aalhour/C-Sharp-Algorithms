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
            var list = new int[]{23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            QuickSortDemo.QuickSort(list);
            int[] sortedList = { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 };
            Assert.True(list.SequenceEqual(sortedList));
        }

        [Fact(DisplayName = "demo test 2")]
        public static void DoTest2()
        {
            var list = new int[]{ 6, 9 , 34, 12, 25, 0, 9 , 18 , 11};
            QuickSortDemo.QuickSort(list);
            int[] sortedList = { 0, 6, 9, 9, 11, 12, 18, 25, 34};
            Assert.True(list.SequenceEqual(sortedList));
        }
        
        [Fact(DisplayName = "demo test 3")]
        public static void DoTest3()
        {
            var list = new int[]{ 6, 1 , 3, 7, 5};
            QuickSortDemo.QuickSort(list);
            int[] sortedList = { 1, 3, 5,6, 7 };
            Assert.True(list.SequenceEqual(sortedList));
        }
    }
}
