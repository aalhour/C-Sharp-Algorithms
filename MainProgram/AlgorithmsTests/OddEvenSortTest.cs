using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Sorting;

namespace C_Sharp_Algorithms.AlgorithmsTests
{
    public static class OddEvenSortTest
    {
        public static void DoTest()
        {
            DoTestAscending();
            DoTestDescending();
        }

        public static void DoTestAscending()
        {
            List<int> numbers = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            numbers.OddEvenSortAscending(Comparer<int>.Default);
            
            Debug.Assert(numbers.SequenceEqual(numbers.OrderBy(i => i)), "Wrong OddEven ascending");
        }

        public static void DoTestDescending()
        {
            List<int> numbers = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            numbers.OddEvenSortDescending(Comparer<int>.Default);
            
            Debug.Assert(numbers.SequenceEqual(numbers.OrderByDescending(i => i)), "Wrong OddEven descending");
        }
    }
}
