using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Sorting;

namespace C_Sharp_Algorithms.AlgorithmsTests
{
    public static class BucketSortTest
    {
        public static void DoTest()
        {
            DoTestAscending();
            DoTestDescending();
        }

        public static void DoTestAscending()
        {
            int[] numbers = { 54, 26, 93, 17, 77, 31, 44, 55, 20 };
            numbers.BucketSortAscending();
            Debug.Assert(numbers.SequenceEqual(numbers.OrderBy(i => i)), "Wrong BucketSort ascending");
        }

        public static void DoTestDescending()
        {
            List<int> numbers = new List<int> { 84, 69, 76, 86, 94, 91 };
            numbers.BucketSortDescending();
            Debug.Assert(numbers.SequenceEqual(numbers.OrderByDescending(i => i)), "Wrong BucketSort descending");
        }
    }
}
