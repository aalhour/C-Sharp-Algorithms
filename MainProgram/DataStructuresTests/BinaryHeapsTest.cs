using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Heaps;
using Algorithms.Sorting;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class MinHeapTest
	{
		public static void DoTest ()
		{
			BinaryMinHeap<long> minHeap = new BinaryMinHeap<long> (Comparer<long>.Default);

			minHeap.Add (23);
			minHeap.Add (42);
			minHeap.Add (4);
			minHeap.Add (16);
			minHeap.Add (8);
			minHeap.Add (15);
			minHeap.Add (9);
			minHeap.Add (55);
			minHeap.Add (0);
			minHeap.Add (34);
			minHeap.Add (12);
			minHeap.Add (2);
			minHeap.Add (93);
			minHeap.Add (14);
			minHeap.Add (27);

			var array = minHeap.ToArray ();
			Debug.Assert (array.Length == minHeap.Count, "Wrong size.");

			var list = minHeap.ToList ();
			Debug.Assert (list.Count == minHeap.Count, "Wrong size.");

			array.HeapSortDescending();
			var maxHeap = minHeap.ToMaxHeap ();
			Debug.Assert (maxHeap.Peek() == array[0], "Wrong maximum.");
		}
	}
}

