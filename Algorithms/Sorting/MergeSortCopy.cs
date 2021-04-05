using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Sorting
{
    public static class MergeSortCopy
    {
        public static List<T> MergeSortCopySort<T>(this List<T> collection, Comparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;

            return InternalMergeSortCopySort(collection, comparer);
        }

        // Implements the recursive merge-sort algorithm
        private static List<T> InternalMergeSortCopySort<T>(List<T> collection, Comparer<T> comparer)
        {
            if (collection.Count < 2)
            {
                return collection;
            }

            //find the middle point 
            int middleIndex = collection.Count / 2;

            //divide it into 2 halves
            var leftCollection = collection.GetRange(0, middleIndex);
            var rightCollection = collection.GetRange(middleIndex, collection.Count - middleIndex);

            //sort the left half
            leftCollection = InternalMergeSortCopySort<T>(leftCollection, comparer);
            //sort the right half
            rightCollection = InternalMergeSortCopySort<T>(rightCollection, comparer);

            //merge the sorted two halves into one
            return MergeSortCopyMerge<T>(leftCollection, rightCollection, comparer);
        }

        private static List<T> MergeSortCopyMerge<T>(List<T> leftCollection, List<T> rightCollection, Comparer<T> comparer)
        {
            int index;
            int left = 0, right = 0;
            int length = leftCollection.Count + rightCollection.Count;

            List<T> result = new List<T>(length); //return result 
            
            for (index = 0; right < rightCollection.Count && left < leftCollection.Count; ++index)
            {
                //check right <= left; if true, add right to result else add left to result
                if (comparer.Compare(rightCollection[right], leftCollection[left]) <= 0)
                {
                    result.Insert(index, rightCollection[right++]); 
                }
                else
                {
                    result.Insert(index, leftCollection[left++]);
                }
            }

            // At most one of left and right might still have elements left
            // nb index ++ and also right ++
            while (right < rightCollection.Count)
            {
                result.Insert(index++, rightCollection[right++]);
            }
            while (left < leftCollection.Count)
            {
                result.Insert(index++, leftCollection[left++]);
            }

            return result;
        }
    }
}
