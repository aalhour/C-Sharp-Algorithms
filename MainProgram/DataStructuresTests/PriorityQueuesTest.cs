using System;
using System.Diagnostics;

using DataStructures;

namespace C_Sharp_Algorithms
{
	public static class PriorityQueuesTest
	{
        internal class Process
        {
            public int Id { get; set; }
            public Action Action { get; set; }
            public string Description { get; set; }

            public Process(int id, Action action, string desc)
            {
                Id = id;
                Action = action;
                Description = desc;
            }
        }

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
			Debug.Assert (keyedPQHighest == 18, "Wrong node!");

			//
			// Integer-index priority-queue
			string alphabet = "abcdefghijklmnopqrstuvwxyz";
			PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int> (alphabet.Length);

			for (int i = 0; i < alphabet.Length; ++i)
			{
				priorityQueue.Enqueue (alphabet[i].ToString(), (i/3) + 1);
			}

			var PQHighest = priorityQueue.Dequeue ();
			Debug.Assert (PQHighest == "y", "Wrong node!");

            //
            // Processes with priorities
            PriorityQueue<Process, int> sysProcesses = new PriorityQueue<Process, int>();

            var process1 = new Process(
                id: 432654, 
                action: new Action(() => System.Console.Write("I am Process #1.\r\n1 + 1 = " + (1 + 1))), 
                desc: "Process 1");

            var process2 = new Process(
                id: 123456,
                action: new Action(() => System.Console.Write("Hello, World! I am Process #2")),
                desc: "Process 2");

            var process3 = new Process(
                id: 345098,
                action: new Action(() => System.Console.Write("I am Process #3")),
                desc: "Process 3");

            var process4 = new Process(
                id: 109875,
                action: new Action(() => System.Console.Write("I am Process #4")),
                desc: "Process 4");

            var process5 = new Process(
                id: 13579,
                action: new Action(() => System.Console.Write("I am Process #5")),
                desc: "Process 5");

            var process6 = new Process(
                id: 24680,
                action: new Action(() => System.Console.Write("I am Process #6")),
                desc: "Process 6");

            sysProcesses.Enqueue(process1, 1);
            sysProcesses.Enqueue(process2, 10);
            sysProcesses.Enqueue(process3, 5);
            sysProcesses.Enqueue(process4, 7);
            sysProcesses.Enqueue(process5, 3);
            sysProcesses.Enqueue(process6, 6);

            var highestPriorityProcess = sysProcesses.PeekAtHighestPriority();
            Debug.Assert(highestPriorityProcess.Id == process2.Id, "Wrong process!");

            sysProcesses.Dequeue();

            highestPriorityProcess = sysProcesses.PeekAtHighestPriority();
            Debug.Assert(highestPriorityProcess.Id == process4.Id, "Wrong process!");

            sysProcesses.Dequeue();

            highestPriorityProcess = sysProcesses.PeekAtHighestPriority();
            Debug.Assert(highestPriorityProcess.Id == process6.Id, "Wrong process!");

            sysProcesses.Dequeue();

            highestPriorityProcess = sysProcesses.PeekAtHighestPriority();
            Debug.Assert(highestPriorityProcess.Id == process3.Id, "Wrong process!");

            highestPriorityProcess.Action();
		}
	}
}

