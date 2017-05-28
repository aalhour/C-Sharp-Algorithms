using System;
using System.Collections.Generic;

using DataStructures;
using Algorithms.Sorting;

namespace UnitTest.AlgorithmsTests
{
    public static class HeapSorterTest
    {
        public static void DoTest()
        {
            int[] numbersList1 = new int[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            List<long> numbersList2 = new List<long> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };

            numbersList1.HeapSort ();

			// Sort Ascending (same as the method above);
			numbersList2.HeapSortAscending ();

			if (numbersList2 [0] > numbersList2 [numbersList2.Count - 1])
				throw new Exception ();

			// Sort Descending
            numbersList2.HeapSortDescending ();

			if (numbersList2 [0] < numbersList2 [numbersList2.Count - 1])
				throw new Exception ();
        }
    }
}
