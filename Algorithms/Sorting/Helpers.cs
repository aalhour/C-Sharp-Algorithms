using System;
using System.Collections.Generic;

namespace Algorithms
{
	public static class Helpers
	{
		//
		// Swap two values in an IList<T> collection given their indexes.
		public static void Swap<T>(this IList<T> list, int index1, int index2)
		{
			if (index1 == index2)   //This check is not required but Partition function may make many calls so its for perf reason
				return;

			var temp = list[index1];
			list[index1] = list[index2];
			list[index2] = temp;
		}
	}
}

