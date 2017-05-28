using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures;
using Algorithms.Sorting;

namespace UnitTest.AlgorithmsTests
{
	public static class MergeSorterTest
	{
		public static void DoTest ()
		{
            List<int> numbersList = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };

            var sortedList = numbersList.MergeSort();
			Debug.Assert (sortedList[0] == 0, "Wrong min value!");
		}
	}
}

