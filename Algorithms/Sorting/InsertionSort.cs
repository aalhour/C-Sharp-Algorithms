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
		/// <summary>
		/// Perform Insertion Sort over the System.Collections.Generic.List<T> collection.
		/// </summary>
		/// <param name="listOfNumbers">System.List of numbers.</param>
		public static void InsertionSort(this List<int> listOfNumbers)
		{
			if (listOfNumbers.Count > 0)
			{
				for (int i = 1; i < listOfNumbers.Count; i++)
				{
					for (int j = i; j > 0; j--)
					{
						if (listOfNumbers [j] < listOfNumbers [j - 1])
						{
							var temp = listOfNumbers [j - 1];
							listOfNumbers [j - 1] = listOfNumbers [j];
							listOfNumbers [j] = temp;
						}
					}
				}
			}//end-if
		}


		/// <summary>
		/// Perform Insertion Sort over the DataStructures.ArrayList<T> collection.
		/// </summary>
		/// <param name="listOfNumbers">List of numbers.</param>
		public static void InsertionSort(this ArrayList<int> listOfNumbers)
		{
			if (listOfNumbers.Count > 0)
			{
				for (int i = 1; i < listOfNumbers.Count; i++)
				{
					for (int j = i; j > 0; j--)
					{
						if (listOfNumbers [j] < listOfNumbers [j - 1])
						{
							var temp = listOfNumbers [j - 1];
							listOfNumbers [j - 1] = listOfNumbers [j];
							listOfNumbers [j] = temp;
						}
					}
				}
			}//end-if
		}


		/// <summary>
		/// Perform Insertion Sort over the generic System.List<T> collection.
		/// </summary>
		/// <param name="list">List of items of type T.</param>
		public static void InsertionSort<T>(this List<T> list)
		{
			if (list.Count > 0)
			{
				var comparer = Comparer<T>.Default;
				InsertionSort (list, comparer);
			}
		}


		/// <summary>
		/// Perform Insertion Sort over the generic System.List<T> collection.
		/// </summary>
		/// <param name="list">List of items of type T.</param>
		/// <param name="comparer">Value Comparer for the list items.</param>
		/// <typeparam name="T">Type of list items.</typeparam>
		public static void InsertionSort<T>(this List<T> list, Comparer<T> comparer)
		{
			if (list.Count > 0)
			{
				for (int i = 1; i < list.Count; i++)
				{
					for (int j = i; j > 0; j--)
					{
						if (comparer.Compare(list [j], list [j - 1]) < 0)
						{
							var temp = list [j - 1];
							list [j - 1] = list [j];
							list [j] = temp;
						}
					}
				}
			}//end-if
		}


		/// <summary>
		/// Perform Insertion Sort over the generic DataStructures.ArrayList<T> collection.
		/// </summary>
		/// <param name="list">List of items of type T.</param>
		public static void InsertionSort<T>(this ArrayList<T> list)
		{
			if (list.Count > 0)
			{
				var comparer = Comparer<T>.Default;
				InsertionSort (list, comparer);
			}
		}


		/// <summary>
		/// Perform Insertion Sort over the generic DataStructures.ArrayList<T> collection.
		/// </summary>
		/// <param name="list">List of items of type T.</param>
		/// <param name="comparer">Value Comparer for the list items.</param>
		/// <typeparam name="T">Type of list items.</typeparam>
		public static void InsertionSort<T>(this ArrayList<T> list, Comparer<T> comparer)
		{
			if (list.Count > 0)
			{
				for (int i = 1; i < list.Count; i++)
				{
					for (int j = i; j > 0; j--)
					{
						if (comparer.Compare(list [j], list [j - 1]) < 0)
						{
							var temp = list [j - 1];
							list [j - 1] = list [j];
							list [j] = temp;
						}
					}
				}
			}//end-if
		}

	}

}

