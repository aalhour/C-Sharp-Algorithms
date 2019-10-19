using Algorithms.Numeric;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class GreatestCommonDivisorTests
    {
        [Fact]
        public void FindGCD_BothAreZero()
        {
            var gcdEuclidean = GreatestCommonDivisor.FindGCDEuclidean(0, 0);
            Assert.Equal(0, gcdEuclidean);

            var gcdStein = GreatestCommonDivisor.FindGCDStein(0, 0);
            Assert.Equal(0, gcdStein);
        }

        [Theory]
        [InlineData(0, 4, 4)]
        [InlineData(0, 9, 9)]
        [InlineData(0, -14, 14)]
        [InlineData(0, -99, 99)]
        public void FindGCD_FirstIsZero(int a, int b, int expected)
        {
            var gcdEuclidean = GreatestCommonDivisor.FindGCDEuclidean(a, b);
            Assert.Equal(expected, gcdEuclidean);

            var gcdStein = GreatestCommonDivisor.FindGCDStein(a, b);
            Assert.Equal(expected, gcdStein);
        }

        [Theory]
        [InlineData(4, 0, 4)]
        [InlineData(9, 0, 9)]
        [InlineData(-14, 0, 14)]
        [InlineData(-99, 0, 99)]
        public void FindGCD_SecondIsZero(int a, int b, int expected)
        {
            var gcdEuclidean = GreatestCommonDivisor.FindGCDEuclidean(a, b);
            Assert.Equal(expected, gcdEuclidean);

            var gcdStein = GreatestCommonDivisor.FindGCDStein(a, b);
            Assert.Equal(expected, gcdStein);
        }

        [Theory]
        [InlineData(2, 4, 2)]
        [InlineData(27, 9, 9)]
        [InlineData(27, 14, 1)]
        [InlineData(9, 6, 3)]
        public void FindGCD_BothNumberArePositive(int a, int b, int expected)
        {
            var gcdEuclidean = GreatestCommonDivisor.FindGCDEuclidean(a, b);
            Assert.Equal(expected, gcdEuclidean);

            var gcdStein = GreatestCommonDivisor.FindGCDStein(a, b);
            Assert.Equal(expected, gcdStein);
        }

        [Theory]
        [InlineData(-2, -4, 2)]
        [InlineData(-27, -9, 9)]
        [InlineData(-27, -14, 1)]
        [InlineData(-9, -6, 3)]
        public void FindGCD_BothNumberAreNegative(int a, int b, int expected)
        {
            var gcdEuclidean = GreatestCommonDivisor.FindGCDEuclidean(a, b);
            Assert.Equal(expected, gcdEuclidean);

            var gcdStein = GreatestCommonDivisor.FindGCDStein(a, b);
            Assert.Equal(expected, gcdStein);
        }

        [Theory]
        [InlineData(2, -4, 2)]
        [InlineData(-27, 9, 9)]
        [InlineData(27, -14, 1)]
        [InlineData(-9, 6, 3)]
        public void FindGCD_CombinationPositiveAndNegative(int a, int b, int expected)
        {
            var gcdEuclidean = GreatestCommonDivisor.FindGCDEuclidean(a, b);
            Assert.Equal(expected, gcdEuclidean);

            var gcdStein = GreatestCommonDivisor.FindGCDStein(a, b);
            Assert.Equal(expected, gcdStein);
        }
    }
}
