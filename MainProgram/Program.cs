using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using C_Sharp_Algorithms.AlgorithmsTests;
using C_Sharp_Algorithms.DataStructuresTests;
using Algorithms;
using DataStructures;

namespace C_Sharp_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
			int[] numbersArray = new int[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
			MergeSort (numbersArray);
        }


		public static int[] MergeSort(int[] array)
		{
			return mergeSort (array, 0, array.Length - 1);
		}
		
		private static int[] mergeSort(int[] array, int startIndex, int endIndex)
		{
			if (endIndex - startIndex < 1)
			{
				return array;
			}
			else if(endIndex == startIndex + 1)
			{
				if (array [endIndex] < array [startIndex])
				{
					array.Swap (endIndex, startIndex);
				}

				return array;
			}
			else
			{
				int midIndex = (endIndex - startIndex) / 2;
				var array1 = mergeSort (array, startIndex, midIndex);
				var array2 = mergeSort (array, midIndex + 1, endIndex);

				return merge (array1, array2);
			}
		}

		private static int[] merge(int[] array1, int[] array2)
		{
			int left = 0, right = 0, 
			length = array1.Length + array2.Length;

			int[] resultArray = new int[length];

			for (int i = 0; i < resultArray.Length; ++i)
			{
				if (right < array2.Length && array2 [right] <= array1 [left])
				{
					resultArray [i] = array2 [right];
					right++;
				}
				else
				{
					resultArray [i] = array1 [left];
					left++;

					if (left == array1.Length)
						break;
				}
			}

			return resultArray;
		}
    }
}
