using System;
using System.Collections.Generic;

using DataStructures;

namespace Algorithms
{
	public static class QuickSorter
	{
		/***
		 * The public APIs for the quick sort algorithm.
		 */

		public static void QuickSort<T>(this IList<T> collection) where T : IComparable<T>
		{
			int startIndex = 0;
			int endIndex = collection.Count - 1;
			collection.InternalQuickSort(startIndex, endIndex);
		}


		/***
		 * The quick sort algorithm.
		 */

		// The partition function
		private static int InternalPartition<T>(this IList<T> collection, int leftmostIndex, int rightmostIndex) where T : IComparable<T>
		{
			int wallIndex;

			// Choose pivotValue = last element in collection
			int pivotIndex = rightmostIndex;
			T pivotValue = collection [pivotIndex];

			// Compare remaining array elements against pivotValue
			wallIndex = leftmostIndex;

			// Loop until pivot: exclusive!
			for (int i = leftmostIndex; i <= (rightmostIndex - 1); i++)
			{
				// check if collection[i] <= pivotValue
				if (collection[i].Equals(pivotValue) || collection[i].CompareTo(pivotValue) < 0)
				{
					T swapVal = collection [i];
					collection [i] = collection [wallIndex];
					collection [wallIndex] = swapVal;
					wallIndex++;
				}
			}

			T swapStoreVal = collection[wallIndex];
			collection[wallIndex] = pivotValue;
			collection[pivotIndex] = swapStoreVal;

			return wallIndex;
		}


		// The recursive quick sort algorithm
		private static void InternalQuickSort<T>(this IList<T> collection, int leftmostIndex, int rightmostIndex) where T : IComparable<T>
		{
			if (leftmostIndex < rightmostIndex)
			{
				int wallIndex = collection.InternalPartition (leftmostIndex, rightmostIndex);
				collection.InternalQuickSort (leftmostIndex, wallIndex - 1);
				collection.InternalQuickSort (wallIndex + 1, rightmostIndex);
			}
		}

	}

}

