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
			PriorityQueue<int, int, int> priorityQueue = new PriorityQueue<int, int, int> (10);

			for (int i = 0; i < 20; ++i)
			{
				priorityQueue.Enqueue (i, i, (i/3) + 1);
			}

			var highest = priorityQueue.Dequeue ();
        }

    }

}
