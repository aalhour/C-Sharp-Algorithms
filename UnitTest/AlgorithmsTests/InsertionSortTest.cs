using System.Collections.Generic;
using DataStructures.Lists;
using Algorithms.Sorting;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class InsertionSortTest
    {
        [Fact]
        public static void DoTest()
        {
            var list1 = new ArrayList<int>
            {
                23,
                42,
                4,
                16,
                8,
                15,
                9,
                55,
                0,
                34,
                12,
                2
            };
            var list2 = new List<long>
            {
                23,
                42,
                4,
                16,
                8,
                15,
                9,
                55,
                0,
                34,
                12,
                2
            };

            // sort both lists 
            list1.InsertionSort();
            list2.InsertionSort();

            bool isListEqual = true;
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    isListEqual = false;
                    break;
                }
            }

            Assert.True(isListEqual);
        }
    }
}

