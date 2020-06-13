using Algorithms.Numeric;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
 
using System.Diagnostics;
namespace UnitTest.AlgorithmsTests
{
    public class LinearSieveOfEratosthenesTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public LinearSieveOfEratosthenesTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        private const int MaxNumber = 100;

        [Fact]
        public void DoTests()
        {
            var results = LinearSieveOfEratosthenes.GeneratePrimesUpTo(MaxNumber);
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
        public void TestsForEmpty()
        {
            var results = LinearSieveOfEratosthenes.GeneratePrimesUpTo(0);
            Assert.NotNull(results);
            Assert.False(results.Any());

            var results2 = LinearSieveOfEratosthenes.GeneratePrimesUpTo(-100);
            Assert.NotNull(results2);
            Assert.False(results2.Any());
        }
    }
}