using Algorithms.Common;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public static class BubbleSorterCopy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        public static void BubbleSortCopy<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            collection.BubbleSortAscendingCopy<T>(comparer);
        }

        /// <summary>
        /// Sort ascending
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        public static void BubbleSortAscendingCopy<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                for (int index = 0; index < collection.Count - i -1; index++)
                {
                    if (comparer.Compare(collection[index], collection[index+1]) >0) 
                    {
                        collection.Swap(index, index + 1);
                    }
                }

            }
        }

        /// <summary>
        /// sort descending
        /// loop through the whole list; start from the 2nd position; compare to the previous position; 
        /// if bigger than the previous position then swap them; 
        /// so the smallest one will be moved to the end of the list in the first run
        /// and so on; the no of items in the 2nd loop will be reduced by 1;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        public static void BubbleSortDecendingCopy<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for (int i = 0; i < collection.Count - 1; i++)
            {
                for (int index = 1; index < collection.Count - i; index++)
                {
                    if (comparer.Compare(collection[index], collection[index-1]) > 0)
                    {
                        collection.Swap(index, index-1);
                    }
                }
            }
        }           
    }
}
