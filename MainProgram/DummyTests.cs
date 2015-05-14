using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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

            var first = listOfNumbers.Append(10);
            var second = listOfNumbers.Append(124);
            var third = listOfNumbers.Append(654);
            var fourth = listOfNumbers.Append(8);
            var fifth = listOfNumbers.Append(127485693);
            var sixth = listOfNumbers.Append(34);
            var seventh = listOfNumbers.Append(823);

            Console.WriteLine(listOfNumbers.ToReadable());

            listOfNumbers.Remove(first);
            listOfNumbers.Remove(first);
            listOfNumbers.Remove(first);

            Console.WriteLine("Removed 1st: " + listOfNumbers.ToReadable());

            listOfNumbers.Remove(fourth);
            listOfNumbers.Remove(fifth);

            Console.WriteLine("Removed 4th & 5th: " + listOfNumbers.ToReadable());

            listOfNumbers.Remove(sixth);

            Console.WriteLine("Removed 6th: " + listOfNumbers.ToReadable());

            listOfNumbers.Remove(seventh);

            Console.WriteLine("Removed 7th: " + listOfNumbers.ToReadable());

            listOfNumbers.Remove(first);

            Console.WriteLine("Try remove old 1st: " + listOfNumbers.ToReadable());

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
