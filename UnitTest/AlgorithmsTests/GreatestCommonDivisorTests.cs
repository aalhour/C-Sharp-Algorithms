using Algorithms.Numeric;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class GreatestCommonDivisorTests
    {
        [Fact]
        public void FindGDC_BothAreZero()
        {
            var gdcEuclidean = GreatestCommonDivisor.FindGDCEuclidean(0, 0);
            Assert.Equal(0, gdcEuclidean);

            var gdcStein = GreatestCommonDivisor.FindGDCStein(0, 0);
            Assert.Equal(0, gdcStein);
        }

        [Theory]
        [InlineData(0, 4, 4)]
        [InlineData(0, 9, 9)]
        [InlineData(0, -14, 14)]
        [InlineData(0, -99, 99)]
        public void FindGDC_FirstIsZero(int a, int b, int expected)
        {
            var gdcEuclidean = GreatestCommonDivisor.FindGDCEuclidean(a, b);
            Assert.Equal(expected, gdcEuclidean);

            var gdcStein = GreatestCommonDivisor.FindGDCStein(a, b);
            Assert.Equal(expected, gdcStein);
        }

        [Theory]
        [InlineData(4, 0, 4)]
        [InlineData(9, 0, 9)]
        [InlineData(-14, 0, 14)]
        [InlineData(-99, 0, 99)]
        public void FindGDC_SecondIsZero(int a, int b, int expected)
        {
            var gdcEuclidean = GreatestCommonDivisor.FindGDCEuclidean(a, b);
            Assert.Equal(expected, gdcEuclidean);

            var gdcStein = GreatestCommonDivisor.FindGDCStein(a, b);
            Assert.Equal(expected, gdcStein);
        }

        [Theory]
        [InlineData(2, 4, 2)]
        [InlineData(27, 9, 9)]
        [InlineData(27, 14, 1)]
        [InlineData(9, 6, 3)]
        public void FindGDC_BothNumberArePositive(int a, int b, int expected)
        {
            var gdcEuclidean = GreatestCommonDivisor.FindGDCEuclidean(a, b);
            Assert.Equal(expected, gdcEuclidean);

            var gdcStein = GreatestCommonDivisor.FindGDCStein(a, b);
            Assert.Equal(expected, gdcStein);
        }

        [Theory]
        [InlineData(-2, -4, 2)]
        [InlineData(-27, -9, 9)]
        [InlineData(-27, -14, 1)]
        [InlineData(-9, -6, 3)]
        public void FindGDC_BothNumberAreNegative(int a, int b, int expected)
        {
            var gdcEuclidean = GreatestCommonDivisor.FindGDCEuclidean(a, b);
            Assert.Equal(expected, gdcEuclidean);

            var gdcStein = GreatestCommonDivisor.FindGDCStein(a, b);
            Assert.Equal(expected, gdcStein);
        }

        [Theory]
        [InlineData(2, -4, 2)]
        [InlineData(-27, 9, 9)]
        [InlineData(27, -14, 1)]
        [InlineData(-9, 6, 3)]
        public void FindGDC_CombinationPositiveAndNegative(int a, int b, int expected)
        {
            var gdcEuclidean = GreatestCommonDivisor.FindGDCEuclidean(a, b);
            Assert.Equal(expected, gdcEuclidean);

            var gdcStein = GreatestCommonDivisor.FindGDCStein(a, b);
            Assert.Equal(expected, gdcStein);
        }
    }
}
