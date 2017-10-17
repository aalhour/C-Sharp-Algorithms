
using Algorithms.Strings;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class StringPermutationTests
    {
        [Fact]
        public static void DoTest()
        {
            var alphabets = "abcdefg";

            var permutations = Permutations.ComputeDistinct(alphabets);
            Assert.True(permutations.Count == 720);

            var one = "abcdefg";
            var two = "dabcgfe";
            Assert.True(Permutations.IsAnargram(one, two) == true);

            one = "123456";
            two = "789123";
            Assert.True(Permutations.IsAnargram(one, two) == false);

            one = "abc";
            two = "bbb";
            Assert.True(Permutations.IsAnargram(one, two) == false);

            one = "acdf";
            two = "bcde";
            Assert.True(Permutations.IsAnargram(one, two) == false);

            one = "I am legion";    // L is small
            two = "Legion I am";    // L is capital
            Assert.True(Permutations.IsAnargram(one, two) == false);

            one = "I am legion";    // L is small
            two = "legion I am";    // L is small
            Assert.True(Permutations.IsAnargram(one, two) == true);
        }
    }
}
