using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Heaps;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class BinomialHeapsTest
    {
        public static void DoTest()
        {
            int i = 37;
            int numberOfItems = 100000;
            BinomialMinHeap<int> firstHeap = new BinomialMinHeap<int>();
            BinomialMinHeap<int> secondHeap = new BinomialMinHeap<int>();
            BinomialMinHeap<int> thirdHeap = new BinomialMinHeap<int>();

            Console.WriteLine("Begin Test.");

            for (i = 37; i != 0; i = (i + 37) % numberOfItems)
            {
                if (i % 2 == 0)
                    secondHeap.Add(i);
                else
                    firstHeap.Add(i);
            }

            firstHeap.Merge(secondHeap);
            thirdHeap = firstHeap;

            for (i = 1; i <= thirdHeap.Count; i++)
            {
                var min = thirdHeap.ExtractMin();
                Debug.Assert(min == i, "WRONG MIN");
            }

            Debug.Assert(secondHeap.IsEmpty, "SECOND HEAP SHOULD BE EMPTY");

            Console.WriteLine("END OF TEST");

            Console.ReadLine();
        }
    }
}
