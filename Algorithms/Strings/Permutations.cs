/***
 * Algorithms for computing the list of permutations of a string and checking if two strings are anagrams of each other.
 */

using System;
using System.Collections.Generic;

namespace Algorithms.Strings
{
    public static class Permutations
    {
        /// <summary>
        /// Private Helper. Computes permutations recursively for string source.
        /// </summary>
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
        public static HashSet<string> ComputeDistinct(string source)
        {
            return _permutations(source);
        }

        /// <summary>
        /// Determines if the Other string is an anargram of the Source string.
        /// </summary>
        public static bool IsAnargram(string source, string other)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(other))
                return false;
            if (source.Length != other.Length)
                return false;
            if (source.Equals(other, StringComparison.Ordinal))
                return true;

            int len = source.Length;
            // Hash set which will contains all the characters present in input source.
            var hashSetSourceChars = new HashSet<char>();
            var hashSetOtherChars = new HashSet<char>();
            for (int i = 0; i < len; i++)
            {
                hashSetSourceChars.Add(source[i]);
                hashSetOtherChars.Add(other[i]);
            }
            for (int i = 0; i < len; i++)
            {
                // Inputs are not Anargram if characers from *other are not present in *source.
                if (!hashSetSourceChars.Contains(other[i])) return false;
                if (!hashSetOtherChars.Contains(source[i])) return false;
            }
            return true;
        }
    }
}

