using System.Collections.Generic;
using Algorithms.Common;

namespace Algorithms.Sorting
{
    /// <summary>
    /// Based on bubble sort
    /// </summary>
    public static class OddEvenSorter
    {
        public static void OddEvenSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            collection.OddEvenSortAscending(comparer);
        }

        /// <summary>
        /// Public API: Sorts ascending
        /// </summary>
        public static void OddEvenSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (var i = 1; i < collection.Count - 1; i += 2)
                {
                    if (comparer.Compare(collection[i], collection[i + 1]) > 0)
                    {
                        collection.Swap(i, i + 1);
                        sorted = false;
                    }
                }

                for (var i = 0; i < collection.Count - 1; i += 2)
                {
                    if (comparer.Compare(collection[i], collection[i + 1]) > 0)
                    {
                        collection.Swap(i, i + 1);
                        sorted = false;
                    }
                }
            }
        }

        /// <summary>
        /// Public API: Sorts descending
        /// </summary>
        public static void OddEvenSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (var i = 1; i < collection.Count - 1; i += 2)
                {
                    if (comparer.Compare(collection[i], collection[i + 1]) < 0)
                    {
                        collection.Swap(i, i + 1);
                        sorted = false;
                    }
                }

                for (var i = 0; i < collection.Count - 1; i += 2)
                {
                    if (comparer.Compare(collection[i], collection[i + 1]) < 0)
                    {
                        collection.Swap(i, i + 1);
                        sorted = false;
                    }
                }
            }
        }
    }
}
