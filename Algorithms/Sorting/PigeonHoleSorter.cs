using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sorting
{
    /// <summary>
    /// Only support IList<int> Sort
    /// Also called CountSort (not CountingSort)
    /// </summary>
    public static class PigeonHoleSorter
    {
        public static void PigeonHoleSort(this IList<int> collection)
        {
            collection.PigeonHoleSortAscending();
        }

        public static void PigeonHoleSortAscending(this IList<int> collection)
        {
            int min = collection.Min();
            int max = collection.Max();
            int size = max - min + 1;
            int[] holes = new int[size];
            foreach (int x in collection)
            {
                holes[x - min]++;
            }
            
            int i = 0;
            for (int count = 0; count < size; count++)
            {
                while (holes[count]-- > 0)
                {
                    collection[i] = count + min;
                    i++;
                }
            }    
        }

        public static void PigeonHoleSortDescending(this IList<int> collection)
        {
            int min = collection.Min();
            int max = collection.Max();
            int size = max - min + 1;
            int[] holes = new int[size];
            foreach (int x in collection)
            {
                holes[x - min]++;
            }

            int i = 0;
            for (int count = size-1; count >= 0; count--)
            {
                while (holes[count]-- >0)
                {
                    collection[i] = count + min;
                    i++;
                }
            }
        }
    }
}
