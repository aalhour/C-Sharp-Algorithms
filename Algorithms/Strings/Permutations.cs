/***
 * Algorithms for computing the list of permutations of a string and checking if two strings are anagrams of each other.
 */

using System;
using System.Collections.Generic;

using Algorithms.Sorting;

namespace Algorithms.Strings
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

            if (source.Length == 1)
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

            foreach (var perm in permutations)
            {
                for (int i = 0; i < perm.Length; ++i)
                {
                    var newMerge = perm.Insert(i, character);

                    if (!merges.Contains(newMerge))
                        merges.Add(newMerge);
                }
            }

            return merges;
        }

        /// <summary>
        /// Computes the permutations of a string.
        /// </summary>
        public static HashSet<string> ComputeDistinct(string Source)
        {
            return _permutations(Source);
        }

        /// <summary>
        /// Determines if the Other string is an anargram of the Source string.
        /// </summary>
        public static bool IsAnargram(string Source, string Other)
        {
            if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(Other))
                return false;
            else if (Source.Length != Other.Length)
                return false;
            else if (Source == Other)
                return true;

            // Begin the Anagram check
            // Covnert strings to character arrays
            // O(N) space complexity
            var sourceCharArray = Source.ToCharArray();
            var otherCharArray = Other.ToCharArray();

            // Sort both character arrays in place using heapsort
            // O(N log N) operation
            sourceCharArray.HeapSort<char>(Comparer<char>.Default);
            otherCharArray.HeapSort<char>(Comparer<char>.Default);

            // One pass scan
            // O(N) operation
            for (int i = 0; i < sourceCharArray.Length; ++i)
                if (sourceCharArray[i] != otherCharArray[i])
                    return false;

            return true;
        }
    }
}

