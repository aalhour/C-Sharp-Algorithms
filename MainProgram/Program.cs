using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Algorithms;
using DataStructures;
using C_Sharp_Algorithms.AlgorithmsTests;
using C_Sharp_Algorithms.DataStructuresTests;

namespace C_Sharp_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
			List<int> numbersArray = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
			var sortedArray = numbersArray.MergeSort();
        }

    }

}
