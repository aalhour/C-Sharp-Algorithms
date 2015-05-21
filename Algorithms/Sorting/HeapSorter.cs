using System;
using System.Collections.Generic;

using DataStructures;

namespace Algorithms.Sorting
{
    public static class HeapSorter
    {
        public static void HeapSort<T>(this IList<T> collection)
        {
        }


        public static void HeapSort(int[] array)
        {
            array.BuildMaxHeap();
        }

        private static void BuildMaxHeap(this int[] array)
        {
            int leavesStartAt = array.Length / 2;

            for(int i = leavesStartAt - 1; i >= 0; --i)
            {
                array.MaxHeapify(i);
            }
        }

        private static void MaxHeapify(this int[] array, int node)
        {
            // assume left(i) and right(i) are mx-heaps
            int left = (node * 2) + 1;
            int right = left + 1;

            if (left < array.Length && array[node] < array[left])
            {
                array.Swap(node, left);
            }

            if (right < array.Length && array[node] < array[right])
            {
                array.Swap(node, right);
            }
        }

        public static string VisualizeHeap(int[] array)
        {
            string visual = string.Empty;

            int root = 0;
            int left = 2 * root;
            int right = left + 1;

            for (int i = array.Length / 2; i < array.Length; ++i)
            {
                visual = visual + String.Format("");
            }

            return visual;
        }
    }
}
