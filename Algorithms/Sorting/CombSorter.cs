using System.Collections.Generic;
using Algorithms.Common;

namespace Algorithms.Sorting
{
    public static class CombSorter
    {
        public static void CombSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            collection.ShellSortAscending(comparer);
        }

        /// <summary>
        /// Public API: Sorts ascending
        /// </summary>
        public static void CombSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            double gap = collection.Count;
            bool swaps = true;
            while (gap > 1 || swaps)
            {
                gap /= 1.247330950103979;
                if (gap < 1) { gap = 1; }
                int i = 0;
                swaps = false;
                while (i + gap < collection.Count)
                {
                    int igap = i + (int)gap;
                    if (comparer.Compare(collection[i], collection[igap])>0)
                    {
                        collection.Swap(i,igap);
                        swaps = true;
                    }
                    i++;
                }
            }
        }

        /// <summary>
        /// Public API: Sorts descending
        /// </summary>
        public static void CombSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            double gap = collection.Count;
            bool swaps = true;
            while (gap > 1 || swaps)
            {
                gap /= 1.247330950103979;
                if (gap < 1) { gap = 1; }
                int i = 0;
                swaps = false;
                while (i + gap < collection.Count)
                {
                    int igap = i + (int)gap;
                    if (comparer.Compare(collection[i], collection[igap]) < 0)
                    {
                        collection.Swap(i, igap);
                        swaps = true;
                    }
                    i++;
                }
            }
        }
    }
}
