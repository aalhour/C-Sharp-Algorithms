using System;
using System.Diagnostics;
using System.Collections.Generic;

using Algorithms.Sorting;

namespace C_Sharp_Algorithms.AlgorithmsTests
{
    public static class BinarySearchTreeSorterTest
    {
        public static void DoTest()
        {
            List<int> numbers = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            numbers.UnbalancedBSTSort<int>();
        }
    }
}
