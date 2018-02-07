using System.Collections.Generic;
using Algorithms.Common;

namespace Algorithms.Sorting
{
    public static class QuickSorter
    {
        //
        // The public APIs for the quick sort algorithm.
        public static void QuickSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            int startIndex = 0;
            int endIndex = collection.Count - 1;

            //
            // If the comparer is Null, then initialize it using a default typed comparer
            comparer = comparer ?? Comparer<T>.Default;

            collection.InternalQuickSort(startIndex, endIndex, comparer);
        }


        //
        // Private static method
        // The recursive quick sort algorithm
        private static void InternalQuickSort<T>(this IList<T> collection, int leftmostIndex, int rightmostIndex, Comparer<T> comparer)
        {
            //
            // Recursive call check
            if (leftmostIndex < rightmostIndex)
            {
                int wallIndex = collection.InternalPartition(leftmostIndex, rightmostIndex, comparer);
                collection.InternalQuickSort(leftmostIndex, wallIndex - 1, comparer);
                collection.InternalQuickSort(wallIndex + 1, rightmostIndex, comparer);
            }
        }


        //
        // Private static method
        // The partition function, used in the quick sort algorithm
        private static int InternalPartition<T>(this IList<T> collection, int leftmostIndex, int rightmostIndex, Comparer<T> comparer)
        {
            int wallIndex, pivotIndex;

            // Choose the pivot
            pivotIndex = rightmostIndex;
            T pivotValue = collection[pivotIndex];

            // Compare remaining array elements against pivotValue
            wallIndex = leftmostIndex;

            // Loop until pivot: exclusive!
            for (int i = leftmostIndex; i <= (rightmostIndex - 1); i++)
            {
                // check if collection[i] <= pivotValue
                if (comparer.Compare(collection[i], pivotValue) <= 0)
                {
                    collection.Swap(i, wallIndex);
                    wallIndex++;
                }
            }

            collection.Swap(wallIndex, pivotIndex);

            return wallIndex;
        }

    }

}

