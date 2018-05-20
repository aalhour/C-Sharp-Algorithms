using DataStructures.Heaps;
using System;
using System.Diagnostics;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class PriorityQueuesTest
    {
        internal class Process : IComparable<Process>
        {
            public Process(int id, Action action, string desc)
            {
                Id = id;
                Action = action;
                Description = desc;
            }

            public int Id { get; set; }
            public Action Action { get; set; }
            public string Description { get; set; }

            public int CompareTo(Process other)
            {
                if (other == null)
                {
                    return -1;
                }

                return Id.CompareTo(other.Id);
            }
        }

        [Fact]
        public static void DoTest()
        {
            //
            // KEYED PRIORITY QUEUE
            var keyedPriorityQueue = new PriorityQueue<int, int, int>(10);

            for (var i = 0; i < 20; ++i)
            {
                keyedPriorityQueue.Enqueue(i, i, i / 3 + 1);
            }

            var keyedPQHighest = keyedPriorityQueue.Dequeue();
            Debug.Assert(keyedPQHighest == 18, "Wrong node!");

            //
            // Integer-index priority-queue
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var priorityQueue = new MinPriorityQueue<string, int>((uint)alphabet.Length);

            for (var i = 0; i < alphabet.Length; ++i)
            {
                priorityQueue.Enqueue(alphabet[i].ToString(), i / 3 + 1);
            }

            var PQMin = priorityQueue.DequeueMin();
            Debug.Assert(PQMin == "a", "Wrong node!");

            //
            // Processes with priorities
            var sysProcesses = new MinPriorityQueue<Process, int>();

            var process1 = new Process(
                432654,
                () => Console.Write("I am Process #1.\r\n1 + 1 = " + (1 + 1)),
                "Process 1");

            var process2 = new Process(
                123456,
                () => Console.Write("Hello, World! I am Process #2"),
                "Process 2");

            var process3 = new Process(
                345098,
                () => Console.Write("I am Process #3"),
                "Process 3");

            var process4 = new Process(
                109875,
                () => Console.Write("I am Process #4"),
                "Process 4");

            var process5 = new Process(
                13579,
                () => Console.Write("I am Process #5"),
                "Process 5");

            var process6 = new Process(
                24680,
                () => Console.Write("I am Process #6"),
                "Process 6");

            sysProcesses.Enqueue(process1, 1);
            sysProcesses.Enqueue(process2, 10);
            sysProcesses.Enqueue(process3, 5);
            sysProcesses.Enqueue(process4, 7);
            sysProcesses.Enqueue(process5, 3);
            sysProcesses.Enqueue(process6, 6);

            var leastPriorityProcess = sysProcesses.PeekAtMinPriority();
            Assert.True(leastPriorityProcess.Id == process1.Id, "Wrong process!");

            sysProcesses.DequeueMin();

            leastPriorityProcess = sysProcesses.PeekAtMinPriority();
            Assert.True(leastPriorityProcess.Id == process5.Id, "Wrong process!");

            sysProcesses.DequeueMin();

            leastPriorityProcess = sysProcesses.PeekAtMinPriority();
            Assert.True(leastPriorityProcess.Id == process3.Id, "Wrong process!");

            sysProcesses.DequeueMin();

            leastPriorityProcess = sysProcesses.PeekAtMinPriority();
            Assert.True(leastPriorityProcess.Id == process6.Id, "Wrong process!");

            leastPriorityProcess.Action();
        }
    }
}

