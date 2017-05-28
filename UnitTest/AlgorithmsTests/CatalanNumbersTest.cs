using Algorithms.Numeric;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class CatalanNumbersTest
    {
        [Fact]
        public static void DoTest()
        {
            var list = CatalanNumbers.GetRange(0, 100);
            var list2 = new List<ulong>();

            // TRY CALCULATING FROM Bin.Coeff.
            for (uint i = 0; i < list.Count; ++i)
            {
                var catalanNumber = CatalanNumbers.GetNumberByBinomialCoefficients(i);
                list2.Add(catalanNumber);

                Assert.True(list[(int)i] == list2[(int)i], "Wrong calculation.");
            }

            // TODO: Remove.
            // PRINT NUMBERS
            for (uint i = 0; i < list.Count; ++i)
            {
                Console.Write(list[(int)i] + ", ");
            }

            Console.ReadLine();
        }
    }
}
