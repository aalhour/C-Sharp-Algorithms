using System;
using System.Collections.Generic;

using DataStructures;

namespace Algorithms
{
	/// <summary>
	/// Implements this Insertion Sort algorithm over ArrayLists.
	/// </summary>
	public static class InsertionSorter
	{
		/***
		 * INSERTION SORT FOR SYSTEM ARRAYS and LISTS
		 */

		public static void InsertionSort<T>(this IList<T> list) where T : IComparable<T>
		{	
			var comparer = Comparer<T>.Default;
			InsertionSort (list, comparer);
		}


		public static void InsertionSort<T>(this IList<T> list, IComparer<T> comparer)
		{
			// Null comparers are not allowed.
			if(comparer == null)
			{
				throw new ArgumentNullException ();
			}

			// Do sorting if list is not empty.
			int i, j;
			for (i = 1; i < list.Count; i++)
			{
				T value = list [i];
				j = i - 1;

				while((j >= 0) && (comparer.Compare(list [j], value) > 0))
				{
					list [j + 1] = list [j];
					j--;
				}

				list [j + 1] = value;
			}
		}


		/***
		 * INSERTION SORT FOR ARRAY-LISTS
		 * See DataStructures.ArrayList.cs
		 */

		public static void InsertionSort<T>(this ArrayList<T> list) where T : IComparable<T>
		{
			var comparer = Comparer<T>.Default;
			InsertionSort (list, comparer);
		}


		public static void InsertionSort<T>(this ArrayList<T> list, Comparer<T> comparer)
		{
			// Null comparers are not allowed.
			if(comparer == null)
			{
				throw new ArgumentNullException ();
			}

			for (int i = 1; i < list.Count; i++)
			{
				for (int j = i; j > 0; j--)
				{
					if (comparer.Compare(list [j], list [j - 1]) < 0) //(j)th is less than (j-1)th
					{
						var temp = list [j - 1];
						list [j - 1] = list [j];
						list [j] = temp;
					}
				}
			}
		}

	}

}

