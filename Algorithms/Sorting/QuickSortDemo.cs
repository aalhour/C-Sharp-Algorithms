using Algorithms.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    public static class QuickSortDemo
    {
        //public API for quick sort algorithm
        public static void QuickSortDemoSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            int startIndex = 0;
            int endIndex = collection.Count - 1;

            comparer = comparer ?? Comparer<T>.Default;
            collection.InternalQuickSortDemo(startIndex, endIndex, comparer);
        }

        //recursive quick sort algorithm
        private static void InternalQuickSortDemo<T>(this IList<T> collection, int leftMostIndex, int rightMostIndex, Comparer<T> comparer)
        {
            //recursive call check
            if (leftMostIndex < rightMostIndex)
            {
                int wallIndex = collection.InternalPartitionDemo(leftMostIndex, rightMostIndex, comparer);
                collection.InternalQuickSortDemo(leftMostIndex, wallIndex - 1, comparer);
                collection.InternalQuickSortDemo(wallIndex + 1, rightMostIndex, comparer);
            }
        }

        //partition function
        private static int InternalPartitionDemo<T>(this IList<T> collection, int leftmostIndex, int rightmostIndex, Comparer<T> comparer)
        {
            int wallIndex, pivotIndex;

            //set the pivot - always the right most index
            pivotIndex = rightmostIndex;
            T pivotValue = collection[pivotIndex];

            wallIndex = leftmostIndex;
            //comparing remaining array elements against pivotvalue
            //starts from left most index
            //add 1 
            //until to the end 
            for (int i = leftmostIndex; i <= (rightmostIndex - 1); i++)
            {
                //check if collection[i] <= pivot
                if (comparer.Compare(collection[i], pivotValue) <= 0)
                {
                    //swap element
                    collection.Swap(i, wallIndex);
                    wallIndex++;
                }
            }

            collection.Swap(wallIndex, pivotIndex);
            return wallIndex;
        }
    }
}
