using System;
using System.Collections.Generic;

using DataStructures;
using Algorithms;

namespace C_Sharp_Algorithms.AlgorithmsTests
{
	public static class QuickSortTest
	{
		public static void DoTest ()
		{
			List<long> list2 = new List<long> ();

			list2.Add (23);
			list2.Add (42);
			list2.Add (4);
			list2.Add (16);
			list2.Add (8);
			list2.Add (15);
			list2.Add (3);
			list2.Add (9);
			list2.Add (55);
			list2.Add (0);
			list2.Add (34);
			list2.Add (12);
			list2.Add (2);
			list2.Add (46);
			list2.Add (25);

			list2.QuickSort ();
		}
	}
}

