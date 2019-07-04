using System.Collections.Generic;
using DataStructures.Lists;

namespace Algorithms.Sorting
{
    /// <summary>
    /// Implements this Insertion Sort algorithm over ArrayLists.
    /// </summary>
    public static class InsertionSorter
    {
        //
        // The quick insertion sort algorithm.
        // For any collection that implements the IList interface.
        public static void InsertionSort<T>(this IList<T> list, Comparer<T> comparer = null)
        {
            //
            // If the comparer is Null, then initialize it using a default typed comparer
            comparer = comparer ?? Comparer<T>.Default;

            // Do sorting if list is not empty.
            int i, j;
            for (i = 1; i < list.Count; i++)
            {
                T value = list[i];
                j = i - 1;

                while ((j >= 0) && (comparer.Compare(list[j], value) > 0))
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = value;
            }
        }


        //
        // The quick insertion sort algorithm.
        // For the internal ArrayList<T>. Check the DataStructures.ArrayList.cs.
        public static void InsertionSort<T>(this ArrayList<T> list, Comparer<T> comparer = null)
        {
            //
            // If the comparer is Null, then initialize it using a default typed comparer
            comparer = comparer ?? Comparer<T>.Default;

            for (int i = 1; i < list.Count; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (comparer.Compare(list[j], list[j - 1]) < 0) //(j)th is less than (j-1)th
                    {
                        var temp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }

    }

}

