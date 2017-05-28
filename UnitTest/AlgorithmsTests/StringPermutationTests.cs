using System;
using System.Diagnostics;

using Algorithms.Strings;

namespace UnitTest.AlgorithmsTests
{
    public static class StringPermutationTests
    {
        public static void DoTest()
        {
            var alphabets = "abcdefg";

            var permutations = Permutations.ComputeDistinct(alphabets);
            Debug.Assert(permutations.Count == 720);

            var one = "abcdefg";
            var two = "dabcgfe";
            Debug.Assert(Permutations.IsAnargram(one, two) == true);

            one = "123456";
            two = "789123";
            Debug.Assert(Permutations.IsAnargram(one, two) == false);

            one = "abc";
            two = "bbb";
            Debug.Assert(Permutations.IsAnargram(one, two) == false);

            one = "acdf";
            two = "bcde";
            Debug.Assert(Permutations.IsAnargram(one, two) == false);

            one = "I am legion";    // L is small
            two = "Legion I am";    // L is capital
            Debug.Assert(Permutations.IsAnargram(one, two) == false);

            one = "I am legion";    // L is small
            two = "legion I am";    // L is small
            Debug.Assert(Permutations.IsAnargram(one, two) == true);
        }
    }
}
