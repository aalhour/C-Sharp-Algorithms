using System;
using System.Collections.Generic;

using Algorithms.Common;

namespace Algorithms.Sorting
{
    public static class MergeSorter
    {
        //
        // Public merge-sort API
        public static List<T> MergeSort<T>(this List<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;

            return InternalMergeSort(collection, 0, collection.Count - 1, comparer);
        }


        //
        // Private static method
        // Implements the recursive merge-sort algorithm
        private static List<T> InternalMergeSort<T>(List<T> collection, int startIndex, int endIndex, Comparer<T> comparer)
        {
            if (collection.Count < 2)
            {
                return collection;
            }
            else if (collection.Count == 2)
            {
                if (comparer.Compare(collection[endIndex], collection[startIndex]) < 0)
                {
                    collection.Swap(endIndex, startIndex);
                }

                return collection;
            }
            else
            {
                int midIndex = collection.Count / 2;

                var leftCollection = collection.GetRange(startIndex, midIndex);
                var rightCollection = collection.GetRange(midIndex, (endIndex - midIndex) + 1);

                leftCollection = InternalMergeSort<T>(leftCollection, 0, leftCollection.Count - 1, comparer);
                rightCollection = InternalMergeSort<T>(rightCollection, 0, rightCollection.Count - 1, comparer);

                return InternalMerge<T>(leftCollection, rightCollection, comparer);
            }
        }


        //
        // Private static method
        // Implements the merge function inside the merge-sort
        private static List<T> InternalMerge<T>(List<T> leftCollection, List<T> rightCollection, Comparer<T> comparer)
        {
            int left = 0;
            int right = 0;
            int index;
            int length = leftCollection.Count + rightCollection.Count;

            List<T> result = new List<T>(length);

            for (index = 0; index < length; ++index)
            {
                if (right < rightCollection.Count && comparer.Compare(rightCollection[right], leftCollection[left]) <= 0) // rightElement <= leftElement
                {
                    //resultArray.Add(rightCollection[right]);
                    result.Insert(index, rightCollection[right]);
                    right++;
                }
                else
                {
                    //result.Add(leftCollection[left]);
                    result.Insert(index, leftCollection[left]);
                    left++;

                    if (left == leftCollection.Count)
                        break;
                }
            }

            //
            // Either one might have elements left
            int rIndex = index + 1;
            int lIndex = index + 1;

            while (right < rightCollection.Count)
            {
                result.Insert(rIndex, rightCollection[right]);
                rIndex++;
                right++;
            }

            while (left < leftCollection.Count)
            {
                result.Insert(lIndex, leftCollection[left]);
                lIndex++;
                left++;
            }

            return result;
        }
    }
}

