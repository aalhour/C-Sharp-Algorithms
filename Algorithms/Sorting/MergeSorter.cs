using System;
using System.Collections.Generic;

using DataStructures;

namespace Algorithms
{
	public static class MergeSorter
	{
		//
		// Public merge-sort API
		public static void MergeSort<T>(this IList<T> collection, Comparer<T> comparer = null)
		{
			comparer = comparer ?? Comparer<T>.Default;

			collection.InternalMergeSort(0, collection.Count - 1, comparer);
		}


		//
		// Private static method
		// Implements the recursive merge-sort algorithm
		private static void InternalMergeSort<T>(this IList<T> collection, int startIndex, int endIndex, Comparer<T> comparer)
		{
			if (endIndex - startIndex < -1)
			{
				return;
			}
			else if (endIndex == startIndex + 1)
			{
				// if collection[endIndex] < collection[startIndex]
				if(comparer.Compare(collection[endIndex], collection[startIndex]) < 0)
				{
					collection.Swap (startIndex, endIndex);
				}
			}

			int middleIndex = startIndex + (endIndex - startIndex) / 2;
				
			collection.InternalMergeSort (startIndex, middleIndex, comparer);
			collection.InternalMergeSort (middleIndex + 1, endIndex, comparer);

			collection.InternalMerge (startIndex, middleIndex, endIndex, comparer);
		}


		//
		// Private static method
		// Implements the merge function inside the merge-sort
		private static void InternalMerge<T>(this IList<T> collection, int startIndex, int middleIndex, int endIndex, Comparer<T> comparer)
		{
			int i = startIndex;
			int j = middleIndex + 1;


		}
	}
}

