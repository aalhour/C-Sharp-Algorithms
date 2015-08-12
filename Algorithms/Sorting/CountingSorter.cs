using System;
using System.Collections.Generic;

using Algorithms.Common;

namespace Algorithms.Sorting
{
    public static class CountingSorter
    {
        public static void CountingSort(this int[] array)
        {
            if (array == null || array.Length == 0)
                return;

            //
            // Get the maximum number in array.
            int maxK = 0;
            int index = 0;
            while (true)
            {
                if (index >= array.Length)
                    break;

                maxK = Math.Max(maxK, array[index] + 1);
                index++;
            }

            // The array of keys, used to sort the original array.
            int[] keys = new int[maxK];
            keys.Populate(0); // populate it with zeros

            // Assign the keys
            for (int i = 0; i < array.Length; ++i)
            {
                keys[array[i]] += 1;
            }

            // Reset index.
            index = 0;

            // Sort the elements
            for (int j = 0; j < keys.Length; ++j)
            {
                var val = keys[j];

                if (val > 0)
                {
                    while (val-- > 0)
                    {
                        array[index] = j;
                        index++;
                    }
                }
            }
        }
    }
}
