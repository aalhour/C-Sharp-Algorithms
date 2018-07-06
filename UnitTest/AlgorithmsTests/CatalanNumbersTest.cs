using System.Collections.Generic;
using System.Numerics;
using Algorithms.Numeric;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class CatalanNumbersTest
    {
        [Fact]
        public void DoTest()
        {
            var list = CatalanNumbers.GetRange(0, 100);
            var list2 = new List<BigInteger>();

            // TRY CALCULATING FROM Bin.Coeff.
            for (uint i = 0; i < list.Count; ++i)
            {
                var catalanNumber = CatalanNumbers.GetNumberByBinomialCoefficients(i);
                list2.Add(catalanNumber);

                Assert.True(list[(int)i] == list2[(int)i], "Wrong calculation.");
            }
        }

        // Values retrieved from https://oeis.org/A000108/list.
        [Theory]
        [InlineData(0, "1")]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(3, "5")]
        [InlineData(4, "14")]
        [InlineData(5, "42")]
        [InlineData(6, "132")]
        [InlineData(7, "429")]
        [InlineData(8, "1430")]
        [InlineData(9, "4862")]
        [InlineData(10, "16796")]
        [InlineData(11, "58786")]
        [InlineData(12, "208012")]
        [InlineData(13, "742900")]
        [InlineData(14, "2674440")]
        [InlineData(15, "9694845")]
        [InlineData(16, "35357670")]
        [InlineData(17, "129644790")]
        [InlineData(18, "477638700")]
        [InlineData(19, "1767263190")]
        [InlineData(20, "6564120420")]
        [InlineData(21, "24466267020")]
        [InlineData(22, "91482563640")]
        [InlineData(23, "343059613650")]
        [InlineData(24, "1289904147324")]
        [InlineData(25, "4861946401452")]
        [InlineData(26, "18367353072152")]
        [InlineData(27, "69533550916004")]
        [InlineData(28, "263747951750360")]
        [InlineData(29, "1002242216651368")]
        [InlineData(30, "3814986502092304")]
        public void ManuallyVerifyCatalanNumber(uint rank, string value)
        {
            // This conversion seems to be necessary because as of this
            // writing xunit doesn't behave well with BigInteger inline
            // data values.
            var bigint = BigInteger.Parse(value);

            Assert.True(CatalanNumbers.GetNumber(rank) == bigint);
        }
    }
}