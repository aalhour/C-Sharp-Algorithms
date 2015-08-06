using System;
using System.Collections.Generic;

namespace Algorithms
{
	public static class Permutations
	{
		/// <summary>
		/// Private Helper. Computes permutations recursively for string source.
		/// </summary>
		/// <param name="s">S.</param>
		private static HashSet<string> _permutations(string source) 
		{
			var perms = new HashSet<string>();

			if(source.Length == 1)
			{
				perms.Add(source);
			} 
			else if (source.Length > 1) 
			{
				int lastIndex = source.Length - 1;
				string lastChar = Convert.ToString(source[lastIndex]);
				string substring = source.Substring(0, lastIndex);
				perms = _mergePermutations(_permutations(substring), lastChar);
			}

			return perms;
		}

		/// <summary>
		/// Private helper. Merges a set of permutations with a character.
		/// </summary>
		private static HashSet<string> _mergePermutations(HashSet<string> permutations, string character) 
		{
			var merges = new HashSet<string>();

			foreach(var perm in permutations) {
				for(int i = 0; i < perm.Length; ++i) {
					var newMerge = perm.Insert(i, character);

					if(!merges.Contains(newMerge))
						merges.Add(newMerge);
				}
			}

			return merges;
		}

		/// <summary>
		/// Computes the permutations of a string.
		/// </summary>
		public static HashSet<string> Compute(string Source)
		{
			return _permutations (Source);
		}

		/// <summary>
		/// Determines if the Other string is an anargram of the Source string.
		/// </summary>
		public static bool IsAnargram(string Source, string Other)
		{
			bool nullCheck = false;
			int sourceCharSum = 0, otherCharSum = 0;
			bool isSourceNull = string.IsNullOrEmpty (Source);
			bool isOtherNull = string.IsNullOrEmpty (Other);

			// Null values check
			nullCheck |= (isSourceNull && isOtherNull) || (isSourceNull && !isOtherNull) || (!isSourceNull && isOtherNull);
				
			if (nullCheck == true)
				return false;
			else if (Source.Length != Other.Length)
				return false;
			else if (Source == Other)
				return true;

			// Anagram check
			// Calculate the characters sum of both Source and Other
			for (int i = 0; i < Source.Length; ++i) {
				sourceCharSum += Source [i];
				otherCharSum += Other [i];
			}

			// true if sums match; otherwise, false.
			return (sourceCharSum == otherCharSum);
		}
	}
}

