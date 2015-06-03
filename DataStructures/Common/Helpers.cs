using System;
using System.Text;
using System.Collections.Generic;

using DataStructures.Lists;

namespace DataStructures.Common
{
	public static class Helpers
	{
        //
        // RETURNS A COPY !!
        public static int[] Primes
        {
            get
            {
                return new int[] {
                    2, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
                    7, 17, 29, 59, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
                    1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
                    17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
                    187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
                    1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369 };
            }
        }

        /// <summary>
        /// Swap two values in an IList<T> collection given their indexes.
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
        /// Swap two values in an ArrayList<T> collection given their indexes.
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
        /// Centralize a text.
        /// </summary>
        public static string PadCenter(this string text, int newWidth, char fillerCharacter = ' ')
        {
            if (string.IsNullOrEmpty(text))
                return text;

            int length = text.Length;
            int charactersToPad = newWidth - length;
            if (charactersToPad < 0) throw new ArgumentException("New width must be greater than string length.", "newWidth");
            int padLeft = charactersToPad / 2 + charactersToPad % 2;
            //add a space to the left if the string is an odd number
            int padRight = charactersToPad / 2;

            StringBuilder resultBuilder = new StringBuilder(newWidth);
            for (int i = 0; i < padLeft; i++) resultBuilder.Insert(i, fillerCharacter);
            for (int i = 0; i < length; i++) resultBuilder.Insert(i + padLeft, text[i]);
            for (int i = newWidth - padRight; i < newWidth; i++) resultBuilder.Insert(i, fillerCharacter);
            return resultBuilder.ToString();
        }

        /// <summary>
        /// Checks if a number is a Prime Number.
        /// </summary>
        public static bool IsPrime(int candidate)
        {
            if ((candidate & 1) != 0) 
            {
                int limit = (int)Math.Sqrt (candidate);
                
                for (int divisor = 3; divisor <= limit; divisor+=2)
                {
                    if ((candidate % divisor) == 0)
                        return false;
                }

                return true;
            }

            return (candidate == 2);
        }

	}

}