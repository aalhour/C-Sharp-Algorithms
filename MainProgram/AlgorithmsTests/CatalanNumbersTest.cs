using System;
using System.Collections.Generic;
using System.Diagnostics;

using Algorithms.Numeric;


namespace C_Sharp_Algorithms.AlgorithmsTests
{
    public static class CatalanNumbersTest
    {
        public static void DoTest()
        {
            var list = CatalanNumbers.GetRange(0, 100);
            var list2 = new List<ulong>();


            // TRY CALCULATING FROM Bin.Coeff.
            for (uint i = 0; i < list.Count; ++i)
            {
                var catalanNumber = CatalanNumbers.GetNumberByBinomialCoefficients(i);
                list2.Add(catalanNumber);

                Debug.Assert(list[(int)i] == list2[(int)i], "Wrong calculation.");
            }


            // PRINT NUMBERS
            for(uint i = 0; i < list.Count; ++i)
            {
                Console.Write(list[(int)i] + ", ");
            }


            Console.ReadLine();
        }

    }

}
