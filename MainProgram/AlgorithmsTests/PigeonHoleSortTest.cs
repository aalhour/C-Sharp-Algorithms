using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Sorting;

namespace C_Sharp_Algorithms.AlgorithmsTests
{
    public static class PigeonHoleSortTest
    {
        public static void DoTest()
        {
            DoTestAscending();
            DoTestDescending();
        }

        public static void DoTestAscending()
        {
            List<int> numbers = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            numbers.PigeonHoleSortAscending();
            
            Debug.Assert(numbers.SequenceEqual(numbers.OrderBy(i => i)), "Wrong PigeonHole ascending");
        }

        public static void DoTestDescending()
        {
            List<int> numbers = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            numbers.PigeonHoleSortDescending();
            
            Debug.Assert(numbers.SequenceEqual(numbers.OrderByDescending(i => i)), "Wrong PigeonHole descending");
        }
    }
}
