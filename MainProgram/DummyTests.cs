using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using DataStructures;

namespace C_Sharp_Algorithms
{
    /// <summary>
    /// Functions that use the classes and use them with dummy data.
    /// These are used as simple functionality tests.
    /// </summary>
    public static class DummyTests
    {
        public static void Test_SinglyLinkedList()
        {
            SLList<int> listOfNumbers = new SLList<int>();

            listOfNumbers.Append(10);
            listOfNumbers.Append(124);
            listOfNumbers.Append(654);
            listOfNumbers.Append(8);
            listOfNumbers.Append(127485693);
            listOfNumbers.Append(34);
            listOfNumbers.Append(823);

            Console.WriteLine(listOfNumbers.ToReadable());

            listOfNumbers.RemoveAt(0);
            Console.WriteLine("Removed 1st: " + listOfNumbers.ToReadable());

            listOfNumbers.RemoveAt(3);
            listOfNumbers.RemoveAt(4);
            Console.WriteLine("Removed 3rd & 4th: " + listOfNumbers.ToReadable());

			listOfNumbers.RemoveAt(2);
            Console.WriteLine("Removed 3rd: " + listOfNumbers.ToReadable());

			listOfNumbers.RemoveAt(2);

            Console.WriteLine("Removed 3rd: " + listOfNumbers.ToReadable());

			listOfNumbers.RemoveAt(0);
            Console.WriteLine("Remove 1st: " + listOfNumbers.ToReadable());

			// Capture the console.
            Console.ReadLine();
        }

        public static void Test_DoublyLinkedList()
        {
            DLList<string> listOfNumbers = new DLList<string>();

            var w1 = listOfNumbers.Append("fst");
            var w2 = listOfNumbers.Append("sec");
            var w3 = listOfNumbers.Append("trd");
            var w4 = listOfNumbers.Append("for");
            var w5 = listOfNumbers.Append("fft");
            var w6 = listOfNumbers.Append("sxt");
            var w7 = listOfNumbers.Append("svn");
            var w8 = listOfNumbers.Append("egt");

            // Print
            Console.WriteLine(listOfNumbers.ToReadable());

            // Remove w1 - try to cause a bug
            listOfNumbers.Remove(w1);
            listOfNumbers.Remove(w1);
            listOfNumbers.Remove(w1);
            Console.WriteLine("Removed 1st:\r\n" + listOfNumbers.ToReadable());

            // Remove w5 and w6
            listOfNumbers.Remove(w5);
            listOfNumbers.Remove(w6);
            Console.WriteLine("Removed 5th & 6th:\r\n" + listOfNumbers.ToReadable());

            // Remove w7
            listOfNumbers.Remove(w7);
            Console.WriteLine("Removed 7th:\r\n" + listOfNumbers.ToReadable());

            // Remove w8 - last
            listOfNumbers.Remove(w8);
            Console.WriteLine("Removed 8th (last):\r\n" + listOfNumbers.ToReadable());

            // Try to remove w1 again
            listOfNumbers.Remove(w1);
            Console.WriteLine("Try remove old 1st:\r\n" + listOfNumbers.ToReadable());

            // Print count
            Console.WriteLine("Count: " + listOfNumbers.Count);

            Console.ReadLine();
        }
    }
}
