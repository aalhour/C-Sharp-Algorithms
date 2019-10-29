using System.Linq;
using Algorithms.Numeric;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class SieveOfAtkinTest
    {
        [Theory]
        [InlineData("Primes up to -100", -100)]
        [InlineData("Primes up to 2", 2, 2)]
        [InlineData("Primes up to 3", 3, 2, 3)]
        [InlineData("Primes up to 100", 100, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97)]
        public static void GeneratePrimesUpTo(string test, int max, params int[] expectedResult)
        {
            var result = SieveOfAtkin.GeneratePrimesUpTo(max);
            Assert.True(result.SequenceEqual(expectedResult), test);
        }
    }
}