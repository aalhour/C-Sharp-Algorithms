using System.Collections.Generic;
using DataStructures.Lists;

namespace Algorithms.Common
{
    public static class Helpers
    {
        /// <summary>
        /// Swaps two values in an IList<T> collection given their indexes.
        /// </summary>
        public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            if (list.Count < 2 || firstIndex == secondIndex)   //This check is not required but Partition function may make many calls so its for perf reason
                return;

            var temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        /// <summary>
        /// Swaps two values in an ArrayList<T> collection given their indexes.
        /// </summary>
        public static void Swap<T>(this ArrayList<T> list, int firstIndex, int secondIndex)
        {
            if (list.Count < 2 || firstIndex == secondIndex)   //This check is not required but Partition function may make many calls so its for perf reason
                return;

            var temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        /// <summary>
        /// Populates a collection with a specific value.
        /// </summary>
        public static void Populate<T>(this IList<T> collection, T value)
        {
            if (collection == null)
                return;

            for (int i = 0; i < collection.Count; i++)
            {
                collection[i] = value;
            }
        }
    }
}

