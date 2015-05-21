using System;

using DataStructures;

namespace C_Sharp_Algorithms
{
	public static class PriorityQueuesTest
	{
		public static void DoTest ()
		{
			//
			// KEYED PRIORITY QUEUE
			PriorityQueue<int, int, int> keyedPriorityQueue = new PriorityQueue<int, int, int> (10);

			for (int i = 0; i < 20; ++i)
			{
				keyedPriorityQueue.Enqueue (i, i, (i/3) + 1);
			}

			var keyedPQHighest = keyedPriorityQueue.Dequeue ();


			//
			// Integer-index priority-queue
			string alphabet = "abcdefghijklmnopqrstuvwxyz";
			PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int> (alphabet.Length);

			for (int i = 0; i < alphabet.Length; ++i)
			{
				priorityQueue.Enqueue (alphabet[i].ToString(), (i/3) + 1);
			}

			var PQHighest = priorityQueue.Dequeue ();
		}
	}
}

