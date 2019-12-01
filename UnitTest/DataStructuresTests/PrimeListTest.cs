using DataStructures.Common;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public class PrimeListTest
    {
        [Fact]
        public void DoTest()
        {
            var instance = PrimesList.Instance;
            Assert.Equal(10000, instance.Count);
        }
    }
}
