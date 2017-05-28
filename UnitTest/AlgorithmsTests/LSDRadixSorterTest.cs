using System;
using System.Diagnostics;
using System.Collections.Generic;

using Algorithms.Sorting;

namespace UnitTest.AlgorithmsTests
{
    public static class LSDRadixSorterTest
    {
        public static void DoTest()
        {
            //
            // Sort strings
            var name1 = "Mr. Ahmad Alhour";
            var name2 = "Msr. Anna John Hopcraft";
            var number1 = "0987654321";
            var number2 = "000999888777111222333777666555";

            name1 = (name1.LSDRadixSort()).Trim();
            Debug.Assert(name1 == ".AAMadhhlmorru");

            name2 = (name2.LSDRadixSort()).Trim();
            Debug.Assert(name2 == ".AHJMaacfhnnnooprrst");

            number1 = number1.LSDRadixSort();
            Debug.Assert(number1 == "0123456789");

            number2 = number2.LSDRadixSort();
            Debug.Assert(number2 == "000111222333555666777777888999");

            //
            // Sort a list of strings of the same length
            var toBeSorted = new List<string>() { "ahmad", "ahmed", "johny", "ammy1", "ammy2", "zeyad", "aliaa", "aaaaa", "mmmmm", "zzzzz" };
            var alreadySorted = new List<string>() { "aaaaa", "ahmad", "ahmed", "aliaa", "ammy1", "ammy2", "johny", "mmmmm", "zeyad", "zzzzz" };

            toBeSorted.LSDRadixSort(stringFixedWidth: 5);

            for (int i = 0; i < toBeSorted.Count; ++i)
            {
                Debug.Assert(toBeSorted[i] == alreadySorted[i]);
            }

        }

    }

}
