using System;
using System.Collections.Generic;

using DataStructures;
using Algorithms.Sorting;

namespace UnitTest.AlgorithmsTests
{
	public static class QuickSortTest
	{
		public static void DoTest ()
		{
			List<long> list = new List<long> () { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
			list.QuickSort ();
		}
	}
}

