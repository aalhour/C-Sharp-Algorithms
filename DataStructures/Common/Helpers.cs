using System;
using System.Text;
using System.Collections.Generic;

using DataStructures.Lists;

namespace DataStructures.Common
{
	public static class Helpers
	{
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
            if (string.IsNullOrEmpty(text) || fillerCharacter == null)
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
	}
}

