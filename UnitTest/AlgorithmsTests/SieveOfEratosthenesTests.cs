using Algorithms.Numeric;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class SieveOfEratosthenesTests
    {
        private const int MaxNumber = 100;

        [Fact]
        public void SieveOfEratosthenesGeneratesCorrectResults()
        {
            var results = SieveOfEratosthenes.GeneratePrimesUpTo(MaxNumber);
            Assert.NotNull(results);
            Assert.True(results.Any());
            Assert.Equal(results.Count(), 25);
            Assert.DoesNotContain(1, results);
            Assert.Contains(2, results);
            Assert.Contains(7, results);
            Assert.Contains(23, results);
            Assert.Contains(41, results);
            Assert.Contains(97, results);

        }

        [Fact]
        public void SieveOfEratosthenesReturnsEmptyListWhenGiven0()
        {
            var results = SieveOfEratosthenes.GeneratePrimesUpTo(0);
            Assert.NotNull(results);
            Assert.False(results.Any());
        }
    }
}