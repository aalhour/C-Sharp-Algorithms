using System;
using System.Collections.Generic;


namespace Algorithms.Search
{
    public static class BinarySearcher
    {


        /// <summary>
        /// Apply Binary Search in a list.
        /// </summary>
        /// <typeparam name="T">Data type of items</typeparam>
        /// <param name="collection">A list of items</param>
        /// <param name="item">The item we search for</param>
        /// <returns>If item found, its' index, -1 otherwise</returns>
        public static int BinarySearch<T>(IList<T> collection, T item)
        {
            if (collection == null)
            {
                throw new NullReferenceException("List is null");
            }

            IList<T> sCollection = MergeSort<T>(collection);
            int itemIndex = sCollection.InternalBinarySearch(0, sCollection.Count - 1, item);
            return itemIndex;
        }


        /// <summary>
        /// Apply MerseSort algorithm in a list.
        /// </summary>
        /// <typeparam name="T">Data type of items</typeparam>
        /// <param name="collection">A list of items</param>
        /// <returns>Given list in ascending order</returns>
        public static IList<T> MergeSort<T>(IList<T> collection)
        {
            int mid = collection.Count / 2;
            IList<T> leftList;
            IList<T> rightList;

            if (collection.Count < 2)
            {
                return collection;
               
            }

            leftList = GetCopyOfList(collection, 0, mid - 1);
            rightList = GetCopyOfList(collection, mid, collection.Count - 1);

            leftList = MergeSort<T>(leftList);
            rightList = MergeSort<T>(rightList);

            return InternalMerge<T>(leftList, rightList);
        }


        /// <summary>
        /// Merge two lists in ascending order.
        /// </summary>
        /// <typeparam name="T">Data type of items</typeparam>
        /// <param name="leftCollection">The first list of items</param>
        /// <param name="rightCollection">The second list of items</param>
        /// <returns>Merged list in ascending order</returns>
        private static IList<T> InternalMerge<T>(IList<T> leftCollection, IList<T> rightCollection)
        {
            int i = 0;
            int j = 0;
 
            IList<T> tmp = new List<T>();
            Comparer<T> comparer = Comparer<T>.Default;

            while (i < leftCollection.Count && j < rightCollection.Count)
            {
                if (comparer.Compare(leftCollection[i], rightCollection[j]) <= 0)
                {
                    tmp.Add(leftCollection[i++]);
                }
                else
                {
                    tmp.Add(rightCollection[j++]);
                }
            }

            //Copy the remaining items of left list
            while(i < leftCollection.Count)
            {
                tmp.Add(leftCollection[i++]);
            }

            //Copy the remaining items of right list
            while (j < rightCollection.Count)
            {
                tmp.Add(rightCollection[j++]);
            }

            return tmp;
        }


        /// <summary>
        /// An implementation of binary search algorithm.
        /// </summary>
        /// <typeparam name="T">Data type of items</typeparam>
        /// <param name="collection">A list of items</param>
        /// <param name="left">The far left index of the list</param>
        /// <param name="right">The far right index of the list</param>
        /// <param name="item">The item we search for</param>
        /// <returns>If found, the index of the item, otherwise -1</returns>
        private static int InternalBinarySearch<T>(this IList<T> collection, int left, int right, T item)
        {
            int mid = left + (right - left) / 2;
            Comparer<T> comparer = Comparer<T>.Default;

            if (left <= right)
            {
                if (comparer.Compare(item,collection[mid]) < 0)
                {
                    return collection.InternalBinarySearch<T>(left, mid - 1, item);
                }
                else if (comparer.Compare(item, collection[mid]) > 0)
                {
                    return collection.InternalBinarySearch<T>(mid + 1, right, item);
                }
                else
                {
                    return mid;
                }
            }
            else
            {
                return -1;
            }

        }


        /// <summary>
        /// Copies a range of a list to another list
        /// </summary>
        /// <typeparam name="T">Data type of items</typeparam>
        /// <param name="list">A list of items</param>
        /// <param name="left">Starting index of the range</param>
        /// <param name="right">The last index of the range</param>
        /// <returns>A copy of the given list within the given range</returns>
        private static IList<T> GetCopyOfList<T>(IList<T> list, int left, int right)
        {
            IList<T> copyList = new List<T>();

            for (int i = left; i <= right; i++)
            {
                copyList.Add(list[i]);
            }

            return copyList;
        }
    }

}