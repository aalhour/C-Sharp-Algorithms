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


		// The partition function
		private static int InternalPartition<T>(this IList<T> collection, int leftmostIndex, int rightmostIndex) where T : IComparable<T>
		{
			int wallIndex, pivotIndex;

			// Choose the pivot
			// pivotIndex = collection.SelectPivotIndex(leftmostIndex, rightmostIndex);
			pivotIndex = rightmostIndex;
			T pivotValue = collection [pivotIndex];

			// put the chosen pivot at the end of collection
			//collection.Swap(rightmostIndex, pivotIndex);

			// Compare remaining array elements against pivotValue
			wallIndex = leftmostIndex;

			// Loop until pivot: exclusive!
			for (int i = leftmostIndex; i <= (rightmostIndex - 1); i++)
			{
				// check if collection[i] <= pivotValue
				if (collection[i].CompareTo(pivotValue) <= 0)
				{
					collection.Swap(i, wallIndex);
					wallIndex++;
				}
			}

			collection.Swap(wallIndex, pivotIndex);

			return wallIndex;
		}


		// The pivot selection algorithm.
		// Returns the index of the median between (firstElement, middleElement, and lastElement).
		private static int SelectPivotIndex<T>(this IList<T> collection, int leftmostIndex, int rightmostIndex) where T : IComparable<T>
		{
			int middleIndex = (rightmostIndex - leftmostIndex) / 2;

			var arrayOfValues = new T[3] { collection[leftmostIndex], collection[middleIndex], collection[rightmostIndex] };
			arrayOfValues.InsertionSort ();

			if (arrayOfValues [1].Equals(collection [leftmostIndex])) return leftmostIndex;
			else if (arrayOfValues [1].Equals(collection [middleIndex])) return middleIndex;
			else return rightmostIndex;
		}


		// Swap two values in an IList<T> collection given their indexes.
		public static void Swap<T>(this IList<T> list, int index1, int index2)
		{
			if (index1 == index2)   //This check is not required but Partition function may make many calls so its for perf reason
				return;
			
			var temp = list[index1];
			list[index1] = list[index2];
			list[index2] = temp;
		}

	}

}

