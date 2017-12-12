using Algorithms.Sorting;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class LSDRadixSorterTest
    {
        [Fact]
        public static void DoTest()
        {
            //
            // Sort strings
            var name1 = "Mr. Ahmad Alhour";
            var name2 = "Msr. Anna John Hopcraft";
            var number1 = "0987654321";
            var number2 = "000999888777111222333777666555";

            name1 = (name1.LSDRadixSort()).Trim();
            Assert.True(name1 == ".AAMadhhlmorru");

            name2 = (name2.LSDRadixSort()).Trim();
            Assert.True(name2 == ".AHJMaacfhnnnooprrst");

            number1 = number1.LSDRadixSort();
            Assert.True(number1 == "0123456789");

            number2 = number2.LSDRadixSort();
            Assert.True(number2 == "000111222333555666777777888999");

            //
            // Sort a list of strings of the same length
            var toBeSorted = new List<string>() { "ahmad", "ahmed", "johny", "ammy1", "ammy2", "zeyad", "aliaa", "aaaaa", "mmmmm", "zzzzz" };
            var alreadySorted = new List<string>() { "aaaaa", "ahmad", "ahmed", "aliaa", "ammy1", "ammy2", "johny", "mmmmm", "zeyad", "zzzzz" };

            toBeSorted.LSDRadixSort(stringFixedWidth: 5);

            for (int i = 0; i < toBeSorted.Count; ++i)
            {
                Assert.True(toBeSorted[i] == alreadySorted[i]);
            }

        }

    }

}
